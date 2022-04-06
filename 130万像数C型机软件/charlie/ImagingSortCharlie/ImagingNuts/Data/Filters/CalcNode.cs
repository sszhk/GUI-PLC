using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagingSortCharlie.Data.Filters;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcNode : IFilter
  {
    public NodeOptions NodeOptions;
    public NodeInputValues InputValues;


    [XmlIgnore]
    public NodeReport NodeReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("25001"); }
    }
    public void Default()
    {
      InputValues.Initialize();
    }

    public CalcNode()
    {
      name = GD.GetString("25001")/*"焊点"*/;
      //copyName = GD.GetString("25001")/*"焊点"*/;
      NodeOptions.RotatedRectangle.Initialize();
      NodeOptions.IsWhite = true;
      NodeOptions.Similarity = 60;
      correction = true;
      InputValues.Initialize();
    }

    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddRotatedRectangle(PublicOptions.View, ref NodeOptions.RotatedRectangle, ref boundary[0]);
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
      Speak.Wizard(31000, 6);
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingNode(this);
    }


    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (windownumber == (int)PublicOptions.View)
      {
        if (type == IA.WindowEventType.IMAQ_DRAW_EVENT)
        {
          bool rectresult = IAROI.IA_ROI_GetRotatedRectangle(PublicOptions.View, toolID, ref NodeOptions.RotatedRectangle, ref boundary[0]);
          GD.PreviewView(PublicOptions.View);
          return rectresult;
        }
      }
      return true;
    }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(NodeOptions.RotatedRectangle.Left, NodeOptions.RotatedRectangle.Top);
      return pt;
    }

    public bool Result = true;
    public override void Apply(List<ResultValue> this_time)
    {
      passedTest = true;
      if (!Enabled)
        return;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      bool result = IACalc.IA_Calc_FindNode(ref PublicOptions, ref NodeOptions, ref NodeReport);
      tc.Stop();

      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        AddNotFound(this_time, GD.GetString("25002")/*"计算焊点时出错"*/, GD.GetString("25003")/*"未找到螺帽"*/ /*+ IA.IA_GetMyError1()*/);
      }
      else
      {
        if (InputValues.NodeCount)
        {
          if (NodeReport.nodesCount < InputValues.ExpMinNodeCount
            || NodeReport.nodesCount > InputValues.ExpMaxNodeCount)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("25004")/*"焊点个数未达标"*/, "{0:0}", NodeReport.nodesCount);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("25004"));
          }
          else AddPass(this_time, GD.GetString("25005")/*"焊点个数"*/, "{0:0}", NodeReport.nodesCount);
        }
        if (InputValues.NodeArea)
        {
          if (NodeReport.minNodeArea < InputValues.ExpMinNodeArea ||
               NodeReport.minNodeArea > InputValues.ExpMaxNodeArea)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("25006")/*"最小焊点面积未达标"*/, "{0:0.00}  pix", NodeReport.minNodeArea);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("25006"));
          }
          else AddPass(this_time, GD.GetString("25007")/*"最小焊点面积"*/, "{0:0.00}  pix", NodeReport.minNodeArea);

          if (NodeReport.maxNodeArea < InputValues.ExpMinNodeArea ||
               NodeReport.maxNodeArea > InputValues.ExpMaxNodeArea)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("25008")/*"最大焊点面积未达标"*/, "{0:0.00}  pix", NodeReport.maxNodeArea);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("25008"));
          }
          else AddPass(this_time, GD.GetString("25009")/*"最大焊点面积"*/, "{0:0.00}  pix", NodeReport.maxNodeArea);
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
  public struct NodeInputValues
  {
    public float ExpMaxNodeArea;
    public float ExpMinNodeArea;
    public int ExpMinNodeCount; 
    public int ExpMaxNodeCount;
    public bool NodeArea;
    public bool NodeCount;
    public void Initialize()
    {
      ExpMaxNodeArea = 0;
      ExpMinNodeArea = 0;
      ExpMinNodeCount = 0;
      ExpMaxNodeCount = 0;
      NodeArea = true;
      NodeCount = true;
    }
  }

}
