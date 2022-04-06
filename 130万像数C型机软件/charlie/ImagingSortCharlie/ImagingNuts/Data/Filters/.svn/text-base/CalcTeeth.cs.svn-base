using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcTeeth : IFilter
  {
    public TeethOptions TeethOptions;
    public TeethInputValues InputValues;
    [XmlIgnore]
    public TeethReport TeethReport;
    public void Default()
    {
      InputValues.Initialize();
      TeethOptions.HelixAngle = false;
      TeethOptions.BottomDiameter = false;
      TeethOptions.Length = false;
      TeethOptions.MaxOutDiameter = true;
      TeethOptions.MidDiameter = false;
      TeethOptions.MinOutDiameter = false;
      TeethOptions.Pitch = false;
      TeethOptions.Ratio = false;
      TeethOptions.Slopeteeth = true;
      TeethOptions.TeethCount = false;
      TeethOptions.Cylinder = false;
      TeethOptions.Pilot = false;
      TeethOptions.Kernel = 20;
      TeethOptions.AngleSens = false;
      TeethOptions.Offset_X = 0;
    }
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("80001"); }
    }
    public CalcTeeth()
    {
      TeethOptions.HelixAngle = false;
      TeethOptions.BottomDiameter = false;
      TeethOptions.Length = false;
      TeethOptions.MaxOutDiameter = true;
      TeethOptions.MidDiameter = false;
      TeethOptions.MinOutDiameter = false;
      TeethOptions.Pitch = false;
      TeethOptions.Ratio = false;
      TeethOptions.Slopeteeth = true;
      TeethOptions.TeethCount = false;
      TeethOptions.Cylinder = false;
      TeethOptions.Pilot = false;
      TeethOptions.Kernel = 20;
      TeethOptions.AngleSens = false;
      TeethOptions.Offset_X = 0;
      TeethOptions.Rectangle.Initialize(50, 50, 500, 400);
      //TeethOptions.RatioRectangle.Initialize(100, 100, 100, 300);
      TeethOptions.TeethRectangle.Initialize(300, 100, 200, 300);
      InputValues.Initialize();
      name = GD.GetString("80001")/*"牙部测量"*/;
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref TeethOptions.Rectangle, ref boundary[0]);
      IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref TeethOptions.TeethRectangle, ref boundary[1]);
      //IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref TeethOptions.RatioRectangle, ref boundary[2]);
      DisplayTitle();
    }
    public override void HideTool()
    {
      if (toolID == 0)
        return;
      //IAROI.IA_ROI_Remove(PublicOptions.View, toolID + 2);
      IAROI.IA_ROI_Remove(PublicOptions.View, toolID + 1);
      IAROI.IA_ROI_Remove(PublicOptions.View, toolID);
      toolID = 0;
    }
    public override void ShowSpeak()
    {
      Speak.Wizard(50000, 5);
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingTeeth(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool result_toolID = IAROI.IA_ROI_GetRectangle(
          PublicOptions.View, toolID, ref TeethOptions.Rectangle, ref boundary[0]);
        bool result_toolID_teeth = IAROI.IA_ROI_GetRectangle(
          PublicOptions.View, toolID + 1/*toolID_teeth*/, ref TeethOptions.TeethRectangle, ref boundary[1]);
        //         bool result_toolID_ratio =
        //           IAROI.IA_ROI_GetRectangle(
        //           PublicOptions.View, toolID + 2, ref TeethOptions.RatioRectangle, ref boundary[2]);
        GD.PreviewView(PublicOptions.View);
        return (result_toolID && result_toolID_teeth /*&& result_toolID_ratio*/);
      }
      return true;
    }
    protected override NIPoint DisplayAt()
    {
      return new NIPoint(0, 0);
    }
    void DisplayTitle(NIRect rc, string title, int color)
    {
      IA.OverlayTextOptions oto = new IA.OverlayTextOptions(20);
      oto.bold = 1;
      NIPoint pt = new NIPoint(rc.Left, rc.Top);
      IA.IA_OverlayDrawText2(PublicOptions.View, ref pt, Name + title, color, ref oto);
    }
    public override void DisplayTitle()
    {
      if (!IsVisible)
        return;
      DisplayTitle(TeethOptions.Rectangle, GD.GetString("80002"), System.Drawing.Color.Lime.ToArgb());
      DisplayTitle(TeethOptions.TeethRectangle, GD.GetString("80003"), System.Drawing.Color.Blue.ToArgb());
      //DisplayTitle(TeethOptions.RatioRectangle, GD.GetString("80028"), System.Drawing.Color.DeepSkyBlue.ToArgb());
    }
    protected override void SetColor(Color cl)
    {
      color = cl;
      Color cl_teeth = System.Drawing.Color.Red;
      IAROI.IA_ROI_SetColor(PublicOptions.View, toolID, cl.ToArgb());
      IAROI.IA_ROI_SetColor(PublicOptions.View, toolID + 1/*toolID_teeth*/, cl_teeth.ToArgb() /*cl.ToArgb()*/);
    }
    private TeethReport Correction(ref TeethReport report)
    {
      TeethReport tp;
      tp.avr_teeth_distance = report.avr_teeth_distance * InputValues.mm_per_px;
      tp.outerDiameter = report.outerDiameter * InputValues.mm_per_px;
      tp.innerDiameter = report.innerDiameter * InputValues.mm_per_px;
      tp.midDiameter = report.midDiameter * InputValues.mm_per_px;
      tp.maxTeeth = report.maxTeeth * InputValues.mm_per_px;
      tp.minTeeth = report.minTeeth * InputValues.mm_per_px;
      tp.teethLenth = CalcLength(report);
      tp.helixAngle = report.helixAngle;
      tp.maxSlope = report.maxSlope;
      tp.minSlope = report.minSlope;
      tp.teeth1 = report.teeth1;
      tp.teeth2 = report.teeth2;
      tp.ratio = report.ratio;
      tp.cylinder = report.cylinder * InputValues.mm_per_px;
      tp.pilot = report.pilot * InputValues.mm_per_px;
      tp.left_most.X = report.left_most.X;
      tp.left_most.Y = report.left_most.Y;
      return tp;
    }
    private float CalcLength(TeethReport report)
    {
      float l = report.teethLenth;
      float ratio = InputValues.mm_per_px;
      float mm = l * ratio;
      float L = mm + InputValues.ThoseWeCantSee;
      return L;
    }
    public bool Result = false;
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;
      bool result = false;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      result = IACalc.IA_Calc_FindTeeth(ref PublicOptions, ref TeethOptions, ref TeethReport);
      tc.Stop();
      TeethReport tp = Correction(ref TeethReport);
      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("80004")/*"计算牙部时出错"*/, GD.GetString("80005")/*"未找到牙部"*/);
      }
      else
      {
        if (TeethOptions.MaxOutDiameter)
        {
          if (tp.maxTeeth <= InputValues.ExpMaxMaxOutDiameter &&
              tp.maxTeeth >= InputValues.ExpMaxMinOutDiameter)
          {
            AddPass(this_time, GD.GetString("80006")/*"最大外径"*/, "{0:0.00} mm", tp.maxTeeth);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80007")/*"最大外径未达标"*/, "{0:0.00} mm", tp.maxTeeth);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80007"));
          }
        }

        if (TeethOptions.MinOutDiameter)
        {
          if (tp.minTeeth <= InputValues.ExpMinMaxOutDiameter &&
              tp.minTeeth >= InputValues.ExpMinMinOutDiameter)
          {
            AddPass(this_time, GD.GetString("80008")/*"最小外径"*/, "{0:0.00} mm", tp.minTeeth);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80009")/*"最小外径未达标"*/, "{0:0.00} mm", tp.minTeeth);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80009"));
          }
        }

        //         if (InputValues.OutDiameter)
        //         {
        //           if (tp.outerDiameter <= InputValues.ExpMaxOutDiameter &&
        //             tp.outerDiameter >= InputValues.ExpMinOutDiameter)
        //           {
        //             AddResult(this, "外径", "{0:0.000} mm", tp.outerDiameter);  
        //           }
        //           else 
        //           {
        //             passedTest = false;
        //             AddResult(this, COLOR_ERROR, "外径未达标", "{0:0.000} mm", tp.outerDiameter);
        //           }
        //         }
        if (TeethOptions.MidDiameter)
        {
          if (tp.midDiameter <= InputValues.ExpMaxMidDiameter &&
            tp.midDiameter >= InputValues.ExpMinMidDiameter)
          {
            AddPass(this_time, GD.GetString("80010")/*"中径"*/, "{0:0.00} mm", tp.midDiameter);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80011")/*"中径未达标"*/, "{0:0.00} mm", tp.midDiameter);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80011"));
          }
        }

        if (TeethOptions.BottomDiameter)
        {
          if (tp.innerDiameter <= InputValues.ExpMaxBottomDiameter &&
            tp.innerDiameter >= InputValues.ExpMinBottomDiameter)
          {
            AddPass(this_time, GD.GetString("80012")/*"底径"*/, "{0:0.00} mm", tp.innerDiameter);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80013")/*"底径未达标"*/, "{0:0.00} mm", tp.innerDiameter);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80013"));
          }
        }

        if (TeethOptions.Pitch)
        {
          if (tp.avr_teeth_distance <= InputValues.ExpMaxPitch &&
            tp.avr_teeth_distance >= InputValues.ExpMinPitch)
          {
            AddPass(this_time, GD.GetString("80014")/*"牙距"*/, "{0:0.00} mm", tp.avr_teeth_distance);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80015")/*"牙距未达标"*/, "{0:0.00} mm", tp.avr_teeth_distance);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80015"));
          }
        }

        if (TeethOptions.Length)
        {
          if (tp.teethLenth <= InputValues.ExpMaxLenth &&
            tp.teethLenth >= InputValues.ExpMinLenth)
          {
            AddPass(this_time, GD.GetString("80016")/*"长度"*/, "{0:0.00} mm", tp.teethLenth);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80017")/*"长度未达标"*/, "{0:0.00} mm", tp.teethLenth);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80017"));
          }
        }

        if (TeethOptions.Cylinder)
        {
          if (tp.cylinder <= InputValues.ExpMaxCylinder &&
            tp.cylinder >= InputValues.ExpMinCylinder)
          {
            AddPass(this_time, GD.GetString("80028")/*"上光杆"*/, "{0:0.00} mm", tp.cylinder);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80029")/*"上光杆未达标"*/, "{0:0.00} mm", tp.cylinder);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80029"));
          }
        }

        if (TeethOptions.Pilot)
        {
          if (tp.pilot <= InputValues.ExpMaxPilot &&
            tp.pilot >= InputValues.ExpMinPilot)
          {
            AddPass(this_time, GD.GetString("80030")/*"下光杆"*/, "{0:0.00} mm", tp.pilot);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80031")/*"下光杆未达标"*/, "{0:0.00} mm", tp.pilot);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80031"));
          }
        }

        if (TeethOptions.Slopeteeth)
        {
          if (tp.maxSlope > InputValues.ExpMinSlopeAngle &&
            tp.maxSlope < InputValues.ExpMaxSlopeAngle)
          {
            AddPass(this_time, GD.GetString("80018")/*"最大斜牙度"*/, "{0:0.0} ", tp.maxSlope);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80019")/*"最大斜牙度未达标"*/, "{0:0.0} ", tp.maxSlope);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80019"));
          }
          if (tp.minSlope > InputValues.ExpMinSlopeAngle &&
            tp.minSlope < InputValues.ExpMaxSlopeAngle)
          {
            AddPass(this_time, GD.GetString("80020")/*"最小斜牙度"*/, "{0:0.0} ", tp.minSlope);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80021")/*"最小斜牙度未达标"*/, "{0:0.0} ", tp.minSlope);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80021"));
          }
        }

        if (TeethOptions.HelixAngle)
        {
          if (tp.helixAngle <= InputValues.ExpMaxHelixAngle &&
            tp.helixAngle >= InputValues.ExpMinHelixAngle)
          {
            AddPass(this_time, GD.GetString("80022")/*"螺旋角度"*/, "{0:0.0} °", tp.helixAngle);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80023")/*"螺旋角未达标"*/, "{0:0.0} °", tp.helixAngle);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80023"));
          }
        }

        if (TeethOptions.TeethCount)
        {
          if (tp.teeth1 <= InputValues.ExpMaxTeethCount &&
            tp.teeth1 >= InputValues.ExpMinTeethCount &&
            tp.teeth2 <= InputValues.ExpMaxTeethCount &&
            tp.teeth2 >= InputValues.ExpMinTeethCount)
          {
            AddPass(this_time, GD.GetString("80024")/*"牙数"*/, "{0}+{1}", tp.teeth1, tp.teeth2);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80025")/*"牙数未达标"*/, "{0}+{1}", tp.teeth1, tp.teeth2);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80025"));
          }
        }

        if (TeethOptions.Ratio)
        {
          if (tp.ratio <= InputValues.ExpMaxRatio && tp.ratio >= InputValues.ExpMinRatio)
          {
            AddPass(this_time, GD.GetString("80026")/*"尖锐度"*/, "{0:0.0}", tp.ratio);
          }
          else
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("80027")/*"尖锐度未达标"*/, "{0:0.0}", tp.ratio);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("80027"));
          }
        }

