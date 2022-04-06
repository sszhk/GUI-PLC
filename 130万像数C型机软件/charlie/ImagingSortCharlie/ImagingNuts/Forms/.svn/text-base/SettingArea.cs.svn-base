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
  public partial class SettingArea : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    CalcArea filter = null;
    int CorrectionCount = 0;
    float AreaCorrection = 0;
    const int CORRECTIONCOUNT = 1;

    public SettingArea(CalcArea fil)
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
      filter.Name = tb_name.Text;
      float.TryParse(tb_max_area.Text, out filter.InputValues.ExpMaxArea);
      float.TryParse(tb_min_area.Text, out filter.InputValues.ExpMinArea);
      filter.AreaOptions.IsWhite = rbWhiteHead.Checked;
      filter.InputValues.Area = cb_area.Checked;
    }

    public void OnCancel()
    {
      ReadData();
    }

    public int GetCorrectionCount()
    {
      return CorrectionCount;
    }

    public bool OnCorrection()
    {
      if (CorrectionCount == 0)
      {
        CorrectionCount = CORRECTIONCOUNT;
        AreaCorrection = 0;
      }
      GD.PreviewCurrent();
      if (!filter.Result)
        return false;
      AreaCorrection += filter.AreaReport.area;
      CorrectionCount--;
      if (CorrectionCount == 0)
        filter.InputValues.AreaCorrection = AreaCorrection / CORRECTIONCOUNT;
      GD.PreviewView(view);
      return true;
    }

    public void ReadData()
    {
      tb_name.Text = filter.Name;
      tb_max_area.Text = filter.InputValues.ExpMaxArea.ToString();
      tb_min_area.Text = filter.InputValues.ExpMinArea.ToString();
      rbWhiteHead.Checked = filter.AreaOptions.IsWhite;
      rbBlackHead.Checked = !filter.AreaOptions.IsWhite;
      cb_area.Checked = filter.InputValues.Area;
    }

    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }

    private void SettingArea_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.AreaOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }

    private void cb_teeth_CheckedChanged(object sender, EventArgs e)
    {
      filter.InputValues.RelyTeeth = cb_teeth.Checked;
      if (filter.InputValues.RelyTeeth)
        cb_locating.Checked = false;
      else
        GD.PreviewCurrent();
    }

    private void cb_locating_CheckedChanged(object sender, EventArgs e)
    {
      filter.InputValues.RelyLocting = cb_locating.Checked;
      if (filter.InputValues.RelyLocting)
        cb_teeth.Checked = false;
      else
        GD.PreviewCurrent();
    }

  }
}
