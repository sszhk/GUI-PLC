using System.Runtime.InteropServices;
namespace ImagingSortCharlie.Utils.Devices
{
  internal static class IFCDev
  {
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern uint IFCInit(uint board_idx, DelegateGrabCallback callback, DelegateMissing missing);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern void IFCFree(uint board);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern bool IFCSetTrigger(uint board,int trigger);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern int IFCGetTrigger(uint board);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern int IFCWidth(uint board);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern int IFCHeight(uint board);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern int IFCBpp(uint board);

    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern bool IFCStart(uint board);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern void IFCStop(uint board);
    [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
    public static extern bool IFCLive(uint board);

//     [DllImport("IFCGrabNuts.dll", CharSet = CharSet.Auto)]
//     public static extern void IFCReleaseSeq(uint board, int grabid, int seq);

    //public unsafe delegate void DelegateIFC(uint board_idx, byte* p, int width, int height);
    public delegate void DelegateMissing(uint board_idx);
  }
}