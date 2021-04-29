using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB
{
    public partial class Form2 : Form
    {
        string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\db.mdf;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            SqlConnection connection = new SqlConnection(con);
            using (connection)
            {
                try
                {
                    SqlCommand com = new SqlCommand("SELECT * FROM CSV_Export WHERE parent_id=0", connection);
                    connection.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id = (int)reader["id"];
                            string name = reader["name"].ToString();
                            TreeNode newNode = treeView1.Nodes.Add(name);
                            GetTree(id, newNode);
                        }
                }
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
        
        private void GetTree(int id, TreeNode parentNode)
        {
            
            SqlConnection connection = new SqlConnection(con);
            using (connection)
            {
                try
                {
                    string sqlText = String.Format("SELECT * FROM CSV_Export WHERE  parent_id= '{0}'", id);

                    SqlCommand command = new SqlCommand(sqlText, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id1 = (int)reader["id"];
                            TreeNode newNode = new TreeNode(reader["name"].ToString());
                            parentNode.Nodes.Add(newNode);
                            GetTree(id1, newNode);
                        }

                    connection.Close();
                }
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            SqlConnection connection = new SqlConnection(con);
            using (connection)
            {
                try
                {
                    string sqlText = String.Format("SELECT note FROM CSV_Export WHERE  name= N'{0}'", e.Node.Text);

                    SqlCommand command = new SqlCommand(sqlText, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();



                    if (reader.HasRows)
                    {
                        reader.Read();
                        textBox1.Text = reader["note"].ToString();
                    }


                    connection.Close();
                }
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
