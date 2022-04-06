using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ImagingSortCharlie
{
  public class GradientSeparator : Label
  {
    bool vertical = true;
    [DefaultValue(typeof(bool), "true"), Category("Appearance"), Description("Horizontal or Vertical")]
    public bool Vertical
    {
      get { return vertical; }
      set
      {
        vertical = value;
        this.Refresh();
      }
    }
    float width = 1;
    [DefaultValue(typeof(float), "1"), Category("Appearance"), Description("Width of Line")]
    public float LineWidth
    {
      get
      {
        return width;
      }
      set
      {
        width = value;
        Refresh();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Rectangle rc = this.ClientRectangle;

      int x = rc.Left + rc.Width / 2;
      int y1 = rc.Top + rc.Height / 8;
      int y2 = rc.Bottom - rc.Height / 8;

      int y = rc.Top + rc.Height / 2;
      int x1 = rc.Left + rc.Width / 8;
      int x2 = rc.Right - rc.Width / 8;
      Point p1, p2, p3, p4;
      if( vertical )
      {
        p1 = new Point(x, rc.Top);
        p2 = new Point(x, y1);
        p3 = new Point(x, y2);
        p4 = new Point(x, rc.Bottom);
      }
      else
      {
        p1 = new Point(rc.Left, y);
        p2 = new Point(x1, y);
        p3 = new Point(x2, y);
        p4 = new Point(rc.Right, y);
      }

      LinearGradientBrush br1 = new LinearGradientBrush(p1, p2, Color.Transparent, this.ForeColor);
      LinearGradientBrush br2 = new LinearGradientBrush(p3, p4, this.ForeColor, Color.Transparent);
      Pen pen1 = new Pen(br1);
      Pen pen2 = new Pen(br2);
      Pen pen3 = new Pen(this.ForeColor);
      pen1.Width = pen2.Width = pen3.Width = width;
      g.DrawLine(pen1, p1, p2);
      g.DrawLine(pen3, p2, p3);
      g.DrawLine(pen2, p3, p4);

      pen3.Dispose();
      pen2.Dispose();
      pen1.Dispose();
      br1.Dispose();
      br2.Dispose();
    }
  }
}
