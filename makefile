all: huffman_demo

huffman_demo: ./*.cs
	mcs -r:System.Windows.Forms -r:System.Data -r:System.Drawing -out:huffman_demo ./*.cs
