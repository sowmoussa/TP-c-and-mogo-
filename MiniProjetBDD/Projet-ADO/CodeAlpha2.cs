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
    public partial class CodeAlpha2 : Form
    {
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        

        public CodeAlpha2()
        {
           
            InitializeComponent();
            
            label1.Text = "formulaire de recherche par code alpha2";
        
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();
                string sqlQuery = "SELECT CodeNum, NomFR, NomEN,CapitaleFR,CapiTaleEN  from Pays where Alpha2 Like '" + textBox1.Text+ "' ";

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
        private void CodeAlpha2_Load(object sender, EventArgs e)
        {

        }
    }
}
