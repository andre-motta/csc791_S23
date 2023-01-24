using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homeworks.src.Hw3
{
    public class Data
    {
        public List<Col> Cols { get; set; }
        public List<int> X { get; set; }
        public List<int> Y { get; set; }
        public List<int> Skip { get; set; }
        public List<Row> Rows { get; set; }
        public List<string> Header { get; set; }

        public Data(string type, string filename = null, Tuple<List<string>, List<ArrayList>> data = null)
        {
            Cols = new List<Col>();
            X = new List<int>();
            Y = new List<int>();
            Skip = new List<int>();
            Rows = new List<Row>();
            Header = new List<string>();
            
            if (type == "file")
            {
                data = Csv.Read(filename);
            }
            CreateCols(data.Item1);
            foreach(var cells in data.Item2)
            {
                Add(cells);
            }
        }
        public void CreateCols(List<string> colNames)
        {
            Header = colNames;
            var pos = 0;
            foreach(var name in colNames)
            {
                
                if(!string.IsNullOrEmpty(name) && (name[name.Length-1]=='-' || name[name.Length - 1] == '+' || name[name.Length - 1] == '!'))
                {
                    Y.Add(pos);
                    Col newCol = new Col(name, pos);
                    Cols.Add(newCol);
                }
                else if (!string.IsNullOrEmpty(name) && (name[name.Length-1]) == 'X')
                {
                    Skip.Add(pos);
                    Col newCol = new Col(name, pos);
                    Cols.Add(newCol);
                }
                else if (!string.IsNullOrEmpty(name))
                {
                    X.Add(pos);
                    Col newCol = new Col(name, pos);
                    Cols.Add(newCol);
                }
                pos++;
            }
        }

        public void Add(ArrayList cells)
        {
            var pos = 0;
            var row = new Row(cells);
            Rows.Add(row);
            foreach(string value in cells)
            {
                if (!Skip.Contains(pos))
                {
                    Cols[pos].Add(value);
                }
                pos++;
            }
        }

        public Data Clone(List<Row> rows = null)
        {
            List<ArrayList> data = new List<ArrayList>();
            if (rows == null)
            {
                foreach (var row in Rows)
                {
                    data.Add(row.Cells);
                }
            }
            else
            {
                foreach (var row in rows)
                {
                    data.Add(row.Cells);
                }
            }
            return new Data("memory", null, new Tuple<List<string>, List<ArrayList>>(Header, data));
        }

        public string Stats(string what, List<int> cols = null, int nPlaces = 2)
        {
            var pos = 0;
            string finalstring = "";
            foreach(var col in Cols)
            {
                if (!Skip.Contains(pos) && cols!=null && cols.Contains(pos))
                {
                    if(what == "mid")
                    {
                        string mid = col.IsNum ? string.Format("{0:0.0}", Math.Round(col.Num.Mid(), nPlaces)) : col.Sym.Mid();
                        finalstring+= ":"+ col.Txt + " " + mid + " ";
                    }
                    else if(what == "div")
                    {
                        string div = col.IsNum ? string.Format("{0:0.0}", Math.Round(col.Num.Div(), nPlaces)) : Math.Round(col.Sym.Div(), 2).ToString();
                        finalstring+= ":" + col.Txt +" "+ div + " ";
                    }
                    else
                    {
                        return "Unrecognized stat function";
                    }
                }
                pos++;
            }
            return finalstring;
        }

        public bool Better(Row row1, Row row2)
        {
            var s1 = 0.0;
            var s2 = 0.0;
            foreach(var pos in Y)
            {
                var res1 = Cols[pos].Norm((string)row1.Cells[pos]);
                var x = res1.Item1 ? res1.Item2 : res1.Item2;
                var w = Cols[pos].IsNum ? Cols[pos].Num.Weight : 1;
                var res2 = Cols[pos].Norm((string)row2.Cells[pos]);
                var y = res2.Item1 ? res2.Item2 : res2.Item2;
                s1 = s1 - Math.Exp(w * (x - y) / ((double)Y.Count));
                s2 = s2 - Math.Exp(w * (y - x) / ((double)Y.Count));
            }
            return (s1 / Y.Count) < (s2 / Y.Count);
        }

        public double Distance(Row row1, Row row2)
        {
            var n = 0;
            var d = 0.0;
            foreach(var pos in X)
            {
                n++;
                d += Cols[pos].Distance((string)row1.Cells[pos], (string)row2.Cells[pos]);
            }
            return Math.Pow((d / n), (1 / Settings.DistCoeff));
        }

        public List<Tuple<Row, double>> Around(Row row1, List<Row> rows)
        {
            return rows.Select(row2 => new Tuple<Row, double>(row2, Distance(row1, row2))).OrderBy(t => t.Item2).ToList();
        }

        public Tuple<List<Row>, List<Row>, Row, Row, Row, double> Half(List<Row> rows = null, Row above = null)
        {
            if (rows == null)
            {
                rows = Rows;
            }
            var some = rows.Many((int)Settings.Sample);
            var A = above != null ? above : rows.Any();
            var B = Around(A, some)[Convert.ToInt32(Math.Floor(Settings.Faraway * some.Count))].Item1;
            var C = Distance(A, B);
            var left = new List<Row>();
            var right = new List<Row>();
            var projections = new List<Tuple<Row, double, double>>();
            foreach (var row in rows)
            {
                var distances = Utils.Cosine(Distance(row, A), Distance(row, B), C);
                projections.Add(new Tuple<Row, double, double>(row, distances.Item1, distances.Item2));
            }
            projections.Sort(delegate (Tuple<Row, double, double> a, Tuple<Row, double, double> b) { return a.Item3.CompareTo(b.Item3); });
            var n = 0;
            var half = Convert.ToInt32(rows.Count / 2);
            Row mid = null;
            foreach (var proj in projections)
            {
                if(n < half)
                {
                    left.Add(proj.Item1);
                    mid = proj.Item1;
                }
                else
                {
                    right.Add(proj.Item1);
                }
                n++;
            }
            return new Tuple<List<Row>, List<Row>, Row, Row, Row, double>(left, right, A, B, mid, C);
        }

        public Node Cluster(List<Row> rows = null, double min = float.PositiveInfinity, Row above = null)
        {
            if (rows == null) rows = Rows;
            if (min == float.PositiveInfinity) min = Math.Pow(rows.Count, Settings.MinStopClusters);
            var node = new Node(Clone(rows), null, null);
            if(rows.Count > 2 * min)
            {
                var result = Half(rows, above);
                var left = Cluster(result.Item1, min, result.Item3);
                var right = Cluster(result.Item2, min, result.Item4);
                node.AddChildren(left, right);
            }
            return node;
        }

        public Node Sway(List<Row> rows = null, double min = float.PositiveInfinity, Row above = null)
        {
            if (rows == null) rows = Rows;
            if (min == float.PositiveInfinity) min = Math.Pow(rows.Count, Settings.MinStopClusters);
            var node = new Node(Clone(rows), null, null);
            if (rows.Count > 2 * min)
            {
                var result = Half(rows, above);
                var l = result.Item1;
                var r = result.Item2;
                var A = result.Item3;
                var B = result.Item4;
                Node children;
                if (!Better(A, B))
                {
                    children = Sway(l, min, A);
                }
                else
                {
                    children = Sway(r, min, B);
                }                
                node.AddChild(children);
            }
            return node;
        }
    }
}
