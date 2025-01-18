
namespace DALGenerator
{
    partial class SupabaseSPGen
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
            this.txtDirctory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtDbCon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoadDB = new System.Windows.Forms.Button();
            this.ddlTables = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnFunc = new System.Windows.Forms.Button();
            this.ddl_detl1 = new System.Windows.Forms.ComboBox();
            this.ddl_detl2 = new System.Windows.Forms.ComboBox();
            this.ddl_detl3 = new System.Windows.Forms.ComboBox();
            this.ddl_detl4 = new System.Windows.Forms.ComboBox();
            this.btnGenSp = new System.Windows.Forms.Button();
            this.ddl_json_column1 = new System.Windows.Forms.ComboBox();
            this.ddl_json_column2 = new System.Windows.Forms.ComboBox();
            this.ddl_json_column3 = new System.Windows.Forms.ComboBox();
            this.ddl_json_column4 = new System.Windows.Forms.ComboBox();
            this.ddl_json_column5 = new System.Windows.Forms.ComboBox();
            this.ddl_detl5 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_function = new System.Windows.Forms.ComboBox();
            this.btnGenFuncBackEnd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grid_table_output = new System.Windows.Forms.DataGridView();
            this.grid_allselect_columns = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ddljoinwith = new System.Windows.Forms.ComboBox();
            this.btnAddToSelect = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.ddljoinwithcolumn = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ddljointocolumn = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddljointo = new System.Windows.Forms.ComboBox();
            this.btnAddJoin = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.ddlOutputTables = new System.Windows.Forms.ComboBox();
            this.chkAllOutput = new System.Windows.Forms.CheckBox();
            this.cmbParentMenu = new System.Windows.Forms.ComboBox();
            this.ddlSingleDoubleColumn = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_table_output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_allselect_columns)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDirctory
            // 
            this.txtDirctory.Location = new System.Drawing.Point(12, 9);
            this.txtDirctory.Name = "txtDirctory";
            this.txtDirctory.Size = new System.Drawing.Size(221, 22);
            this.txtDirctory.TabIndex = 51;
            this.txtDirctory.Text = "D:\\Generated_Files";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 30);
            this.button1.TabIndex = 52;
            this.button1.Text = "Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDbCon
            // 
            this.txtDbCon.Location = new System.Drawing.Point(453, 12);
            this.txtDbCon.Margin = new System.Windows.Forms.Padding(4);
            this.txtDbCon.Name = "txtDbCon";
            this.txtDbCon.Size = new System.Drawing.Size(643, 22);
            this.txtDbCon.TabIndex = 64;
            this.txtDbCon.Text = "User Id=postgres;Password=123456;Server=4.194.89.164;Port=5432;Database=postgres;" +
    "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 63;
            this.label6.Text = "Connection :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 16);
            this.label7.TabIndex = 66;
            this.label7.Text = "Table Name";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(517, 614);
            this.dataGridView1.TabIndex = 67;
            // 
            // btnLoadDB
            // 
            this.btnLoadDB.Location = new System.Drawing.Point(1115, 8);
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
            this.ddlTables.Location = new System.Drawing.Point(101, 41);
            this.ddlTables.Name = "ddlTables";
            this.ddlTables.Size = new System.Drawing.Size(317, 24);
            this.ddlTables.TabIndex = 69;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(437, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 31);
            this.button2.TabIndex = 70;
            this.button2.Text = "Load Table";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnLoadColumn);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1452, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 34);
            this.button3.TabIndex = 77;
            this.button3.Text = "Menu Action";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnFunc
            // 
            this.btnFunc.Location = new System.Drawing.Point(1569, 3);
            this.btnFunc.Name = "btnFunc";
            this.btnFunc.Size = new System.Drawing.Size(119, 34);
            this.btnFunc.TabIndex = 96;
            this.btnFunc.Text = "Back End Gen";
            this.btnFunc.UseVisualStyleBackColor = true;
            this.btnFunc.Click += new System.EventHandler(this.btnFunc_Click);
            // 
            // ddl_detl1
            // 
            this.ddl_detl1.FormattingEnabled = true;
            this.ddl_detl1.Location = new System.Drawing.Point(538, 136);
            this.ddl_detl1.Name = "ddl_detl1";
            this.ddl_detl1.Size = new System.Drawing.Size(256, 24);
            this.ddl_detl1.TabIndex = 101;
            // 
            // ddl_detl2
            // 
            this.ddl_detl2.FormattingEnabled = true;
            this.ddl_detl2.Location = new System.Drawing.Point(538, 226);
            this.ddl_detl2.Name = "ddl_detl2";
            this.ddl_detl2.Size = new System.Drawing.Size(256, 24);
            this.ddl_detl2.TabIndex = 103;
            // 
            // ddl_detl3
            // 
            this.ddl_detl3.FormattingEnabled = true;
            this.ddl_detl3.Location = new System.Drawing.Point(538, 314);
            this.ddl_detl3.Name = "ddl_detl3";
            this.ddl_detl3.Size = new System.Drawing.Size(256, 24);
            this.ddl_detl3.TabIndex = 104;
            // 
            // ddl_detl4
            // 
            this.ddl_detl4.FormattingEnabled = true;
            this.ddl_detl4.Location = new System.Drawing.Point(538, 400);
            this.ddl_detl4.Name = "ddl_detl4";
            this.ddl_detl4.Size = new System.Drawing.Size(256, 24);
            this.ddl_detl4.TabIndex = 105;
            // 
            // btnGenSp
            // 
            this.btnGenSp.Location = new System.Drawing.Point(538, 512);
            this.btnGenSp.Name = "btnGenSp";
            this.btnGenSp.Size = new System.Drawing.Size(256, 29);
            this.btnGenSp.TabIndex = 106;
            this.btnGenSp.Text = "Generate SP";
            this.btnGenSp.UseVisualStyleBackColor = true;
            this.btnGenSp.Click += new System.EventHandler(this.btnGenSp_Click);
            // 
            // ddl_json_column1
            // 
            this.ddl_json_column1.FormattingEnabled = true;
            this.ddl_json_column1.Location = new System.Drawing.Point(538, 106);
            this.ddl_json_column1.Name = "ddl_json_column1";
            this.ddl_json_column1.Size = new System.Drawing.Size(256, 24);
            this.ddl_json_column1.TabIndex = 108;
            // 
            // ddl_json_column2
            // 
            this.ddl_json_column2.FormattingEnabled = true;
            this.ddl_json_column2.Location = new System.Drawing.Point(538, 196);
            this.ddl_json_column2.Name = "ddl_json_column2";
            this.ddl_json_column2.Size = new System.Drawing.Size(256, 24);
            this.ddl_json_column2.TabIndex = 109;
            // 
            // ddl_json_column3
            // 
            this.ddl_json_column3.FormattingEnabled = true;
            this.ddl_json_column3.Location = new System.Drawing.Point(538, 284);
            this.ddl_json_column3.Name = "ddl_json_column3";
            this.ddl_json_column3.Size = new System.Drawing.Size(256, 24);
            this.ddl_json_column3.TabIndex = 110;
            // 
            // ddl_json_column4
            // 
            this.ddl_json_column4.FormattingEnabled = true;
            this.ddl_json_column4.Location = new System.Drawing.Point(538, 370);
            this.ddl_json_column4.Name = "ddl_json_column4";
            this.ddl_json_column4.Size = new System.Drawing.Size(256, 24);
            this.ddl_json_column4.TabIndex = 111;
            // 
            // ddl_json_column5
            // 
            this.ddl_json_column5.FormattingEnabled = true;
            this.ddl_json_column5.Location = new System.Drawing.Point(538, 452);
            this.ddl_json_column5.Name = "ddl_json_column5";
            this.ddl_json_column5.Size = new System.Drawing.Size(256, 24);
            this.ddl_json_column5.TabIndex = 113;
            // 
            // ddl_detl5
            // 
            this.ddl_detl5.FormattingEnabled = true;
            this.ddl_detl5.Location = new System.Drawing.Point(538, 482);
            this.ddl_detl5.Name = "ddl_detl5";
            this.ddl_detl5.Size = new System.Drawing.Size(256, 24);
            this.ddl_detl5.TabIndex = 112;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 721);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 16);
            this.label12.TabIndex = 114;
            this.label12.Text = "Select Function";
            // 
            // cmb_function
            // 
            this.cmb_function.FormattingEnabled = true;
            this.cmb_function.Location = new System.Drawing.Point(153, 718);
            this.cmb_function.Name = "cmb_function";
            this.cmb_function.Size = new System.Drawing.Size(376, 24);
            this.cmb_function.TabIndex = 115;
            // 
            // btnGenFuncBackEnd
            // 
            this.btnGenFuncBackEnd.Location = new System.Drawing.Point(538, 710);
            this.btnGenFuncBackEnd.Name = "btnGenFuncBackEnd";
            this.btnGenFuncBackEnd.Size = new System.Drawing.Size(217, 39);
            this.btnGenFuncBackEnd.TabIndex = 116;
            this.btnGenFuncBackEnd.Text = "Generate Function BackEnd";
            this.btnGenFuncBackEnd.UseVisualStyleBackColor = true;
            this.btnGenFuncBackEnd.Click += new System.EventHandler(this.btnGenFuncBackEnd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(568, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 16);
            this.label1.TabIndex = 117;
            this.label1.Text = "Master Table Json Column/ Table 1";
            // 
            // grid_table_output
            // 
            this.grid_table_output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_table_output.Location = new System.Drawing.Point(804, 398);
            this.grid_table_output.Name = "grid_table_output";
            this.grid_table_output.RowHeadersVisible = false;
            this.grid_table_output.RowHeadersWidth = 51;
            this.grid_table_output.RowTemplate.Height = 24;
            this.grid_table_output.Size = new System.Drawing.Size(282, 303);
            this.grid_table_output.TabIndex = 125;
            // 
            // grid_allselect_columns
            // 
            this.grid_allselect_columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_allselect_columns.Location = new System.Drawing.Point(1106, 106);
            this.grid_allselect_columns.Name = "grid_allselect_columns";
            this.grid_allselect_columns.RowHeadersVisible = false;
            this.grid_allselect_columns.RowHeadersWidth = 51;
            this.grid_allselect_columns.RowTemplate.Height = 24;
            this.grid_allselect_columns.Size = new System.Drawing.Size(582, 554);
            this.grid_allselect_columns.TabIndex = 126;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(568, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 16);
            this.label2.TabIndex = 127;
            this.label2.Text = "Master Table Json Column/ Table 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(568, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 16);
            this.label3.TabIndex = 128;
            this.label3.Text = "Master Table Json Column/ Table 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(568, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 16);
            this.label4.TabIndex = 129;
            this.label4.Text = "Master Table Json Column/ Table 4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(568, 433);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 16);
            this.label8.TabIndex = 130;
            this.label8.Text = "Master Table Json Column/ Table 5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(801, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 133;
            this.label5.Text = "Join With Table";
            // 
            // ddljoinwith
            // 
            this.ddljoinwith.FormattingEnabled = true;
            this.ddljoinwith.Location = new System.Drawing.Point(804, 106);
            this.ddljoinwith.Name = "ddljoinwith";
            this.ddljoinwith.Size = new System.Drawing.Size(282, 24);
            this.ddljoinwith.TabIndex = 132;
            this.ddljoinwith.SelectedIndexChanged += new System.EventHandler(this.ddljoinwith_SelectedIndexChanged);
            // 
            // btnAddToSelect
            // 
            this.btnAddToSelect.Location = new System.Drawing.Point(804, 707);
            this.btnAddToSelect.Name = "btnAddToSelect";
            this.btnAddToSelect.Size = new System.Drawing.Size(282, 31);
            this.btnAddToSelect.TabIndex = 134;
            this.btnAddToSelect.Text = "Add To Select";
            this.btnAddToSelect.UseVisualStyleBackColor = true;
            this.btnAddToSelect.Click += new System.EventHandler(this.btnAddToSelect_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(801, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 16);
            this.label9.TabIndex = 136;
            this.label9.Text = "Join With Column";
            // 
            // ddljoinwithcolumn
            // 
            this.ddljoinwithcolumn.FormattingEnabled = true;
            this.ddljoinwithcolumn.Location = new System.Drawing.Point(804, 152);
            this.ddljoinwithcolumn.Name = "ddljoinwithcolumn";
            this.ddljoinwithcolumn.Size = new System.Drawing.Size(282, 24);
            this.ddljoinwithcolumn.TabIndex = 135;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(801, 229);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 16);
            this.label10.TabIndex = 140;
            this.label10.Text = "Join To Column";
            // 
            // ddljointocolumn
            // 
            this.ddljointocolumn.FormattingEnabled = true;
            this.ddljointocolumn.Location = new System.Drawing.Point(804, 248);
            this.ddljointocolumn.Name = "ddljointocolumn";
            this.ddljointocolumn.Size = new System.Drawing.Size(282, 24);
            this.ddljointocolumn.TabIndex = 139;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(801, 183);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 16);
            this.label11.TabIndex = 138;
            this.label11.Text = "Join To Table";
            // 
            // ddljointo
            // 
            this.ddljointo.FormattingEnabled = true;
            this.ddljointo.Location = new System.Drawing.Point(804, 202);
            this.ddljointo.Name = "ddljointo";
            this.ddljointo.Size = new System.Drawing.Size(282, 24);
            this.ddljointo.TabIndex = 137;
            this.ddljointo.SelectedIndexChanged += new System.EventHandler(this.ddljointo_SelectedIndexChanged);
            // 
            // btnAddJoin
            // 
            this.btnAddJoin.Location = new System.Drawing.Point(804, 284);
            this.btnAddJoin.Name = "btnAddJoin";
            this.btnAddJoin.Size = new System.Drawing.Size(282, 31);
            this.btnAddJoin.TabIndex = 141;
            this.btnAddJoin.Text = "Add Table Join";
            this.btnAddJoin.UseVisualStyleBackColor = true;
            this.btnAddJoin.Click += new System.EventHandler(this.btnAddJoin_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(801, 346);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 16);
            this.label13.TabIndex = 143;
            this.label13.Text = "Output Tables";
            // 
            // ddlOutputTables
            // 
            this.ddlOutputTables.FormattingEnabled = true;
            this.ddlOutputTables.Location = new System.Drawing.Point(804, 365);
            this.ddlOutputTables.Name = "ddlOutputTables";
            this.ddlOutputTables.Size = new System.Drawing.Size(213, 24);
            this.ddlOutputTables.TabIndex = 142;
            this.ddlOutputTables.SelectedIndexChanged += new System.EventHandler(this.ddlOutputTables_SelectedIndexChanged);
            // 
            // chkAllOutput
            // 
            this.chkAllOutput.AutoSize = true;
            this.chkAllOutput.Location = new System.Drawing.Point(1042, 367);
            this.chkAllOutput.Name = "chkAllOutput";
            this.chkAllOutput.Size = new System.Drawing.Size(44, 20);
            this.chkAllOutput.TabIndex = 144;
            this.chkAllOutput.Text = "All";
            this.chkAllOutput.UseVisualStyleBackColor = true;
            this.chkAllOutput.CheckedChanged += new System.EventHandler(this.chkAllOutput_CheckedChanged);
            // 
            // cmbParentMenu
            // 
            this.cmbParentMenu.FormattingEnabled = true;
            this.cmbParentMenu.Location = new System.Drawing.Point(1483, 672);
            this.cmbParentMenu.Name = "cmbParentMenu";
            this.cmbParentMenu.Size = new System.Drawing.Size(205, 24);
            this.cmbParentMenu.TabIndex = 148;
            // 
            // ddlSingleDoubleColumn
            // 
            this.ddlSingleDoubleColumn.FormattingEnabled = true;
            this.ddlSingleDoubleColumn.Items.AddRange(new object[] {
            "Single Column",
            "Double Column"});
            this.ddlSingleDoubleColumn.Location = new System.Drawing.Point(1203, 672);
            this.ddlSingleDoubleColumn.Name = "ddlSingleDoubleColumn";
            this.ddlSingleDoubleColumn.Size = new System.Drawing.Size(205, 24);
            this.ddlSingleDoubleColumn.TabIndex = 147;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1103, 675);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 16);
            this.label14.TabIndex = 146;
            this.label14.Text = "Style Column";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(1483, 703);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(205, 34);
            this.btnGenerate.TabIndex = 145;
            this.btnGenerate.Text = "Generate Table BackEnd ";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1414, 672);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 16);
            this.label15.TabIndex = 149;
            this.label15.Text = "Module";
            // 
            // SupabaseSPGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1700, 762);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbParentMenu);
            this.Controls.Add(this.ddlSingleDoubleColumn);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.chkAllOutput);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ddlOutputTables);
            this.Controls.Add(this.btnAddJoin);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ddljointocolumn);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ddljointo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ddljoinwithcolumn);
            this.Controls.Add(this.btnAddToSelect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ddljoinwith);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grid_allselect_columns);
            this.Controls.Add(this.grid_table_output);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenFuncBackEnd);
            this.Controls.Add(this.cmb_function);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ddl_json_column5);
            this.Controls.Add(this.ddl_detl5);
            this.Controls.Add(this.ddl_json_column4);
            this.Controls.Add(this.ddl_json_column3);
            this.Controls.Add(this.ddl_json_column2);
            this.Controls.Add(this.ddl_json_column1);
            this.Controls.Add(this.btnGenSp);
            this.Controls.Add(this.ddl_detl4);
            this.Controls.Add(this.ddl_detl3);
            this.Controls.Add(this.ddl_detl2);
            this.Controls.Add(this.ddl_detl1);
            this.Controls.Add(this.btnFunc);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ddlTables);
            this.Controls.Add(this.btnLoadDB);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDbCon);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDirctory);
            this.Name = "SupabaseSPGen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SupabaseSPGen_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_table_output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_allselect_columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtDirctory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtDbCon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoadDB;
        private System.Windows.Forms.ComboBox ddlTables;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnFunc;
        private System.Windows.Forms.ComboBox ddl_detl1;
        private System.Windows.Forms.ComboBox ddl_detl2;
        private System.Windows.Forms.ComboBox ddl_detl3;
        private System.Windows.Forms.ComboBox ddl_detl4;
        private System.Windows.Forms.Button btnGenSp;
        private System.Windows.Forms.ComboBox ddl_json_column1;
        private System.Windows.Forms.ComboBox ddl_json_column2;
        private System.Windows.Forms.ComboBox ddl_json_column3;
        private System.Windows.Forms.ComboBox ddl_json_column4;
        private System.Windows.Forms.ComboBox ddl_json_column5;
        private System.Windows.Forms.ComboBox ddl_detl5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmb_function;
        private System.Windows.Forms.Button btnGenFuncBackEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grid_table_output;
        private System.Windows.Forms.DataGridView grid_allselect_columns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ddljoinwith;
        private System.Windows.Forms.Button btnAddToSelect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddljoinwithcolumn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ddljointocolumn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddljointo;
        private System.Windows.Forms.Button btnAddJoin;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox ddlOutputTables;
        private System.Windows.Forms.CheckBox chkAllOutput;
        private System.Windows.Forms.ComboBox cmbParentMenu;
        private System.Windows.Forms.ComboBox ddlSingleDoubleColumn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label15;
    }
}