using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace blue_patcher
{
    public partial class Interface : Form
    {
        private delegate void LogDelegate(string text);

        public Interface()
        {
            InitializeComponent();
            textBox1.Text = Patcher.FindGamePath();
        }

        public void Log(string text)
        {
            if (textBox2.InvokeRequired)
                textBox2.Invoke(new LogDelegate(Log), text);
            else
            {
                textBox2.Text += "[+] " + text + "\r\n";
                textBox2.SelectionStart = textBox2.Text.Length - 1;
                textBox2.ScrollToCaret();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "CCP blue|blue.dll|Any File|*.*"};
            dialog.CheckFileExists = true;
            dialog.Title = "Select the blue.dll";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("You didn't select a valid file!", "blue_patcher error");
                return;
            }
            ThreadPool.QueueUserWorkItem(DoPatch, textBox1.Text);
            button2.Enabled = false;
        }

        private delegate void ReenableDelegate();
        private void Reenable()
        {
            button2.Enabled = true;
        }

        private void DoPatch(object o)
        {
            Patcher.Patch(o as string);
            button2.Invoke(new ReenableDelegate(Reenable));
        }
    }
}
