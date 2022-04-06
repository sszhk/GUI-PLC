using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Utils;
using System.IO;
using ImagingSortCharlie.Data.Filters;

namespace ImagingSortCharlie.Forms
{
  public partial class FormToolKit : UserControl
  {
//     public bool IsEdit;
//     IFilter ChangeFilter;
//     ListViewItem CurrentFilter;
    Timer tmChecker = new Timer();
    public event EventHandler OnRefresh;

    public FormToolKit()
    {
      InitializeComponent();
    }

    public void Init()
    {
      this.Controls.Clear();
      InitializeComponent();
      ReadParam();
      add_tools();
      InitlvTools();
      GD.InitializeAllFilters();
      Settings.save_ps();
      init_config();
      RefreshProfile();
      save_count();
    }

    private void init_config()
    {
      cb_consecutive_bad_parts.Visible = tb_consecutive_bad_parts.Visible = Configure.This.ConsecutiveBadParts;
      cb_pack.Visible = tb_pack.Visible = Configure.This.Pack;
      cb_clear_plate.Visible = tb_clear_plate.Visible = Configure.This.ClearPlate;
      lb_unit.Visible = cb_check_feed.Visible = tb_check_feed.Visible = Configure.This.CheckFeed;
      if (!Configure.This.ConsecutiveBadParts)
        Settings.PS.EnableConsecutiveBadParts = false;
      if (!Configure.This.Pack)
        Settings.PS.EnablePack = false;
      if (!Configure.This.ClearPlate)
        Settings.PS.EnableClearPlate = false;
      if (!Configure.This.CheckFeed)
        Settings.PS.EnableCheckFeed = false;
      btnShowSetup.Visible = Configure.This.EnableSetupCamera;
    }

