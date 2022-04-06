using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcDiameter : IFilter
  {
    public DiameterOptions DiameterOptions;
    public DiameterInputValues InputValues;

    [XmlIgnore]
    public DiameterReport DiameterReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("40001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(PublicOptions.View, ref DiameterOptions.Annulus, ref boundary[0]);
    }
    public override void HideTool()
    {
      if (toolID == 0)
        return;
      IAROI.IA_ROI_Remove(PublicOptions.View, toolID);
      toolID = 0;

    }
    public override void ShowSpeak()
    {
      Speak.Wizard(30000, 5);
    }
    public void Default()
    {
      DiameterOptions.MaxRadius = true;
      DiameterOptions.MinRadius = false;
      DiameterOptions.IsCrack = false;
      DiameterOptions.InsideToOutside = true;
      DiameterOptions.IsWhite = true;
      InputValues.Initialize();
    }
    [XmlIgnore]
    public NIPointFloat CalcCenter = new NIPointFloat();
    public CalcDiameter()
    {
      name = GD.GetString("40001")/*"内孔径"*/;
      DiameterOptions.Annulus.Initialize();
      DiameterOptions.MaxRadius = true;
      DiameterOptions.MinRadius = false;
      DiameterOptions.InsideToOutside = true;
      DiameterOptions.IsWhite = true;
      DiameterOptions.IsCrack = false;
      InputValues.Initialize();
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingDiameter(this);
    }
    private void Correction(ref float radio)
    {
      radio *= InputValues.mm_per_px;
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      bool result = true;
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View )
      {
        result = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref DiameterOptions.Annulus, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
      }
      return result;
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(DiameterOptions.Annulus.Center.X + DiameterOptions.Annulus.InnerRadius, DiameterOptions.Annulus.Center.Y);
      return pt;
    }
    public bool Result = false;
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FindDiameter(ref PublicOptions, ref DiameterOptions, ref DiameterReport);
      tc.Stop();
      Result = result;
      float minDiameter = DiameterReport.minDiameter * InputValues.mm_per_px;
      float maxDiameter = DiameterReport.maxDiameter * InputValues.mm_per_px;
      float concent = CalcConcentricity(InputValues.SelectedConcentObject, DiameterReport.center);
      concent *= InputValues.mm_per_px;
      
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        string err = GD.GetString("40003") + DiameterReport.particle.ToString();
        AddNotFound(this_time, GD.GetString("40002"), err);
      }
      else
      {
        if (DiameterOptions.MaxRadius)
        {
          if (maxDiameter > InputValues.ExpMaxMaxRadius ||
            maxDiameter < InputValues.ExpMaxMinRadius)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("40004")/*"最大直径未达标"*/, "{0:0.00} mm", maxDiameter);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("40004"));
          }
          else AddPass(this_time, GD.GetString("40005")/*"最大直径"*/, "{0:0.00} mm", maxDiameter);
        }
        if (DiameterOptions.MinRadius)
        {
          if (minDiameter < InputValues.ExpMinMinRadius ||
            minDiameter > InputValues.ExpMinMaxRadius)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("40006")/*"最小直径未达标"*/, "{0:0.00} mm", minDiameter);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("40006"));
          }
          else AddPass(this_time, GD.GetString("40007")/*"最小直径"*/, "{0:0.00} mm", minDiameter);
        }
        if (InputValues.Roundness)
        {
          if (DiameterReport.roundness > InputValues.ExpRoundness)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("40008")/*"真圆度未达标"*/, "{0:0.0} ", DiameterReport.roundness);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("40008"));
          }
          else AddPass(this_time, GD.GetString("40009")/*"真圆度"*/, "{0:0.0} ", DiameterReport.roundness);
        }
        if (DiameterOptions.IsCrack)
        {
          if (DiameterReport.crack > InputValues.ExpCrack)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("40012")/*"边缘开裂未达标"*/, "{0:0.0} ", DiameterReport.crack);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("40012"));
          }
          else AddPass(this_time, GD.GetString("40013")/*"边缘开裂"*/, "{0:0.0} ", DiameterReport.crack);
        }
        if (InputValues.Concent && concent >= 0)
        {
          if (concent > InputValues.ExpConcent)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("40010")/*"同心度未达标"*/, "{0:0.00} mm", concent);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("40010"));
          }
          else
            AddPass(this_time, GD.GetString("40011")/*"同心度"*/, "{0:0.00} mm", concent /** mm_per_px*/);
        }
        else if (InputValues.Concent && concent < 0)
        {
          AddNotFound(this_time, GD.GetString("40014"), GD.GetString("40015"));
        }
        FaultyCount.increase_total(PublicOptions.View, this.Name);
        if (passedTest)
        {
          FaultyCount.increase_pass(PublicOptions.View, this.Name);
        }
        else
        {
          FaultyCount.increase_fail_cat(PublicOptions.View, this.Name);
        }
      }

    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DiameterInputValues
    {
      public float ExpMaxMaxRadius;
      public float ExpMaxMinRadius;
      public float ExpMinMaxRadius;
      public float ExpMinMinRadius;
      public float ExpRoundness;
      public float ExpConcent;
      public float MaxRadiusCorrection;
      public float mm_per_px;
      public string SelectedConcentObject;
      public bool Roundness;
      public bool Concent;
      public float ExpCrack;

      public void Initialize()
      {
        ExpMaxMaxRadius = 0;
        ExpMaxMinRadius = 0;
        ExpMinMaxRadius = 0;
        ExpMinMinRadius = 0;
        ExpRoundness = 0;
        ExpConcent = 0;
        MaxRadiusCorrection = 0;
        mm_per_px = 1;
        Roundness = false;
        Concent = false;
        SelectedConcentObject = "";
        ExpCrack = 0;
      }
    }
    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
}
