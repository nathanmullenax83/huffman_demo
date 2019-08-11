using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Huffman_demo
{
    public partial class Form1 : Form
    {
        private CharFreqTable ctable;
        private Dictionary<object, string> hoverInfo = new Dictionary<object, string>();

        public Form1()
        {
            InitializeComponent();
            ctable = new CharFreqTable();
            dataGridView1.DataSource = ctable;
            hoverInfo.Add(button1, "Generate a huffman key for a specified string of text.");
            hoverInfo.Add(button2, "Populate and then service a priority queue at random to "
                            + "see if it is working.");
            hoverInfo.Add(button3, "Populate a Bitstream object and compare its output to the"
                            + " input.");
            hoverInfo.Add(button4, "Encode the text. Display before and after in hex to show "
                            + "how effective (or ineffective) the compression was.");
            hoverInfo.Add(button5, "Test hex dumping functionality.");
            button1.MouseEnter += new EventHandler(show_buttoninfo);
            button2.MouseEnter += new EventHandler(show_buttoninfo);
            button3.MouseEnter += new EventHandler(show_buttoninfo);
            button4.MouseEnter += new EventHandler(show_buttoninfo);
            button5.MouseEnter += new EventHandler(show_buttoninfo);
        }

        void show_buttoninfo(object sender, EventArgs e)
        {
            if( hoverInfo.ContainsKey(sender) )
                label1.Text = hoverInfo[sender];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctable.update(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PriorityQueue<int> pq = new PriorityQueue<int>();
            Random r = new Random();
            for (int i = 0; i < 100; ++i)
            {
                int n = r.Next(0, 1000);
                pq.enqueue(n,n);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; ++i)
            {
                sb.Append(pq.dequeue());
                sb.Append(" ");
            }
            textBox1.Text = sb.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            Bitstream bs = new Bitstream();
            Random r = new Random();
            for (int i = 0; i < 137; ++i)
            {
                sb.Append(r.Next(2) == 1 ? 1 : 0);
            }
            bs.write(sb.ToString());
            textBox1.Text = sb.ToString() + "=\n" + bs.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            byte[] bs = new byte[200];
            r.NextBytes(bs);

            textBox1.Text = Hexdump.dump(bs);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            ctable.update(textBox1.Text);
            Bitstream bs = new Bitstream();

            sb.Append("Before:\r\n\r\n");
            sb.Append(Hexdump.dump(textBox1.Text));
            sb.Append("\r\n\r\nAfter:\r\n\r\n");

            Dictionary<char, string> enc = ctable.encoding();

            foreach (char c in textBox1.Text)
            {
                bs.write(enc[c]);
            }

            sb.Append(Hexdump.dump(bs.bytes));

            textBox1.Text = sb.ToString();
        }
    }
}
