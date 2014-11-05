using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SchetsEditor
{
    class Schets
    {
        private Bitmap bitmap;

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
        public void Teken(Graphics gr)
        {
           Graphics.FromImage((Image)this.bitmap).FillRectangle(Brushes.White, 0, 0, this.bitmap.Width, this.bitmap.Height);
           Graphics bitmap = Graphics.FromImage((Image)this.bitmap);
           foreach (Bouwsteen item in lijst)
           {
              item.teken(bitmap);
           } 
           gr.DrawImage(this.bitmap, 0, 0);
        }

        public void Schoon()
        {
            Graphics gr = Graphics.FromImage(bitmap);
            gr.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
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
           bitmap.Save(sfd.FileName, ImageFormat.Png);
        }

        public void RemoveBouwsteen(int x, int y)
        {
           for (int i = lijst.Count - 1; i >= 0; i--)
           {
              if ()
              {
                 lijst.RemoveAt(i);
                 return;
              }
           }
        }
    }
}
