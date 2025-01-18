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

namespace DALGenerator
{
    public partial class SupabaseGen : Form
    {
        DataTable dtColumns = new DataTable();
        public SupabaseGen()
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

        private string GetTableFieldString(string col_name, string col_type, string is_identity, string is_nullable, int isEntityOrDTO) //entity)
        {
            var Column = col_name;
            var ColType = col_type;

            string table_field = "";

            if (is_identity == "yes" && isEntityOrDTO == 1)
                table_field = " \t\t\t[PrimaryKey(\"" + Column + "\", false)]\n";

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.Contains("character"))
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";

                if(isEntityOrDTO == 1)
                    table_field = " \t\t\t[Column(\""+ Column + "\")]\n";

                table_field += "\t\t\tpublic string" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "tinyint" || ColType.ToLower() == "integer")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";
                if (isEntityOrDTO == 1)
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                table_field += "\t\t\tpublic Int32?" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "bigint")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";
                if (isEntityOrDTO == 1)
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                table_field += "\t\t\tpublic Int64" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "money")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";
                if (isEntityOrDTO == 1)
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                table_field += "\t\t\tpublic Decimal" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";
                table_field += "\t\t\tpublic Boolean?" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.Contains("timestamp"))
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";
                if (isEntityOrDTO == 1)
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                table_field += "\t\t\tpublic DateTime" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2)
                    table_field = " \t\t\t[Required]\n";
                if (isEntityOrDTO == 1)
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                table_field += "\t\t\tpublic Decimal" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }
            else if (ColType.ToLower() == "json")
            {
                if (is_nullable == "no" && isEntityOrDTO == 2 && is_identity != "yes" && Column != "added_by" && Column != "date_added")
                    table_field = " \t\t\t[Required]\n";
                if (isEntityOrDTO == 1)
                {
                    table_field = " \t\t\t[Column(\"" + Column + "\")]\n";
                    table_field += "\t\t\tpublic String" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
                }
                else
                    table_field += "\t\t\tpublic JArray" + (is_nullable == "no" ? "?" : "") + Column + "  { get; set;}\n\n";
            }

            return table_field;
        }

        private string BuildJS()
        {
            try
            {
                var table_view_name = "";
                List<string> scripts = new List<string>();
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
        private string BuildTableEntity(int isEntityOrDTO)
        {
            try
            {
                var table_view_name = "";
                List<string> scripts = new List<string>();
                String sb = "";

                if (isEntityOrDTO == 1) //entity
                    sb = "\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nnamespace EPYSLSAILORAPP.Domain.Entity\n{";
                else//dto
                    sb = "\nusing BDO.Core.Base;\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nusing System.ComponentModel.DataAnnotations;\nnamespace EPYSLSAILORAPP.Domain.DTO\n{";


                table_view_name = ddlTables.SelectedValue.ToString();

                if (isEntityOrDTO == 1) //entity
                {
                    sb += " [Table(\"" + table_view_name + "\")]";
                    sb += "\n\n\t\tpublic class " + ddlTables.SelectedValue.ToString() + "_entity : BaseModel\n\t\t{\n\n";
                }
                else
                    sb += "\n\n\t\tpublic class " + ddlTables.SelectedValue.ToString() + "_DTO : BaseEntity\n\t\t{\n\n";

                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {

                    string column_name = dtColumns.Rows[i][0].ToString().ToLower();
                    string data_type = dtColumns.Rows[i][2].ToString().ToLower();
                    string is_nullable = dtColumns.Rows[i][1].ToString().ToLower();
                    string is_identity = dtColumns.Rows[i][3].ToString().ToLower();


                    var singlecol = GetTableFieldString(column_name, data_type, is_identity, is_nullable, isEntityOrDTO);
                    sb += singlecol;
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
        private string BuildTableInterface(string table_view_name)
        {
            var filetext = GetFileText("Templates//IDataAccessService.txt");

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            CreateFileWithText(generated_text, EntityLocation + "\\I" + servicename + "Service.cs");

            return servicename;

        }


        private void BuildTableService(string table_view_name)
        {

            var filetext = GetFileText("Templates//DataAccessService.txt");

            var servicename = CamelCase(table_view_name);

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name)
                .Replace("{PrimaryColumn}", dtColumns.Rows[0][0].ToString().ToLower());

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "Service.cs");

        }

        private void CreateFileWithText(string generate_text, string path)
        {


            if (System.IO.File.Exists(path))
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            //Entity
            if (dtColumns.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    // Get the value from the specified column in the selected row
                    object cellValue = dataGridView1.Rows[i].Cells[5].Value;

                    if (cellValue != null)
                    {
                        dtColumns.Rows[i][5] = cellValue;
                    }
                }


                BuildTableFiles();
            }

            MessageBox.Show("Done");
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

            var AllColumns = "";

            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                //col.column_name,col.is_nullable,col.data_type,col.is_identity

                string column_name = dtColumns.Rows[i][0].ToString();
                string data_type = dtColumns.Rows[i][2].ToString();
                string is_nullable = dtColumns.Rows[i][1].ToString().ToLower();
                string is_identity = dtColumns.Rows[i][3].ToString().ToLower();

                AllColumns += "\t\t\tt." + column_name + ",\n";

            }

            generated_text = generated_text.Replace("{AllColumns}", AllColumns);

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
                if (dtColumns.Rows[i][5].ToString().ToLower() == "true")
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

            string popupwidth = (ddlSingleDoubleColumn.SelectedValue == "Single Column" || ddlSingleDoubleColumn.SelectedValue == "") ? "30" : "75";

            generated_text = generated_text.Replace("{AllDataTableHeaders}", AllColumnHeaders)
                .Replace("{AllDataTableColumns}", AllDataTableColumns)
                .Replace("{AllInsertUpdateColumns}", AllInsertUpdateColumns)
                .Replace("{popup_width}", popupwidth);

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
                    if (dtColumns.Rows[i][5].ToString().ToLower() == "true")
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

        private void BuildTableFiles()
        {
            var tablename = BuildTableEntity(1); //Entity
            BuildTableEntity(2); //DTO
            var servicename = BuildTableInterface(tablename); //Interface
            BuildTableService(tablename);

            BuildTableController(tablename);

            BuildTableLandingView(tablename);
            BuildTableCreateView(tablename, "NewView.txt", "New.cshtml", false);
            BuildTableCreateView(tablename, "EditView.txt", "Edit.cshtml", false);
            BuildTableCreateView(tablename, "View.txt", "View.cshtml", true);

        }

        private void BuildViewFiles()
        {
            BuildViewEntity();
        }

       
        private void BuildRPCFunctionInterface(List<string> paramList)
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

        private void BuildRPCFunctionService(List<string> paramList)
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

        private string GetRPCFunctionFieldString(DataRow dr)
        {

            string table_field = "";

            var ColType = dr["data_type"].ToString();
            var Column =  dr["column_name"].ToString();

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

                for (var i=0;i< outputparams.ToTable().Rows.Count;i++)
                {

                    sb += GetRPCFunctionFieldString(outputparams.ToTable().Rows[i]);

                }

                sb += "\n\t\t}\n}";

                for (var i = 0; i < inputparams.ToTable().Rows.Count; i++)
                {

                    if (inputparams.ToTable().Rows.Count > 0)
                    {
                        paramList.Add(inputparams.ToTable().Rows[i]["column_name"].ToString()+
                            " "+ inputparams.ToTable().Rows[i]["data_type"].ToString());
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
                return new List<string>();
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
                List<string> scripts = new List<string>();

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

        private void btnGnerateSelect2_Click(object sender, EventArgs e)
        {
            BuildViewComponent();
            BuildSelect2View();
            BuildSelect2ControllerMethod();
        }

        private void BuildSelect2ControllerMethod()
        {
            string table_name = txtS2Name.Text;

            var servicename = CamelCase(ddlTables.SelectedValue.ToString().Replace("_", ""));

            var filetext = GetFileText("Templates//Select2ControllerMethod.txt");

            String generated_text = filetext.Replace("{TableName}", table_name).Replace("{ServiceName}", servicename)
                .Replace("{ValueField}", ddlS2ValueField.SelectedValue.ToString()).Replace("{TextField}", ddl_S2TextField.SelectedValue.ToString()).
                 Replace("{EntityName}", ddlTables.SelectedValue.ToString());

            string EntityLocation = txtDirctory.Text.Trim() + "\\S2" + table_name;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\Select2ControllerMethod.cs");

        }

        private void BuildViewComponent()
        {
            string table_name = txtS2Name.Text;

            var filetext = GetFileText("Templates//Select2ViewComponent.txt");

            String generated_text = filetext.Replace("{TableName}", table_name);

            string EntityLocation = txtDirctory.Text.Trim() + "\\S2" + table_name;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\S2" + table_name + "ViewComponent.cs");

        }

        private void BuildSelect2View()
        {
            string table_name = txtS2Name.Text;

            var filetext = GetFileText("Templates//Select2View.txt");

            String generated_text = filetext.Replace("{TableName}", table_name);

            string EntityLocation = txtDirctory.Text.Trim() + "\\S2" + table_name;

            if (!Directory.Exists(EntityLocation))
                Directory.CreateDirectory(EntityLocation);

            CreateFileWithText(generated_text, EntityLocation + "\\S2" + table_name + ".cshtml");

        }



        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {

                DataTable dataTable = new DataTable();

                try
                {
                    string query = @"select table_name from information_schema.tables where table_schema='public' order by table_name";

                    connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dataTable);
                    }


                    ddlTables.DataSource = dataTable;
                    ddlTables.DisplayMember = "table_name";
                    ddlTables.ValueMember = "table_name";



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                DataTable dt = new DataTable();

                try
                {
                    string query = @"select m.menu_caption,m.menu_id from menu m where m.menu_id=m.parent_id order by m.seq_no";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dt);
                    }

                    cmbParentMenu.DataSource = dt;
                    cmbParentMenu.DisplayMember = "menu_caption";
                    cmbParentMenu.ValueMember = "menu_id";

                }
                catch (Exception ex)
                {
                    // Handle exception, log, and optionally rollback the transaction
                    Console.WriteLine($"Error: {ex.Message}");

                }

                 dt = new DataTable();

               
            }
        }

        private void btnLoadColumn(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {

                dtColumns = new DataTable();

                try
                {
                    string query = @"SELECT  col.column_name,col.is_nullable,col.data_type,col.is_identity,
                                    col.ordinal_position,true as ChkSelect FROM information_schema.columns col
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

                        if (i == 0 || colname.ToString().ToLower() == "added_by" || colname.ToString().ToLower() == "updated_by" || colname.ToString().ToLower() == "date_added" || colname.ToString().ToLower() == "date_updated")
                        {
                            dtColumns.Rows[i][5] = false;
                        }
                    }

                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dtColumns;

                    txtS2Name.Text = CamelCase(ddlTables.SelectedValue.ToString());

                    ddlS2ValueField.DataSource = dtColumns.Copy();
                    ddlS2ValueField.ValueMember = "column_name";
                    ddlS2ValueField.DisplayMember = "column_name";
                    // ddlS2ValueField.SelectedIndex = 0;

                    ddl_S2TextField.DataSource = dtColumns.Copy();
                    ddl_S2TextField.ValueMember = "column_name";
                    ddl_S2TextField.DisplayMember = "column_name";
                    // ddl_S2TextField.SelectedIndex = 1;

                   
                }
                catch (Exception ex)
                {
                    // Handle exception, log, and optionally rollback the transaction
                    MessageBox.Show($"Error: {ex.Message}");

                }

            }
        }

        private void SupabaseGen_Load(object sender, EventArgs e)
        {

        }

        private void SupabaseGen_Load_1(object sender, EventArgs e)
        {

        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {

            string insertQuery = "INSERT INTO menu(parent_id, menu_caption, navigate_url,image_url,seq_no,is_visible,added_by,date_added) " +
                "VALUES (@parent_id, @menu_caption, @navigate_url,@image_url,@seq_no,@is_visible,@added_by,@date_added)";

            // Create and open a connection
            using (NpgsqlConnection conn = new NpgsqlConnection(txtDbCon.Text))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@parent_id", Convert.ToInt64(cmbParentMenu.SelectedValue));
                    cmd.Parameters.AddWithValue("@menu_caption", GetColumnNameText(ddlTables.Text));
                    cmd.Parameters.AddWithValue("@navigate_url", CamelCase(ddlTables.Text) + "/" + CamelCase(ddlTables.Text) + "Landing");
                    cmd.Parameters.AddWithValue("@image_url", "../../Images/General/hk.png");
                    cmd.Parameters.AddWithValue("@seq_no", 99);
                    cmd.Parameters.AddWithValue("@is_visible", true);
                    cmd.Parameters.AddWithValue("@added_by", 1);
                    cmd.Parameters.AddWithValue("@date_added", DateTime.Now);


                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if the insertion was successful
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data inserted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert data.");
                    }
                }
            }

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
            SupabaseSPGen sistema = new SupabaseSPGen();
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

            string InputParamsWithTypeOnlyText = "", InputParamsWithType = "";

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                var colname = dt.Rows[i]["column_name"].ToString();
                var coltype = dt.Rows[i]["data_type"].ToString();

                if (coltype.ToLower().Contains("text") || coltype.ToLower().Contains("json"))
                {
                    if (coltype.ToLower().Contains("text"))
                        InputParamsWithTypeOnlyText += "\t\t{\"" + "in_" + dt.Rows[i][0] + "\", " + prefix + dt.Rows[i][0] + " }";
                    else
                        InputParamsWithTypeOnlyText += "\t\t{\"" + "in_" + dt.Rows[i][0] + "\", " + prefix + dt.Rows[i][0] + ".ToString() }";


                    if (i != (dt.Rows.Count - 1))
                    {
                        InputParamsWithTypeOnlyText += "," + Environment.NewLine;

                    }
                    else
                    {
                        InputParamsWithTypeOnlyText += Environment.NewLine;

                    }
                }
                else
                {
                    InputParamsWithType += "\t\t{\"" + "in_" + dt.Rows[i][0] + "\", " + prefix + dt.Rows[i][0] + " }";

                    if (i != (dt.Rows.Count - 1))
                    {
                        InputParamsWithType += "," + Environment.NewLine;
                    }
                    else
                    {
                        InputParamsWithType += Environment.NewLine;

                    }
                }


            }


            return InputParamsWithType + InputParamsWithTypeOnlyText;
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

        private List<string> GetInnerSQL(string newforeignkey, string inner_table_name, string detl_column, string docno)
        {
            //var inner_table_name = ddl_detl1.SelectedValue.ToString();
            var ret = new List<string>();

            var NewIDVariables = "";

            DataTable dt_detl1 = GetTableDefinition(inner_table_name);

            NewIDVariables += "row_" + inner_table_name + " RECORD;" + Environment.NewLine;

            NewIDVariables += "NEW_" + dt_detl1.Rows[0]["column_name"].ToString() + " BIGINT;" + Environment.NewLine;

            var col_names_with_prefix = GetTableColumnNames(dt_detl1, "inner_" + inner_table_name, "");
            var col_names = GetTableColumnNames(dt_detl1, "", "");
            var col_names_with_rowprefix = GetTableColumnNames(dt_detl1, "row_" + inner_table_name, newforeignkey);

            var inner_query = @" FOR " + "row_" + inner_table_name + @" IN (
                                        SELECT
                                            " + "inner_" + dt_detl1.Rows[0]["column_name"].ToString() + "," + col_names_with_prefix + @"

                                        FROM
                                            LATERAL json_array_elements(" + "in_" + detl_column + @"::json) AS " + docno + @"(doc),
                                            LATERAL json_populate_record(null::" + inner_table_name + @", " + docno + @".doc) AS " + "inner_" + inner_table_name + @"
                                    )
                                    LOOP
                                                CASE
                                                 
                                                WHEN row_" + inner_table_name + @".current_state=1 THEN

                                                INSERT INTO "" + inner_table_name + @"" (
                                                    "" + col_names + @""

                                                )
                                                VALUES (
                                                   "" + col_names_with_rowprefix + @""

                                                )

                                                RETURNING "" + dt_detl1.Rows[0][""column_name""].ToString() + @"" INTO "" + ""NEW_"" + dt_detl1.Rows[0][""column_name""].ToString() + @"";

                                                WHEN row_"" + inner_table_name+@"".current_state=2 THEN

                                                WHEN row_"" + inner_table_name+@"".current_state=3 THEN
                        
                                                END CASE;

                                     
                                    END LOOP;";

            ret.Add(NewIDVariables);
            ret.Add(inner_query);
            return ret;
        }

        private List<string> GetInnerUpdateSQL(string newforeignkey, string inner_table_name, string detl_column, string docno)
        {
            //var inner_table_name = ddl_detl1.SelectedValue.ToString();
            var ret = new List<string>();

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

        private List<string> GetSelectJSONInnerSQL(string inner_table_name)
        {
            var ret = new List<string>();

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

       
    }
}
