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
  public partial class SettingCushion : UserControl, ISetting
  {
    public SettingCushion()
    {
      InitializeComponent();
    }
    VIEW View = VIEW.NONE;
    CalcCushion filter = null;
    const int CORRECTIONCOUNT = 1;
    public SettingCushion(CalcCushion fil)
    {
      filter = fil;
      View = fil.PublicOptions.View;
      InitializeComponent();
    }
    public UserControl GetControl()
    {
      return this;
    }
    public void OnOK()
    {
      filter.Name = tb_name.Text;
      filter.CushionOptions.IsWhite = rb_white.Checked;
      filter.InputValues.Height = cbExpectedCushionHeight.Checked;
      filter.InputValues.Width = cb_width.Checked;
      float.TryParse(tbMaxCushionHeight.Text, out filter.InputValues.ExpMaxCushionHeight);
      float.TryParse(tbMinCushionHeight.Text, out filter.InputValues.ExpMinCushionHeight);
      float.TryParse(tb_max_width.Text, out filter.InputValues.ExpMaxWidth);
      float.TryParse(tb_min_width.Text, out filter.InputValues.ExpMinWidth);
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
      tb_name.Text = filter.Name;
      rb_white.Checked = filter.CushionOptions.IsWhite;
      rb_black.Checked = !filter.CushionOptions.IsWhite;
      cbExpectedCushionHeight.Checked = filter.InputValues.Height;
      cb_width.Checked = filter.InputValues.Width;
      tbMinCushionHeight.Text = filter.InputValues.ExpMinCushionHeight.ToString();
      tbMaxCushionHeight.Text = filter.InputValues.ExpMaxCushionHeight.ToString();
      tb_min_width.Text = filter.InputValues.ExpMinWidth.ToString();
      tb_max_width.Text = filter.InputValues.ExpMaxWidth.ToString();
    }

    private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
    {

    }
    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }

    private void SettingThreadLocating_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    private void rb_white_Click(object sender, EventArgs e)
    {
      filter.CushionOptions.IsWhite = rb_white.Checked;
      GD.PreviewCurrent();
    }
  }
}
