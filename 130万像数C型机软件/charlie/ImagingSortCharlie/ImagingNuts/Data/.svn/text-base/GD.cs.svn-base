//130w像素请把#define HD_VERSION注释去掉，否则注释掉,另需在IAMain.h里面同步
//#define HD_VERSION

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Hardware;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using ImagingSortCharlie.Data.Settings;

/// <summary>
/// Global Data
/// </summary>


public static class GD
{
  public static System.Drawing.Rectangle rc;
  public static VIEW CurrentView ;
  public static bool IsSnapping;
  public static string extension_orig = ".orig.png";
  public static string extension_screen = ".screen.png";
#if HD_VERSION
  public static int WIDTH_IMAGE { get { return 1280; } }
  public static int HEIGHT_IMAGE { get { return 960; } }
#else
  public static int WIDTH_IMAGE { get { return 640; } }
  public static int HEIGHT_IMAGE{ get { return 480; } }
#endif

  public static void RefreshImage(VIEW idx)
  {
    IA.IA_Refresh(idx);
  }
  public static void InitializeAllFilters()
  {
    for (VIEW idx = VIEW.VIEW1; idx < VIEW.VIEW_COUNT; idx++)
    {
      SettingsForView(idx).InitializeFilters(idx);
      //         //         foreach (IFilter p in GD.SettingsForView(idx))
      //         //         {
      //         //           p.Initialize(idx);     
      //         //         }
    }
  }
  public static void CopyImage(VIEW idx) { IA.IA_CopyImage(idx); }
  public static bool IsValid;
//   public static int ImageWidth { get { return FrameGrabber.Width; } }
//   public static int ImageHeight { get { return FrameGrabber.Height; } }
  public static bool ReadImage(VIEW idx, string imgfile)   { return IA.IA_ReadImage(idx, ViewType.Back, imgfile);}
  public static bool Screenshot(VIEW idx, string imgfile) 
  { return IA.IA_Screenshot(idx, imgfile); }
  public static bool ScreenshotDisplay(VIEW idx, string imgfile) 
  {
    return IA.IA_Screenshot_Display(idx, imgfile);
  }
  public static string AppPath()
  {
    return Path.GetDirectoryName(Application.ExecutablePath);
  }
  public static string Home
  {
    get { return Path.GetDirectoryName(Application.ExecutablePath); }
  }
  public static string HomePath(string path)
  {
    // 出现盘符，作为绝对路径处理
    if (Path.GetPathRoot(path).Contains(":"))
      return path;
    return Path.Combine(Home, path);
  }
  public static string[] Screenshot(VIEW idx)
  {
    int w = WIDTH_IMAGE/*GD.ImageWidth*/;
    int h = HEIGHT_IMAGE/*GD.ImageHeight*/;
    string[] files = new string[3];
    if (w == 0)
      w = WIDTH_IMAGE;
    if (h == 0)
      h = HEIGHT_IMAGE;
    if (Settings.PS.ScreenshotPath==null)
    {
      Settings.PS.ScreenshotPath = "Screenshots";
    }
    DirectoryInfo di = new DirectoryInfo(Settings.PS.ScreenshotPath);
    try
    {
      if (!di.Exists)
        di.Create();
    }
    catch(IOException )
    {
      return null;
    }
    DateTime now = DateTime.Now;
    VIEW view = idx;
    string time; 
    time = string.Format("View{7}.{0:00}{1:00}{2:00}.{3:00}{4:00}{5:00}.{6:X4}",
      now.Year % 100,
      now.Month,
      now.Day,
      now.Hour,
      now.Minute,
      now.Second,
      now.Ticks % 0xFFFF,
      ((int)view + 1).ToString());
    string orig = Path.Combine(di.FullName, time + GD.extension_orig/*".orig.png"*/);
    string scr = Path.Combine(di.FullName, time + GD.extension_screen/*".screen.png"*/);

    ScreenshotDisplay(view, scr);
    Screenshot(view, orig);

    files[0] = orig;
    files[1] = scr;
    files[2] = time;
    return files;
  }

  
  public static FilterList SettingsForView(VIEW idx) { return Settings.PS.ViewFilters[(int)idx]; }
  public static FilterList SettingsView1 { get { return SettingsForView(VIEW.VIEW1); } }
  public static FilterList SettingsView2 { get { return SettingsForView(VIEW.VIEW2); } }
  public static FilterList SettingsView3 { get { return SettingsForView(VIEW.VIEW3); } }
  public static int GetZoom(VIEW idx) { return IA.IA_GetZoom(idx); }
  public static void SetZoom(VIEW idx, int rate) { IA.IA_SetZoom(idx, rate); }
  public static void ClearOverlay(VIEW idx) 
  { 
    IA.IA_OverlayClear(idx); 
    foreach (IFilter fil in SettingsForView(idx))
    {
      fil.DisplayTitle();
    }
  }
  public static bool ApplyFilters(VIEW i, bool add_to_records) { return SettingsForView(i).Apply(add_to_records); }
  public static void CurrentFiltersChanged() { if (!IsValid) return; ClearOverlay(CurrentView); }
  public static event IA.DelegateOfToolsChange OnRegionsChanged { add { IA.OnRegionsChange-=value; IA.OnRegionsChange+=value;} remove { IA.OnRegionsChange-=value; } }
  public static void SetTool(IA.Tool tool) { IA.IA_SetTool(tool); }
  public static void ShowImage(VIEW idx, bool show) { IA.IA_ShowWindow(idx, show); }
  public static bool IsEmpty(VIEW idx) { return IA.IA_IsEmpty(idx); }
  public static void BroadcastRefresh() { }

  /// <summary>
  /// 是否处于运转状态?
  /// </summary>
  public static bool IsSpinning { get { return FrameGrabber.IsSpinning; } }
  /// <summary>
  /// 是否处于动态状态?
  /// </summary>
  public static bool IsLive { get { return FrameGrabber.IsLive; } }
  /// <summary>
  /// 运转或动态?
  /// </summary>
  public static bool IsLiveOrSpinning { get { return IsSpinning || IsLive; } }
  /// <summary>
  /// 是否处于定位状态?
  /// </summary>
  public static bool IsLocating = false;
  /// <summary>
  /// 是否处于滤波器选择状态？
  /// </summary>
  /// 
  public static bool is_edit = false;
  public static IFilter current_filter = null;
  public static void PreviewView(VIEW idx)
  {
    if (IsLiveOrSpinning)
      return;
    CopyImage(idx);
    ApplyFilters(idx, false);
    RefreshImage(idx);
  }
  public static void PreviewCurrent()
  {
    VIEW current = CurrentView;
    PreviewView(current);
  }
  public static void OneShot()
  {
    FrameGrabber.GrabOneFrame();
  }

  public static string GetString(string s)
  {
    return ImagingSortCharlie.Properties.Resources.ResourceManager.GetString("s" + s + "_" + Settings.Lang);
  }

  public static Image GetImg(string s)
  {
    return (Image)ImagingSortCharlie.Properties.Resources.ResourceManager.GetObject(s + "_" + Settings.Lang.ToString());
  }
  public static string GetImgStr(string s)
  {
    return (s + "_" + Settings.Lang.ToString());
  }
}
