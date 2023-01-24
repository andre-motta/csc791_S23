using Homeworks.src.Hw3;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Homeworks
{
    class Program
    {
        static int Main(string[] args)
        {
            Settings.ReadCli(args);
            
            Settings.WriteOptions(args);

            return TestEngine.RunTests();

        }
    }
}
