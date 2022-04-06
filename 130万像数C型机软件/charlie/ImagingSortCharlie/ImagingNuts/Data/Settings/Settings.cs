using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Filters;

namespace ImagingSortCharlie.Data.Settings
{
  [Serializable]
  public class PortableSettings
  {
    public int[] BadCount = new int[(int)TEAM.TEAMCOUNT] { 0, 0, 0, 0, 0 };
    public int BadSum = 0;
    public int GoodSum = 0;
    public int Total = 0;
    public int ClearTotal = 0;
    public int SetBadTime = 30;//设置吹气信号ON的时间
    public int ConsecutiveBadParts = 100;
    public int ClearPlate = 10000;
    public int NoFeedTime = 1000;
    public int ProfileColor = Color.OrangeRed.ToArgb();
    public int PackCount = 100;
    public bool EnablePack = false;
    public bool EnableConsecutiveBadParts = false;
    public bool EnableClearPlate = false;
    public bool EnableCheckFeed = false;
    public VIEW LastActiveView = VIEW.VIEW1;
    public string ScreenshotPath = @"E:\Screenshots";
    public string ScreenshotName = "";
    public FilterList[] ViewFilters = 
      new FilterList[(int)VIEW.VIEW_COUNT] 
      { 
      new FilterList(VIEW.VIEW1),
      new FilterList(VIEW.VIEW2),
      new FilterList(VIEW.VIEW3),
      new FilterList(VIEW.VIEW4),
      new FilterList(VIEW.VIEW5),
      new FilterList(VIEW.VIEW6),
      new FilterList(VIEW.VIEW7),
      new FilterList(VIEW.VIEW8),
     };
    public string Batch = "20091025";
    public string Material = "硫化铜";
    public string Orders = "20091025-1000";
    public string HeatNumber = "10001001";
    public string TestingData = "2009：10：25";
    public string Operator = "刘鑫";

    public static PortableSettings load(string profile, bool backup)
    {
      string dir = Path.GetDirectoryName(Application.ExecutablePath);
      string extension = backup ? ".bk" : ".cfg";
      profile = Path.ChangeExtension(profile, extension);
      string path = Path.Combine(dir, profile);
      PortableSettings ps = new PortableSettings();
      try
      {
        ps = Xml.XMLUtil<PortableSettings>.LoadXml(path);
      }
      catch
      {
        return null;
      }
      return ps;
    }
    public static bool save(PortableSettings ps, string profile, bool backup)
    {
      string dir = Path.GetDirectoryName(Application.ExecutablePath);
      string extension = backup ? ".bk" : ".cfg";
      profile = Path.ChangeExtension(profile, extension);
      string path = Path.Combine(dir, profile);
      try
      {
        return Xml.XMLUtil<PortableSettings>.SaveXml(path, ps);
      }
      catch
      {
        return false;
      }
    }
  }

  [Serializable]
  public class Settings
  {
    /// <summary>
    /// 独立设置文档，不要带有任何路径名称或扩展名。例如："默认设置"
    /// 程序会自动加上路径以及".xml"扩展名。
    /// </summary>
    public string Profile = DEFAULT_PROFILE;
    public int Language = 2052;
    public static string ProfileName
    {
      get { if (This != null) return This.Profile; return null; }
      set { if (This != null) This.Profile = value; }
    }

    public static int Lang
    {
      get { return This.Language; }
      set { This.Language = value; }
    }

    #region - 静态 -
    public static string AppPath()
    {
      return Path.GetDirectoryName(Application.ExecutablePath);
    }

    static bool save_ps(bool backup)
    {
      if (This == null)
        This = new Settings();
      if (PS != null)
        return PortableSettings.save(PS, This.Profile, backup);
      return false;
    }

    public static bool save_ps()
    {
      bool result = save_ps(false) && save_ps(true);
      return result;
    }

    static bool load_ps(string file_name)
    {
      PS = PortableSettings.load(file_name, false);
      if (PS == null)
      {
        PS = PortableSettings.load(This.Profile, true);
        if(PS == null)
          return false;
        save_ps(false);
      }
      return true;
    }

    static void ps_changed()
    {
      GD.CurrentFiltersChanged();
      if (OnResultChanged != null)
      {
        OnResultChanged(null, null);
      }
    }

    public static bool load_ps()
    {
      if (This == null)
      {
        This = new Settings();
        save_ps();
        ps_changed();
        return false;
      }
      if(!load_ps(This.Profile))
      {
        This.Profile = DEFAULT_PROFILE;
        PS = new PortableSettings();
        save_ps();
        ps_changed();
        return false;
      }
      ps_changed();
      return true;
    }

    static bool load(string file_name)
    {
      string dir = Path.GetDirectoryName(Application.ExecutablePath);
      string path = Path.Combine(dir, file_name);
      try
      {
        This = Xml.XMLUtil<Settings>.LoadXml(path);
      }
      catch
      {
        return false;
      }
      return This != null;
    }

    public static bool load()
    {
      if (!load(PATH))
      {
        if (load(PATH_BACKUP))
        {
          save(PATH);
          return true;
        }
        This = new Settings();
        save();
        return false;
      }
      return true;
    }
    
    static bool save(string file_name)
    {
      string dir = Path.GetDirectoryName(Application.ExecutablePath);
      string path = Path.Combine(dir, file_name);
      try
      {
        return Xml.XMLUtil<Settings>.SaveXml(path, This);
      }
      catch
      {
        return false;
      }
    }

    public static bool save()
    {
      bool result = save(PATH) && save(PATH_BACKUP);
      return result;
    }

    private static Settings This = new Settings();
    [XmlIgnore]
    public static EventHandler OnResultChanged;
    public static PortableSettings PS = new PortableSettings();
    const string PATH = "Settings.xml";
    const string PATH_BACKUP = "Settings.bk";
    const string DEFAULT_PROFILE = "默认设置";
    #endregion
  }

}
