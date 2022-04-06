// PGGrab.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "PGObj.h"

#define RUNNING_FPS 30
#define LIVE_FPS 30

// typedef UINT UINT;
// typedef void (WINAPI * GrabCallback)
// (UINT cam, BYTE* data, int width, int height);
// typedef void (WINAPI * GrabMissing)(UINT cam);
int WINAPI GrabTotalCameras()
{
  return PGObj::total_cameras();
}

DWORD WINAPI GrabInit(UINT cam, PFNCALLBACK callback)
{
  PGObj* obj = new PGObj();
  if( !obj->init(cam, callback))
    return 0;
  return (DWORD)obj;
}

void WINAPI GrabFree(DWORD handle)
{
  if( handle )
  {
    PGObj* obj = (PGObj*)handle;
    obj->free();
    delete obj;
  }
}

BOOL WINAPI GrabSetTrigger(DWORD handle, int trigger)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  return true == obj->mode_trigger();
}

int WINAPI GrabGetTrigger(DWORD handle)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  if( obj->is_trigger_mode() )
    return 0;
  return 1;
}

int WINAPI GrabWidth(DWORD handle)
{
  return 640;
}

int WINAPI GrabHeight(DWORD handle)
{
  return 480;
}

int WINAPI GrabBpp(DWORD handle)
{
  return 1;
}

BOOL WINAPI GrabStart(DWORD handle)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  if( obj->is_running() )
    return TRUE;
  return obj->start(RUNNING_FPS, true);
}
BOOL WINAPI GrabStop(DWORD handle)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  obj->stop();
  return TRUE;
}

BOOL WINAPI GrabLive(DWORD handle)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  obj->mode_live();
  return obj->start(LIVE_FPS, true);
  //return GrabStart(handle);
}

BOOL WINAPI GrabSnap(DWORD handle)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  if(obj->is_running())
  {
    if( obj->is_trigger_mode() )
    {
      return obj->soft_trigger_once();
    }
    else
    {
      // live mode, nonsense to do a snapshot
      return FALSE;
    }
  }
  else
  {
    return true == obj->snap();
//     if( !obj->mode_trigger() )
//       return FALSE;
//     if( !obj->start(30, false) )
//       return FALSE;
//     obj->soft_trigger_once();
  }
  return TRUE;
}
void WINAPI GrabShowSetup(DWORD handle, HWND win, bool show)
{
  if( !handle )
    return;
  PGObj* obj = (PGObj*)handle;
  obj->show_setup(win, show);
}
// void WINAPI GrabReleaseSeq(DWORD handle, HIFCGRAB grabid, int ring)
// {
//   if( !handle )
//     return;
//   GrabGrab *cap = (IFCGrab*)handle;
//   cap->ReleaseSeq(grabid, ring);
// }
BOOL WINAPI GrabSaveSettings(DWORD handle, LPCSTR file)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  return true == obj->save_settings(file);
}
BOOL WINAPI GrabLoadSettings(DWORD handle, LPCSTR file, BOOL apply_to_all)
{
  if( !handle )
    return FALSE;
  PGObj* obj = (PGObj*)handle;
  return true == obj->load_settings(file, apply_to_all==TRUE);
}
int WINAPI GrabGetSerial(int idx)
{
  return PGObj::serial_num(idx);
}