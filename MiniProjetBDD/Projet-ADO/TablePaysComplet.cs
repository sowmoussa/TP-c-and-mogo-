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
    public partial class TablePaysComplet : Form
    {
        static string chaineConnexion = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=master;" + "Integrated Security=true";
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        public TablePaysComplet()
        {
            InitializeComponent();
            DeleteDataBaseIsExist();
            CreatedDataBase();
            DeleteTableIfExists();
            CreatedTable();
            importDataFromTabToDataBase(ImportDataFromFileToTab());
            ImportDataFromDBBToDGV();
            AddColonneDrapeaux();  
            importFlagsFromFileToDB(CodeAlpha2());  
        }
        static void DeleteDataBaseIsExist()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(chaineConnexion))
                {
                    connection.Open();
                    try
                    {

                        String sql = "if exists(select * from master..sysdatabases where name='Terre') drop database Terre";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Suppression OK");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("probleme");
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Un probleme");
            }
        }
        static void CreatedDataBase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(chaineConnexion))
                {
                    connection.Open();
                    try
                    {
                        string sql = "CREATE DATABASE Terre";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Base de données existe ou non crée");
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Probleme");
            }
        }
        static void DeleteTableIfExists()
        {
            using (SqlConnection connexion = new SqlConnection(chaineConnexionDataBase))
            {
                try
                {
                    connexion.Open();
                    string sqlQuery = "if exists(select* from sys.tables where type= 'U' and name = 'Pays') drop TABLE Pays";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connexion))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                }
            }
        }
        static void CreatedTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
                {
                    connection.Open();
                    try
                    {
                        string SqlQuery = "CREATE TABLE Pays(CodeNum Int, Alpha2 TEXT , Alpha3 TEXT, NomFR TEXT, NomEN TEXT, CapitaleFR TEXT, CapiTaleEN TEXT)";
                        using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch { Console.WriteLine("table existe dejà ou non crée"); }
                }
            }
            catch (SqlException) { Console.WriteLine("Error"); }
        }

        public static List<string> ImportDataFromFileToTab()
        {
           
            string path = @"pays\pays.csv";
            List<string> list = new List<string>();

            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    list.Add(line);
                }

            }
            return list;
        }
        public static void importDataFromTabToDataBase(List<string> line)
        {
            foreach (string s in line)
            {
                int j;
                string[] value = s.Split(',');
                for (int i = 1; i < value.Length; i++)
                    value[i] = value[i].Substring(0, value[i].Length);

                Int32.TryParse(value[1], out j);

                using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
                {
                    connection.Open();
                    try
                    {
                        string sqlQuery = "INSERT INTO pays VALUES(@CodeNum ,@Alpha2 , @Alpha3, @NomFR ,@NomEN, NULL, NULL)";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.Add(new SqlParameter("CodeNum", j));
                            command.Parameters.Add(new SqlParameter("Alpha2", value[2]));
                            command.Parameters.Add(new SqlParameter("Alpha3", value[3]));
                            command.Parameters.Add(new SqlParameter("NomFR", value[4]));
                            command.Parameters.Add(new SqlParameter("NomEN", value[5]));
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        public void ImportDataFromDBBToDGV()
        {
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();
                string sqlQuery = "SELECT * from Pays";
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

        static void AddColonneDrapeaux()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
                {
                    connection.Open();
                    try
                    {
                        string sqlQuery = "ALTER TABLE Pays ADD Drapeau IMAGE";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cette colonne existe deja");
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("PB");
            }
        }



        public static List<string> CodeAlpha2()
        {
            string path = @"pays\pays.csv";
            List<string> list = new List<string>();
            List<string> listToReturn = new List<string>();

            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    list.Add(line);
                    string[] champ = line.Split(',');
                    listToReturn.Add(champ[2]);
                }
            }
            return listToReturn;
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
        public void importFlagsFromFileToDB(List<string> returnedList)
        {
            using (SqlConnection connection = new SqlConnection(chaineConnexionDataBase))
            {
                connection.Open();
                try
                {
                    foreach (string codeAlpha2 in returnedList)
                    {
                        string flagsFilePath = @"images\";
                        string fullPathOfImage = flagsFilePath + codeAlpha2 + ".png";
                        if (File.Exists(fullPathOfImage))
                        {
                            byte[] flags = GetPhoto(fullPathOfImage);
                            string sqlQuery = "UPDATE Pays SET Drapeau='flags' WHERE Alpha2 LIKE '" + codeAlpha2.ToLower() + "' ";
                            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                            {
                                command.Parameters.Add(new SqlParameter("Drapeau", flags));
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("PB de l'importation des drapeaux");
                }
            }
        }

    }
}
