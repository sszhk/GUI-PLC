using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImagingSortCharlie
{
  public partial class FramedPicture : PictureBox
  {
    private static List<FramedPicture> tbl = new List<FramedPicture>();


//     private CheckBox ck = new CheckBox();
    private void addToList()
    {
      tbl.Add(this);
    }
    public FramedPicture()
    {
      InitializeComponent();
      addToList();
    }

    Color normColor = Color.White;
    Color overColor = Color.SteelBlue;
    Color frmColor = Color.Blue;
    //bool framed = false;
    bool is_checked = false;
    // -1 代表没有组（行为类似Checkbox）
    int grp = -1;
    bool is_over = false;

    public Color NormalColor { get { return normColor; } set { normColor = value; Refresh(); } }
    public Color OverColor { get { return overColor; } set { overColor = value; Refresh(); } }
    public Color FrameColor { get { return frmColor; } set { frmColor = value; Refresh(); } }
    //public bool Framed { get { return framed; } set { framed = value; Refresh(); } }
    public int Group { get { return grp; } set { grp = value; Refresh(); } }
    public bool Checked { get { return is_checked; } set { is_checked = value; Refresh(); } }

    public FramedPicture(IContainer container)
    {
      container.Add(this);

      InitializeComponent();
      addToList();

//       this.Controls.Add(ck);
//       ck.Text = "";
//       ck.AutoSize = true;
// 
//       Rectangle rcCk = ck.ClientRectangle;
//       Rectangle rcThis = this.ClientRectangle;
//       ck.Location = new Point(
//         (rcThis.Width-rcCk.Width)/2,
//         (rcThis.Height-rcCk.Height)/2);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);

      int width = this.ClientRectangle.Width-4;
      if (this.Image != null)
        width = this.Image.Width;

      // 假设边框水平/垂直方向留空相同
      int delta = this.ClientRectangle.Width - width;
      int delta2 = delta / 2;

      Color cl = normColor;
      if (is_checked)
        cl = frmColor;
      if (is_over)
        cl = overColor;
      using(Pen p=new Pen(cl, delta2))
      {
        Rectangle rc = new Rectangle();
        rc.Location = new Point(delta2, delta2);
        rc.Width = this.ClientRectangle.Width-delta;
        rc.Height = this.ClientRectangle.Height-delta;
        pe.Graphics.DrawRectangle(p, rc);
      }
    }

    private void inverseCheck()
    {
      if (this.Group == -1)
      {
        this.Checked = !this.Checked;
        return;
      }
      foreach (FramedPicture fp in tbl)
      {
        if (fp == this)
        {
          this.Checked = true;
          continue;
        }
        if( fp.Group == this.Group )
        {
          fp.Checked = false;
        }
      }
    }
    private void FramedPicture_Click(object sender, EventArgs e)
    {
      if (this.Checked)
        return;

      inverseCheck();
    }

    private void FramedPicture_MouseEnter(object sender, EventArgs e)
    {
      is_over = true;
      Refresh();
    }

    private void FramedPicture_MouseLeave(object sender, EventArgs e)
    {
      is_over = false;
      Refresh();
    }
  }
}
