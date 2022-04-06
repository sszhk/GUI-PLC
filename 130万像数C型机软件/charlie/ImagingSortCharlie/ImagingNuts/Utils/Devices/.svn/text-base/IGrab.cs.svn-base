namespace ImagingSortCharlie.Utils.Devices
{
  public unsafe delegate void DelegateGrabCallback(uint cam_serial, byte* p, int width, int height);
  public interface IGrab
  {
    uint Handle { get; }
    bool Init(uint dev_idx, DelegateGrabCallback callback);
    void Free();
    bool SetTrigger(int trigger);
    int GetTrigger();
    int GetWidth();
    int GetHeight();
    int GetBpp();
    bool Start();
    void Stop();
    bool Live();
    bool Snapshot();
    void ShowSetup(uint parent, bool show);
    bool SaveSettings(string file);
    bool LoadSettings(string file, bool apply_to_all);
    uint GetSerial(int cam_idx);
    int GetTotalCameras();
  }
  public static class GrabSelector
  {
    private static bool is_ifc = true;
    private static bool initialized = false;
    private static IGrab first_select(uint dev_idx, DelegateGrabCallback callback)
    {
      IFCGrab ifc = new IFCGrab();
      PGGrab pg = new PGGrab();
      initialized = true;
      if ( IFCGrab.Present && ifc.Init(dev_idx, callback))
      {
        is_ifc = true;
        return ifc;
      }
      else
      {
        if( PGGrab.Present && pg.Init(dev_idx, callback) )
        {
          is_ifc = false;
          return pg;
        }
      }
      initialized = false;
      return null;
    }
    public static int Total { get { if (PGGrab.Present) return PGGrab.Total; return 0; } }
    public static bool IsIFC { get { return initialized && is_ifc; } }
    public static bool IsPG { get { return initialized && !is_ifc; } }
    public static void Reset()
    {
      initialized = false;
      is_ifc = false;
    }
    public static IGrab Instance(uint dev_idx, DelegateGrabCallback callback)
    {
      if (!initialized)
        return first_select(dev_idx, callback);
      if( is_ifc )
      {
        IFCGrab ifc = new IFCGrab();
        if (ifc.Init(dev_idx, callback))
        {
          return ifc;
        }
      }
      else
      {
        PGGrab pg = new PGGrab();
        if (pg.Init(dev_idx, callback))
          return pg;
      }
      return null;
    }
  }
}
