using Homeworks.Hw1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestHomeworks
{
    [TestClass]
    public class UnitTestHW1
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
            Console.WriteLine("Pass: num");
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
            Console.WriteLine("Pass: sym");
        }
    }
}
