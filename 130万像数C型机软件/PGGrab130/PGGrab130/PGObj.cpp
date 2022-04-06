/* 
* (c)2010 enMind Software Co., Ltd. All rights reserved.
*
* +===================| NON-DISCLOSURE STATEMENT |===================+
* | Everything related to developing is property of enMind Co., Ltd. |
* | enMind employees MUST NOT disclose any of that for any purpose   |
* | by any means without permission.                                 |
* | The non-disclosure materials are including but not limited to    |
* | the following:                                                   |
* |  . source code (C/C++/Javascript/HTML/C#/AS/CSS/Exe/Dll etc.);   |
* |  . documents (design documents etc.);                            |
* |  . diagrams & figures;                                           |
* |  . datasheets;                                                   |
* | This statement applies to all employees of enMind Co., Ltd.      |
* |                    http://www.enmind.com.cn                      |
* +==================================================================+
* 
* Created:      2010-04-08  13:22
* Filename:     pgobj.cpp
* Author:       John
* Revisions:    initial
* 2010-04-26  John
* 修改为同步处理，一张图像处理完毕后，才尝试抓取下一张图像。
*
* Purpose:     
*
*/
#include "stdafx.h"
#include "PGObj.h"
#include "iostream"
#include "fstream"
#include "sstream"
#include "tchar.h"
#include "tinystr.h"
#include "tinyxml.h"
using namespace std;
#include "pgobjmisc.h"

#pragma comment(lib, "PGRFlyCapture.lib")
#pragma comment(lib, "pgrflycapturegui.lib")

//
// Register defines
// 
#define INITIALIZE    0x000
#define CAMERA_POWER  0x610
#define FRAME_INFO    0x12F8
#define SOFTWARE_TRIGGER   0x62C
#define SOFT_ASYNC_TRIGGER 0x102C
//
// Image size
//
// Not initialized
//
#define NOT_EVEN_INITIALIZED 128

#define NOT_OK (error != FLYCAPTURE_OK)

UINT PGObj::total_cams = NOT_EVEN_INITIALIZED;
FlyCaptureInfoEx PGObj::info[_MAX_CAMERAS];

UINT PGObj::total_cameras()
{
  if( total_cams == NOT_EVEN_INITIALIZED )
    total_init();
  return total_cams;
}
bool PGObj::total_init()
{
  if( total_cams != NOT_EVEN_INITIALIZED )
    return true;
  ZeroMemory(info, sizeof(FlyCaptureInfoEx)*_MAX_CAMERAS);

  FlyCaptureError error = flycaptureBusEnumerateCamerasEx(info, &total_cams);
  if( error != FLYCAPTURE_OK )
  {
    return false;
  }
#ifdef _DEBUG
  for (int i=0; i<(int)total_cams; i++)
  {
    printf("idx: %d, cam serial: %d, node: %d\n", i, info[i].SerialNumber, info[i].iNodeNum);
  }
#endif
  return true;
}

void PGObj::total_free()
{
  total_cams = NOT_EVEN_INITIALIZED;
  //PvUnInitialize();
  ZeroMemory(info, sizeof(FlyCaptureInfoEx)*_MAX_CAMERAS);
}

//////////////////////////////////////////////////////////////////////////

PGObj::PGObj(): ctx(NULL), gui_ctx(NULL), cam_index(NOT_EVEN_INITIALIZED),
                width(0), height(0), bpp(0), stop_requested(true), callback(NULL),
                free_requested(false), async(true), busy(false)
{
  ZeroMemory(&co, sizeof(calc_opt));
}

PGObj::~PGObj()
{
  free();
}

bool PGObj::alloc_buffers()
{
  width = WIDTH;
  height = HEIGHT;
  bpp = BPP;

  int bytes_per_image = width*height*bpp/8;

  // allocate buffers
  return true;
}

