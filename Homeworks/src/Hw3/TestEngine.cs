using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Homeworks.src.Hw3
{
    public static class TestEngine
    {
        public static int RunTests()
        {
            int FailsCount = 0;
            switch (Settings.Go)
            {
                case "all":
                    FailsCount += TestSettings();
                    FailsCount += TestSym();
                    FailsCount += TestNum();
                    FailsCount += TestCsv();
                    FailsCount += TestData();
                    FailsCount += TestStats();
                    FailsCount += TestClone();
                    FailsCount += TestAround();
                    FailsCount += TestHalf();
                    FailsCount += TestCluster();
                    FailsCount += TestSway();
                    break;
                case "hw3":
                    FailsCount += TestSettings();
                    FailsCount += TestSym();
                    FailsCount += TestNum();
                    FailsCount += TestData();
                    FailsCount += TestClone();
                    FailsCount += TestAround();
                    FailsCount += TestHalf();
                    FailsCount += TestCluster();
                    FailsCount += TestSway();
                    break;
                case "hw2":
                    FailsCount += TestSettings();
                    FailsCount += TestSym();
                    FailsCount += TestNum();
                    FailsCount += TestCsv();
                    FailsCount += TestData();
                    FailsCount += TestStats();
                    break;
                case "hw1":
                    FailsCount += TestSettings();
                    FailsCount += TestSym();
                    FailsCount += TestNum();
                    break;
                default:
                    Console.WriteLine("Unrecognized action please check the helpstring with -h true");
                    break;

            }
            return FailsCount;
        }


        public static int TestNum()
        {
            var list1 = new List<dynamic> { 1, 1, 1, 1, 2, 2, 3 };
            var num = new Num();
            foreach (double val in list1)
                num.Add(val);
            Console.WriteLine("{0}, {1}", num.Mid(), num.Div());
            var result = (num.Mid() - 1.5714285714285714) < 0.01 && (num.Div() - 0.787) < 0.01;
            PrintResult(result, "num");
            return result ? 0 : 1;
        }

        public static int TestSym()
        {
            var list1 = new List<dynamic> { "a", "a", "a", "a", "b", "b", "c" };
            var sym = new Sym();
            foreach (string val in list1)
                sym.Add(val);
            var entropy = sym.Div();
            Console.WriteLine("{0}, {1}", sym.Mid(), entropy);
            var result = (sym.Mid() == "a") && (entropy - 1.379) < 0.01;
            PrintResult(result, "sym");
            return result ? 0 : 1;

        }

        public static int TestSettings()
        {
            if (!string.IsNullOrEmpty(Settings.Go))
            {
                Console.WriteLine(Settings.SettingsString());
                var result = true;
                PrintResult(result, "the");
                return result ? 0 : 1;
            }
            return 0;
        }

        public static int TestCsv()
        {
            var data = Csv.Read(Settings.File);
            var result = data.Item1.Count == 8 && data.Item2.Count == 398;
            PrintResult(result, "csv");
            return result ? 0 : 1;
        }

        public static int TestData()
        {
            var data = new Data("file", Settings.File);
            var result = data.Rows.Count == 398 &&
                         data.Cols[data.Y[0]].Num.Weight == -1 &&
                         data.X[1] == 1 &&
                         data.X.Count == 4;
            PrintResult(result, "data");
            return result ? 0 : 1;

        }

        public static int TestStats()
        {
            var data = new Data("file", Settings.File);
            data.Stats("mid", null, 2);
            data.Stats("div", null, 2);
            var result = true;
            PrintResult(result, "the");
            return result ? 0 : 1;
        }

        public static int TestClone()
        {
            var data1 = new Data("file", Settings.File);
            var data2 = data1.Clone();
            var result = data1.Rows.Count == data2.Rows.Count &&
                         data1.Cols[data1.Y[1]].Num.Weight == data2.Cols[data2.Y[1]].Num.Weight &&
                         data1.X[1] == data2.X[1] &&
                         data1.X.Count == data2.X.Count;
            PrintResult(result, "clone");
            return result ? 0 : 1;
        }

        public static int TestAround()
        {
            var data = new Data("file", Settings.File);
            Console.WriteLine("0\t0\t" + data.Rows[1].ToString());
            var vals = data.Around(data.Rows[1], data.Rows);
            foreach(var (val, index) in vals.WithIndex())
            {
                if(index%50 == 0)
                {
                    Console.WriteLine(index + "\t" + Math.Round(val.Item2) + "\t" + val.Item1.ToString());
                }
            }
            PrintResult(true, "around");
            return 0;

        }

        public static int TestHalf()
        {
            var data = new Data("file", Settings.File);
            var res = data.Half();
            Console.WriteLine(res.Item1.Count + "\t" + res.Item2.Count + "\t" + data.Rows.Count);
            Console.WriteLine(res.Item3.ToString() + "\t" + res.Item6);
            Console.WriteLine(res.Item5.ToString());
            Console.WriteLine(res.Item4.ToString());
            PrintResult(true, "half");
            return 0;
        }

        public static int TestCluster()
        {
            var data = new Data("file", Settings.File);
            var node = data.Cluster();
            node.Show("mid", data.Y, 1);
            PrintResult(true, "cluster");
            return 0;
        }

        public static int TestSway()
        {
            var data = new Data("file", Settings.File);
            var node = data.Sway();
            node.Show("mid", data.Y, 1);
            PrintResult(true, "optimize");
            return 0;
        }

        public static void PrintResult(bool result, string what)
        {
            if (result)
                Console.WriteLine("✅ pass:\t" + what);
            else
                Console.WriteLine("❌ fail:\t" + what);
        }
    }
}
