using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Homeworks.Hw2
{
    public static class TestEngine
    {
        public static void RunTest(List<dynamic> input, string type)
        {
            switch (type)
            {
                case "num":
                    var num = new Num();
                    foreach (double val in input)
                        num.Add(val);
                    Console.WriteLine("{0}, {1}", num.Mid(), num.Div());
                    Debug.Assert((num.Mid() - 1.5714285714285714) < 0.01 && (num.Div() - 0.787) < 0.01);
                    Console.WriteLine("Pass: "+type);
                    break;
                case "sym":
                    var sym = new Sym();
                    foreach (string val in input)
                        sym.Add(val);
                    var entropy = sym.Div();
                    Console.WriteLine("{0}, {1}", sym.Mid(), entropy);
                    Debug.Assert((sym.Mid() == "a") && (entropy - 1.379) < 0.01);
                    Console.WriteLine("Pass: " + type);
                    break;
                default:
                    break;

            }
        }
    }
}
