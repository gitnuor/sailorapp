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
    public partial class Form1 : Form
    {
        private bool state;
        DataTable dtRelation = new DataTable();
        DataTable dtColumn = new DataTable();
        List<string> strcolumnList = new List<string>();
        private bool checkshiftkey = false;
        private bool checkshiftkey2 = false;
        public Form1()
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
                txtDirectory.Text = folderBrowserDialog1.SelectedPath;

                if (txtDirectory.Text.Trim() != "")
                {
                    string[] directoryList = System.IO.Directory.GetDirectories(txtDirectory.Text.Trim());

                    Array.Resize(ref directoryList, directoryList.Length + 1);
                    directoryList[directoryList.Length - 1] = txtDirectory.Text + "\\Web Project Directory";
                
                    drpProject.DataSource = directoryList;
                }
                
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
            string conString = txtDbCon.Text + "Initial Catalog="+cmbDb.Text; ;//"Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";

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
            string conString = txtDbCon.Text+"Initial Catalog=" + cmbDb.Text; ;//"Data Source=192.168.25.190;User ID=sa;Password=121212@db;Initial Catalog=KAF_NationalServicePortal_V1.1;";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("select * from information_schema.parameters  where specific_name='"+spname+"'", con))
                {
                    
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                }
            }

            return dt;

        }
        public void GetSpList()
        {
            if(cmbDb.Text.Length>0)
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
                                chkSpList.Items.Add(dr[0].ToString());
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
     
        public bool getMultipleJoin(String Table,string FK_Table)
        {
            DataTable dt = new DataTable();
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ; ;

            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
                                        select r.PK_Table from
                                        (
                                        SELECT distinct
                                        K_Table  = FK.TABLE_NAME,
                                        FK_Column = CU.COLUMN_NAME,
                                        PK_Table = PK.TABLE_NAME,
                                        PK_Column = PT.COLUMN_NAME
                                       
                                        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
                                        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
                                        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
                                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
                                        INNER JOIN (
                                        SELECT i1.TABLE_NAME, i2.COLUMN_NAME
                                        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
                                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
                                        WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
                                        ) PT ON PT.TABLE_NAME = PK.TABLE_NAME
                                        )r
                                        where r.K_Table='"+Table+"'"+@" 

                                        group by r.PK_Table having count(r.PK_Table)>1";
                connection.Open();

                dt.Load(command.ExecuteReader());
                connection.Close();
            }

            if (dt.Select("PK_Table='" + FK_Table + "'").Count() > 0)
                return true;
            else
                return false;
        }

        public string GetJoin(string TableName)
        {
            foreach (DataGridViewRow row in gvRelation.Rows)
            {
                if (row.Cells["tblRelation"].Value.ToString() == TableName)
                    return row.Cells["DBJoin"].Value.ToString()+"  ";
            }
            return "";
                 
        }

        public bool CheckRelationGV(string TableName)
        {
            foreach (DataGridViewRow row in gvRelation.Rows)
            {
                if (row.Cells["tblRelation"].Value.ToString() == TableName && Convert.ToBoolean(row.Cells["Select"].Value)==true)
                    return true;
            }
            return false;

        }

        public void GenerateSP(String SpName)
        {
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ; ;

            string strparam = "";

            string strfilter = "\n\t\tWHERE";
           
            string strquery = "\n\t\tSELECT\t";

            foreach (DataGridViewRow row in gvColumns.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Filter"].Value) == true)
                {
                    string len = "";
                    if (row.Cells["Type"].Value.ToString().ToLower() == "varchar" || row.Cells["Type"].Value.ToString().ToLower() == "nvarchar" || row.Cells["Type"].Value.ToString().ToLower() == "decimal")
                        len = "(" + row.Cells["Length"].Value.ToString() + ")";

                    strparam += "\n\t@" + row.Cells["Name"].Value.ToString() + "\t" + row.Cells["Type"].Value.ToString() + len + " = NULL,";

                    strfilter += "\n\t\t(@" + row.Cells["Name"].Value.ToString() + " is NULL OR  " + row.Cells[0].Value.ToString() + "." + row.Cells["Name"].Value.ToString() + " = @" + row.Cells["Name"].Value.ToString() + ") AND";
                
                }

                if (Convert.ToBoolean(row.Cells["chkOutput"].Value) == true)
                {
                    strquery += "\n\t\t"+row.Cells["Table"].Value.ToString() + "." + row.Cells["Name"].Value.ToString()+",";
                }

            }

            strquery = strquery.Substring(0, strquery.Length - 1);

            if (strparam.Length>0)
            strparam = strparam.Substring(0, strparam.Length - 1);

            if (strfilter.Contains(" AND"))
                strfilter = strfilter.Substring(0, strfilter.Length - 4);
            else
                strfilter = strfilter.Replace("WHERE", "");

            strquery += "\n\t\tFROM " + cmbTable.Text;

            int counter = 0;

            if (dtRelation.Rows.Count > 0)
            {
                DataView dv = dtRelation.DefaultView;
                dv.RowFilter = "K_Table='" + cmbTable.Text + "'";

                DataTable dtFilter = dv.ToTable();

                foreach (DataRow drFilter in dtFilter.Rows)
                {

                    if (drFilter["PK_Table"].ToString() != cmbTable.Text)
                    {
                        if (CheckRelationGV(drFilter["PK_Table"].ToString()))
                        {
                            if (!getMultipleJoin(cmbTable.Text, drFilter["PK_Table"].ToString()))
                                strquery += "\n\t\t " + GetJoin(drFilter["PK_Table"].ToString()) + drFilter["PK_Table"].ToString() + " ON " + drFilter["K_Table"].ToString() + "." + drFilter["FK_COLUMN"].ToString() + " = " + drFilter["PK_Table"].ToString() + "." + drFilter["PK_COLUMN"].ToString();

                            else
                            {
                                if (counter == 0)
                                {
                                    strquery += "\n\t\t" + GetJoin(drFilter["PK_Table"].ToString()) + drFilter["PK_Table"].ToString() + " ON " + drFilter["K_Table"].ToString() + "." + drFilter["FK_COLUMN"].ToString() + " = " + drFilter["PK_Table"].ToString() + "." + drFilter["PK_COLUMN"].ToString();
                                }
                                else
                                {
                                    strquery += "\n\t\t" + GetJoin(drFilter["PK_Table"].ToString()) + drFilter["PK_Table"].ToString() + " AS " + drFilter["PK_Table"].ToString() + "_" + counter.ToString() + " ON " + drFilter["K_Table"].ToString() + "." + drFilter["FK_COLUMN"].ToString() + " = " + drFilter["PK_Table"].ToString() + "_" + counter.ToString() + "." + drFilter["PK_COLUMN"].ToString();
                                }
                                counter++;
                            }
                        }
                    
                    }
                }

            }


            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                string dropsptext = @"exec(
                'IF  EXISTS (SELECT * FROM sys.objects WHERE  type in (N''P'', N''PC'') and  name = ''"+SpName+"''"+@" )
				DROP PROCEDURE " + SpName + "')";

                string sptext = @"
                exec(
                '
CREATE PROCEDURE {0}
    {1}
AS
BEGIN
    {2}
END
') ";
                sptext = sptext.Replace("{0}", SpName).Replace("{1}", strparam).Replace("{2}", strquery + strfilter);

                command.CommandText = dropsptext;
                connection.Open();

               command.ExecuteNonQuery();


               command.CommandText = sptext;

               command.ExecuteNonQuery();

               connection.Close();
            }
           
        }

        public DataTable getRelation()
        {
            DataTable dt = new DataTable();
            string conString = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ; ;

            List<string> listacolumnas = new List<string>();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"select *,
                (
                SELECT DATA_TYPE 
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE 
                     TABLE_NAME = r.K_Table AND 
                     COLUMN_NAME = r.FK_Column  ) Type,
                (
                SELECT CHARACTER_MAXIMUM_LENGTH 
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE 
                     TABLE_NAME = r.K_Table AND 
                     COLUMN_NAME = r.FK_Column  ) Length,
                (
                SELECT case when IS_NULLABLE='YES' then 1 else 0 end 
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE 
                     TABLE_NAME = r.K_Table AND 
                     COLUMN_NAME = r.FK_Column  ) IS_NULLABLE
                 from (
                SELECT DISTINCT FK.TABLE_NAME AS K_Table, CU.COLUMN_NAME AS FK_Column, PK.TABLE_NAME AS PK_Table, PT.COLUMN_NAME AS PK_Column

                FROM            INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS C INNER JOIN
                                         INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN
                                         INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN
                                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN
                                             (SELECT        i1.TABLE_NAME, i2.COLUMN_NAME
                                                FROM            INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS i1 INNER JOIN
                                                                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
                                                WHERE        (i1.CONSTRAINT_TYPE = 'PRIMARY KEY')) AS PT ON PT.TABLE_NAME = PK.TABLE_NAME 
								                --inner join sys.objects on  FK.TABLE_NAME=sys.objects.name
								                --inner  JOIN SYS.COLUMNS 
								                --ON sys.objects.OBJECT_ID=SYS.COLUMNS.OBJECT_ID
								                --JOIN SYS.TYPES  ON SYS.COLUMNS.SYSTEM_TYPE_ID=SYS.TYPES.system_type_id



                )r
                ";
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
                command.CommandText = 	"SELECT COLUMN_NAME,DATA_TYPE,"+
	            "("+
	            " SELECT  COL_NAME(ic.OBJECT_ID,ic.column_id) "+
	            " FROM    sys.indexes AS i INNER JOIN "+
	            " sys.index_columns AS ic ON  i.OBJECT_ID = ic.OBJECT_ID "+
	            " AND i.index_id = ic.index_id "+
	            " WHERE  "+
	            " OBJECT_NAME(ic.OBJECT_ID)='Table' and COL_NAME(ic.OBJECT_ID,ic.column_id)=COLUMN_NAME "+
	            " ) as PK"+
	            " ,"+
	            " ("+
	            " SELECT c1.name from sys.objects o1 "+
	            " INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id "+
	            " INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id "+
	            " INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id "+
	            " INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id "+
	            " INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id "+
	            " INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id "+
                " where o1.name='" + Table + "' and c1.name=COLUMN_NAME" +
	            " ) as FK_column"+
	            ","+
	            "("+
	            " SELECT o2.name from sys.objects o1"+
                " INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id " +
                " INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id " +
                " INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id " +
                " INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id " +
                " INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id " +
                " INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id " +
                " where o1.name='" + Table + "' and c1.name=COLUMN_NAME" +
	            " ) as PK_table"+
	            ","+
	            "("+
	            " SELECT c2.name from sys.objects o1"+
                " INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id " +
                " INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id " +
                " INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id " +
                " INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id " +
                " INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id " +
                " INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id " +
                " where o1.name='" + Table + "' and c1.name=COLUMN_NAME " +
	            " ) as PK_column "+
	            " FROM INFORMATION_SCHEMA.COLUMNS "+
	            " WHERE TABLE_NAME =  '"+Table+"'";
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
        public static void DeleteLastLine(string filepath,int del=2)
        {
            List<string> lines = File.ReadAllLines(filepath).ToList();

            File.WriteAllLines(filepath, lines.GetRange(0, lines.Count - del).ToArray());

        }

        public bool CheckExistance(string filepath,string functionname)
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


        private void CreateDALsFromSP(string SpName)
        {


            try
            {
                DataTable dtColumnsFromSp = GetColumnsFromSP(chkSpList.Text);

                #region BusinessDataObjects

                String TableName = SpName;
                StringBuilder sb = new StringBuilder();
                string str = txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials";
               
                if (chkGenList2.GetItemChecked(0))
                {
                    if (!Directory.Exists(txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity"))
                        Directory.CreateDirectory(txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity");

                    string path = txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity\\" + SpName + "Entity.cs";
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    System.IO.StreamWriter file2 = System.IO.File.AppendText(txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity\\" + SpName + "Entity.cs");
                    sb = new StringBuilder();
                    sb.Append(Gen_DataObjectsPartial_Entity_FromSP(SpName, dtColumnsFromSp));
                    file2.WriteLine(sb.ToString());
                    file2.Close();

                }
                #endregion BusinessDataObjects

                string strfilepath = "", strpath = "";
                if (chkGenList2.GetItemChecked(1))
                {
                    #region BusinessFacadeObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.BusinessFacadeObjects\\BusinessFacadeObjectsPartial\\ReportExtensionFacade.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.BusinessFacadeObjects\\BusinessFacadeObjectsPartial";

                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();
                            sb.Append(Gen_ReportExtensionFacade(TableName));

                            file5.WriteLine(sb.ToString());
                            file5.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(Gen_ReportExtensionFacade(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\ReportExtentionFacade.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();

                    }

                    #endregion BusinessFacadeObjectsPartial

                    #region IBusinessFacadeObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.IBusinessFacadeObjects\\IBusinessFacadeObjectsPartial\\IReportExtensionFacade.cs";
                    strpath = txtDirectory.Text.Trim() +"\\KAF.IBusinessFacadeObjects\\IBusinessFacadeObjectsPartial";

                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file5 = System.IO.File.AppendText(txtDirectory.Text.Trim() + "\\KAF.IBusinessFacadeObjects\\IBusinessFacadeObjectsPartial\\IReportExtensionFacade.cs");
                            sb = new StringBuilder();
                            sb.Append(Gen_IReportExtensionFacade(TableName));

                            file5.WriteLine(sb.ToString());
                            file5.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(Gen_IReportExtensionFacade(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\IReportExtentionFacade.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();

                    }


                    #endregion IBusinessFacadeObjectsPartial

                    #region DataAccessObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.DataAccessObjects\\DataAccessObjectsPartial\\ReportExtensionDataAccess.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.DataAccessObjects\\DataAccessObjectsPartial";

                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file3 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();

                            sb.Append(Gen_DataAccessObjectsPartial_FromSp(TableName,dtColumnsFromSp));

                            file3.WriteLine(sb.ToString());
                            file3.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(Gen_DataAccessObjectsPartial_FromSp(TableName, dtColumnsFromSp,1));


                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\ReportExtensionDataAccess.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();

                    }


                    #endregion DataAccessObjectsPartial

                    #region IDataAccessObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.IDataAccessObjects\\IDataAccessObjectsPartial\\IReportExtensionDataAccess.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.IDataAccessObjects\\IDataAccessObjectsPartial";

                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file3 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();

                            sb.Append(IReportExtensionDataAccess(TableName));

                            file3.WriteLine(sb.ToString());
                            file3.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(IReportExtensionDataAccess(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\IReportExtensionDataAccess.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();

                    }

                    strfilepath = drpProject.Text.Trim() + "\\HelperClasses\\clsReportingEntity.cs";
                    strpath = drpProject.Text.Trim() + "\\HelperClasses";

                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {

                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file3 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();

                            sb.Append(HelperClassclsReportingEntity(TableName));

                            file3.WriteLine(sb.ToString());
                            file3.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(HelperClassclsReportingEntity(TableName,1));


                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\clsReportingEntity.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();

                    }



                    #endregion IDataAccessObjectsPartial
                }

            }
            catch (Exception es)
            {

            }



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
      
        private void CreateDALs()
        {


            try
            {
                #region BusinessDataObjects

                String TableName = txtSpName.Text;
                StringBuilder sb = new StringBuilder();
                string str = txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials";
                //if (!System.IO.Directory.Exists(str))
                //{
                //    MessageBox.Show("Invalid Directory");
                //    return;
                //}

                if (chkGenList.GetItemChecked(1))
                {
                    if (!Directory.Exists(txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity"))
                        Directory.CreateDirectory(txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity");

                    string path = txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity\\" + TableName + "Entity.cs";
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);

                    }

                    System.IO.StreamWriter file2 = System.IO.File.AppendText(txtDirectory.Text.Trim() + "\\KAF.BusinessDataObjects\\BusinessDataObjectsPartials\\ReportEntity\\" + TableName + "Entity.cs");
                    sb = new StringBuilder();
                    sb.Append(Gen_DataObjectsPartial_Entity(TableName));
                    file2.WriteLine(sb.ToString());
                    file2.Close();

                }
                #endregion BusinessDataObjects

                string strfilepath = "", strpath = "";

                if (chkGenList.GetItemChecked(2))
                {
                    #region BusinessFacadeObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.BusinessFacadeObjects\\BusinessFacadeObjectsPartial\\ReportExtensionFacade.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.BusinessFacadeObjects\\BusinessFacadeObjectsPartial";
                   
                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();
                            sb.Append(Gen_ReportExtensionFacade(TableName));

                            file5.WriteLine(sb.ToString());
                            file5.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();

                        sb.Append(Gen_ReportExtensionFacade(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\ReportExtentionFacade.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();

                    }

                    #endregion BusinessFacadeObjectsPartial

                    #region IBusinessFacadeObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.IBusinessFacadeObjects\\IBusinessFacadeObjectsPartial\\IReportExtensionFacade.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.IBusinessFacadeObjects\\IBusinessFacadeObjectsPartial";

                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);
                            sb = new StringBuilder();
                            sb.Append(Gen_IReportExtensionFacade(TableName));

                            file5.WriteLine(sb.ToString());
                            file5.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(Gen_IReportExtensionFacade(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\IReportExtentionFacade.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();
                    }


                    #endregion IBusinessFacadeObjectsPartial

                    #region DataAccessObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.DataAccessObjects\\DataAccessObjectsPartial\\ReportExtensionDataAccess.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.DataAccessObjects\\DataAccessObjectsPartial";


                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file3 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();

                            sb.Append(Gen_DataAccessObjectsPartial(TableName));

                            file3.WriteLine(sb.ToString());
                            file3.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(Gen_DataAccessObjectsPartial(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\ReportExtensionDataAccess.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);

                        file5.Close();
                    }

                    #endregion DataAccessObjectsPartial

                    #region IDataAccessObjectsPartial

                    strfilepath = txtDirectory.Text.Trim() + "\\KAF.IDataAccessObjects\\IDataAccessObjectsPartial\\IReportExtensionDataAccess.cs";
                    strpath = txtDirectory.Text.Trim() + "\\KAF.IDataAccessObjects\\IDataAccessObjectsPartial";


                    if (System.IO.File.Exists(strfilepath))
                    {
                        if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                        {
                            DeleteLastLine(strfilepath);

                            System.IO.StreamWriter file3 = System.IO.File.AppendText(strfilepath);

                            sb = new StringBuilder();

                            sb.Append(IReportExtensionDataAccess(TableName));

                            file3.WriteLine(sb.ToString());
                            file3.Close();
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(IReportExtensionDataAccess(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\IReportExtensionDataAccess.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();
                    }

                    strfilepath = drpProject.Text.Trim() + "\\HelperClasses\\clsReportingEntity.cs";
                    strpath =  drpProject.Text.Trim() + "\\HelperClasses";

                    if (System.IO.File.Exists(strfilepath))
                    {
                            if (!CheckExistance(strfilepath, "Get_" + TableName + "("))
                            {

                                DeleteLastLine(strfilepath);

                                System.IO.StreamWriter file3 = System.IO.File.AppendText(strfilepath);

                                sb = new StringBuilder();

                                sb.Append(HelperClassclsReportingEntity(TableName));

                                file3.WriteLine(sb.ToString());
                                file3.Close();
                            }
                    }
                    else
                    {
                        if (!Directory.Exists(strpath))
                            Directory.CreateDirectory(strpath);

                        System.IO.StreamWriter file5 = System.IO.File.AppendText(strfilepath);

                        sb = new StringBuilder();
                        sb.Append(HelperClassclsReportingEntity(TableName,1));

                        string filetext = GetFileText(Application.StartupPath + "\\Templates\\clsReportingEntity.txt");

                        filetext=filetext.Replace("{0}", sb.ToString());

                        file5.WriteLine(filetext);
                        file5.Close();
                    }

                    #endregion IDataAccessObjectsPartial
                }

            }
            catch (Exception es)
            {

            }
                   
           

        }
        public string Gen_ReportExtensionFacade(String TableName,int newfile=0)
        {
            try
            {


                string format = "\n\t\tIList<{0}Entity> IReportExtensionFacade.GET_{0}({0}Entity {0}Entity)" +
                "\n\t\t{" +
                    "\n\t\t\ttry" +
                    "\n\t\t\t{" +
                        "\n\t\t\t\treturn DataAccessFactory.CreateReportExtensionDataAccess().Get_{0}({0}Entity).ToList();" +
                    "\n\t\t\t}" +
                    "\n\t\t\tcatch (DataException ex)" +
                    "\n\t\t\t{" +

                            "\n\t\t\t\tthrow GetFacadeException(ex, SourceOfException(\"IList<{0}Entity> IReportExtensionFacade.rpt_{0}\"));" +

                    "\n\t\t\t}" +
                    "\n\t\t\tcatch (Exception exx)" +
                    "\n\t\t\t{" +
                      "  \n\t\t\t\tthrow exx;" +
                    "\n\t\t\t}" +
                "\n\t\t}";

                if(newfile==0)
                    format += "\n\t}" +
                             "\n}";
          

                return format.Replace("{0}",TableName);

                    }
            // 
            catch (Exception es)
            {
                return "";
            }


           
        }



        public string Gen_DataBaseFactoryEx(String TableName, int newfile = 0)
        {
            string format = "\n\t\t#region IReportExtensionDataAccess" +
                  "\n\t\t\tpublic abstract IReportExtensionDataAccess CreateReportExtensionDataAccess();" +
                  "\n\t\t#endregion IReportExtensionDataAccess";

            if (newfile == 0)
                format += "\n\t}" +
                         "\n}";

            return format.Replace("{0}",TableName);
        }

        public string Gen_IReportExtensionFacade(String TableName, int newfile = 0)
        {
            string format = "\n\t\t[OperationContract]" +
                  "\n\t\tIList<{0}Entity> GET_{0}({0}Entity {0}Entity);";

            if (newfile == 0)
                format += "\n\t}" +
                         "\n}";


            return format.Replace("{0}", TableName);
        }

        public string IReportExtensionDataAccess(String TableName, int newfile = 0)
        {

            string format = "\n\t\tIList<{0}Entity> Get_{0}({0}Entity {0}Entity);";

            if (newfile == 0)
                format += "\n\t}" +
                         "\n}";

            return format.Replace("{0}", TableName);
        }
        public string HelperClassclsReportingEntity(String TableName, int newfile = 0)
        {

            string format = "\n\t\tpublic List<{0}Entity> Get_{0}()" +
                "\n\t\t{" +
                "\n\t\t\tList<{0}Entity> db = new List<{0}Entity>();" +
                "\n\t\t\treturn db.ToList();" +
                "\n\t\t}";

            if (newfile == 0)
                format += "\n\t}" +
                         "\n}";

            return format.Replace("{0}", TableName);
        }
        private string Gen_DataAccessObjectsPartial(String TableName, int newfile = 0)
        {

            string spname = txtSpName.Text;// chkSpList.CheckedItems[0].ToString();

            //Database.AddInParameter(cmd, ""@CivilID"", DbType.Int64, rpt_ApplicantBasicProfileEntity.civilid);
           
            string parameterstr = "";

            //  DataTable dtparam = GetParamListBySpName(spname);

            for (int i = 0; i < gvColumns.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvColumns.Rows[i].Cells["chkOutput"].Value))
                {
                    string strformat = "\n\t\t\t\tDatabase.AddInParameter(cmd, \"{0}\", DbType.{1}, {2}Entity.{3});";

                    parameterstr += strformat.Replace("{0}", gvColumns.Rows[i].Cells["Name"].Value.ToString()).Replace("{1}",GetDotNetDataType( gvColumns.Rows[i].Cells["Type"].Value.ToString())).Replace("{2}", TableName).Replace("{3}", gvColumns.Rows[i].Cells["Name"].Value.ToString());
                }
            }

            string format = "\n\t\tIList<{0}Entity> IReportExtensionDataAccess.Get_{0}({0}Entity {0}Entity)" +
            "\n\t\t{" +
               "\n\t\t\tIList<{0}Entity> itemList = new List<{0}Entity>();" +
                "\n\t\t\ttry" +
                "\n\t\t\t{" +
                    "\n\t\t\t\tconst string SP = \"{1}\";" +
                    "\n\t\t\t\tusing (DbCommand cmd = Database.GetStoredProcCommand(SP))" +
                    "\n\t\t\t\t{" +

                       "{2}" +

                       "\n\t\t\t\t\tusing (IDataReader reader = Database.ExecuteReader(cmd))" +
                       "\n\t\t\t\t\t{" +
                          "\n\t\t\t\t\twhile (reader.Read())" +
                            "\n\t\t\t\t\t{" +
                                "\n\t\t\t\t\t\titemList.Add(new {0}Entity(reader));" +
                           "\n\t\t\t\t\t}" +
                            "reader.Dispose();" +
                        "\n\t\t\t\t\t}" +
                       "\n\t\t\t\tcmd.Dispose();" +
                    "\n\t\t\t}" +
                "\n\t\t\t}" +
                "\n\t\t\tcatch (Exception ex)" +
                "\n\t\t\t{" +
                    "\n\t\t\t\tthrow GetDataAccessException(ex, SourceOfException(\"IReportExtensionDataAccess.Get_{0}\"));" +
                "\n\t\t\t}" +
           " \n\t\treturn itemList;" +
           " \n\t\t}";

            if (newfile == 0)
                format += "\n\t }" +
                        "\n }";

            return format.Replace("{0}", TableName).Replace("{1}", spname).Replace("{2}", parameterstr);
        }

        private string Gen_DataAccessObjectsPartial_FromSp(String SpName,DataTable dtColumnsFromSp,int newfile=0)
        {

            string spname = SpName;

            string parameterstr = "";

             DataTable dtparam = GetParamListBySpName(spname);

             for (int i = 0; i < dtparam.Rows.Count; i++)
            {
               
                    string strformat = "\n\t\t\t\tDatabase.AddInParameter(cmd, \"{0}\", DbType.{1}, {2}Entity.{3});";

                    parameterstr += strformat.Replace("{0}", dtparam.Rows[i]["PARAMETER_NAME"].ToString()).Replace("{1}", GetDotNetDataType(dtparam.Rows[i]["DATA_TYPE"].ToString().ToString())).Replace("{2}", spname).Replace("{3}", dtparam.Rows[i]["PARAMETER_NAME"].ToString().Replace("@",""));
               
            }

             string format = "\n\t\tIList<{0}Entity> IReportExtensionDataAccess.Get_{0}({0}Entity {0}Entity)" +
             "\n\t\t{" +
                "\n\t\t\tIList<{0}Entity> itemList = new List<{0}Entity>();" +
                 "\n\t\t\ttry" +
                 "\n\t\t\t{" +
                     "\n\t\t\t\tconst string SP = \"{1}\";" +
                     "\n\t\t\t\tusing (DbCommand cmd = Database.GetStoredProcCommand(SP))" +
                     "\n\t\t\t\t{" +

                        "{2}" +

                        "\n\t\t\t\t\tusing (IDataReader reader = Database.ExecuteReader(cmd))" +
                        "\n\t\t\t\t\t{" +
                           "\n\t\t\t\t\twhile (reader.Read())" +
                             "\n\t\t\t\t\t{" +
                                 "\n\t\t\t\t\t\titemList.Add(new {0}Entity(reader));" +
                            "\n\t\t\t\t\t}" +
                             "reader.Dispose();" +
                         "\n\t\t\t\t\t}" +
                        "\n\t\t\t\tcmd.Dispose();" +
                     "\n\t\t\t}" +
                 "\n\t\t\t}" +
                 "\n\t\t\tcatch (Exception ex)" +
                 "\n\t\t\t{" +
                     "\n\t\t\t\tthrow GetDataAccessException(ex, SourceOfException(\"IReportExtensionDataAccess.Get_{0}\"));" +
                 "\n\t\t\t}" +
            " \n\t\treturn itemList;" +
            " \n\t\t}";

            if(newfile==0)
                format += "\n\t }" +
                        "\n }";
     

            return format.Replace("{0}", spname).Replace("{1}", spname).Replace("{2}", parameterstr);
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
        private string Gen_DataObjectsPartial_Entity(String TableName,int newfile=0)
        {
            StringBuilder sb = new StringBuilder();
            string strnamespace=@"using System;
            using System.Runtime.Serialization;
            using System.Data;
            using KAF.BusinessDataObjects.BusinessDataObjectsBase;";
            sb.AppendLine(strnamespace);
           
            sb.AppendLine("");
            sb.AppendLine("namespace KAF.BusinessDataObjects.BusinessDataObjectsPartials");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic class " + TableName + "Entity:  BaseEntity");
            sb.AppendLine("\t{");

            for (int i = 0; i < gvColumns.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvColumns.Rows[i].Cells["chkOutput"].Value) == false) continue;

                String Column = gvColumns.Rows[i].Cells[1].Value.ToString();
                String ColType = gvColumns.Rows[i].Cells[2].Value.ToString();

                
                string isnull = "";
                if (Convert.ToBoolean(gvColumns.Rows[i].Cells["is_nullable"].Value))
                    isnull = "?";

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                    sb.Append("\t\tprotected String _" + Column + ";\n");
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                    sb.Append("\t\tprotected Int32" + isnull + " _" + Column + ";\n");
                else if (ColType.ToUpper() == "BIGINT")
                    sb.Append("\t\tprotected Int64" + isnull + " _" + Column + ";\n");
                else if (ColType.ToUpper() == "MONEY")
                    sb.Append("\t\tprotected Decimal" + isnull + " _" + Column + ";\n");
                else if (ColType.ToUpper() == "BIT")
                    sb.Append("\t\tprotected Boolean" + isnull + " _" + Column + ";\n");
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                    sb.Append("\t\tprotected DateTime" + isnull + " _" + Column + ";\n");
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                    sb.Append("\t\tprotected Decimal" + isnull + " _" + Column + ";\n");

            }
            sb.AppendLine("");

            for (int i = 0; i < gvColumns.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvColumns.Rows[i].Cells["chkOutput"].Value) == false) continue;

                String Column = gvColumns.Rows[i].Cells["Name"].Value.ToString();
                String ColType = gvColumns.Rows[i].Cells["Type"].Value.ToString();
                string isnull = "";

                if (Convert.ToBoolean(gvColumns.Rows[i].Cells["is_nullable"].Value))
                    isnull = "?";

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic String " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Int32" + isnull + " " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "BIGINT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Int64" + isnull + " " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "MONEY")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Decimal" + isnull + " " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "BIT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Boolean" + isnull + " " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic DateTime" + isnull + " " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Decimal" + isnull + " " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }

            }

            sb.AppendLine("\t\tpublic " + TableName + "Entity():base()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");

            sb.AppendLine("\t\tpublic " + TableName + "Entity(IDataReader reader)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tthis.LoadFromReader(reader);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");

            sb.AppendLine("\t\tprotected void LoadFromReader(IDataReader reader)");
            sb.AppendLine("\t\t{");


            for (int i = 0; i < gvColumns.Rows.Count; i++)
            {

                //if (!reader.IsDBNull(reader.GetOrdinal("BasicInfoID"))) _basicinfoid = reader.GetInt64(reader.GetOrdinal("BasicInfoID"));
                if (Convert.ToBoolean(gvColumns.Rows[i].Cells["chkOutput"].Value) == false) continue;


                String Column = gvColumns.Rows[i].Cells["Name"].Value.ToString();
                String ColType = gvColumns.Rows[i].Cells["Type"].Value.ToString();

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetString(reader.GetOrdinal(\"" + Column + "\"));\n");
                }
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetInt32(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "BIGINT")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetInt64(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "MONEY")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetDecimal(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "BIT")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetBoolean(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetDateTime(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetDecimal(reader.GetOrdinal(\"" + Column + "\"));\n");

                }

            }


            sb.AppendLine("\t\t}");
            if (newfile == 0)
            {
                sb.AppendLine("\t}");
                sb.AppendLine("}"); //class ends
            }
            return sb.ToString();
        }

        private string Gen_DataObjectsPartial_Entity_FromSP(String Spname,DataTable dtspColumns,int newfile=0)
        {
            StringBuilder sb = new StringBuilder();
            string strnamespace = @"using System;
            using System.Runtime.Serialization;
            using System.Data;
            using KAF.BusinessDataObjects.BusinessDataObjectsBase;";
            sb.AppendLine(strnamespace);

            sb.AppendLine("");
            sb.AppendLine("namespace KAF.BusinessDataObjects.BusinessDataObjectsPartials");
            sb.AppendLine("{");
            sb.AppendLine("\n[Serializable]");
            sb.AppendLine("\tpublic class " + Spname + "Entity:  BaseEntity");
            sb.AppendLine("\t{");

            for (int i = 0; i < dtspColumns.Rows.Count; i++)
            {

                String Column = dtspColumns.Rows[i]["name"].ToString();
                String ColType = dtspColumns.Rows[i]["Type"].ToString();

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                    sb.Append("\t\tprotected String _" + Column + ";\n");
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                    sb.Append("\t\tprotected Int32? _" + Column + ";\n");
                else if (ColType.ToUpper() == "BIGINT")
                    sb.Append("\t\tprotected Int64? _" + Column + ";\n");
                else if (ColType.ToUpper() == "MONEY")
                    sb.Append("\t\tprotected Decimal? _" + Column + ";\n");
                else if (ColType.ToUpper() == "BIT")
                    sb.Append("\t\tprotected Boolean? _" + Column + ";\n");
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                    sb.Append("\t\tprotected DateTime? _" + Column + ";\n");
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                    sb.Append("\t\tprotected Decimal? _" + Column + ";\n");

            }

            DataTable dtColumnsFromSP = GetParamListBySpName(Spname);

            for (int i = 0; i < dtColumnsFromSP.Rows.Count; i++)
            {
              

                String Column = dtColumnsFromSP.Rows[i]["PARAMETER_NAME"].ToString().Replace("@","");
                String ColType = dtColumnsFromSP.Rows[i]["DATA_TYPE"].ToString();

                DataView dv = dtspColumns.DefaultView;
                dv.RowFilter = "name='" + Column + "'";

                if (dv.ToTable().Rows.Count > 0) continue;

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                    sb.Append("\t\tprotected String _" + Column + ";\n");
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                    sb.Append("\t\tprotected Int32? _" + Column + ";\n");
                else if (ColType.ToUpper() == "BIGINT")
                    sb.Append("\t\tprotected Int64? _" + Column + ";\n");
                else if (ColType.ToUpper() == "MONEY")
                    sb.Append("\t\tprotected Decimal? _" + Column + ";\n");
                else if (ColType.ToUpper() == "BIT")
                    sb.Append("\t\tprotected Boolean? _" + Column + ";\n");
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                    sb.Append("\t\tprotected DateTime? _" + Column + ";\n");
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                    sb.Append("\t\tprotected Decimal? _" + Column + ";\n");

            }


            sb.AppendLine("");

            for (int i = 0; i < dtspColumns.Rows.Count; i++)
            {

                String Column = dtspColumns.Rows[i]["name"].ToString();
                String ColType = dtspColumns.Rows[i]["Type"].ToString();

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic String " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Int32? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "BIGINT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Int64? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "MONEY")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Decimal? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "BIT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Boolean? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic DateTime? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Decimal? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }

            }

            for (int i = 0; i < dtColumnsFromSP.Rows.Count; i++)
            {

                String Column = dtColumnsFromSP.Rows[i]["PARAMETER_NAME"].ToString().Replace("@","");;
                String ColType = dtColumnsFromSP.Rows[i]["DATA_TYPE"].ToString();


                DataView dv = dtspColumns.DefaultView;
                dv.RowFilter = "name='" + Column + "'";

                if (dv.ToTable().Rows.Count > 0) continue;

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic String " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Int32? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "BIGINT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Int64? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "MONEY")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Decimal? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "BIT")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Boolean? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic DateTime? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                {
                    sb.Append("\t\t[DataMember]\n\t\tpublic Decimal? " + Column + "\n\t\t{\n\t\t\tget { return _" + Column + "; }\n\t\t\tset {_" + Column + "=value; }\n\t\t}\n");

                }

            }

            sb.AppendLine("\t\tpublic " + Spname + "Entity():base()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");

            sb.AppendLine("\t\tpublic " + Spname + "Entity(IDataReader reader)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tthis.LoadFromReader(reader);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");

            sb.AppendLine("\t\tprotected void LoadFromReader(IDataReader reader)");
            sb.AppendLine("\t\t{");


            for (int i = 0; i < dtspColumns.Rows.Count; i++)
            {

                String Column = dtspColumns.Rows[i]["name"].ToString();
                String ColType = dtspColumns.Rows[i]["Type"].ToString();

                if (ColType.ToUpper() == "VARCHAR" || ColType.ToUpper() == "NVARCHAR" || ColType.ToUpper() == "CHAR")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetString(reader.GetOrdinal(\"" + Column + "\"));\n");
                }
                else if (ColType.ToUpper() == "INT" || ColType.ToUpper() == "TINYINT")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetInt32(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "BIGINT")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetInt64(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "MONEY")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetDecimal(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "BIT")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetBoolean(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "DATE" || ColType.ToUpper() == "DATETIME" || ColType.ToUpper() == "SMALLDATETIME")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetDateTime(reader.GetOrdinal(\"" + Column + "\"));\n");

                }
                else if (ColType.ToUpper() == "DECIMAL" || ColType.ToUpper() == "FLOAT" || ColType.ToUpper() == "NUMERIC")
                {
                    sb.Append("\t\t\tif (!reader.IsDBNull(reader.GetOrdinal(\"" + Column + "\"))) _" + Column + " = reader.GetDecimal(reader.GetOrdinal(\"" + Column + "\"));\n");

                }

            }


            sb.AppendLine("\t\t}");

            if (newfile == 0)
            {
                sb.AppendLine("\t}");
                sb.AppendLine("}"); //class ends
            }

            return sb.ToString();
        }
     

        private void cmbDbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbDbs.SelectedValue.ToString()=="SELECT")
            //{
            //  //  MessageBox.Show("Please Select Db");
            //}
            //else
            //{
            //    chklistTables.Items.Clear();
            //    GetTableList();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            CreateDALs();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          //  CreateForms();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnLoadServer_Click(sender, e);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox2.Checked == false)
            //{
            //    for (int i = 0; i < chklistTables.Items.Count; i++)
            //        chklistTables.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
            //}
            //else
            //{
            //     for (int i = 0; i < chklistTables.Items.Count; i++)
            //        chklistTables.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Checked));
            //}
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
         

            this.Height = 350;

            if (txtDirectory.Text.Trim() != "")
            {
                if (Directory.Exists(txtDirectory.Text.Trim()))
                {
                    string[] directoryList = System.IO.Directory.GetDirectories(txtDirectory.Text.Trim());
                    drpProject.DataSource = directoryList;
                }
                else
                {
                    txtDirectory.Text = "";
                }
            }

            for (int i = 0; i < chkGenList.Items.Count; i++)
                chkGenList.SetItemChecked(i, true);

            for (int i = 0; i < chkGenList2.Items.Count; i++)
                chkGenList2.SetItemChecked(i, true);

            groupBox1.Location = new Point(9, 156);
            groupBox2.Location = new Point(9, 156);

          
            groupBox1.Visible = false;
            groupBox2.Visible = true;
          
        }

        private void btnLoadDb_Click(object sender, EventArgs e)
        {
            cmbDb.DataSource = GetDatabaseList();
        }

        public List<string> GetDatabaseList()
        {
            var connection = txtDbCon.Text + "Initial Catalog=" + cmbDb.Text; ; ;

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
     

       
       

        public void MergeTables( DataTable dtMerge)
        {
            if (dtColumn.Rows.Count==0)
            {
                dtColumn=dtMerge.Clone();
                dtColumn.Columns["tbl"].MaxLength = 50;
                dtColumn.Columns["Name"].MaxLength = 50;
                dtColumn.Columns["Type"].MaxLength = 50;
            }

            foreach (DataRow dr in dtMerge.Rows)
            {
                if ( dtColumn.Select("Name ='" + dr["Name"].ToString() + "'").Count() == 0)
                {
                    dtColumn.ImportRow(dr);
                }
                //else if (dtColumn.Select("tbl='" + dr["tbl"].ToString() + "'").Count() == 0 && dtColumn.Select("Name ='" + dr["Name"].ToString() + "'").Count() > 0)
                //{
                //    dtColumn.ImportRow(dr);
                //}

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

        private void btnGenSp_Click(object sender, EventArgs e)
        {
            if (txtSpName.Text.Trim() == "")
                MessageBox.Show("Please Provide Sp Name");
            else
            {
                GenerateSP(txtSpName.Text.Trim());
            }
        }

        private void RemoveColumns()
        {
            var rows = dtColumn.Select("tbl='" + gvRelation.Rows[gvRelation.CurrentCell.RowIndex].Cells["tblRelation"].Value.ToString() + "'");
            foreach (var row in rows)
                row.Delete();

            dtColumn.Constraints.Clear();
            gvColumns.AutoGenerateColumns = false;
            gvColumns.DataSource = dtColumn;

          
        }


        private void gvRelation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }
       
        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                if ((txtDirectory.Text.Trim() == ""))
                    MessageBox.Show("Please Select Directory");
                else if (txtSpName.Text.Trim() == "")
                    MessageBox.Show("Please Provide Sp Name");

                else
                {

                    GenerateSP(txtSpName.Text.Trim());

                    MessageBox.Show("Generated Completed.");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void gvRelation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.ShiftKey && (e.KeyChar == (char)Keys.Down || e.KeyChar == (char)Keys.Up))
            {
               // MessageBox.Show(gvRelation.SelectedCells.ToString());
            }
        }
        private void LoadColumns(int rowindex)
        {
            if (gvRelation.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
            {
                if (gvRelation.CurrentCell.IsInEditMode)
                {
                    DataTable GetColumns = getColumnsName(gvRelation.Rows[rowindex].Cells["tblRelation"].Value.ToString());
                    dtColumn.Constraints.Clear();
                    GetColumns.Constraints.Clear();
                    MergeTables(GetColumns);

                    gvColumns.AutoGenerateColumns = false;
                    gvColumns.DataSource = dtColumn;

                  
                }
            }

        }
        private void gvRelation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.ShiftKey))
            {
                checkshiftkey = true;
            }
            else if (((e.KeyCode.Equals(Keys.Up)) || (e.KeyCode.Equals(Keys.Down))) && checkshiftkey)
            {
                if (gvRelation.CurrentCell.ColumnIndex == 0)
                {
                    

                    if ((e.KeyCode.Equals(Keys.Down)))
                    {
                        gvRelation.Rows[gvRelation.CurrentCell.RowIndex].Cells[0].Value = true;
                        LoadColumns(gvRelation.CurrentCell.RowIndex);

                        if ((gvRelation.CurrentCell.RowIndex + 1) < gvRelation.Rows.Count)
                        {
                            gvRelation.Rows[gvRelation.CurrentCell.RowIndex + 1].Cells[0].Value = true;
                            LoadColumns(gvRelation.CurrentCell.RowIndex+1);
                           
                        }

                    }
                    else  if ((e.KeyCode.Equals(Keys.Up)))
                   
                    {
                        gvRelation.Rows[gvRelation.CurrentCell.RowIndex].Cells[0].Value = false;

                        RemoveColumns();

                        if ((gvRelation.CurrentCell.RowIndex) == 0)
                        {
                            gvRelation.Rows[gvRelation.CurrentCell.RowIndex].Cells[0].Value = false;
                          
                            cmbTable_SelectedIndexChanged(sender, e);
                            gvRelation.ClearSelection();
                        }
                       
                    }
                }

            }
            else
            {
                checkshiftkey = false;
                gvRelation.Rows[gvRelation.CurrentCell.RowIndex].Cells[0].Value = false;
            }
           
           // e.Handled = true;
        }

        private void gvRelation_KeyUp(object sender, KeyEventArgs e)
        {
            if ((gvRelation.CurrentCell.RowIndex) == 0)
                gvRelation.Rows[gvRelation.CurrentCell.RowIndex].Cells[0].Value = false;
        }

        private void gvColumns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.ShiftKey))
            {
                checkshiftkey2 = true;
            }
            else if (((e.KeyCode.Equals(Keys.Up)) || (e.KeyCode.Equals(Keys.Down))) && checkshiftkey2)
            {
                if (gvColumns.CurrentCell.ColumnIndex == 5)
                {
                    if ((e.KeyCode.Equals(Keys.Down)))
                    {
                        gvColumns.Rows[gvColumns.CurrentCell.RowIndex].Cells[5].Value = true;

                        if ((gvColumns.CurrentCell.RowIndex + 1) < gvColumns.Rows.Count)
                        {
                            gvColumns.Rows[gvColumns.CurrentCell.RowIndex + 1].Cells[5].Value = true;
                        }

                    }
                    else if ((e.KeyCode.Equals(Keys.Up)))
                    {
                        gvColumns.Rows[gvColumns.CurrentCell.RowIndex].Cells[5].Value = false;

                        if ((gvColumns.CurrentCell.RowIndex) == 0)
                        {
                            gvColumns.Rows[gvColumns.CurrentCell.RowIndex].Cells[5].Value = false;

                        }

                    }
                }

            }
            else
            {
                checkshiftkey2 = false;
                gvColumns.Rows[gvRelation.CurrentCell.RowIndex].Cells[5].Value = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "From Stored Procedure")
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
                GetSpList();
                this.Height = 350;
              
            }
            else
            {
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                this.Height = 645;
            }
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            try
            {
                if (chkSpList.SelectedIndex > 0)
                {
                    CreateDALsFromSP(chkSpList.Text);
                    MessageBox.Show("Business Classes are generated");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((txtDirectory.Text.Trim() == ""))
                    MessageBox.Show("Please Select Directory");

                else
                {

                    CreateDALs();

                    if (chkGenList.GetItemChecked(0))
                    {
                        if (txtSpName.Text.Trim() == "")
                            MessageBox.Show("Please Provide Sp Name");
                        else
                            GenerateSP(txtSpName.Text.Trim());

                        MessageBox.Show("Generated Completely.");
                    }


                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void cmbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtRelation.Rows.Count > 0)
            {
                DataView dv = dtRelation.DefaultView;
                dv.RowFilter = "K_Table='" + cmbTable.Text + "'";

                DataTable GetColumns = getColumnsName(cmbTable.Text);
                dtColumn.Constraints.Clear();
                GetColumns.Constraints.Clear();
                MergeTables(GetColumns);

                gvColumns.AutoGenerateColumns = false;

                gvColumns.DataSource = dtColumn;


                DataTable dtNew = dv.ToTable();

                var distinctRows = (from DataRow dRow in dtNew.Rows
                                    select new { PK_Table = dRow["PK_Table"], strJoin = "INNER JOIN" }).Distinct();

                gvRelation.AutoGenerateColumns = false;
                gvRelation.DataSource = distinctRows.ToList();


                foreach (DataGridViewRow grv in gvRelation.Rows)
                    grv.Cells["DBJoin"].Value = "INNER JOIN";



            }
        }

        private void gvRelation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvRelation.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
            {
                if (gvRelation.CurrentCell.IsInEditMode)
                {
                    if (Convert.ToBoolean(gvRelation.Rows[e.RowIndex].Cells["Select"].Selected) == true)
                    {
                        DataTable GetColumns = getColumnsName(gvRelation.Rows[e.RowIndex].Cells["tblRelation"].Value.ToString());
                        dtColumn.Constraints.Clear();
                        GetColumns.Constraints.Clear();
                        MergeTables(GetColumns);

                        gvColumns.AutoGenerateColumns = false;
                        gvColumns.DataSource = dtColumn;
                    }
                    else
                    {
                        RemoveColumns();
                    }
                }
            }
        }

        private void cmbDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTable.Items.Clear();
            chkSpList.Items.Clear();
            GetTableList();
            GetSpList();
            // drpProject.Items.Clear();

            if (txtDirectory.Text.Trim() != "")
            {
                string[] directoryList = System.IO.Directory.GetDirectories(txtDirectory.Text.Trim());
                Array.Resize(ref directoryList, directoryList.Length + 1);
                directoryList[directoryList.Length - 1] = txtDirectory.Text + "\\Web Project Directory";

                drpProject.DataSource = directoryList;
            }

            dtRelation = getRelation();
        }

      
  

    }
}
