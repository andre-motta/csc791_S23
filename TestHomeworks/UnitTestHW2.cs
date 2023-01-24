using Homeworks.src.Hw2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestHomeworks
{
    [TestClass]
    public class UnitTestHW2
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
            var result = true;
        }
    }
}
