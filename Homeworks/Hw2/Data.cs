using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.Hw2
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

        public Data Clone()
        {
            List<ArrayList> data = new List<ArrayList>();
            foreach(var row in Rows)
            {
                data.Add(row.Cells);
            }
            return new Data("memory", null, new Tuple<List<string>, List<ArrayList>>(Header, data));
        }

        public void Stats()
        {
            var pos = 0;
            foreach(var col in Cols)
            {
                if (!Skip.Contains(pos))
                {
                    string mid = col.IsNum ? Math.Round(col.Num.Mid(), 2).ToString() : col.Sym.Mid();
                    string div = col.IsNum ? Math.Round(col.Num.Div(),2).ToString() : Math.Round(col.Sym.Div(),2).ToString();
                    Console.WriteLine(col.Txt + " mid = " + mid);
                    Console.WriteLine(col.Txt + " div = " + div);
                }
            }
        }
    }
}
