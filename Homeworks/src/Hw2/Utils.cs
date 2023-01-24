using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homeworks.src.Hw2
{
    public static class Utils
    {
        public static List<T> Many<T>(this IEnumerable<T> list, int elementsCount)
        {
            var r = new Random(Settings.Seed);
            return list.OrderBy(arg => GenerateSeededGuid(r)).Take(elementsCount).ToList();
        }

        public static T Any<T>(this IEnumerable<T> list)
        {
            return list.Many(1).FirstOrDefault();
        }

        public static Tuple<double, double> Cosine(double a, double b, double c)
        {
            var x1 = (Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2)) / (2 * c);
            var x2 = Math.Max(0, Math.Min(1, x1));
            var y = Math.Sqrt(Math.Pow(a, 2) - Math.Pow(x2, 2));
            return new Tuple<double, double>(x2, y);
        }

        public static int RandInt(int low, int high)
        {
            Random r = new Random(Settings.Seed);
            return r.Next(low, high);
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }
        public static double Rand(double low, double high)
        {
            Random r = new Random(Settings.Seed);
            return r.NextDouble() * (high - low) + low;
        }

        public static Guid GenerateSeededGuid(Random r)
        {
            var guid = new byte[16];
            r.NextBytes(guid);

            return new Guid(guid);
        }

    }
}
