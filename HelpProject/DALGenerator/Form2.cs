using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DALGenerator
{
    public partial class Form2 : Form
    {
        private bool state;
        DataTable dtRelation = new DataTable();
        DataTable dtColumn = new DataTable();
        DataTable GetColumns = new DataTable();
        List<string> strcolumnList = new List<string>();
        private bool checkshiftkey = false;
        private bool checkshiftkey2 = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //
                // The user selected a folder and pressed the OK button.
                // We print the number of files found.
                //
              //  txtDirectory.Text = folderBrowserDialog1.SelectedPath;

                //if (txtDirectory.Text.Trim() != "")
                //{
                //    string[] directoryList = System.IO.Directory.GetDirectories(txtDirectory.Text.Trim());

                //    Array.Resize(ref directoryList, directoryList.Length + 1);
                //    directoryList[directoryList.Length - 1] = txtDirectory.Text + "\\Web Project Directory";

                //}

            }
        }
        public Boolean CheckTableForMat(String TableName)
        {
            Boolean bol = true;
            DataTable dt = new DataTable();
            dt = getColumnsName(TableName);
            int check = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Name"].ToString().ToUpper() == TableName.ToUpper() + "ID")
                    check++;
                if (dt.Rows[i]["Name"].ToString().ToUpper() == TableName.ToUpper() + "NAME")
                    check++;

                if (check == 2)
                    return true;
            }


            return false;
        }

        public void GetTableList()
        {
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ;//"Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT  Name from Sysobjects where xtype = 'u' order by Name", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            cmbTable.Items.Add(dr[0].ToString());
                        }
                    }
                    con.Close();
                }
            }

        }
        public DataTable GetParamListBySpName(string spname)
        {
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ;//"Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("select * from information_schema.parameters  where specific_name='" + spname + "'", con))
                {

                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                }
            }

            return dt;

        }
        public void GetSpList()
        {
            if (cmbDb.Text.Length > 0)
            {
                string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ;//"Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(" select *  from    sys.procedures  order by name   ", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                //chkSpList.Items.Add(dr[0].ToString());
                            }
                        }
                    }
                }
            }
        }


        public DataTable getColumnsName(String Table)
        {
            DataTable dt = new DataTable();
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ; ;

            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT        c.name AS 'Name', t.name AS 'Type', c.max_length AS 'Length', ISNULL(i.is_primary_key, 0) AS 'PK', c.is_nullable,'" + Table + "'" + @" as tbl, 1 as chkOutput
                         FROM            sys.columns AS c INNER JOIN
                         sys.types AS t ON c.user_type_id = t.user_type_id LEFT OUTER JOIN
                         sys.index_columns AS ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN
                         sys.indexes AS i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + Table + "')";
                connection.Open();

                dt.Load(command.ExecuteReader());
                connection.Close();
            }
            return dt;
        }


        public DataTable RunQueryforDatatable(String Query)
        {
            DataTable dt = new DataTable();
            string conString = "Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";

            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = Query;
                connection.Open();
                dt.Load(command.ExecuteReader());
                connection.Close();
            }
            return dt;
        }

        public DataTable getTableColumnsNames(String Table)
        {
            DataTable dt = new DataTable();
            string conString = "Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";

            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COLUMN_NAME,DATA_TYPE," +
                "(" +
                " SELECT  COL_NAME(ic.OBJECT_ID,ic.column_id) " +
                " FROM    sys.indexes AS i INNER JOIN " +
                " sys.index_columns AS ic ON  i.OBJECT_ID = ic.OBJECT_ID " +
                " AND i.index_id = ic.index_id " +
                " WHERE  " +
                " OBJECT_NAME(ic.OBJECT_ID)='Table' and COL_NAME(ic.OBJECT_ID,ic.column_id)=COLUMN_NAME " +
                " ) as PK" +
                " ," +
                " (" +
                " SELECT c1.name from sys.objects o1 " +
                " INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id " +
                " INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id " +
                " INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id " +
                " INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id " +
                " INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id " +
                " INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id " +
                " where o1.name='" + Table + "' and c1.name=COLUMN_NAME" +
                " ) as FK_column" +
                "," +
                "(" +
                " SELECT o2.name from sys.objects o1" +
                " INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id " +
                " INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id " +
                " INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id " +
                " INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id " +
                " INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id " +
                " INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id " +
                " where o1.name='" + Table + "' and c1.name=COLUMN_NAME" +
                " ) as PK_table" +
                "," +
                "(" +
                " SELECT c2.name from sys.objects o1" +
                " INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id " +
                " INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id " +
                " INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id " +
                " INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id " +
                " INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id " +
                " INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id " +
                " where o1.name='" + Table + "' and c1.name=COLUMN_NAME " +
                " ) as PK_column " +
                " FROM INFORMATION_SCHEMA.COLUMNS " +
                " WHERE TABLE_NAME =  '" + Table + "'";
                connection.Open();

                dt.Load(command.ExecuteReader());
                connection.Close();
            }
            return dt;
        }
        private void btnLoadServer_Click(object sender, EventArgs e)
        {
            //if (txtServer.Text.Trim() == "")
            //    MessageBox.Show("Please Provide Server Name");
            //else
            //{
            //    cmbDbs.DataSource = GetDatabaseList();
            //}
        }

        private void btnLoadTables_Click(object sender, EventArgs e)
        {

        }


        public string stringspace(String theString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in theString)
            {
                if (Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
                builder.Append(c);
            }
            return builder.ToString();

        }
        public static void DeleteLastLine(string filepath, int del = 2)
        {
            List<string> lines = File.ReadAllLines(filepath).ToList();

            File.WriteAllLines(filepath, lines.GetRange(0, lines.Count - del).ToArray());

        }

        public bool CheckExistance(string filepath, string functionname)
        {

            List<string> lines = File.ReadAllLines(filepath).ToList();

            foreach (string str in lines)
            {
                if (str.ToLower().Contains(functionname.ToLower()))
                    return true;
            }
            return false;

        }
        private DataTable GetColumnsFromSP(string Sp)
        {
            DataTable dt = new DataTable();
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ; ;

            using (SqlConnection connection = new SqlConnection(conString))

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT name, case when CHARINDEX('(',system_type_name)>0 then  SUBSTRING( system_type_name,1,CHARINDEX('(',system_type_name)-1)
				else system_type_name end as Type,
                case when CHARINDEX('(',system_type_name)>0 then  SUBSTRING( system_type_name,CHARINDEX('(',system_type_name)+1,len(system_type_name)-1-CHARINDEX('(',system_type_name))
				                else system_type_name end as Length
                 FROM sys.dm_exec_describe_first_result_set_for_object
                (OBJECT_ID('" + Sp + "'), NULL);";
                connection.Open();

                dt.Load(command.ExecuteReader());
                connection.Close();
            }
            return dt;
        }



        public string GetFileText(string path)
        {
            string readText = "";

            if (File.Exists(path))
            {
                readText = File.ReadAllText(path);
            }

            return readText;
        }




        public string GetDotNetDataType(string DataType)
        {
            if (DataType.ToUpper() == "VARCHAR" || DataType.ToUpper() == "NVARCHAR" || DataType.ToUpper() == "CHAR")
            {
                return "String";
            }
            else if (DataType.ToUpper() == "INT" || DataType.ToUpper() == "TINYINT")
            {
                return "Int32";
            }
            else if (DataType.ToUpper() == "BIGINT")
            {
                return "Int64";
            }
            else if (DataType.ToUpper() == "MONEY")
            {
                return "Decimal";
            }
            else if (DataType.ToUpper() == "BIT")
            {
                return "Boolean";
            }
            else if (DataType.ToUpper() == "DATE" || DataType.ToUpper() == "DATETIME" || DataType.ToUpper() == "SMALLDATETIME")
            {
                return "DateTime";
            }
            else if (DataType.ToUpper() == "DECIMAL" || DataType.ToUpper() == "FLOAT" || DataType.ToUpper() == "NUMERIC")
            {
                return "Decimal";
            }
            return "";
        }




        private void txtPageHeader_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                // txtPageHeader.BackColor = colorDialog1.Color;
            }
        }

        private void txtTableBack_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                // txtTableBack.BackColor = colorDialog1.Color;
            }
        }

        private void txtGridHeader_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                // txtGridHeader.BackColor = colorDialog1.Color;
            }
        }

        private void txtGridBack_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                // txtGridBack.BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // CreateSPs();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //  CreateForms();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnLoadServer_Click(sender, e);
        }



        private void btnLoadDb_Click(object sender, EventArgs e)
        {
            cmbDb.DataSource = GetDatabaseList();
        }

        public List<string> GetDatabaseList()
        {
            var connection = txtDbCon.Text;

            List<string> list = new List<string>();
            list.Add("SELECT");

            // Open connection to the database
            string conString = " database=master; " + connection.Trim() + ";";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases order by name", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            list.Add(dr[0].ToString());

                        }
                    }
                    con.Close();
                }
            }
            return list;

        }





        public void MergeTables(DataTable dtMerge)
        {
            if (dtColumn.Rows.Count == 0)
            {
                dtColumn = dtMerge.Clone();
                dtColumn.Columns["tbl"].MaxLength = 50;
                dtColumn.Columns["Name"].MaxLength = 50;
                dtColumn.Columns["Type"].MaxLength = 50;
            }

            foreach (DataRow dr in dtMerge.Rows)
            {
                if (dtColumn.Select("Name ='" + dr["Name"].ToString() + "'").Count() == 0)
                {
                    dtColumn.ImportRow(dr);
                }

            }

        }

        private void chkRelationTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                //DataTable GetColumns = getColumnsName(chkRelationTables.Items[e.Index].ToString());
                //dtColumn.Constraints.Clear();
                //GetColumns.Constraints.Clear();
                //MergeTables(GetColumns);

                //gvColumns.AutoGenerateColumns = false;
                //gvColumns.DataSource = dtColumn;
            }
        }




        private void gvRelation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void gvRelation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.ShiftKey && (e.KeyChar == (char)Keys.Down || e.KeyChar == (char)Keys.Up))
            {
                // MessageBox.Show(gvRelation.SelectedCells.ToString());
            }
        }



        private void button2_Click_3(object sender, EventArgs e)
        {
            try
            {
                //if (chkSpList.SelectedIndex > 0)
                //{

                //    MessageBox.Show("Business Classes are generated");
                //}
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void cmbDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDb.SelectedIndex != 0)
            {
                GetTableList();
            }
        }


        private string GetDisplayCol()
        {
            string ret = "";
            for (int row = 0; row < GetColumns.Rows.Count; row++)
            {
                if (GetColumns.Rows[row][1].ToString() == "varchar" ||
                   GetColumns.Rows[row][1].ToString() == "nvarchar" ||
                    GetColumns.Rows[row][1].ToString() == "text" ||
                    GetColumns.Rows[row][1].ToString() == "ntext")
                {
                    ret = GetColumns.Rows[row][0].ToString().ToLower();
                    break;
                }
            }
            return ret;
        }
        public string ReplaceKeyWord(string str,int type)
        {
            string tablename = cmbTable.Text;

            string tablenamemainpart = cmbTable.Text;

            if (tablenamemainpart.Contains('_'))
                tablenamemainpart = tablenamemainpart.Split('_')[1];

            if (type == 1)//controller
            {
                str = str.Replace("{{tablename_mainpart}}", tablenamemainpart);
                str = str.Replace("{{tablename}}", tablename.ToLower());
                str = str.Replace("{{tableidcolumn}}", GetColumns.Rows[0][0].ToString().ToLower().ToLower());
                str = str.Replace("{{tabletextcolumn}}", GetDisplayCol());
            }
            else if (type == 2)//create view
            {
                string allrowhtml = "";
                string rowtemplatehtml = GetFileText("Templates\\CreateViewRow.txt");

                allrowhtml += GetRowHtml(rowtemplatehtml);

                str = str.Replace("{{tablename_mainpart}}", tablenamemainpart);
                str = str.Replace("{{tablename}}", tablename.ToLower());
                str = str.Replace("{{allrows}}", allrowhtml);
            }
            else if (type == 3)//update view
            {
                string allrowhtml = "";
                string rowtemplatehtml = GetFileText("Templates\\CreateViewRow.txt").Replace("col-md-6", "col-md-12");

                allrowhtml += GetRowHtml(rowtemplatehtml);
                
                str = str.Replace("{{tablename_mainpart}}", tablenamemainpart);
                str = str.Replace("{{tablename}}", tablename.ToLower());
                str = str.Replace("{{tableidcolumn}}", GetColumns.Rows[0][0].ToString().ToLower().ToLower());
                str = str.Replace("{{allrows}}", allrowhtml);
            }
            else if (type == 4)//details view
            {
                string allrowhtml = "";
                string rowtemplatehtml = GetFileText("Templates\\DetailsViewRow.txt");

                allrowhtml += GetRowHtml(rowtemplatehtml);

                str = str.Replace("{{tablename_mainpart}}", tablenamemainpart);
                str = str.Replace("{{tablename}}", tablename.ToLower());
                str = str.Replace("{{allrows}}", allrowhtml);
            }
            else if (type == 5 || type == 6)
            {
                string allrowhtml = "";
                str = str.Replace("{{tablename_mainpart}}", tablenamemainpart);
                str = str.Replace("{{tablename_mainpart_lower}}", tablenamemainpart.ToLower());
                str = str.Replace("{{tablename}}", tablename.ToLower());
                str = str.Replace("{{tabletextcolumn}}", GetDisplayCol());
            }
            else if (type == 7 || type == 8)
            {

                str = str.Replace("{{tablename_mainpart}}", tablenamemainpart);
                str = str.Replace("{{tablename_mainpart_lower}}", tablenamemainpart.ToLower());
                str = str.Replace("{{tablename}}", tablename.ToLower());
                str = str.Replace("{{allrows}}", GetJsforRows());
            }

            return str;
        }

        public string GetRowHtml(string rowtemplate)
        {
              
            string rowhtml="";
                for (int row = 0; row < GetColumns.Rows.Count; row++)
                {
                    if (GetColumns.Rows[row][0].ToString().ToLower() == "ex_date1") break;
                    else if (GetColumns.Rows[row][0].ToString().ToLower() == "uniqueidentifier") continue;

                    rowhtml += rowtemplate.Replace("{{columnname}}", GetColumns.Rows[row][0].ToString().ToLower());
                }
                return rowhtml;
        }
        public string GetJsforRows()
        {

            string rowhtml = "";
            for (int row = 0; row < GetColumns.Rows.Count; row++)
            {
                if (GetColumns.Rows[row][0].ToString().ToLower() == "ex_date1") break;
                else if (GetColumns.Rows[row][0].ToString().ToLower() == "uniqueidentifier") continue;

                rowhtml += "," + GetColumns.Rows[row][0].ToString().ToLower() + ":  " + "$('#" + GetColumns.Rows[row][0].ToString().ToLower() + "').val()\n";
            }
            return rowhtml;
        }


        public void GenControl(string filename,int type)
        {
            string tablename = cmbTable.Text;

            string tablenamemainpart = cmbTable.Text;

            if (tablenamemainpart.Contains('_'))
                tablenamemainpart = tablenamemainpart.Split('_')[1];

            string filepath = ""; string strpath = "";
            string filetext = GetFileText("Templates\\" + filename);

            StringBuilder sb = new StringBuilder();            // Display the file contents by using a foreach loop.

            if (type == 1)
            {
                 filepath = @"Output\" + cmbTable.Text + "\\Controller\\" + cmbTable.Text + "Controller.cs";
                 strpath = @"Output\" + cmbTable.Text + "\\Controller";
            }
            else if (type == 2)
            {
                filepath = @"Output\" + cmbTable.Text + "\\Views\\Create" + tablenamemainpart + ".cshtml";
                strpath = @"Output\" + cmbTable.Text+"\\Views";
            }
            else if (type == 3)
            {
                filepath = @"Output\" + cmbTable.Text + "\\Views\\_Update" + tablenamemainpart + ".cshtml";
                strpath = @"Output\" + cmbTable.Text + "\\Views"; ;
            }
            else if (type == 4)
            {
                filepath = @"Output\" + cmbTable.Text + "\\Views\\_Details" + tablenamemainpart + ".cshtml";
                strpath = @"Output\" + cmbTable.Text + "\\Views"; ;
            }
            else if (type ==5)
            {
                filepath = @"Output\" + cmbTable.Text + "\\Views\\" + tablenamemainpart + "List.cshtml";
                strpath = @"Output\" + cmbTable.Text + "\\Views"; 
            }
            else if (type == 6)
            {
                filepath = @"Output\" + cmbTable.Text + "\\JS\\Load" + tablenamemainpart + ".js";
                strpath = @"Output\" + cmbTable.Text + "\\JS"; 
            }
            else if (type == 7)
            {
                filepath = @"Output\" + cmbTable.Text + "\\JS\\Create" + tablenamemainpart + ".js";
                strpath = @"Output\" + cmbTable.Text + "\\JS"; 
            }
            else if (type == 8)
            {
                filepath = @"Output\" + cmbTable.Text + "\\JS\\Update" + tablenamemainpart + ".js";
                strpath = @"Output\" + cmbTable.Text + "\\JS"; 
            }

            if (System.IO.File.Exists(filepath))
            {

                File.Delete(filepath);

            }
            Directory.CreateDirectory(strpath);

            System.IO.StreamWriter file5 = System.IO.File.AppendText(filepath);

            file5.WriteLine(ReplaceKeyWord(filetext, type));
            file5.Close();

        }

       

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                GenControl("Controller.txt", 1);
                GenControl("CreateView.txt", 2);
                GenControl("UpdateView.txt", 3);
                GenControl("DetailsView.txt", 4);
                GenControl("ListView.txt", 5);
                GenControl("JSList.txt", 6);
                GenControl("JSCreateData.txt", 7);
                GenControl("JSUpdateData.txt", 8);

                MessageBox.Show("Generation Completed");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

        }

        private void cmbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetColumns = getColumnsName(cmbTable.Text);
           // gvCols.DataSource = GetColumns;


        }

        private void gvRelation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




    }
}
