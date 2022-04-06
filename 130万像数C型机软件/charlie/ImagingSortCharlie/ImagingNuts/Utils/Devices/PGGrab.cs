//130w像素需要定义，另需在GD.cs和IAMain.h里面同时定义
//#define HD_VERSION
using System;
using System.Runtime.InteropServices;
namespace ImagingSortCharlie.Utils.Devices
{
  internal class PGGrab : IGrab
  {
    private static bool present = false;
    private static int total = 0;
    static PGGrab()
    {
      IntPtr h = LoadLibrary("PGRFLYCAPTURE.DLL");
      if (h.ToInt32() != 0)
      {
        present = true;
        //FreeLibrary(h);
      }
      total = PGGrab.GrabTotalCameras();
    }
    public static bool Present { get { return present; } }
    public static int Total { get { return total; } }

    private uint handle = 0;
#if HD_VERSION
    private const string dll_string = "PGGrab130.dll";
#else
    private const string dll_string = "PGGrabII.dll";
#endif

    // public 
    public uint Handle { get { return handle; } }
    public bool Init(uint dev_idx, DelegateGrabCallback callback)
    {
      if (!present)
        return false;
      if (handle != 0)
        Free();
      handle = GrabInit(dev_idx, callback);
      //SaveSettings("cam0.xml");
      return handle != 0;
    }
    public void Free()
    {
      if (!present)
        return;
      if (handle != 0)
      {
        GrabFree(handle);
        handle = 0;
      }
    }
    public bool SetTrigger(int trigger)
    {
      if (!present)
        return false;
      if (handle != 0) { GrabSetTrigger(handle, trigger); } return false;
    }
    public int GetTrigger()
    {
      if (!present)
        return 0;
      if (handle != 0) { return GrabGetTrigger(handle); } return 0;
    }
    public int GetWidth()
    {
      if (!present)
        return 0;
      if (handle != 0) return GrabWidth(handle); return 0;
    }
    public int GetHeight()
    {
      if (!present)
        return 0;
      if (handle != 0) return GrabHeight(handle); return 0;
    }
    public int GetBpp()
    {
      if (!present)
        return 0;
      if (handle != 0) return GrabBpp(handle); return 0;
    }
    public bool Start()
    {
      if (!present)
        return false;
      if (handle != 0) return GrabStart(handle); return false;
    }
    public void Stop()
    {
      if (!present)
        return;
      if (handle != 0) GrabStop(handle);
    }
    public bool Live()
    {
      if (!present)
        return false;
      if (handle != 0) return GrabLive(handle); return false;
    }
    public bool Snapshot()
    {
      if (!present)
        return false;
      if (handle != 0) return GrabSnap(handle); return false;
    }
    public void ShowSetup(uint parent, bool show)
    {
      if (!present)
        return;
      if (handle != 0)
        GrabShowSetup(handle, parent, show);
    }
    public bool SaveSettings(string file)
    {
      if (!present)
        return false;
      if (handle != 0)
        return GrabSaveSettings(handle, file);
      return false;
    }
    public bool LoadSettings(string file, bool apply_to_all)
    {
      if (!present)
        return false;
      if (handle != 0)
        return GrabLoadSettings(handle, file, apply_to_all);
      return false;
    }
    public uint GetSerial(int cam_idx)
    {
      if (!present)
        return 0;
      if (handle != 0)
        return GrabGetSerial(cam_idx);
      return 0;
    }
    public int GetTotalCameras()
    {
      if (!present)
        return 0;
      return GrabTotalCameras();
    }

    // DLL declaration
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern uint GrabInit(uint cam_idx, DelegateGrabCallback callback);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern void GrabFree(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern bool GrabSetTrigger(uint handle, int trigger);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern int GrabGetTrigger(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern int GrabWidth(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern int GrabHeight(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern int GrabBpp(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern bool GrabStart(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern void GrabStop(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern bool GrabLive(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern bool GrabSnap(uint handle);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern void GrabShowSetup(uint handle,uint parent, bool show);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern bool GrabSaveSettings(uint handle, [MarshalAs(UnmanagedType.LPStr)]string file);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern bool GrabLoadSettings(uint handle, [MarshalAs(UnmanagedType.LPStr)]string file, bool apply_to_all);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern uint GrabGetSerial(int cam_idx);
    [DllImport(dll_string, CharSet = CharSet.Auto)]
    public static extern int GrabTotalCameras();

    [DllImport("Kernel32.dll")]
    public static extern IntPtr LoadLibrary(string lpFileName);
    [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    //     [DllImport("PGGrabII.dll", CharSet = CharSet.Auto)]
    //     public static extern void PGGrabReleaseSeq(uint handle, int grabid, int seq);

  }
}