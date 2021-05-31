using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_ADO
{
    public partial class NomPaysFR : Form
    {
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        public NomPaysFR()
        {
            InitializeComponent();
            label1.Text = "Formulaire de Recherche par Nom pays en français"; 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();
                string sqlQuery = "SELECT  NomEN from Pays where NomFR Like '" + textBox1.Text + "' ";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    using (new SqlCommandBuilder(adapter))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }
    }
}
