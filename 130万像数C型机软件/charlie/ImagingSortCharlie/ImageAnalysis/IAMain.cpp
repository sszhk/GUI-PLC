// TestNIDLL.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "iamain.h"
#include "nivision.h"
#include "tchar.h"
#include "NICalc.h"
#include "NIDispose.h"
#include "Colors.h"
#include "log_entry.h"

#ifdef NDEBUG
#pragma comment(lib, "log/enlog2static.lib")
#else
#pragma comment(lib, "log/enlog2staticd.lib")
#endif

//extern BOOL GetMAC(BYTE mac[]);
#pragma comment(lib,"nivision.lib")

ImagePtr niImage[VIEW_COUNT][TYPE_COUNT];
/*ROIPtr niROI[VIEW_COUNT];*/
TCHAR ErrMsg[500];
bool initialized = false;

void SetWindowStyle(HWND wnd, LONG style)
{
  SetWindowLong(wnd, GWL_STYLE, style);
}
void SetWindowStyleEx(HWND wnd, LONG styleEx)
{
  SetWindowLong(wnd, GWL_EXSTYLE, styleEx);
}
ImagePtr GetImage(int idx, int type)
{
//   ENTER_FUNCTION;
  if( idx < 0 || idx>= VIEW_COUNT ||
    type < 0 || type > 1)
    return NULL;

  return niImage[idx][type];
}

BOOL IsEmpty(ImagePtr img)
{
  int empty = 0;
  if( !img || !imaqIsImageEmpty(img, &empty))
    return TRUE;
  return empty;
}
IA_EXPORT BOOL IA_IsEmpty(int idx)
{
//   ENTER_FUNCTION;
  ImagePtr img = GetImage(idx, DISPLAY);
  return IsEmpty(img);
}

IA_EXPORT int IA_GetLastError(LPTSTR outStr)
{
  ENTER_FUNCTION;
  int err = imaqGetLastError();
  LPSTR errstr = imaqGetErrorText(err);
  RegisterDispose d1(errstr);
  //WideCharToMultiByte(CP_UTF8, 0, filename, -1, translate, MAX_PATH, NULL, NULL);
  int len = MultiByteToWideChar(CP_UTF8, 0, errstr, -1, outStr, 500);

  return len;
}

// CRITICAL_SECTION csMyError;
// CRITICAL_SECTION csImage[VIEW_COUNT][TYPE_COUNT];
// CRITICAL_SECTION& get_cs(int idx, int type)
// {
//   return csImage[idx][type];
// }

TCHAR MYERROR[500];
void ClearMyError()
{
//   auto_lock lock(csMyError);
//   if( MYERROR[0] )
//     MYERROR[0] = 0;
}

void SetMyError(LPCTSTR err)
{
//   auto_lock al(csMyError);
//   //imaqSetError(MYERRCODE, err);
//   _tcscpy_s(MYERROR, err);
}

IA_EXPORT int IA_GetMyError(LPTSTR outStr)
{
  return 0;
//   auto_lock al(csMyError);
//   //strcpy_s(outStr, MYERROR);
//   int len = _tcslen(MYERROR)+1;
//   _tcscpy_s(outStr, len, MYERROR);
//   return len;
  //return MYERROR;
}

//////////////////////////////////////////////////////////////////////////



IA_EXPORT void IA_ShowWindow(int idx, BOOL show)
{
  ENTER_FUNCTION;
  imaqShowWindow(idx, show);
}

typedef void (WINAPI * CustomEventCallback)(
  WindowEventType type, 
  int windowNumber, 
  Tool tool,
  Rect rc);

CustomEventCallback ExternalCallback = NULL;

