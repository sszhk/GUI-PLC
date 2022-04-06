using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagingSortCharlie.Controls
{
  public class FlickerFreeList : ListView
  {
    public FlickerFreeList()
    {
      // Enable internal ListView double-buffering
      SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
      // Disable default CommCtrl painting on non-XP systems
      if (!NativeInterop.IsWinXP)
        SetStyle(ControlStyles.UserPaint, true);
    }
    protected override void OnPaint(PaintEventArgs e)
    {
      if (GetStyle(ControlStyles.UserPaint))
      {
        Message m = new Message();
        m.HWnd = Handle;
        m.Msg = NativeInterop.WM_PRINTCLIENT;
        m.WParam = e.Graphics.GetHdc();
        m.LParam = (IntPtr)NativeInterop.PRF_CLIENT;
        DefWndProc(ref m);
        e.Graphics.ReleaseHdc(m.WParam);
      }
      base.OnPaint(e);
    }
  }
}
