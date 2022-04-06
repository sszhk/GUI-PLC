using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Data.Settings;

  public static class IAROI
  {
    [DllImport("IMAGEANA.DLL")]
    public extern static int IA_ROI_AddRotatedRectangle(
      VIEW idx, 
      [MarshalAs(UnmanagedType.Struct)] ref NIRotatedRect rr,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect boundary);

    [DllImport("IMAGEANA.DLL")]
    public extern static int IA_ROI_AddRectangle(
      VIEW idx,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect rc,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect boundary);

    [DllImport("IMAGEANA.DLL")]
    public extern static int IA_ROI_AddAnnulus(
      VIEW idx,
      [MarshalAs(UnmanagedType.Struct)] ref NIAnnulus an,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect boundary);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_ROI_GetRotatedRectangle(
      VIEW idx,
      int id,
      [MarshalAs(UnmanagedType.Struct)]ref NIRotatedRect rc,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect boundary);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_ROI_GetRectangle(
      VIEW idx,
      int id,
      [MarshalAs(UnmanagedType.Struct)]ref NIRect rc,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect boundary);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_ROI_GetAnnulus(
      VIEW idx,
      int id,
      [MarshalAs(UnmanagedType.Struct)]ref NIAnnulus an,
      [MarshalAs(UnmanagedType.Struct)] ref NIRect boundary);

    [DllImport("IMAGEANA.DLL")]
    public extern static bool IA_ROI_Remove(VIEW idx, int id);

    [DllImport("IMAGEANA.DLL")]
    public extern static void IA_ROI_SetColor(VIEW idx, int id, int color);

    public const int ALL_ROI = -1;
  }
