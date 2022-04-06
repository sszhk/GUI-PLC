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
  public partial class SettingDistance : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.CalcDistance filter = null;
    int CorrectionCount = 0;
    float mm_per_px = 0;
    const int CORRECTIONCOUNT = 1;
    public SettingDistance(CalcDistance fil)
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
      float.TryParse(tbMaxDistance.Text, out filter.InputValues.ExpMaxDistance);
      float.TryParse(tbMinDistance.Text, out filter.InputValues.ExpMinDistance);
      float.TryParse(tbDistanceCorrection.Text, out filter.InputValues.DistanceCorrection);
      filter.DistanceOptions.Horizontal = pbHorzIn.Checked || pbHorzOut.Checked;
      filter.DistanceOptions.InsideToOutside = pbHorzIn.Checked || pbVertIn.Checked;
      filter.DistanceOptions.AngleSensitive = ckAngleSens.Checked;
      filter.InputValues.Distance = cbDistance.Checked;
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
        mm_per_px = 0;
      }
      this.Correction();
      filter.Apply(null);
      if (!filter.Result || filter.DistanceReport.distance == 0)
      {
        return false;
      }
      mm_per_px += (filter.InputValues.DistanceCorrection / filter.DistanceReport.distance);
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.InputValues.mm_per_px = mm_per_px / CORRECTIONCOUNT;
      }
      lbl_ratio.Text = filter.InputValues.mm_per_px.ToString("0.000mm/pix");
      GD.PreviewView(View);
      return true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMaxDistance.Text = filter.InputValues.ExpMaxDistance.ToString();
      tbMinDistance.Text = filter.InputValues.ExpMinDistance.ToString();
      tbDistanceCorrection.Text = filter.InputValues.DistanceCorrection.ToString();
      pbHorzIn.Checked = filter.DistanceOptions.Horizontal && filter.DistanceOptions.InsideToOutside;
      pbHorzOut.Checked = filter.DistanceOptions.Horizontal && !filter.DistanceOptions.InsideToOutside;
      pbVertIn.Checked = filter.DistanceOptions.InsideToOutside && !filter.DistanceOptions.Horizontal;
      pbVertOut.Checked = !filter.DistanceOptions.Horizontal && !filter.DistanceOptions.InsideToOutside;
      ckAngleSens.Checked = filter.DistanceOptions.AngleSensitive;
      cbDistance.Checked = filter.InputValues.Distance;
    }
    private void SettingDistance_Load(object sender, EventArgs e)
    {
      ReadData();
    }
    public void Correction()
    {
      float distancecorrectionresult;
      if (float.TryParse(tbDistanceCorrection.Text, out distancecorrectionresult))
      {
        filter.InputValues.DistanceCorrection = distancecorrectionresult;
      }
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
    private void btnDefault_Click(object sender, EventArgs e)
    {
      Default();
    }

    private void pbVertOut_Click(object sender, EventArgs e)
    {
      filter.DistanceOptions.InsideToOutside = false;
      filter.DistanceOptions.Horizontal = false;
      checkbtn_visible();
      GD.PreviewCurrent();
    }

    private void pbVertIn_Click(object sender, EventArgs e)
    {
      filter.DistanceOptions.InsideToOutside = true;
      filter.DistanceOptions.Horizontal = false;
      checkbtn_visible();
      GD.PreviewCurrent();
    }

    private void pbHorzOut_Click(object sender, EventArgs e)
    {
      filter.DistanceOptions.InsideToOutside = false;
      filter.DistanceOptions.Horizontal = true;
      checkbtn_visible();
      GD.PreviewCurrent();
    }

    private void pbHorzIn_Click(object sender, EventArgs e)
    {
      filter.DistanceOptions.InsideToOutside = true;
      filter.DistanceOptions.Horizontal = true;
      checkbtn_visible();
      GD.PreviewCurrent();
    }

    private void ckAngleSens_CheckedChanged(object sender, EventArgs e)
    {
      filter.DistanceOptions.AngleSensitive = ckAngleSens.Checked;
      if (ckAngleSens.Checked)
      {
        gb_sens.Visible = true;
        checkbtn_visible();
        if (filter.DistanceOptions.Horizontal == true)
        {
          rbtn_left.Checked = true;
        }
        else
        {
          rbtn_top.Checked = true;
        }
        filter.DistanceOptions.ordinal = true;
      }
      else
      {
        gb_sens.Visible = false;
      }
      GD.PreviewCurrent();
    }

    private void radioButton_CheckedChanged(object sender, EventArgs e)
    {
      filter.DistanceOptions.AngleSensitive = ckAngleSens.Checked;
      if (rbtn_left.Checked)
        filter.DistanceOptions.ordinal = true;
      else if (rbtn_right.Checked)
        filter.DistanceOptions.ordinal = false;
      else if (rbtn_top.Checked)
        filter.DistanceOptions.ordinal = true;
      else if (rbtn_bottom.Checked)
        filter.DistanceOptions.ordinal = false;
      GD.PreviewCurrent();
    }
    private void checkbtn_visible()
    {
      if (!ckAngleSens.Checked)
        return;
      if (filter.DistanceOptions.Horizontal == true)
      {
        rbtn_top.Visible = false;
        rbtn_bottom.Visible = false;
        rbtn_left.Visible = true;
        rbtn_right.Visible = true;
        if (!rbtn_left.Checked && !rbtn_right.Checked)
          rbtn_left.Checked = true;
      }
      else
      {
        rbtn_top.Visible = true;
        rbtn_bottom.Visible = true;
        rbtn_left.Visible = false;
        rbtn_right.Visible = false;
        if (!rbtn_top.Checked && !rbtn_bottom.Checked)
          rbtn_top.Checked = true;
      }
    }
  }
}