void IMAQ_CALLBACK internalEventCallback(
  WindowEventType type, 
  int windownumber, 
  Tool tool, 
  Rect rc)
{
  if( ExternalCallback )
    ExternalCallback(type, windownumber, tool, rc);
}
IA_EXPORT void IA_Free()
{
  ENTER_FUNCTION;

  initialized = false;
  //   plog = &safe_log;
  //   saved_log = NULL;
  for (int i=0; i<VIEW_COUNT; i++)
  {
    imaqShowWindow(i, FALSE);
    for (int j=0; j<TYPE_COUNT; j++)
    {
      imaqDispose(niImage[i][j]);
      niImage[i][j] = NULL;
      //DeleteCriticalSection(&csImage[i][j]);
    }
  }
  //DeleteCriticalSection(&csMyError);
  //enlog2_finish();

  DO_IF_DEBUG(if(!_CrtDumpMemoryLeaks()){std::cerr<<"leak detection: no memory leak."<<std::endl;});

}
//extern enLog* saved_log;
IA_EXPORT BOOL IA_Initialize(void* _cb, void* pl)
{
//   if( !enlog2_init("image_analysis.dll.log") )
//   {
//     //TRACE("");
//     OutputDebugString(_T("initialize log failure\n"));
//     return FALSE;
//   }
  ENTER_FUNCTION;
  LOG_DEBUG("paremeter _cb=%p, pl=%p", _cb, pl);

  //   ZeroMemory(niROI, sizeof(niROI));
  //InitializeCriticalSection(&csMyError);
  //ZeroMemory(ErrMsg, sizeof(ErrMsg));
  //ZeroMemory(csImage, sizeof(CRITICAL_SECTION)*VIEW_COUNT*TYPE_COUNT);

//   if( !imaqSetWindowThreadPolicy(IMAQ_CALLING_THREAD) )
//     return FALSE;

  for (int i=0; i<VIEW_COUNT; i++)
  {
    for (int j=0; j<TYPE_COUNT; j++)
    {
      //InitializeCriticalSection(&csImage[i][j]);
      niImage[i][j] = imaqCreateImage(IMAQ_IMAGE_U8, 0);
      if( !niImage[i][j] )
      {
        IA_Free();
        return FALSE;
      }
    }
  }

  ExternalCallback = (CustomEventCallback)_cb;
  imaqSetEventCallback(internalEventCallback, TRUE);
  imaqSetToolContextSensitivity(TRUE);
  imaqSetCurrentTool(IMAQ_SELECTION_TOOL);

  initialized = true;

  return TRUE;
}

IA_EXPORT void IA_SetTool(Tool tool)
{
  imaqSetCurrentTool(tool);
}

IA_EXPORT HWND IA_SetupWindow(HWND container, int idx)
{
  ENTER_FUNCTION;
  HWND niWnd = (HWND)imaqGetSystemWindowHandle(idx);
  if( !niWnd )
    return NULL;

  // 	SetWindowPos(niWnd, NULL, 0, 0, 0, 0, SWP_NOSIZE);
  // 	SetWindowStyle(niWnd, WS_CHILD | WS_CLIPSIBLINGS | WS_CLIPCHILDREN);
  // 	SetWindowStyleEx(niWnd, 0);
  SetParent(niWnd, container);
  imaqSetupWindow(idx, 0);
  Point o;
  o.x = 0;
  o.y = 0;
  imaqMoveWindow(idx, o);
  imaqShowWindow(idx, TRUE);
  RECT rc;
  GetWindowRect(container, &rc);
  int width = rc.right - rc.left;
  int height = rc.bottom - rc.top;
  SetWindowPos(niWnd, NULL, 0, 0, width, height, SWP_SHOWWINDOW);
  imaqSetToolColor(&ColorSteelBlue);

  // 	ToolWindowOptions two;
  // 	ZeroMemory(&two, sizeof(two));
  // 	two.showSelectionTool = TRUE;
  // 	two.showSelectionTool = TRUE;
  // 	two.showZoomTool = TRUE;
  // 	two.showRotatedRectangleTool = TRUE;
  // 	imaqShowToolWindow(TRUE);
  // 	imaqSetupToolWindow(TRUE, 4, &two);

  //   if( !niROI[idx] )
  //   {
  //     niROI[idx] = imaqCreateROI();
  //     imaqSetWindowROI(idx, niROI[idx]);
  //   }

  // 	int ok = 0;
  // 	ToolWindowOptions two;
  // 	ZeroMemory(&two, sizeof(two));
  // 	int result = imaqConstructROI2(GetImage(idx, DISPLAY), niROI[idx], IMAQ_ROTATED_RECT_TOOL, &two, NULL, &ok);

  return niWnd;
}

