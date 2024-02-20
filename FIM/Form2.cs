using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIM
{
    public partial class Form2 : MetroSet_UI.Forms.MetroSetForm
    {
        internal DataGridView dataGridView1;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void UpdateDataGridView(WatcherChangeTypes changeType, string fileName, string timestamp)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new MethodInvoker(() => UpdateDataGridView(changeType, fileName, timestamp)));
                return;
            }

            // Update your DataGridView here
            dataGridView1.Rows.Add(changeType, fileName, timestamp);
        }

        private void Close1_Click(object sender, EventArgs e)
        {
            StopMonitoring();
            this.Close();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private FileSystemWatcher fileSystemWatcher;
        private void StopMonitoring()
        {
            if (fileSystemWatcher != null)
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                fileSystemWatcher.Dispose();
                fileSystemWatcher = null;
                MessageBox.Show("Monitoring stopped.", "Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            StopMonitoring();
            this.Close();
        }
    }
}
