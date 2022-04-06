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
  public partial class SettingMarking : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    Data.Filters.CalcMarking filter = null;
    public SettingMarking(CalcMarking fil)
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
      float.TryParse(tbMaxArea.Text, out filter.MarkingOptions.MaxMarkingArea);
      float.TryParse(tbMinArea.Text, out filter.MarkingOptions.MinMarkingArea);
      float.TryParse(tbMaxRoundness.Text, out filter.MarkingOptions.MaxRoundness);
      float.TryParse(tbMinRoundness.Text, out filter.MarkingOptions.MinRoundness);
      int.TryParse(tbMarkingCount.Text, out filter.InputValues.ExpMarkingCount);
      filter.InputValues.MarkingCount=cbMarkingCount.Checked;
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
      cbMarkingCount.Checked = filter.InputValues.MarkingCount;
      rbWhiteMarking.Checked = filter.MarkingOptions.IsWhite;
      rbBlackMarking.Checked = !filter.MarkingOptions.IsWhite;
      tbMaxArea.Text = filter.MarkingOptions.MaxMarkingArea.ToString();
      tbMinArea.Text = filter.MarkingOptions.MinMarkingArea.ToString();
      tbMaxRoundness.Text = filter.MarkingOptions.MaxRoundness.ToString();
      tbMinRoundness.Text = filter.MarkingOptions.MinRoundness.ToString();
      tbMarkingCount.Text = filter.InputValues.ExpMarkingCount.ToString();
    }

    private void SettingAngle_Load(object sender, EventArgs e)
    {
      ReadData();
    }
    private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (Utils.TextboxKeyFilter.IsNumber(e.KeyChar))
        e.Handled = true;
    }
    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }

    private void rbWhiteMarking_Click(object sender, EventArgs e)
    {
      filter.MarkingOptions.IsWhite = rbWhiteMarking.Checked;
      GD.PreviewCurrent();
    }

    private void rbBlackMarking_Click(object sender, EventArgs e)
    {
      filter.MarkingOptions.IsWhite = !rbBlackMarking.Checked;
      GD.PreviewCurrent();
    }

  }
}