#if UNICODE
char translate[MAX_PATH];
#endif

const char* GetCharStar(LPCTSTR filename)
{
#if UNICODE
  WideCharToMultiByte(CP_ACP, 0, filename, -1, translate, MAX_PATH, NULL, NULL);
  return translate;
#else
  return filename;
#endif
}

IA_EXPORT BOOL IA_ReadImage(int idx, int type, LPCTSTR filename)
{
  ENTER_FUNCTION;
  ImagePtr img = GetImage(idx, type);
  if( !img )
    return FALSE;

  const char* name = GetCharStar(filename);

  int width, height;
  ImageType t;
  if (!imaqGetFileInfo(name, NULL, NULL, NULL, &width, &height, &t)) {
    char* err = imaqGetErrorText(imaqGetLastError());
    //MessageBox(err, "Error Reading the File", MB_OK);
    imaqDispose(err);
    return FALSE;
  }
  if( !imaqSetImageSize(img, width, height))
  {
    return FALSE;
  }

  //auto_lock lock(csImage[idx][type]);
  if( !imaqReadFile(img, name, NULL, NULL) )
    return FALSE;

  return TRUE;
}

// 记得释放这个对象 registerdispose dis(...);
ImagePtr save_image(int idx, int type)
{
  ENTER_FUNCTION;
  ImagePtr src = GetImage(idx, type);
  if( IsEmpty(src) )
    return NULL;
  //auto_lock lock(csImage[idx][type]);
  ImagePtr copy = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  // 这里不能释放
  //RegisterDispose dis_copy(copy);
  int result = imaqDuplicate(copy, src);
  if( !result )
    return NULL;

  return copy;
}

BOOL restore_image(int idx, int type, ImagePtr copy)
{
  ENTER_FUNCTION;
  ImagePtr src = GetImage(idx, type);
  if(!src)
    return FALSE;

  //auto_lock lock(csImage[idx][type]);
  return imaqDuplicate(src, copy);
}


IA_EXPORT void IA_CopyImage(int idx)
{
  ENTER_FUNCTION;
  ImagePtr src = GetImage(idx, BACK);
  if(/* IsEmpty(img)*/ !src )
    return;

  ImagePtr dst = GetImage(idx, DISPLAY);
  if( !src || !dst )
    return;

  //auto_lock lock1(csImage[idx][BACK]);
  //auto_lock lock2(csImage[idx][DISPLAY]);
  imaqDuplicate(dst, src);
}

IA_EXPORT void IA_Refresh(int idx)
{
  ENTER_FUNCTION;
  ImagePtr img = GetImage(idx, DISPLAY);
  if(/* IsEmpty(img)*/ !img )
    return;

  //auto_lock cs(csImage[idx][DISPLAY]);
  //imaqClearOverlay(img, NULL);
//   ImageInfo info;
//   imaqGetImageInfo(img, &info);
//   int w = 0, h = 0;
//   imaqGetImageSize(img, &w, &h);
//   if( w == 0 || h == 0 )
//     return;
  imaqDisplayImage(img, idx, FALSE);
}

// IA_EXPORT void IA_SetWindowSize(int idx, int width, int height)
// {
//   imaqsetwindow
// }


