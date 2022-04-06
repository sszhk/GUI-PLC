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
  public partial class SettingThreadDamage : UserControl, ISetting
  {
    public SettingThreadDamage()
    {
      InitializeComponent();
    }

    VIEW View = VIEW.NONE;
    CalcThreadDameged filter = null;
    const int CORRECTIONCOUNT = 1;
    public SettingThreadDamage(CalcThreadDameged fil)
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
      filter.Name = tbName.Text;
      filter.ThreadDamageOptions.IsThreadDamaged = cbThreadDamaged.Checked;
      float.TryParse(tbThreadDameged.Text, out filter.ThreadDamageOptions.ThreadDamage);
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
      filter.ThreadDamageOptions.Correct = true;
      filter.Apply(null);
      filter.ThreadDamageOptions.Offset = filter.ThreadDamageOptions.Rectangle.Top - filter.ThreadDamageReport.top;
      filter.ThreadDamageOptions.Correct = false;
      GD.PreviewCurrent();
      return true;
    }

    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbThreadDameged.Text = filter.ThreadDamageOptions.ThreadDamage.ToString();
      cbThreadDamaged.Checked = filter.ThreadDamageOptions.IsThreadDamaged;
      tbWidth.Value = filter.ThreadDamageOptions.Width;
      tbHeight.Value = filter.ThreadDamageOptions.Height;
      tb_contrast.Value = (decimal)filter.ThreadDamageOptions.Contrast;
      cb_rotated.Checked = filter.ThreadDamageOptions.Rotated;
    }

    private void SettingThreadDameged_Load(object sender, EventArgs e)
    {
      ReadData();
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



    private void tbHeight_ValueChanged(object sender, EventArgs e)
    {
      filter.ThreadDamageOptions.Height = (int)tbHeight.Value;
      GD.PreviewCurrent();
    }

    private void tbWidth_ValueChanged(object sender, EventArgs e)
    {
      filter.ThreadDamageOptions.Width = (int)tbWidth.Value;
      GD.PreviewCurrent();
    }

    private void tb_contrast_ValueChanged(object sender, EventArgs e)
    {
      filter.ThreadDamageOptions.Contrast = (float)tb_contrast.Value;
      GD.PreviewCurrent();
    }

    private void cb_rotated_CheckedChanged(object sender, EventArgs e)
    {
      filter.ThreadDamageOptions.Rotated = cb_rotated.Checked;
      GD.PreviewCurrent();
    }

  }
}
