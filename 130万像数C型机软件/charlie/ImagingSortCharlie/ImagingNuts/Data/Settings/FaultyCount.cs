using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using ImagingSortCharlie.Hardware;
using ImagingSortCharlie.Data.Filters;
namespace ImagingSortCharlie.Data.Settings
{
  static class FaultyCount
  {
    const string CAT_TBL_NAME = "CATEGORY";
    const string CAT_NAME = "CAT_NAME";
    const string CAT_TOTAL = "CAT_TOTAL";
    const string CAT_GOOD = "CAT_GOOD";
    const string CAT_BAD = "CAT_BAD";
    const string CAT_SUBTYPE = "CAT_SUBTYPE";

    const string SUB_TBL_NAME = "SUBTYPE";
    const string SUB_NAME = "SUB_NAME";
    const string SUB_COUNT = "SUB_COUNT";

    private static DataTable cat_template = new DataTable(CAT_TBL_NAME);
    private static DataTable sub_template = new DataTable(SUB_TBL_NAME);
    private static DataTable[] tables = new DataTable[(int)VIEW.VIEW_COUNT];
    private static List<ResultValue>[] arr_list = new List<ResultValue>[(int)VIEW.VIEW_COUNT];

    private static object lk = new object();

    static FaultyCount()
    {
      DataColumn dc = cat_template.Columns.Add(CAT_NAME, typeof(string));
      dc.Unique = true;
      cat_template.Columns.Add(CAT_BAD, typeof(int));
      cat_template.Columns.Add(CAT_GOOD, typeof(int));
      cat_template.Columns.Add(CAT_TOTAL, typeof(int));
      cat_template.Columns.Add(CAT_SUBTYPE, typeof(DataTable));
      cat_template.PrimaryKey = new DataColumn[] { dc };

      dc = sub_template.Columns.Add(SUB_NAME, typeof(string));
      dc.Unique = true;
      sub_template.Columns.Add(SUB_COUNT, typeof(int));
      sub_template.PrimaryKey = new DataColumn[] { dc };
      for (int i = 0; i < (int)VIEW.VIEW_COUNT; i++)
      {
        tables[i] = cat_template.Clone() as DataTable;
      }
    }
    private static DataTable new_subtype()
    {
      return sub_template.Clone() as DataTable;
    }
    private static DataTable get_table(VIEW view)
    {
      //       if (GD.IsSpinning)
      //       {
      //       }
      //       else
      //         tables[(int)view].Clear();
      return tables[(int)view];
    }
    /// <summary>
    ///根据view和category名字，找到业已存在的记录，并返回
    ///如果找不到，则插入新记录，并返回      
    /// </summary>
    /// <param name="view"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    private static DataRow find_category(VIEW view, string category)
    {
      DataTable dt = get_table(view);
      lock (lk)
      {
        DataRow[] rows = dt.Select(string.Format("{0}='{1}'", CAT_NAME, category));
        if (rows == null || rows.Length == 0 || DBNull.Value.Equals(rows[0][CAT_SUBTYPE]))
        {
          DataRow dr = dt.Rows.Add(category, 0, 0, 0, new_subtype());
          return dr;
        }
        return rows[0];
      }
    }
    /// <summary>
    /// 根据view和category和subtype名字，找到业已存在的记录，并返回
    /// 如果找不到，则插入新记录，并返回 
    /// </summary>
    /// <param name="view"></param>
    /// <param name="category"></param>
    /// <param name="subtype"></param>
    /// <returns></returns>
    private static DataRow find_subtype(VIEW view, string category, string subtype)
    {
      DataRow cat = find_category(view, category);
      DataTable sub = cat[CAT_SUBTYPE] as DataTable;
      lock (lk)
      {
        DataRow[] rows = sub.Select(string.Format("{0}='{1}'", SUB_NAME, subtype));
        if (DBNull.Value.Equals(rows) || rows == null || rows.Length == 0)
        {
          DataRow dr = sub.Rows.Add(subtype, 0);
          return dr;
        }
        return rows[0];
      }
    }
    /// <summary>
    ///按照category的选择，总数增加。
    /// </summary>
    /// <param name="view"></param>
    /// <param name="category"></param>
    public static void increase_total(VIEW view, string category)
    {
      if (GD.IsSpinning)
      {
        DataRow dr = find_category(view, category);
        int total = (int)dr[CAT_TOTAL];
        total++;
        lock (lk)
        {
          dr[CAT_TOTAL] = total;
        }
      }
    }
    /// <summary>
    /// 按照category的选择，不良品增加
    /// </summary>
    /// <param name="view"></param>
    /// <param name="category"></param>
    public static void increase_fail_cat(VIEW view, string category)
    {
      if (GD.IsSpinning)
      {
        DataRow dr = find_category(view, category);
        int fail = (int)dr[CAT_BAD];
        lock (lk)
        {
          dr[CAT_BAD] = fail + 1;
        }
      }

    }
    /// <summary>
    /// 按照subtype的选择，不良品增加
    /// </summary>
    /// <param name="view"></param>
    /// <param name="category"></param>
    /// <param name="subtype"></param>
    public static void increase_fail_sub(VIEW view, string category, string subtype)
    {
      if (GD.IsSpinning)
      {
        DataRow dr = find_subtype(view, category, subtype);
        int fail = (int)dr[SUB_COUNT];
        lock (lk)
        {
          dr[SUB_COUNT] = fail + 1;
        }
      }

    }
    /// <summary>
    /// 按照category的选择，良品增加
    /// </summary>
    /// <param name="view"></param>
    /// <param name="category"></param>
    public static void increase_pass(VIEW view, string category)
    {
      if (GD.IsSpinning)
      {
        DataRow dr = find_category(view, category);
        int pass = (int)dr[CAT_GOOD];
        lock (lk)
        {
          dr[CAT_GOOD] = pass + 1;
        }
      }

    }
    public static void test()
    {
      for (VIEW i = 0; i < VIEW.VIEW_COUNT; i++)
      {
        Debug.Print("View=" + i.ToString());
        Debug.Indent();
        DataTable dt = get_table(i);
        //Debug.Print("Category="+dt.TableName);
        foreach (DataRow r1 in dt.Rows)
        {
          DataTable sub = r1[CAT_SUBTYPE] as DataTable;
          Debug.Print("{0}", r1.ItemArray.Length);
          Debug.Print("Category={0}, Total={1}, Good={2}, Bad={3}", r1[CAT_NAME], r1[CAT_TOTAL], r1[CAT_GOOD], r1[CAT_BAD]);
          Debug.Indent();
          foreach (DataRow r2 in sub.Rows)
          {
            Debug.Print("SubName=" + r2[SUB_NAME] + ", Count=" + r2[SUB_COUNT]);
            Debug.Print("{0}", sub.Rows.Count);
          }
          Debug.Print("{0}", dt.Rows.Count);
          Debug.Unindent();
        }
        Debug.Unindent();
      }
    }
    private static string Time()
    {
      DateTime now = DateTime.Now;
      string time;
      time = string.Format("{0:00}{1:00}{2:00}.{3:00}{4:00}{5:00}.{6:X4}",
      now.Year % 100,
      now.Month,
      now.Day,
      now.Hour,
      now.Minute,
      now.Second,
      now.Ticks % 0xFFFF);
      return time;
    }
    public static void DataTabletoXML()
    {
      try
      {
        for (VIEW m = 0; m < VIEW.VIEW_COUNT; m++)
        {
          DataTable dt = get_table(m);
          dt.WriteXml(@"E:\Raw." + m.ToString() + ".xml");
        }
      }
      catch (System.Exception ex)
      {
        System.Diagnostics.Debug.Print(ex.Message);
      }
    }
    public static void ClearDataTable()
    {
      for (VIEW m = 0; m < VIEW.VIEW_COUNT; m++)
      {
        DataTable dt = get_table(m);
        dt.Clear();
      }
    }
    public static bool isClear()
    {
      bool success = false;
      for (VIEW m = 0; m < VIEW.VIEW_COUNT; m++)
      {
        DataTable dt = get_table(m);
        if (dt.Rows.Count == 0)
        {
          success = true;
        }
        else
          success = false;
      }
      return success;
    }
    public static void DataTabletoExcel()
    {
      try
      {
        //         SaveFileDialog saveFileDialog = new SaveFileDialog();
        //         saveFileDialog.Filter = "导出Excel (*.xlsx)|*.xls";
        //         saveFileDialog.FilterIndex = 0;
        //         saveFileDialog.RestoreDirectory = true;
        //         saveFileDialog.CreatePrompt = true;
        //         saveFileDialog.Title = "导出文件保存路径";
        //         saveFileDialog.ShowDialog();
        //         string strFileName = saveFileDialog.FileName;
        //         if (strFileName.Length != 0)
        //         {
        System.Windows.Forms.Application.DoEvents();
        //ImagingSortCharlie.Forms.Message.appear(ImagingSortCharlie.Utils.Speak.GetString("r25002"), ImagingSortCharlie.Forms.Message.MessageType.Info);
        //toolStripProgressBar1.Visible = true;
        //System.Windows.Forms.Application.DoEvents();
        System.Reflection.Missing miss = System.Reflection.Missing.Value;
        string ERROR_STR = "X";
        string strFileName = string.Format("E:\\data-{0}.xlsx", Time()); /*"d:\\data.xlsx";*/
        /*     string filename= Path.Combine(GetExeDir(), strFileName);*/
        Excel.ApplicationClass xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        xlApp.Application.Workbooks.Add(true); ;
        xlApp.Visible = false;//若是true，则在导出的时候会显示EXcel界面。
        xlApp.DefaultFilePath = "";
        xlApp.DisplayAlerts = true;
        xlApp.SheetsInNewWorkbook = 1;
        Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
        Excel.Worksheet wSheet = (Excel.Worksheet)xlBook.ActiveSheet;
        Excel.Range ran;
        int count = 0;
        int current = 0;
        int item_count = get_item_count();
        int row_count = GD.SettingsView1.ResultsCount();
        object[,] arr_data = new object[row_count, item_count];
        string[] arr_head = new string[item_count];
        System.Drawing.Color[,] arr_result = new System.Drawing.Color[row_count, item_count];
        for (VIEW i = 0; i < VIEW.VIEW_COUNT; i++)
        {
          FilterList fl = GD.SettingsForView(i);
          List<ResultValue> l = arr_list[(int)i];
          if (l == null)
          {
            continue;
          }
          for (int t = 0; t < l.Count; t++)
          {
            ResultValue rv = l[t];
            if (rv.Owner == null)
              continue;
            if (rv.ResultType == ResultType.Duration || rv.ResultType == ResultType.Site || rv.Owner.TypeName == GD.GetString("93001"))
              continue;
            xlApp.Cells[2, count + 1] = rv.Owner.TypeName;
            arr_head[count] = rv.Owner.TypeName;
            if (rv.ResultType == ResultType.NotFound)
              xlApp.Cells[3, count + 1] = "--";
            else
              xlApp.Cells[3, count + 1] = filter_substandard(rv.Key);
            count++;
          }

          for (int j = 0; j < count - current; j++)
          {
            xlApp.Cells[1, current + 1] = GD.GetString("93039") + (((int)i) + 1).ToString();//i.ToString();
            for (int k = 0; k < fl.ResultsCount(); k++)
            {
              List<ResultValue> lst = fl.GetResultHistory(k);
              if (lst == null)
                break;
              int temp = 0;
              for (int m = 0; m < lst.Count; m++)
              {
                ResultValue rst = lst[m];
                if (rst.Owner == null)
                  continue;
                if (rst.ResultType == ResultType.Site || rst.ResultType == ResultType.Duration || rst.Name == GD.GetString("93001"))
                  continue;
                if (rst.ResultType == ResultType.NotFound)
                {
                  for (int p = current; p < count; p++)
                  {
                    if (arr_head[p] == rst.Owner.TypeName)
                    {
                      arr_data[k, p] = ERROR_STR;
                      arr_result[k, p] = rst.Color;
                    }
                  }

                  //                     int reapeat_count = name_reapeat_count(lst,rst.Name, i, m);
                  //                     int key_len = get_key_length(rst.Name, i, reapeat_count);
                  //                     for (int n = 0; n < key_len; n++)
                  //                     {
                  //                       arr_data[k, current + temp + n] = ERROR_STR;
                  //                       arr_result[k, current + temp + n] = rst.Color;
                  //                     }
                }
                else
                {
                  arr_data[k, current + temp] = rst.Value;
                }
                if (!rst.Passed)
                {
                  arr_result[k, current + temp] = rst.Color;//如果不合格取颜色
                }
                //xlApp.Cells[k + 3, current + temp + 1] = rst.Key + ":" + rst.Value;
                //wSheet.get_Range(xlApp.Cells[k + 3, current + temp + 1], xlApp.Cells[k + 3, current + temp + 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(rst.Color);
                temp++;
              }
            }
          }
          if (count > current)
          {
            Excel.Range r = wSheet.get_Range(xlApp.Cells[1, current + 1], xlApp.Cells[1, count]);
            r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            r.MergeCells = true;
            current = count;
          }

        }
        if (item_count > 0 && row_count > 0)
        {
          ran = wSheet.get_Range(xlApp.Cells[4, 1], xlApp.Cells[row_count + 3, item_count]);
          ran.Value2 = arr_data;
          //冻结表头
          Excel.Range freeze_range = wSheet.get_Range(xlApp.Cells[4, 1], xlApp.Cells[4, item_count]);
          freeze_range.Select();
          xlApp.ActiveWindow.FreezePanes = true;
          //设置所有框线
          Excel.Range all_range = wSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[3 + row_count, item_count]);
          //all_range.Borders.get_Item(Excel.XlBordersIndex.xlDiagonalDown).LineStyle = Excel.XlLineStyle.xlContinuous;
          //all_range.Borders.get_Item(Excel.XlBordersIndex.xlDiagonalUp).LineStyle = Excel.XlLineStyle.xlContinuous;
          //all_range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
          all_range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
          all_range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
          //all_range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
          all_range.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
          all_range.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;
          //居中
          all_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        }
        //Utils.TimerCounter t = new ImagingSortCharlie.Utils.TimerCounter();
        for (int i = 0; i < row_count; i++)
        {
          for (int j = 0; j < item_count; j++)
          {
            if (arr_result[i, j] != null)
            {
              wSheet.get_Range(xlApp.Cells[i + 4, j + 1], xlApp.Cells[i + 4, j + 1]).Font.Color =
                System.Drawing.ColorTranslator.ToOle(arr_result[i, j]);
            }
          }
        }
        //ran.Formula
        System.Windows.Forms.Application.DoEvents();
        xlBook.SaveAs(strFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange
  , Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        xlBook.Close(false, Missing.Value, Missing.Value);
        xlApp.Quit();
        System.Runtime.InteropServices.Marshal.ReleaseComObject(wSheet);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
        xlApp = null;
        wSheet = null;
        xlBook = null;
        GC.Collect();
        ImagingSortCharlie.Forms.Message.appear(ImagingSortCharlie.Utils.Speak.GetString("r25001") + "\n" + strFileName, ImagingSortCharlie.Forms.Message.MessageType.OK);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "导出异常");
      }

    }
    public static void DataTabletoExcel_bk()
    {
      try
      {
        string strFileName = string.Format("E:\\data-{0}.xlsx", Time()); /*"d:\\data.xlsx";*/
        /*     string filename= Path.Combine(GetExeDir(), strFileName);*/
        Excel.ApplicationClass xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        xlApp.DefaultFilePath = "";
        xlApp.DisplayAlerts = true;
        xlApp.SheetsInNewWorkbook = 1;
        Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
        Excel.Worksheet wSheet = (Excel.Worksheet)xlBook.ActiveSheet;
        int rowIndex = 0;
        for (VIEW m = 0; m < VIEW.VIEW_COUNT; m++)
        {
          rowIndex++;
          DataTable dt = get_table(m);
          xlApp.Cells[rowIndex, 1] = m.ToString();
          foreach (DataRow r1 in dt.Rows)
          {
            rowIndex++;
            int CloumnNum = dt.Columns.Count;
            DataTable sub = r1[CAT_SUBTYPE] as DataTable;
            for (int i = 1; i < dt.Columns.Count; i++)
            {
              xlApp.Cells[rowIndex, i] = r1[i - 1].ToString();
              wSheet.get_Range(xlApp.Cells[rowIndex, i], xlApp.Cells[rowIndex, i]).Interior.Color = System.Drawing.Color.FromArgb(255, 204, 153).ToArgb();
            }
            wSheet.get_Range(xlApp.Cells[rowIndex, dt.Columns.Count - 2], xlApp.Cells[rowIndex, dt.Columns.Count - 2]).Font.ColorIndex = 4;
            wSheet.get_Range(xlApp.Cells[rowIndex, 2], xlApp.Cells[rowIndex, 2]).Font.ColorIndex = 3;
            foreach (DataRow r2 in sub.Rows)
            {
              rowIndex++;
              for (int n = 0; n < sub.Columns.Count; n++)
              {
                xlApp.Cells[rowIndex, n + 1] = "   " + r2[n];
                wSheet.get_Range(xlApp.Cells[rowIndex, n + 1], xlApp.Cells[rowIndex, n + 1]).Font.ColorIndex = 3;
              }
            }
          }
          //dt.Clear();
        }
//         rowIndex++;
//         xlApp.Cells[rowIndex, 1] = GD.GetString("25001");
//         rowIndex++;
//         xlApp.Cells[rowIndex, 1] = GD.GetString("26001");
        xlBook.SaveAs(strFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange
  , Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        xlApp.Quit();
        xlApp = null;
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.Print(e.Message);
        return;
      }
      //xlBook.SaveCopyAs(HttpUtility.UrlDecode(strFileName, System.Text.Encoding.UTF8));
    }
    public static void DataTabletoPDF()
    {
      /*string strFileName = "E:\\data.pdf";*/
      string strFileName = string.Format("E:\\data-{0}.pdf", Time());
      /*      string filename = Path.Combine(GetExeDir(), strFileName);*/
      BaseFont bfHei = BaseFont.CreateFont(@"c:\Windows\fonts\SIMHEI.TTF",
          BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
      Font font = new Font(bfHei, 30, 1, Color.RED);
      Font fontCat = new Font(bfHei, 25);
      Font fontSub = new Font(bfHei, 15);
      fontSub.SetStyle("normal");
      string space = "    ";
      Document dc = new Document(PageSize.A4.Rotate());
      try
      {
        PdfWriter.GetInstance(dc, new FileStream(strFileName, FileMode.Create));
      }
      catch
      {
        return;
      }
      dc.Open();
      for (VIEW i = 0; i < VIEW.VIEW_COUNT; i++)
      {
        DataTable dt = get_table(i);
        dc.NewPage();
        dc.Add(new Paragraph(i.ToString(), font));
        foreach (DataRow r1 in dt.Rows)
        {
          int CloumnNum = dt.Columns.Count;
          DataTable sub = r1[CAT_SUBTYPE] as DataTable;
          PdfPTable aTable = new PdfPTable(dt.Columns.Count - 1);
          int[] width = { 25, 25, 25, 25 };
          aTable.SetWidths(width);
          aTable.WidthPercentage = 100;
          aTable.HorizontalAlignment = Element.ALIGN_RIGHT;
          PdfPCell catname = new PdfPCell(new Phrase(r1[CAT_NAME].ToString(), fontCat));
          PdfPCell catbad = new PdfPCell(new Phrase(r1[CAT_BAD].ToString(), fontSub));
          PdfPCell catgood = new PdfPCell(new Phrase(r1[CAT_GOOD].ToString(), fontSub));
          PdfPCell cat_total = new PdfPCell(new Phrase(r1[CAT_TOTAL].ToString(), fontSub));
          catname.Border = Rectangle.NO_BORDER;
          catname.GrayFill = 0.9f;
          catbad.Border = Rectangle.NO_BORDER;
          catbad.GrayFill = 0.9f;
          /*          catbad.VerticalAlignment = Element.ALIGN_BOTTOM;*/
          catgood.Border = Rectangle.NO_BORDER;
          catgood.GrayFill = 0.9f;
          cat_total.Border = Rectangle.NO_BORDER;
          cat_total.GrayFill = 0.9f;
          aTable.AddCell(catname);
          aTable.AddCell(catbad);
          aTable.AddCell(catgood);
          aTable.AddCell(cat_total);
          foreach (DataRow r2 in sub.Rows)
          {
            PdfPCell subname = new PdfPCell(new Phrase(space + r2[SUB_NAME].ToString(), fontSub));
            PdfPCell subcount = new PdfPCell(new Phrase(r2[SUB_COUNT].ToString(), fontSub));
            PdfPCell spaceCell = new PdfPCell(new Phrase(" ", fontSub));
            subname.Border = Rectangle.NO_BORDER;
            subcount.Border = Rectangle.NO_BORDER;
            spaceCell.Border = Rectangle.NO_BORDER;
            aTable.AddCell(subname);
            aTable.AddCell(subcount);
            aTable.AddCell(spaceCell);
            aTable.AddCell(spaceCell);
          }
          dc.Add(aTable);
        }
        dt.Clear();
      }
      dc.Close();

    }
    //         public static void DataTabletoPDF(string strFileName)
    //         {
    //             BaseFont bfHei = BaseFont.createFont(@"c:\Windows\fonts\SIMHEI.TTF",
    //                 BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
    //             Font font = new Font(bfHei, 20);
    //             Font font1 = new Font(bfHei, 20);
    //             font1.setStyle("normal");         
    //             Document dc = new Document(PageSize.A4.rotate());
    //             try
    //             {
    //                 PdfWriter.getInstance(dc, new FileStream(strFileName, FileMode.Create));
    //             }
    //             catch
    //             {
    //                 return;
    //             }
    //             dc.Open();
    //             for (ViewIndex i = 0; i < ViewIndex.VIEW_COUNT; i++)
    //             {
    //                 DataTable dt = get_table(i);
    //                 dc.Add(new Paragraph(i.ToString(),font));
    //                 foreach (DataRow r1 in dt.Rows)
    //                 {
    //                     int CloumnNum = dt.Columns.Count;
    //                     DataTable sub = r1[CAT_SUBTYPE] as DataTable;
    //                     int interval = 15;
    //                     int intervalSub = 20;
    //                     int intervalSpace = 5;
    //                     string space="";
    //                     space = space.PadRight(intervalSpace, ' ');
    //                     string catname = string.Format("{0}", r1[CAT_NAME]);
    //                     catname = catname.PadRight(interval, ' ');
    //                     string cat_total = string.Format("Total={0}", r1[CAT_TOTAL]);
    //                     cat_total = cat_total.PadRight(interval, ' ');
    //                     string catgood = string.Format("Good={0}", r1[CAT_GOOD]);
    //                     catgood = catgood.PadRight(interval, ' ');
    //                     string catbad = string.Format("Bad={0}", r1[CAT_BAD]);
    //                     catbad = catbad.PadRight(interval, ' ');
    //                     dc.Add(new Paragraph(string.Format("{0}{1}{2}{3}", catname, cat_total, catgood, catbad), font));
    //                     foreach (DataRow r2 in sub.Rows)
    //                     {
    //                         string subname = string.Format("{0}", r2[SUB_NAME]);
    //                         subname = subname.PadRight(intervalSub, ' ');
    //                         string subcount = string.Format("Bad={0}", r2[SUB_COUNT]);
    //                         subcount = subcount.PadRight(intervalSub, ' ');
    //                         for (int n = 0; n < sub.Columns.Count; n++)
    //                         {
    //                             dc.Add(new Paragraph(string.Format("{0}{1}{2}",space,subname, subcount), font1));
    //                         }
    //                     }
    //                 }
    //             }
    //             dc.Close();
    //         }
    public static int get_item_count()
    {
      int count = 0;
      List<ResultValue> lst = null;
      List<ResultValue> max_list = null;
      int max = 0;
      for (VIEW i = 0; i < VIEW.VIEW_COUNT; i++)
      {
        max = 0;
        max_list = null;
        FilterList fl = GD.SettingsForView(i);
        for (int j = 0; j < fl.ResultsCount(); j++)
        {
          lst = fl.GetResultHistory(j);
          if (lst != null)
          {
            if (lst.Count > max)
            {
              max = lst.Count;
              max_list = lst;
            }
          }
        }
        if (max_list == null)
          continue;
        arr_list[(int)i] = max_list;
        foreach (ResultValue rv in max_list)
        {
          if (rv.Owner == null)
            continue;
          if (rv.ResultType == ResultType.Duration || rv.ResultType == ResultType.Site || rv.Owner.TypeName == GD.GetString("93001"))
            continue;
          //xlApp.Cells[2, count + 1] = rv.Name;
          count++;
        }
      }
      return count;
    }
    public static string filter_substandard(string s)
    {
      string filter_string = string.Empty;
      if (Settings.Lang == 1033)
      {
        filter_string = "is substandard";
      }
      else
      {
        filter_string = "未达标";
      }
      int idx = s.IndexOf(filter_string);
      if (idx > 0)
        return s.Substring(0, idx);
      else
        return s;
    }
    public static int get_key_length(string name, VIEW v, int idx)
    {
      int count = 0;
      int reapeat = 0;
      string str_reapeat = name;
      List<ResultValue> lst = arr_list[(int)v];
      for (int i = 1; i < lst.Count; i++)
      {
        ResultValue rv = lst[i];
        ResultValue pre_rv = lst[i - 1];
        if ((rv.Name == name && rv.Name != pre_rv.Name) || (rv.Name == name && rv.ResultType == ResultType.Duration))
        {
          count++;
        }
        if (idx == count)
        {
          for (int j = i; j < lst.Count; j++)
          {
            ResultValue rst = lst[j];
            ResultValue prv_rst = lst[j - 1];
            ResultValue next_rst;
            if (j + 1 < lst.Count)
            {
              next_rst = lst[j + 1];
              if ((rst.Name == name && rst.Name != prv_rst.Name) || (rst.Name == name && next_rst.ResultType == ResultType.Duration))
              {
                if (next_rst.ResultType == ResultType.Duration)
                  break;
              }
            }
            if (rst.Owner == null)
              continue;
            if (rst.ResultType == ResultType.Site || rst.Name == GD.GetString("93001"))
              continue;

            if (rst.Name == name)
              reapeat++;
          }
          return reapeat;
        }

      }
      return reapeat;
    }
  }
  

}
