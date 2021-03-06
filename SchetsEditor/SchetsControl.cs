﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SchetsEditor
{
    public class SchetsControl : UserControl
    {
        private Schets schets;
        private Color penkleur;

		  public bool Saved
		  {
			  get { return schets.saved; }
		  }

        public Color PenKleur 
        {   get { return penkleur; } 
        }

        public SchetsControl()
        {   this.BorderStyle = BorderStyle.Fixed3D;
            this.schets = new Schets();
            this.Paint += this.teken;
            this.Resize += this.veranderAfmeting;
            this.veranderAfmeting(null, null);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private void teken(object o, PaintEventArgs pea)
		  {  
			  schets.Teken(pea.Graphics);
        }

        private void veranderAfmeting(object o, EventArgs ea)
        {   schets.VeranderAfmeting(this.ClientSize);
            this.Invalidate();
        }

        public Graphics MaakBitmapGraphics()
        {   Graphics g = schets.BitmapGraphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            return g;
        }

        public void Schoon(object o, EventArgs ea)
        {   schets.Schoon();
            this.Invalidate();
        }

        public void Roteer(object o, EventArgs ea)
        {   schets.Roteer();
            this.veranderAfmeting(o, ea);
        }

        public void VeranderKleur(object obj, EventArgs ea)
        {   string kleurNaam = ((ComboBox)obj).Text;
            penkleur = Color.FromName(kleurNaam);
        }

        public void VeranderKleurViaMenu(object obj, EventArgs ea)
        {   string kleurNaam = ((ToolStripMenuItem)obj).Text;
            penkleur = Color.FromName(kleurNaam);
        }

        public void AddBouwsteen(Bouwsteen bouwsteen)
        {
           this.schets.AddBouwsteen(bouwsteen);
        }
        
        public void RemoveBouwsteen(int x, int y)
        {
           this.schets.RemoveBouwsteen(x, y);
           this.Invalidate();
        }
        
        public void opslaan(object sender, EventArgs ea)
        {
           this.schets.opslaan();
        }

		  public void opslaanSchets(object sender, EventArgs ea)
		  {
			  this.schets.opslaanSchets();
		  }

        public void openBitmap(Bitmap afbeelding)
        {
			  schets.laden(afbeelding);
			  this.Invalidate();
        }

		  public void laadSchets(List<string> lijst)
		  {
			  schets.laadSchets(lijst);
			  this.Invalidate();
		  }
    }
}