    public void RefreshTools()
    {

      lb_header_tools.rearrange(lvTools);
      lb_header_tools.Refresh();
    }
    private void add_tools()
    {
      if (Configure.This.Binarize)
      {
        ListViewItem item_binarize = new ListViewItem(GD.GetString("93001"));
        item_binarize.UseItemStyleForSubItems = false;
        item_binarize.SubItems.Add(GD.GetString("93002"));
        lvTools.Items.Add(item_binarize);
      }
      if (Configure.This.Distance)
      {
        ListViewItem item_distance = new ListViewItem(GD.GetString("93003"));
        item_distance.UseItemStyleForSubItems = false;
        item_distance.SubItems.Add(GD.GetString("93004"));
        lvTools.Items.Add(item_distance);
      }
      if (Configure.This.Angle)
      {
        ListViewItem item_angle = new ListViewItem(GD.GetString("93005"));
        item_angle.UseItemStyleForSubItems = false;
        item_angle.SubItems.Add(GD.GetString("93006"));
        lvTools.Items.Add(item_angle);
      }
      if (Configure.This.ThreadDamage)
      {
        ListViewItem item_thread_damage = 
          new ListViewItem(GD.GetString("93007"));
        item_thread_damage.UseItemStyleForSubItems = false;
        item_thread_damage.SubItems.Add(GD.GetString("93008"));
        lvTools.Items.Add(item_thread_damage);
      }
      if (Configure.This.Diameter)
      {
        ListViewItem item_diameter = new ListViewItem(GD.GetString("93009"));
        item_diameter.UseItemStyleForSubItems = false;
        item_diameter.SubItems.Add(GD.GetString("93010"));
        lvTools.Items.Add(item_diameter);
      }
      if (Configure.This.Thread)
      {
        ListViewItem item_thread = new ListViewItem(GD.GetString("93011"));
        item_thread.UseItemStyleForSubItems = false;
        item_thread.SubItems.Add(GD.GetString("93012"));
        lvTools.Items.Add(item_thread);
      }
      if (Configure.This.Hexagon)
      {
        ListViewItem item_hexagon = new ListViewItem(GD.GetString("93013"));
        item_hexagon.UseItemStyleForSubItems = false;
        item_hexagon.SubItems.Add(GD.GetString("93014"));
        lvTools.Items.Add(item_hexagon);
      }
      if (Configure.This.Head)
      {
        ListViewItem item_head = new ListViewItem(GD.GetString("93015"));
        item_head.UseItemStyleForSubItems = false;
        item_head.SubItems.Add(GD.GetString("93016"));
        lvTools.Items.Add(item_head);
      }
      if (Configure.This.CenterCrack)
      {
        ListViewItem item_center_crack = 
          new ListViewItem(GD.GetString("93017"));
        item_center_crack.UseItemStyleForSubItems = false;
        item_center_crack.SubItems.Add(GD.GetString("93018"));
        lvTools.Items.Add(item_center_crack);
      }
      if (Configure.This.Weld)
      {
        ListViewItem item_node = new ListViewItem(GD.GetString("93019"));
        item_node.UseItemStyleForSubItems = false;
        item_node.SubItems.Add(GD.GetString("93020"));
        lvTools.Items.Add(item_node);
      }
      if (Configure.This.Square)
      {
        ListViewItem item_square = new ListViewItem(GD.GetString("93021"));
        item_square.UseItemStyleForSubItems = false;
        item_square.SubItems.Add(GD.GetString("93022"));
        lvTools.Items.Add(item_square);
      }
      if (Configure.This.Starving)
      {
        ListViewItem item_starving = new ListViewItem(GD.GetString("93023"));
        item_starving.UseItemStyleForSubItems = false;
        item_starving.SubItems.Add(GD.GetString("93024"));
        lvTools.Items.Add(item_starving);
      }
      if (Configure.This.ThreadCount)
      {
        ListViewItem item_thread_count = 
          new ListViewItem(GD.GetString("93025"));
        item_thread_count.UseItemStyleForSubItems = false;
        item_thread_count.SubItems.Add(GD.GetString("93026"));
        lvTools.Items.Add(item_thread_count);
      }
      if (Configure.This.ThreadLocate)
      {
        ListViewItem item_thread_locate = 
          new ListViewItem(GD.GetString("93027"));
        item_thread_locate.UseItemStyleForSubItems = false;
        item_thread_locate.SubItems.Add(GD.GetString("93028"));
        lvTools.Items.Add(item_thread_locate);
      }
      if (Configure.This.Cushion)
      {
        ListViewItem item_curshion = new ListViewItem(GD.GetString("93029"));
        item_curshion.UseItemStyleForSubItems = false;
        item_curshion.SubItems.Add(GD.GetString("93030"));
        lvTools.Items.Add(item_curshion);
      }
      if (Configure.This.FillArea)
      {
        ListViewItem item_fill_area = new ListViewItem(GD.GetString("93031"));
        item_fill_area.UseItemStyleForSubItems = false;
        item_fill_area.SubItems.Add(GD.GetString("93032"));
        lvTools.Items.Add(item_fill_area);
      }
      if (Configure.This.Marking)
      {
        ListViewItem item_marking = new ListViewItem(GD.GetString("93033"));
        item_marking.UseItemStyleForSubItems = false;
        item_marking.SubItems.Add(GD.GetString("93034"));
        lvTools.Items.Add(item_marking);
      }
      if (Configure.This.Area)
      {
        ListViewItem item_area = new ListViewItem(GD.GetString("93035"));
        item_area.UseItemStyleForSubItems = false;
        item_area.SubItems.Add(GD.GetString("93036"));
        lvTools.Items.Add(item_area);
      }
      if(Configure.This.WheelCrack)
      {
        ListViewItem item_wheel_crack = new ListViewItem(GD.GetString("93037"));
        item_wheel_crack.UseItemStyleForSubItems = false;
        item_wheel_crack.SubItems.Add(GD.GetString("93038"));
        lvTools.Items.Add(item_wheel_crack);
      }

    }
    private void InitlvTools()
    {
      for (int i = 0; i < lvTools.Items.Count; i++)
      {
        lvTools.Items[i].BackColor = lvTools.BackColor;
        if (Settings.Lang == 2052)
        {
          lvTools.Items[i].SubItems[0].Font = new Font(lvTools.Font.FontFamily, 12, FontStyle.Bold);
        }
        if (Settings.Lang == 1033)
        {
          lvTools.Items[i].SubItems[0].Font = new Font(lvTools.Font.FontFamily, 9, FontStyle.Bold);
        }
        lvTools.Items[i].SubItems[0].BackColor = lvTools.BackColor;
        lvTools.Items[i].SubItems[1].BackColor = lvTools.BackColor;
        lvTools.Items[i].SubItems[1].ForeColor = Color.OliveDrab;
      }

    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      Setup();
    }

