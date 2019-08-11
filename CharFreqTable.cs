using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Huffman_demo
{


    public class CharFreqTable : DataTable
    {
        private Dictionary<char, int> freqs = new Dictionary<char, int>();
        private int maxweight = int.MinValue;



        public int MaxWeight
        {
            get { return maxweight; }
        }

        public void ClearTable()
        {
            Clear();
            freqs.Clear();
            maxweight = int.MinValue;
        }

        public CharFreqTable()
        {
            TableName = "CharacterFrequencies";
            Columns.Add(new DataColumn("Character", typeof(string)));
            Columns.Add(new DataColumn("Count", typeof(int)));
            Columns.Add(new DataColumn("Encoding", typeof(string)));
        }

        private void encoding_helper(string prefix, Dictionary<char, string> enc, HuffmanTree ht)
        {
            if (ht.Terminal)
            {
                enc.Add(ht.Val, prefix); return;
            }
            if (ht.Left != null)
            {
                encoding_helper(prefix + "0", enc, ht.Left);
            }
            if (ht.Right != null)
            {
                encoding_helper(prefix + "1", enc, ht.Right);
            }
            
        }

        public Dictionary<char, string> encoding()
        {
            HuffmanTree ht = create_huffman();
            Dictionary<char, string> enc = new Dictionary<char, string>();

            if (ht.Left != null)
            {
                encoding_helper("0", enc, ht.Left);
            }
            if (ht.Right != null)
            {
                encoding_helper("1", enc, ht.Right);
            }

            return enc;

        }

        public HuffmanTree create_huffman()
        {
            /* 
                Create a leaf node for each symbol and add it to the priority queue.
                While there is more than one node in the queue:
                Remove the two nodes of highest priority (lowest probability) from the queue
                Create a new internal node with these two nodes as children and with probability equal to the sum of the two nodes' probabilities.
                Add the new node to the queue.
                The remaining node is the root node and the tree is complete. 
             */
            PriorityQueue<HuffmanTree> trees = new PriorityQueue<HuffmanTree>();
            int prio_bias = MaxWeight;

            foreach (char c in freqs.Keys)
            {
                trees.enqueue(prio_bias - freqs[c], new HuffmanTree(c, freqs[c]));
            }

            while (trees.Count != 1)
            {
                HuffmanTree t1 = trees.dequeue();
                HuffmanTree t2 = trees.dequeue();
                HuffmanTree t = new HuffmanTree();
                t1.Parent = t;
                t2.Parent = t;
                t.Left = t1;
                t.Right = t2;
                t.Weight = t1.Weight + t2.Weight;
                trees.enqueue(prio_bias - t.Weight, t);
            }

            return trees.dequeue();
        }

        /// <summary>
        /// Insert chars from string into the table.
        /// </summary>
        /// <param name="chars">The string to be entered</param>
        public void update(string chars)
        {
            ClearTable();
            foreach (char c in chars)
            {
                if (!freqs.ContainsKey(c))
                {
                    freqs.Add(c, 0);

                }
                freqs[c]++;
            }

            Dictionary<char, string> enc = encoding();

            Rows.Clear();
            foreach (char c in freqs.Keys)
            {
                string cstr = string.Format("0x{0,2:X}", (byte)c);
                cstr += " '" + c + "'";
                object[] row = { cstr, freqs[c], enc[c] };
                if (freqs[c] > maxweight)
                    maxweight = freqs[c];
                Rows.Add(row);
            }
        }

    }
}
