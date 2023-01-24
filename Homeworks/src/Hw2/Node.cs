using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homeworks.src.Hw2
{
    public class Node
    {
        public Data Current { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public bool IsLeaf { get; set; }
        public Node(Data cur, Node left = null, Node right=null)
        {
            if (left == null && right == null) IsLeaf = true;
            Current = cur;
            Left = left;
            Right = right;
        }

        public void AddChildren(Node left, Node right)
        {
            IsLeaf = false;
            Left = left;
            Right = right;
        }
        public void AddChild(Node child)
        {
            IsLeaf = false;
            Left = child;
        }

        public void Show(string what, List<int> cols, int nPlaces, int lvl = 0)
        {
            if (Current != null)
            {
                var start = String.Concat(Enumerable.Repeat("| ", lvl)) + Current.Rows.Count.ToString() + "  ";
                var final = start +"{"+ Current.Stats(what, cols, nPlaces).TrimEnd()+"}";

                if (lvl == 0 || IsLeaf)
                    Console.WriteLine(final);
                else
                    Console.WriteLine(start);
                if (Left != null)
                    Left.Show(what, cols, nPlaces, lvl + 1);
                if (Right != null)
                    Right.Show(what, cols, nPlaces, lvl + 1);
            }
        }

    }
}