IA_EXPORT void IA_AqcuiredImage(byte* buffer, int start_view, int end_view)
{
//   ENTER_FUNCTION;
  // buffer: 640x1440
  int w = WIDTH_IMAGE;
  int h = HEIGHT_IMAGE;
  int bpp = 1;
  int num = end_view - start_view + 1;
  if (num == 1)
  {
    ImagePtr img = GetImage(start_view, BACK);
    if( img )
    {
      //auto_lock lock(csImage[start_view][BACK]);
      imaqArrayToImage(img, buffer, w, h);
    } 
    return;
  }
  for (int i = 0; i < num; i ++)
  {
    ImagePtr img = GetImage(start_view + i, BACK);
    if( img )
    {
      //auto_lock lock(csImage[start_view + i][BACK]);
      imaqArrayToImage(img, buffer+i*w*h*bpp, w, h);
    }
  }
}

IA_EXPORT int IA_GetZoom(int idx)
{
  ENTER_FUNCTION;
  int x = 0;
  int y = 0;
  const int error = -99;
  if( !imaqGetWindowZoom(idx, &x, &y) )
    return error;
  if( x != y )
    return error;
  switch (x)
  {
  case -1:
  case 1:
    return 0;
  case 2:
    return 1;
  case 4:
    return 2;
  case 8:
    return 3;
  case -2:
    return -1;
  case -4:
    return -2;
  case -8:
    return -3;
  default:
    return error;
  }
}

IA_EXPORT void IA_SetZoom(int idx, int rate)
{
  ENTER_FUNCTION;
  // rate:
  // 0: 原图
  // 1: 放大2倍
  // 2: 放大4倍
  // 3: 放大8倍
  // -1: 缩小2倍
  // -2: 缩小4倍
  // -3: 缩小8倍
  if( rate < -3 || rate > 3 )
    return;
  int x = 0;

  switch (rate)
  {
  case 0:
    x = 1;
    break;
  case 1:
    x = 2;
    break;
  case 2:
    x = 4;
    break;
  case 3:
    x = 8;
    break;
  case -1:
    x = -2;
    break;
  case -2:
    x = -4;
    break;
  case -3:
    x = -8;
    break;
  default:
    return;
  }

  imaqZoomWindow(idx, x, x, IMAQ_NO_POINT);
}

IA_EXPORT BOOL IA_Screenshot(int idx, LPCTSTR file)
{
  ENTER_FUNCTION;
  ImagePtr src = GetImage(idx, BACK);
  if( !src )
    return FALSE;

  //auto_lock lock(csImage[idx][BACK]);
  ImagePtr copy = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_copy(copy);
  imaqDuplicate(copy, src);
  return 0 != imaqWritePNGFile2(copy, GetCharStar(file), 750, NULL, FALSE);
}
IA_EXPORT BOOL IA_Screenshot_Display(int idx, LPCTSTR file)
{
  ENTER_FUNCTION;
  ImagePtr src = GetImage(idx, DISPLAY);
  if( !src )
    return FALSE;

  ImagePtr save = NULL;
  {
    //auto_lock lock(csImage[idx][DISPLAY]);
    save = imaqCreateImage(IMAQ_IMAGE_RGB, 0);
    //int result = imaqCast(save, src, IMAQ_IMAGE_RGB, NULL, -1);
    int result = imaqMergeOverlay(save, src, NULL, 0, NULL);
  }
  RegisterDispose dis_save(save);

  return 0 != imaqWritePNGFile2(save, GetCharStar(file), 750, NULL, FALSE);

}
//////////////////////////////////////////////////////////////////////////
// OVERLAYS
IA_EXPORT void IA_OverlayClear(int idx)
{
  ENTER_FUNCTION;
  ImagePtr disp = GetImage(idx, DISPLAY);
  if( !IsEmpty(disp))
  {
    //auto_lock lock(csImage[idx][DISPLAY]);
    imaqClearOverlay(disp, NULL);
    //IA_Refresh(idx);
  }
}

RGBValue GetColor(int color)
{
  ENTER_FUNCTION;
  RGBValue rgb = {color&0xFF, (color>>8)&0xFF, (color>>16)&0xFF, (color>>24)&0xFF};
  return rgb;
}
IA_EXPORT void IA_OverlayDrawLine(int idx, Point* start, Point* end, int color)
{
  ENTER_FUNCTION;
  ImagePtr d = GetImage(idx, DISPLAY);
  if( !d )
    return;
  //auto_lock lock(csImage[idx][DISPLAY]);
  imaqOverlayLine(d, *start, *end, &GetColor(color), NULL);
}

