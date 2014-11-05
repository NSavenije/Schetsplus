using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SchetsEditor
{
    public class SchetsControl : UserControl
    {
        private Schets schets;
        private Color penkleur;

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
        {   schets.Teken(pea.Graphics);
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
        /*public void listEntry(ISchetsTool tool, SizeF grootte, string text, Font font, Point begin, Point eind)
        {
           Bouwsteen entry = new Bouwsteen();
           entry.tool = tool;
           entry.grootte = grootte;
           entry.kleur = this.PenKleur;
           entry.text = text;
           entry.font = font;
           entry.begin = begin;
           entry.einde = eind;
           schets.lijst.Add(entry);
        }*/

        public void opslaan(object sender, EventArgs ea)
        {
           this.schets.opslaan();
        }

       public void openBitmap(Bitmap afbeelding)
        {
           this.schets.laden(afbeelding);
        }
    }
}
