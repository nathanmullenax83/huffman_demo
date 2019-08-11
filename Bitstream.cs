using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman_demo
{
    /// <summary>
    /// Efficiently manage a memory stream of bits. Probably better termed a bit buffer.
    /// </summary>
    public class Bitstream
    {
        public List<byte> bytes = new List<byte>();
        short currentBit = 0;

        public Bitstream()
        {
            bytes.Add(0);
        }

        /// <summary>
        /// Write bits to buffer.
        /// </summary>
        /// <param name="bits">Encoded as a string of 1s and 0s</param>
        public void write(string bits)
        {
            foreach (char c in bits)
                write(c == '1');
        }

        /// <summary>
        /// Write a single bit. 
        /// </summary>
        /// <param name="bit">true=1, false=0</param>
        public void write(bool bit)
        {
            byte m = bytes.Last();
            int b = (bit) ? 1 : 0;
            m = (byte)(m | (b << (7 - currentBit)));
            bytes[bytes.Count - 1] = m;
            currentBit = (short)((currentBit + 1) % 8);
            if (currentBit == 0)
                bytes.Add(0);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Count - 1; ++i)
            {
                byte b = bytes[i];
                for (int j = 0; j < 8; ++j)
                {
                    sb.Append((((b >> (7 - j)) & 1) == 1) ? "1" : "0");
                }
            }
            {
                byte b = bytes.Last();
                for (int i = 0; i < currentBit; ++i)
                {
                    sb.Append((((b >> (7 - i)) & 1) == 1) ? "1" : "0");
                }
            }
            return sb.ToString();
        }
    }
}
