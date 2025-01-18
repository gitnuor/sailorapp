
namespace DALGenerator
{
    partial class Form3
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
            this.txtScript = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtDirctory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtS2Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTextFieldName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValueFieldName = new System.Windows.Forms.TextBox();
            this.btnGnerateSelect2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.btnFunc = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtScript
            // 
            this.txtScript.Location = new System.Drawing.Point(27, 80);
            this.txtScript.Margin = new System.Windows.Forms.Padding(4);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(910, 680);
            this.txtScript.TabIndex = 49;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(162, 46);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(228, 30);
            this.btnGenerate.TabIndex = 50;
            this.btnGenerate.Text = "Generate BackEnd";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtDirctory
            // 
            this.txtDirctory.Location = new System.Drawing.Point(162, 17);
            this.txtDirctory.Name = "txtDirctory";
            this.txtDirctory.Size = new System.Drawing.Size(775, 22);
            this.txtDirctory.TabIndex = 51;
            this.txtDirctory.Text = "D:\\Generated_Files";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 30);
            this.button1.TabIndex = 52;
            this.button1.Text = "Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(983, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Generate Select2";
            // 
            // txtS2Name
            // 
            this.txtS2Name.Location = new System.Drawing.Point(955, 197);
            this.txtS2Name.Name = "txtS2Name";
            this.txtS2Name.Size = new System.Drawing.Size(182, 22);
            this.txtS2Name.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(952, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 55;
            this.label2.Text = "S2 Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(952, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Text Field";
            // 
            // txtTextFieldName
            // 
            this.txtTextFieldName.Location = new System.Drawing.Point(955, 263);
            this.txtTextFieldName.Name = "txtTextFieldName";
            this.txtTextFieldName.Size = new System.Drawing.Size(182, 22);
            this.txtTextFieldName.TabIndex = 56;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(952, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 59;
            this.label4.Text = "Value Field";
            // 
            // txtValueFieldName
            // 
            this.txtValueFieldName.Location = new System.Drawing.Point(955, 335);
            this.txtValueFieldName.Name = "txtValueFieldName";
            this.txtValueFieldName.Size = new System.Drawing.Size(182, 22);
            this.txtValueFieldName.TabIndex = 58;
            // 
            // btnGnerateSelect2
            // 
            this.btnGnerateSelect2.Location = new System.Drawing.Point(955, 374);
            this.btnGnerateSelect2.Name = "btnGnerateSelect2";
            this.btnGnerateSelect2.Size = new System.Drawing.Size(182, 23);
            this.btnGnerateSelect2.TabIndex = 60;
            this.btnGnerateSelect2.Text = "Generate Select2";
            this.btnGnerateSelect2.UseVisualStyleBackColor = true;
            this.btnGnerateSelect2.Click += new System.EventHandler(this.btnGnerateSelect2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(952, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 62;
            this.label5.Text = "Entity Name";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(955, 144);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(182, 22);
            this.txtEntityName.TabIndex = 61;
            // 
            // btnFunc
            // 
            this.btnFunc.Location = new System.Drawing.Point(957, 590);
            this.btnFunc.Name = "btnFunc";
            this.btnFunc.Size = new System.Drawing.Size(182, 56);
            this.btnFunc.TabIndex = 97;
            this.btnFunc.Text = "Menu Action Gen";
            this.btnFunc.UseVisualStyleBackColor = true;
            this.btnFunc.Click += new System.EventHandler(this.btnFunc_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(955, 519);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 56);
            this.button3.TabIndex = 96;
            this.button3.Text = "Db Back End Gen";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 784);
            this.Controls.Add(this.btnFunc);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.btnGnerateSelect2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValueFieldName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTextFieldName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtS2Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDirctory);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtScript);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtDirctory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtS2Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTextFieldName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValueFieldName;
        private System.Windows.Forms.Button btnGnerateSelect2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Button btnFunc;
        private System.Windows.Forms.Button button3;
    }
}