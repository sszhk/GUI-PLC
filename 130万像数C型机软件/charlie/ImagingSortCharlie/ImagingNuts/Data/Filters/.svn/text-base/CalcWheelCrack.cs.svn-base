using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
  public class CalcWheelCrack:IFilter
  {
    public WheelCrackOptions WheelCrackOptions;

    [XmlIgnore]
    public WheelCrackReport WheelCrackReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("93037"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(
        PublicOptions.View, ref WheelCrackOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(60000, 3);
    }
    public void Default()
    {
    }
    public CalcWheelCrack()
    {
      name = GD.GetString("93037")/*"轮裂"*/;
      correction = false;
      WheelCrackOptions.Annulus.Initialize();
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingWheelCrack(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref WheelCrackOptions.Annulus, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(WheelCrackOptions.Annulus.Center.X + WheelCrackOptions.Annulus.InnerRadius, WheelCrackOptions.Annulus.Center.Y);
      return pt;
    }
    public override void Apply(List<ResultValue> this_time)
    {

    }
    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
}
