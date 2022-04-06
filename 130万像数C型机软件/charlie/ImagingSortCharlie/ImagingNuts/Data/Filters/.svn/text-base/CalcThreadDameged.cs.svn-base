using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
using System.Runtime.InteropServices;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcThreadDameged : IFilter
  {
    public ThreadDamageOptions ThreadDamageOptions;

    [XmlIgnore]
    public ThreadDamageReport ThreadDamageReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("27001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref ThreadDamageOptions.Rectangle, ref boundary[0]);
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
      Speak.Wizard(32000, 3);
    }
    public void Default()
    {
      ThreadDamageOptions.IsThreadDamaged = true;
      ThreadDamageOptions.ThreadDamage = 500;
      ThreadDamageOptions.Correct = false;
      ThreadDamageOptions.Offset = 0;
    }
    public CalcThreadDameged()
    {
      name = GD.GetString("27001")/*"牙伤检测"*/;
      ThreadDamageOptions.Height = 3;
      ThreadDamageOptions.Width = 3;
      ThreadDamageOptions.IsThreadDamaged = true;
      ThreadDamageOptions.ThreadDamage = 500;
      ThreadDamageOptions.Contrast = 45;
      ThreadDamageOptions.Correct = false;
      ThreadDamageOptions.Offset = 0;
      ThreadDamageOptions.Rectangle.Initialize();
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingThreadDamage(this);
    }

    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(PublicOptions.View, toolID, ref ThreadDamageOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(ThreadDamageOptions.Rectangle.Left, ThreadDamageOptions.Rectangle.Top);
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
      bool result = IACalc.IA_Calc_ThreadDamage(ref PublicOptions, ref ThreadDamageOptions, ref ThreadDamageReport);
      tc.Stop();
      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("27002")/*"计算牙伤时出错"*/, GD.GetString("27003")/*"未找到牙部"*/);
      }
      else
      {
        if (ThreadDamageOptions.IsThreadDamaged)
        {

          if (ThreadDamageReport.damege >= ThreadDamageOptions.ThreadDamage)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("27004")/*"牙伤未达标"*/, "{0:0.000}", ThreadDamageReport.damege);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("27004"));
          }
          else AddPass(this_time, GD.GetString("27005")/*"牙伤"*/, "{0:0.000}", ThreadDamageReport.damege);
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

}
