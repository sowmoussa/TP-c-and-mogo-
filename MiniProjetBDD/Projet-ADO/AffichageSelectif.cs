using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_ADO
{
    public partial class AffichageSelectif : Form
    {
        public string chaine { get; set; }
        public string radio { get; set; }
        public AffichageSelectif()
        {
            InitializeComponent();
            groupBox1.Text = "Affichage Selective";
            radioButton1.Text = "Code Alpha2";
            radioButton2.Text = "Nom du pays en Français";
            radioButton3.Text = "Capitale du pays en Français";
            button1.Text = "OK";  
        }

        private void button1_Click(object sender, EventArgs e)
        {
              
            foreach(Control control in groupBox1.Controls)
            {
                RadioButton rb = control as RadioButton;
                if(rb == null)
                {
                    MessageBox.Show("Control is not a RadioButton");
                    return;  
                }
                if (rb.Checked)
                {
                    chaine = rb.Text;
                    if (chaine == radioButton1.Text)
                    {
                        CodeAlpha2 codeAlpha2 = new CodeAlpha2(); codeAlpha2.Show();
                        break;
                    }
                    if (chaine == radioButton2.Text)
                    {
                        NomPaysFR nomPaysFR = new NomPaysFR(); nomPaysFR.Show();
                        break;
                    }
                    if (chaine == radioButton3.Text)
                    {
                        CapitalePaysFR capitalePaysFR = new CapitalePaysFR(); capitalePaysFR.Show(); 
                        break;
                    }
                }
            }
          
        }
    }
}
