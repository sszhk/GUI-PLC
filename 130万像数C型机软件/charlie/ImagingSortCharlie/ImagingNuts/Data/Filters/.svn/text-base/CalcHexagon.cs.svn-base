using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class CalcHexagon : IFilter
  {
    public HexagonOptions HexagonOptions;
    public HexagonInputValues InputValues;


    [XmlIgnore]
    public HexagonReport HexagonReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("70001"); }
    }
    public void Default()
    {
      InputValues.Initialize();
      HexagonOptions.Crack = false;
      HexagonOptions.MaxDiagonal = true;
      HexagonOptions.MinDiagonal = false;
      HexagonOptions.MaxSubtense = true;
      HexagonOptions.MinSubtense = false;
      HexagonOptions.Concent = false;
      HexagonOptions.InsideToOutside = true;
      HexagonOptions.IsWhite = true;
      HexagonOptions.Open = false;
    }
  
    public NIPointFloat CalcCenter = new NIPointFloat();

    public CalcHexagon()
    {
      name = GD.GetString("70001")/*"六边形"*/;
      PublicOptions.Threshold = -1;
      HexagonOptions.Crack = false;
      HexagonOptions.MaxDiagonal = true;
      HexagonOptions.MinDiagonal = false;
      HexagonOptions.MaxSubtense = true;
      HexagonOptions.MinSubtense = false;
      HexagonOptions.Concent = false;
      HexagonOptions.InsideToOutside = true;
      HexagonOptions.IsWhite = true;
      HexagonOptions.Open = false;
      HexagonOptions.Annulus.Initialize();
      InputValues.Initialize();
    }

    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(PublicOptions.View, ref HexagonOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(70000, 5);
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingHexagon(this);
    }


    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref HexagonOptions.Annulus, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return rectresult;
      }
      return true;
    }

