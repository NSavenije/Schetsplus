using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchetsEditor
{
    public interface ISchetsTool
    {
        void MuisVast(SchetsControl s, Point p);
        void MuisDrag(SchetsControl s, Point p);
        void MuisLos(SchetsControl s, Point p);
        void Letter(SchetsControl s, char c);
    }

   
    public abstract class StartpuntTool : ISchetsTool
    {
        protected Point startpunt;
        protected Brush kwast;
        protected Bouwsteen bouwsteen = null;

      
        public virtual void MuisVast(SchetsControl s, Point p)
        {   startpunt = p;
        }
        public virtual void MuisLos(SchetsControl s, Point p)
        {   kwast = new SolidBrush(s.PenKleur);
        }
        public abstract void MuisDrag(SchetsControl s, Point p);
        public abstract void Letter(SchetsControl s, char c);
    }

    public class TekstTool : StartpuntTool
    {
        public override string ToString() { return "tekst"; }

        public override void MuisDrag(SchetsControl s, Point p) { }

        public override void Letter(SchetsControl s, char c)
        {
            if (c >= 32)
            {
                Graphics gr = s.MaakBitmapGraphics();
                Font font = new Font("Tahoma", 40);
                string tekst = c.ToString();
                SizeF sz = 
                gr.MeasureString(tekst, font, this.startpunt, StringFormat.GenericTypographic);
                gr.DrawString   (tekst, font, kwast, 
                                              this.startpunt, StringFormat.GenericTypographic);
                // gr.DrawRectangle(Pens.Black, startpunt.X, startpunt.Y, sz.Width, sz.Height);
                this.bouwsteen = new TekstSteen(startpunt.X, startpunt.Y, c, font, kwast);
                ((TekstSteen)this.bouwsteen).Grootte= new Size((int)sz.Width, (int)sz.Height);
                s.AddBouwsteen(this.bouwsteen);
                startpunt.X += (int)sz.Width; 
                s.Invalidate();
            }
        }
    }

    public abstract class TweepuntTool : StartpuntTool
    {
        public static Rectangle Punten2Rechthoek(Point p1, Point p2)
        {   return new Rectangle( new Point(Math.Min(p1.X,p2.X), Math.Min(p1.Y,p2.Y))
                                , new Size (Math.Abs(p1.X-p2.X), Math.Abs(p1.Y-p2.Y))
                                );
        }
        public static Pen MaakPen(Brush b, int dikte)
        {   Pen pen = new Pen(b, dikte);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }
        public override void MuisVast(SchetsControl s, Point p)
        {   base.MuisVast(s, p);
            kwast = Brushes.Gray;
        }
        public override void MuisDrag(SchetsControl s, Point p)
        {   s.Refresh();
            this.Bezig(s.CreateGraphics(), this.startpunt, p);
        }
        public override void MuisLos(SchetsControl s, Point p)
        {   base.MuisLos(s, p);
            this.Compleet(s.MaakBitmapGraphics(), this.startpunt, p);
            s.Invalidate();
        }
        public override void Letter(SchetsControl s, char c)
        {
        }
        public abstract void Bezig(Graphics g, Point p1, Point p2);

        public virtual void Compleet(Graphics g, Point p1, Point p2)
        {
           this.Bezig(g, p1, p2);
        }
    }

    public class RechthoekTool : TweepuntTool
    {
        public override string ToString() { return "kader"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {   g.DrawRectangle(MaakPen(kwast,3), TweepuntTool.Punten2Rechthoek(p1, p2));
        this.bouwsteen = new RechthoekSteen(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), MaakPen(this.kwast, 3), new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y)));
        }

        public override void MuisLos(SchetsControl s, Point p)
        {
           base.MuisLos(s, p);
           if (this.bouwsteen == null)
              this.bouwsteen = new RechthoekSteen(Math.Min(startpunt.X, p.X), Math.Min(p.Y, startpunt.Y), MaakPen(this.kwast, 3),
                                              new Size(Math.Abs(p.X - startpunt.X), Math.Abs(p.Y - startpunt.Y)));
           s.AddBouwsteen(this.bouwsteen);
        }
    }
    
    public class VolRechthoekTool : RechthoekTool
    {
        public override string ToString() { return "vlak"; }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {   g.FillRectangle(kwast, TweepuntTool.Punten2Rechthoek(p1, p2));
        this.bouwsteen = new VolRechthoekSteen(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), this.kwast, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y)));
        }

        public override void MuisLos(SchetsControl s, Point p)
        {
           base.MuisLos(s, p);
           if (this.bouwsteen == null)
              this.bouwsteen = new VolRechthoekSteen(Math.Min(startpunt.X, p.X), Math.Min(p.Y, startpunt.Y), this.kwast,
                                              new Size(Math.Abs(p.X - startpunt.X), Math.Abs(p.Y - startpunt.Y)));
           s.AddBouwsteen(this.bouwsteen);
        }
    }

    public class LijnTool : TweepuntTool
    {
        public override string ToString() { return "lijn"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {   g.DrawLine(MaakPen(this.kwast,3), p1.X, p1.Y, p2.X, p2.Y);
            this.bouwsteen = new LijnSteen(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), MaakPen(this.kwast, 3), new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y)));
       } 

        public override void MuisLos(SchetsControl s, Point p)
        {
           base.MuisLos(s, p);
           this.bouwsteen = new LijnSteen(startpunt.X, startpunt.Y, MaakPen(this.kwast, 3), new Size(p.X - startpunt.X, p.Y - startpunt.Y));
           s.AddBouwsteen(this.bouwsteen);
        }

    }

   public class CirkelTool : TweepuntTool
   {
      public override string ToString() { return "Cirkel";  }

      public override void Bezig(Graphics g, Point p1, Point p2)
        {   g.DrawEllipse(MaakPen(kwast,3), TweepuntTool.Punten2Rechthoek(p1, p2));
        this.bouwsteen = new CirkelSteen(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), MaakPen(this.kwast, 3), new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y)));
        }

      public override void MuisLos(SchetsControl s, Point p)
      {
         base.MuisLos(s, p);
         if (this.bouwsteen == null)
            this.bouwsteen = new CirkelSteen(Math.Min(startpunt.X, p.X), Math.Min(p.Y, startpunt.Y), MaakPen(this.kwast, 3),
                                            new Size(Math.Abs(p.X - startpunt.X), Math.Abs(p.Y - startpunt.Y)));
         s.AddBouwsteen(this.bouwsteen);
      }
    }

   public class VolCirkelTool : CirkelTool
   {
      public override string ToString() { return "Disc"; }

      public override void Compleet(Graphics g, Point p1, Point p2)
      {
         g.FillEllipse(kwast, TweepuntTool.Punten2Rechthoek(p1, p2));
         this.bouwsteen = new VolCirkelSteen(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), this.kwast, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y)));
      }

      public override void MuisLos(SchetsControl s, Point p)
      {
         base.MuisLos(s, p);
         if (this.bouwsteen == null)
            this.bouwsteen = new VolCirkelSteen(Math.Min(startpunt.X, p.X), Math.Min(p.Y, startpunt.Y), this.kwast,
                                            new Size(Math.Abs(p.X - startpunt.X), Math.Abs(p.Y - startpunt.Y)));
         s.AddBouwsteen(this.bouwsteen);
      }
   }

    public class PenTool : LijnTool
    {
        public override string ToString() { return "pen"; }

        public override void MuisDrag(SchetsControl s, Point p)
        {
           if (this.bouwsteen == null)
              this.bouwsteen = new PenSteen(Math.Min(startpunt.X, p.X), Math.Min(p.Y, startpunt.Y), MaakPen(this.kwast, 3));
           s.AddBouwsteen(this.bouwsteen);
           this.MuisLos(s, p);
            this.MuisVast(s, p);
        }


    }
    
    public class GumTool : ISchetsTool
    {
       public override string ToString() { return "gum"; }
       public void MuisLos(SchetsControl s, Point p)
       {
          s.RemoveBouwsteen(p.X, p.Y);
       }
       public  void MuisVast(SchetsControl s, Point p)
       { }
       public  void MuisDrag(SchetsControl s, Point p)
       { }
       public void Letter(SchetsControl s, char c)
       { }
        
    }
}
