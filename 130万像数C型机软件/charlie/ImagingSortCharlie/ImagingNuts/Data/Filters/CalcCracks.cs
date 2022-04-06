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
  public class CalcCracks : IFilter
  {
    public CrackOptions CrackOptions;

    [XmlIgnore]
    public CrackReport CrackReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("30001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(
        PublicOptions.View, ref CrackOptions.Annulus, ref boundary[0]);
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
      CrackOptions.CenterCrack = 1000;
      CrackOptions.IsCenterCrack = true;
    }
    public CalcCracks()
    {
      name = GD.GetString("30001")/*"中心开裂"*/;
      correction = false;
      CrackOptions.CenterCrack = 1000;
      CrackOptions.IsCenterCrack = true;
      CrackOptions.Annulus.Initialize();
    }

    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingCracks(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref CrackOptions.Annulus, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(CrackOptions.Annulus.Center.X + CrackOptions.Annulus.InnerRadius, CrackOptions.Annulus.Center.Y);
      return pt;
    }
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;
      //             FilterList lst = GD.SettingsForView(PublicOptions.View);
      //             foreach (IFilter fil in lst)
      //             {
      //                 if (fil is FindShape)
      //                 {
      //                     if (!((FindShape)fil).FindShapeReport.result)
      //                     {
      //                         passedTest = false;
      //                         AddNotFound(this_time, GD.GetString("30002")/*"头裂计算出错"*/, GD.GetString("30003")/*"未找到头部"*/);
      //                         return;
      //                     }
      //                     if (((FindShape)fil).FindShapeReport.result)
      //                     {
      //                         CrackOptions.Annulus.Center.MakePointFromPointfloat(((FindShape)fil).FindShapeReport.center);
      //                         //CrackOptions.Annulus.OuterRadius = (int)((FindShape)fil).FindShapeReport.radius;
      //                         CrackOptions.IsWhite = ((FindShape)fil).FindShapeOptions.IsWhite;
      //                         break;
      //                     }  
      //                 }
      //             }
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FindCrack(ref PublicOptions, ref CrackOptions, ref CrackReport);
      tc.Stop();
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("30002")/*"计算头裂时出错"*/, GD.GetString("30003")/*"未找到头部"*/);
      }
      else
      {
        if (CrackOptions.IsCenterCrack)
        {
          if (CrackReport.centerCrack > CrackOptions.CenterCrack)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("30004")/*"最大中心开裂度未达标"*/, "{0:0.0}", CrackReport.centerCrack);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("30004"));
          }
          else AddPass(this_time, GD.GetString("30005")/*"最大中心开裂度"*/, "{0:0.0}", CrackReport.centerCrack);
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
