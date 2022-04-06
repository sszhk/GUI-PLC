using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Data.Filters
{
  public class CalcSquare : IFilter
  {
    public SquareOptions SquareOptions;
    public SquareInputValues InputValues;


    [XmlIgnore]
    public SquareReport SquareReport;
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("26001"); }
    }
    public void Default()
    {
      InputValues.Initialize();
      SquareOptions.IsWhite = true;
      SquareOptions.MaxDiagonal = true;
      SquareOptions.MinDiagonal = false;
      SquareOptions.MaxSubtense = true;
      SquareOptions.MinSubtense = false;
    }

    public NIPointFloat CalcCenter = new NIPointFloat();

    public CalcSquare()
    {
      name = GD.GetString("26001")/*"四边形"*/;
      SquareOptions.Annulus.Initialize();
      SquareOptions.IsWhite = true;
      SquareOptions.MaxDiagonal = true;
      SquareOptions.MinDiagonal = false;
      SquareOptions.MaxSubtense = true;
      SquareOptions.MinSubtense = false;
      InputValues.Initialize();
    }

    public override void ShowTool()
    {
      if (toolID != 0)
        return;
      toolID = IAROI.IA_ROI_AddAnnulus(PublicOptions.View, ref SquareOptions.Annulus, ref boundary[0]);
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
      Speak.Wizard(33000, 4);
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingSquare(this);
    }


    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool rectresult = IAROI.IA_ROI_GetAnnulus(PublicOptions.View, toolID, ref SquareOptions.Annulus, ref boundary[0]);
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
//         return Utils.Geo.Point2Point(SquareReport.center, cc);
//       else
//         return -1;
//     }

    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(SquareOptions.Annulus.Center.X + SquareOptions.Annulus.InnerRadius, SquareOptions.Annulus.Center.Y);
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
      bool result = IACalc.IA_Calc_FindSquare(ref PublicOptions, ref SquareOptions, ref SquareReport);
      tc.Stop();
      float minSubtense = SquareReport.minSubtense * InputValues.mm_per_px_flat;
      float maxSubtense = SquareReport.maxSubtense * InputValues.mm_per_px_flat;
      float maxDiagonal = SquareReport.maxDiagonal * InputValues.mm_per_px_angle;
      float minDiagonal = SquareReport.minDiagonal * InputValues.mm_per_px_angle;

      //float concent = CalcConcentricity();
      float concent = CalcConcentricity(InputValues.SelectedConcentObject, SquareReport.center);
      concent *= InputValues.mm_per_px_flat;

      Result = result;
      AddDuration(this_time, tc.Duration * 1000);
      if (!result)
      {
        passedTest = false;
        string err = GD.GetString("26003") + SquareReport.particle.ToString();
        AddNotFound(this_time, GD.GetString("26002"), err);
      }
      else
      {
        if (SquareOptions.MaxDiagonal)
        {
          if (maxDiagonal > InputValues.ExpMaxMaxDiagonal ||
            maxDiagonal < InputValues.ExpMaxMinDiagonal)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("26004")/*"最大对角未达标"*/, "{0:0.00} mm", maxDiagonal);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("26004"));
          }
          else AddPass(this_time, GD.GetString("26005")/*"最大对角"*/, "{0:0.00} mm", maxDiagonal);
        }
        if (SquareOptions.MinDiagonal)
        {
          if (minDiagonal < InputValues.ExpMinMinDiagonal ||
               minDiagonal > InputValues.ExpMinMaxDiagonal)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("26006")/*"最小对角未达标"*/, "{0:0.00} mm", minDiagonal);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("26006"));
          }
          else AddPass(this_time, GD.GetString("26007")/*"最小对角"*/, "{0:0.00} mm", minDiagonal);
        }
        if (SquareOptions.MaxSubtense)
        {
          if (maxSubtense > InputValues.ExpMaxMaxSubtense ||
            maxSubtense < InputValues.ExpMaxMinSubtense)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("26008")/*"最大对边未达标"*/, "{0:0.00} mm", maxSubtense);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("26008"));
          }
          else AddPass(this_time, GD.GetString("26009")/*"最大对边"*/, "{0:0.00} mm", maxSubtense);
        }
        if (SquareOptions.MinSubtense)
        {
          if (minSubtense < InputValues.ExpMinMinSubtense ||
          minSubtense > InputValues.ExpMinMaxSubtense)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("26010")/*"最小对边未达标"*/, "{0:0.00} mm", minSubtense);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("26010"));
          }
          else AddPass(this_time, GD.GetString("26011")/*"最小对边"*/, "{0:0.00} mm", minSubtense);
        }
        if (InputValues.Concent && concent >=0)
        {
          if (concent > InputValues.ExpConcent)
          {
            passedTest = false;
            AddNotMeet(this_time, GD.GetString("26012")/*"同心度未达标"*/, "{0:0.00} mm ", concent);
            FaultyCount.increase_fail_sub(PublicOptions.View, this.Name, GD.GetString("26012"));
          }
          else
            AddPass(this_time, GD.GetString("26013")/*"同心度"*/, "{0:0.00} mm", concent /** mm_per_px*/);
        }
        else if (InputValues.Concent && concent < 0)
        {
          AddNotFound(this_time, GD.GetString("40014"), GD.GetString("40015"));
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
  public struct SquareInputValues
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
    public float MaxSubtenseCorrection;
    public float max_oppo_angle_correction;
    public float mm_per_px_flat;
    public float mm_per_px_angle;
    public bool InsideToOutside;
    public bool Concent;
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
      MaxSubtenseCorrection = 0;
      max_oppo_angle_correction = 0;
      mm_per_px_flat = 1;
      mm_per_px_angle = 1;
      InsideToOutside = true;
      Concent = false;
      SelectedConcentObject = "";
    }
  }

}
