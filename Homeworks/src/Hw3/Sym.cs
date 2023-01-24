using System;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.src.Hw3
{
    public class Sym
    {
        public int Count { get; set; }
        public int Len { get; set; }
        public double Entropy { get; set; }
        public Dictionary<string, int> Has { get; set; }
        public int Most { get; set; }
        public string Mode { get; set; }

        public Sym()
        {
            Count = 0;
            Len = 0;
            Entropy = 0;
            Has = new Dictionary<string, int>();
            Most = 0;
        }

        public void Add(string x)
        {
            if (!Has.ContainsKey(x))
            {
                Count += 1;
                Has.Add(x, 1);
            }
            else
            {
                Has[x] += 1;
            }
            Len += 1;
            if (Has[x] > Most)
            {
                Most = Has[x];
                Mode = x;
            }
        }

        public string Mid()
        {
            return Mode;
        }

        public double Div()
        {
            if(Len <= 0)
            {
                return 0;
            }
            var probs = new List<double>();
            foreach(var val in Has.Values)
            {
                probs.Add((double) val / (double)Len);
            }
            foreach(var prob in probs)
            {
                if (prob > 0)
                {
                    Entropy -= prob * Math.Log2(prob);
                }
            }
            return Entropy;
        }

        public string Round(string x, int n)
        {
            return x;
        }

        public double Distance(string s1, string s2)
        {
            return (s1 == s2 ? 1 : (s1 == s2 ? 0 : 1));
        }
    }
}
