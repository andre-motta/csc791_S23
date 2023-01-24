using Homeworks.src.Hw3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestHomeworks
{
    [TestClass]
    public class UnitTestHW3
    {
        [TestMethod]
        public void TestNum()
        {
            var list1 = new List<dynamic> { 1, 1, 1, 1, 2, 2, 3 };
            var num = new Num();
            foreach (double val in list1)
                num.Add(val);
            Console.WriteLine("{0}, {1}", num.Mid(), num.Div());
            Debug.Assert((num.Mid() - 1.5714285714285714) < 0.01 && (num.Div() - 0.787) < 0.01);

        }

        [TestMethod]
        public void TestSym()
        {
            var list1 = new List<dynamic> { "a", "a", "a", "a", "b", "b", "c" };
            var sym = new Sym();
            foreach (string val in list1)
                sym.Add(val);
            var entropy = sym.Div();
            Console.WriteLine("{0}, {1}", sym.Mid(), entropy);
            Debug.Assert((sym.Mid() == "a") && (entropy - 1.379) < 0.01);
        }

        [TestMethod]
        public void TestSettings()
        {
            if (!string.IsNullOrEmpty(Settings.Go))
            {
                Console.WriteLine(Settings.SettingsString());
            }
        }
        
        [TestMethod]
        public void TestCsv()
        {
            var data = Csv.Read(Settings.File);
            Debug.Assert(data.Item1.Count == 8 && data.Item2.Count == 398);
        }

        [TestMethod]
        public void TestData()
        {
            var data = new Data("file", Settings.File);
            Debug.Assert(data.Rows.Count == 398 &&
                         data.Cols[data.Y[0]].Num.Weight == -1 &&
                         data.X[1] == 1 &&
                         data.X.Count == 4);
        }

        [TestMethod]
        public void TestStats()
        {
            var data = new Data("file", Settings.File);
            data.Stats("mid", null, 2);
            data.Stats("div", null, 2);
        }

        [TestMethod]
        public void TestClone()
        {
            var data1 = new Data("file", Settings.File);
            var data2 = data1.Clone();
            Debug.Assert(data1.Rows.Count == data2.Rows.Count &&
                         data1.Cols[data1.Y[1]].Num.Weight == data2.Cols[data2.Y[1]].Num.Weight &&
                         data1.X[1] == data2.X[1] &&
                         data1.X.Count == data2.X.Count);
        }
        
        [TestMethod]
        public void TestAround()
        {
            var data = new Data("file", Settings.File);
            Console.WriteLine("0\t0\t" + data.Rows[1].ToString());
            var vals = data.Around(data.Rows[1], data.Rows);
            foreach (var (val, index) in vals.WithIndex())
            {
                if (index % 50 == 0)
                {
                    Console.WriteLine(index + "\t" + Math.Round(val.Item2) + "\t" + val.Item1.ToString());
                }
            }

        }
        
        [TestMethod]
        public void TestHalf()
        {
            var data = new Data("file", Settings.File);
            var res = data.Half();
            Console.WriteLine(res.Item1.Count + "\t" + res.Item2.Count + "\t" + data.Rows.Count);
            Console.WriteLine(res.Item3.ToString() + "\t" + res.Item6);
            Console.WriteLine(res.Item5.ToString());
            Console.WriteLine(res.Item4.ToString());
        }

        [TestMethod]
        public void TestCluster()
        {
            var data = new Data("file", Settings.File);
            var node = data.Cluster();
            node.Show("mid", data.Y, 1);
        }

        [TestMethod]
        public void TestSway()
        {
            var data = new Data("file", Settings.File);
            var node = data.Sway();
            node.Show("mid", data.Y, 1);
        }
    }
}
