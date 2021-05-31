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
using System.IO;

namespace Projet_ADO
{
    public partial class AjoutPaysImagianaire : Form
    {
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        
        public AjoutPaysImagianaire()
        {
            InitializeComponent();
            label1.Text = "CodeNum";
            label2.Text = "Alpha2";
            label3.Text = "Alpha3";
            label4.Text = "NomFR";
            label5.Text = "NomEN";
            button1.Text = "OK";
           
        }
        public static byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            byte[] photo = reader.ReadBytes((int)stream.Length);
            reader.Close();
            stream.Close();
            return photo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             byte[] flags = GetPhoto(@"images\hacker.jpg");
          
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();
                try
                {
                    string sqlQuery = "INSERT INTO pays VALUES(@CodeNum ,@Alpha2 , @Alpha3, @NomFR ,@NomEN, NULL, NULL,@Drapeau)";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.Add(new SqlParameter("CodeNum", textBox1.Text));
                        command.Parameters.Add(new SqlParameter("Alpha2", textBox2.Text));
                        command.Parameters.Add(new SqlParameter("Alpha3", textBox3.Text));
                        command.Parameters.Add(new SqlParameter("NomFR", textBox4.Text));
                        command.Parameters.Add(new SqlParameter("NomEN", textBox5.Text));
                        command.Parameters.Add(new SqlParameter("Drapeau", flags));
                        MessageBox.Show("added finished");  
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    MessageBox.Show("PB de requete");  
                }
            }
        
        }
    }
}
