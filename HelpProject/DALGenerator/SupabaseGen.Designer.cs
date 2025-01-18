
namespace DALGenerator
{
    partial class SupabaseGen
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtDirctory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtS2Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGnerateSelect2 = new System.Windows.Forms.Button();
            this.txtDbCon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoadDB = new System.Windows.Forms.Button();
            this.ddlTables = new System.Windows.Forms.ComboBox();
            this.ddlSingleDoubleColumn = new System.Windows.Forms.ComboBox();
            this.cmbParentMenu = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAddMenu = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnFunc = new System.Windows.Forms.Button();
            this.ddlS2ValueField = new System.Windows.Forms.ComboBox();
            this.ddl_S2TextField = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(659, 632);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(196, 32);
            this.btnGenerate.TabIndex = 50;
            this.btnGenerate.Text = "Generate Table BackEnd ";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtDirctory
            // 
            this.txtDirctory.Location = new System.Drawing.Point(117, 11);
            this.txtDirctory.Name = "txtDirctory";
            this.txtDirctory.Size = new System.Drawing.Size(317, 22);
            this.txtDirctory.TabIndex = 51;
            this.txtDirctory.Text = "D:\\Generated_Files";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 7);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 30);
            this.button1.TabIndex = 52;
            this.button1.Text = "Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(974, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Generate Select2";
            // 
            // txtS2Name
            // 
            this.txtS2Name.Location = new System.Drawing.Point(953, 210);
            this.txtS2Name.Name = "txtS2Name";
            this.txtS2Name.Size = new System.Drawing.Size(182, 22);
            this.txtS2Name.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(953, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 55;
            this.label2.Text = "S2 Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(950, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Text Field";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(950, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 59;
            this.label4.Text = "Value Field";
            // 
            // btnGnerateSelect2
            // 
            this.btnGnerateSelect2.Location = new System.Drawing.Point(950, 330);
            this.btnGnerateSelect2.Name = "btnGnerateSelect2";
            this.btnGnerateSelect2.Size = new System.Drawing.Size(182, 37);
            this.btnGnerateSelect2.TabIndex = 60;
            this.btnGnerateSelect2.Text = "Generate Select2";
            this.btnGnerateSelect2.UseVisualStyleBackColor = true;
            this.btnGnerateSelect2.Click += new System.EventHandler(this.btnGnerateSelect2_Click);
            // 
            // txtDbCon
            // 
            this.txtDbCon.Location = new System.Drawing.Point(117, 45);
            this.txtDbCon.Margin = new System.Windows.Forms.Padding(4);
            this.txtDbCon.Name = "txtDbCon";
            this.txtDbCon.Size = new System.Drawing.Size(820, 22);
            this.txtDbCon.TabIndex = 64;
            this.txtDbCon.Text = "User Id=postgres.daxvpqmciumhfnxldwvd;Password=Erp123!Erp123;Server=aws-0-ap-sout" +
    "h-1.pooler.supabase.com;Port=5432;Database=postgres;";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 63;
            this.label6.Text = "Connection :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 16);
            this.label7.TabIndex = 66;
            this.label7.Text = "Table Name";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(828, 509);
            this.dataGridView1.TabIndex = 67;
            // 
            // btnLoadDB
            // 
            this.btnLoadDB.Location = new System.Drawing.Point(964, 42);
            this.btnLoadDB.Name = "btnLoadDB";
            this.btnLoadDB.Size = new System.Drawing.Size(182, 28);
            this.btnLoadDB.TabIndex = 68;
            this.btnLoadDB.Text = "Load DB";
            this.btnLoadDB.UseVisualStyleBackColor = true;
            this.btnLoadDB.Click += new System.EventHandler(this.btnLoadDB_Click);
            // 
            // ddlTables
            // 
            this.ddlTables.FormattingEnabled = true;
            this.ddlTables.Location = new System.Drawing.Point(117, 80);
            this.ddlTables.Name = "ddlTables";
            this.ddlTables.Size = new System.Drawing.Size(317, 24);
            this.ddlTables.TabIndex = 69;
            // 
            // ddlSingleDoubleColumn
            // 
            this.ddlSingleDoubleColumn.FormattingEnabled = true;
            this.ddlSingleDoubleColumn.Items.AddRange(new object[] {
            "Single Column",
            "Double Column"});
            this.ddlSingleDoubleColumn.Location = new System.Drawing.Point(177, 636);
            this.ddlSingleDoubleColumn.Name = "ddlSingleDoubleColumn";
            this.ddlSingleDoubleColumn.Size = new System.Drawing.Size(205, 24);
            this.ddlSingleDoubleColumn.TabIndex = 73;
            // 
            // cmbParentMenu
            // 
            this.cmbParentMenu.FormattingEnabled = true;
            this.cmbParentMenu.Location = new System.Drawing.Point(462, 636);
            this.cmbParentMenu.Name = "cmbParentMenu";
            this.cmbParentMenu.Size = new System.Drawing.Size(176, 24);
            this.cmbParentMenu.TabIndex = 74;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(399, 639);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 75;
            this.label9.Text = "Module";
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Location = new System.Drawing.Point(673, 73);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(182, 31);
            this.btnAddMenu.TabIndex = 76;
            this.btnAddMenu.Text = "Add Menu Entry";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new System.EventHandler(this.btnAddMenu_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 639);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 16);
            this.label8.TabIndex = 71;
            this.label8.Text = "Single/Double Column";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(455, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 31);
            this.button2.TabIndex = 70;
            this.button2.Text = "Load Table";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnLoadColumn);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(884, 626);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 34);
            this.button3.TabIndex = 77;
            this.button3.Text = "Menu Action";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnFunc
            // 
            this.btnFunc.Location = new System.Drawing.Point(1014, 626);
            this.btnFunc.Name = "btnFunc";
            this.btnFunc.Size = new System.Drawing.Size(119, 34);
            this.btnFunc.TabIndex = 96;
            this.btnFunc.Text = "Function Gen";
            this.btnFunc.UseVisualStyleBackColor = true;
            this.btnFunc.Click += new System.EventHandler(this.btnFunc_Click);
            // 
            // ddlS2ValueField
            // 
            this.ddlS2ValueField.FormattingEnabled = true;
            this.ddlS2ValueField.Location = new System.Drawing.Point(953, 300);
            this.ddlS2ValueField.Name = "ddlS2ValueField";
            this.ddlS2ValueField.Size = new System.Drawing.Size(182, 24);
            this.ddlS2ValueField.TabIndex = 98;
            // 
            // ddl_S2TextField
            // 
            this.ddl_S2TextField.FormattingEnabled = true;
            this.ddl_S2TextField.Location = new System.Drawing.Point(953, 254);
            this.ddl_S2TextField.Name = "ddl_S2TextField";
            this.ddl_S2TextField.Size = new System.Drawing.Size(182, 24);
            this.ddl_S2TextField.TabIndex = 99;
            // 
            // SupabaseGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 672);
            this.Controls.Add(this.ddl_S2TextField);
            this.Controls.Add(this.ddlS2ValueField);
            this.Controls.Add(this.btnFunc);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnAddMenu);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbParentMenu);
            this.Controls.Add(this.ddlSingleDoubleColumn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ddlTables);
            this.Controls.Add(this.btnLoadDB);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDbCon);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGnerateSelect2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtS2Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDirctory);
            this.Controls.Add(this.btnGenerate);
            this.Name = "SupabaseGen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SupabaseGen_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtDirctory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtS2Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGnerateSelect2;
        private System.Windows.Forms.TextBox txtDbCon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoadDB;
        private System.Windows.Forms.ComboBox ddlTables;
        private System.Windows.Forms.ComboBox ddlSingleDoubleColumn;
        private System.Windows.Forms.ComboBox cmbParentMenu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAddMenu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnFunc;
        private System.Windows.Forms.ComboBox ddlS2ValueField;
        private System.Windows.Forms.ComboBox ddl_S2TextField;
    }
}