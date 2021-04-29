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
    public partial class Form1 : Form
    {
        static string con = "C:\\Users\\user\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\db.mdf";
        static SqlConnection connection = new SqlConnection(con);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            using (connection)
            {
                SqlCommand com = new SqlCommand("SELECT * FROM CSV_Export WHERE parent_id=0", connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                    {
                        int id =(int)reader["id"];
                        string name = reader["name"].ToString();
                        TreeNode newNode = treeView1.Nodes.Add(name);
                        GetTree(id, newNode);
                    }
            }
        }
        private void GetTree(int id, TreeNode parentNode)
        {
            using (connection)
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
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void BindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
