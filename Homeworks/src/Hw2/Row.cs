using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Homeworks.src.Hw2
{
    public class Row
    {
        public ArrayList Cells { get; set; }

        public Row (ArrayList data)
        {
            Cells = data;
        }

        public override string ToString()
        {
            string final = "{";
            foreach(var val in Cells)
            {
                final += val + " ";
            }
            return final.TrimEnd() + "}";
        }

    }
}