bool PGObj::set_cam_defaults()
{
  FlyCaptureError error = FLYCAPTURE_OK;
  //
  // Reset the camera to default factory settings by asserting bit 0
  //
  error = flycaptureSetCameraRegister( ctx, INITIALIZE, 0x80000000 );
  if( NOT_OK )
    return false;

  //
  // Power-up the camera (for cameras that support this feature)
  //
  error = flycaptureSetCameraRegister( ctx, CAMERA_POWER, 0x80000000 );
  if( NOT_OK )
    return false;
  //
  // Turn on Timestamp (0x01) bit in FRAME_INFO to attach the image timestamp to the image header.
  //
  error = flycaptureSetCameraRegister( ctx, FRAME_INFO, 0x80000001 );
  if( NOT_OK )
    return false;

  return true;
}
int PGObj::serial_num(int idx)
{
  if( idx < 0 || idx >= (int)total_cams )
    return 0;
  return info[idx].SerialNumber;
}

bool PGObj::create_context()
{
  FlyCaptureError error = FLYCAPTURE_OK;
  CameraGUIError gerr = PGRCAMGUI_OK;
  // initialization
  error = flycaptureCreateContext( &ctx );
  if( NOT_OK )
    return false;
  // setup dialog
  gerr = pgrcamguiCreateContext( &gui_ctx );
  if( gerr != PGRCAMGUI_OK )
  {
    free();
    return false;
  }
  // pgrcamguiSetSettingsWindowHelpPrefix

  error = ::flycaptureInitialize( 
    ctx, 
    cam_index);
  if( NOT_OK )
  {
    free();
    return false;
  }
  gerr = pgrcamguiInitializeSettingsDialog(gui_ctx, (GenericCameraContext)ctx);
  if( gerr != PGRCAMGUI_OK )
  {
    free();
    return false;
  }

  return true;
}

bool PGObj::init(UINT idx, PFNCALLBACK cb)
{
  if( ctx || gui_ctx )
    free();

  if( idx < 0 || idx >= total_cams )
    return false;

  free_requested = false;
  cam_index = idx;
  callback = cb;

  if( !alloc_buffers() || 
      !create_context() ||
      !set_cam_defaults() )
  {
    free();
    return false;
  }
  // create waiter threads...
  co.serial = info[idx].SerialNumber;
  co.buffer_index = 0;
  co.obj = this;
//     co[i].cap_event = CreateEvent(NULL, TRUE, FALSE, NULL);
//     if(co[i].cap_event == INVALID_HANDLE_VALUE )
//       return false;
//     co[i].waiter = NULL;
//     co[i].waiter = CreateThread(NULL, 0, thrd_waiter, &co[i], 0, NULL);
//     if( co[i].waiter == INVALID_HANDLE_VALUE )
//       return false;
    //CloseHandle(h);
#ifdef _DEBUG
  printf("idx: %d, serial: %d\n", idx, info[idx].SerialNumber);
#endif
  return true;
}

DWORD WINAPI PGObj::thrd_waiter(LPVOID param)
{
  calc_opt* co = (calc_opt*)param;
  PGObj* obj = (PGObj*)co->obj;
#ifdef _DEBUG
  printf("thrd_waiter #%d start...\n", obj->cam_index);
#endif

  obj->waiter(co);
#ifdef _DEBUG
  printf("thrd_waiter #%d exiting...\n", obj->cam_index);
#endif
  return 0;
}

#define TRIGGER_MODE  0

