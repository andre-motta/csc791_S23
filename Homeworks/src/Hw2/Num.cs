using System;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.src.Hw2
{
    public class Num
    {
        public double Sum { get; set; }
        public double Len { get; set; }
        public double Median { get; set; }
        public double Moment { get; set; }
        public double Weight { get; set; }
        public double Low { get; set; }
        public double High { get; set; }

        public Num()
        {
            Sum = 0;
            Len = 0;
            Median = 0;
            Moment = 0;
            Low = float.PositiveInfinity;
            High = float.NegativeInfinity;
        }

        public void Add(double n)
        {
            Len++;
            Sum += n;
            var delta = n - Median;
            Median += delta / Len;
            Moment += delta * (n - Median);
            if (n > High)
            {
                High = n;
            }
            if (n < Low)
            {
                Low = n;
            }
        }

        public double Mid()
        {
            return Median;
        }

        public double Div()
        {
            return Math.Pow((Moment / (Len - 1)), 0.5);
        }

        public double Round(double x, int n)
        {
            return Math.Round(x, n);
        }

        public double Norm(string val)
        {
            if(val != "?")
            {
                var n = double.Parse(val);
                return (n - Low) / (High - Low - 1E-32);
            }
            return float.NegativeInfinity;
        }

        public double Distance(string s1, string s2)
        {
            if (s1 == "?" && s2 == "?") return 1;
            var n1 = Norm(s1);
            var n2 = Norm(s2);
            if (s1 == "?") n1 = n2 < 0.5 ? 1 : 0;
            if (s2 == "?") n2 = n1 < 0.5 ? 1 : 0;
            return Math.Abs(n1 - n2);
        }
    }
}
