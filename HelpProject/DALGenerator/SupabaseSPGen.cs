using Npgsql;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DALGenerator
{
    public partial class SupabaseSPGen : Form
    {
        DataTable dtColumns = new DataTable();
        DataTable dtRelations = new DataTable();
        DataTable dtSelectCols = new DataTable();
        DataTable dataTable = new DataTable();
        DataTable dtAllFunctons = new DataTable();
        string DataAccessCode_insert = "";
        string DataAccessCode_update = "";
        string IDataAccessCode_insert = "";
        string IDataAccessCode_update = "";
        string DataAccessCode_get = "";
        string IDataAccessCode_get = "";
        public SupabaseSPGen()
        {
            InitializeComponent();
        }


        private string GetColumnHtmlType(string col_type) //entity)
        {

            var ColType = col_type;

            string table_field = "";

            if (ColType.ToLower() == "text" || ColType.Contains("character") || ColType.ToLower() == "json")
                table_field += "text";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "tinyint" || ColType.ToLower() == "bigint" || ColType.ToLower() == "integer"
                || ColType.ToLower() == "money" || ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "number";

            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
                table_field += "date";

            return table_field;
        }

        private string GetCSharpType(string col_type) //entity)
        {

            var ColType = col_type;

            string table_field = "";

            if (ColType.ToLower() == "text" || ColType.Contains("character") || ColType.ToLower() == "json")
                table_field += "string ";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "tinyint" || ColType.ToLower() == "bigint" || ColType.ToLower() == "integer")
                table_field += "Int64 ";
            else if (ColType.ToLower() == "money" || ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "Decimal ";

            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
                table_field += "DateTime ";
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
                table_field += "Boolean ";

            return table_field;
        }

        private string GetCSharpTypeForNGSQL(string col_type) //entity)
        {

            var ColType = col_type;

            string table_field = "";

            if (ColType.ToLower() == "text" || ColType.Contains("character") || ColType.ToLower() == "json")
                table_field += "NpgsqlDbType.Text";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "tinyint" || ColType.ToLower() == "bigint" || ColType.ToLower() == "integer")
                table_field += "NpgsqlDbType.Bigint";
            else if (ColType.ToLower() == "money" || ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "NpgsqlDbType.Numeric";

            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
                table_field += "NpgsqlDbType.Date";
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
                table_field += "NpgsqlDbType.Boolean";

            return table_field;
        }

        private string GetTableFieldString(string col_name, string col_type, string is_identity, string is_nullable, int isEntityOrDTO) //entity)
        {
            var Column = col_name;
            var ColType = col_type;

            string table_field = "";

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.Contains("character"))
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }

                table_field += "\t\t\tpublic string" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "tinyint" || ColType.ToLower() == "integer")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }
                table_field += "\t\t\tpublic Int32" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "bigint")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = "\t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }
                table_field += "\t\t\tpublic Int64" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "money")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }
                table_field += "\t\t\tpublic Decimal" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }
                table_field += "\t\t\tpublic Boolean" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }
                table_field += "\t\t\tpublic DateTime" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2)
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1 && is_identity == "yes")
                {
                    table_field += " \t\t\t[Key]\n";
                }
                else
                {
                    table_field += " \t\t\t[Column(\"" + Column + "\")]\n";
                }
                table_field += "\t\t\tpublic Decimal" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "json")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if (isEntityOrDTO == 1)
                {
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                    table_field += "\t\t\tpublic String" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
                }
                else
                    table_field += "\t\t\tpublic JArray" + (is_nullable == "yes" ? "?" : "") + " " + Column + "  { get; set;}\n\n";
            }

            return table_field;
        }

        private string BuildJS()
        {
            try
            {
                var table_view_name = "";
                List<string>
    scripts = new List<string>
        ();
                String sb = "";

                string[] strss = { "", "" };//this.txtScript.Lines


                foreach (string input in strss)
                {

                    if (input.Contains("CREATE TABLE"))
                    {
                        table_view_name = input.Substring(input.LastIndexOf('.') + 1);

                        sb += "\n\nvar obj" + input.Substring(input.LastIndexOf('.') + 1) + " = {\n\n";

                        continue;
                    }

                    if (input.Contains("CONSTRAINT") || (input.Contains("(") && input.Trim().Length == 1))
                    {
                        continue;
                    }

                    if (input.Contains(")") && input.Trim().Length == 1)
                    {
                        break;
                    }
                    var str = input.Trim().Split(' ');

                    sb += "\t\t" + str[0] + ": $('#" + str[0] + "').val(),\n";
                }

                sb += "\n\t\t}";


                string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

                if (!Directory.Exists(txtDirctory.Text.Trim() + "\\" + table_view_name))
                    Directory.CreateDirectory(txtDirctory.Text.Trim() + "\\" + table_view_name);

                string path = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\" + table_view_name + "_Insert.js";

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }

                System.IO.StreamWriter file2 = System.IO.File.AppendText(path);

                file2.WriteLine(sb.ToString());
                file2.Close();

                return table_view_name;

            }
            catch (Exception es)
            {
                return "";
            }
        }
        private string BuildTableEntity(int isEntityOrDTO,string inputtablename)
        {
            try
            {
                var table_view_name = "";
                List<string> scripts = new List<string>();
                String sb = "";

                if (isEntityOrDTO == 1) //entity
                    sb = "\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;using Dapper.Contrib.Extensions;\n\nnamespace EPYSLSAILORAPP.Domain.Entity\n{";
                else//dto
                    sb = "\nusing BDO.Core.Base;\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nusing System.ComponentModel.DataAnnotations;\nnamespace EPYSLSAILORAPP.Domain.DTO\n{";

                table_view_name =inputtablename;

                if (isEntityOrDTO == 1) //entity
                {
                    sb += " [System.ComponentModel.DataAnnotations.Schema.Table(\"" + table_view_name + "\")]";
                    sb += "\n\n\t\tpublic class " +inputtablename + "_entity : DapperExt\n\t\t{\n\n";
                }
                else
                    sb += "\n\n\t\tpublic class " +inputtablename + "_DTO : BaseEntity\n\t\t{\n\n";

                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    string column_name = dtColumns.Rows[i][0].ToString().ToLower();
                    string data_type = dtColumns.Rows[i][2].ToString().ToLower();
                    string is_nullable = dtColumns.Rows[i][1].ToString().ToLower();
                    string is_identity = dtColumns.Rows[i][3].ToString().ToLower();

                    var singlecol = GetTableFieldString(column_name, data_type, is_identity, is_nullable, isEntityOrDTO);
                    sb += singlecol;


                }

                if (isEntityOrDTO == 2 && ddlOutputTables.Items.Count > 1) //entity
                {
                    for (int ind = 1; ind < ddlOutputTables.Items.Count; ind++)
                    {
                        DataRowView rowView = (DataRowView)ddlOutputTables.Items[ind];

                        // Assuming the column you want to retrieve the value from is named "ColumnName"
                        object value = rowView[0];

                        var det_tblname = CamelCase(value.ToString());
                        sb += "\t\t\tpublic List<" + value.ToString() + "_DTO> " + det_tblname + "_List {get; set;}\n\n";
                    }
                }

                sb += "\n\t\t}\n}";

                string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

                if (!Directory.Exists(txtDirctory.Text.Trim() + "\\" + table_view_name))
                    Directory.CreateDirectory(txtDirctory.Text.Trim() + "\\" + table_view_name);

                string path = "";
                if (isEntityOrDTO == 1) //entity
                    path = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\" + table_view_name + "_entity.cs";
                else
                    path = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\" + table_view_name + "_DTO.cs";

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                System.IO.StreamWriter file2 = System.IO.File.AppendText(path);

                file2.WriteLine(sb.ToString());
                file2.Close();

                //MappingProfile
                path = "";
                if (isEntityOrDTO == 1) //entity
                {
                    path = txtDirctory.Text.Trim() + "\\MappingProfile.cs";
                    var filetext = "";

                    if (System.IO.File.Exists(path))
                    {
                        filetext = File.ReadAllText(path);
                    }

                    if (string.IsNullOrEmpty(filetext) || !filetext.Contains(table_view_name + "_DTO"))
                    {
                        System.IO.StreamWriter file3 = System.IO.File.AppendText(path);

                        file3.WriteLine("CreateMap<" + table_view_name + "_DTO, " + table_view_name + "_entity>();");
                        file3.WriteLine("CreateMap<" + table_view_name + "_entity," + table_view_name + "_DTO>();");

                        file3.Close();
                    }
                }

                return table_view_name;

            }
            catch (Exception es)
            {
                return "";
            }
        }
        public string CamelCase(string str)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string[] words = str.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i]);
            }
            return string.Join("", words);
        }
        public string GetColumnNameText(string str)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string[] words = str.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i]);
            }
            return string.Join(" ", words);
        }
        private string BuildTableInterface(string table_view_name )
        {
            var filetext = GetFileText("Templates//IDataAccessService.txt");

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext
                .Replace("{TableName}", servicename)
                .Replace("{EntityName}", table_view_name)
                .Replace("{ExtendedFunctions}", IDataAccessCode_insert + Environment.NewLine + IDataAccessCode_update + Environment.NewLine + IDataAccessCode_get + Environment.NewLine);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            CreateFileWithText(generated_text, EntityLocation + "\\I" + servicename + "Service.cs");

            return servicename;
        }


        private void BuildTableService(string table_view_name)
        {
            var filetext = GetFileText("Templates//DataAccessService.txt");

            var servicename = CamelCase(table_view_name);

            DataView dv = dtColumns.DefaultView;
            dv.RowFilter = "data_type='text'";

            String generated_text = filetext
                .Replace("{TableName}", servicename)
                .Replace("{EntityName}", table_view_name)
                .Replace("{TextColumn}", dv.ToTable().Rows.Count>0? dv.ToTable().Rows[0][0].ToString():"")
                .Replace("{PrimaryColumn}", dtColumns.Rows[0][0].ToString().ToLower())
                .Replace("{ExtendedFunctions}", DataAccessCode_insert + Environment.NewLine + DataAccessCode_update + Environment.NewLine + DataAccessCode_get + Environment.NewLine);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "Service.cs");

        }

        private void CreateFileWithText(string generate_text, string path, bool? isDelete = true)
        {


            if (System.IO.File.Exists(path) && isDelete == true)
            {
                System.IO.File.Delete(path);

            }

            System.IO.StreamWriter file2 = System.IO.File.AppendText(path);

            file2.WriteLine(generate_text);
            file2.Close();
        }

        public string GetFileText(string path)
        {
            string readText = "";

            path = Path.Combine(Environment.CurrentDirectory, path);

            if (File.Exists(path))
            {
                readText = File.ReadAllText(path);
            }

            return readText;
        }



        private void BuildViewFiles()
        {
            BuildViewEntity();
        }


        private void BuildRPCFunctionInterface(List<string>
            paramList)
        {
            var all_paramDeclarationList = "";

            var filetext = GetFileText("Templates//IRPCDataAccessService.txt");

            var servicename = CamelCase(paramList[0].Replace("_", ""));

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", paramList[0]);

            if (paramList.Count > 1)
            {
                for (int i = 1; i < paramList.Count; i++)
                {
                    all_paramDeclarationList += GetRPCFunctionFieldParameterString(paramList[i].Replace(",", ""));

                    if (i != paramList.Count)
                        all_paramDeclarationList += ",";
                }

                if (all_paramDeclarationList.Length > 0)
                    all_paramDeclarationList = all_paramDeclarationList.Substring(0, all_paramDeclarationList.Length - 1);

                generated_text = generated_text.Replace("{all_param_declaration}", all_paramDeclarationList);
            }

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + paramList[0];

            CreateFileWithText(generated_text, EntityLocation + "\\I" + servicename + "Service.cs");

        }

        private void BuildRPCFunctionService(List<string>
            paramList)
        {
            var all_paramAddList = "";
            var all_paramDeclarationList = "";

            var filetext = GetFileText("Templates//RPCDataAccessService.txt");

            if (paramList.Count > 1)
            {
                for (int i = 1; i < paramList.Count; i++)
                {
                    all_paramDeclarationList += GetRPCFunctionFieldParameterString(paramList[i].Replace(",", ""));

                    if (i != paramList.Count - 1)
                        all_paramDeclarationList += ",";

                    var col = paramList[i].Split(' ');

                    if (col.Length > 0)
                    {
                        all_paramAddList += "\tobjFilter.Add(\"" + col[0].Trim() + "\", " + col[0].Trim() + ");\n";
                    }
                }

                filetext = filetext.Replace("{all_params}", all_paramAddList);
            }

            var servicename = CamelCase(paramList[0].Replace("_", ""));

            String generated_text = filetext.Replace("{all_param_declaration}", all_paramDeclarationList).Replace("{TableName}", servicename).Replace("{EntityName}", paramList[0]);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + paramList[0];

            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "Service.cs");

        }

        private void BuildPGSQLunctionService(List<string> paramList, bool? isRPCFile = false)
        {
            var all_paramAddList = "";
            var all_paramDeclarationList = "";
            var all_param_names = "";

            var filetext = GetFileText("Templates//PGSQLDapperDataAccessService.txt");

            if (paramList.Count > 1)
            {
                for (int i = 1; i < paramList.Count; i++)
                {
                    all_param_names += "@" + paramList[i].Split(' ')[0].Replace(",", "");

                    all_paramDeclarationList += GetRPCFunctionFieldParameterString(paramList[i].Replace(",", ""));

                    if (i != paramList.Count - 1)
                    {
                        all_paramDeclarationList += ",";
                        all_param_names += ",";
                    }

                }

            }

            var servicename = CamelCase(paramList[0].Replace("_", ""));

            string EntityLocation = "";

            if (isRPCFile == true)
            {
                servicename = "PGSQLDapper";
                EntityLocation = txtDirctory.Text.Trim();
            }
            else
            {
                EntityLocation = txtDirctory.Text.Trim() + "\\" + paramList[0];
            }

            String generated_text = filetext.Replace("{all_params}", all_param_names.Replace("@", "")).Replace("{all_param_names}", all_param_names).Replace("{all_param_declaration}", all_paramDeclarationList).Replace("{TableName}", servicename).Replace("{EntityName}", paramList[0]);


            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "ServicePGSQL.cs", false);

        }

        private string GetRPCFunctionFieldString(DataRow dr)
        {

            string table_field = "";

            var ColType = dr["data_type"].ToString();
            var Column = dr["column_name"].ToString();

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.Contains("character"))
                table_field += "\t\t\tpublic String " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "TINYINT")
                table_field += "\t\t\tpublic Int32? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "bigint")
                table_field += "\t\t\tpublic Int64? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "json")
                table_field += "\t\t\tpublic JArray? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "money")
                table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
                table_field += "\t\t\tpublic Boolean? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
                table_field += "\t\t\tpublic DateTime? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";


            return table_field;
        }

        private string GetRPCFunctionFieldString(string ColType, string Column)
        {
            string table_field = "";

            //var ColType = dr["data_type"].ToString();
            //var Column = dr["column_name"].ToString();

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.Contains("character"))
                table_field += "\t\t\tpublic String " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "TINYINT")
                table_field += "\t\t\tpublic Int32? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "bigint")
                table_field += "\t\t\tpublic Int64? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "json")
                table_field += "\t\t\tpublic JArray? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "money")
                table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
                table_field += "\t\t\tpublic Boolean? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
                table_field += "\t\t\tpublic DateTime? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";


            return table_field;
        }
        private string GetRPCFunctionFieldParameterString(string inputLine)
        {

            var columns = inputLine.Split(' ');
            string table_field = "";

            var ColType = columns[1].Trim();
            var Column = columns[0].Trim();

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.Contains("character"))
                table_field += "\t\t\tString " + Column + "\n";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "TINYINT")
                table_field += "\t\t\t Int32? " + Column + "\n";
            else if (ColType.ToLower() == "bigint")
                table_field += "\t\t\t Int64? " + Column + "\n";
            else if (ColType.ToLower() == "json")
                table_field += "\t\t\t JArray " + Column + "\n";
            else if (ColType.ToLower() == "money")
                table_field += "\t\t\t Decimal? " + Column + "\n";
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
                table_field += "\t\t\t Boolean? " + Column + "\n";
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
                table_field += "\t\t\tp DateTime? " + Column + "\n";
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "\t\t\t Decimal? " + Column + "\n";


            return table_field;
        }

        private List<string> BuildRPCFunctionEntityNew(string func_name)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var connection = new NpgsqlConnection(txtDbCon.Text))
                {
                    string query = @"
                            SELECT
                            column_name,
                            format_type(i, NULL) AS data_type,
                            CASE
                            WHEN j = 'i' THEN 'INPUT'
                            ELSE 'OUTPUT'
                            END
                            FROM (
                            SELECT
                            unnest(proargnames) AS column_name,
                            unnest(proallargtypes) AS i, unnest(proargmodes) j
                            ,*
                            FROM
                            pg_proc
                            WHERE
                            proname = '" + func_name + @"'
                            ) AS function_columns;";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                DataView outputparams = dt.Copy().DefaultView;
                outputparams.RowFilter = "case='OUTPUT'";

                DataView inputparams = dt.Copy().DefaultView;
                inputparams.RowFilter = "case='INPUT'";

                List<string> scripts = new List<string>();
                List<string> paramList = new List<string>();

                String sb = "\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nnamespace EPYSLSAILORAPP.Domain.RPC\n{";

                var table_view_name = func_name;
                paramList.Add(table_view_name);
                sb += "\n\n\t\tpublic class rpc_" + table_view_name + "_DTO \n\t\t{\n\n";

                for (var i = 0; i < outputparams.ToTable().Rows.Count; i++)
                {
                    sb += GetRPCFunctionFieldString(outputparams.ToTable().Rows[i]);
                }

                sb += "\n\t\t}\n}";

                for (var i = 0; i < inputparams.ToTable().Rows.Count; i++)
                {
                    if (inputparams.ToTable().Rows.Count > 0)
                    {
                        paramList.Add(inputparams.ToTable().Rows[i]["column_name"].ToString() +
                        " " + inputparams.ToTable().Rows[i]["data_type"].ToString());
                    }
                }

                string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

                if (!Directory.Exists(txtDirctory.Text.Trim() + "\\" + table_view_name))
                    Directory.CreateDirectory(txtDirctory.Text.Trim() + "\\" + table_view_name);

                string path = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\" + table_view_name + "_DTO.cs";

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                System.IO.StreamWriter file2 = System.IO.File.AppendText(path);

                file2.WriteLine(sb.ToString());
                file2.Close();

                return paramList;

            }
            catch (Exception es)
            {
                return new List<string>
                    ();
            }
        }

        private string GetViewFieldString(string inputLine)
        {

            var Column = inputLine.Trim().Contains(".") ? inputLine.Trim().Split('.')[1] : inputLine.Trim();
            Column = Column.Replace(",", "");
            string table_field = "";

            if (Column.Contains("added_by") || Column.Contains("updated_by"))
                table_field += "\t\t\tpublic Int64? " + Column + "  { get; set;}\n\n";
            else if (Column.ToLower().Contains("_id"))
                table_field += "\t\t\tpublic Int64? " + Column + "  { get; set;}\n\n";
            else if (Column.ToLower().Contains("date"))
                table_field += "\t\t\tpublic Datetime? " + Column + "  { get; set;}\n\n";
            else if (Column.ToLower().Contains("_quantity"))
                table_field += "\t\t\tpublic Int64?? " + Column + "  { get; set;}\n\n";
            else if (Column.ToLower().Contains("_price"))
                table_field += "\t\t\tpublic Decimal?? " + Column + "  { get; set;}\n\n";
            else
                table_field += "\t\t\tpublic String " + Column + "  { get; set;}\n\n";

            return table_field;
        }

        private void BuildViewEntity()
        {
            try
            {
                List<string>
                    scripts = new List<string>
();

                String sb = "\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nnamespace EPYSLSAILORAPP.Domain.DTO\n{";

                var table_view_name = "";

                string[] strss = { "", "" };//this.txtScript.Lines

                foreach (string input in strss)
                {

                    if (input.Contains("CREATE OR REPLACE VIEW"))
                    {
                        table_view_name = input.Substring(input.LastIndexOf('.') + 1);
                        sb += "\n\n\t\tpublic class " + input.Substring(input.LastIndexOf('.') + 1) + "_DTO \n\t\t{\n\n";
                        continue;
                    }

                    if (input.Contains("AS") && input.Trim().Length == 2)
                    {
                        continue;
                    }

                    if (input.Contains(" FROM "))
                    {
                        break;
                    }



                    var singlecol = GetViewFieldString(input.Replace("SELECT ", ""));
                    sb += singlecol;
                }

                sb += "\n\t\t}\n}";

                string EntityLocation = txtDirctory.Text.Trim() + "\\Entity";

                if (!Directory.Exists(txtDirctory.Text.Trim() + "\\Entity"))
                    Directory.CreateDirectory(txtDirctory.Text.Trim() + "\\Entity");

                string path = txtDirctory.Text.Trim() + "\\Entity\\" + table_view_name + "_DTO.cs";

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }
                System.IO.StreamWriter file2 = System.IO.File.AppendText(EntityLocation + "\\" + table_view_name + "_DTO.cs");

                file2.WriteLine(sb.ToString());
                file2.Close();

            }
            catch (Exception es)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDirctory.Text = folderBrowserDialog1.SelectedPath;

            }
        }


        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {

                dataTable = new DataTable();

                try
                {
                    string query = @"select table_name from information_schema.tables where table_schema='public' order by table_name";

                    connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dataTable);
                    }

                    DataRow dr = dataTable.NewRow();
                    dr[0] = "All_Tables";

                    dataTable.Rows.InsertAt(dr,0);

                    ddlTables.DataSource = dataTable;
                    ddlTables.DisplayMember = "table_name";
                    ddlTables.ValueMember = "table_name";

                    ddljointo.DataSource = dataTable.Copy();
                    ddljointo.DisplayMember = "table_name";
                    ddljointo.ValueMember = "table_name";


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                DataTable dt = new DataTable();

                dt = new DataTable();

                try
                {
                    string query = @"SELECT p.proname AS function_name FROM pg_proc p
                    LEFT JOIN pg_namespace n ON p.pronamespace = n.oid
                    WHERE n.nspname = 'public' ORDER BY function_name;";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dtAllFunctons);
                    }

                    DataRow drnew = dtAllFunctons.NewRow();
                    drnew["function_name"] = "All Functions";
                    dtAllFunctons.Rows.InsertAt(drnew, 0);
                    cmb_function.DataSource = dtAllFunctons;
                    cmb_function.DisplayMember = "function_name";
                    cmb_function.ValueMember = "function_name";

                }
                catch (Exception ex)
                {
                    // Handle exception, log, and optionally rollback the transaction
                    Console.WriteLine($"Error: {ex.Message}");

                }

                dt = new DataTable();

                try
                {
                    string query = @"select m.menu_caption,m.menu_id from menu m where m.menu_id=m.parent_id order by m.seq_no";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dt);
                    }

                    cmbParentMenu.DataSource = dt.Copy();
                    cmbParentMenu.DisplayMember = "menu_caption";
                    cmbParentMenu.ValueMember = "menu_id";

                }
                catch (Exception ex)
                {
                    // Handle exception, log, and optionally rollback the transaction
                    Console.WriteLine($"Error: {ex.Message}");

                }


            }
        }

        private void btnLoadColumn(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {

                dtColumns = new DataTable();

                try
                {
                    string query = @"SELECT  col.column_name,col.is_nullable,col.data_type,col.is_identity, true as ChkSelect, false as ChkFilter FROM information_schema.columns col
                    WHERE table_name='" + ddlTables.SelectedValue + "' order by col.ordinal_position";

                    connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dtColumns);
                    }

                    for (int i = 0; i < dtColumns.Rows.Count; i++)
                    {

                        // Get the value from the specified column in the selected row
                        object colname = dtColumns.Rows[i][0];

                        if (colname.ToString().ToLower() == "added_by" || colname.ToString().ToLower() == "updated_by" || colname.ToString().ToLower() == "date_added" || colname.ToString().ToLower() == "date_updated")
                        {
                            dtColumns.Rows[i][4] = false;
                        }
                    }

                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dtColumns;
                    dataGridView1.Columns[0].Width = 170;
                    dataGridView1.Columns[1].Width = 0;
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[3].Width = 0;
                    dataGridView1.Columns[4].Width = 45;
                    dataGridView1.Columns[5].Width = 45;

                    dtSelectCols = new DataTable();
                    dtSelectCols.Columns.Add("table_name", typeof(string));
                    dtSelectCols.Columns.Add("column_name", typeof(string));
                    dtSelectCols.Columns.Add("data_type", typeof(string));

                    DataTable data1 = dtColumns.Copy();
                    var dv = data1.DefaultView;
                    dv.RowFilter = "data_type='json'";
                    data1 = dv.ToTable();
                    DataRow newRow = data1.NewRow();
                    newRow["column_name"] = "select";
                    data1.Rows.InsertAt(newRow, 0);
                    ddl_json_column1.DataSource = data1;
                    ddl_json_column1.ValueMember = "column_name";
                    ddl_json_column1.DisplayMember = "column_name";

                    DataTable data2 = dtColumns.Copy();
                    dv = data2.DefaultView;
                    dv.RowFilter = "data_type='json'";
                    data2 = dv.ToTable();
                    newRow = data2.NewRow();
                    newRow["column_name"] = "select";
                    data2.Rows.InsertAt(newRow, 0);
                    ddl_json_column2.DataSource = data2;
                    ddl_json_column2.ValueMember = "column_name";
                    ddl_json_column2.DisplayMember = "column_name";

                    DataTable data3 = dtColumns.Copy();
                    dv = data3.DefaultView;
                    dv.RowFilter = "data_type='json'";
                    data3 = dv.ToTable();
                    newRow = data3.NewRow();
                    newRow["column_name"] = "select";
                    data3.Rows.InsertAt(newRow, 0);
                    ddl_json_column3.DataSource = data3;
                    ddl_json_column3.ValueMember = "column_name";
                    ddl_json_column3.DisplayMember = "column_name";

                    DataTable data4 = dtColumns.Copy();
                    dv = data4.DefaultView;
                    dv.RowFilter = "data_type='json'";
                    data4 = dv.ToTable();
                    newRow = data4.NewRow();
                    newRow["column_name"] = "select";
                    data4.Rows.InsertAt(newRow, 0);
                    ddl_json_column4.DataSource = data4;
                    ddl_json_column4.ValueMember = "column_name";
                    ddl_json_column4.DisplayMember = "column_name";

                    DataTable data5 = dtColumns.Copy();
                    dv = data5.DefaultView;
                    dv.RowFilter = "data_type='json'";
                    data5 = dv.ToTable();
                    newRow = data5.NewRow();
                    newRow["column_name"] = "select";
                    data5.Rows.InsertAt(newRow, 0);
                    ddl_json_column5.DataSource = data5;
                    ddl_json_column5.ValueMember = "column_name";
                    ddl_json_column5.DisplayMember = "column_name";

                    var dtForeignKeys = new DataTable();
                    query = @"SELECT
                    conrelid::regclass::text AS referencing_table,
                    a.attname AS referencing_column,
                    confrelid::regclass::text AS referenced_table,
                    b.attname AS referenced_column
                    FROM
                    pg_constraint AS c
                    JOIN
                    pg_attribute AS a ON a.attnum = ANY(c.conkey) AND a.attrelid = c.conrelid
                    JOIN
                    pg_attribute AS b ON b.attnum = ANY(c.confkey) AND b.attrelid = c.confrelid
                    WHERE
                    confrelid::regclass::text = '" + ddlTables.SelectedValue + @"';";

                    // connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dtForeignKeys);
                    }

                    DataTable frdata1 = dtForeignKeys.Copy();

                    newRow = frdata1.NewRow();
                    newRow["referencing_table"] = "select";
                    frdata1.Rows.InsertAt(newRow, 0);

                    ddl_detl1.DataSource = frdata1;
                    ddl_detl1.DisplayMember = "referencing_table";
                    ddl_detl1.ValueMember = "referencing_table";

                    DataTable frdata2 = dtForeignKeys.Copy();

                    newRow = frdata2.NewRow();
                    newRow["referencing_table"] = "select";
                    frdata2.Rows.InsertAt(newRow, 0);

                    ddl_detl2.DataSource = frdata2;
                    ddl_detl2.DisplayMember = "referencing_table";
                    ddl_detl2.ValueMember = "referencing_table";

                    DataTable frdata3 = dtForeignKeys.Copy();

                    newRow = frdata3.NewRow();
                    newRow["referencing_table"] = "select";
                    frdata3.Rows.InsertAt(newRow, 0);

                    ddl_detl3.DataSource = frdata3;
                    ddl_detl3.DisplayMember = "referencing_table";
                    ddl_detl3.ValueMember = "referencing_table";

                    DataTable frdata4 = dtForeignKeys.Copy();

                    newRow = frdata4.NewRow();
                    newRow["referencing_table"] = "select";
                    frdata4.Rows.InsertAt(newRow, 0);

                    ddl_detl4.DataSource = frdata4;
                    ddl_detl4.DisplayMember = "referencing_table";
                    ddl_detl4.ValueMember = "referencing_table";

                    DataTable frdata5 = dtForeignKeys.Copy();

                    newRow = frdata5.NewRow();
                    newRow["referencing_table"] = "select";
                    frdata5.Rows.InsertAt(newRow, 0);

                    ddl_detl5.DataSource = frdata5;
                    ddl_detl5.DisplayMember = "referencing_table";
                    ddl_detl5.ValueMember = "referencing_table";

                    dtRelations = new DataTable();
                    query = @"select r.* from (SELECT conrelid::regclass::text  AS referencing_table,
                      a.attname                 AS referencing_column,
                      confrelid::regclass::text AS referenced_table,
                      b.attname                 AS referenced_column,
                      0                         as tableorder
                       FROM pg_constraint AS c
                                JOIN
                            pg_attribute AS a ON a.attnum = ANY (c.conkey) AND a.attrelid = c.conrelid
                                JOIN
                            pg_attribute AS b ON b.attnum = ANY (c.confkey) AND b.attrelid = c.confrelid
                       WHERE confrelid::regclass::text =  '" + ddlTables.SelectedValue + @"'

                       union

                       SELECT confrelid::regclass::text AS referencing_table,
                              a.attname                 AS referencing_column,
                              conrelid::regclass::text  AS referenced_table,
                              b.attname                 AS referenced_column,
                              1                         as tableorder
                       FROM pg_constraint AS c
                                JOIN
                            pg_attribute AS a ON a.attnum = ANY (c.conkey) AND a.attrelid = c.conrelid
                                JOIN
                            pg_attribute AS b ON b.attnum = ANY (c.confkey) AND b.attrelid = c.confrelid
                       WHERE conrelid::regclass::text =  '" + ddlTables.SelectedValue + @"') r
                        order by r.tableorder;
                        ";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dtRelations);
                    }

                    DataTable frndata1 = dtRelations.Copy();

                    newRow = frndata1.NewRow();
                    newRow["referencing_table"] = "select";
                    frndata1.Rows.InsertAt(newRow, 0);

                    newRow = frndata1.NewRow();
                    newRow["referencing_table"] = ddlTables.SelectedValue;
                    frndata1.Rows.InsertAt(newRow, 1);

                    ddljoinwith.DataSource = frndata1;
                    ddljoinwith.DisplayMember = "referencing_table";
                    ddljoinwith.ValueMember = "referencing_table";


                    DataTable frndata2 = dtRelations.Copy();

                    newRow = frndata2.NewRow();
                    newRow["referencing_table"] = "select";
                    frndata2.Rows.InsertAt(newRow, 0);

                    ddlOutputTables.DataSource = frndata2;
                    ddlOutputTables.DisplayMember = "referencing_table";
                    ddlOutputTables.ValueMember = "referencing_table";


                }
                catch (Exception ex)
                {
                    // Handle exception, log, and optionally rollback the transaction
                    MessageBox.Show($"Error: {ex.Message}");

                }

            }
        }

        private void SupabaseSPGen_Load(object sender, EventArgs e)
        {

        }

        private void SupabaseSPGen_Load_1(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuActionGenerator sistema = new MenuActionGenerator();
            sistema.ShowDialog();
            this.Close();
        }

        private void btnFunc_Click(object sender, EventArgs e)
        {
            this.Hide();
            SupabaseGen sistema = new SupabaseGen();
            sistema.ShowDialog();
            this.Close();
        }
        private string GetAbbreviation(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            string[] words = input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            string abbreviation = string.Join("", words.Select(word => word[0]));

            return abbreviation.ToLower();
        }
        private void btnGenSp_Click(object sender, EventArgs e)
        {
            try
            {
                Generate_Insert_SP(ddlTables.SelectedValue.ToString());
                Generate_Update_SP(ddlTables.SelectedValue.ToString());
                Generate_GetData_SP(ddlTables.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Generate_GetData_SP(string tablename)
        {
            //DataTable dtMain = GetTableDefinition(ddlTables.SelectedValue.ToString());

            string TableName =tablename, ServiceFunctionFilters = "", FilterParams = "", OutputParamsWithType = "", TableColumns = "", strWhereCondition = "", ListVariablesDeclare = "", ListVariableWithRename = "";

            string table_aleas = GetAbbreviation(TableName);

            String str_rpc_function = "\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nnamespace EPYSLSAILORAPP.Domain.RPC\n{";

            string all_paramAddList = "";

            str_rpc_function += "\n\n\t\tpublic class rpc_" + TableName + "_DTO \n\t\t{\n\n";

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = dataGridView1.Rows[i].Cells[4] as DataGridViewCheckBoxCell;
                bool? isChecked = (bool?)checkBoxCell.Value;

                DataGridViewCheckBoxCell checkBoxFilterCell = dataGridView1.Rows[i].Cells[5] as DataGridViewCheckBoxCell;
                bool? isCheckedFilter = (bool?)checkBoxFilterCell.Value;

                var colname = dataGridView1.Rows[i].Cells["column_name"].Value.ToString();
                var coltype = dataGridView1.Rows[i].Cells["data_type"].Value.ToString();

                if (isChecked.HasValue && isChecked == true)
                {
                    str_rpc_function += GetRPCFunctionFieldString(coltype, colname);

                    TableColumns += table_aleas + "." + colname + "," + Environment.NewLine;

                    OutputParamsWithType += colname + " " + (coltype.ToLower().Contains("json") ? " text" : coltype.ToLower()) + "," + Environment.NewLine;
                }

                if (isCheckedFilter.HasValue && isCheckedFilter == true)
                {
                    ServiceFunctionFilters += GetCSharpType(coltype) + " " + colname + "_filter," + Environment.NewLine;

                    FilterParams += colname + "_filter " + (coltype.ToLower().Contains("json") ? " text" : coltype.ToLower()) + "," + Environment.NewLine;

                    all_paramAddList += "\tobjFilter.Add(\"" + colname.Trim() + "_filter" + "\", " + colname.Trim() + "_filter" + ");" + Environment.NewLine;


                    if (string.IsNullOrEmpty(strWhereCondition))
                        strWhereCondition += "WHERE " + table_aleas + "." + colname + " = " + colname + "_filter";
                    else
                        strWhereCondition += " AND " + table_aleas + "." + colname + " = " + colname + "_filter";
                }

            }



            for (int i = 0; i < grid_allselect_columns.Rows.Count; i++)
            {
                if (grid_allselect_columns.Rows[i].Cells[1].Value != null)
                {
                    var colname = grid_allselect_columns.Rows[i].Cells[1].Value.ToString();
                    var coltype = grid_allselect_columns.Rows[i].Cells[2].Value.ToString();

                    str_rpc_function += GetRPCFunctionFieldString(coltype, colname);

                    table_aleas = GetAbbreviation(grid_allselect_columns.Rows[i].Cells[0].Value.ToString());

                    OutputParamsWithType += colname + " " +
                        (coltype.ToLower().Contains("json") ? " text" : coltype.ToLower()) + " ," + Environment.NewLine;

                    TableColumns += table_aleas + "." + colname + "," + Environment.NewLine;
                }

            }

            str_rpc_function += "\n\t\t}\n}";

            var filetext = GetFileText("Templates//getdata_Function.txt");

            var inner_table_text = "";
            ////////////////generate det table
            if (ddl_detl1.SelectedIndex != 0)
            {
                var retSQL = GetSelectJSONInnerSQL(ddl_detl1.SelectedValue.ToString());

                ListVariablesDeclare += retSQL[0];
                ListVariableWithRename += retSQL[1];
                inner_table_text += retSQL[2];
                OutputParamsWithType += ddl_detl1.SelectedValue.ToString() + "_list text,";
            }
            if (ddl_detl2.SelectedIndex != 0)
            {
                var retSQL = GetSelectJSONInnerSQL(ddl_detl2.SelectedValue.ToString());

                ListVariablesDeclare += retSQL[0];
                ListVariableWithRename += retSQL[1];
                inner_table_text += retSQL[2];
                OutputParamsWithType += ddl_detl2.SelectedValue.ToString() + "_list text,";
            }
            if (ddl_detl3.SelectedIndex != 0)
            {
                var retSQL = GetSelectJSONInnerSQL(ddl_detl3.SelectedValue.ToString());

                ListVariablesDeclare += retSQL[0];
                ListVariableWithRename += retSQL[1];
                inner_table_text += retSQL[2];
                OutputParamsWithType += ddl_detl3.SelectedValue.ToString() + "_list text,";
            }
            if (ddl_detl4.SelectedIndex != 0)
            {
                var retSQL = GetSelectJSONInnerSQL(ddl_detl4.SelectedValue.ToString());

                ListVariablesDeclare += retSQL[0];
                ListVariableWithRename += retSQL[1];
                inner_table_text += retSQL[2];
                OutputParamsWithType += ddl_detl4.SelectedValue.ToString() + "_list text,";
            }

            var strJoinTableNames = TableName + " " + GetAbbreviation(TableName) + Environment.NewLine;

            for (var i = 0; i < dtRelations.Rows.Count; i++)
            {
                strJoinTableNames += " INNER JOIN " + dtRelations.Rows[i]["referencing_table"].ToString() +
                    " " + GetAbbreviation(dtRelations.Rows[i]["referencing_table"].ToString()) + " ON " + Environment.NewLine
                    + GetAbbreviation(dtRelations.Rows[i]["referencing_table"].ToString()) +
                    "." + dtRelations.Rows[i]["referenced_column"].ToString() + " = " +
                    GetAbbreviation(dtRelations.Rows[i]["referenced_table"].ToString()) +
                    "." + dtRelations.Rows[i]["referencing_column"].ToString() + Environment.NewLine;
            }

            string allcolumns = TableColumns;

            if (ListVariableWithRename.Length < 3)
            {
                allcolumns = allcolumns.Substring(0, allcolumns.Length - 3);
            }
            else
            {
                allcolumns += ListVariableWithRename.Substring(0, ListVariableWithRename.Length - 3);
            }

            String generated_text = filetext
            .Replace("{TableNameWithAleas}", strJoinTableNames + strWhereCondition)
            .Replace("{TableName}", TableName)
            .Replace("{ListVariables}", ListVariablesDeclare)
            .Replace("{DetlTableQueries}", inner_table_text)
            .Replace("{OutputParamsWithType}", OutputParamsWithType.Length > 3 ? OutputParamsWithType.Substring(0, OutputParamsWithType.Length - 3) : "")
            .Replace("{SelectColumns}", allcolumns)

            //.Replace("{DetlSelectColumns}", ListVariableWithRename.Length > 3 ? ListVariableWithRename.Substring(0, ListVariableWithRename.Length - 3) : "")

            .Replace("{FilterParams}", FilterParams.Length > 3 ? FilterParams + ",currnet_page bigint,page_size bigint" : "currnet_page bigint,page_size bigint");

            generated_text += Environment.NewLine + Environment.NewLine;

            string EntityLocation = txtDirctory.Text.Trim() + "\\" +tablename;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" +tablename + "_get_data_sp.sql");

            var table_view_name =tablename;

            EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            if (!Directory.Exists(txtDirctory.Text.Trim() + "\\" + table_view_name))
                Directory.CreateDirectory(txtDirctory.Text.Trim() + "\\" + table_view_name);

            string path = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\rpc_" + table_view_name + "_DTO.cs";

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            System.IO.StreamWriter file2 = System.IO.File.AppendText(path);

            file2.WriteLine(str_rpc_function.ToString());
            file2.Close();

            IDataAccessCode_get = "      Task<List<rpc_" + table_view_name + @"_DTO>> GetAllJoined_" + CamelCase(table_view_name) + "Async(" + (ServiceFunctionFilters.Length > 3 ? ServiceFunctionFilters + "Int64 currnet_page,Int64 page_size" : "Int64 currnet_page,Int64 page_size") + ");" + Environment.NewLine;

            DataAccessCode_get = @"     public async Task<List<rpc_" + table_view_name + @"_DTO>> GetAllJoined_" + CamelCase(table_view_name) + @"Async(" + (ServiceFunctionFilters.Length > 3 ? ServiceFunctionFilters + "Int64 currnet_page,Int64 page_size" : "Int64 currnet_page,Int64 page_size") + @")
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(""PGConnectionString"")))
                    {
                        connection.Open();

                        string query = $""SELECT * FROM proc_sp_get_data_" + table_view_name + @"( @currnet_page,@page_size)"";

                        var dataList = connection.Query<rpc_" + table_view_name + @"_DTO>(query,
                              new {
                                  currnet_page = currnet_page,
                                  page_size =  page_size }
                             ).AsList();

                        return dataList;

                    }
                
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }";



        }
        private void Generate_Update_SP(string tablename)
        {
            DataTable dtMain = GetTableDefinition(tablename);

            string TableName =tablename, ParamsForUpdate = "", InputParamsWithTypeOnlyText = "", InputParamsWithType = "", TableColumns = "", InputParams = "", NewIDVariables = "";

            NewIDVariables += "NEW_" + dtMain.Rows[0]["column_name"].ToString() + " BIGINT;" + Environment.NewLine;

            for (int i = 1; i < dtMain.Rows.Count; i++)
            {
                var colname = dtMain.Rows[i]["column_name"].ToString();
                var coltype = dtMain.Rows[i]["data_type"].ToString();

                TableColumns += colname;
                InputParams += "in_" + colname + (coltype.ToLower().Contains("json") ? "::text" : "");

                if (coltype.ToLower().Contains("text") || coltype.ToLower().Contains("json"))
                {
                    ParamsForUpdate += colname + " = " + "in_" + colname +
                    (coltype.ToLower().Contains("json") ? "::json" : "");

                    InputParamsWithTypeOnlyText += "in_" + colname + " text" + " DEFAULT NULL::text";

                    if (i != (dtMain.Rows.Count - 1))
                    {
                        InputParamsWithTypeOnlyText += "," + Environment.NewLine;
                        TableColumns += "," + Environment.NewLine;
                        InputParams += "," + Environment.NewLine;
                        ParamsForUpdate += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithTypeOnlyText += Environment.NewLine;
                        TableColumns += Environment.NewLine;
                        InputParams += Environment.NewLine;
                        ParamsForUpdate += Environment.NewLine;
                    }
                }
                else
                {
                    InputParamsWithType += "in_" + colname + " " + coltype+ " DEFAULT NULL";

                    ParamsForUpdate += colname + " = " + "in_" + colname;

                    if (i != (dtMain.Rows.Count - 1))
                    {
                        InputParamsWithType += "," + Environment.NewLine;
                        TableColumns += "," + Environment.NewLine;
                        InputParams += "," + Environment.NewLine;
                        ParamsForUpdate += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithType += Environment.NewLine;
                        TableColumns += Environment.NewLine;
                        InputParams += Environment.NewLine;
                        ParamsForUpdate += Environment.NewLine;
                    }
                }

            }

            var Update_Query = @"UPDATE " + TableName + @" SET " + ParamsForUpdate + Environment.NewLine + Environment.NewLine +

            "WHERE " + dtMain.Rows[0]["column_name"].ToString() + " = in_" + dtMain.Rows[0]["column_name"].ToString() + ";" +


            Environment.NewLine + Environment.NewLine + Environment.NewLine;

            var filetext = GetFileText("Templates//update_Function.txt");

            var inner_table_text = "";
            //////////////////generate det table
            if (ddl_detl1.SelectedIndex != 0 && ddl_json_column1.SelectedIndex != 0)
            {
                var retSQL = GetInnerUpdateSQL("in_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl1.SelectedValue.ToString(), ddl_json_column1.SelectedValue.ToString(), "t1");
                NewIDVariables += retSQL[0];

                inner_table_text += retSQL[1];
            }
            if (ddl_detl2.SelectedIndex != 0 && ddl_json_column2.SelectedIndex != 0)
            {
                var retSQL = GetInnerUpdateSQL("in_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl2.SelectedValue.ToString(), ddl_json_column2.SelectedValue.ToString(), "t2");
                NewIDVariables += retSQL[0];

                inner_table_text += retSQL[1];
            }
            if (ddl_detl3.SelectedIndex != 0 && ddl_json_column3.SelectedIndex != 0)
            {
                var retSQL = GetInnerUpdateSQL("in_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl3.SelectedValue.ToString(), ddl_json_column2.SelectedValue.ToString(), "t3");

                NewIDVariables += retSQL[0] + Environment.NewLine;

                inner_table_text += retSQL[1] + Environment.NewLine;
            }
            if (ddl_detl4.SelectedIndex != 0 && ddl_json_column4.SelectedIndex != 0)
            {
                var retSQL = GetInnerUpdateSQL("in_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl4.SelectedValue.ToString(), ddl_json_column3.SelectedValue.ToString(), "t4"); ;

                NewIDVariables += retSQL[0] + Environment.NewLine;

                inner_table_text += retSQL[1] + Environment.NewLine;
            }

            ////////////////////end det table

            String generated_text = filetext
            .Replace("{UpdateQuery}", Update_Query)
            .Replace("{TableName}", TableName)
            .Replace("{InputParamsWithType}", "in_" + dtMain.Rows[0]["column_name"].ToString() + " bigint," + Environment.NewLine + InputParamsWithType + InputParamsWithTypeOnlyText)
            .Replace("{NewIDVariables}", NewIDVariables)
            .Replace("{InputParams}", InputParams)
            .Replace("{UpdateQuery_Details}", inner_table_text)
            .Replace("{TableColumns}", TableColumns);


            var col_name_as_parameter = GetTableColumnNames(dtMain, "obj" + TableName);

            IDataAccessCode_update = @"     Task<bool> " + TableName + "_update_sp" + @"(" + TableName + "_DTO" + @" obj" + TableName + @");";

            DataAccessCode_update = @"      public async Task<bool> " + TableName + "_update_sp" + @"(" + TableName + "_DTO" + @" obj" + TableName + @")
                                     {
                                        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(""PGConnectionString"")))
                                        {
                                            connection.Open();

                                           using (var transaction = connection.BeginTransaction())
                                           {
                                               try
                                               {
                                                   var Command = new NpgsqlCommand(""" + TableName + "_update" + @""", connection);
			   
                                                   Command.CommandType = CommandType.StoredProcedure;

				                                    " + col_name_as_parameter + @"

                                                   Command.ExecuteNonQuery();

                                                   transaction.Commit();

                                                   return true;
                                               }
                                               catch (Exception ex)
                                               {
                                                   Console.WriteLine($""Error: {ex.Message}"");
                                                   transaction.Rollback();
                                                   return false;
                                               }
                                           }
                                        }
                                    }";

            generated_text += Environment.NewLine + Environment.NewLine;

            string EntityLocation = txtDirctory.Text.Trim() + "\\" +tablename;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" + "sp_update_" +tablename + ".sql");

            //CreateFileWithText(DataAccessCode, EntityLocation + "\\" + "RPC_update_" +tablename + ".cs");

        }

        public string GetTableColumnNamesFor_jsonb_agg(DataTable dt, string prefix)
        {
            var str = "";

            if (prefix != "")
                prefix = prefix + ".";

            for (int i = 1; i < dt.Rows.Count; i++)
            {

                str += "'" + dt.Rows[i][0].ToString() + "'," + prefix + dt.Rows[i][0].ToString();

                if (i != (dt.Rows.Count - 1))
                {
                    str += "," + Environment.NewLine;
                }
                else
                {
                    str += Environment.NewLine;
                }

            }

            return str;
        }

        public string GetTableColumnNames(DataTable dt, string prefix, string newforeignkey)
        {
            //objFilter.Add(""user_name_flter"", user_name_flter);
            var str = "";

            if (prefix != "")
                prefix = prefix + ".";

            for (int i = 1; i < dt.Rows.Count; i++)
            {

                if (i == 1 && newforeignkey.Length > 0)
                {
                    str += newforeignkey;
                }
                else
                {
                    str += prefix + dt.Rows[i][0];
                }

                if (i != (dt.Rows.Count - 1))
                {
                    str += "," + Environment.NewLine;
                }
                else
                {
                    str += Environment.NewLine;
                }

            }

            return str;
        }
      
        public string GetTableColumnNames(DataTable dt, string prefix)
        {
            //objFilter.Add(""user_name_flter"", user_name_flter);
            var str = "";

            if (prefix != "")
                prefix = prefix + ".";

            string InputParamsWithTypeOnlyText = "";

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                var colname = dt.Rows[i]["column_name"].ToString();
                var coltype = dt.Rows[i]["data_type"].ToString();
                
                InputParamsWithTypeOnlyText += "\t\tCommand.Parameters.AddWithValue(\"" + "in_" + dt.Rows[i][0] + "\"," + GetCSharpTypeForNGSQL(coltype) + ", " + prefix + dt.Rows[i][0] + "==null? DBNull.Value:" + prefix + dt.Rows[i][0] + ");" + Environment.NewLine;

            }

            return  InputParamsWithTypeOnlyText;
        }

        public string GetTableColumnNamesForUpdate(DataTable dt, string prefix)
        {
            var str = "";

            if (prefix != "")
                prefix = prefix + ".";

            string InputParamsWithTypeOnlyText = "";

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                var colname = dt.Rows[i]["column_name"].ToString();
                var coltype = dt.Rows[i]["data_type"].ToString();

                if (coltype.ToLower().Contains("json"))
                    InputParamsWithTypeOnlyText += "\t\t" + dt.Rows[i][0] + " = " + prefix + dt.Rows[i][0] + "::json";
                else
                    InputParamsWithTypeOnlyText += "\t\t" + dt.Rows[i][0] + " = " + prefix + dt.Rows[i][0];

                if (i != (dt.Rows.Count - 1))
                {
                    InputParamsWithTypeOnlyText += "," + Environment.NewLine;
                }
                else
                {
                    InputParamsWithTypeOnlyText += Environment.NewLine;

                }

            }


            return InputParamsWithTypeOnlyText;
        }

        private List<string>
            GetInnerSQL(string newforeignkey, string inner_table_name, string detl_column, string docno)
        {
            //var inner_table_name = ddl_detl1.SelectedValue.ToString();
            var ret = new List<string>
                ();

            var NewIDVariables = "";

            DataTable dt_detl1 = GetTableDefinition(inner_table_name);

            NewIDVariables += "row_" + inner_table_name + " RECORD;" + Environment.NewLine;

            NewIDVariables += "NEW_" + dt_detl1.Rows[0]["column_name"].ToString() + " BIGINT;" + Environment.NewLine;

            var col_names_with_prefix = GetTableColumnNames(dt_detl1, "inner_" + inner_table_name, "");
            var col_names = GetTableColumnNames(dt_detl1, "", "");
            var col_names_with_rowprefix = GetTableColumnNames(dt_detl1, "row_" + inner_table_name, newforeignkey);

            var inner_query = @" FOR " + "row_" + inner_table_name + @" IN (
                    SELECT
                    " + newforeignkey + "," + col_names_with_prefix + @"

                    FROM
                    LATERAL json_array_elements(" + "in_" + detl_column + @"::json) AS " + docno + @"(doc),
                    LATERAL json_populate_record(null::" + inner_table_name + @", " + docno + @".doc) AS " + "inner_" + inner_table_name + @"
                    )
                    LOOP
                   
                    INSERT INTO " + inner_table_name + @" (
                    " + col_names + @"

                    )
                    VALUES (
                    " + col_names_with_rowprefix + @"
                    )

                    RETURNING " + dt_detl1.Rows[0]["column_name"].ToString() + @" INTO " + "NEW_" + dt_detl1.Rows[0]["column_name"].ToString() + @";

                    END LOOP;";

            ret.Add(NewIDVariables);
            ret.Add(inner_query);
            return ret;
        }

        private List<string>
            GetInnerUpdateSQL(string newforeignkey, string inner_table_name, string detl_column, string docno)
        {
            //var inner_table_name = ddl_detl1.SelectedValue.ToString();
            var ret = new List<string>
                ();

            var NewIDVariables = "";

            DataTable dt_detl1 = GetTableDefinition(inner_table_name);

            NewIDVariables += "row_" + inner_table_name + " RECORD;" + Environment.NewLine;

            NewIDVariables += "NEW_" + dt_detl1.Rows[0]["column_name"].ToString() + " BIGINT;" + Environment.NewLine;

            var col_names_with_prefix = GetTableColumnNames(dt_detl1, "inner_" + inner_table_name, "");
            var col_names = GetTableColumnNames(dt_detl1, "", "");
            var col_names_with_rowprefix = GetTableColumnNames(dt_detl1, "row_" + inner_table_name, newforeignkey);
            var col_names_with_prefix_for_update = GetTableColumnNamesForUpdate(dt_detl1, "row_" + inner_table_name);

            var inner_query = @" FOR " + "row_" + inner_table_name + @" IN (
                            SELECT
                            " + "inner_" + inner_table_name + "." + dt_detl1.Rows[0]["column_name"].ToString() + "," + Environment.NewLine
            + col_names_with_prefix + @"

                            FROM
                            LATERAL json_array_elements(" + "in_" + detl_column + @"::json) AS " + docno + @"(doc),
                            LATERAL json_populate_record(null::" + inner_table_name + @", " + docno + @".doc) AS " + "inner_" + inner_table_name + @"
                            )
                            LOOP
                            CASE

                            WHEN row_" + inner_table_name + @".current_state=1 THEN

                            INSERT INTO " + inner_table_name + @" (
                            " + col_names + @"

                            )
                            VALUES (
                            " + col_names_with_rowprefix + @"
                            )

                            RETURNING " + dt_detl1.Rows[0]["column_name"].ToString() + @" INTO " + "NEW_" + dt_detl1.Rows[0]["column_name"].ToString() + ";" + @"

                            WHEN row_" + inner_table_name + @".current_state=2 THEN

                            UPDATE  " + inner_table_name + @"
                            SET " + col_names_with_prefix_for_update + @"
                            WHERE " + dt_detl1.Rows[0]["column_name"].ToString() + "= " + @"
                            row_" + inner_table_name + "." + dt_detl1.Rows[0]["column_name"].ToString() + @";

                            WHEN row_" + inner_table_name + @".current_state=3 THEN
                            DELETE  FROM " + inner_table_name + @"
                            WHERE " + dt_detl1.Rows[0]["column_name"].ToString() + "= " + @"
                            row_" + inner_table_name + "." + dt_detl1.Rows[0]["column_name"].ToString() + @";
                            END CASE;


                            END LOOP;";

            ret.Add(NewIDVariables);
            ret.Add(inner_query);
            return ret;
        }

        private List<string>
            GetSelectJSONInnerSQL(string inner_table_name)
        {
            var ret = new List<string>
                ();

            var table_aleas = GetAbbreviation(inner_table_name);

            DataTable dt_detl1 = GetTableDefinition(inner_table_name);

            var col_names_with_prefix = GetTableColumnNamesFor_jsonb_agg(dt_detl1, table_aleas);

            var listVariablesdeclare = "obj_list_" + dt_detl1.Rows[0]["column_name"].ToString() + " text;" + Environment.NewLine;

            var listVariablesdeclare_with_rename = "obj_list_" + dt_detl1.Rows[0]["column_name"].ToString() + " " + inner_table_name + "_list," + Environment.NewLine;

            var inner_query = @"  select jsonb_agg(
                                    jsonb_build_object(
                                    " + col_names_with_prefix + @"
                                    )
                                    )
                                    INTO  obj_list_" + dt_detl1.Rows[0]["column_name"].ToString() + @"
                                    from " + inner_table_name + " " + table_aleas + ";";

            ret.Add(listVariablesdeclare);
            ret.Add(listVariablesdeclare_with_rename);
            ret.Add(inner_query);
            return ret;
        }

        private void Generate_Insert_SP(string tablename)
        {
            DataTable dtMain = GetTableDefinition(tablename);

            string TableName = tablename, InputParamsWithTypeOnlyText = "", InputParamsWithType = "", TableColumns = "", InputParams = "", NewIDVariables = "";

            NewIDVariables += "NEW_" + dtMain.Rows[0]["column_name"].ToString() + " BIGINT;" + Environment.NewLine;

            for (int i = 1; i < dtMain.Rows.Count; i++)
            {
                var colname = dtMain.Rows[i]["column_name"].ToString();
                var coltype = dtMain.Rows[i]["data_type"].ToString();

                TableColumns += colname;
                InputParams += "in_" + colname + (coltype.ToLower().Contains("json") ? "::json" : "");

                if (coltype.ToLower().Contains("text") || coltype.ToLower().Contains("json"))
                {

                    InputParamsWithTypeOnlyText += "IN in_" + colname + " text" + " DEFAULT NULL::text";

                    if (i != (dtMain.Rows.Count - 1))
                    {
                        InputParamsWithTypeOnlyText += "," + Environment.NewLine;
                        TableColumns += "," + Environment.NewLine;
                        InputParams += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithTypeOnlyText += Environment.NewLine;
                        TableColumns += Environment.NewLine;
                        InputParams += Environment.NewLine;
                    }
                }
                else
                {
                    InputParamsWithType += "IN in_" + colname + " " + coltype+ " DEFAULT NULL";

                    if (i != (dtMain.Rows.Count - 1))
                    {
                        InputParamsWithType += "," + Environment.NewLine;
                        TableColumns += "," + Environment.NewLine;
                        InputParams += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithType += Environment.NewLine;
                        TableColumns += Environment.NewLine;
                        InputParams += Environment.NewLine;
                    }
                }
            }

            var Insert_Query = " INSERT INTO " + TableName + " (" + TableColumns + ")" + Environment.NewLine +
            "\tVALUES (" + InputParams + ")" + Environment.NewLine + Environment.NewLine +
            "RETURNING " + dtMain.Rows[0]["column_name"].ToString() + " INTO NEW_" + dtMain.Rows[0]["column_name"].ToString() + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine; ;

            var filetext = GetFileText("Templates//insert_Function.txt");

            var inner_table_text = "";
            ////////////////generate det table
            if (ddl_detl1.SelectedIndex != 0 && ddl_json_column1.SelectedIndex != 0)
            {
                var retSQL = GetInnerSQL("NEW_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl1.SelectedValue.ToString(), ddl_json_column1.SelectedValue.ToString(), "t1");
                NewIDVariables += retSQL[0];

                inner_table_text += retSQL[1];
            }
            if (ddl_detl2.SelectedIndex != 0 && ddl_json_column2.SelectedIndex != 0)
            {
                var retSQL = GetInnerSQL("NEW_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl2.SelectedValue.ToString(), ddl_json_column2.SelectedValue.ToString(), "t2");
                NewIDVariables += retSQL[0];

                inner_table_text += retSQL[1];
            }
            if (ddl_detl3.SelectedIndex != 0 && ddl_json_column3.SelectedIndex != 0)
            {
                var retSQL = GetInnerSQL("NEW_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl3.SelectedValue.ToString(), ddl_json_column3.SelectedValue.ToString(), "t3");

                NewIDVariables += retSQL[0] + Environment.NewLine;

                inner_table_text += retSQL[1] + Environment.NewLine;
            }
            if (ddl_detl4.SelectedIndex != 0 && ddl_json_column4.SelectedIndex != 0)
            {
                var retSQL = GetInnerSQL("NEW_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl4.SelectedValue.ToString(), ddl_json_column4.SelectedValue.ToString(), "t4"); ;

                NewIDVariables += retSQL[0] + Environment.NewLine;

                inner_table_text += retSQL[1] + Environment.NewLine;
            }
            if (ddl_detl5.SelectedIndex != 0 && ddl_json_column5.SelectedIndex != 0)
            {
                var retSQL = GetInnerSQL("NEW_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl5.SelectedValue.ToString(), ddl_json_column5.SelectedValue.ToString(), "t5"); ;

                NewIDVariables += retSQL[0] + Environment.NewLine;

                inner_table_text += retSQL[1] + Environment.NewLine;
            }

            String generated_text = filetext
            .Replace("{InsertQuery}", Insert_Query)
            .Replace("{TableName}", TableName)
            .Replace("{InputParamsWithType}", InputParamsWithType + InputParamsWithTypeOnlyText)
            .Replace("{NewIDVariables}", NewIDVariables)
            .Replace("{InputParams}", InputParams)
            .Replace("{InsertQuery_Details}", inner_table_text)
            .Replace("{TableColumns}", TableColumns);

            var col_name_as_parameter = GetTableColumnNames(dtMain, "obj" + TableName);

            IDataAccessCode_insert = @"     Task<bool> " + TableName + "_insert_sp" + @"(" + TableName + "_DTO" + @" obj" + TableName + @");";
            
            DataAccessCode_insert = @"      public async Task<bool> " + TableName + "_insert_sp" + @"(" + TableName + "_DTO" + @" obj" + TableName + @")
                                            {
                                             using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(""PGConnectionString"")))
                                                {
                                                connection.Open();
                                                
                                                using (var transaction = connection.BeginTransaction())
                                               {
                                                   try
                                                   {
                                                       var Command = new NpgsqlCommand(""" + TableName + "_insert" + @""", connection);
			   
                                                       Command.CommandType = CommandType.StoredProcedure;

				                                        " + col_name_as_parameter + @"

                                                       Command.ExecuteNonQuery();

                                                       transaction.Commit();

                                                       return true;
                                                   }
                                                   catch (Exception ex)
                                                   {
                                                       Console.WriteLine($""Error: {ex.Message}"");
                                                       transaction.Rollback();
                                                       return false;
                                                   }
                                               }
                                             }
                                          }";

            generated_text += Environment.NewLine + Environment.NewLine;

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + tablename;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" + "sp_insert_" + tablename + ".sql");

        }

        public DataTable GetTableDefinition(string tablename)
        {
            DataTable dt = new DataTable();
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {

                string query = @"SELECT  col.column_name,col.data_type,col.is_nullable,col.is_identity,
                                            col.ordinal_position,true as ChkSelect FROM information_schema.columns col
                                            WHERE table_name='" + tablename + "' order by col.ordinal_position";

                connection.Open();

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                {
                    dataAdapter.Fill(dt);
                }
            }

            return dt;
        }

        private void btnGenFuncBackEnd_Click(object sender, EventArgs e)
        {
            if (cmb_function.SelectedValue.ToString() != "All Functions")
            {
                var paramList = BuildRPCFunctionEntityNew(cmb_function.SelectedValue.ToString());
                BuildRPCFunctionInterface(paramList); //Interface
                BuildRPCFunctionService(paramList);
                BuildPGSQLunctionService(paramList, false);
            }
            else
            {
                for (int i = 0; i < dtAllFunctons.Rows.Count; i++)
                {
                    var paramList = BuildRPCFunctionEntityNew(dtAllFunctons.Rows[i][0].ToString());
                    BuildRPCFunctionInterface(paramList); //Interface
                    BuildRPCFunctionService(paramList);
                    BuildPGSQLunctionService(paramList, true);
                }
            }
        }


        private DataTable GetTableColumns(string tbl)
        {
            DataTable dtColumns = new DataTable();
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {
                string query = @"SELECT  col.column_name,col.data_type, false as ChkSelect FROM information_schema.columns col
                    WHERE table_name='" + tbl + "' " +
                    "and col.data_type!='json' " +
                    "and col.column_name not in ('added_by','updated_by','date_added','date_updated')  order by col.ordinal_position";

                connection.Open();

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                {
                    dataAdapter.Fill(dtColumns);
                }
            }

            return dtColumns;

        }


        private void btnAddToSelect_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < grid_table_output.Rows.Count - 1; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = grid_table_output.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                bool? isChecked = (bool?)checkBoxCell.Value;

                if (isChecked.HasValue && isChecked == true)
                {
                    DataView dv = dtSelectCols.Copy().DefaultView;
                    dv.RowFilter = "column_name='" + grid_table_output.Rows[i].Cells[0].Value.ToString() + "'";

                    DataView dv2 = dtColumns.Copy().DefaultView;
                    dv2.RowFilter = "column_name='" + grid_table_output.Rows[i].Cells[0].Value.ToString() + "'";

                    if (dv.ToTable().Rows.Count == 0 && dv2.ToTable().Rows.Count == 0)
                        dtSelectCols.Rows.Add(
                            ddlOutputTables.SelectedValue,
                            grid_table_output.Rows[i].Cells[0].Value,
                            grid_table_output.Rows[i].Cells[1].Value);
                }
            }

            grid_allselect_columns.AutoGenerateColumns = true;
            grid_allselect_columns.DataSource = dtSelectCols.Copy();
            grid_allselect_columns.Columns[0].Width = 170;
            grid_allselect_columns.Columns[1].Width = 170;
            grid_allselect_columns.Columns[2].Width = 100;

            ddlOutputTables.SelectedIndex = 0;
            chkAllOutput.Checked = false;
            grid_table_output.DataSource = new DataTable();

        }

        private void ddljointo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddljointo.SelectedIndex > 0)
            {
                var dt = GetTableColumns(ddljointo.SelectedValue.ToString()).Copy();

                ddljointocolumn.DataSource = dt.Copy();
                ddljointocolumn.ValueMember = "column_name";
                ddljointocolumn.DisplayMember = "column_name";
            }
        }

        private void btnAddJoin_Click(object sender, EventArgs e)
        {

            DataRow newRow = dtRelations.NewRow();
            newRow["referencing_table"] = ddljointo.SelectedValue;
            newRow["referencing_column"] = ddljoinwithcolumn.SelectedValue;
            newRow["referenced_table"] = ddljoinwith.SelectedValue;
            newRow["referenced_column"] = ddljointocolumn.SelectedValue;

            dtRelations.Rows.Add(newRow);

            DataTable frndata2 = dtRelations.Copy();

            newRow = frndata2.NewRow();
            newRow["referencing_table"] = "select";
            frndata2.Rows.InsertAt(newRow, 0);

            ddlOutputTables.DataSource = frndata2;
            ddlOutputTables.DisplayMember = "referencing_table";
            ddlOutputTables.ValueMember = "referencing_table";

            ddljoinwith.SelectedIndex = 0;
            ddljoinwithcolumn.SelectedIndex = 0;
            ddljointo.SelectedIndex = 0;
            ddljointocolumn.SelectedIndex = 0;

        }

        private void ddlOutputTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOutputTables.SelectedIndex > 0)
            {
                var dt = GetTableColumns(ddlOutputTables.SelectedValue.ToString()).Copy();

                grid_table_output.DataSource = dt;
                grid_table_output.Columns[0].Width = 110;
                grid_table_output.Columns[1].Width = 45;
                grid_table_output.Columns[2].Width = 45;

            }
        }

        private void ddljoinwith_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddljoinwith.SelectedIndex > 0)
            {
                var dt = GetTableColumns(ddljoinwith.SelectedValue.ToString()).Copy();

                ddljoinwithcolumn.DataSource = dt.Copy();
                ddljoinwithcolumn.ValueMember = "column_name";
                ddljoinwithcolumn.DisplayMember = "column_name";
            }
        }

        private void chkAllOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllOutput.Checked)
            {
                foreach (DataGridViewRow row in grid_table_output.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells[2] as DataGridViewCheckBoxCell;
                    checkBoxCell.Value = true; // Check the checkbox
                }
            }
            else
            {
                foreach (DataGridViewRow row in grid_table_output.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells[2] as DataGridViewCheckBoxCell;
                    checkBoxCell.Value = false; // Check the checkbox
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlTables.SelectedValue.ToString() != "All_Tables")
            {
                try
                {
                    Generate_Insert_SP(ddlTables.SelectedValue.ToString());
                    Generate_Update_SP(ddlTables.SelectedValue.ToString());
                    Generate_GetData_SP(ddlTables.SelectedValue.ToString());
                //  Generate_Insert_Multi_Level_SP(ddlTables.SelectedValue.ToString());

                    if (dtColumns.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            object cellValue = dataGridView1.Rows[i].Cells[4].Value;

                            if (cellValue != null)
                            {
                                dtColumns.Rows[i][4] = cellValue;
                            }
                        }

                        BuildTableFiles(ddlTables.SelectedValue.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                for (int rr = 2; rr < dataTable.Rows.Count; rr++)
                {
                    try
                    {
                        ddlTables.SelectedValue = dataTable.Rows[rr][0].ToString();
                        dtColumns = new DataTable();

                        btnLoadColumn(sender, e);

                        Generate_Insert_SP(ddlTables.SelectedValue.ToString()) ;
                        Generate_Update_SP(ddlTables.SelectedValue.ToString());
                        Generate_GetData_SP(ddlTables.SelectedValue.ToString());

                        if (dtColumns.Rows.Count > 0)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                object cellValue = dataGridView1.Rows[i].Cells[4].Value;

                                if (cellValue != null)
                                {
                                    dtColumns.Rows[i][4] = cellValue;
                                }
                            }

                            BuildTableFiles(ddlTables.SelectedValue.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            MessageBox.Show("Done");
        }

        private void Generate_Insert_Multi_Level_SP(string tablename)
        {
            DataTable dtMain = GetTableDefinition(tablename);

            string TableName = tablename, InputParamsWithTypeOnlyText = "", InputParamsWithType = "", TableColumns = "", InputParams = "", NewIDVariables = "";

            NewIDVariables += "NEW_" + dtMain.Rows[0]["column_name"].ToString() + " BIGINT;" + Environment.NewLine;

            for (int i = 1; i < dtMain.Rows.Count; i++)
            {
                var colname = dtMain.Rows[i]["column_name"].ToString();
                var coltype = dtMain.Rows[i]["data_type"].ToString();

                TableColumns += colname;
                InputParams += "in_" + colname + (coltype.ToLower().Contains("json") ? "::json" : "");

                if (coltype.ToLower().Contains("text") || coltype.ToLower().Contains("json"))
                {

                    InputParamsWithTypeOnlyText += "IN in_" + colname + " text" + " DEFAULT NULL::text";

                    if (i != (dtMain.Rows.Count - 1))
                    {
                        InputParamsWithTypeOnlyText += "," + Environment.NewLine;
                        TableColumns += "," + Environment.NewLine;
                        InputParams += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithTypeOnlyText += Environment.NewLine;
                        TableColumns += Environment.NewLine;
                        InputParams += Environment.NewLine;
                    }
                }
                else
                {
                    InputParamsWithType += "IN in_" + colname + " " + coltype + " DEFAULT NULL";

                    if (i != (dtMain.Rows.Count - 1))
                    {
                        InputParamsWithType += "," + Environment.NewLine;
                        TableColumns += "," + Environment.NewLine;
                        InputParams += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithType += Environment.NewLine;
                        TableColumns += Environment.NewLine;
                        InputParams += Environment.NewLine;
                    }
                }
            }

            var Insert_Query = " INSERT INTO " + TableName + " (" + TableColumns + ")" + Environment.NewLine +
            "\tVALUES (" + InputParams + ")" + Environment.NewLine + Environment.NewLine +
            "RETURNING " + dtMain.Rows[0]["column_name"].ToString() + " INTO NEW_" + dtMain.Rows[0]["column_name"].ToString() + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine; ;

            var filetext = GetFileText("Templates//insert_Function.txt");

            var inner_table_text = "";
            ////////////////generate det table
            if (ddl_detl1.SelectedIndex != 0 && ddl_json_column1.SelectedIndex != 0)
            {
                var retSQL = GetInnerSQL("NEW_" + dtMain.Rows[0]["column_name"].ToString(), ddl_detl1.SelectedValue.ToString(), ddl_json_column1.SelectedValue.ToString(), "t1");
                NewIDVariables += retSQL[0];

                inner_table_text += retSQL[1];
            }
            
            String generated_text = filetext
            .Replace("{InsertQuery}", Insert_Query)
            .Replace("{TableName}", TableName)
            .Replace("{InputParamsWithType}", InputParamsWithType + InputParamsWithTypeOnlyText)
            .Replace("{NewIDVariables}", NewIDVariables)
            .Replace("{InputParams}", InputParams)
            .Replace("{InsertQuery_Details}", inner_table_text)
            .Replace("{TableColumns}", TableColumns);

            var col_name_as_parameter = GetTableColumnNames(dtMain, "obj" + TableName);

            IDataAccessCode_insert = @"     Task<bool> " + TableName + "_insert_sp" + @"(" + TableName + "_DTO" + @" obj" + TableName + @");";

            DataAccessCode_insert = @"      public async Task<bool> " + TableName + "_insert_sp" + @"(" + TableName + "_DTO" + @" obj" + TableName + @")
                                            {
                                             using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(""PGConnectionString"")))
                                                {
                                                connection.Open();
                                                
                                                using (var transaction = connection.BeginTransaction())
                                               {
                                                   try
                                                   {
                                                       var Command = new NpgsqlCommand(""" + TableName + "_insert" + @""", connection);
			   
                                                       Command.CommandType = CommandType.StoredProcedure;

				                                        " + col_name_as_parameter + @"

                                                       Command.ExecuteNonQuery();

                                                       transaction.Commit();

                                                       return true;
                                                   }
                                                   catch (Exception ex)
                                                   {
                                                       Console.WriteLine($""Error: {ex.Message}"");
                                                       transaction.Rollback();
                                                       return false;
                                                   }
                                               }
                                             }
                                          }";

            generated_text += Environment.NewLine + Environment.NewLine;

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + tablename;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" + "sp_insert_" + tablename + ".sql");

        }

        private void BuildTableController(string table_view_name)
        {

            var filetext = GetFileText("Templates//Controller.txt");

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name)
                .Replace("{PrimaryColumn}", dtColumns.Rows[0][0].ToString());

            if (!string.IsNullOrEmpty(cmbParentMenu.Text))
            {
                generated_text = generated_text.Replace("{FolderName}", cmbParentMenu.Text + "/");

            }

            var AllColumns = ""; var alljoinedcols = "";

            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {

                string column_name = dtColumns.Rows[i][0].ToString();
                string data_type = dtColumns.Rows[i][2].ToString();
                string is_nullable = dtColumns.Rows[i][1].ToString().ToLower();
                string is_identity = dtColumns.Rows[i][3].ToString().ToLower();

                AllColumns += "\t\t\tt." + column_name + ",\n";

            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = dataGridView1.Rows[i].Cells[4] as DataGridViewCheckBoxCell;
                bool? isChecked = (bool?)checkBoxCell.Value;

                if (isChecked.HasValue && isChecked == true)
                {
                    var colname = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    var coltype = dataGridView1.Rows[i].Cells[2].Value.ToString();

                    alljoinedcols += "\t\t\tt." + colname + ",\n";
                }


            }

            for (int i = 0; i < grid_allselect_columns.Rows.Count - 1; i++)
            {
                if (grid_allselect_columns.Rows[i].Cells[1].Value != null)
                {
                    var colname = grid_allselect_columns.Rows[i].Cells[1].Value.ToString();
                    var coltype = grid_allselect_columns.Rows[i].Cells[2].Value.ToString();

                    alljoinedcols += "\t\t\tt." + colname + ",\n";
                }

            }

            generated_text = generated_text
                .Replace("{AllColumns}", AllColumns)
                .Replace("{AllJoinedColumns}", alljoinedcols);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "Controller.cs");

        }

        private void BuildTableLandingView(string table_view_name)
        {
            //Landing View

            var filetext = GetFileText("Templates//View/LandingView.txt");

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name)
                .Replace("{PrimaryColumn}", dtColumns.Rows[0][0].ToString());

            var AllInsertUpdateColumns = ""; var AllColumnHeaders = ""; var AllDataTableColumns = "";

            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                if (dtColumns.Rows[i][4].ToString().ToLower() == "true")
                {
                    string column_name = dtColumns.Rows[i][0].ToString();
                    string data_type = dtColumns.Rows[i][2].ToString();
                    string is_nullable = dtColumns.Rows[i][1].ToString().ToLower();
                    string is_identity = dtColumns.Rows[i][3].ToString().ToLower();

                    if (i != 0)
                    {
                        AllColumnHeaders += "\t\t<th>" + column_name.Replace("_", " ") + "</th>\n";
                        AllDataTableColumns += "\t\t{ \"data\": \"" + column_name + "\", \"name\": \"" + column_name + "\", \"autoWidth\": true },\n";
                    }

                    if (data_type.Contains("int"))
                    {
                        AllInsertUpdateColumns += "\t\t" + column_name + ": check_value($('#modalcontent #" + column_name + "').val()),\n";
                    }
                    else
                    {
                        AllInsertUpdateColumns += "\t\t" + column_name + ": $('#modalcontent #" + column_name + "').val(),\n";
                    }
                }
            }

            for (int i = 0; i < grid_allselect_columns.Rows.Count; i++)
            {
                if (grid_allselect_columns.Rows[i].Cells[1].Value != null)
                {
                    var colname = grid_allselect_columns.Rows[i].Cells[1].Value.ToString();
                    var coltype = grid_allselect_columns.Rows[i].Cells[2].Value.ToString();

                    AllColumnHeaders += "\t\t<th>" + colname.Replace("_", " ") + "</th>\n";
                    AllDataTableColumns += "\t\t{ \"data\": \"" + colname + "\", \"name\": \"" + colname + "\", \"autoWidth\": true },\n";

                }

            }

            var det_ins_ups_all = "";

            for (int ind = 1; ind < ddlOutputTables.Items.Count; ind++)
            {
                DataRowView rowView = (DataRowView)ddlOutputTables.Items[ind];

                // Assuming the column you want to retrieve the value from is named "ColumnName"
                object value = rowView[0];

                var det_tblname = CamelCase(value.ToString());

                var dtcols = GetTableDefinition(value.ToString());

                var det_ins_ups = "";

                for (int i = 0; i < dtcols.Rows.Count; i++)
                {

                    string column_name = dtcols.Rows[i][0].ToString();
                    string data_type = dtcols.Rows[i][1].ToString();

                    if (data_type.Contains("int"))
                    {
                        det_ins_ups += "\t\t" + column_name + ": check_value($('#" + column_name + "').val()),\n";
                    }
                    else
                    {
                        det_ins_ups += "\t\t" + column_name + ": $('#" + column_name + "').val(),\n";
                    }

                }

                det_ins_ups_all += "var obj_" + det_tblname + " = {\r\n                " + det_ins_ups + "\r\n            }\n\n";

            }

            string popupwidth = (ddlSingleDoubleColumn.SelectedValue == "Single Column" || ddlSingleDoubleColumn.SelectedValue == "") ? "30" : "75";

            generated_text = generated_text.Replace("{AllDataTableHeaders}", AllColumnHeaders)
                .Replace("{AllDataTableColumns}", AllDataTableColumns)
                .Replace("{AllInsertUpdateColumns}", AllInsertUpdateColumns)
                .Replace("{popup_width}", popupwidth)
                .Replace("{DetailTableColumns}", det_ins_ups_all);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\" + servicename;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "Landing.cshtml");

            //end Landing View
        }

        private void BuildTableCreateView(string table_view_name, string viewfilename, string filename, bool isReadonly = false)
        {
            //Landing View

            var strReadonly = "";

            if (isReadonly == true)
            {
                strReadonly = "disabled";
            }

            var filetext = GetFileText("Templates//View/" + viewfilename);

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name)
                .Replace("{PrimaryColumn}", dtColumns.Rows[0][0].ToString());

            var AllColumns = ""; ;

            if (ddlSingleDoubleColumn.SelectedItem == "Single Column" || ddlSingleDoubleColumn.SelectedItem == null)
            {
                for (int i = 1; i < dtColumns.Rows.Count; i++)
                {
                    if (dtColumns.Rows[i]["chkselect"].ToString().ToLower() == "true")
                    {
                        string column_name = dtColumns.Rows[i][0].ToString();
                        string data_type = dtColumns.Rows[i][2].ToString().ToLower();
                        string is_nullable = dtColumns.Rows[i][1].ToString().ToLower();
                        string is_identity = dtColumns.Rows[i][3].ToString().ToLower();

                        if (column_name == "added_by" || column_name == "updated_by" || column_name == "date_added" || column_name == "date_updated")
                            continue;

                        if (data_type == "boolean")
                        {
                            AllColumns += "\t\t<div class=\"row\" >\n" +
                                              "\t\t\t<div class=\"col-md-6\">\n" +
                                                         "\t\t\t\t<div class=\"form-group\">\n" +
                                                         "\t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name) + "</label>\n" +
                                                         "\t\t\t\t</div>\n" +
                                                    "\t\t\t</div>\n" +
                                                    "\t\t\t<div class=\"col-md-3\">\n" +
                                                        "\t\t\t\t<div class=\"form-group\">\n" +
                                                            "\t\t\t\t\tHtml.RadioButtonFor(m => m." + column_name + ",\"true\", new { disabled = true })  <label for=\"html\">Yes</label>\n" +
                                                            "\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                        "\t\t\t\t</div>\n" +
                                                    "\t\t\t</div>\n" +
                                                    "\t\t\t<div class=\"col-md-3\">\n" +
                                                    "\t\t\t<div class=\"form-group\">\n" +
                                                             "\t\t\t\tHtml.RadioButtonFor(m => m." + column_name + ",\"false\", new { disabled = true }) <label for=\"html\">No</label>\n" +
                                                             "\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                    "\t\t\t</div>\n" +
                                                "\t\t\t</div>\n" +
                                              "\t\t</div>\n\n";
                        }
                        else
                        {
                            AllColumns += "\t\t<div class=\"row\" >\n" +
                                                    "\t\t\t<div class=\"col-md-12\">\n" +
                                                         "\t\t\t\t<div class=\"form-group\">\n" +
                                                         "   \t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name) + "</label>\n" +
                                                         "   \t\t\t\t\t<input style=\"width:100%\" " + strReadonly + "  asp-for=\"" + column_name + "\" type=\"" + GetColumnHtmlType(data_type) + "\" class=\"border-#d1cccc-200 form-control\" />\n" +
                                                         "    \t\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                         "\t\t\t\t</div>\n" +
                                                    "\t\t\t</div>\n" +
                                              "\t\t</div>\n\n";
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < dtColumns.Rows.Count; i += 2)
                {
                    string column_name_2 = "", data_type_2 = "";

                    string column_name = dtColumns.Rows[i][0].ToString();

                    if (column_name == "added_by" || column_name == "updated_by" || column_name == "date_added" || column_name == "date_updated")
                        continue;

                    string data_type = dtColumns.Rows[i][2].ToString();

                    if ((i + 1) < dtColumns.Rows.Count)
                    {
                        column_name_2 = dtColumns.Rows[i + 1][0].ToString();
                        data_type_2 = dtColumns.Rows[i + 1][2].ToString();
                    }

                    AllColumns += "\t\t<div class=\"row\" >\n" +
                                             "\t\t\t<div class=\"col-md-6\">\n" +
                                                      "\t\t\t\t<div class=\"form-group\">\n" +
                                                            "\t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name) + "</label>\n" +
                                                            "\t\t\t\t\t <input style=\"width:100%\"  " + strReadonly + "  asp-for=\"" + column_name + "\" type=\"" + GetColumnHtmlType(data_type) + "\" class=\"border-#d1cccc-200 form-control \" />\n" +
                                                            "\t\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                      "\t\t\t\t</div>\n" +
                                             "\t\t\t</div>\n" +
                                             (!string.IsNullOrEmpty(column_name_2) ? ("\t\t\t<div class=\"col-md-6\">\n" +
                                                  "\t\t\t\t<div class=\"form-group\">\n" +
                                                        "\t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name_2) + "</label>\n" +
                                                        "\t\t\t\t\t<input style=\"width:100%\" " + strReadonly + "   asp-for=\"" + column_name_2 + "\" type=\"" + GetColumnHtmlType(data_type_2) + "\" class=\"border-#d1cccc-200 form-control \" />\n" +
                                                  "\t\t\t\t<span asp-validation-for=\"" + column_name_2 + "\" class=\"text-danger\"></span>\n" +
                                                  "\t\t\t</div>\n") : "\n") +
                                             "\t\t</div>\n" +
                                       "\t\t</div>\n\n";

                }
            }

            generated_text = generated_text.Replace("{AllColumns}", AllColumns);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name + "\\" + servicename;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" + "_" + servicename + filename);

            //end Landing View
        }

        private void BuildTableCreateDetlView(DataTable dt, string maintablename, string table_view_name, string viewfilename, string filename, bool isReadonly = false)
        {
            //Landing View

            var strReadonly = "";

            if (isReadonly == true)
            {
                strReadonly = "disabled";
            }

            var filetext = GetFileText("Templates//View/" + viewfilename);

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name)
                .Replace("{PrimaryColumn}", dt.Rows[0][0].ToString());

            var AllColumns = ""; ;

            if (ddlSingleDoubleColumn.SelectedItem == "Single Column" || ddlSingleDoubleColumn.SelectedItem == null)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString().ToLower() == "true")
                    {
                        string column_name = dt.Rows[i][0].ToString();
                        string data_type = dt.Rows[i][1].ToString().ToLower();
                        string is_nullable = dt.Rows[i][1].ToString().ToLower();
                        string is_identity = dt.Rows[i][3].ToString().ToLower();

                        if (column_name == "added_by" || column_name == "updated_by" || column_name == "date_added" || column_name == "date_updated")
                            continue;

                        if (data_type == "boolean")
                        {
                            AllColumns += "\t\t<div class=\"row\" >\n" +
                                              "\t\t\t<div class=\"col-md-6\">\n" +
                                                         "\t\t\t\t<div class=\"form-group\">\n" +
                                                         "\t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name) + "</label>\n" +
                                                         "\t\t\t\t</div>\n" +
                                                    "\t\t\t</div>\n" +
                                                    "\t\t\t<div class=\"col-md-3\">\n" +
                                                        "\t\t\t\t<div class=\"form-group\">\n" +
                                                            "\t\t\t\t\tHtml.RadioButtonFor(m => m." + column_name + ",\"true\", new { disabled = true })  <label for=\"html\">Yes</label>\n" +
                                                            "\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                        "\t\t\t\t</div>\n" +
                                                    "\t\t\t</div>\n" +
                                                    "\t\t\t<div class=\"col-md-3\">\n" +
                                                    "\t\t\t<div class=\"form-group\">\n" +
                                                             "\t\t\t\tHtml.RadioButtonFor(m => m." + column_name + ",\"false\", new { disabled = true }) <label for=\"html\">No</label>\n" +
                                                             "\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                    "\t\t\t</div>\n" +
                                                "\t\t\t</div>\n" +
                                              "\t\t</div>\n\n";
                        }
                        else
                        {
                            AllColumns += "\t\t<div class=\"row\" >\n" +
                                                    "\t\t\t<div class=\"col-md!=02\">\n" +
                                                         "\t\t\t\t<div class=\"form-group\">\n" +
                                                         "   \t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name) + "</label>\n" +
                                                         "   \t\t\t\t\t<input style=\"width:100%\" " + strReadonly + "  asp-for=\"" + column_name + "\" type=\"" + GetColumnHtmlType(data_type) + "\" class=\"border-#d1cccc-200 form-control\" />\n" +
                                                         "    \t\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                         "\t\t\t\t</div>\n" +
                                                    "\t\t\t</div>\n" +
                                              "\t\t</div>\n\n";
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < dt.Rows.Count; i += 2)
                {
                    string column_name_2 = "", data_type_2 = "";

                    string column_name = dt.Rows[i][0].ToString();

                    if (column_name == "added_by" || column_name == "updated_by" || column_name == "date_added" || column_name == "date_updated")
                        continue;

                    string data_type = dt.Rows[i][2].ToString();

                    if ((i + 1) < dt.Rows.Count)
                    {
                        column_name_2 = dt.Rows[i + 1][0].ToString();
                        data_type_2 = dt.Rows[i + 1][1].ToString();
                    }

                    AllColumns += "\t\t<div class=\"row\" >\n" +
                                             "\t\t\t<div class=\"col-md-6\">\n" +
                                                      "\t\t\t\t<div class=\"form-group\">\n" +
                                                            "\t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name) + "</label>\n" +
                                                            "\t\t\t\t\t <input style=\"width:100%\"  " + strReadonly + "  asp-for=\"" + column_name + "\" type=\"" + GetColumnHtmlType(data_type) + "\" class=\"border-#d1cccc-200 form-control \" />\n" +
                                                            "\t\t\t\t\t<span asp-validation-for=\"" + column_name + "\" class=\"text-danger\"></span>\n" +
                                                      "\t\t\t\t</div>\n" +
                                             "\t\t\t</div>\n" +
                                             (!string.IsNullOrEmpty(column_name_2) ? ("\t\t\t<div class=\"col-md-6\">\n" +
                                                  "\t\t\t\t<div class=\"form-group\">\n" +
                                                        "\t\t\t\t\t<label class=\"labelNormal \" style=\"margin-bottom: 10px!important;font-weight:bold;\">" + GetColumnNameText(column_name_2) + "</label>\n" +
                                                        "\t\t\t\t\t<input style=\"width:100%\" " + strReadonly + "   asp-for=\"" + column_name_2 + "\" type=\"" + GetColumnHtmlType(data_type_2) + "\" class=\"border-#d1cccc-200 form-control \" />\n" +
                                                  "\t\t\t\t<span asp-validation-for=\"" + column_name_2 + "\" class=\"text-danger\"></span>\n" +
                                                  "\t\t\t</div>\n") : "\n") +
                                             "\t\t</div>\n" +
                                       "\t\t</div>\n\n";

                }
            }

            generated_text = generated_text.Replace("{AllColumns}", AllColumns);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + maintablename + "\\" + CamelCase(maintablename);

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\" + "_" + servicename + filename);

            //end Landing View
        }

        private void BuildTableFiles(string inputtablename)
        {
            var tablename = BuildTableEntity(1, inputtablename); //Entity
            BuildTableEntity(2, inputtablename); //DTO
            var servicename = BuildTableInterface(tablename); //Interface
            BuildTableService(tablename);

            BuildTableController(tablename);

            BuildTableLandingView(tablename);
            BuildTableCreateView(tablename, "NewView.txt", "New.cshtml", false);
            BuildTableCreateView(tablename, "EditView.txt", "Edit.cshtml", false);
            BuildTableCreateView(tablename, "View.txt", "View.cshtml", true);

            for (int ind = 1; ind < ddlOutputTables.Items.Count; ind++)
            {
                DataRowView rowView = (DataRowView)ddlOutputTables.Items[ind];

                // Assuming the column you want to retrieve the value from is named "ColumnName"
                object value = rowView[0];

                var det_tblname = CamelCase(value.ToString());

                DataTable dtd = new DataTable();

                dtd = GetTableDefinition(value.ToString());

                BuildTableCreateDetlView(dtd, tablename, value.ToString(), "NewView.txt", "New.cshtml", false);
                BuildTableCreateDetlView(dtd, tablename, value.ToString(), "EditView.txt", "Edit.cshtml", false);
                BuildTableCreateDetlView(dtd, tablename, value.ToString(), "View.txt", "View.cshtml", true);
            }



        }


    }
}
