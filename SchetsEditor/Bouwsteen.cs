using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

//Deze klasse beschrijft elementen die moeten worden getekent. 

namespace SchetsEditor
{
   //elk type bouwsteen heeft ten minste deze eigenschappen. 
	public abstract class Bouwsteen
   {
      int beginx, beginy;
      protected Size grootte;
		
      //dit is nuttig voor het gummen
		public bool Hitbox(int x, int y)
		{
			return (x >= beginx && x <= beginx + grootte.Width && y >= beginy && y <= beginy + grootte.Height);
		}

		public int X
        { get { return this.beginx; } }

        public int Y
        { get { return this.beginy; } }

        public Bouwsteen(int x, int y, Size size)
        {
            this.beginx = x;
            this.beginy = y;
            this.grootte = size;
        }
        public abstract void teken(Graphics g);
   }

   public class TekstSteen : Bouwsteen
   {
      Font font;
      Char karakter;
      Brush kwast;

      public TekstSteen(int x, int y, char c, Font font, Brush kwast) : base(x, y, new Size())
      {
         this.karakter = c;
         this.font = font;
         this.kwast = kwast;
      }

      public override void teken(Graphics g)
      { g.DrawString(karakter.ToString(), font, kwast, this.X, this.Y);
      }

      public Size Grootte
      { set { this.grootte = value; } }

   }
   public class LijnSteen : Bouwsteen
   {
      Pen pen;

      public LijnSteen(int x, int y, Pen pen, Size grootte) : base(x, y, grootte)
      {
         this.pen = pen;
      }

   public override void teken(Graphics g)
   {
      g.DrawLine(pen, X, Y, X + grootte.Width, Y + grootte.Height);
   }
   }

   public class RechthoekSteen : Bouwsteen
   {
      Pen pen;

      public RechthoekSteen(int x, int y, Pen pen, Size grootte) : base(x, y, grootte)
      { this.pen = pen;
      }

      public override void teken(Graphics g)
      { g.DrawRectangle(pen, X, Y, grootte.Width, grootte.Height);
      }
   }

   public class VolRechthoekSteen : Bouwsteen
   {
      Brush kwast;
      public VolRechthoekSteen(int x, int y, Brush kwast, Size grootte) : base(x, y, grootte)
      {
         this.kwast = kwast;
      }

      public override void teken(Graphics g)
      { g.FillRectangle(kwast, X, Y, grootte.Width, grootte.Height); 
      }
   }
   public class CirkelSteen : Bouwsteen
   {
      Pen pen;
      public CirkelSteen(int x, int y, Pen pen, Size grootte) : base(x, y, grootte)
      { this.pen = pen;
      }

      public override void teken(Graphics g)
      { g.DrawEllipse(pen, X, Y, grootte.Width, grootte.Height);
      }
   }

   public class VolCirkelSteen : Bouwsteen
   {
      Brush kwast;
      public VolCirkelSteen(int x, int y, Brush kwast, Size grootte) : base(x, y, grootte)
      { this.kwast = kwast;
      }

      public override void teken(Graphics g)
      { g.FillEllipse(kwast, X, Y, grootte.Width, grootte.Height);
      }
   }

   public class PenSteen : Bouwsteen
   {
      Pen pen;
      List<LijnSteen>penLijst;
      public PenSteen(int x, int y, Pen pen) : base(x, y, new Size())
      {
         this.pen = pen;
         penLijst = new List<LijnSteen>();
      }

      public void Lijntoevoeg(LijnSteen lijn)
      { penLijst.Add(lijn);
      }

      public override void teken(Graphics g)
      {
         foreach (LijnSteen lijn in penLijst)
         {
            lijn.teken(g);
         }
      }
   }
}

