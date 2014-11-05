using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace SchetsEditor
{
    public class Hoofdscherm : Form
    {
        MenuStrip menuStrip;

        public Hoofdscherm()
        {   this.ClientSize = new Size(800, 600);
            menuStrip = new MenuStrip();
            this.Controls.Add(menuStrip);
            this.maakFileMenu();
            this.maakHelpMenu();
            this.Text = "Schets editor";
            this.IsMdiContainer = true;
            this.MainMenuStrip = menuStrip;
        }

        private void maakFileMenu()
        {   ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("File");
            menu.DropDownItems.Add("Nieuw", null, this.nieuw);
            menu.DropDownItems.Add("Laden", null, this.laden);
				menu.DropDownItems.Add("Laad Schets", null, this.laadSchets);
            menu.DropDownItems.Add("Afsluiten", null, this.afsluiten);
            menuStrip.Items.Add(menu);
        }

        private void maakHelpMenu()
        {   ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("Help");
            menu.DropDownItems.Add("Over \"Schets\"", null, this.about);
            menuStrip.Items.Add(menu);
        }

        private void about(object o, EventArgs ea)
        {   MessageBox.Show("Schets versie 1.0\n(c) UU Informatica 2010"
                           , "Over \"Schets\""
                           , MessageBoxButtons.OK
                           , MessageBoxIcon.Information
                           );
        }
        
		  private void nieuw(object sender, EventArgs e)
        {
			  SchetsWin s = new SchetsWin(); 
			  s.MdiParent = this;
           s.Show();
        }

        private void afsluiten(object sender, EventArgs e)
        {   this.Close();
        }

        //Laad een plaatje vanuit een bestaand bestand.
		  private void laden(object sender, EventArgs e)
        {
			  OpenFileDialog ofd = new OpenFileDialog();
           ofd.InitialDirectory = "C://";
           //alleen png, bmp en jpg files mogen worden geladen.
			  ofd.Filter = "png files (*.png)|*.png|bitmap (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg";
           //alleen als de gebruiker op OK drukt.
			  if (ofd.ShowDialog() == DialogResult.OK)
           {
              Bitmap afbeelding = new Bitmap(ofd.FileName);
				  SchetsWin s = new SchetsWin();
              s.MdiParent = this;
				  //plaatje word meegegeven aan de neiuwe SchetWin.
              s.setBitmap(afbeelding);
              s.Show();
           }
        }
		 
		  //Schets wordt geladen vanuit een tekst bestand.
		  private void laadSchets(object sender, EventArgs e)
		  {
			  OpenFileDialog ofd = new OpenFileDialog();
			  ofd.InitialDirectory = "C://";
			  //alleen bestanden met .schets als extentie kunnen worden geladen.
			  ofd.Filter = "Schets files (*.schets)|*.schets";
			  if (ofd.ShowDialog() == DialogResult.OK)
			  {
				  StreamReader sr = new StreamReader(ofd.FileName);
				  List<string> lijst = new List<string>();
				  //leest het bestand regel voor regel tot het einde van het bestand.
				  for (int i = 0; sr.ReadLine() != null; i++)
					  lijst.Add(sr.ReadLine());//voegt elke regel toe aan een lijst als één element. 
				  SchetsWin s = new SchetsWin();
				  s.MdiParent = this;
				  s.laadSchets(lijst);
				  s.Show();
			  }
		  }
    }
}