IA_EXPORT void IA_OverlayDrawText(int idx, Point* pos, LPCTSTR text, int color, int bkcolor, int fontsize, BOOL bold, BOOL italic, float angle
                                  //OverlayTextOptions* options
                                  )
{
  ENTER_FUNCTION;
  ImagePtr d = GetImage(idx, DISPLAY);
  if( !d )
    return;
  OverlayTextOptions oto;
  ZeroMemory(&oto, sizeof(oto));
  oto.fontName = "Arial";
  oto.backgroundColor = GetColor(bkcolor);
  oto.bold = bold;
  oto.italic = italic;
  oto.angle = angle;
  oto.fontSize = fontsize;
  RGBValue rgb = GetColor(color);
  LPCSTR str = GetCharStar(text);
  rgb.alpha = 0;
  //auto_lock lock(csImage[idx][DISPLAY]);
  imaqOverlayText(d, *pos, str, &rgb, &oto, NULL);
}

IA_EXPORT void IA_OverlayDrawText2(int idx, Point* pos, LPCTSTR text, int color,/* int bkcolor, int fontsize, BOOL bold, BOOL italic, float angle*/
                                   OverlayTextOptions* options
                                   )
{
  ENTER_FUNCTION;
  ImagePtr d = GetImage(idx, DISPLAY);
  if( !d )
    return;
  RGBValue rgb = GetColor(color);
  LPCSTR str = GetCharStar(text);
  rgb.alpha = 0;
  //auto_lock lock(csImage[idx][DISPLAY]);
  imaqOverlayText(d, *pos, str, &rgb, options, NULL);
}

IA_EXPORT void IA_ShowScrollbar(int idx, BOOL show)
{
  imaqShowScrollbars(idx, show);
}

IA_EXPORT void IA_ViewBinary(int idx)
{
  imaqSetWindowPalette(idx, IMAQ_PALETTE_BINARY, NULL, 0);
}
IA_EXPORT void IA_ViewGray(int idx)
{
  imaqSetWindowPalette(idx, IMAQ_PALETTE_GRAY, NULL, 0);
}

BOOL copy_overlay_from(ImagePtr copy, int idx, int type)
{
  ENTER_FUNCTION;
  //auto_lock lock(csImage[idx][type]);
  ImagePtr src = GetImage(idx, type);
  if( IsEmpty(src) || IsEmpty(copy) )
    return FALSE;
  return imaqCopyOverlay(src, copy, NULL);
}

/*

C#:
//////////////////////////////////////////////////////////////////////////
private void Form1_Load(object sender, EventArgs e)
{
NI.IA_Initialize();
NI.IA_SetupWindow(panel1.Handle, ViewIndex.VIEW1);
NI.IA_ReadImage(ViewIndex.VIEW1, ViewType.Display, @"E:\Share\Nuts\bmp\2.26.1.bmp");
NI.IA_Refresh(0);

Hardware.FrameGrabber.Start();
}

private void Form1_FormClosed(object sender, FormClosedEventArgs e)
{
Hardware.FrameGrabber.Exit();
NI.IA_Free();
}

*/
IA_EXPORT void log_debug(const char* s)
{
  LOG_DEBUG(s);
}

IA_EXPORT void log_error(const char* s)
{
  LOG_ERROR(s);
}

IA_EXPORT void log_info(const char* s)
{
  LOG_INFO(s);
}
IA_EXPORT void log_warning(const char* s)
{
  LOG_WARNING(s);
}
IA_EXPORT void log_fatal(const char* s)
{
  LOG_FATAL(s);
}

IA_EXPORT void log_set_level(int level)
{
  enlog2_set_level(level);
}
IA_EXPORT BOOL log_init(const char* log_file)
{
  return enlog2_init(log_file);
}

IA_EXPORT void log_finish()
{
  enlog2_finish();
}
