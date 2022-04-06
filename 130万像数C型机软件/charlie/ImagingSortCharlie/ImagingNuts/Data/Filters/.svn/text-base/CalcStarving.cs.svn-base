using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcStarving : IFilter
  {
    public StarvingOptions StarvingOptions;
    public StarvingInputValues InputValues;

    [XmlIgnore]
    public StarvingReport StarvingReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("28001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(
        PublicOptions.View, ref StarvingOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(34000, 3);
    }
    public void Default()
    {
      InputValues.Initialize();
      StarvingOptions.MaxStarving = (int)InputValues.ExpMaxStarving;
      StarvingOptions.MinStarving = (int)InputValues.ExpMinStarving;
    }
    public CalcStarving()
    {
      name = GD.GetString("28001")/*"缺料"*/;
      correction = false;
      StarvingOptions.IsWhite = true;
      StarvingOptions.Annulus.Initialize();
      InputValues.Initialize();
      StarvingOptions.MaxStarving = (int)InputValues.ExpMaxStarving;
      StarvingOptions.MinStarving = (int)InputValues.ExpMinStarving;
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingStarving(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref StarvingOptions.Annulus, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(StarvingOptions.Annulus.Center.X + StarvingOptions.Annulus.InnerRadius, StarvingOptions.Annulus.Center.Y);
      return pt;
    }
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;

      StarvingOptions.MaxStarving = (int)InputValues.ExpMaxStarving;
      StarvingOptions.MinStarving = (int)InputValues.ExpMinStarving;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FindStarving(ref PublicOptions, ref StarvingOptions, ref StarvingReport);
      tc.Stop();
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        string err = GD.GetString("28003") + StarvingReport.particle.ToString();
        AddNotFound(this_time, GD.GetString("28002"), err);
      }
      else
      {
        if (InputValues.Starving)
        {
          if (StarvingReport.starving < InputValues.ExpMaxStarving &&
            StarvingReport.starving > InputValues.ExpMinStarving)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("28004")/*"最大中心缺料度未达标"*/, "{0:0.0} pix", StarvingReport.starving);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("28004"));
          }
          else AddPass(this_time, GD.GetString("28005")/*"最大中心缺料度"*/, "{0:0.0} pix", StarvingReport.starving);
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
  public struct StarvingInputValues
  {
    public float ExpMinStarving;
    public float ExpMaxStarving;
    public bool Starving;
    public void Initialize()
    {
      ExpMinStarving = 0;
      ExpMaxStarving = 500;
      Starving = true;
    }
  }

}
