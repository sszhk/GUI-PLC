using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using NationalInstruments.CWIMAQControls;
using System.Reflection;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcDistance : IFilter
  {
    public DistanceOptions DistanceOptions;
    public DistanceInputValues InputValues;

    [XmlIgnore]
    public DistanceReport DistanceReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("50001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref DistanceOptions.Rectangle, ref boundary[0]);
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
      Speak.Wizard(20000, 6);
    }
    public void Default()
    {
      InputValues.Initialize();
      DistanceOptions.Horizontal = true;
      DistanceOptions.InsideToOutside = true;
      passedTest = true;
    }
    public CalcDistance()
    {
      name = GD.GetString("50001")/*"夹距量测"*/;
      DistanceOptions.Horizontal = true;
      DistanceOptions.InsideToOutside = true;
      DistanceOptions.Rectangle.Initialize();
      InputValues.Initialize();
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingDistance(this);
      //       viewer.RegionsChanged += new _AxDCWIMAQViewerEvents_RegionsChangedEventHandler(viewer_RegionsChanged);
      //       rc = (CWIMAQRotatedRectangle)viewer.Regions.AddRotatedRectangle(RotatedRectangle.NI);
    }

    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(PublicOptions.View, toolID, ref DistanceOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(DistanceOptions.Rectangle.Left, DistanceOptions.Rectangle.Top);
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
      bool result = IACalc.IA_Calc_FindDistance(ref PublicOptions, ref DistanceOptions, ref DistanceReport);
      tc.Stop();
      float distance = DistanceReport.distance * InputValues.mm_per_px;
      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      //AddCorrection(this_time, GD.GetString("50005"), "{0:0.000} mm/pix", InputValues.mm_per_px);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("50002")/*"计算距离时出错"*/, GD.GetString("50003")/*"未找到距离"*/);
      }
      else
      {
        if (InputValues.Distance)
        {

          if (distance > InputValues.ExpMaxDistance ||
            distance < InputValues.ExpMinDistance)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("50004")/*"距离未达标"*/, "{0:0.000} mm", distance);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("50004"));
          }
          else AddPass(this_time, GD.GetString("50005")/*"距离"*/, "{0:0.000} mm", distance);
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
  public struct DistanceInputValues
  {
    public float ExpMaxDistance;
    public float ExpMinDistance;
    public float DistanceCorrection;
    public float mm_per_px;
    public bool Distance;

    public void Initialize()
    {
      ExpMaxDistance = 0;
      ExpMinDistance = 0;
      DistanceCorrection = 0;
      mm_per_px = 1;
      Distance = true;
    }
  }

  [Serializable]
  public enum DistanceDirections
  {
    Inside = 0,
    Outside
  }
}
