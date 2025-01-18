using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DALGenerator
{
    public partial class MenuActionGenerator : Form
    {
        List<menu_action_entity> ListMethod = new List<menu_action_entity>();
        public MenuActionGenerator()
        {
            InitializeComponent();
        }

        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(txtDbCon.Text))
            {

               
                DataTable dt = new DataTable();

                try
                {
                    string query = @"select m.menu_caption,m.menu_id from menu m where m.menu_id=m.parent_id order by m.seq_no";

                    // connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dt);
                    }

                    DataRow dataRow = dt.NewRow();
                    dataRow[0] = "Select";
                    dataRow[1] = "0";
                    dt.Rows.InsertAt(dataRow, 0);

                    cmbParentMenu.DataSource = dt;
                    cmbParentMenu.DisplayMember = "menu_caption";
                    cmbParentMenu.ValueMember = "menu_id";

                }
                catch (Exception ex)
                {
                    // Handle exception, log, and optionally rollback the transaction
                    Console.WriteLine($"Error: {ex.Message}");

                }

            }

            if (Directory.Exists(txtDirctory.Text))
            {
                string[] files = Directory.GetFiles(txtDirctory.Text,"*", SearchOption.AllDirectories);

                List<dropdown_entity> ListFiles = new List<dropdown_entity>();

                dropdown_entity obja = new dropdown_entity();
                obja.text ="SELECT";
                obja.id = "0";
                ListFiles.Add(obja);

                for (int i=0;i<files.Count();i++)
                {
                    dropdown_entity obj = new dropdown_entity();
                    obj.text = GetSubstringAfterLastBackslash(files[i]);
                    obj.id = files[i];
                    ListFiles.Add(obj);
                }

                if (ListFiles.Count > 0)
                {
                    cmb_Controller.DataSource = ListFiles.OrderBy(p => p.text).ToList(); ;
                    cmb_Controller.DisplayMember = "text";
                    cmb_Controller.ValueMember = "id";
                }
            }
        }

        public class dropdown_entity
        {
            public string id { get; set; }
            public string text { get; set; }

        }

        static string GetSubstringAfterLastBackslash(string input)
        {
            // Find the last index of backslash
            int lastIndex = input.LastIndexOf('\\');

            // Check if backslash is found
            if (lastIndex != -1 && lastIndex < input.Length - 1)
            {
                // Get the substring after the last backslash
                return input.Substring(lastIndex + 1);
            }

            // If no backslash is found or it's the last character, return the original string
            return input;
        }

        private void cmbParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbParentMenu.SelectedValue != null)
            {

                using (var connection = new NpgsqlConnection(txtDbCon.Text))
                {

                    DataTable dt = new DataTable();

                    try
                    {
                        string query = @"select m.menu_caption,m.menu_id from menu m where  m.menu_id<>m.parent_id and  m.parent_id=" + cmbParentMenu.SelectedValue.ToString() + " order by m.seq_no";

                        // connection.Open();

                        using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                        {
                            dataAdapter.Fill(dt);
                        }

                        DataRow dataRow = dt.NewRow();
                        dataRow[0] = "Select";
                        dataRow[1] = "0";
                        dt.Rows.InsertAt(dataRow,0);

                        cmb_TaretMenu.DataSource = dt;
                        cmb_TaretMenu.DisplayMember = "menu_caption";
                        cmb_TaretMenu.ValueMember = "menu_id";

                    }
                    catch (Exception ex)
                    {
                        // Handle exception, log, and optionally rollback the transaction
                        Console.WriteLine($"Error: {ex.Message}");

                    }

                }
            }
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
        static string GetMethodNameFromSignature(string methodSignature)
        {
            // Find the position of "Task<IActionResult>" in the signature
            int taskIndex = methodSignature.IndexOf("Task<IActionResult>");

            if (taskIndex != -1)
            {
                // Find the position of the first parenthesis after "Task<IActionResult>"
                int parenthesisIndex = methodSignature.IndexOf('(', taskIndex + "Task<IActionResult>".Length);

                if (parenthesisIndex != -1)
                {
                    // Extract the substring between "Task<IActionResult>" and the first parenthesis
                    string methodName = methodSignature.Substring(taskIndex + "Task<IActionResult>".Length, parenthesisIndex - taskIndex - "Task<IActionResult>".Length).Trim();

                    return methodName;
                }
            }

            // Return null or some default value if the extraction fails
            return null;
        }
        private void cmb_Controller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Controller.SelectedValue != null)
            {
                 ListMethod=new List<menu_action_entity>();

                var filetext = GetFileText(cmb_Controller.SelectedValue.ToString());

                string[] lines = filetext.Split('\n');

                // Loop through each line
                foreach (string line in lines)
                {
                    if (!line.Contains("Task<IActionResult>")) continue;

                    Console.WriteLine(line);

                    var methodname = GetMethodNameFromSignature(line);

                    var controllername=cmb_Controller.Text.Replace("Controller", "").Replace(".cs","").Trim();

                    var action = controllername + "/" + methodname;


                    menu_action_entity obj = new menu_action_entity();
                    obj.menu_id=Convert.ToInt32(cmb_TaretMenu.SelectedValue);
                    obj.method_name=methodname;
                    obj.action_url=action;
                    obj.is_select=true;

                    ListMethod.Add(obj);
                    // Process each line as needed
                }
                dataGridView1.AutoGenerateColumns = false;
                ListMethod = ListMethod.OrderBy(p => p.method_name).ToList();
                dataGridView1.DataSource = ListMethod.OrderBy(p=>p.method_name).ToList();

            }
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            string insertQuery = "INSERT INTO menu_action(menu_id, action_url, method_name,added_by,date_added) " +
           "VALUES (@menu_id, @action_url, @method_name,@added_by,@date_added)";

            // Create and open a connection
            using (NpgsqlConnection conn = new NpgsqlConnection(txtDbCon.Text))
            {
                conn.Open();
                int i = 0;
                foreach (var single in ListMethod)
                {
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString().ToLower() == "true")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@menu_id", Convert.ToInt64(cmb_TaretMenu.SelectedValue));
                            cmd.Parameters.AddWithValue("@action_url", single.action_url);
                            cmd.Parameters.AddWithValue("@method_name", single.method_name);

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
                    i++;
                }

                MessageBox.Show("Menu Action Added");
            }
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

        private void txtDbCon_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
