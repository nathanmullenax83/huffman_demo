using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman_demo
{
  

    public class PriorityQueue<T> 
    {
        SortedDictionary<int, List<T>> elems;
        int elemCount;


        public PriorityQueue( )
        {
            elems = new SortedDictionary<int, List<T>>();
            elemCount = 0;
        }

        public void enqueue(int prio, T t)
        {
            
            if (!elems.ContainsKey(prio))
            {
                List<T> es = new List<T>();
                es.Add(t);
                elems.Add(prio, es);
            }
            else
            {
                elems[prio].Add(t);
            }
            elemCount++;
        }

        public int Count
        {
            get
            {
                return elemCount;
            }
        }

        public T dequeue()
        {
            KeyValuePair<int, List<T>> kv = elems.Last();
            if (kv.Value.Count == 1)
            {
                T t = kv.Value[0];
                elems.Remove(kv.Key);
                elemCount--;
                return t;
            }
            else
            {
                T t = kv.Value[0];
                kv.Value.RemoveAt(0);
                elemCount--;
                return t;   
            }
            
        }
    
    }
}
