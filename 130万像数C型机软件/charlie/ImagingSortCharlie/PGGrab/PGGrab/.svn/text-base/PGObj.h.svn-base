#ifndef _PGOBJ_H
#define _PGOBJ_H

//////////////////////////////////////////////////////////////////////////
// FlyCap includings
#pragma warning(disable: 4819)
#include <PGRFlyCaptureStereo.h>
#include <PGRFlyCapture.h>
#include <PGRFlyCapturePlus.h>
#include <PGRFlyCaptureMessaging.h>
#include <pgrflycapturegui.h>

// Maximum cameras on the bus. 
// (the maximum devices allowed on a 1394 bus is 64).
//
#define _MAX_CAMERAS  64

// Ring buffers for each camera
//

#ifdef PGGRAB_EXPORTS
#define DLL_PORTS __declspec( dllexport )
#else
#define DLL_PORTS __declspec( dllimport )
#endif
//////////////////////////////////////////////////////////////////////////
// PointGrey Object class
//

typedef DWORD (WINAPI *PFNCALLBACK)(UINT cam, BYTE* buf, int width, int height);

struct calc_opt
{
  void* obj;
  HANDLE cap_event;
  FlyCaptureImage cap_info;
  FlyCaptureCameraSerialNumber serial;
  int buffer_index;
  HANDLE waiter;
};

class /*DLL_PORTS*/ PGObj
{
public:
  PGObj();
  ~PGObj();

  bool init(UINT idx, PFNCALLBACK cb);
  void free();

  bool start(int FPS, bool async);
  void stop();
  bool is_running() const {return !stop_requested;}

  void mode_live();
  bool mode_trigger();
  bool soft_trigger_once();
  bool is_trigger_mode();

  void show_setup(HWND win, bool show);
  bool save_settings(LPCSTR file);
  bool load_settings(LPCSTR file, bool applytoall);
  bool snap();

private:
  bool create_context();
  bool alloc_buffers();
  bool set_cam_defaults();
  static DWORD WINAPI thrd_grab(LPVOID param);
  void loop();

  int cam_index;
  FlyCaptureContext ctx;
  CameraGUIContext gui_ctx;
  //FlyCaptureImagePlus cap_info;
  //BYTE* buffer;
  calc_opt co;
  static DWORD WINAPI thrd_waiter(LPVOID param);
  void waiter(calc_opt* c);

  int width;
  int height;
  int bpp;
  PFNCALLBACK callback;
  volatile bool stop_requested;
  volatile bool free_requested;
  volatile bool busy;

  bool async;

// static
  static UINT total_cams;
  static FlyCaptureInfoEx info[_MAX_CAMERAS];

public:
  static UINT total_cameras();
  static bool total_init();
  static void total_free();
  static int serial_num(int idx);

};

#endif