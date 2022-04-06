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
  public class CalcMarking : IFilter
  {
    public MarkingOptions MarkingOptions;
    public MarkingInputValues InputValues;

    [XmlIgnore]
    public MarkingReport MarkingReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("33001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(PublicOptions.View, ref MarkingOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(41000, 4);
    }
    public void Default()
    {
      MarkingOptions.IsWhite = true;
      InputValues.Initialize();
    }
    [XmlIgnore]
    public NIPointFloat CalcCenter = new NIPointFloat();
    public CalcMarking()
    {
      name = GD.GetString("33001")/*"印记检测"*/;
      correction = false;
      MarkingOptions.Annulus.Initialize();
      MarkingOptions.IsWhite = true;
      MarkingOptions.MaxMarkingArea = 1000;
      MarkingOptions.MinMarkingArea = 0;
      MarkingOptions.MinRoundness = 0;
      MarkingOptions.MaxRoundness = 0;
      InputValues.Initialize();
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingMarking(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      bool result = true;
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        result = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref MarkingOptions.Annulus, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
      }
      return result;
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(MarkingOptions.Annulus.Center.X + MarkingOptions.Annulus.InnerRadius, MarkingOptions.Annulus.Center.Y);
      return pt;
    }
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FindMarking(ref PublicOptions, ref MarkingOptions, ref MarkingReport);
      tc.Stop();
      AddDuration(this_time, tc.Duration * 1000);
      if(!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("33002")/*"计算角度时出错"*/, GD.GetString("33003")/*"未找到角度"*/);
      }
      else
      {
        if (InputValues.MarkingCount)
        {
          if (MarkingReport.count_marking > InputValues.ExpMarkingCount)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("33004"), "{0:0}", MarkingReport.count_marking);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("33004"));
          }
          else
          {
            AddPass(this_time, GD.GetString("33005"), "{0:0}", MarkingReport.count_marking);
          }
        }
      }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MarkingInputValues
    {
      public int ExpMarkingCount;
      public bool MarkingCount;
      public void Initialize()
      {
        ExpMarkingCount = 2;
        MarkingCount = true;
      }
    }
    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
}
