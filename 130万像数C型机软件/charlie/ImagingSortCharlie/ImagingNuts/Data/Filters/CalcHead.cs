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
  public class CalcHead : IFilter
  {
    public HeadOptions HeadOptions;
    public HeadInputValues HeadInputValues;
    [XmlIgnore]
    public HeadReport HeadReport;

    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("60001"); }
    }
    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View,
        ref HeadOptions.Rectangle, ref boundary[0]);
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
      Speak.Wizard(90000, 4);
    }
    public void Default()
    {
      HeadInputValues.Initialize();
    }
    public CalcHead()
    {
      name = GD.GetString("60001")/*"头厚测量"*/;
      HeadOptions.Rectangle.Initialize();
      HeadInputValues.Initialize();
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingHead(this);
    }
    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetRectangle(
          PublicOptions.View, toolID, ref HeadOptions.Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        //         Point pt = new Point(RotatedRectangle.Top, RotatedRectangle.Left);
        //         IA.OverlayTextOptions oto = new IA.OverlayTextOptions(1);
        //         IA.IA_OverlayDrawText2(View, ref pt, Name, System.Drawing.Color.Red.ToArgb(), ref oto);
        return rectresult;
      }
      return true;
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(HeadOptions.Rectangle.Left, HeadOptions.Rectangle.Top);
      return pt;
    }
//     private float CalcDepth(float headdepth)
//     {
//       float l = headdepth;
//       float ratio = HeadInputValues.mm_per_px;
//       float mm = l * ratio;
//       float L = mm + HeadInputValues.ThoseWeCantSee;
//       return L;
//     }
    public bool Result = false;
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;

      if (!Enabled)
        return;
      bool result = false;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      result = IACalc.IA_Calc_FindHead(ref PublicOptions, ref HeadOptions, ref HeadReport);
      tc.Stop();
      Result = result;
      float width = HeadReport.width * HeadInputValues.mm_per_px_w;
      float depth = HeadReport.depth * HeadInputValues.mm_per_px_d;
//       float width = HeadReport.width * HeadInputValues.mm_per_px;
//       float depth = CalcDepth(HeadReport.depth);

      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("60002")/*"计算头厚时出错"*/, GD.GetString("60003")/*"未找到头部"*/);
      }
      else
      {
        if (HeadInputValues.HeadWidth)
        {
          if (HeadReport.width == -1)
          {
            passedTest = false;
            AddNotFound(this_time, GD.GetString("60004")/*"计算直径时出错"*/, GD.GetString("60005")/*"未找到直径"*/);
          }      
          else
          {
            if (width > HeadInputValues.ExpMaxWidth ||
            width < HeadInputValues.ExpMinWidth)
            {
              passedTest = false;
              AddNotMeet(this_time, GD.GetString("60006")/*"直径未达标"*/, "{0:0.00} mm", width);
              FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("60006"));
            }
            else AddPass(this_time, GD.GetString("60007")/*"直径"*/, "{0:0.00} mm", width);
          }  
        }

        if (HeadInputValues.Depth)
        {
          if (HeadReport.depth == -1)
          {
            passedTest = false;
            AddNotFound(this_time, GD.GetString("60008")/*"计算厚度时出错"*/, GD.GetString("60009")/*"未找到厚度"*/);
          }
          else
          {
            if (depth > HeadInputValues.ExpMaxDepth ||
            depth < HeadInputValues.ExpMinDepth)
            {
              passedTest = false;
              AddNotMeet(this_time, GD.GetString("60010")/*"厚度未达标"*/, "{0:0.00} mm", depth);
              FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("60010"));
            }
            else AddPass(this_time, GD.GetString("60011")/*"厚度"*/, "{0:0.00} mm", depth);
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
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct HeadInputValues
  {
    public float ExpMaxWidth;
    public float ExpMinWidth;
    public float ExpMaxDepth;
    public float ExpMinDepth;
    public bool InsideToOutside;
    public float WidthCorrection;
    public float DepthCorrection;
    //public float mm_per_px;
    public float mm_per_px_w;
    public float mm_per_px_d;
    public bool HeadWidth;
    public bool Depth;
    //public float ThoseWeCantSee;
    public void Initialize()
    {
      ExpMaxWidth = 0;
      ExpMinWidth = 0;
      ExpMaxDepth = 0;
      ExpMinDepth = 0;
      InsideToOutside = false;
      WidthCorrection = 0;
      DepthCorrection = 0;
      mm_per_px_w = 1;
      mm_per_px_d = 1;
//       mm_per_px = 1;
//       ThoseWeCantSee = 0;
      HeadWidth = true;
      Depth = true;
    }
  }

}
