using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.ComponentModel;

namespace ImagingSortCharlie
{
  partial class KeyButton : Button
  {
    public KeyButton()
    {
      InitializeComponent();
      curve = 4;
    }
    public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
    {
      GraphicsPath path = new GraphicsPath();
      int l = rectangle.Left;
      int t = rectangle.Top;
      int w = rectangle.Width;
      int h = rectangle.Height;
      int d = radius << 1;
      if (w == h)
      {
        path.AddArc(rectangle, 0, 360);
        //path.CloseAllFigures();
        return path;
      }
      if ((w % 2) == 0)
        d -= 1;
      path.AddArc(l, t, d, d, 180, 90); // topleft
      path.AddLine(l + radius, t, l + w - radius, t); // top
      path.AddArc(l + w - d, t, d, d, 270, 90); // topright
      path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
      path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
      path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
      path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
      path.AddLine(l, t + h - radius, l, t + radius); // left
      path.CloseFigure();
      return path;
    }
    private bool isChecked = false;
    private bool isDown = false;
    private bool isOver = false;
    public bool Checked
    {
      get { return isChecked; }
      set { isChecked = value; Refresh(); }
    }
    private int curve = 0;
    /// <summary>
    /// Gets or sets the background color of the control.
    /// </summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
    [DefaultValue(typeof(int), "-1"), Category("Appearance"), Description("圆边半径，-1表示高度的一半")]
    public int Curve
    {
      get { return curve; }
      set
      {
        if (!curve.Equals(value))
        {
          curve = value;
          //OnBackColorChanged(EventArgs.Empty);
          Reset();
          this.Refresh();
        }
      }
    }
    private bool enableCheck = false;
    /// <summary>
    /// Gets or sets the background color of the control.
    /// </summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
    [DefaultValue(typeof(bool), "false"), Category("Appearance"), Description("是否CheckBox样式")]
    public bool CheckBox
    {
      get { return enableCheck; }
      set
      {
        if (!enableCheck.Equals(value))
        {
          enableCheck = value;
          //OnBackColorChanged(EventArgs.Empty);
          Reset();
          this.Refresh();
        }
      }
    }

    GraphicsPath gp1 = null;
    GraphicsPath gp2 = null;
    private void Reset()
    {
      Rectangle rcClient = this.ClientRectangle;
      //rcClient.Inflate(-1, -1);
      rcClient.Width--;
      rcClient.Height--;
      gp1 = CreateRoundRectangle(rcClient, curve);
      Rectangle rc = rcClient;
      rc.Inflate(-3, -2);
      rc.Width -= 2;
      rc.Height -= 2;
      if (isDown)
        rc.Location = new Point(rc.Left + 1, rc.Top);
      else
        rc.Location = new Point(rc.Left, rc.Top - 1);
      gp2 = CreateRoundRectangle(rc, curve / 2);
    }
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      Color cl = this.BackColor;
      using (Brush br = new SolidBrush(cl))
        pevent.Graphics.FillRectangle(br, this.ClientRectangle);
    }

    public static double Distance(Point a, Point b)
    {
      double xdiff = a.X - b.X;
      double ydiff = a.Y - b.Y;
      return Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
    }
    private Color gradient_back = Color.DarkGray;
    private Color gradient_front = Color.WhiteSmoke;
    private Color checked_back = Color.SaddleBrown;
    private Color frame_color = Color.Gray;
    private Color over_back = Color.DimGray;
    private Color over_front = Color.White;
    [DefaultValue(typeof(Color), "Color.Gray"), Category("Appearance"), Description("边框色")]
    public Color FrameColor { get { return frame_color; } set { frame_color = value; Refresh(); } }
    [DefaultValue(typeof(Color), "Color.DarkGray"), Category("Appearance"), Description("渐变背景")]
    public Color GradientBack { get { return gradient_back; } set { gradient_back = value; Refresh(); } }
    [DefaultValue(typeof(Color), "Color.WhiteSmoke"), Category("Appearance"), Description("渐变前景")]
    public Color GradientFront { get { return gradient_front; } set { gradient_front = value; Refresh(); } }
    [DefaultValue(typeof(Color), "Color.SaddleBrown"), Category("Appearance"), Description("选中时渐变前景")]
    public Color CheckedBack { get { return checked_back; } set { checked_back = value; Refresh(); } }
    [DefaultValue(typeof(Color), "Color.DimGray"), Category("Appearance"), Description("停留时渐变前景")]
    public Color OverFront { get { return over_front; } set { over_front = value; Refresh(); } }
    [DefaultValue(typeof(Color), "Color.White"), Category("Appearance"), Description("停留时渐变背景")]
    public Color OverBack { get { return over_back; } set { over_back = value; Refresh(); } }