//     private float CalcConcentricity()
//     {
//       bool found = false;
//       if (InputValues.SelectedConcentObject == null ||
//         InputValues.SelectedConcentObject.Length == 0)
//         return -1;
//       NIPointFloat cc = new NIPointFloat();
//       IFilter obj = GD.SettingsForView(PublicOptions.View).Search(InputValues.SelectedConcentObject);
//       if (obj is CalcDiameter)
//       {
//         cc = ((CalcDiameter)obj).DiameterReport.center;
//         found = true;
//       }
//       else if (obj is CalcHexagon)
//       {
//         cc = ((CalcHexagon)obj).HexagonReport.center;
//         found = true;
//       }
//       if (found)
//         return Utils.Geo.Point2Point(HexagonReport.center, cc);
//       else
//         return -1;
//     }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(HexagonOptions.Annulus.Center.X + HexagonOptions.Annulus.InnerRadius, HexagonOptions.Annulus.Center.Y);
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
      bool result = IACalc.IA_Calc_FindHexagon(ref PublicOptions, ref HexagonOptions, ref HexagonReport);
      tc.Stop();
      float minSubtense = HexagonReport.minSubtense * InputValues.mm_per_px_sub;
      float maxSubtense = HexagonReport.maxSubtense * InputValues.mm_per_px_sub;
      float maxDiagonal = HexagonReport.maxDiagonal * InputValues.mm_per_px_dia;
      float minDiagonal = HexagonReport.minDiagonal * InputValues.mm_per_px_dia;

      //float concent = CalcConcentricity();
      float concent = CalcConcentricity(InputValues.SelectedConcentObject, HexagonReport.center);
      concent *= InputValues.mm_per_px_sub;

      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        string err = GD.GetString("70003") + HexagonReport.particle.ToString();
        AddNotFound(this_time, GD.GetString("70002"), err);
      }
      else
      {
        if (HexagonOptions.MaxDiagonal)
        {
          if (maxDiagonal > InputValues.ExpMaxMaxDiagonal ||
            maxDiagonal < InputValues.ExpMaxMinDiagonal)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("70004")/*"最大对角未达标"*/, "{0:0.00} mm", maxDiagonal);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("70004"));
          }
          else AddPass(this_time, GD.GetString("70005")/*"最大对角"*/, "{0:0.00} mm", maxDiagonal);
        }
        if (HexagonOptions.MinDiagonal)
        {
          if (minDiagonal < InputValues.ExpMinMinDiagonal ||
               minDiagonal > InputValues.ExpMinMaxDiagonal)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("70006")/*"最小对角未达标"*/, "{0:0.00} mm", minDiagonal);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("70006"));
          }
          else AddPass(this_time, GD.GetString("70007")/*"最小对角"*/, "{0:0.00} mm", minDiagonal);
        }
        if (HexagonOptions.MaxSubtense)
        {
          if (maxSubtense > InputValues.ExpMaxMaxSubtense ||
            maxSubtense < InputValues.ExpMaxMinSubtense)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("70008")/*"最大对边未达标"*/, "{0:0.00} mm", maxSubtense);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("70008"));
          }
          else AddPass(this_time, GD.GetString("70009")/*"最大对边"*/, "{0:0.00} mm", maxSubtense);
        }
        if (HexagonOptions.MinSubtense)
        {
          if (minSubtense < InputValues.ExpMinMinSubtense ||
          minSubtense > InputValues.ExpMinMaxSubtense)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("70010")/*"最小对边未达标"*/, "{0:0.00} mm", minSubtense);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("70010"));
          }
          else AddPass(this_time, GD.GetString("70011")/*"最小对边"*/, "{0:0.00} mm", minSubtense);
        }
        if (HexagonOptions.Concent && concent >= 0)
        {
          if (concent > InputValues.ExpConcent)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("70012")/*"同心度未达标"*/, "{0:0.00} mm ", concent);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("70012"));
          }
          else
            AddPass(this_time, GD.GetString("70013")/*"同心度"*/, "{0:0.00} mm", concent /** mm_per_px*/);
        }
        else if (HexagonOptions.Concent && concent < 0)
        {
          AddNotFound(this_time, GD.GetString("40014"), GD.GetString("40015"));
        }
        if (HexagonOptions.Crack)
        {
          if (HexagonReport.Crack > InputValues.ExpCrack)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("70014")/*"边缘开裂未达标"*/, "{0:0.0} ", HexagonReport.Crack);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("70014"));
          }
          else AddPass(this_time, GD.GetString("70015")/*"边缘开裂"*/, "{0:0.0} ", HexagonReport.Crack);
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
  public struct HexagonInputValues
  {
    public float ExpMaxMaxDiagonal;
    public float ExpMinMaxDiagonal;
    public float ExpMaxMinDiagonal;
    public float ExpMinMinDiagonal;
    public float ExpMaxMaxSubtense;
    public float ExpMinMaxSubtense;
    public float ExpMaxMinSubtense;
    public float ExpMinMinSubtense;
    public float ExpConcent;
    public float ExpCrack;
    public float MaxSubtenseCorrection;
    public float MaxDiagonalCorrection;
    public float mm_per_px_dia;
    public float mm_per_px_sub;
    public bool InsideToOutside;
    public string SelectedConcentObject;
    public void Initialize()
    {
      ExpMaxMaxDiagonal = 0;
      ExpMinMaxDiagonal = 0;
      ExpMaxMinDiagonal = 0;
      ExpMinMinDiagonal = 0;
      ExpMaxMaxSubtense = 0;
      ExpMinMaxSubtense = 0;
      ExpMaxMinSubtense = 0;
      ExpMinMinSubtense = 0;
      ExpConcent = 0;
      ExpCrack = 0;
      MaxDiagonalCorrection = 0;
      MaxSubtenseCorrection = 0;
      mm_per_px_dia = 1;
      mm_per_px_sub = 1;
      InsideToOutside = true;
      SelectedConcentObject = "";
    }
  }


}
