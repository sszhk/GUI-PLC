using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Forms
{
  public partial class SettingFillArea : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    Data.Filters.CalcFillArea filter = null;
    public SettingFillArea(CalcFillArea fil)
    {
      filter = fil;
      view = fil.PublicOptions.View;
      InitializeComponent();
    }
    public UserControl GetControl()
    {
      return this;
    }
    public void OnOK()
    {
      filter.Name = tbName.Text;
      filter.FillAreaOptions.IsWhite = rb_white.Checked;
    }
    public void OnCancel()
    {
      ReadData();
    }
    public int GetCorrectionCount()
    {
      return 0;
    }
    public bool OnCorrection()
    {
      return true;
    }

    public void ReadData()
    {
      tbName.Text = filter.Name;
      rb_white.Checked = filter.FillAreaOptions.IsWhite;
      rb_black.Checked = !filter.FillAreaOptions.IsWhite;
    }

    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }
    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.FillAreaOptions.IsWhite = rb_white.Checked;
      GD.PreviewCurrent();
    }

    private void SettingFillArea_Load(object sender, EventArgs e)
    {
      ReadData();
    }
  }
}