void PGObj::mode_live()
{
  if( !ctx )
    return;
  FlyCaptureError error = FLYCAPTURE_OK;
  error = flycaptureSetTrigger(ctx, false, NULL, NULL, TRIGGER_MODE, NULL);
}
bool PGObj::is_trigger_mode()
{
  if( !ctx )
    return false;

  FlyCaptureError error = FLYCAPTURE_OK;
  bool present = false;
  error = flycaptureQueryTrigger(ctx, &present, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
  if( error != FLYCAPTURE_OK ||
    !present )
    return false;

  bool onoff = false;
  error = flycaptureGetTrigger(ctx, &onoff, NULL, NULL, NULL, NULL, NULL);
  if( error != FLYCAPTURE_OK )
    return false;
  return onoff;
}
bool PGObj::mode_trigger()
{
  if( !ctx )
    return false;

  FlyCaptureError error = FLYCAPTURE_OK;
  bool present = false;
  error = flycaptureQueryTrigger(ctx, &present, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
  if( error != FLYCAPTURE_OK ||
    !present )
    return false;
  
  bool onoff = false;
  error = flycaptureGetTrigger(ctx, &onoff, NULL, NULL, NULL, NULL, NULL);
  if( error != FLYCAPTURE_OK )
    return false;
  error = flycaptureSetTrigger(ctx, true, NULL, NULL, NULL, NULL);
  if( error != FLYCAPTURE_OK )
    return false;

  return true;
}

bool PGObj::soft_trigger_once()
{
  if( !ctx )
    return false;

  FlyCaptureError   error = FLYCAPTURE_OK;
  unsigned long     ulValue = 0;

  bool external = false;
  error = flycaptureGetTrigger(ctx, &external, NULL, NULL, NULL, NULL, NULL);
  if( NOT_OK )
    return false;
  if( !external )
    return true;
  // 
  // Do our check to make sure the camera is ready to be triggered
  // by looking at bits 30-31. Any value other than 1 indicates
  // the camera is not ready to be triggered.
  //
  while( ulValue != 0x80000001 )
  {
    error = flycaptureGetCameraRegister( 
      ctx, SOFT_ASYNC_TRIGGER, &ulValue );
    if( NOT_OK )
      return false;
  }

  error = flycaptureSetCameraRegister( 
    ctx, SOFT_ASYNC_TRIGGER, 0x80000000 );
  if( NOT_OK )
    return false;

  return true;
}

void PGObj::free()
{
  if( cam_index == NOT_EVEN_INITIALIZED )
    return;

  stop_requested = true;
  free_requested = true;
//   for (int i=0; i<BUFFERS_EACH_CAM && co[i].cap_event; i++)
//   {
//     SetEvent(co[i].cap_event);
//   }
  flycaptureDestroyContext(ctx);
  ctx = NULL;
  pgrcamguiDestroyContext(gui_ctx);
  gui_ctx = NULL;
//   for(int i=0; i<BUFFERS_EACH_CAM; i++ )
//   {
//     WaitForSingleObject(co[i].waiter, INFINITE);
//     CloseHandle(co[i].waiter);
//   }
  //Sleep(100);

  cam_index = NOT_EVEN_INITIALIZED;

//   delete[] buffer;
//   buffer = NULL;

  ZeroMemory(&co, sizeof(calc_opt));
  width = 0;
  height = 0;
  bpp = 0;
  callback = NULL;
}

bool PGObj::start(int FPS, bool async)
{
  this->async = async;
  stop_requested = false;
  FlyCaptureFrameRate fr = FLYCAPTURE_FRAMERATE_15;
  switch(FPS)
  {
  case 1:
    fr = FLYCAPTURE_FRAMERATE_1_875;
    break;
  case 3:
    fr = FLYCAPTURE_FRAMERATE_3_75;
    break;
  case 7:
    fr = FLYCAPTURE_FRAMERATE_7_5;
    break;
  case 15:
    fr = FLYCAPTURE_FRAMERATE_15;
    break;
  case 30:
    fr = FLYCAPTURE_FRAMERATE_30;
    break;
  case 60:
    fr = FLYCAPTURE_FRAMERATE_60;
    break;
  case 120:
    fr = FLYCAPTURE_FRAMERATE_120;
    break;
  default:
    fr = FLYCAPTURE_FRAMERATE_15;
    break;
  }
  FlyCaptureError error = FLYCAPTURE_OK;
  error = flycaptureStart(ctx, CAPTURE_RESOLUTION, fr);
  FLYCAPTURE_FRAMERATE_30;
  if( NOT_OK )
    return false;

  HANDLE thrd = NULL;
  thrd = CreateThread(NULL, 0, thrd_grab, (LPVOID)this, 0, NULL);
  if( thrd == INVALID_HANDLE_VALUE )
    return false;
  CloseHandle(thrd);

  CREATE_SUSPENDED;

  return true;
}
bool PGObj::snap()
{
  if( !mode_trigger() )
    return FALSE;
  FlyCaptureFrameRate fr = FLYCAPTURE_FRAMERATE_30;
  FlyCaptureError error = FLYCAPTURE_OK;
  error = flycaptureStart(ctx, FLYCAPTURE_VIDEOMODE_1280x960Y8, fr);
  if( !soft_trigger_once() )
    return false;
  FlyCaptureImage info;
  error = flycaptureGrabImage2(ctx, &info);
  if( error == FLYCAPTURE_OK )
  {
    callback(co.serial,
      info.pData,
      info.iCols,
      info.iRows);
  }
  error = flycaptureStop(ctx);

  return true;
}
void PGObj::stop()
{
  stop_requested = true;
  flycaptureStop(ctx);
}

void PGObj::waiter(calc_opt* c)
{
  int ring = c->buffer_index;
  while (true)
  {
    busy = false;
    WaitForSingleObject(c->cap_event, INFINITE);
    if( free_requested )
      break;
    busy = true;
    ResetEvent(c->cap_event);
    if( callback )
      callback(
      //this->cam_index, 
      co.serial,
      co.cap_info.pData, 
      co.cap_info.iCols, 
      co.cap_info.iRows);

    flycaptureUnlock(ctx, ring);
  }
}
#define INITIAL_UINT ((UINT)-1)
void PGObj::loop()
{
  if( !ctx )
    return;
  FlyCaptureImage cap_info = {0};
  FlyCaptureError error = FLYCAPTURE_OK;
  UINT last_seq = INITIAL_UINT;

  while( !stop_requested )
  {
    error = flycaptureGrabImage2(ctx, &cap_info);
//     if( last_seq != INITIAL_UINT )
//     {
//       if( cap_info.uiSeqNum-last_seq != 1 &&
//           callback )
//       {
//         // 丢帧了
//         callback(co[cap_info.uiBufferIndex].serial,
//           NULL, last_seq, cap_info.uiSeqNum);
//       }
//     }
//     last_seq = cap_info.uiSeqNum;

    // if it's external triggering...
    // it will block here till the ex trigger take place, or STOP is requested.
    FLYCAPTURE_INVALID_ARGUMENT;

    if( stop_requested )
      break;
    if( NOT_OK )
    {
      //std::wcerr<<_T("flycaptureLockNext returns ")<<error<<std::endl;
      //Sleep(1);
      continue;
    }
//     if( cap_info.uiBufferIndex >= BUFFERS_EACH_CAM )
//     {
//       //std::wcerr<<_T("buffer index overflow!")<<std::endl;
//       continue;
//     }
    if( busy )
    {
      // missing
      //std::wcerr<<_T("missing frame: channel is busy.")<<std::endl;
      continue;
    }
    memcpy(&co.cap_info, &cap_info, sizeof(FlyCaptureImage));
    
//     if( async )
//       SetEvent(co[cap_info.uiBufferIndex].cap_event);
//     else
//     {

      if( callback )
        callback(
        co.serial, 
        cap_info.pData, 
        cap_info.iCols, 
        cap_info.iRows);

//       flycaptureStop(ctx);
//       stop_requested = true;
//       break;
//     }
  }
}

DWORD PGObj::thrd_grab(LPVOID param)
{
  PGObj* obj = (PGObj*)param;
  obj->loop();
  
#ifdef _DEBUG
  printf("thrd_grab #%d exiting...\n", obj->cam_index);
#endif
  return 0;
}

