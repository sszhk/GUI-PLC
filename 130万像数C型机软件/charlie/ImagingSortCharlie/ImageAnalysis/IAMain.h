#ifndef _IAMAIN_H
#define _IAMAIN_H

#include "nivision.h"
#include "ni\nimv.h"
#include "basic_types.h"
#include "log_entry.h"

#define IA_EXPORT extern "C" __declspec(dllexport)
//定义130w像素为HD_VERSION,根据需要定义，同时还有GD.cs和PGGrab.cs里面定义需要同步
#define  HD_VERSION
#ifdef HD_VERSION
  #define WIDTH_IMAGE 1280
  #define HEIGHT_IMAGE 960
#else
  #define WIDTH_IMAGE 640
  #define HEIGHT_IMAGE 480
#endif
ImagePtr save_image(int idx, int type);
BOOL restore_image(int idx, int type, ImagePtr copy);
IA_EXPORT void IA_Free();
//IA_EXPORT BOOL IA_Initialize(void* _cb, enLog*);
IA_EXPORT HWND IA_SetupWindow(HWND container, int idx);
IA_EXPORT BOOL IA_ReadImage(int idx, int type, LPCTSTR filename);
IA_EXPORT void IA_Refresh(int idx);
IA_EXPORT void IA_AqcuiredImage(byte* buffer, int start_view, int end_view);
IA_EXPORT void IA_CopyImage(int idx);
IA_EXPORT int IA_GetZoom(int idx);
IA_EXPORT void IA_SetZoom(int idx, int rate);
IA_EXPORT void IA_OverlayClear(int idx);
IA_EXPORT int IA_GetMyError(LPTSTR outStr);
IA_EXPORT void IA_OverlayDrawText(int idx, Point* pos, LPCTSTR text, int color, int bkcolor, int fontsize, BOOL bold, BOOL italic, float angle
                                  //OverlayTextOptions* options
                                  );
IA_EXPORT BOOL match_sn(char* const * sn);
BOOL copy_overlay_from(ImagePtr copy, int idx, int type);
extern CRITICAL_SECTION& get_cs(int idx, int type);
class auto_lock
{
  CRITICAL_SECTION* cs;
public:
  auto_lock(CRITICAL_SECTION& _cs)
  {
    cs = &_cs;
//     if( cs->LockSemaphore &&
//       cs->LockSemaphore != INVALID_HANDLE_VALUE)
//       EnterCriticalSection(cs);
  }
  virtual ~auto_lock()
  {
//     if( cs->LockSemaphore &&
//       cs->LockSemaphore != INVALID_HANDLE_VALUE)
//       LeaveCriticalSection(cs);
  }
};

#pragma pack(push, 4)
struct PublicOptions
{
  int idx;
  int thres;
  bool isdisplaystatus;
};
#pragma pack(pop)

#endif
