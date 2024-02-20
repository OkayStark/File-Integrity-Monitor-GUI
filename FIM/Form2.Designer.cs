
namespace FIM
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close1 = new MetroSet_UI.Controls.MetroSetTile();
            this.Minimize = new MetroSet_UI.Controls.MetroSetTile();
            this.Stop = new MetroSet_UI.Controls.MetroSetButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.ChangeType,
            this.Timestamp});
            this.dataGridView1.Location = new System.Drawing.Point(15, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(683, 177);
            this.dataGridView1.TabIndex = 0;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            this.FileName.Width = 400;
            // 
            // ChangeType
            // 
            this.ChangeType.HeaderText = "ChangeType";
            this.ChangeType.Name = "ChangeType";
            this.ChangeType.Width = 120;
            // 
            // Timestamp
            // 
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.Width = 120;
            // 
            // Close1
            // 
            this.Close1.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.Close1.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.Close1.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.Close1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close1.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Close1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Close1.HoverTextColor = System.Drawing.Color.White;
            this.Close1.IsDerivedStyle = true;
            this.Close1.Location = new System.Drawing.Point(669, 12);
            this.Close1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Close1.Name = "Close1";
            this.Close1.NormalBorderColor = System.Drawing.Color.Transparent;
            this.Close1.NormalColor = System.Drawing.Color.Transparent;
            this.Close1.NormalTextColor = System.Drawing.Color.White;
            this.Close1.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Close1.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Close1.PressTextColor = System.Drawing.Color.White;
            this.Close1.Size = new System.Drawing.Size(32, 30);
            this.Close1.Style = MetroSet_UI.Enums.Style.Custom;
            this.Close1.StyleManager = null;
            this.Close1.TabIndex = 4;
            this.Close1.Text = "X";
            this.Close1.ThemeAuthor = "Narwin";
            this.Close1.ThemeName = "MetroLite";
            this.Close1.TileAlign = MetroSet_UI.Enums.TileAlign.BottomCenter;
            this.Close1.Click += new System.EventHandler(this.Close1_Click);
            // 
            // Minimize
            // 
            this.Minimize.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.Minimize.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.Minimize.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.Minimize.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Minimize.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Minimize.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Minimize.HoverTextColor = System.Drawing.Color.White;
            this.Minimize.IsDerivedStyle = true;
            this.Minimize.Location = new System.Drawing.Point(640, 12);
            this.Minimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Minimize.Name = "Minimize";
            this.Minimize.NormalBorderColor = System.Drawing.Color.Transparent;
            this.Minimize.NormalColor = System.Drawing.Color.Transparent;
            this.Minimize.NormalTextColor = System.Drawing.Color.White;
            this.Minimize.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Minimize.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Minimize.PressTextColor = System.Drawing.Color.White;
            this.Minimize.Size = new System.Drawing.Size(32, 30);
            this.Minimize.Style = MetroSet_UI.Enums.Style.Custom;
            this.Minimize.StyleManager = null;
            this.Minimize.TabIndex = 3;
            this.Minimize.Text = "_";
            this.Minimize.ThemeAuthor = "Narwin";
            this.Minimize.ThemeName = "MetroLite";
            this.Minimize.TileAlign = MetroSet_UI.Enums.TileAlign.BottomCenter;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // Stop
            // 
            this.Stop.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Stop.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Stop.DisabledForeColor = System.Drawing.Color.Gray;
            this.Stop.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stop.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.Stop.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.Stop.HoverTextColor = System.Drawing.Color.White;
            this.Stop.IsDerivedStyle = true;
            this.Stop.Location = new System.Drawing.Point(623, 322);
            this.Stop.Name = "Stop";
            this.Stop.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Stop.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.Stop.NormalTextColor = System.Drawing.Color.White;
            this.Stop.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.Stop.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.Stop.PressTextColor = System.Drawing.Color.White;
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.Style = MetroSet_UI.Enums.Style.Light;
            this.Stop.StyleManager = null;
            this.Stop.TabIndex = 5;
            this.Stop.Text = "Stop";
            this.Stop.ThemeAuthor = "Narwin";
            this.Stop.ThemeName = "MetroLite";
            this.Stop.Click += new System.EventHandler(this.metroSetButton1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(716, 359);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Minimize);
            this.Controls.Add(this.Close1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form2";
            this.Padding = new System.Windows.Forms.Padding(12, 63, 12, 11);
            this.Style = MetroSet_UI.Enums.Style.Custom;
            this.Text = "Monitor";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private MetroSet_UI.Controls.MetroSetTile Close1;
        private MetroSet_UI.Controls.MetroSetTile Minimize;
        private MetroSet_UI.Controls.MetroSetButton Stop;
    }
}