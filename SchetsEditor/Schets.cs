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
            gr.DrawImage(bitmap, 0, 0);
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
           SaveFileDialog svd = new SaveFileDialog();
           svd.Filter = "png files (*.png)|*.png|bitmap (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg";
           if (svd.ShowDialog() == DialogResult.OK)
           bitmap.Save(svd.FileName, ImageFormat.Png);
        }
    }
}
