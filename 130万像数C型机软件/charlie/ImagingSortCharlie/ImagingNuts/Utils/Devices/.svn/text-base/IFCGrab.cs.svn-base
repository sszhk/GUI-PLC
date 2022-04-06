using System.Runtime.InteropServices;
using System;
namespace ImagingSortCharlie.Utils.Devices
{
  public class IFCGrab : IGrab
  {
    private static bool present = false;
    static IFCGrab()
    {
      IntPtr h = LoadLibrary("IFC21.DLL");
      if (h.ToInt32() != 0)
      {
        present = true;
        //FreeLibrary(h);
      }
    }
    public static bool Present { get { return present; } }

    [DllImport("Kernel32.dll")]
    public static extern IntPtr LoadLibrary(string lpFileName);
    [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    uint handle = 0;

    public uint Handle { get { return handle; } }
    public bool Init(uint dev_idx, DelegateGrabCallback callback)
    {
      if (!present)
        return false;
      if (handle != 0)
        return false;
      handle = IFCDev.IFCInit(dev_idx, callback, null);
      return handle != 0;
    }
    public void Free()
    {
      if (!present)
        return; 
      if (handle != 0) { IFCDev.IFCFree(handle); handle = 0; }
    }
    public bool SetTrigger(int trigger)
    {
      if (!present)
        return false; 
      if (handle != 0) return IFCDev.IFCSetTrigger(handle, trigger); return false;
    }
    public int GetTrigger()
    {
      if (!present)
        return 0; 
      if (handle != 0) return IFCDev.IFCGetTrigger(handle); else return 0;
    }
    public int GetWidth()
    {
      if (!present)
        return 0; 
      if (handle != 0) return IFCDev.IFCWidth(handle); else return 0;
    }
    public int GetHeight()
    {
      if (!present)
        return 0; 
      if (handle != 0) return IFCDev.IFCHeight(handle); else return 0;
    }
    public int GetBpp()
    {
      if (!present)
        return 0; 
      if (handle != 0) return IFCDev.IFCBpp(handle); else return 0;
    }
    public bool Start()
    {
      if (!present)
        return false; 
      if (handle != 0) return IFCDev.IFCStart(handle); else return false;
    }
    public void Stop()
    {
      if (!present)
        return; 
      if (handle != 0) IFCDev.IFCStop(handle);
    }
    public bool Live()
    {
      if (!present)
        return false; 
      if (handle != 0) return IFCDev.IFCLive(handle); return false;
    }
    public bool Snapshot() { return false; }
    public void ShowSetup(uint parent, bool show)
    {
//       if (!present)
//         return;
//       if (handle != 0)
//         GrabShowSetup(handle, parent, show);
    }
    public bool SaveSettings(string file)
    {
      return false;
    }
    public bool LoadSettings(string file, bool apply_to_all)
    {
      return false;
    }
    public uint GetSerial(int cam_idx)
    {
      return 0;
    }
    public int GetTotalCameras()
    {
      return 0;
    }
  }
}