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
    public partial class RechercheCode : Form
    {
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        string value; 
        
        public RechercheCode(string codeAlpha2)
        {
            this.value = codeAlpha2;
            InitializeComponent();
            label1.Text = "Les informations du pays";
            importData(); 
        }

        public  void importData()
        {
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();
                string sqlQuery = "SELECT * from Pays where Alpha2 Like '" +value+ "' ";
             
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    using(new SqlCommandBuilder(adapter))
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
