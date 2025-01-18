using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DALGenerator
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private string GetTableFieldString(string inputLine, int isEntityOrDTO) //entity)
        {
            var strtext = inputLine.Trim().Split(' ');

            var Column = strtext[0];
            var ColType = strtext[1];

            string table_field = "";

            if (inputLine.Contains("AS IDENTITY") && isEntityOrDTO == 1)
                table_field = " \t\t\t[PrimaryKey(\"" + Column + "\", false)]\n";

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.ToLower() == "character")
                table_field += "\t\t\tpublic String " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "int" || ColType.ToLower() == "TINYINT")
                table_field += "\t\t\tpublic Int32? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "bigint")
                table_field += "\t\t\tpublic Int64? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "money")
                table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "bit" || ColType.ToLower() == "boolean")
                table_field += "\t\t\tpublic Boolean? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.ToLower() == "timestamp")
                table_field += "\t\t\tpublic DateTime? " + Column + "  { get; set;}\n\n";
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";

            else if (ColType.ToLower() == "json")
            {
                if (isEntityOrDTO == 1)
                    table_field += "\t\t\tpublic String " + Column + "  { get; set;}\n\n";
                else
                    table_field += "\t\t\tpublic JArray " + Column + "  { get; set;}\n\n";
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

                foreach (string input in this.txtScript.Lines)
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
                    sb = "\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nnamespace EPYSLSAILORAPP.Domain.DTO\n{";


                foreach (string input in this.txtScript.Lines)
                {

                    if (input.Contains("CREATE TABLE"))
                    {
                        table_view_name = input.Substring(input.LastIndexOf('.') + 1);
                        if (isEntityOrDTO == 1) //entity
                        {
                            sb += " [Table(\"" + table_view_name + "\")]";
                            sb += "\n\n\t\tpublic class " + input.Substring(input.LastIndexOf('.') + 1) + "_entity : BaseModel\n\t\t{\n\n";
                        }
                        else
                            sb += "\n\n\t\tpublic class " + input.Substring(input.LastIndexOf('.') + 1) + "_DTO \n\t\t{\n\n";
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

                    var singlecol = GetTableFieldString(input, isEntityOrDTO);
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

                return table_view_name.Replace(")","");

            }
            catch (Exception es)
            {
                return "";
            }
        }
        public string CamelCase(string str)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
            str = cultInfo.ToTitleCase(str);
            str = str.Replace(" ", "");
            return str;
        }
        private void BuildTableInterface(string table_view_name)
        {
            var filetext = GetFileText("Templates//IDataAccessService.txt");

            var servicename = CamelCase(table_view_name.Replace("_", ""));

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + table_view_name;

            CreateFileWithText(generated_text, EntityLocation + "\\I" + servicename + "Service.cs");

        }

        private void BuildTableService(string table_view_name)
        {

            var filetext = GetFileText("Templates//DataAccessService.txt");

            var servicename = CamelCase(table_view_name.Replace("_", ""));

            String generated_text = filetext.Replace("{TableName}", servicename).Replace("{EntityName}", table_view_name);

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
            if (this.txtScript.Text.Contains("CREATE TABLE"))
            {
                BuildTableFiles();
            }
            else if (this.txtScript.Text.Contains("CREATE OR REPLACE VIEW"))
            {
                BuildViewFiles();
            }
            else if (this.txtScript.Text.Contains("CREATE OR REPLACE FUNCTION"))
            {
                BuildRPCFunctionFiles();
            }
            MessageBox.Show("Done");
        }

        private void BuildTableFiles()
        {
            var tablename = BuildTableEntity(1); //Entity
            BuildTableEntity(2); //DTO
            BuildTableInterface(tablename); //Interface
            BuildTableService(tablename);
            BuildJS();


        }

        private void BuildViewFiles()
        {
            BuildViewEntity();
        }

        private void BuildRPCFunctionFiles()
        {
            var paramList = BuildRPCFunctionEntity();

            BuildRPCFunctionInterface(paramList); //Interface
            BuildRPCFunctionService(paramList);
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

                generated_text = generated_text.Replace("{all_param_declaration}", all_paramDeclarationList);
            }
            else
            {
                generated_text = generated_text.Replace("{all_param_declaration}", "");
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
                    all_paramDeclarationList += GetRPCFunctionFieldParameterString(paramList[i].Replace(",",""));

                    if (i != paramList.Count-1)
                        all_paramDeclarationList += ",";

                    var col = paramList[i].Split(' ');

                    if (col.Length > 0)
                    {
                        all_paramAddList += "\tobjFilter.Add(\"" + col[0].Trim() + "\", " + col[0].Trim() + ");\n";
                    }
                }

                filetext = filetext.Replace("{all_params}", all_paramAddList);
            }
            else
            {
                filetext = filetext.Replace("{all_params}", "");
            }
            

            var servicename = CamelCase(paramList[0].Replace("_", ""));

            String generated_text = filetext.Replace("{all_param_declaration}", all_paramDeclarationList).Replace("{TableName}", servicename).Replace("{EntityName}", paramList[0]);

            string EntityLocation = txtDirctory.Text.Trim() + "\\" + paramList[0];

            CreateFileWithText(generated_text, EntityLocation + "\\" + servicename + "Service.cs");

        }

        private string GetRPCFunctionFieldString(string inputLine)
        {

            var columns = inputLine.Split(',');
            string table_field = "";

            foreach (var singlecol in columns)
            {

                var ColType = singlecol.Trim().Contains(" ") ? singlecol.Trim().Split(' ')[1] : singlecol.Trim();
                var Column = singlecol.Trim().Contains(" ") ? singlecol.Trim().Split(' ')[0] : singlecol.Trim(); ;

                ColType = ColType.Replace(",", "");

                if (ColType.ToLower() == "text" || ColType.ToLower() == "character")
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
                else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.ToLower() == "timestamp")
                    table_field += "\t\t\tpublic DateTime? " + Column + "  { get; set;}\n\n";
                else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                    table_field += "\t\t\tpublic Decimal? " + Column + "  { get; set;}\n\n";
            }

            return table_field;
        }
        private string GetRPCFunctionFieldParameterString(string inputLine)
        {

            var columns = inputLine.Split(' ');
            string table_field = "";

            var ColType = columns[1].Trim();
            var Column = columns[0].Trim();

            ColType = ColType.Replace(",", "");

            if (ColType.ToLower() == "text" || ColType.ToLower() == "character")
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
            else if (ColType.ToLower() == "date" || ColType.ToLower() == "datetime" || ColType.ToLower() == "timestamp")
                table_field += "\t\t\tp DateTime? " + Column + "\n";
            else if (ColType.ToLower() == "decimal" || ColType.ToLower() == "float" || ColType.ToLower() == "numeric")
                table_field += "\t\t\t Decimal? " + Column + "\n";


            return table_field;
        }
        private List<string> BuildRPCFunctionEntity()
        {
            try
            {
                List<string> scripts = new List<string>();
                List<string> paramList = new List<string>();

                String sb = "\nusing Newtonsoft.Json.Linq;\nusing Postgrest.Attributes;\nusing Postgrest.Models;\n\nnamespace EPYSLSAILORAPP.Domain.RPC\n{";

                var table_view_name = "";
                foreach (string input in this.txtScript.Lines)
                {

                    if (input.Contains("CREATE OR REPLACE FUNCTION"))
                    {
                        table_view_name = input.Substring(input.LastIndexOf('.') + 1).Replace("(", "");
                        paramList.Add(table_view_name);
                        sb += "\n\n\t\tpublic class rpc_" + table_view_name + "_DTO \n\t\t{\n\n";
                        continue;
                    }


                    else if (input.Contains("RETURNS TABLE"))
                    {
                        var linestr = input.Replace("(", "").Replace(")", "").Replace("RETURNS TABLE", "");

                        sb += GetRPCFunctionFieldString(linestr);


                        break;
                    }
                    else if (input.Contains(",") || input.Contains(")"))
                    {
                        if (!string.IsNullOrEmpty(input.Replace(")", "").Trim()))
                        {
                            paramList.Add(input.Replace(")", "").Trim());
                        }
                    }

                }

                sb += "\n\t\t}\n}";

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
                foreach (string input in this.txtScript.Lines)
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

            var servicename = CamelCase(txtEntityName.Text.Replace("_", ""));

            var filetext = GetFileText("Templates//Select2ControllerMethod.txt");

            String generated_text = filetext.Replace("{TableName}", table_name).Replace("{ServiceName}", servicename)
                .Replace("{ValueField}", txtValueFieldName.Text).Replace("{TextField}", txtTextFieldName.Text).
                Replace("{EntityName}", txtEntityName.Text);

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            SupabaseGen sistema = new SupabaseGen();
            sistema.ShowDialog();
            this.Close();
        }

        private void btnFunc_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 sistema = new Form3();
            sistema.ShowDialog();
            this.Close();
        }
    }
}
