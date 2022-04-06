using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
// using NationalInstruments.CWIMAQControls;
using System.Drawing;
using System.IO;
using System.Reflection;
//using ImagingNuts.Hardware;
using ImagingSortCharlie.Data.Settings;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  public class FilterList : ArrayList, IXmlSerializable
  {
    #region - 影像设置 -
    public int Zoom = 0;
    public int View = (int)VIEW.NONE;
    public string ImageFile = null;
    public bool Enable = true;
    public bool EnableReport = true;
    #endregion

    #region - 计算结果列表 -
    private const int RESERVED_HISTORY = 500;
    private const int RESERVED_RESULTS = 40;
    [NonSerialized]
    private RingList<List<ResultValue>> ResultHistory =
      new RingList<List<ResultValue>>(RESERVED_HISTORY);

    public void ClearHistory()
    {
      lock (lock_results)
      {
        ResultHistory.Clear();
      }
    }
    public int ResultsCount()
    {
      lock(lock_results)
      {
        return ResultHistory.Count;
      }
    }
    public List<ResultValue> GetLastResult()
    {
      lock(lock_results)
      {
        return ResultHistory.Current;
      }
    }
    public int CurrentResultsIndex()
    {
      lock(lock_results)
      {
        return ResultHistory.CurrentIndex;
      }
    }
    public List<ResultValue> GetResultHistory(int idx)
    {
      lock (lock_results)
      {
        return ResultHistory.GetItem(idx);
      }
    }
    private void AddResults(List<ResultValue> this_time)
    {
      lock(lock_results)
      {
        ResultHistory.Push(this_time);
      }
    }
    public void AddResultValue(List<ResultValue> this_time, ResultValue rv)
    {
      if (this_time == null)
        return;

      if (GD.IsLive)
        return;
      if (!EnableReport)
      {
        return;
      }
      this_time.Add(rv);
//       lock (lock_results)
//       {
//         ResultHistory.Add(this_time);
//       }
      //this_time.Add(rv);
    }

    public void AddResult(
      List<ResultValue> this_time,
      VIEW vi, IFilter data, System.Drawing.Color cl, 
      string descr, string fmt, ResultType resultType)
    {
      AddResultValue(this_time, new ResultValue(data != null, GD.GetString("90001")/*"<无名称>"*/,
        DateTime.Now, vi, descr, fmt, data, cl, resultType));
    }
    /// <summary>
    /// 计算站点
    /// </summary>
    public void AddSite(List<ResultValue> this_time, int site)
    {
      if (View == (int)VIEW.NONE)
      {
        return;
      }
      AddResultValue(this_time, new ResultValue(
        this != null, GD.GetString("90002")/*"站点"*/,
        DateTime.Now, (VIEW)this.View, GD.GetString("90002")/*"站点"*/, site.ToString(), null, System.Drawing.Color.Blue,
        ResultType.Site));

      //       AddResultValue(new ResultValue(data != null, "", DateTime.Now,
      //         vi, descr, fmt, null, cl, 0, rt, site));
    }
    #endregion 

    public FilterList()
    {
    }
    public FilterList(VIEW idx)
    {
      View = (int)idx;
      ImageFile = string.Format("{0}{0}.jpg", (int)idx + 1);
      //this.Add(new ImageBinarize());
    }
    #region - 滤波器处理 -
    public void MoveUp(IFilter fil)
    {
      ArrayList lst = ArrayList.Synchronized(this);
      int i = 0;
      for (i = 0; i < lst.Count; i++)
      {
        if (lst[i] == fil)
          break;
      }
      if (i == 0 ||
        i == lst.Count)
        return;
      this.Reverse(i - 1, 2);
    }
    public void MoveDown(IFilter fil)
    {
      ArrayList lst = ArrayList.Synchronized(this);

      int i = 0;
      for (i = 0; i < lst.Count; i++)
      {
        if (lst[i] == fil)
          break;
      }
      if (i == (lst.Count - 1) ||
        i == lst.Count)
        return;
      this.Reverse(i, 2);
    }
    public override int Add(object value)
    {
      int r = base.Add(value);
      GD.CurrentFiltersChanged();
      return r;
    }
    public void Removes(object obj)
    {
      IFilter fil = (IFilter)obj;
      fil.Remove();
      base.Remove(obj);
      IAROI.IA_ROI_Remove((VIEW)View, IAROI.ALL_ROI);

      ArrayList lst = ArrayList.Synchronized(this);
      foreach (IFilter item in lst)
      {
        item.Initialize((VIEW)View);
      }
    }
    #endregion

    [NonSerialized]
    public EventHandler AfterApply = new EventHandler(dummy);
    private static void dummy(object sender, EventArgs e) { }
    // 是否通知界面更新？（计算结果列表等）
    public bool Apply(bool add_to_records)
    {
      enLog.enter_function("FilterList.Apply");

      ResultType rt = ResultType.Other;
      /* int site=MachineController.Site;*/
      //GD.ClearOverlay(view);
      bool result = true;

      List<ResultValue> this_time = new List<ResultValue>(RESERVED_RESULTS);
      //this.NewResults();
      if (Enable)
      {
        foreach (IFilter op in this)
        {
          enLog.enter_function(op.Name + ".Apply");
          bool only_diaplay_this = (GD.is_edit && op == GD.current_filter);
          if (only_diaplay_this)
            IA.IA_OverlayClear((VIEW)View);
          op.Apply(this_time);
          enLog.exit_function(op.Name + ".Apply");
          result &= op.Passed;
          if (only_diaplay_this)
            break;
        }
        if( add_to_records )
          AddResults(this_time);
      }
      else
      {
        this.AddResult(this_time, 
          (VIEW)View, null, 
          System.Drawing.Color.SteelBlue, GD.GetString("90003")/*"信息"*/, GD.GetString("90004")/*"该路影像未启用"*/, rt);
        AddResults(this_time);
        result = true;  // 未启用影像，不影响计算结果
      }
      if (!GD.IsLiveOrSpinning)
        DisplayResult(this_time);
      
      if(add_to_records )
        AfterApply(this, new CustomEvent(this_time));

      enLog.exit_function("FilterList.Apply");
      return result;
    }
    private void DisplayResult(List<ResultValue> this_time)
    {
      if ((VIEW)View == VIEW.NONE)
        return;
      Random rnd = new Random();
      string result = "";
      //CWIMAQTextOptions options = new CWIMAQTextOptions();
      
      //options.BackColor = 0x2000000;
      int x = 10;
      
      //const int TRANSPARENT = 0x01000000;
      int forecolor = 0;
#if HD_VERSION
      NIPoint pt = new NIPoint(10, 30);
      IA.OverlayTextOptions options = new IA.OverlayTextOptions(36);
      int y = 30;
#else
      NIPoint pt = new NIPoint(10, 15);
      IA.OverlayTextOptions options = new IA.OverlayTextOptions(18);
      int y = 15;
#endif

      foreach (ResultValue p in this_time)
      {
        forecolor = p.Color.ToArgb();
        result = string.Format("{2} {0}: {1}\r\n", p.Key, p.Value, (p.Owner != null) ? p.Owner.TypeName : "");
        pt = new NIPoint(x, y);
        //IA.IA_OverlayDrawText(view, ref pt, result, forecolor, TRANSPARENT, 12, false, false, 0);
        IA.IA_OverlayDrawText2((VIEW)View, ref pt, result, forecolor, ref options);
#if HD_VERSION
        y += 30;
#else
        y += 15;
#endif
        //         if( y >= GD.ImageHeight - 30 )
        //         {
        //           x += 150;
        //           y = 15;
        //         }
      }
    }
    public void InitializeFilters(VIEW viewer)
    {
      ArrayList lst = ArrayList.Synchronized(this);
      View = (int)viewer;
      GD.OnRegionsChanged -= GD_OnRegionsChanged;
      GD.OnRegionsChanged += GD_OnRegionsChanged;
      foreach (IFilter op in lst)
      {
        op.Initialize(viewer);
      }
    }

    public IFilter Search(string id)
    {
      ArrayList lst = ArrayList.Synchronized(this);
      foreach (IFilter fil in lst)
      {
        if (fil.ID == id)
        {
          return fil;
        }
      }
      return null;
    }

    public List<IFilter> ConcentricityList(IFilter filter)
    {
      List<IFilter> lst = new List<IFilter>();
      foreach (IFilter f in ArrayList.Synchronized(this))
      {
        if (f == filter)
        {
          break;
        }
        if (f is CalcHexagon ||
          f is CalcDiameter ||
          f is CalcSquare)
        {
          lst.Add(f);
        }
      }
      return lst;
    }

    public static void SaveHistory(VIEW idx)
    {
      FilterList fl = GD.SettingsForView(idx);
      DateTime now = DateTime.Now;
      string history = string.Format("{7}.{0:00}{1:00}{2:00}.{3:00}{4:00}{5:00}.{6:X4}.csv",
        now.Year % 100,
        now.Month,
        now.Day,
        now.Hour,
        now.Minute,
        now.Second,
        now.Ticks % 0xFFFF,
      idx.ToString());
      history = Path.Combine(Settings.Settings.AppPath(), history);
      
      List<string[]> results = new List<string[]>();
      results.Add(new string[] { 
        //"站点,",
        GD.GetString("90005")/*"时间,"*/,
        GD.GetString("90006")/*"检测项目,"*/,
        GD.GetString("90007")/*"影像,"*/,
        GD.GetString("90008")/*"子项目,"*/,
        GD.GetString("90009")/*"是否成功,"*/,
        GD.GetString("90010")/*"计算结果"*/
      });
      for (int i = 0; i < fl.Count+1; i++)
      {
        List<ResultValue> l = fl.GetResultHistory(i);
        for (int j = 0; j < l.Count; j++)
        {
          results.Add(new string[] {
                          //l[j].Site.ToString(),
                         ",\""+l[j].Time.ToString("yyyy-MM-dd HH.mm.ss.FFF")+"\"",
                         ",\"" + l[j].Name +"\"",
                         ",\"" + l[j].View.ToString()+"\"",
                         ",\"" + l[j].Key+"\"",
                         ",\"" + (l[j].Passed?"":GD.GetString("90011")/*"失败"*/)+"\"",
                         ",\"" + l[j].Value/*.Replace(',', ';')*/+"\""});
        }
      }
    }
    void GD_OnRegionsChanged(IA.WindowEventType type, int windownumber, IA.Tool tool,
      Data.Filters.NIRect rc)
    {
      if (type != IA.WindowEventType.IMAQ_DRAW_EVENT ||
        windownumber != View)
      {
        return;
      }
      //       List<IFilter> lst = new List<IFilter>();
      GD.ClearOverlay((VIEW)View);
      ArrayList lst = ArrayList.Synchronized(this);
#if !DEBUG
      EnableReport = false;
#endif
      foreach (IFilter op in lst)
      {
        if (op.ToolID != 0)
        {
          op.RegionsChanged(type, windownumber, tool, rc);
        }
      }
      EnableReport = true;
    }
    //private STATION_INDEX view = STATION_INDEX.NONE;
    private object lock_results = new object();

    static void WriteToPDF(Document document, string text, float size, bool bold, bool isCentered)
    {
      BaseFont bfHei = BaseFont.CreateFont("C:\\Windows\\Fonts\\msyh.TTF", 
        BaseFont.IDENTITY_H, 
        BaseFont.NOT_EMBEDDED);
      iTextSharp.text.Font font = new iTextSharp.text.Font(bfHei, size, bold ? 1 : 0);
      Paragraph p = new Paragraph(text, font);
      p.Alignment = isCentered ? 1 : 0;
      document.Add(p);
    }

    static void WriteToPDF(Document document, PdfContentByte cb, string text, float size, float x, float y)
    {
      BaseFont bfHei = BaseFont.CreateFont("C:\\Windows\\Fonts\\msyh.TTF",
        BaseFont.IDENTITY_H,
        BaseFont.NOT_EMBEDDED);
      cb.BeginText();
      cb.SetFontAndSize(bfHei, size);
      cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
      cb.EndText();
    }

    public static void SaveToPDF()
    {
      Document document = new Document();
      PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("E:\\1.pdf", FileMode.Create));
      PdfContentByte cb = writer.DirectContent;
      document.Open();
      string text = GD.GetString("90012")/*"检 测 结 果"*/;
      WriteToPDF(document, text, 32, true, true);

      WriteToPDF(document, cb, GD.GetString("90013")/*"检测总数"*/, 24, 120, 660);
      WriteToPDF(document, cb, (Settings.Settings.PS.BadSum + Settings.Settings.PS.GoodSum).ToString(), 24, 420, 660);

      WriteToPDF(document, cb, GD.GetString("90014")/*"良品总数"*/, 24, 120, 620);
      WriteToPDF(document, cb, Settings.Settings.PS.GoodSum.ToString(), 24, 420, 620);

      WriteToPDF(document, cb, GD.GetString("90015")/*"不良品总数"*/, 24, 120, 580);
      WriteToPDF(document, cb, Settings.Settings.PS.BadSum.ToString(), 24, 420, 580);

      WriteToPDF(document, cb, GD.GetString("90016")/*"第一影像不良品总数"*/, 24, 120, 540);
      WriteToPDF(document, cb, Settings.Settings.PS.BadCount[0].ToString(), 24, 420, 540);

      WriteToPDF(document, cb, GD.GetString("90017")/*"第二影像不良品总数"*/, 24, 120, 500);
      WriteToPDF(document, cb, Settings.Settings.PS.BadCount[1].ToString(), 24, 420, 500);

      WriteToPDF(document, cb, GD.GetString("90018")/*"第三影像不良品总数"*/, 24, 120, 460);
      WriteToPDF(document, cb, Settings.Settings.PS.BadCount[2].ToString(), 24, 420, 460);

      WriteToPDF(document, cb, GD.GetString("90019")/*"批次："*/, 20, 50, 300);
      WriteToPDF(document, cb, Settings.Settings.PS.Batch, 16, 145, 300);

      WriteToPDF(document, cb, GD.GetString("90020")/*"炉号："*/, 20, 340, 300);
      WriteToPDF(document, cb, Settings.Settings.PS.HeatNumber, 16, 440, 300);

      WriteToPDF(document, cb, GD.GetString("90021")/*"订货单："*/, 20, 50, 250);
      WriteToPDF(document, cb, Settings.Settings.PS.Orders, 16, 145, 250);

      WriteToPDF(document, cb, GD.GetString("90022")/*"材质："*/, 20, 340, 250);
      WriteToPDF(document, cb, Settings.Settings.PS.Material, 16, 440, 250);

      WriteToPDF(document, cb, GD.GetString("90023")/*"操作员："*/, 20, 50, 200);
      WriteToPDF(document, cb, Settings.Settings.PS.Operator, 16, 145, 200);

      WriteToPDF(document, cb, GD.GetString("90024")/*"时间："*/, 20, 340, 200);
      WriteToPDF(document, cb, Settings.Settings.PS.TestingData, 16, 440, 200);

      document.Close(); 
    }

    #region - XML Serialization -
    public System.Xml.Schema.XmlSchema GetSchema()
    {
      return null;
    }
    private void ReadThisMembers(System.Xml.XmlReader reader)
    {
      while (true)
      {
        if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
        {
          return;
        }
        FieldInfo fi = this.GetType().GetField(reader.Name);
        if (fi == null)
        {
          //string str = string.Format("Invalid field in XML: {0}", reader.Name);
          //throw new Exception(str);
          return;
        }
        fi.SetValue(this, reader.ReadElementContentAs(fi.FieldType, null));
      }
      //       if( reader.Name == "Zoom" )
      //         Zoom = reader.ReadElementContentAsInt();
    }
    private void WriteThisMembers(System.Xml.XmlWriter writer)
    {
      FieldInfo[] fis = this.GetType().GetFields();
      foreach (FieldInfo fi in fis)
      {
        if (fi.ReflectedType != this.GetType())
          continue;
        if (fi.IsStatic)
          continue;
        if (!fi.IsPublic)
          continue;
        if (fi.IsLiteral)
          continue;
        if (fi.IsNotSerialized)
          continue;
        if ((fi.MemberType & MemberTypes.Field) == 0 &&
          (fi.MemberType & MemberTypes.Property) == 0)
          continue;
        string v = fi.GetValue(this).ToString();
        if (fi.GetValue(this) is bool)
        {
          v = v.ToLower();
        }
        writer.WriteElementString(fi.Name, v);
      }
      //       MemberInfo[] mis = this.GetType().GetMembers();
      //       foreach (MemberInfo mi in mis)
      //       {
      //         if (mi.MemberType != MemberTypes.Field &&
      //           mi.MemberType != MemberTypes.Property )
      //           continue;
      //         if (mi.DeclaringType != this.GetType())
      //           continue;
      //         writer.WriteElementString(mi.Name, mi.ToString());
      //       }
      //writer.WriteElementString("Zoom", Zoom.ToString());
    }
    public void ReadXml(System.Xml.XmlReader reader)
    {
      this.Clear();
      string NAME = this.GetType().Name;
      // 刘鑫发现问题：
      // <FilterList />
      if (reader.IsEmptyElement)
      {
        reader.Read();
        return;
      }
      if (!reader.Read())
        return;
      ReadThisMembers(reader);
      while (true)
      {
        if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
        {
          reader.Read();
          break;
        }
        if (reader.NodeType == System.Xml.XmlNodeType.Element)
        {
          Type t = Type.GetType(typeof(FilterList).Namespace + "." + reader.Name);

          if (t != null)
          {
            XmlSerializer x = new XmlSerializer(t);
            object o = x.Deserialize(reader);
            if (o.GetType() == t)
              this.Add(o);
            if (reader.NodeType == System.Xml.XmlNodeType.EndElement &&
              reader.Name == NAME)
            {
              reader.Read();
              break;
            }
          }
        }
      }
    }
    public void WriteXml(System.Xml.XmlWriter writer)
    {
      WriteThisMembers(writer);
      foreach (IFilter op in this)
      {
        XmlSerializer x = new XmlSerializer(op.GetInternalType());
        x.Serialize(writer, op);
      }
    }
    #endregion
  }
  public class CustomEvent:EventArgs
  {
    public object custom;
    public CustomEvent(object o) { custom = o; }
  }
}
