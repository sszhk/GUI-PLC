using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagingSortCharlie.Forms;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcAngle : IFilter
  {
    public AngleOptions AngleOptions;
    public AngleInputValues InputValues;
    [XmlIgnore]
    public AngleReport AngleReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("20001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
//       toolID = IAROI.IA_ROI_AddRotatedRectangle(
//         PublicOptions.View, ref AngleOptions.RotatedRectangle, ref boundary[0]);
      toolID = IAROI.IA_ROI_AddAnnulus(PublicOptions.View, ref AngleOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(40000, 4);
    }
    public void Default()
    {
      AngleOptions.Horizontal = false;
      InputValues.Initialize();
    }
    public CalcAngle()
    {
      name = GD.GetString("20001")/*"角度"*/;
      correction = true;
      AngleOptions.Horizontal = true;
//       AngleOptions.RotatedRectangle.Initialize();
      AngleOptions.Annulus.Initialize();
      AngleOptions.Annulus.EndAngle = 90;
      InputValues.Initialize();
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingAngle(this);
    }

    bool is_regions_changed = false;
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber,
      IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (windownumber == (int)PublicOptions.View)
      {
        if (type == IA.WindowEventType.IMAQ_DRAW_EVENT)
        {
          is_regions_changed = true;
//           bool rectresult = IAROI.IA_ROI_GetRotatedRectangle(
//             PublicOptions.View, toolID, ref AngleOptions.RotatedRectangle, ref boundary[0]);
          bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref AngleOptions.Annulus, ref boundary[0]);
          GD.PreviewView(PublicOptions.View);

          return rectresult;
        }
      }
      return true;
    }

    public bool RefreshTool()
    {
      return IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref AngleOptions.Annulus, ref boundary[0]);
    }

    protected override NIPoint DisplayAt()
    {
//       NIPoint pt = new NIPoint(AngleOptions.RotatedRectangle.Left, AngleOptions.RotatedRectangle.Top);
      NIPoint pt = new NIPoint(AngleOptions.Annulus.Center.X + AngleOptions.Annulus.InnerRadius, AngleOptions.Annulus.Center.Y);
      return pt;
    }

    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;

      if (!is_regions_changed)
      {
        FilterList lst = GD.SettingsForView(PublicOptions.View);
        foreach (IFilter fil in lst)
        {
          if (fil is CalcThreadLocating && fil.Enabled)
          {
            CalcThreadLocating thread_locating = (CalcThreadLocating)fil;
            if (thread_locating.Passed)
            {
              AngleOptions.Annulus.Center.X =
                (int)(thread_locating.ThreadLocatingReport.center.X + InputValues.Offset_X);
              AngleOptions.Annulus.Center.Y =
                (int)(thread_locating.ThreadLocatingReport.center.Y + InputValues.Offset_Y);
            }
            break;
          }
        }
      }
      else
        is_regions_changed = false;

      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FindAngle(ref PublicOptions, ref AngleOptions, ref AngleReport);
      tc.Stop();
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("20002")/*"计算角度时出错"*/, GD.GetString("20003")/*"未找到角度"*/);
      }
      else
      {
        if (InputValues.Angle)
        {

          if (AngleReport.angle > InputValues.ExpMaxAngle ||
            AngleReport.angle < InputValues.ExpMinAngle)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("20004")/*"角度未达标"*/, "{0:0.0} °", AngleReport.angle);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("20004"));
          }
          else AddPass(this_time, GD.GetString("20001")/*"角度"*/, "{0:0.0} °", AngleReport.angle);

        }
//         if (InputValues.Ratio)
//         {
//           if (AngleReport.ratio > InputValues.ExpMaxRatio ||
//             AngleReport.ratio < InputValues.ExpMinRatio)
//           {
//             passedTest = false;
//             AddNotMeet(this_time, GD.GetString("20005")/*"尖锐度未达标"*/, "{0:0.0}", AngleReport.ratio);
//             FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("20005"));
//           }
//           else AddPass(this_time, GD.GetString("20006")/*"尖锐度"*/, "{0:0.0}", AngleReport.ratio);
//         }
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
    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct AngleInputValues
  {
    public float ExpMaxAngle;
    public float ExpMinAngle;
    public float ExpMaxRatio;
    public float ExpMinRatio;
    public bool Angle;
    public bool Ratio;
    public float Offset_X;
    public float Offset_Y;
    public void Initialize()
    {
      ExpMaxAngle = 0;
      ExpMinAngle = 0;
      ExpMaxRatio = 0;
      ExpMinRatio = 0;
      Angle = true;
      Ratio = true;
      Offset_X = 0;
      Offset_Y = 0;
    }
  }

  [Serializable]
  public enum RectanglesearchDirections
  {
    TopToBottom,
    BottomToTop,
    LeftToRight,
    RightToLeft
  }
}
