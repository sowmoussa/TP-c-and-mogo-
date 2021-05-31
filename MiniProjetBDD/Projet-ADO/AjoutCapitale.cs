using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Projet_ADO
{
    public partial class AjoutCapitale : Form
    {
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        public AjoutCapitale()
        {
            InitializeComponent();
            label1.Text = "Formulaire ajout de capitale";
            label2.Text = "Alpha2";
            label3.Text = "CapitaleFR";
            label4.Text = "CapitaleEN";
            button1.Text = "OK";  
            label1.TextAlign = ContentAlignment.TopRight; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();

                if (textBox2.Text.Equals("") && textBox3.Text.Equals(""))
                {

                    string sqlQuery = "UPDATE Pays set CapitaleFR = 'NULL', CapiTaleEN = 'NULL' where Alpha2 Like '" + textBox1.Text + "' ";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                    {
                        using (new SqlCommandBuilder(adapter))
                        {
                            adapter.Fill(table);
                            adapter.Update(table);
                            MessageBox.Show("modification done");
                        }
                    }
                }
                if(!textBox2.Text.Equals("") && !textBox3.Text.Equals(""))
                {
                    string sqlQuery = "UPDATE Pays set CapitaleFR = '" + textBox2.Text + "', CapiTaleEN = '" + textBox3.Text + "' where Alpha2 Like '" + textBox1.Text + "' ";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                    {
                        using (new SqlCommandBuilder(adapter))
                        {
                            adapter.Fill(table);
                            adapter.Update(table);
                            MessageBox.Show("modification done");
                        }
                    }
                }
                if (textBox2.Text.Equals("") && !textBox3.Text.Equals(""))
                {
                    string sqlQuery = "UPDATE Pays set CapitaleFR='NULL' ,CapiTaleEN='" + textBox3.Text + "' where Alpha2 Like '" + textBox1.Text + "' ";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                    {
                        using (new SqlCommandBuilder(adapter))
                        {
                            adapter.Fill(table);
                            adapter.Update(table);
                            MessageBox.Show("modification done");
                        }
                    }
                }
                if (!textBox2.Text.Equals("") && textBox3.Text.Equals(""))
                {
                    string sqlQuery = "UPDATE Pays set CapitaleFR='" + textBox2.Text + "',CapiTaleEN='NULL' where Alpha2 Like '" + textBox1.Text + "' ";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                    {
                        using (new SqlCommandBuilder(adapter))
                        {
                            adapter.Fill(table);
                            adapter.Update(table);
                            MessageBox.Show("modification done");
                        }
                    }
                }
                string sqlQueryA = "SELECT * from Pays where Alpha2 Like '" + textBox1.Text + "' ";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQueryA, connection))
                {
                    using (new SqlCommandBuilder(adapter))
                    {
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }
    }
}
