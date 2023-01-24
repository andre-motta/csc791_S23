using Homeworks.Hw2;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Homeworks
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Data("file", "../netcoreapp3.1/Hw2/auto93.csv");
            data.Stats();
        }
    }
}
