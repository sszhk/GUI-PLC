using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcThreadLocating : IFilter
  {
    public ThreadLocatingOptions ThreadLocatingOptions;
    [XmlIgnore]
    public ThreadLocatingReport ThreadLocatingReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("31001"); }
    }

    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View,
        ref ThreadLocatingOptions.Rectangle, ref boundary[0]);
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
      ImagingSortCharlie.Utils.Speak.Wizard(38000, 3);
    }
    public void Default()
    {
      return;
    }
    public CalcThreadLocating()
    {
      name = GD.GetString("31001")/*"牙部定位"*/;
      correction = false;
      ThreadLocatingOptions.IsWhite = true;
      ThreadLocatingOptions.Rectangle.Initialize();
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingThreadLocating(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(
          PublicOptions.View, toolID, ref ThreadLocatingOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(ThreadLocatingOptions.Rectangle.Left, ThreadLocatingOptions.Rectangle.Top);
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
      result = IACalc.IA_Calc_FindThreadLocating(ref PublicOptions, ref ThreadLocatingOptions, ref ThreadLocatingReport);
      tc.Stop();
      Result = result;

      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("60002"), GD.GetString("60003"));
      }
      else
      {
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
}
