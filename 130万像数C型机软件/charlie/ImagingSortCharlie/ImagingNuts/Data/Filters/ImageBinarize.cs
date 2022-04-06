using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Utils;
using ImagingSortCharlie.Data.Settings;
namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class ImageBinarize : IFilter
  {
    private bool passedTest = true;
    public override bool Passed { get { return passedTest; } }
    public override string TypeName
    {
      get { return GD.GetString("12001"); }
    }
    public bool InterVariance = false;
    public NIRect Rectangle = new NIRect();
    public BinarizeOptions BinarizeOptions = new BinarizeOptions();
    public bool IsFullScreen = true;
    public void Default()
    {
      InterVariance = false;
      IsFullScreen = true;
      BinarizeOptions.Inverse = false;
      PublicOptions.Threshold = 128;
    }
    public ImageBinarize()
    {
      Rectangle.Initialize();
      name = GD.GetString("12001")/*"二值化"*/;
      correction = false;
      PublicOptions.Threshold = 128;
    }
    public override void ShowTool()
    {
      if (toolID != 0 || IsFullScreen)
        return;
      toolID = IAROI.IA_ROI_AddRectangle(PublicOptions.View, ref Rectangle, ref boundary[0]);
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
      Speak.Wizard(80000, 2);
    }
    public override void Initialize(VIEW viewer)
    {
      base.Initialize(viewer);
      configure = new SettingBinarize(this);
    }

    public override bool RegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool, Data.Filters.NIRect rc)
    {
      if (type == IA.WindowEventType.IMAQ_DRAW_EVENT &&
        windownumber == (int)PublicOptions.View)
      {
        bool result = IAROI.IA_ROI_GetRectangle(PublicOptions.View, toolID, ref Rectangle, ref boundary[0]);
        GD.PreviewView(PublicOptions.View);
        return result;
      }
      return true;
    }
    public override void Remove()
    {
      base.Remove();
      RefreshImage();
    }
    protected override NIPoint DisplayAt()
    {
      NIPoint pt = new NIPoint(Rectangle.Left, Rectangle.Top);
      return pt;
    }
    public override void DisplayTitle()
    {
      if (IsFullScreen)
        return;
      base.DisplayTitle();
    }
    public override void Apply(List<ResultValue> this_time)
    {
      if (!Enabled)
        return;
      string threshold = GD.GetString("12002")/*"<无>"*/;
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      if (InterVariance)
      {
        tc.Start();
        int thres = IAProcess.IA_Process_BinarizeAuto1(PublicOptions.View, ref Rectangle, IsFullScreen);
        tc.Stop();
        threshold = thres.ToString();
        AddDuration(this_time, tc.Duration * 1000);
        AddResult(this_time, GD.GetString("12003")/*"二值化 自动"*/, threshold);
      }
      else
      {
        tc.Start();
        if (PublicOptions.Threshold != -1)
        {
          IAProcess.IA_Process_BinarizeManual(ref PublicOptions, ref Rectangle, ref BinarizeOptions, IsFullScreen);
          threshold = PublicOptions.Threshold.ToString();
        }
        tc.Stop();
        AddDuration(this_time, tc.Duration * 1000);
        AddResult(this_time, GD.GetString("12001")/*"二值化"*/, threshold);
      }
    }

    private void RefreshImage()
    {
      if (GD.IsSnapping)
        return;
      GD.PreviewView(PublicOptions.View);
    }
    public override UserControl Setup()
    {
      configure.ReadData();
      return configure.GetControl();
    }
  }
}
