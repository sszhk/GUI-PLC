using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagingSortCharlie
{
  public partial class ListViewEx : ListView
  {
    public ListViewEx()
    {
      InitializeComponent();
    }

    public ListViewEx(IContainer container)
    {
      container.Add(this);
      // Activate double buffering
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

      // Enable the OnNotifyMessage event so we get a chance to filter out 
      // Windows messages before they get to the form's WndProc
      this.SetStyle(ControlStyles.EnableNotifyMessage, true);
      InitializeComponent();
    }
    public delegate void ListViewScroll(object sender, bool vscroll);
    public event ListViewScroll OnScroll;
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);

      //System.Diagnostics.Debug.Print("0x{0:x}", m.Msg);
      if (m.Msg == 0x114 || m.Msg == 0x115 || m.Msg == 0x20a ||
        (m.Msg >= 0x100 && m.Msg <= 0x107) || m.Msg == 0x0202 || m.Msg == 0x0201)
      {
        if (OnScroll != null)
        {
          OnScroll(this, m.Msg == 0x115);
        }
      }

    }
    protected override void OnNotifyMessage(Message m)
    {
      //Filter out the WM_ERASEBKGND message
      if (m.Msg != 0x14)
      {
        base.OnNotifyMessage(m);
      }
    }
  }
}