//         AddPass(this_time, "上空刀", "{0:0.00}", tp.cylinder);
//         AddPass(this_time, "下空刀", "{0:0.00}", tp.pilot);
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
      /*      FaultyCount.test();*/
    }

    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct TeethInputValues
  {
    public float ExpMaxMaxOutDiameter;
    public float ExpMaxMinOutDiameter;
    public float ExpMinMaxOutDiameter;
    public float ExpMinMinOutDiameter;
    public float ExpMinOutDiameter;
    public float ExpMaxMidDiameter;
    public float ExpMinMidDiameter;
    public float ExpMaxBottomDiameter;
    public float ExpMinBottomDiameter;
    public float ExpMaxPitch;
    public float ExpMinPitch;
    public float ExpMaxLenth;
    public float ExpMinLenth;
    public float ExpMaxSlopeAngle;
    public float ExpMinSlopeAngle;
    public float ExpMaxHelixAngle;
    public float ExpMinHelixAngle;
    public float ExpMaxTeethCount;
    public float ExpMinTeethCount;
    public float OutDiameterCorrection;
    public float LenthCorrection;
    public float mm_per_px;
    public float ExpMinRatio;
    public float ExpMaxRatio;
    public float ExpMinCylinder;
    public float ExpMaxCylinder;
    public float ExpMinPilot;
    public float ExpMaxPilot;
    public float ThoseWeCantSee;

    public void Initialize()
    {
      ExpMaxMaxOutDiameter = 0;
      ExpMaxMinOutDiameter = 0;
      ExpMinMaxOutDiameter = 0;
      ExpMinMinOutDiameter = 0;
      ExpMaxMidDiameter = 0;
      ExpMinMidDiameter = 0;
      ExpMaxBottomDiameter = 0;
      ExpMinBottomDiameter = 0;
      ExpMaxPitch = 0;
      ExpMinPitch = 0;
      ExpMaxLenth = 0;
      ExpMinLenth = 0;
      ExpMaxSlopeAngle = 8;
      ExpMinSlopeAngle = 2;
      ExpMaxHelixAngle = 70;
      ExpMinHelixAngle = 50;
      ExpMaxTeethCount = 0;
      ExpMinTeethCount = 0;
      ExpMinRatio = 0;
      ExpMaxRatio = 0;
      ExpMinCylinder = 0;
      ExpMaxCylinder = 0;
      ExpMinPilot = 0;
      ExpMaxPilot = 0;
      OutDiameterCorrection = 0;
      LenthCorrection = 0;
      mm_per_px = 1;
      ThoseWeCantSee = 0;
    }
  }
}
