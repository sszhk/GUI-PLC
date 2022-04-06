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
  public partial class SettingAngle : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    Data.Filters.CalcAngle filter = null;
    public SettingAngle(CalcAngle fil)
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
      float.TryParse(tbMaxAngle.Text, out filter.InputValues.ExpMaxAngle);
      float.TryParse(tbMinAngle.Text, out filter.InputValues.ExpMinAngle);
      float.TryParse(tbMaxRatio.Text, out filter.InputValues.ExpMaxRatio);
      float.TryParse(tbMinRatio.Text, out filter.InputValues.ExpMinRatio);
      filter.AngleOptions.Horizontal = rbHorizontal.Checked;
      filter.InputValues.Angle = cbAngle.Checked;
      filter.InputValues.Ratio = cbRatio.Checked;
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
      FilterList lst = GD.SettingsForView(view);
      foreach (IFilter fil in lst)
      {
        if (fil is CalcThreadLocating && fil.Enabled)
        {
          CalcThreadLocating thread_locating = (CalcThreadLocating)fil;
          thread_locating.Apply(null);
          if (thread_locating.Passed && filter.RefreshTool())
          {
            filter.InputValues.Offset_X = filter.AngleOptions.Annulus.Center.X -
              thread_locating.ThreadLocatingReport.center.X;
            filter.InputValues.Offset_Y = filter.AngleOptions.Annulus.Center.Y -
              thread_locating.ThreadLocatingReport.center.Y;
          }
          break;
        }
      }
      return true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMaxAngle.Text = filter.InputValues.ExpMaxAngle.ToString();
      tbMinAngle.Text = filter.InputValues.ExpMinAngle.ToString();
      tbMaxRatio.Text = filter.InputValues.ExpMaxRatio.ToString();
      tbMinRatio.Text = filter.InputValues.ExpMinRatio.ToString();
      rbHorizontal.Checked = filter.AngleOptions.Horizontal;
      rbVertical.Checked = !filter.AngleOptions.Horizontal;
      cbAngle.Checked = filter.InputValues.Angle;
      cbRatio.Checked = filter.InputValues.Ratio;
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

    private void rbHorizontal_CheckedChanged(object sender, EventArgs e)
    {
      filter.AngleOptions.Horizontal = rbHorizontal.Checked;
      GD.PreviewCurrent();
    }

  }
}
