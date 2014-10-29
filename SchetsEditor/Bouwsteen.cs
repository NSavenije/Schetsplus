using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SchetsEditor
{
   class Bouwsteen
   {
      private ISchetsTool tool;
      private Color tint;
      private Point start, eind;
      private string text;
      private Font font;
      private SizeF grootte;
      private Brush kwast;
      public ISchetsTool Soort
      {
         get { return tool; }
         set { tool = value; }
      }

      public Color kleur
      {
         get { return tint; }
         set { tint = value; }
      }

      public Point begin
      {
         get { return start; }
         set { start = value; }
      }

      public Point einde
      {
         get { return eind; }
         set { eind = value; }
      }

      public string inhoud
      {
         get { return text; }
         set { text = value; }
      }

      public Font type
      {
         get { return font; }
         set { font = value; }
      }

      public SizeF formaat
      {
         get { return grootte; }
         set { grootte = value; }
      }

      public Brush brush
      {
         get { return kwast; }
         set { kwast = value; }
      }
   }
}
