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
  public class CalcFillArea : IFilter
  {
    public FillAreaOptions FillAreaOptions;
    [XmlIgnore]
    public FillAreaReport FillAreaReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("92001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      //       toolID = IAROI.IA_ROI_AddRotatedRectangle(
      //         PublicOptions.View, ref AngleOptions.RotatedRectangle, ref boundary[0]);
      toolID = IAROI.IA_ROI_AddAnnulus(PublicOptions.View, ref FillAreaOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(37000, 3);
    }
    public void Default()
    {
      FillAreaOptions.Annulus.Initialize();
      FillAreaOptions.IsWhite = true;
    }
    public CalcFillArea()
    {
      name = GD.GetString("92001");
      correction = false;
      FillAreaOptions.Annulus.Initialize();
      FillAreaOptions.IsWhite = true;
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingFillArea(this);
    }
    //bool is_regions_changed = false;
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber,
      IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (windownumber == (int)PublicOptions.View)
      {
        if (type == IA.WindowEventType.IMAQ_DRAW_EVENT)
        {   
          //is_regions_changed= true;
          //           bool rectresult = IAROI.IA_ROI_GetRotatedRectangle(
          //             PublicOptions.View, toolID, ref AngleOptions.RotatedRectangle, ref boundary[0]);
          bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref FillAreaOptions.Annulus, ref boundary[0]);
          GD.PreviewView(PublicOptions.View);
          return rectresult;
        }
      }
      return true;
    }
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FillArea(ref PublicOptions, 
        ref FillAreaOptions, ref FillAreaReport);
      tc.Stop();
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        string err = GD.GetString("92003") + FillAreaReport.prticle.ToString();
        AddNotFound(this_time, GD.GetString("92002"), err);
      }
    }
    public bool RefreshTool()
    {
      return IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref FillAreaOptions.Annulus, ref boundary[0]);
    }
    protected override NIPoint DisplayAt()
    {
      //       NIPoint pt = new NIPoint(AngleOptions.RotatedRectangle.Left, AngleOptions.RotatedRectangle.Top);
      NIPoint pt = new NIPoint(FillAreaOptions.Annulus.Center.X + FillAreaOptions.Annulus.InnerRadius, FillAreaOptions.Annulus.Center.Y);
      return pt;
    }
    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
}
