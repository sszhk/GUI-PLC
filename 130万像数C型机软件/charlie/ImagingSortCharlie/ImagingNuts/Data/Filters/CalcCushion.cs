using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;
using System.Runtime.InteropServices;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcCushion:IFilter
  {
    public CushionOptions CushionOptions;
    public CushionInputValues InputValues;
    [XmlIgnore]
    public CushionReport CushionReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("32001"); }
    }

    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View,
        ref CushionOptions.Rectangle, ref boundary[0]);
      DisplayTitle();
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
      ImagingSortCharlie.Utils.Speak.Wizard(36000, 4);
    }
    public void Default()
    {
      return;
    }
    public CalcCushion()
    {
      name = GD.GetString("32001");
      correction = false;
      CushionOptions.IsWhite = false;
      CushionOptions.Rectangle.Initialize();
      InputValues.Initialize();
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingCushion(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(
          PublicOptions.View, toolID, ref CushionOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(CushionOptions.Rectangle.Left, CushionOptions.Rectangle.Top);
      return pt;
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
      result = IACalc.IA_Calc_Cushion(ref PublicOptions, ref CushionOptions, ref CushionReport);
      tc.Stop();
      Result = result;

      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("32002"), GD.GetString("32003"));
      }
      else
      {
        if (InputValues.Height)
        {
          if (CushionReport.distance_average > InputValues.ExpMaxCushionHeight
            || CushionReport.distance_average < InputValues.ExpMinCushionHeight)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("32004"), "{0:0.00} pix", CushionReport.distance_average);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("32004"));
          }
          else
          {
            AddPass(this_time, GD.GetString("32005"), "{0:0.00} pix", CushionReport.distance_average);
          }
        }
        if (InputValues.Width)
        {
          if (CushionReport.width > InputValues.ExpMaxWidth
            || CushionReport.width < InputValues.ExpMinWidth)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("32006"), "{0:0.00} pix", CushionReport.width);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("32006"));
          }
          else
          {
            AddPass(this_time, GD.GetString("32007"), "{0:0.00} pix", CushionReport.width);
          }
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
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CushionInputValues
    {
      public float ExpMinCushionHeight;
      public float ExpMaxCushionHeight;
      public float ExpMinWidth;
      public float ExpMaxWidth;
      public bool Height;
      public bool Width;
      public void Initialize()
      {
        ExpMinCushionHeight = 0;
        ExpMaxCushionHeight = 0;
        ExpMaxWidth = 0;
        ExpMinWidth = 0;
        Height = true;
        Width = true;
      }
    }
  }
}