    public event EventHandler OnSetup;
    void Setup()
    {
      if (OnSetup != null)
      {
        OnSetup(null, null);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (GD.current_filter == null)
        return;
      GD.is_edit = false;
      GD.current_filter.Setting.OnCancel();
      RefreshFilters();
      DefaultSettings(true);
      GD.PreviewCurrent();
      UpdateButtons();
    }

    public void Cancel()
    {
      btnCancel_Click(null, null);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      Speak.Wizard(29000, 1);
      if (Utils.MB.OKCancel(Speak.GetString("r29001"), 
        GD.GetString("91016"), FormMessage.MessageType.Info))
      {
        Speak.Wizard(93000, 1);
        GD.is_edit = false;
        GD.current_filter.Setting.OnOK();
        GD.current_filter.Setting.ReadData();
        RefreshFilters();
        DefaultSettings(true);
        GD.PreviewCurrent();
        UpdateButtons();
      }
      Settings.save_ps();
    }
    public void RefreshFilters()
    {
      lvFilters.Items.Clear();
      VIEW idx = GD.CurrentView;
      FilterList fl = GD.SettingsForView(idx);
      if (fl == null)
        return;
      ckEnable.Checked = GD.SettingsForView(idx).Enable;
      foreach (IFilter fil in fl)
      {
        ListViewItem lvi = new ListViewItem();
        lvi.UseItemStyleForSubItems = false;
        lvi.Text = (lvFilters.Items.Count + 1).ToString();
        lvi.SubItems.Add(fil.TypeName, lvTools.ForeColor, lvTools.BackColor,
          new Font(lvTools.Font, FontStyle.Bold));
        lvi.SubItems.Add(fil.TypeName, lvTools.ForeColor, lvTools.BackColor, null);
        lvi.Checked = fil.Enabled;
        lvi.Tag = fil;
        lvFilters.Items.Add(lvi);
        /*lvi.UseItemStyleForSubItems = false;*/
        //lvi.SubItems[1].Font = new Font(lvTools.Font.FontFamily, 12, FontStyle.Bold);
        lvi.SubItems[1].Font = new Font(lvTools.Font, FontStyle.Bold);
        lvi.SubItems[1].BackColor = lvFilters.BackColor;
        lvi.SubItems[1].ForeColor = lvFilters.ForeColor;
        lvi.SubItems[2].ForeColor = Color.OliveDrab;
//         if (!ckEnable.Checked)
//           lvi.BackColor = Color.Gray;
//         else
//           lvi.BackColor = lvFilters.BackColor;
      }
      lb_header_filters.rearrange(lvFilters);
      lb_header_filters.Refresh();
//       switch (idx)
//       {
//         case ViewIndex.VIEW1:
//           lbView.Text = GD.GetString("17001")/*"第一影像"*/;
//           break;
//         case ViewIndex.VIEW2:
//           lbView.Text = GD.GetString("17002")/*"第二影像"*/;
//           break;
//         case ViewIndex.VIEW3:
//           lbView.Text = GD.GetString("17003")/*"第三影像"*/;
//           break;
//         case ViewIndex.VIEW4:
//           lbView.Text = GD.GetString("17025")/*"第四影像"*/;
//           break;
//         case ViewIndex.VIEW5:
//           lbView.Text = GD.GetString("17026")/*"第五影像"*/;
//           break;
//         case ViewIndex.VIEW6:
//           lbView.Text = GD.GetString("17027")/*"第六影像"*/;
//           break;
//         default:
//           break;
//       }
    }

    public void DefaultSettings(bool isdefaultsetting)
    {
      plItemSettings.SendToBack();
      plItemSettings.Controls.Clear();
      plMachineParam.Visible = isdefaultsetting;
      plNoSettings.Visible = isdefaultsetting;
      plCalc.Visible = isdefaultsetting;
      if (GD.current_filter != null)
        GD.current_filter.HideTool();
    }

    private void btnProfileColor_Click(object sender, EventArgs e)
    {
//       ColorDialog cd = new ColorDialog();
//       cd.Color = Settings.ProfileColor;
//       cd.SolidColorOnly = true;
//       if (cd.ShowDialog() != DialogResult.OK)
//       {
//         return;
//       }
//       Settings.ProfileColor = cd.Color;
//       LoadSettings();
    }

    private void btnLoadSettings_Click(object sender, EventArgs e)
    {
      if (!Utils.MB.OKCancelW(GD.GetString("17019")/*"更换配置必须重新运行系统后才能生效，确认要更改吗？"*/, GD.GetString("17020")/*"注意"*/))
        return;

      string old = Settings.ProfileName;
      OpenFileDialog fd = new OpenFileDialog();
      fd.InitialDirectory = Settings.AppPath();
      fd.Title = GD.GetString("17021")/*"读取历史设置"*/;
      fd.Filter = GD.GetString("17022")/*"配置文件 (*.cfg)|*.cfg|所有文件 (*.*)|*.*"*/;
      if (fd.ShowDialog() != DialogResult.OK)
        return;
      string file = Path.GetFileNameWithoutExtension(fd.FileName);
      if (file == null || file.Length == 0)
      {
        WarningLoadFailed();
        return;
      }
      Settings.ProfileName = file;
      Settings.save();
      //ExitSystem();
    }

    public event EventHandler OnExitSystem; 
    void ExitSystem()
    {
      if (OnExitSystem != null)
      {
        OnExitSystem(null, null);
      }
    }

    private void WarningLoadFailed()
    {
      Utils.MB.Warning(GD.GetString("17018")/*"读取配置文件失败！"*/);
    }

    bool save_count()
    {
      int count = 0;
      if (!Settings.PS.EnablePack)
        return true;
      int.TryParse(tb_pack.Text, out count);
      if (count > 32000 || count < 1)
      {
        Utils.MB.WarningDlg(GD.GetString("17028"));
        return false;
      }
      if (!Hardware.MachineController.save_count(count))
      {
        Utils.MB.Error(GD.GetString("17029"));
        return false;
      }
      Settings.PS.PackCount = count;
      return true;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (!save_count())
        return;
//       int.TryParse(tbFullCount.Text, out Settings.PS.BoxCapacity);
      int.TryParse(tb_consecutive_bad_parts.Text, out Settings.PS.ConsecutiveBadParts);
      int.TryParse(tb_clear_plate.Text, out Settings.PS.ClearPlate);
      int.TryParse(tb_check_feed.Text, out Settings.PS.NoFeedTime);
      while(!Settings.save_ps())
      {
        if (!Utils.MB.OKCancelW(GD.GetString("91035"), GD.GetString("91036")))
        {
          break;
        }
      }
      Utils.MB.OKI(GD.GetString("17012")/*"保存成功！"*/);
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      FileDialog fd = new OpenFileDialog();
      fd.Filter = GD.GetString("17024")/*"图像文件 (*.bmp *.jpg *.png)|*.bmp;*.jpg;*.png|所有文件 (*.*)|*.*"*/;
      fd.Title = GD.GetString("17023")/*"打开图像文件"*/;
      if (fd.ShowDialog() != DialogResult.OK)
        return;
      bool result = false;
      string msg = "";
      VIEW view = GD.CurrentView;
      result = GD.ReadImage(view, fd.FileName);

      if (!result)
        Utils.MB.Warning(GD.GetString("17008")/*"打开文件失败 "*/ + msg);
      else
      {
        GD.CopyImage(view);
        GD.RefreshImage(view);
        GD.SettingsForView(view).ImageFile = fd.FileName;
      }
    }
    public string shotsnap_orig = "";
    public string shotsnap_screen = "";
    private void btnSnap_Click(object sender, EventArgs e)
    {
      if (!ckContScrshot.Checked)
      {
        VIEW view = GD.CurrentView;
        string[] files = GD.Screenshot(view);
        shotsnap_orig = files[0].ToString();
        shotsnap_screen = files[1].ToString();
//         int snap1Index = shotsnap_orig.LastIndexOf("\\");
//         string snap1Filename = shotsnap_orig.Substring(snap1Index + 1);
//         Settings.PS.extension_orig = snap1Filename.Substring(snap1Filename.Length - 9, 9);
//         int snap2Index = shotsnap_screen.LastIndexOf("\\");
//         string snap2Filename = shotsnap_screen.Substring(snap2Index + 1);
//         Settings.PS.extension_screen = snap2Filename.Substring(snap2Filename.Length - 11, 11);
        Settings.PS.ScreenshotName = files[2].ToString();
        FormScrshot frm = new FormScrshot(Settings.PS.ScreenshotName);
        if (DialogResult.OK == frm.ShowDialog())
        {
        }
      }
      else
      {
        VIEW view = GD.CurrentView;
        string[] files = GD.Screenshot(view);
        shotsnap_orig = files[0].ToString();
        shotsnap_screen = files[1].ToString();
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      AddTool();
      RefreshTools();
      if (OnRefresh != null)
      {
        OnRefresh(null, null);
      }
    }

    private void AddTool()
    {
      for (int i = 0; i < lvTools.Items.Count; i++)
      {
        if (lvTools.Items[i].Selected)
        {
          ListViewItem fil = lvTools.Items[i];
          switch (fil.Text)
          {
            case "夹距量测":
            case "Distance":
              {
                AddTool(new CalcDistance());
              }
              break;

            case "直径":
            case "Diameter":
              {
                AddTool(new CalcDiameter());
              }
              break;

            case "角度":
            case "Angle":
              {
                AddTool(new CalcAngle());
              }
              break;

            case "牙部":
            case "Thread":
              {
                AddTool(new CalcTeeth());
              }
              break;

            case "中心开裂":
            case "CenterCrack":
              {
                AddTool(new CalcCracks());
              }
              break;

            case "六边形":
            case "Hexagon":
              {
                AddTool(new CalcHexagon());
              }
              break;

            case "二值化":
            case "Binarize":
              {
                AddTool(new ImageBinarize());
              }
              break;
// 
//             case "复原影像":
//             case "Restore": AddTool(new ImageRestore());
//               break;
// 
//             case "去除杂斑":
//             case "Remove Noise": AddTool(new ImageRemoveNoise());
//               break;
// 
            case "头厚测量":
            case "Head":
              {
                AddTool(new CalcHead());
              }
              break;
// 
//             case "头外径":
//             case "Outer Diameter":
//               {
//                 AddTool(new FindShape());
//                 if (Speak.speak)
//                   Speak.Read("你需要选择头部的颜色和调整选择区域的大小 使头外径大于内圆 小于外圆 然后选择需要检测的项目 如果您需要检测最大外径和最小外径 请先进行校正");
//               }
//               break;
// 
//             case "轮裂":
//             case "Ring Crack":
//               {
//                 AddTool(new CalcCracks());
//                 if (Speak.speak)
//                   Speak.Read("你需要选择搜索方向和调整区域的大小 然后进行校正");
//               }
//               break;

            case "缺料":
            case "LackOfMaterial":
              {
                AddTool(new CalcStarving());
              }
              break;

            case "四边形":
            case "Square":
              {
                AddTool(new CalcSquare());
              }
              break;

            case "焊点":
            case "Weld":
              {
                AddTool(new CalcNode());
              }
              break;

            case "牙伤检测":
            case "ThreadDamage":
              {
                AddTool(new CalcThreadDameged());
              }
              break;
            case "牙数":
            case "ThreadCount":
              {
                AddTool(new CalcThreads());
              }
              break;
            case "牙部定位":
            case "ThreadLocating":
              {
                AddTool(new CalcThreadLocating());
              }
              break;
            case "弹垫检测":
            case "Cushion":
              {
                AddTool(new CalcCushion());
              }
              break;
            case "区域填充":
            case "FillingArea":
              {
                AddTool(new CalcFillArea());
              }
              break;
            case "印记检测":
            case "Marking":
              {
                AddTool(new CalcMarking());
              }
              break;
            case "面积":
            case "Area":
              {
                AddTool(new CalcArea());
              }
              break;
            case "轮裂":
            case "WheelCrack":
              {
                AddTool(new CalcWheelCrack());
              }
              break;
            default: break;
          }
          break;
        }
      }
    }

    private void AddTool(IFilter fil)
    {
      VIEW view = GD.CurrentView;
      if (view == VIEW.NONE)
        return;
      fil.Initialize(view);
      GD.SettingsForView(view).Add(fil);
      RefreshFilters();
      GD.PreviewView(view);
      //EditFilter(fil);
    }

    private void EditFilter(IFilter fil)
    {
      foreach (ListViewItem lvi in lvFilters.Items)
      {
        if (lvi.Tag == fil)
        {
          lvi.Focused = true;
          EditFilter(lvFilters);
          break;
        }
      }
    }

    private void FormToolKit_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void LoadSettings()
    {
      ReadParam();
      RefreshFilters();
      RefreshProfile();
      tmChecker.Interval = 100;
      tmChecker.Tick -= tmChecker_Tick;
      tmChecker.Tick += new EventHandler(tmChecker_Tick);
      tmChecker.Start();
      
    }

    void tmChecker_Tick(object sender, EventArgs e)
    {
      LocateBtnAdd();
      LocateBtnModifyAndDelete();
    }

    private void LocateBtnAdd()
    {
      LocateBtn(lvTools, btnAdd, btnAdd.Width, 0);
    }

    private void LocateBtnModifyAndDelete()
    {
      LocateBtn(lvFilters, btnDelete, btnDelete.Width, 2);
      LocateBtn(lvFilters, btnModify, 2 * btnDelete.Width - 2, 2);
    }

    private void LocateBtn(ListView lv, KeyButton btn, int xoffset, int yoffset)
    {
      bool visible = false;
      for (int i = 0; i < lv.Items.Count; i++)
      {
        ListViewItem lvi = lv.Items[i];
        if (lvi.Selected)
        {
          int l, t;
          l = lv.ClientRectangle.Right - xoffset;
          t = lvi.Position.Y + yoffset;

          System.Drawing.Point pt = new System.Drawing.Point(l, t);
          if (!btn.Location.Equals(pt))
            btn.Location = pt;
          if ((t >= lv.Top + 20) &&
            (t + btn.Height) <= (lv.Bottom) )
            visible = true;
          break;
        }
      }
      if (btn.Visible != visible)
        btn.Visible = visible;
    }

    private void ReadParam()
    {
      tb_pack.Text = Settings.PS.PackCount.ToString();
      tb_consecutive_bad_parts.Text = Settings.PS.ConsecutiveBadParts.ToString();
      tb_clear_plate.Text = Settings.PS.ClearPlate.ToString();
      tb_check_feed.Text = Settings.PS.NoFeedTime.ToString();
      cb_consecutive_bad_parts.Checked = Settings.PS.EnableConsecutiveBadParts;
      cb_clear_plate.Checked = Settings.PS.EnableClearPlate;
      cb_check_feed.Checked = Settings.PS.EnableCheckFeed;
      cb_pack.Checked = Settings.PS.EnablePack;
      tb_pack.Enabled = Settings.PS.EnablePack;
      tb_clear_plate.Enabled = Settings.PS.EnableClearPlate;
      tb_check_feed.Enabled = Settings.PS.EnableCheckFeed;
      tb_consecutive_bad_parts.Enabled = Settings.PS.EnableConsecutiveBadParts;
      tbSnapPath.Text = Settings.PS.ScreenshotName;
      //tbProfile.Text = Settings.ProfileName;
      //tbProfile.ForeColor = Settings.ProfileColor;
      //lbView.ForeColor = Settings.ProfileColor;
    }

    private void btnSnapPath_Click(object sender, EventArgs e)
    {
      if (tbSnapPath.Text != Settings.PS.ScreenshotName)
      {
        if (Utils.MB.OKCancelSnap())
        {
          try
          {
            string shotsnap1 = string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath, Settings.PS.ScreenshotName,
              GD.extension_orig);
            System.IO.FileInfo f = new FileInfo(shotsnap1);
            f.MoveTo(string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath,
              tbSnapPath.Text, GD.extension_orig));
            string shotsnap2 = string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath,
              Settings.PS.ScreenshotName, GD.extension_screen);
            System.IO.FileInfo f1 = new FileInfo(shotsnap2);
            f1.MoveTo(string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath,
              tbSnapPath.Text, GD.extension_screen));
            Settings.PS.ScreenshotName = tbSnapPath.Text;
          }
          catch (Exception theException)
          {
            String errorMessage;
            errorMessage = "Error: ";
            errorMessage = String.Concat(errorMessage, theException.Message);
            errorMessage = String.Concat(errorMessage, " Line: ");
            errorMessage = String.Concat(errorMessage, theException.Source);
            MessageBox.Show(errorMessage, "Error");
            tbSnapPath.Text = Settings.PS.ScreenshotName;
            return;
          }
        }
      }
//       FolderBrowserDialog dlg = new FolderBrowserDialog();
//       dlg.RootFolder = Environment.SpecialFolder.MyComputer;
//       dlg.SelectedPath = Settings.PS.ScreenshotPath;
//       dlg.Description = GD.GetString("17016")/*"请选择抓图保存路径："*/;
//       if (dlg.ShowDialog() == DialogResult.OK)
//       {
//         tbSnapPath.Text = dlg.SelectedPath;
//         Settings.PS.ScreenshotPath = dlg.SelectedPath;
//       }
    }

    private void btnBrowseOpen_Click(object sender, EventArgs e)
    {
      DirectoryInfo di = new DirectoryInfo(Settings.PS.ScreenshotPath);
      if (!di.Exists)
        di.Create();
      System.Diagnostics.Process.Start("Explorer.exe", Settings.PS.ScreenshotPath);
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      VIEW view = GD.CurrentView;
      if (view == VIEW.NONE)
        return;
      GD.PreviewView(view);
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      ListViewUp();
      GD.PreviewCurrent();
    }

    private void Only1FilterPlease()
    {
      MB.Warning(GD.GetString("17009")/*"请选择1个滤波器"*/);
    }

    private IFilter GetFilter(ListViewItem i)
    {
      if (i.Tag == null ||
        !(i.Tag is IFilter))
        return null;
      return (IFilter)i.Tag;
    }

    public void ListViewUp()
    {
      if (lvFilters.SelectedItems.Count != 1)
      {
        Only1FilterPlease();
        return;
      }
      IFilter fil = GetFilter(lvFilters.SelectedItems[0]);
      if (fil != null)
      {
        FilterList fl = GD.SettingsForView(GD.CurrentView);
        fl.MoveUp(fil);
        RefreshFilters();
      }
      for (int i = 0; i < lvFilters.Items.Count; i++)
      {
        if ((IFilter)lvFilters.Items[i].Tag == fil)
        {
          lvFilters.Items[i].Selected = true;
          lvFilters.Items[i].Focused = true;
        }
      }
    }

    public void ListViewDown()
    {
      if (lvFilters.SelectedItems.Count != 1)
      {
        Only1FilterPlease();
        return;
      }
      IFilter fil = GetFilter(lvFilters.SelectedItems[0]);
      if (fil != null)
      {
        FilterList fl = GD.SettingsForView(GD.CurrentView);
        fl.MoveDown(fil);
        RefreshFilters();
      }
      for (int i = 0; i < lvFilters.Items.Count; i++)
      {
        if ((IFilter)lvFilters.Items[i].Tag == fil)
        {
          lvFilters.Items[i].Selected = true;
          lvFilters.Items[i].Focused = true;
        }
      }
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      ListViewDown();
      GD.PreviewCurrent();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      if (Utils.MB.OKCancelQ(GD.GetString("17004")/*"该操作无法恢复，确定要删除该检测项目吗？"*/))
        ListViewRemoves();
      DefaultSettings(true);
      GD.PreviewCurrent();
    }

    public void ListViewRemoves()
    {
      int count = lvFilters.Items.Count;
      for (int i = count - 1; i >= 0; i--)
      {
        if (lvFilters.Items[i].Selected)
        {
          IFilter fil = GetFilter(lvFilters.Items[i]);
          if (fil != null)
            GD.SettingsForView(GD.CurrentView).Removes(fil);
        }
      }
      RefreshFilters();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      if (Utils.MB.OKCancelQ(GD.GetString("17015")/*"该操作无法恢复，确定要清除全部检测项目吗？"*/))
        ListViewClear(lvFilters);
      GD.PreviewCurrent();
    }

    public void ListViewClear(ListView lv)
    {
      int count = lv.Items.Count;
      for (int i = count - 1; i >= 0; i--)
      {
        if (lv.Items[i].Checked)
        {
          IFilter fil = GetFilter(lv.Items[i]);

          if (fil != null)
            GD.SettingsForView(GD.CurrentView).Removes(fil);
        }
      }
      RefreshFilters();
    }

    private void btnModify_Click(object sender, EventArgs e)
    {
      EditFilter(lvFilters);
      Refresh();
    }
    private void EditBtnDefault()
    {
      btnBack.Visible = true;
      btnDefault.Visible = true;
      btnOK.Visible = true;
      btnCancel.Visible = true;
      btnBack.BringToFront();
      btnOK.BringToFront();
      btnCancel.BringToFront();
      btnDefault.BringToFront();
    }
    public void EditFilter(ListView lv)
    {
      if (lv.FocusedItem == null || lv.FocusedItem.Tag == null)
        return;
      GD.is_edit = true;
      GD.current_filter = (IFilter)lv.FocusedItem.Tag;
      if (!FilterSelected(GD.current_filter))
        return;
      GD.PreviewCurrent();
      GD.current_filter.ShowTool();
      EditBtnCorrection();
      EditBtnDefault();
      plItemSettings.BringToFront();
      plItemSettings.Location = new System.Drawing.Point(0, 0);
      plItemSettings.Size = new System.Drawing.Size(327, 706);
      UpdateButtons();
    }

    public event EventHandler OnUpdateButtons;
    void UpdateButtons()
    {
      if (OnUpdateButtons != null)
      {
        OnUpdateButtons(null, null);
      }
    }

    public bool FilterSelected(IFilter fil)
    {
      if (fil == null)
        return false;
      UserControl uc = fil.Setup();
      fil.ShowSpeak();
      if (uc == null)
      {
        Utils.MB.OK(GD.GetString("17007")/*"该检测项目无需设置"*/);
        return false;
      }
      DefaultSettings(false);
      plItemSettings.Controls.Add(uc);
      uc.Controls.Add(btnCorrection);
      uc.Controls.Add(lbCorrection);
      uc.Controls.Add(btnOK);
      uc.Controls.Add(btnDefault);
      uc.Controls.Add(btnCancel);
      return true;
    }

    public void EditBtnCorrection()
    {
      if (GD.current_filter == null)
        return;
      bool display = (GD.current_filter.IsCorrection && GD.is_edit);
      btnCorrection.Visible = display;
     // /*btnAfreshCorrection*/.Visible = display;
      lbCorrection.Visible = display;
      if (display)
      {
        //btnCorrection.Enabled = (!GD.IsLive && !ChangeFilter.Corrected);
        //btnAfreshCorrection.Enabled = (!GD.IsLive && ChangeFilter.Corrected);
        btnCorrection.BringToFront();
        //btnAfreshCorrection.BringToFront();
        lbCorrection.BringToFront();
        lbCorrection.ForeColor = GD.current_filter.Corrected ? Color.Green : Color.Red;
        lbCorrection.Text = GD.current_filter.Corrected ? GD.GetString("17010")/*"校正已完成"*/ : GD.GetString("17011")/*"未进行校正"*/;
      }
    }

    private void tbProfile_Enter(object sender, EventArgs e)
    {
      tbProfile.BorderStyle = BorderStyle.Fixed3D;
      tbProfile.BackColor = SystemColors.Window;
    }

    private void tbProfile_Leave(object sender, EventArgs e)
    {
      tbProfile.BorderStyle = BorderStyle.None;
      tbProfile.BackColor = Color.FromArgb(245, 248, 253);
    }

    private void tbProfile_TextChanged(object sender, EventArgs e)
    {
      if (tbProfile.Text.Length == 0)
      {
        Utils.MB.Warning(GD.GetString("17017")/*"配置文件名未指定，将恢复原有配置"*/);
        tbProfile.Text = Settings.ProfileName;
        return;
      }
      Settings.ProfileName = tbProfile.Text;
    }

    private bool cont_scrshot = false;
    public bool IsContinuousScreenshot
    {
      get { return cont_scrshot; }
    }

    private void btnCorrection_Click(object sender, EventArgs e)
    {
      if (GD.current_filter.Setting.OnCorrection() == false)
      {
        Utils.MB.Correction(GD.GetString("17013")/*"校正失败，请检查校正零件！"*/, false);
        lbCorrection.Visible = true;
        lbCorrection.Text = GD.GetString("17014")/*"校正失败"*/;
        lbCorrection.ForeColor = Color.Red;
        return;
      }
      else
      {
        GD.current_filter.Corrected = true;
        lbCorrection.Visible = true;
        lbCorrection.Text = GD.GetString("17010")/*"校正已完成"*/;
        lbCorrection.ForeColor = Color.Green;
        //btnCorrection.Enabled = false;
        //btnAfreshCorrection.Enabled = true;
        btnCancel.Enabled = true;
        btnOK.Enabled = true;
      }
    }

    private void lvTools_SelectedIndexChanged(object sender, EventArgs e)
    {
      LocateBtnAdd();
    }

    private void lvFilters_SelectedIndexChanged(object sender, EventArgs e)
    {
      LocateBtnModifyAndDelete();
    }

    private void ckContScrshot_CheckedChanged(object sender, EventArgs e)
    {
      cont_scrshot = ckContScrshot.Checked;
    }

    private void ckEnable_CheckedChanged(object sender, EventArgs e)
    {
      bool isCheck = ckEnable.Checked;
      btnDown.Enabled = isCheck;
      btnUp.Enabled = isCheck;
      btnRemove.Enabled = isCheck;
      btnClear.Enabled = isCheck;
      lvFilters.Enabled = isCheck;
      VIEW idx = GD.CurrentView;
      GD.SettingsForView(idx).Enable = isCheck;
      GD.PreviewCurrent();
      //RefreshFilters();
      if (!ckEnable.Checked)
        IA.IA_ViewGray(idx);
    }

    private void lvFilters_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      VIEW view = GD.CurrentView;
      ListViewItem lvi = e.Item;
      if (lvi.Tag == null ||
        !(lvi.Tag is IFilter))
        return;
      IFilter fil = (IFilter)lvi.Tag;
      fil.Enabled = e.Item.Checked;
      //GD.PreviewCurrent();
    }

    private void btnShowSetup_Click(object sender, EventArgs e)
    {
      Hardware.FrameGrabber.Setup(GD.CurrentView,
        this.Handle, true);
    }

    private void cbFullCount_CheckedChanged(object sender, EventArgs e)
    {
      Settings.PS.EnablePack = cb_pack.Checked;
      tb_pack.Enabled = Settings.PS.EnablePack;
      ImagingSortCharlie.Hardware.MachineController.clear_current();
      ImagingSortCharlie.Hardware.MachineController.UsingCount(Settings.PS.EnablePack);
    }

    private void btnDefault_Click(object sender, EventArgs e)
    {
      GD.current_filter.Setting.Default();
    }

    private void lvFilters_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == (char)Keys.Delete)
      {
        ListViewRemoves();
        DefaultSettings(true);
        VIEW idx = GD.CurrentView;
      }
      if (e.KeyCode == Keys.Up && e.Alt)
      {
        ListViewUp();
      }
      if (e.KeyCode == Keys.Down && e.Alt)
      {
        ListViewDown();
      }
    }

    public void UpdateButtons(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(UpdateButtons));
        return;
      }
      btnCorrection.Enabled = !GD.IsLive;
    }
    public void RefreshProfile()
    {
      string dir = GD.AppPath();
      try
      {
        DirectoryInfo di = new DirectoryInfo(dir);
        FileInfo[] f = di.GetFiles("*.cfg");
        if (f == null)
        {
          char[] c1 = { '.' };
          cbProfile.DataSource = Settings.ProfileName.Split(c1)[0];
        }
        List<string> file_name = new List<string>();
        char[] c = { '.' };
        foreach (FileInfo n in f)
        {
          file_name.Add(n.Name.Split(c)[0]);
        }
        cbProfile.DataSource = file_name;
        cbProfile.DisplayMember = "NAME";
        cbProfile.SelectedItem = Settings.ProfileName;
      }
      catch
      {
      }
    }
    private bool enable = false;
    private void tbCreate_Click(object sender, EventArgs e)
    {
      FormProfile fp = new FormProfile(cbProfile.DataSource,
        cbProfile.SelectedValue as string);
      if (DialogResult.OK != fp.ShowDialog())
      {
        Message.appear(GD.GetString("91026"), Message.MessageType.Info);
        return;
      }
      enable = fp.ApplyNow;
      Settings.save_ps();
      if (fp.CopyFrom)
      {
        string from = fp.SelectedConfig;
        string to = Path.Combine(Path.GetDirectoryName(from), fp.ConfigName);
        to = Path.ChangeExtension(to, ".cfg");
        string to_back = Path.ChangeExtension(to, ".bk");
        try
        {
          File.Copy(from, to, true);
          File.Copy(from, to_back, true);
        }
        catch
        {
          Message.appear("File copy error", Message.MessageType.Error);
        }
      }
      else
      {
        Settings.PS = new PortableSettings();
        Settings.ProfileName = Path.ChangeExtension(fp.ConfigName, ".cfg");
        Settings.save_ps();
      }
      if (enable)
      {
        Settings.ProfileName = fp.ConfigName;
        Settings.save();
      }
      else
        Settings.ProfileName = cbProfile.SelectedItem.ToString();
      Message.appear(Settings.ProfileName, Message.MessageType.OK);
      RefreshProfile();
      Settings.load_ps();
      ReadParam();
      RefreshFilters();
      GD.InitializeAllFilters();  
    }

    private void cbProfile_SelectionChangeCommitted(object sender, EventArgs e)
    {
      if (cbProfile.SelectedValue == null)
        return;
      string before = Settings.ProfileName;
      string after = cbProfile.SelectedValue.ToString();
      if (before == after)
        return;
      string prompt = string.Format(GD.GetString("91028"),before, after);
      prompt = prompt.Replace(@"\n", Environment.NewLine);
      if (Utils.MB.OKCancel(prompt,GD.GetString("91016"), 
        FormMessage.MessageType.Info))
      {
        Settings.save_ps();
        string fileSnap = GD.AppPath();
        string file = Path.Combine(fileSnap, cbProfile.SelectedValue.ToString());
        if (file == null || file.Length == 0)
        {
          WarningLoadFailed();
          return;
        }
        ReadParam();
        Settings.ProfileName = cbProfile.SelectedValue.ToString();
        Message.appear(GD.GetString("91033"), Message.MessageType.OK);
        Settings.save();
        Settings.load_ps();
        ReadParam();
        GD.InitializeAllFilters();
        RefreshFilters();
      }
      else
      {
        RefreshProfile();
      }
    }

    private void cb_out_blow_count_CheckedChanged(object sender, EventArgs e)
    {
      Settings.PS.EnableConsecutiveBadParts = cb_consecutive_bad_parts.Checked;
      tb_consecutive_bad_parts.Enabled = Settings.PS.EnableConsecutiveBadParts;
    }

    private void checkBox_clear_plate_CheckedChanged(object sender, EventArgs e)
    {
      Settings.PS.EnableClearPlate = cb_clear_plate.Checked;
      tb_clear_plate.Enabled = Settings.PS.EnableClearPlate;
    }

    private void checkBox_no_feed_CheckedChanged(object sender, EventArgs e)
    {
      Settings.PS.EnableCheckFeed = cb_check_feed.Checked;
      tb_check_feed.Enabled = Settings.PS.EnableCheckFeed;
    }

  }
}