    protected override void OnPaint(PaintEventArgs pevent)
    {
      if (gp1 == null)
        Reset();

      Rectangle cr = ClientRectangle;
      OnPaintBackground(pevent);
      Point pt1;
      //Point pt2;

      pt1 = Cursor.Position;//new Point(ClientRectangle.Left, ClientRectangle.Top);
      pt1 = this.PointToClient(pt1);
      if (cr.Contains(pt1))
      {
        pt1.Y = this.ClientSize.Height;
        //         Rectangle rc1 = new Rectangle(cr.Left, cr.Top,
        //           cr.Width / 2, cr.Height / 2);
        //         Rectangle rc2 = new Rectangle(cr.Left + cr.Width / 2, cr.Top, cr.Width / 2, cr.Height / 2);
        //         Rectangle rc3 = new Rectangle(cr.Left, cr.Top + cr.Height / 2, cr.Width / 2, cr.Height / 2);
        //         Rectangle rc4 = new Rectangle(cr.Left + cr.Width / 2, cr.Top + cr.Height / 2, cr.Width / 2, cr.Height / 2);
        //         if (rc1.Contains(pt1))
        //           pt2 = new Point(cr.Right, cr.Bottom);
        //         else if (rc2.Contains(pt1))
        //           pt2 = new Point(cr.Left, cr.Bottom);
        //         else if (rc3.Contains(pt1))
        //           pt2 = new Point(cr.Right, cr.Top);
        //         else //if (rc4.Contains(pt1))
        //           pt2 = new Point(cr.Left, cr.Top);
      }
      else
      {
        pt1 = new Point(cr.Right, cr.Top);
        //         pt2 = new Point(cr.Left, cr.Bottom);
      }
      //pt2 = new Point(ClientRectangle.Right, ClientRectangle.Bottom);

      if (isChecked || isDown)
      {
        //         Point pt3 = pt1;
        //         pt1 = pt2;
        //         pt2 = pt3;
      }

      Color frame = FrameColor;
      Color from = gradient_front;
      Color to = GradientBack;
      if (isOver && !isDown)
      {
        from = over_front;
        to = over_back;
      }
      if (isChecked)
      {
        from = GradientFront;
        to = checked_back;
      }
      Graphics g = pevent.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      //       using (LinearGradientBrush br =
      //         new LinearGradientBrush(pt1, pt2, from, to))
      GraphicsPath circle = new GraphicsPath();
      float diameter = (float)Math.Sqrt(cr.Width * cr.Width + cr.Height * cr.Height);
      float radius = diameter / 2;
      circle.AddArc(cr.Left + cr.Width / 2 - radius, cr.Top + cr.Height / 2 - radius,
        diameter, diameter, 0, 360);
      using (PathGradientBrush br = new PathGradientBrush(circle))
      {
        br.CenterColor = from;
        br.CenterPoint = pt1;
        br.SurroundColors = new Color[] { to };
        //         br.WrapMode = WrapMode.TileFlipXY;
        g.FillPath(br, gp1);
      }
      using (Pen br1 = new Pen(frame))
        g.DrawPath(br1, gp1);

      Color clDown = Color.White;
      Color textColor = this.ForeColor;

      if (isDown)
      {
        //clDown = Color.Black;
        //textColor = Color.White;
      }
      using (Brush br2 = new SolidBrush(Color.FromArgb(0x80, clDown)))
      {
        g.FillPath(br2, gp2);
      }
      using (Pen pen = new Pen(Color.FromArgb(0x90, Color.White)))
        g.DrawPath(pen, gp2);
      //base.OnPaint(pevent);

      StringFormat sf = new StringFormat();
      sf.Alignment = StringAlignment.Center;
      sf.LineAlignment = StringAlignment.Center;
      sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;

      if (!this.Enabled)
        textColor = SystemColors.GrayText;
      RectangleF bound = gp2.GetBounds();
      bound.Offset(0, 1);
      using (Brush br3 = new SolidBrush(textColor))
        g.DrawString(this.Text, this.Font, br3, bound, sf);
    }

    private void RoundButton_Resize(object sender, EventArgs e)
    {
      Reset();
      Refresh();
    }

    private void RoundButton_MouseDown(object sender, MouseEventArgs e)
    {
      isDown = true;
      Reset();
      Refresh();
    }

    private void RoundButton_MouseUp(object sender, MouseEventArgs e)
    {
      isDown = false;
      Reset();
      Refresh();
    }

    private void RoundButton_MouseEnter(object sender, EventArgs e)
    {
      isOver = true;
      Refresh();
    }

    private void RoundButton_MouseLeave(object sender, EventArgs e)
    {
      isOver = false;
      Refresh();
    }

    private void RoundButton_MouseMove(object sender, MouseEventArgs e)
    {
      Refresh();
    }

    private void RoundButton_MouseClick(object sender, MouseEventArgs e)
    {
      //       isChecked = !isChecked;
      //       Reset();
      //       Refresh();
    }

    private void RoundButton_Click(object sender, EventArgs e)
    {
      if (enableCheck)
        isChecked = !isChecked;
      Reset();
      Refresh();
    }

  }
}
