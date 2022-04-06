using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImagingSortCharlie.Controls
{
  public partial class SimpleHeader : Label
  {
    public SimpleHeader()
    {
      InitializeComponent();
    }

    public SimpleHeader(IContainer container)
    {
      container.Add(this);

      InitializeComponent();
    }
    ListView.ColumnHeaderCollection c;
    public void rearrange(ListView x)
    {
      c = x.Columns;
      int height = get_column_height(x);
      if (height == 0)
        this.Visible = false;
      this.Location = x.Location;
      this.Width = x.ClientRectangle.Width;
      this.Height = height;
    }
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      Rectangle rc = this.ClientRectangle;
      using(Brush br = new SolidBrush(this.BackColor))
      {
        pevent.Graphics.FillRectangle(br, rc);
      }
      //base.OnPaintBackground(pevent);
    }
    int padleft = 5;
    protected override void OnPaint(PaintEventArgs e)
    {
      //base.OnPaint(e);
      if (c == null)
      {
        base.OnPaint(e);
        return;
      }
      int left = 0;
      for (int i = 0; i < c.Count; i++ )
      {
        Rectangle rc = new Rectangle(left+padleft, 0, c[i].Width, this.Height);
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Near;
        sf.LineAlignment = StringAlignment.Center;
        using(Brush br = new SolidBrush(this.ForeColor))
          e.Graphics.DrawString(c[i].Text, c[i].ListView.Font, br, rc, sf);
        left += c[i].Width;

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Point pt1 = new Point(rc.Right-padleft, 4);
        Point pt2 = new Point(rc.Right-padleft, Height - 4);
        using(Pen p = new Pen(Color.FromArgb(0x20, Color.Black)))
        {
          e.Graphics.DrawLine(p, pt1, pt2);
        }
        e.Graphics.SmoothingMode = SmoothingMode.Default;
      }
      Point pt3 = new Point(0, this.Height-1);
      Point pt4 = new Point(this.Width, this.Height-1);
      using(Pen p = new Pen(Color.FromArgb(0x20, Color.Black)))
        e.Graphics.DrawLine(p, pt3, pt4);
    }

    const long LVM_FIRST = 0x1000;
    const long LVM_GETHEADER = (LVM_FIRST + 31);

    [DllImport("user32.dll", EntryPoint="SendMessage")]
    private static extern IntPtr SendMessage(IntPtr hwnd, long wMsg, long wParam, long lParam);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);

    private int get_column_height(ListView lv)
    {
      RECT rc = new RECT();
      IntPtr hwnd = SendMessage(lv.Handle, LVM_GETHEADER, 0, 0);
      if (hwnd != null)
      {
        if (GetWindowRect(new HandleRef(null, hwnd), out rc))
        {
          int headerHeight = rc.Bottom - rc.Top;
          return headerHeight;
        }
      }
      return 0;
    }
  }
  [Serializable, StructLayout(LayoutKind.Sequential)]
  public struct RECT 
  {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
  }

}
