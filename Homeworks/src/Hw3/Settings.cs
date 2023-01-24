using System;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.src.Hw3
{
    public static class Settings
    {
        public static bool Dump { get; set; } = false;
        public static string Go { get; set; } = "hw3";
        public static bool Help { get; set; } = false;
        public static int Seed { get; set; } = 927162211;
        public static double Faraway { get; set; } = .95;
        public static double DistCoeff { get; set; } = 2;
        public static string File { get; set; } = "etc/data/auto93.csv";
        public static double MinStopClusters { get; set; } = .5;
        public static double Sample { get; set; } = 512;

        public static void ReadCli(string[] args)
        {
            List<Tuple<string, string>> p = new List<Tuple<string, string>>();
            for(int i = 0; i<args.Length-1; i += 2)
            {
                p.Add(new Tuple<string, string>(args[i], args[i+1]));
            }
            foreach(var arg in p)
            {
                ProcessArgument(arg);
            }
        }

        public static void ProcessArgument(Tuple<string, string> argument)
        {
            switch (argument.Item1)
            {
                case "-d":
                case "--dump":
                    Dump = argument.Item2 == "true" ? true : false;
                    break;
                case "-f":
                case "--file":
                    File = argument.Item2;
                    break;
                case "-F":
                case "--Far":
                    double faraway;
                    var successF = double.TryParse(argument.Item2, out faraway);
                    if (successF)
                    {
                        Faraway = faraway;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect parameter type for -F use -h true to get the helpstring");
                    }
                    break;
                case "-g":
                case "--go":
                    Go = argument.Item2;
                    break;
                case "-h": 
                case "--help":
                    Help = argument.Item2 == "true" ? true : false;
                    break;
                case "-m":
                case "--min":
                    double minStopClusters;
                    var successm = double.TryParse(argument.Item2, out minStopClusters);
                    if (successm)
                    {
                        MinStopClusters = minStopClusters;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect parameter type for -m use -h true to get the helpstring");
                    }
                    break;
                case "-p":
                case "--p":
                    double distcoeff;
                    var successp = double.TryParse(argument.Item2, out distcoeff);
                    if (successp)
                    {
                        DistCoeff = distcoeff;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect parameter type for -p use -h true to get the helpstring");
                    }
                    break;
                case "-s":
                case "--seed":
                    int seed;
                    var successs = Int32.TryParse(argument.Item2, out seed);
                    if (successs)
                    {
                        Seed = seed;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect parameter type for -s use -h true to get the helpstring");
                    }
                    break;
                case "-S":
                case "--Sample":
                    double sample;
                    var successS = double.TryParse(argument.Item2, out sample);
                    if (successS)
                    {
                        Sample = sample;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect parameter type for -S use -h true to get the helpstring");
                    }
                    break;
                default:
                    Console.WriteLine("Unrecognized parameter " + argument.Item1 + " with value " + argument.Item2 + " use -h true to get the helpstring");
                    break;

            }
        }

        public static string HelpString()
        {
            return string.Format(@"Program.cs : Homework for CSC791
(c)2023, Andre Lustosa <alustos@ncsu.edu> 
USAGE: dotnet run  [OPTIONS] [-g ACTION]
OPTIONS:
    -d  --dump    on crash, dump stack   = {0}
    -f  --file    name of file           = {1}
    -F  --Far     distance to 'faraway'  = {2}
    -g  --go      start-up action        = {3}
    -h  --help    show help              = {4}
    -m  --min     stop clusters at N^min = {5}
    -p  --p       distance coefficient   = {6}
    -s  --seed    random number seed     = {7}
    -S  --Sample  sampling data size     = {8}
ACTIONS: [all, hw1, hw2, hw3]",Dump.ToString(), File, Faraway.ToString(), Go, Help.ToString(), MinStopClusters.ToString(), DistCoeff.ToString(), Seed.ToString(), Sample.ToString());
        }

        public static string SettingsString()
        {
            return string.Format(@":dump {0} :file {1} :Far {2} :go {3} :help {4} :min {5} :p {6} :seed {7} :Sample {8} ", Dump.ToString(), File, Faraway.ToString(), Go, Help.ToString(), MinStopClusters.ToString(), DistCoeff.ToString(), Seed.ToString(), Sample.ToString());
        }

        public static void WriteOptions(string[] args)
        {
            if (Settings.Help)
            {
                Console.WriteLine(Settings.HelpString());
            }
            string mode = "default";
            if(args.Length != 0)
            {
                mode = string.Join(" ", args);
            }
            Console.WriteLine("Mode: " + mode);
        }
    }
}
