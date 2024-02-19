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
            InitializeDataGridView();
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

    }
}
