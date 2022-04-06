using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ImagingSortCharlie.Forms;
using System.Windows.Forms;
using ImagingSortCharlie.Utils;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcArea : IFilter
  {
    public AreaOptions AreaOptions;
    public AreaInputValues InputValues;
    [XmlIgnore]
    public AreaReport AreaReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("34001"); }
    }

    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref AreaOptions.Rectangle, ref boundary[0]);
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
      Speak.Wizard(42000, 3);
    }
    public void Default()
    {
      AreaOptions.IsWhite = false;
      InputValues.Initialize();
    }
    public CalcArea()
    {
      name = GD.GetString("34001");
      AreaOptions.IsWhite = false;
      AreaOptions.Rectangle.Initialize();
      InputValues.Initialize();
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingArea(this);
    }

    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(PublicOptions.View, toolID, ref AreaOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }

    public bool RefreshTool()
    {
      return IAROI.IA_ROI_GetRectangle(PublicOptions.View, toolID, ref AreaOptions.Rectangle, ref boundary[0]);
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(AreaOptions.Rectangle.Left, AreaOptions.Rectangle.Top);
      return pt;
    }
    public bool Result = false;
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;

      FilterList lst = GD.SettingsForView(PublicOptions.View);
      foreach (IFilter fil in lst)
      {
        if (fil is CalcTeeth && fil.Enabled)
        {
          CalcTeeth filter_teeth = (CalcTeeth)fil;
          if(filter_teeth.Result)
            AreaOptions.Rectangle.Left = 
              (int)filter_teeth.TeethReport.left_most.X;
          break;
        }
      }

      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      Result = IACalc.IA_Calc_Area(ref PublicOptions, ref AreaOptions, ref AreaReport);
      tc.Stop();
      float per = AreaReport.area / InputValues.AreaCorrection * 100;
      AddDuration(this_time, tc.Duration * 1000);
      if (!Result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("34002"), GD.GetString("34003"));
      }
      else
      {
        if (InputValues.Area)
        {

          if (per > InputValues.ExpMaxArea ||
            per < InputValues.ExpMinArea)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("34004"), "{0:0.00} %", per);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("34004"));
          }
          else AddPass(this_time, GD.GetString("34005"), "{0:0.00} %", per);
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

    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }

  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct AreaInputValues
  {
    public float ExpMaxArea;
    public float ExpMinArea;
    public bool Area;
    public bool RelyTeeth;
    public bool RelyLocting;
    public float Offset_X;
    public float Offset_Y;
    public float AreaCorrection;

    public void Initialize()
    {
      ExpMaxArea = 0;
      ExpMinArea = 0;
      Area = true;
      RelyTeeth = false;
      RelyLocting = false;
      Offset_X = 0;
      Offset_Y = 0;
      AreaCorrection = 1;
    }
  }
}
