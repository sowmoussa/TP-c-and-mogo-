using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Projet_ADO
{
    public partial class Accueil : Form
    {
        static string chaineConnexionDataBase = "Data Source=DESKTOP-L8BVLJG\\SQLSERVER;Initial Catalog=Terre;" + "Integrated Security=true";
        TablePaysComplet tpc = new TablePaysComplet();  
        public Accueil()
        {
            InitializeComponent();
            label1.Text = "Afficher tous les pays";
            label2.Text = "Rechercher par code Alpha2";
            label3.Text = "Que des lettres SVP";  
            button1.Text = "OK";
            button2.Text = "OK";
            label4.Text = "Ajout de Capitales";
            label5.Text = "Recherche Selective"; 
            button3.Text = "OK";
            button4.Text = "OK";
            button5.Text = "OK";
            label6.Text = "Ajout d'un pays imaginaire";   

        }
        private void button1_Click(object sender, EventArgs e)
        {
            TablePaysComplet f2 = new TablePaysComplet();
            f2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RechercheCode f3 = new RechercheCode(textBox1.Text);
            f3.Show(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AjoutCapitale f4 = new AjoutCapitale();
            f4.Show();  
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AffichageSelectif f5 = new AffichageSelectif();
            f5.Show();  
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            AjoutPaysImagianaire ajoutPaysImagianaire = new AjoutPaysImagianaire(); 
            ajoutPaysImagianaire.Show();  
        }
    }
}
