namespace DALGenerator
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
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtDbCon = new System.Windows.Forms.TextBox();
            this.btnLoadDb = new System.Windows.Forms.Button();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDb = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connection :";
            // 
            // txtDbCon
            // 
            this.txtDbCon.Location = new System.Drawing.Point(87, 22);
            this.txtDbCon.Name = "txtDbCon";
            this.txtDbCon.Size = new System.Drawing.Size(375, 20);
            this.txtDbCon.TabIndex = 24;
            this.txtDbCon.Text = "Data Source=192.168.25.107;User ID=sa;Password=121212;";
            // 
            // btnLoadDb
            // 
            this.btnLoadDb.Location = new System.Drawing.Point(478, 18);
            this.btnLoadDb.Name = "btnLoadDb";
            this.btnLoadDb.Size = new System.Drawing.Size(122, 27);
            this.btnLoadDb.TabIndex = 25;
            this.btnLoadDb.Text = "Load DataBase";
            this.btnLoadDb.UseVisualStyleBackColor = true;
            this.btnLoadDb.Click += new System.EventHandler(this.btnLoadDb_Click);
            // 
            // cmbTable
            // 
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Location = new System.Drawing.Point(87, 81);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(375, 21);
            this.cmbTable.TabIndex = 45;
            this.cmbTable.SelectedIndexChanged += new System.EventHandler(this.cmbTable_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Main Table";
            // 
            // cmbDb
            // 
            this.cmbDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDb.FormattingEnabled = true;
            this.cmbDb.Location = new System.Drawing.Point(87, 50);
            this.cmbDb.Name = "cmbDb";
            this.cmbDb.Size = new System.Drawing.Size(375, 21);
            this.cmbDb.TabIndex = 33;
            this.cmbDb.SelectedIndexChanged += new System.EventHandler(this.cmbDb_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 53;
            this.label11.Text = "Database :";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(87, 108);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(141, 47);
            this.btnGenerate.TabIndex = 43;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 167);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbDb);
            this.Controls.Add(this.btnLoadDb);
            this.Controls.Add(this.txtDbCon);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.cmbTable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtDbCon;
        private System.Windows.Forms.Button btnLoadDb;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnGenerate;
    }
}

