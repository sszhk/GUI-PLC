using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Data.Settings;

  [Serializable]
  public enum ViewType
  {
    NONE = -1,
    Back = 0,
    Display,
    TYPECOUNT
  }

  public static class IA
  {
    public static event DelegateOfToolsChange OnRegionsChange;
    public static event DelegateOfClick OnClick;
    public static event DelegateOfClick DoubleClick;
    private static DelegateOfToolsChange callback = new DelegateOfToolsChange(toolsChanged);
    static IA() { callback = new DelegateOfToolsChange(toolsChanged); }
    public static void Init()
    {
      enLog.enter_function("IA.Init");
      IA_Initialize(callback, 0);
      enLog.exit_function("IA.Init");
    }

    private static void toolsChanged(WindowEventType type, int windownumber, Tool tool, NIRect rc)
    {
      if( type == WindowEventType.IMAQ_ACTIVATE_EVENT ||
        type == WindowEventType.IMAQ_CLICK_EVENT)
      {
        if (OnClick != null)
          if (OnClick(windownumber))
            return;
      }
      else if( type == WindowEventType.IMAQ_DOUBLE_CLICK_EVENT )
      {
        if (DoubleClick != null)
          if (DoubleClick(windownumber))
            return;
      }
      
      if (OnRegionsChange != null)
        OnRegionsChange(type, windownumber, tool, rc);
    }

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_Free();

    public delegate void DelegateOfToolsChange(
      WindowEventType type, int windownumber, Tool tool, NIRect rc);

    public delegate bool DelegateOfClick(int windownumber);
    [DllImport("IMAGEANA.DLL")]
    private extern static bool IA_Initialize(
      [MarshalAs(UnmanagedType.FunctionPtr)] DelegateOfToolsChange p,
      uint log_handle);

    [DllImport("IMAGEANA.DLL")]
    public extern static IntPtr IA_SetupWindow(IntPtr container, VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_ShowWindow(VIEW idx, bool show);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_ReadImage(VIEW idx, ViewType type,  [MarshalAs(UnmanagedType.LPWStr)] string filename);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_Refresh(VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static unsafe void IA_AqcuiredImage(byte* buffer, int start_view, int end_view);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_CopyImage(VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static int IA_GetZoom(VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_Screenshot(VIEW idx, 
      [MarshalAs(UnmanagedType.LPWStr)] string file);
    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_Screenshot_Display(VIEW idx,
      [MarshalAs(UnmanagedType.LPWStr)] string file);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_OverlayClear(VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_SetTool(Tool tool);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_IsEmpty(VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_ViewBinary(VIEW idx);
    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_ViewGray(VIEW idx);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_ShowScrollbar(VIEW idx, bool show);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_OverlayDrawText(
      VIEW idx, 
      [MarshalAs(UnmanagedType.Struct)] ref NIPoint pos, 
      [MarshalAs(UnmanagedType.LPWStr)] string text, 
      int color, int bkcolor, int fontsize, bool bold, bool italic, float angle);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_OverlayDrawText2(
      VIEW idx,
      [MarshalAs(UnmanagedType.Struct)] ref NIPoint pos,
      [MarshalAs(UnmanagedType.LPWStr)] string text,
      int color,
      [MarshalAs(UnmanagedType.Struct)] ref OverlayTextOptions options);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_OverlayDrawLine(
      VIEW idx,
      [MarshalAs(UnmanagedType.Struct)] NIPoint start,
      [MarshalAs(UnmanagedType.Struct)] NIPoint end, 
      int color);

    /// <summary>
    /// 放大、缩小
    /// </summary>
    /// <param name="idx">窗口</param>
    /// <param name="rate">[-3, 3], 0表示原始大小，其余是2的乘方</param>
    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_SetZoom(VIEW idx, int rate);

    public enum WindowEventType
    {
      IMAQ_NO_EVENT = 0,           //No event occurred since the last call to imaqGetLastEvent().
      IMAQ_CLICK_EVENT = 1,           //The user clicked on a window.
      IMAQ_DRAW_EVENT = 2,           //The user drew an ROI in a window.
      IMAQ_MOVE_EVENT = 3,           //The user moved a window.
      IMAQ_SIZE_EVENT = 4,           //The user sized a window.
      IMAQ_SCROLL_EVENT = 5,           //The user scrolled a window.
      IMAQ_ACTIVATE_EVENT = 6,           //The user activated a window.
      IMAQ_CLOSE_EVENT = 7,           //The user closed a window.
      IMAQ_DOUBLE_CLICK_EVENT = 8,           //The user double-clicked in a window.
      IMAQ_WINDOW_EVENT_TYPE_SIZE_GUARD = -1
    };
    public enum Tool
    {
      IMAQ_NO_TOOL = -1,          //No tool is in the selected state.
      IMAQ_SELECTION_TOOL = 0,           //The selection tool selects an existing ROI in an image.
      IMAQ_POINT_TOOL = 1,           //The point tool draws a point on the image.
      IMAQ_LINE_TOOL = 2,           //The line tool draws a line on the image.
      IMAQ_RECTANGLE_TOOL = 3,           //The rectangle tool draws a rectangle on the image.
      IMAQ_OVAL_TOOL = 4,           //The oval tool draws an oval on the image.
      IMAQ_POLYGON_TOOL = 5,           //The polygon tool draws a polygon on the image.
      IMAQ_CLOSED_FREEHAND_TOOL = 6,           //The closed freehand tool draws closed freehand shapes on the image.
      IMAQ_ANNULUS_TOOL = 7,           //The annulus tool draws annuluses on the image.
      IMAQ_ZOOM_TOOL = 8,           //The zoom tool controls the zoom of an image.
      IMAQ_PAN_TOOL = 9,           //The pan tool shifts the view of the image.
      IMAQ_POLYLINE_TOOL = 10,          //The polyline tool draws a series of connected straight lines on the image.
      IMAQ_FREEHAND_TOOL = 11,          //The freehand tool draws freehand lines on the image.
      IMAQ_ROTATED_RECT_TOOL = 12,          //The rotated rectangle tool draws rotated rectangles on the image.
      IMAQ_TOOL_SIZE_GUARD = -1
    } ;

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct OverlayTextOptions {
      [MarshalAs(UnmanagedType.LPStr)]
      public string fontName;                //The name of the font to use.
      public int fontSize;                //The size of the font.
      public int bold;                    //Set this element to TRUE to bold the text.
      public int italic;                  //Set this element to TRUE to italicize the text.
      public int underline;               //Set this element to TRUE to underline the text.
      public int strikeout;               //Set this element to TRUE to strikeout the text.
      public int horizontalTextAlignment; //Sets the alignment of the text.
      public int verticalTextAlignment;   //Sets the vertical alignment for the text.
      public int backgroundColor;         //Sets the color for the text background pixels.
      public double angle;                   //The counterclockwise angle, in degrees, of the text relative to the x-axis.

    public OverlayTextOptions(int font_size)
    {
      fontName = /*"Tahoma";*/"微软雅黑";
      fontSize = /*14;*/font_size;  // 18是正常大小
      backgroundColor = 0x01000000;

      bold = 0;
      italic = 0;
      underline = 0;
      strikeout = 0;
      horizontalTextAlignment = 0;
      verticalTextAlignment = 0;
      angle = 0;
    }

    };

  }
// }
