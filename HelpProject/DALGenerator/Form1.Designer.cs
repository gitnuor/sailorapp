namespace DALGenerator
{
    partial class Form1
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtDbCon = new System.Windows.Forms.TextBox();
            this.btnLoadDb = new System.Windows.Forms.Button();
            this.drpProject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkGenList = new System.Windows.Forms.CheckedListBox();
            this.gvRelation = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tblRelation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBJoin = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpName = new System.Windows.Forms.TextBox();
            this.gvColumns = new System.Windows.Forms.DataGridView();
            this.Table = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_nullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Filter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Opr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkSpList = new System.Windows.Forms.ComboBox();
            this.chkGenList2 = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDb = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connection :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Directory :";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(87, 18);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(614, 20);
            this.txtDirectory.TabIndex = 7;
            this.txtDirectory.Text = "D:\\Sourcesafe Projects\\KAF_IMS_EXC_New\\KAFMVCSolution.root\\KAFMVCSolution";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(707, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(337, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDbCon
            // 
            this.txtDbCon.Location = new System.Drawing.Point(87, 47);
            this.txtDbCon.Name = "txtDbCon";
            this.txtDbCon.Size = new System.Drawing.Size(375, 20);
            this.txtDbCon.TabIndex = 24;
            this.txtDbCon.Text = "Data Source=192.168.200.202;User ID=sa;Password=Asdf1234;";
            // 
            // btnLoadDb
            // 
            this.btnLoadDb.Location = new System.Drawing.Point(484, 42);
            this.btnLoadDb.Name = "btnLoadDb";
            this.btnLoadDb.Size = new System.Drawing.Size(217, 27);
            this.btnLoadDb.TabIndex = 25;
            this.btnLoadDb.Text = "Load DataBase";
            this.btnLoadDb.UseVisualStyleBackColor = true;
            this.btnLoadDb.Click += new System.EventHandler(this.btnLoadDb_Click);
            // 
            // drpProject
            // 
            this.drpProject.FormattingEnabled = true;
            this.drpProject.Location = new System.Drawing.Point(87, 109);
            this.drpProject.Name = "drpProject";
            this.drpProject.Size = new System.Drawing.Size(678, 21);
            this.drpProject.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Project Folder";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkGenList);
            this.groupBox1.Controls.Add(this.gvRelation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSpName);
            this.groupBox1.Controls.Add(this.gvColumns);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbTable);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Location = new System.Drawing.Point(9, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1147, 442);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "from Tables";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(970, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "Generate List";
            // 
            // chkGenList
            // 
            this.chkGenList.CheckOnClick = true;
            this.chkGenList.FormattingEnabled = true;
            this.chkGenList.Items.AddRange(new object[] {
            "Stored Procedure",
            "Entity",
            "ReportExtensionClasses"});
            this.chkGenList.Location = new System.Drawing.Point(971, 61);
            this.chkGenList.Name = "chkGenList";
            this.chkGenList.Size = new System.Drawing.Size(167, 64);
            this.chkGenList.TabIndex = 51;
            // 
            // gvRelation
            // 
            this.gvRelation.AllowUserToAddRows = false;
            this.gvRelation.AllowUserToDeleteRows = false;
            this.gvRelation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRelation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.tblRelation,
            this.DBJoin});
            this.gvRelation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvRelation.Location = new System.Drawing.Point(8, 76);
            this.gvRelation.Name = "gvRelation";
            this.gvRelation.Size = new System.Drawing.Size(302, 348);
            this.gvRelation.TabIndex = 50;
            this.gvRelation.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRelation_CellContentClick);
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Width = 50;
            // 
            // tblRelation
            // 
            this.tblRelation.DataPropertyName = "PK_Table";
            this.tblRelation.HeaderText = "tblRelation";
            this.tblRelation.Name = "tblRelation";
            // 
            // DBJoin
            // 
            this.DBJoin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DBJoin.HeaderText = "DBJoin";
            this.DBJoin.Items.AddRange(new object[] {
            "INNER JOIN",
            "LEFT OUTER JOIN"});
            this.DBJoin.Name = "DBJoin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(973, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Sp/Entity Name :";
            // 
            // txtSpName
            // 
            this.txtSpName.Location = new System.Drawing.Point(968, 159);
            this.txtSpName.Name = "txtSpName";
            this.txtSpName.Size = new System.Drawing.Size(170, 20);
            this.txtSpName.TabIndex = 48;
            // 
            // gvColumns
            // 
            this.gvColumns.AllowUserToAddRows = false;
            this.gvColumns.AllowUserToDeleteRows = false;
            this.gvColumns.AllowUserToResizeColumns = false;
            this.gvColumns.AllowUserToResizeRows = false;
            this.gvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Table,
            this.Name,
            this.Type,
            this.Length,
            this.is_nullable,
            this.chkOutput,
            this.Filter,
            this.Opr});
            this.gvColumns.Location = new System.Drawing.Point(316, 33);
            this.gvColumns.MultiSelect = false;
            this.gvColumns.Name = "gvColumns";
            this.gvColumns.RowHeadersVisible = false;
            this.gvColumns.Size = new System.Drawing.Size(638, 391);
            this.gvColumns.TabIndex = 47;
            // 
            // Table
            // 
            this.Table.DataPropertyName = "tbl";
            this.Table.HeaderText = "Table";
            this.Table.Name = "Table";
            // 
            // Name
            // 
            this.Name.DataPropertyName = "Name";
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Length
            // 
            this.Length.DataPropertyName = "length";
            this.Length.HeaderText = "Length";
            this.Length.Name = "Length";
            this.Length.Width = 60;
            // 
            // is_nullable
            // 
            this.is_nullable.DataPropertyName = "is_nullable";
            this.is_nullable.HeaderText = "is_nullable";
            this.is_nullable.Name = "is_nullable";
            this.is_nullable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_nullable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.is_nullable.Width = 60;
            // 
            // chkOutput
            // 
            this.chkOutput.HeaderText = "Output";
            this.chkOutput.Name = "chkOutput";
            this.chkOutput.Width = 50;
            // 
            // Filter
            // 
            this.Filter.HeaderText = "Filter";
            this.Filter.Name = "Filter";
            this.Filter.Width = 60;
            // 
            // Opr
            // 
            this.Opr.HeaderText = "Operator";
            this.Opr.Items.AddRange(new object[] {
            "EQUAL TO\t",
            "LIKE"});
            this.Opr.Name = "Opr";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Related Tables";
            // 
            // cmbTable
            // 
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Location = new System.Drawing.Point(83, 33);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(227, 21);
            this.cmbTable.TabIndex = 45;
            this.cmbTable.SelectedIndexChanged += new System.EventHandler(this.cmbTable_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Main Table";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(968, 188);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(170, 24);
            this.btnGenerate.TabIndex = 43;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(781, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Type";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "From Table ",
            "From Stored Procedure"});
            this.comboBox1.Location = new System.Drawing.Point(818, 111);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(226, 21);
            this.comboBox1.TabIndex = 31;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Select Stored Procedure";
            // 
            // chkSpList
            // 
            this.chkSpList.FormattingEnabled = true;
            this.chkSpList.Location = new System.Drawing.Point(139, 22);
            this.chkSpList.Name = "chkSpList";
            this.chkSpList.Size = new System.Drawing.Size(255, 21);
            this.chkSpList.TabIndex = 46;
            // 
            // chkGenList2
            // 
            this.chkGenList2.CheckOnClick = true;
            this.chkGenList2.FormattingEnabled = true;
            this.chkGenList2.Items.AddRange(new object[] {
            "Entity",
            "ReportExtensionClasses"});
            this.chkGenList2.Location = new System.Drawing.Point(139, 56);
            this.chkGenList2.Name = "chkGenList2";
            this.chkGenList2.Size = new System.Drawing.Size(255, 34);
            this.chkGenList2.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Generate List";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 108);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(255, 24);
            this.button2.TabIndex = 53;
            this.button2.Text = "Generate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_3);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.chkGenList2);
            this.groupBox2.Controls.Add(this.chkSpList);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(9, 598);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(869, 152);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "from Stored Procedure";
            this.groupBox2.Visible = false;
            // 
            // cmbDb
            // 
            this.cmbDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDb.FormattingEnabled = true;
            this.cmbDb.Location = new System.Drawing.Point(87, 75);
            this.cmbDb.Name = "cmbDb";
            this.cmbDb.Size = new System.Drawing.Size(375, 21);
            this.cmbDb.TabIndex = 33;
            this.cmbDb.SelectedIndexChanged += new System.EventHandler(this.cmbDb_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 53;
            this.label11.Text = "Database :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 766);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbDb);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.drpProject);
            this.Controls.Add(this.btnLoadDb);
            this.Controls.Add(this.txtDbCon);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
           
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtDbCon;
        private System.Windows.Forms.Button btnLoadDb;
        private System.Windows.Forms.ComboBox drpProject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox chkGenList;
        private System.Windows.Forms.DataGridView gvRelation;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblRelation;
        private System.Windows.Forms.DataGridViewComboBoxColumn DBJoin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpName;
        private System.Windows.Forms.DataGridView gvColumns;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox chkSpList;
        private System.Windows.Forms.CheckedListBox chkGenList2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Table;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewCheckBoxColumn is_nullable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkOutput;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Filter;
        private System.Windows.Forms.DataGridViewComboBoxColumn Opr;
        private System.Windows.Forms.ComboBox cmbDb;
        private System.Windows.Forms.Label label11;
    }
}

