using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman_demo
{
    public static class Hexdump
    {
        public static string dump(IEnumerable<byte> bytes)
        {
            StringBuilder sb = new StringBuilder();
            int c = 0;
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0,2:X} ", b);
                c++;
                if (c == 20)
                {
                    sb.Append("\r\n");
                    c = 0;
                }
            }
            return sb.ToString();
        }

        public static string dump(IEnumerable<char> cs)
        {
            byte[] bytes = new byte[cs.Count()];
            for (int i = 0; i < cs.Count(); ++i)
            {
                bytes[i] = (byte)cs.ElementAt(i);
            }
            return dump(bytes);
        }

        public static string dump(string s)
        {
            return dump(s.ToCharArray());
        }
    }
}
