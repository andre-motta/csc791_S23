using System;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.Hw2
{
    public class Col
    {
        public string Txt { get; set; }
        public bool IsNum { get; set; }
        public int N { get; set; }
        public int At { get; set; }

        public Num Num { get; set; }
        public Sym Sym { get; set; }

        public Col(string name, int pos)
        {
            Txt = name;
            At = pos;
            Parse(Txt);
        }

        private void Parse(string name)
        {
            if (!string.IsNullOrEmpty(name) && char.IsUpper(name[0]))
            {
                IsNum = true;
                N = 0;
                Num = new Num();
                _ = name[name.Length - 1] == '-' ? Num.Weight = -1.0 : Num.Weight = 1.0;
            }
            else if (!string.IsNullOrEmpty(name) && !char.IsUpper(name[0]))
            {
                IsNum = false;
                N = 0;
                Sym = new Sym();
            }
            else
            {
                Console.WriteLine("Col Name Error");
                new System.ArgumentException();
            }
        }

        public void Add(string value)
        {
            if (value != "?")
            {
                N++;
                if (IsNum)
                {
                    Num.Add(double.Parse(value));
                }
                else
                {
                    Sym.Add(value);
                }
            }
        }
    }
}
