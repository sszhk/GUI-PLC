using System.Runtime.InteropServices;
using System;
namespace ImagingSortCharlie.Utils.Devices
{
  public enum PCI1710_DIGITAL_SWITCH
  {
    PCI1710_SW_NONE = -1,
    PCI1710_SW_OFF = 0,
    PCI1710_SW_ON = 1
  };

  public static class PCI1710
  {
    private static bool present = false;
    static PCI1710()
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

    public static bool Init() { return PCI1710_Init(); }
    public static PCI1710_DIGITAL_SWITCH GetDI(int port)
    {
      lock (access)
      {
        return PCI1710_GetDI(port);
      }
    }
    public static void DO_ON(int port)
    {
      SetDO(port, PCI1710_DIGITAL_SWITCH.PCI1710_SW_ON);
    }
    public static void DO_OFF(int port)
    {
      SetDO(port, PCI1710_DIGITAL_SWITCH.PCI1710_SW_OFF);
    }
    public static void SetDO(int port, PCI1710_DIGITAL_SWITCH sw)
    {
      lock (access)
      {
        PCI1710_SetDO(port, sw);
      }
    }
    public static void SetAO(int ch, float volt)
    {
      lock (access)
      {
        //PCI1710_SetAO(ch, volt);
      }
    }

    public static void Free() { PCI1710_Free(); }

    [DllImport("PCI1710.dll")]
    private static extern bool PCI1710_Init();
    [DllImport("PCI1710.dll")]
    private static extern PCI1710_DIGITAL_SWITCH PCI1710_GetDI(int port);
    [DllImport("PCI1710.dll")]
    private static extern void PCI1710_SetDO(int port, PCI1710_DIGITAL_SWITCH sw);
    [DllImport("PCI1710.dll")]
    private static extern void PCI1710_SetAO(int ch, float volt);
    [DllImport("PCI1710.dll")]
    private static extern void PCI1710_Free();
    //         [DllImport("PCI1710.dll")]
    //         private static extern void PCI1710_SetSpinSpeed(int PCSperMinute);

    private static object access = new object();
  }

}