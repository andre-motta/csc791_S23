using Homeworks.Hw1;
using System;
using System.Collections.Generic;

namespace Homeworks
{
    class Program
    {
        static void Main(string[] args)
        {
            var list1 = new List<dynamic> { 1, 1, 1, 1, 2, 2, 3 };
            TestEngine.RunTest(list1,  "num");
            var list2 = new List<dynamic> { "a", "a", "a", "a", "b", "b", "c" };
            TestEngine.RunTest(list2, "sym");
        }
    }
}
