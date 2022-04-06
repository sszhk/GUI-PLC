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
  public partial class SettingTeeth : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.CalcTeeth filter = null;
    int CorrectionCount = 0;
    float mm_per_px = 0;
    float ThoseWeCantSee = 0;
    float offset_x = 0;
    const int CORRECTIONCOUNT = 1;

    public SettingTeeth(CalcTeeth fil)
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
      float.TryParse(tbMaxMaxOutDiameter.Text, out filter.InputValues.ExpMaxMaxOutDiameter);
      float.TryParse(tbMaxMinOutDiameter.Text, out filter.InputValues.ExpMaxMinOutDiameter);
      float.TryParse(tbMinMaxOutDiameter.Text, out filter.InputValues.ExpMinMaxOutDiameter);
      float.TryParse(tbMinMinOutDiameter.Text, out filter.InputValues.ExpMinMinOutDiameter);
      float.TryParse(tbMaxMidDiameter.Text, out filter.InputValues.ExpMaxMidDiameter);
      float.TryParse(tbMinMidDiameter.Text, out filter.InputValues.ExpMinMidDiameter);
      float.TryParse(tbMaxBottomDiameter.Text, out filter.InputValues.ExpMaxBottomDiameter);
      float.TryParse(tbMinBottomDiameter.Text, out filter.InputValues.ExpMinBottomDiameter);
      float.TryParse(tbMaxPitch.Text, out filter.InputValues.ExpMaxPitch);
      float.TryParse(tbMinPitch.Text, out filter.InputValues.ExpMinPitch);
      float.TryParse(tbMaxLength.Text, out filter.InputValues.ExpMaxLenth);
      float.TryParse(tbMinLength.Text, out filter.InputValues.ExpMinLenth);
      float.TryParse(tbMaxHelixAngle.Text, out filter.InputValues.ExpMaxHelixAngle);
      float.TryParse(tbMinHelixAngle.Text, out filter.InputValues.ExpMinHelixAngle);
      float.TryParse(tbMaxSlopeteeth.Text, out filter.InputValues.ExpMaxSlopeAngle);
      float.TryParse(tbMinSlopeteeth.Text, out filter.InputValues.ExpMinSlopeAngle);
      float.TryParse(tbMinTeethCount.Text, out filter.InputValues.ExpMinTeethCount);
      float.TryParse(tbMaxTeethCount.Text, out filter.InputValues.ExpMaxTeethCount);
      float.TryParse(tbMaxRatio.Text, out filter.InputValues.ExpMaxRatio);
      float.TryParse(tbMinRatio.Text, out filter.InputValues.ExpMinRatio);
      float.TryParse(tbDiameterCorrection.Text, out filter.InputValues.OutDiameterCorrection);
      float.TryParse(tbLenthCorrection.Text, out filter.InputValues.LenthCorrection);
      float.TryParse(tbMinCylinder.Text, out filter.InputValues.ExpMinCylinder);
      float.TryParse(tbMaxCylinder.Text, out filter.InputValues.ExpMaxCylinder);
      float.TryParse(tbMaxPilot.Text, out filter.InputValues.ExpMaxPilot);
      float.TryParse(tbMinPilot.Text, out filter.InputValues.ExpMinPilot);
      int.TryParse(tbKernel.Text, out filter.TeethOptions.Kernel);
      filter.TeethOptions.MaxOutDiameter = cbMaxOutDiameter.Checked;
      filter.TeethOptions.MinOutDiameter = cbMinOutDiameter.Checked;
      filter.TeethOptions.MidDiameter = cbMidDiameter.Checked;
      filter.TeethOptions.BottomDiameter = cbBottomDiameter.Checked;
      filter.TeethOptions.Pitch = cbPitch.Checked;
      filter.TeethOptions.Length = cbLength.Checked;
      filter.TeethOptions.Slopeteeth = cbSlopeteeth.Checked;
      filter.TeethOptions.HelixAngle = cbHelixAngle.Checked;
      filter.TeethOptions.TeethCount = cbTeethCount.Checked;
      filter.TeethOptions.Ratio = cbRatio.Checked;
      filter.TeethOptions.Cylinder = cbCylinder.Checked;
      filter.TeethOptions.Pilot = cbPilot.Checked;
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
        ThoseWeCantSee = 0;
        offset_x = 0;
      }
      this.Correction();
      filter.TeethOptions.Offset_X = 0;
      filter.Apply(null);
      if (!filter.Result)
        return false;
      mm_per_px += filter.InputValues.OutDiameterCorrection / filter.TeethReport.maxTeeth;
      ThoseWeCantSee += CalcThoseWeCantSee(ref filter.TeethReport);
      offset_x += filter.TeethOptions.TeethRectangle.Left - filter.TeethReport.left_most.X;
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.InputValues.mm_per_px = mm_per_px / CORRECTIONCOUNT;
        filter.InputValues.ThoseWeCantSee = ThoseWeCantSee / CORRECTIONCOUNT;
        filter.TeethOptions.Offset_X = offset_x / CORRECTIONCOUNT;
      }
      lbl_ratio.Text = filter.InputValues.mm_per_px.ToString("0.000mm/pix");
      GD.PreviewView(View);
      return true;
    }
    private float CalcThoseWeCantSee(ref TeethReport report)
    {
      float l = report.teethLenth;
      float ratio = filter.InputValues.OutDiameterCorrection / report.maxTeeth;
      float mm = l * ratio;
      float a = filter.InputValues.LenthCorrection - mm;
      return a;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMaxMaxOutDiameter.Text = filter.InputValues.ExpMaxMaxOutDiameter.ToString();
      tbMaxMinOutDiameter.Text = filter.InputValues.ExpMaxMinOutDiameter.ToString();
      tbMinMaxOutDiameter.Text = filter.InputValues.ExpMinMaxOutDiameter.ToString();
      tbMinMinOutDiameter.Text = filter.InputValues.ExpMinMinOutDiameter.ToString();
      tbMaxMidDiameter.Text = filter.InputValues.ExpMaxMidDiameter.ToString();
      tbMinMidDiameter.Text = filter.InputValues.ExpMinMidDiameter.ToString();
      tbMaxBottomDiameter.Text = filter.InputValues.ExpMaxBottomDiameter.ToString();
      tbMinBottomDiameter.Text = filter.InputValues.ExpMinBottomDiameter.ToString();
      tbMaxPitch.Text = filter.InputValues.ExpMaxPitch.ToString();
      tbMinPitch.Text = filter.InputValues.ExpMinPitch.ToString();
      tbMaxLength.Text = filter.InputValues.ExpMaxLenth.ToString();
      tbMinLength.Text = filter.InputValues.ExpMinLenth.ToString();
      tbMaxHelixAngle.Text = filter.InputValues.ExpMaxHelixAngle.ToString();
      tbMinHelixAngle.Text = filter.InputValues.ExpMinHelixAngle.ToString();
      tbMaxSlopeteeth.Text = filter.InputValues.ExpMaxSlopeAngle.ToString();
      tbMinSlopeteeth.Text = filter.InputValues.ExpMinSlopeAngle.ToString();
      tbMinTeethCount.Text = filter.InputValues.ExpMinTeethCount.ToString();
      tbMaxTeethCount.Text = filter.InputValues.ExpMaxTeethCount.ToString();
      tbMinRatio.Text = filter.InputValues.ExpMinRatio.ToString();
      tbMaxRatio.Text = filter.InputValues.ExpMaxRatio.ToString();
      tbMinCylinder.Text = filter.InputValues.ExpMinCylinder.ToString();
      tbMaxCylinder.Text = filter.InputValues.ExpMaxCylinder.ToString();
      tbMinPilot.Text = filter.InputValues.ExpMinPilot.ToString();
      tbMaxPilot.Text = filter.InputValues.ExpMaxPilot.ToString();
      tbDiameterCorrection.Text = filter.InputValues.OutDiameterCorrection.ToString();
      tbLenthCorrection.Text = filter.InputValues.LenthCorrection.ToString();
      cbMaxOutDiameter.Checked = filter.TeethOptions.MaxOutDiameter;
      cbMinOutDiameter.Checked = filter.TeethOptions.MinOutDiameter;
      cbMidDiameter.Checked = filter.TeethOptions.MidDiameter;
      cbBottomDiameter.Checked = filter.TeethOptions.BottomDiameter;
      cbPitch.Checked = filter.TeethOptions.Pitch;
      cbLength.Checked = filter.TeethOptions.Length;
      cbSlopeteeth.Checked = filter.TeethOptions.Slopeteeth;
      cbHelixAngle.Checked = filter.TeethOptions.HelixAngle;
      cbTeethCount.Checked = filter.TeethOptions.TeethCount;
      cbRatio.Checked = filter.TeethOptions.Ratio;
      cbCylinder.Checked = filter.TeethOptions.Cylinder;
      cbPilot.Checked = filter.TeethOptions.Pilot;
      tbKernel.Text = filter.TeethOptions.Kernel.ToString();
      cb_angle_sens.Checked = filter.TeethOptions.AngleSens;
      //pl_lenth.Visible = filter.TeethOptions.AngleSens;
      //pl_correct_lenth.Visible = filter.TeethOptions.AngleSens;
//       if (!filter.TeethOptions.AngleSens)
//         filter.TeethOptions.Length = false;
    }

    public void Correction()
    {
      float outdiametercorrectionresult, lenthcorrectionresult;
      if (float.TryParse(tbDiameterCorrection.Text, out outdiametercorrectionresult))
      {
        filter.InputValues.OutDiameterCorrection = outdiametercorrectionresult;
      }
      if (float.TryParse(tbLenthCorrection.Text, out lenthcorrectionresult))
      {
        filter.InputValues.LenthCorrection = lenthcorrectionresult;
      }
    }

    private void SettingTeeth_Load(object sender, EventArgs e)
    {
      ReadData();
    }
    
    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }


    private void tbKernel_ValueChanged(object sender, EventArgs e)
    {
      filter.TeethOptions.Kernel = (int)tbKernel.Value;
      GD.PreviewView(View);
    }

    private void cb_angle_sens_CheckedChanged(object sender, EventArgs e)
    {
      filter.TeethOptions.AngleSens = cb_angle_sens.Checked;
      //pl_lenth.Visible = filter.TeethOptions.AngleSens;
      //pl_correct_lenth.Visible = filter.TeethOptions.AngleSens;
//       if (!filter.TeethOptions.AngleSens)
//         filter.TeethOptions.Length = false;
      GD.PreviewCurrent();
    }  

  }
}
