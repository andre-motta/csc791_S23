using System;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.Hw2
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
    }
}
