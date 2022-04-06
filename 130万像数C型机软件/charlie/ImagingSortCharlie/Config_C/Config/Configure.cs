using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
namespace ConfigApp
{
  [Serializable]
  public class Configure
  {
    public string SequenceNumber = "";
    public bool EnableSetupCamera = false;
    public log_level LogLevel = log_level.LV_CLOSE;
    public bool Language = true;
    public bool Test = true;
    public bool Wizard = false;
    public bool DataSheet = false;
    public bool ConsecutiveBadParts = true;
    public bool Pack = true;
    public bool ClearPlate = true;
    public bool CheckFeed = true;
    #region --滤波器设置与隐藏
    public bool Binarize = true;
    public bool Distance = true;
    public bool Angle = true;
    public bool ThreadDamage = true;
    public bool Diameter = true;
    public bool Thread = true;
    public bool Hexagon = true;
    public bool Head = true;
    public bool CenterCrack = false;
    public bool Weld = true;
    public bool Square = true;
    public bool Starving = true;
    public bool ThreadCount = true;
    public bool ThreadLocate = true;
    public bool Cushion = true;
    public bool FillArea = true;
    public bool Marking = true;
    public bool Area = true;
    #endregion
    #region--测试状态
    public string label_X00 = "X00";
    public string label_X10 = "X10";
    public string label_X0B = "X0B";
    public string label_X11 = "X11";
    public string label_X12 = "X12";
    public string label_X13 = "X13";
    public string label_X15 = "X15";
    public string label_X16 = "X16";
    public string label_X17 = "X17";
    public string label_X18 = "X18";
    public string label_X19 = "X19";
    #endregion

    #region --相机站点设置
    public int CameraCount = 0;
    public int TeamCount = 5;
    public int StationCount = 4;
    public List<uint> ViewSerial = new List<uint>();
    public List<TEAM> ViewTeam = new List<TEAM>();
    public List<STATION> ViewStation = new List<STATION>();
    #endregion
    static bool load(string path)
    {
      try
      {
        This = Xml.XMLUtil<Configure>.LoadXml(path);
      }
      catch
      {
        return false;
      }
      return This != null;
    }

    public static bool load()
    {
      string dir = Path.GetDirectoryName(Application.ExecutablePath);
      string path = Path.Combine(dir, PATH);
      string path_backup = Path.Combine(dir, PATH_BACKUP);
      if (!File.Exists(path) && !File.Exists(path_backup))
      {
        save(path);
        save(path_backup);
        return true;
      }
      if (!load(path))
      {
        if (load(path_backup))
        {
          save(path);
          return true;
        }
        return false;
      }
      return true;
    }

    public static void GetCurrentDirectory()
    {
      Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
    }

    public static string AppPath()
    {
      return Path.GetDirectoryName(Application.ExecutablePath);
    }

    static bool save(string path)
    {
      if (This == null)
        return false;
      GetCurrentDirectory();
      try
      {
        return Xml.XMLUtil<Configure>.SaveXml(path, This);
      }
      catch
      {
        return false;
      }
    }

    public static bool save()
    {
      string dir = Path.GetDirectoryName(Application.ExecutablePath);
      string path = Path.Combine(dir, PATH);
      string path_backup = Path.Combine(dir, PATH_BACKUP);
      bool result = save(path) && save(path_backup);
      return result;
    }
    public static Configure This = new Configure();
    [XmlIgnore]
    const string PATH = "Configure.xml";
    const string PATH_BACKUP = "Configure.bk";

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

    public enum TEAM
    {
      TEAM1 = 0,
      TEAM2,
      TEAM3,
      TEAM4,
      TEAM5,
      TEAMCOUNT
    }

    public enum VIEW
    {
      NONE = -1,
      VIEW1 = 0,
      VIEW2,
      VIEW3,
      VIEW4,
      VIEW5,
      VIEW6,
      VIEW7,
      VIEW_COUNT
    }

    public enum STATION
    {
      STATION1 = 0,
      STATION2,
      STATION3,
      STATION4,
      STATION_COUNT
    }

  }
}
