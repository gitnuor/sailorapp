namespace DALGenerator
{
    partial class MenuActionGenerator
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
            this.btnAddMenu = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbParentMenu = new System.Windows.Forms.ComboBox();
            this.btnLoadDB = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.action_url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.method_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDbCon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDirctory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_TaretMenu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Controller = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnFunc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Location = new System.Drawing.Point(962, 131);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(182, 85);
            this.btnAddMenu.TabIndex = 87;
            this.btnAddMenu.Text = "Add Menu Action";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new System.EventHandler(this.btnAddMenu_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 86;
            this.label9.Text = "Parent Menu";
            // 
            // cmbParentMenu
            // 
            this.cmbParentMenu.FormattingEnabled = true;
            this.cmbParentMenu.Location = new System.Drawing.Point(151, 84);
            this.cmbParentMenu.Name = "cmbParentMenu";
            this.cmbParentMenu.Size = new System.Drawing.Size(205, 24);
            this.cmbParentMenu.TabIndex = 85;
            this.cmbParentMenu.SelectedIndexChanged += new System.EventHandler(this.cmbParentMenu_SelectedIndexChanged);
            // 
            // btnLoadDB
            // 
            this.btnLoadDB.Location = new System.Drawing.Point(962, 44);
            this.btnLoadDB.Name = "btnLoadDB";
            this.btnLoadDB.Size = new System.Drawing.Size(182, 28);
            this.btnLoadDB.TabIndex = 82;
            this.btnLoadDB.Text = "Load DB";
            this.btnLoadDB.UseVisualStyleBackColor = true;
            this.btnLoadDB.Click += new System.EventHandler(this.btnLoadDB_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.action_url,
            this.method_name,
            this.is_select});
            this.dataGridView1.Location = new System.Drawing.Point(25, 131);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(910, 587);
            this.dataGridView1.TabIndex = 81;
            // 
            // action_url
            // 
            this.action_url.DataPropertyName = "action_url";
            this.action_url.HeaderText = "Action URL";
            this.action_url.MinimumWidth = 6;
            this.action_url.Name = "action_url";
            this.action_url.Width = 340;
            // 
            // method_name
            // 
            this.method_name.DataPropertyName = "method_name";
            this.method_name.HeaderText = "Method_Name";
            this.method_name.MinimumWidth = 6;
            this.method_name.Name = "method_name";
            this.method_name.Width = 250;
            // 
            // is_select
            // 
            this.is_select.DataPropertyName = "is_select";
            this.is_select.FalseValue = "false";
            this.is_select.HeaderText = "is_approved";
            this.is_select.MinimumWidth = 6;
            this.is_select.Name = "is_select";
            this.is_select.TrueValue = "true";
            this.is_select.Width = 125;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 16);
            this.label7.TabIndex = 80;
            this.label7.Text = "Controller Location";
            // 
            // txtDbCon
            // 
            this.txtDbCon.Location = new System.Drawing.Point(151, 14);
            this.txtDbCon.Margin = new System.Windows.Forms.Padding(4);
            this.txtDbCon.Name = "txtDbCon";
            this.txtDbCon.Size = new System.Drawing.Size(784, 22);
            this.txtDbCon.TabIndex = 79;
            this.txtDbCon.Text = "User Id=postgres;Password=123456;Server=4.194.89.164;Port=5432;Database=sailor_db" +
    "_15_aug;";
            this.txtDbCon.TextChanged += new System.EventHandler(this.txtDbCon_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 78;
            this.label6.Text = "Connection :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(830, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 30);
            this.button1.TabIndex = 89;
            this.button1.Text = "Directory";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtDirctory
            // 
            this.txtDirctory.Location = new System.Drawing.Point(151, 48);
            this.txtDirctory.Name = "txtDirctory";
            this.txtDirctory.Size = new System.Drawing.Size(672, 22);
            this.txtDirctory.TabIndex = 88;
            this.txtDirctory.Text = "C:\\Users\\nishad\\source\\repos\\sailor\\EPYSLSAILORAPP\\Controllers";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 91;
            this.label1.Text = "Target Menu";
            // 
            // cmb_TaretMenu
            // 
            this.cmb_TaretMenu.FormattingEnabled = true;
            this.cmb_TaretMenu.Location = new System.Drawing.Point(466, 84);
            this.cmb_TaretMenu.Name = "cmb_TaretMenu";
            this.cmb_TaretMenu.Size = new System.Drawing.Size(355, 24);
            this.cmb_TaretMenu.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(827, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 93;
            this.label2.Text = "Controller";
            // 
            // cmb_Controller
            // 
            this.cmb_Controller.FormattingEnabled = true;
            this.cmb_Controller.Location = new System.Drawing.Point(897, 84);
            this.cmb_Controller.Name = "cmb_Controller";
            this.cmb_Controller.Size = new System.Drawing.Size(247, 24);
            this.cmb_Controller.TabIndex = 92;
            this.cmb_Controller.SelectedIndexChanged += new System.EventHandler(this.cmb_Controller_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(962, 591);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 56);
            this.button3.TabIndex = 94;
            this.button3.Text = "Db Back End Gen";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnFunc
            // 
            this.btnFunc.Location = new System.Drawing.Point(964, 662);
            this.btnFunc.Name = "btnFunc";
            this.btnFunc.Size = new System.Drawing.Size(182, 56);
            this.btnFunc.TabIndex = 95;
            this.btnFunc.Text = "Function Gen";
            this.btnFunc.UseVisualStyleBackColor = true;
            this.btnFunc.Click += new System.EventHandler(this.btnFunc_Click);
            // 
            // MenuActionGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 730);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnFunc);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Controller);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_TaretMenu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddMenu);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbParentMenu);
            this.Controls.Add(this.btnLoadDB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDbCon);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDirctory);
            this.Name = "MenuActionGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuActionGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddMenu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbParentMenu;
        private System.Windows.Forms.Button btnLoadDB;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDbCon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDirctory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_TaretMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Controller;
        private System.Windows.Forms.DataGridViewTextBoxColumn action_url;
        private System.Windows.Forms.DataGridViewTextBoxColumn method_name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn is_select;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnFunc;
    }
}