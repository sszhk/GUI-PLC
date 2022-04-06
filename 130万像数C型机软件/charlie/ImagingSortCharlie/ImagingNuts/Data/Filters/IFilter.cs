//using NationalInstruments.CWIMAQControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using ImagingSortCharlie.Data.Filters;
using System.Collections;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Data.Filters
{
  //[Serializable]
  public abstract class IFilter//: IXmlSerializable
  {
    #region - 公共属性 -
    public virtual string ID { get { return uniqueID; } set { uniqueID = value; } }
    public virtual string Name { get { if (name == null) return GetInternalType().Name; return name; } set { name = value; } }
    public abstract string TypeName { get; }
    public virtual bool Enabled { get { return enabled; } set { enabled = value; } }
    public bool IsCorrection { get { return correction; } }
    public bool Corrected { get { return corrected; } set { corrected = value; } }
    public ISetting Setting { get { return configure; } }
    public PublicOptions PublicOptions = new PublicOptions(VIEW.NONE, 40, true);
    public int ToolID { get { return toolID; } }
    #endregion

    public abstract void ShowTool();
    public abstract void HideTool();
    public abstract void ShowSpeak();
    protected abstract ImagingSortCharlie.Data.Filters.NIPoint DisplayAt();
    public virtual void DisplayTitle()
    {
      if (!IsVisible)
        return;

      ImagingSortCharlie.Data.Filters.NIPoint pt = DisplayAt();
      IA.OverlayTextOptions oto = new IA.OverlayTextOptions(20);
      oto.bold = 1;
      IA.IA_OverlayDrawText2(
        PublicOptions.View,
        ref pt,
        Name,
        System.Drawing.Color.Blue.ToArgb(),
        ref oto);
    }

    protected virtual void SetColor(Color cl)
    {
      color = cl;
      IAROI.IA_ROI_SetColor(PublicOptions.View, toolID, cl.ToArgb());
    }
    public Type GetInternalType() { return this.GetType(); }
    public abstract void Apply(List<ResultValue> this_time);
    public virtual void Remove() { IAROI.IA_ROI_Remove(PublicOptions.View, toolID); }
    /// <summary>
    /// 返回true表示状态正常
    /// 返回false表示异常，该滤波器将会被自动删除
    /// </summary>
    /// <param name="type"></param>
    /// <param name="windownumber"></param>
    /// <param name="tool"></param>
    /// <returns></returns>
    public abstract bool RegionsChanged(
      IA.WindowEventType type,
      int windownumber,
      IA.Tool tool,
      NIRect rc);
    public virtual void Initialize(VIEW viewer)
    {
      PublicOptions.View = viewer;
    }


    public abstract bool Passed { get; }

    public float CalcConcentricity(string id, NIPointFloat pt)
    {
      if (id == null || id.Length == 0)
        return -1;
      NIPointFloat cc = new NIPointFloat();
      IFilter obj = GD.SettingsForView(PublicOptions.View).Search(id);
      if (obj is CalcDiameter)
      {
        cc = ((CalcDiameter)obj).DiameterReport.center;
        return Utils.Geo.Point2Point(pt, cc);
      }
      if (obj is CalcHexagon)
      {
        cc = ((CalcHexagon)obj).HexagonReport.center;
        return Utils.Geo.Point2Point(pt, cc);
      }
      if (obj is CalcSquare)
      {
        cc = ((CalcSquare)obj).SquareReport.center;
        return Utils.Geo.Point2Point(pt, cc);
      }
      return -1;
    }

    #region - 设置 -
    public abstract UserControl Setup();
    public virtual bool IsSettingsChanged()
    {
      return false;
    }
    public virtual bool IsCorrectionValueChanged()
    {
      return false;
    }
    #endregion
    #region - 计算结果 -

    /// <summary>
    ///计算未达标
    /// </summary>
    /// <param name="descr"></param>
    /// <param name="fmt"></param>
    /// <param name="others"></param>
    public void AddNotMeet(List<ResultValue> this_time,
      string descr, string fmt, params object[] others)
    {
      Color cl = COLOR_NOT_MEET;
      VIEW view = this.PublicOptions.View;
      if (view == VIEW.NONE)
      {
        return;
      }
      string v = string.Format(fmt, others);
      AddResultValue(this_time, new ResultValue(false, Name,
        DateTime.Now, PublicOptions.View, descr, v, this,
        cl, ResultType.NotMeet));
    }
    private void AddResultValue(List<ResultValue> this_time, ResultValue rv)
    {
      if (GD.is_edit && GD.current_filter != this)
        return;
      FilterList fl = GD.SettingsForView(this.PublicOptions.View);
      if (fl == null)
        return;
      fl.AddResultValue(this_time, rv);
    }
    /// <summary>
    /// 计算时间
    /// </summary>
    /// <param name="others"></param>
    public void AddDuration(List<ResultValue> this_time, params object[] others)
    {
      Color cl = COLOR_DURATION;
      string descr = GD.GetString("00001")/*"计算时间"*/;
      string fmt = "{0:0.000} ms";
      string str = string.Format(fmt, others);
      VIEW view = this.PublicOptions.View;
      if (view == VIEW.NONE)
      {
        return;
      }
      AddResultValue(this_time, new ResultValue(this != null, Name,
        DateTime.Now, PublicOptions.View, descr, str, this,
        cl, ResultType.Duration));
    }

    /// <summary>
    /// 计算未找到
    /// </summary>
    /// <param name="descr"></param>
    /// <param name="fmt"></param>
    public void AddNotFound(List<ResultValue> this_time, string descr, string fmt)
    {
      Color cl = COLOR_ERROR;
      VIEW view = this.PublicOptions.View;
      if (view == VIEW.NONE)
      {
        return;
      }
      AddResultValue(this_time,
        new ResultValue(false, Name,
        DateTime.Now, PublicOptions.View, descr, fmt, this,
        cl, ResultType.NotFound));
    }
    /// <summary>
    /// 计算正确
    /// </summary>
    /// <param name="descr"></param>
    /// <param name="fmt"></param>
    /// <param name="others"></param>
    public void AddPass(List<ResultValue> this_time, string descr, string fmt, params object[] others)
    {
      Color cl = COLOR_DEFAULT;
      if (this == null)
        cl = COLOR_ERROR;
      //计算未达标
      string str = string.Format(fmt, others);
      VIEW view = this.PublicOptions.View;
      if (view == VIEW.NONE)
      {
        return;
      }
      AddResultValue(this_time, new ResultValue(this != null, Name,
        DateTime.Now, PublicOptions.View,
        descr, str, this, cl, ResultType.Succeeded));
    }
    /// <summary>
    /// 计算二值化
    /// </summary>
    /// <param name="descr"></param>
    /// <param name="fmt"></param>
    public void AddResult(List<ResultValue> this_time, string descr, string fmt)
    {
//       if (GD.IsLive)
//         return;

      Color cl = COLOR_DEFAULT;
      if (this == null)
        cl = COLOR_ERROR;
      //计算错误
      VIEW view = this.PublicOptions.View;
      if (view == VIEW.NONE)
      {
        return;
      }
      AddResultValue(this_time, new ResultValue(this != null, Name,
        DateTime.Now, PublicOptions.View, descr, fmt,
        this, cl, ResultType.NotFound));
    }
    #endregion
    #region - 调试 -
    protected void D(string fmt) { System.Diagnostics.Debug.Print(fmt); }
    protected void D(string fmt, params object[] parm) { System.Diagnostics.Debug.Print(fmt, parm); }
    #endregion
    #region - 继承属性 -
    public static Color COLOR_DURATION = Color.Gold;
    public static Color COLOR_PASS = Color.YellowGreen;
    public static Color COLOR_ERROR = Color.Red;
    public static Color COLOR_NOT_MEET = Color.Magenta;
    public static Color COLOR_DEFAULT = COLOR_PASS;
    public static Color COLOR_SITE = Color.Blue;

    protected string uniqueID = DateTime.Now.ToBinary().ToString("X");
    protected bool IsVisible { get { return toolID != 0; } }
    protected ISetting configure;
    protected Color color = Color.SteelBlue;
    protected string name = null;
    protected int toolID = 0;
    protected bool enabled = true;
    protected bool correction = true;
    protected bool corrected = false;
    protected NIRect[] boundary = new NIRect[3] { new NIRect(), new NIRect(), new NIRect() };
    #endregion
  }
}
[Serializable]
public enum ResultType
{
  Succeeded = 0,
  NotFound,
  NotMeet,
  Duration,
  Other,
  Site
}
[Serializable]
public struct ResultValue
{
  public bool Passed;
  public ResultType ResultType;
  public DateTime Time;
  public VIEW View;
  public string Name;
  public string Key;
  public string Value;
  [XmlIgnore]
  public IFilter Owner;
  public ResultValue(
    bool passed, string name, DateTime when, 
    VIEW vi, string k, string v, IFilter o, 
    Color cl, ResultType rt)
  { Passed = passed; Name = name; Time = when; 
    View = vi; Key = k; Value = v; Owner = o; 
    Color = cl; ResultType = rt; }
  [XmlIgnore]
  public Color Color;
}
