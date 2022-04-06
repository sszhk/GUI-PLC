using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

//namespace ImagingNuts.Log
//{
  public static class enLog
  {
    [DllImport("IMAGEANA.DLL")]
    public extern static bool log_init(
      [MarshalAs(UnmanagedType.LPStr)]
      string log_file
      );
    [DllImport("IMAGEANA.DLL")]
    public extern static void log_finish();

    public static void info(string fmt)
    {
      log_info("#" + fmt);
    }
    public static void info(string fmt, params object[] p)
    {
      string s = string.Format(fmt, p);
      info(s);
    }
    public static void debug(string s)
    {
      log_debug("#" + s);
    }
    public static void debug(string fmt, params object[] p)
    {
      string s = string.Format(fmt, p);
      debug(s);
    }
    public static void warning(string s)
    {
      log_warning("#" + s);
    }
    public static void warning(string fmt, params object[] p)
    {
      string s = string.Format(fmt, p);
      warning(s);
    }
    public static void error(string s)
    {
      log_error("#" + s);
    }
    public static void error(string fmt, params object[] p)
    {
      string s = string.Format(fmt, p);
      error(s);
    }
    public static void fatal(string s)
    {
      log_fatal("#" + s);
    }
    public static void fatal(string fmt, params object[] p)
    {
      string s = string.Format(fmt, p);
      fatal(s);
    }
    public static void enter_function(string fun)
    {
      info("+{0}", fun);
    }
    public static void exit_function(string fun)
    {
      info("-{0}", fun);
    }

    [DllImport("IMAGEANA.DLL")]
    private extern static void log_info([MarshalAs(UnmanagedType.LPStr)]
      string s
      );
    [DllImport("IMAGEANA.DLL")]
    private extern static void log_debug([MarshalAs(UnmanagedType.LPStr)]
      string s
      );
    [DllImport("IMAGEANA.DLL")]
    private extern static void log_warning([MarshalAs(UnmanagedType.LPStr)]
      string s
      );
    [DllImport("IMAGEANA.DLL")]
    private extern static void log_error([MarshalAs(UnmanagedType.LPStr)]
      string s
      );
    [DllImport("IMAGEANA.DLL")]
    private extern static void log_fatal([MarshalAs(UnmanagedType.LPStr)]
      string s
      );
    [DllImport("IMAGEANA.DLL")]
    public extern static void log_set_level(
      [MarshalAs(UnmanagedType.I4)]
      log_level level
      );
  }
  public enum log_level
  {
    LV_INFO = 0,
    LV_DEBUG = 1,
    LV_WARNING = 2,
    LV_ERROR = 3,
    LV_FATAL = 4,
    LV_LAST = LV_FATAL,
    LV_CLOSE = 99,
    LV_ALL = LV_INFO
  }
//}
