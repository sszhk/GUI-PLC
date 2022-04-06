using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Data.Settings;

  public static class IAProcess
  {
    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_Process_BinarizeManual(
      [MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
      [MarshalAs(UnmanagedType.Struct)]
      ref NIRect rc,
      [MarshalAs(UnmanagedType.Struct)]
      ref BinarizeOptions mask,
      bool isFullScreen);

    [DllImport("IMAGEANA.DLL")]
    private extern static unsafe int IA_Process_BinarizeAuto(VIEW idx, NIRect* mask, bool isFullScreen);

    public static int IA_Process_BinarizeAuto1(VIEW idx,
      [MarshalAs(UnmanagedType.Struct)]
      ref NIRect mask,
      bool isFullScreen)
    {
      unsafe
      {
        fixed (NIRect* ptr = &mask)
        {
          return IA_Process_BinarizeAuto(idx, ptr, isFullScreen);
        }
      }
    }

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_Process_RemoveNoise(
      VIEW idx,
      [MarshalAs(UnmanagedType.Struct)]
      ref NIRect mask);

  }
