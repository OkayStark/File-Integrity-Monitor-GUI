using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FIM
{
    public partial class Form1 : MetroSet_UI.Forms.MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog1.SelectedPath;
                metroSetTextBox1.Text = selectedFolderPath;
            }
        }

        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog2.SelectedPath;
                metroSetTextBox2.Text = selectedFolderPath;
            }
        }
        private void metroSetTile1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroSetTile2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void metroSetButton6_Click(object sender, EventArgs e)
        {
            string enteredPath = metroSetTextBox1.Text;
            metroSetListBox1.Items.Add(enteredPath);
            metroSetTextBox1.Text = "";
        }

        private void metroSetButton7_Click(object sender, EventArgs e)
        {
            string enteredPath = metroSetTextBox2.Text;
            metroSetListBox2.Items.Add(enteredPath);
            metroSetTextBox2.Text = "";
        }
    }
}
