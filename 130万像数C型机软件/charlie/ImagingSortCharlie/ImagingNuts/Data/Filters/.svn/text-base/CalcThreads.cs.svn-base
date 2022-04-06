using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcThreads : IFilter
  {
    public ThreadsOptions ThreadsOptions;
    public ThreadsInputValues InputValues;

    [XmlIgnore]
    public ThreadsReport ThreadsReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("29001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref ThreadsOptions.Rectangle, ref boundary[0]);
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
      Speak.Wizard(39000, 4);
    }
    public void Default()
    {
      InputValues.Initialize();
      passedTest = true;
      ThreadsOptions.IsWhite = false;
      ThreadsOptions.MinArea = 100.0f;
      ThreadsOptions.MaxArea = 10000.0f;
    }
    public CalcThreads()
    {
      name = GD.GetString("29001")/*"牙数"*/;
      correction = false;
      ThreadsOptions.Rectangle.Initialize();
      ThreadsOptions.IsWhite = false;
      ThreadsOptions.MinArea = 100.0f;
      ThreadsOptions.MaxArea = 10000.0f;
      InputValues.Initialize();
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingThreads(this);
    }

    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(PublicOptions.View, toolID, ref ThreadsOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(ThreadsOptions.Rectangle.Left, ThreadsOptions.Rectangle.Top);
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
      PublicOptions.DisplayStatus = !GD.IsSpinning;
      bool result = IACalc.IA_Calc_Threads(ref PublicOptions, ref ThreadsOptions, ref ThreadsReport);
      tc.Stop();
      float max_pitch=ThreadsReport.pitch_max*InputValues.mm_per_px_pitch;
      float min_pitch=ThreadsReport.pitch_min*InputValues.mm_per_px_pitch;
      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("29002")/*"计算牙数时出错"*/, GD.GetString("29003")/*"未找到牙部"*/);
      }
      else
      {
        if (InputValues.IsTeethsCount)
        {

          if (ThreadsReport.threads < InputValues.threads_min_count
            ||ThreadsReport.threads>InputValues.threads_max_count)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("29004")/*"牙数未达标"*/, "{0}", ThreadsReport.threads);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("29004"));
          }
          else AddPass(this_time, GD.GetString("29005")/*"牙数"*/, "{0}", ThreadsReport.threads);
        }
        if (InputValues.is_pitch)
        {
          if (min_pitch < InputValues.exp_min_pitch)
          {
            AddNotMeet(this_time, GD.GetString("29008"), "{0:0.00} pix", min_pitch);
          }
          else AddPass(this_time, GD.GetString("29009"), "{0:0.00} pix", min_pitch);
          if (max_pitch > InputValues.exp_max_pitch)
            
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("29006"), "{0:0.00} pix"/*最大牙距*/, max_pitch);
            //AddNotMeet(this_time, GD.GetString("29006"), "{0:0.000}"/*最大牙距*/, min_pitch);
          }
          else AddPass(this_time, GD.GetString("29007"), "{0:0.00} pix", ThreadsReport.pitch_max);           
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
  public struct ThreadsInputValues
  {
    public float threads_min_count;
    public float threads_max_count;
    public bool IsTeethsCount;
    public bool is_pitch;
    public float pitch_correction;
    public float mm_per_px_pitch;
    public float exp_min_pitch;
    public float exp_max_pitch;
    public void Initialize()
    {
      threads_min_count = 0;
      threads_max_count = 0;
      IsTeethsCount = true;
      is_pitch = true;
      pitch_correction = 0;
      mm_per_px_pitch = 1;
      exp_min_pitch = 0;
      exp_max_pitch = 0;
    }
  }

}
