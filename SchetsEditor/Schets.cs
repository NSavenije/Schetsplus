using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace SchetsEditor
{
    class Schets
    {
        private Bitmap bitmap;
		  public bool saved = false;
		  //maakt lijst waar alle elementen die getekent moeten worden in komen te staan.
        public List<Bouwsteen>lijst = new List<Bouwsteen>();

        public Schets()
        {
			  bitmap = new Bitmap(1, 1);
        }

        public void AddBouwsteen(Bouwsteen bouwsteen)
        {
           this.lijst.Add(bouwsteen);
        }

        public Graphics BitmapGraphics
        {
            get { return Graphics.FromImage(bitmap); }
        }

        public void VeranderAfmeting(Size sz)
        {
            if (sz.Width > bitmap.Size.Width || sz.Height > bitmap.Size.Height)
            {
                Bitmap nieuw = new Bitmap( Math.Max(sz.Width,  bitmap.Size.Width)
                                         , Math.Max(sz.Height, bitmap.Size.Height)
                                         );
                Graphics gr = Graphics.FromImage(nieuw);
                gr.FillRectangle(Brushes.White, 0, 0, sz.Width, sz.Height);
                gr.DrawImage(bitmap, 0, 0);
                bitmap = nieuw;
            }
        }

        public void laden(Bitmap afbeelding)
        {
			  bitmap = afbeelding;
        }

        public void Teken(Graphics g)
        {
           Graphics.FromImage((Image)this.bitmap).FillRectangle(Brushes.White, 0, 0, this.bitmap.Width, this.bitmap.Height);
           Graphics gr = Graphics.FromImage((Image)this.bitmap);
           //tekent elke elemeent uit de lijst.
			  foreach (Bouwsteen item in lijst)
           {
              item.teken(gr);
           } 
           g.DrawImage(bitmap, 0, 0);
			  //omdat er nu iets is verandert moet de waarschuwing weer getoont worden als de form wordt afgesloten.
			  saved = false;
        }

        public void Schoon()
        {//de lijst word leeg gemaakt, waardoor er een lege afbeelding ontstaat.
			  lijst.Clear();
        }

        public void Roteer()
        {
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        public void opslaan()
        {
           SaveFileDialog sfd = new SaveFileDialog();
           sfd.Filter = "png files (*.png)|*.png|bitmap (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg";
           if (sfd.ShowDialog() == DialogResult.OK)
					bitmap.Save(sfd.FileName, ImageFormat.Bmp);
			  //er is opgeslagen, dus de waarschuwing moet nu niet worden getoont.
			  saved = true;
        }

		  public void opslaanSchets()
		  {
			  SaveFileDialog sfd = new SaveFileDialog();
			  sfd.Filter = "Schets files (*.schets)|*.schets";
			  StreamWriter sw = null;
			  if (sfd.ShowDialog() == DialogResult.OK)
				  sw = new StreamWriter(sfd.FileName);
				  for (int i = 0; i < lijst.Count; i++)
				  {
					  sw.WriteLine(convertToString(lijst[i]));
				  }
				  saved = true;
		  }

		  public void laadSchets(List<string> laadLijst)
		  {
			  for (int i = 0; i < laadLijst.Count; i++)
				  lijst.Add(convertToSteen(laadLijst[i]));
		  }

		  Bouwsteen convertToSteen(string s)
		  {
			  Bouwsteen b = null;
			  //hier een string omzetten naar een bouwsteen. niet af
			  return b;
		  }

		  string convertToString(Bouwsteen b)
		  {
			  string s = null;
			  //hier een bouwsteen omzetten naar een string. niet af
			  return s;
		  }

        public void RemoveBouwsteen(int x, int y)
        {
           //hier wordt gegumt.
			  for (int i = lijst.Count - 1; i >= 0; i--)
           {
              if (lijst[i].Hitbox(x, y))
              {
                 lijst.RemoveAt(i);
                 return;
              }
           }
        }
    }
}
