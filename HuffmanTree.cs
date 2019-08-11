using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman_demo
{
    public class HuffmanTree : IComparable<HuffmanTree>
    {
        private HuffmanTree left;
        private HuffmanTree right;
        private char v;
        private int weight;
        private HuffmanTree parent;

        public HuffmanTree Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public HuffmanTree()
        {
        }

        public bool Terminal
        {
            get { return left == null && right == null; }
        }

        public HuffmanTree(char v, int weight)
        {
            Val = v;
            Weight = weight;
        }

        public int CompareTo(HuffmanTree oth)
        {
            if (oth.weight < weight) return -1;
            if (oth.weight > weight) return 1;
            return 0;
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public bool Equals(HuffmanTree oth)
        {
            return oth.weight == weight;
        }

        public char Val
        {
            get { return v; }
            set { v = value; }
        }

        public HuffmanTree Left
        {
            get { return left; }
            set { left = value; }
        }

        public HuffmanTree Right
        {
            get { return right; }
            set { right = value; }
        }

        

        
    }
}
