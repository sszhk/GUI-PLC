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
  public partial class SettingBinarize : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.ImageBinarize filter = null;
    public SettingBinarize(ImageBinarize fil)
    {
      filter = fil;
      View = fil.PublicOptions.View;
      InitializeComponent();

    }
    public UserControl GetControl()
    {
      return this;
    }
    private void Preview()
    {
      GD.PreviewView(filter.PublicOptions.View);
    }
    private void knob_KnobChangeValue(object sender, LBSoft.IndustrialCtrls.Knobs.LBKnobEventArgs e)
    {
      rbManual.Checked = true;
      tbThres.Text = knob.Value.ToString("0");
      filter.InterVariance = false;
      filter.PublicOptions.Threshold = (int)knob.Value;
      Preview();
    }
    public void OnOK()
    {
      filter.Name = tbName.Text;
      filter.InterVariance = rbInterVar.Checked;
      filter.PublicOptions.Threshold = (int)tbThres.Value;
      filter.InterVariance = !rbManual.Checked;
    }
    public void OnCancel()
    {
    }
    public int GetCorrectionCount()
    {
      return 0;
    }
    public bool OnCorrection()
    {
      return true;
    }
    private void rbInterVar_CheckedChanged(object sender, EventArgs e)
    {
      rbManual.Checked = !rbInterVar.Checked;
      tbThres.Enabled = rbManual.Checked;
      filter.InterVariance = rbInterVar.Checked;
      Preview();
    }

    private void rbManual_CheckedChanged(object sender, EventArgs e)
    {
      tbThres.Enabled = true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      knob.Value = filter.PublicOptions.Threshold;
      tbThres.Text = filter.PublicOptions.Threshold.ToString();
      cbReverse.Checked = filter.BinarizeOptions.Inverse;
      if (filter.InterVariance)
        rbInterVar.Checked = true;
      else
        rbManual.Checked = true;
      rbFullScreen.Checked = filter.IsFullScreen;
      rbPartial.Checked = !filter.IsFullScreen;
    }
    private void SettingBinarize_Load(object sender, EventArgs e)
    {
      ReadData();
    }
    private void tbThres_ValueChanged(object sender, EventArgs e)
    {
      knob.Value = (float)tbThres.Value;
    }
    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
      else
        OnOK();
    }

    private void rbFullScreen_CheckedChanged(object sender, EventArgs e)
    {
      filter.IsFullScreen = rbFullScreen.Checked;
      if (!rbFullScreen.Checked)
      {
        VIEW view = GD.CurrentView;
        filter.ShowTool();
      }
      else
        filter.HideTool();
      Preview();
    }

    private void cbReverse_CheckedChanged(object sender, EventArgs e)
    {
      filter.BinarizeOptions.Inverse = cbReverse.Checked;
      Preview();
    }


  }
}
