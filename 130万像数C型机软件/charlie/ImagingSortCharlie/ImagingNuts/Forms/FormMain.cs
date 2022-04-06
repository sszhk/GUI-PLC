//#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Settings;
using System.Globalization;
using ImagingSortCharlie.Hardware;
using System.Diagnostics;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Utils;
using System.Collections;
using System.IO;
using ImagingSortCharlie.Data;

namespace ImagingSortCharlie.Forms
{
  public partial class FormMain : Form
  {
    public bool IsSetting { get { return btnSetup.Checked; } }
    bool canSnap = false;

    public Panel[] plViews;
    KeyButton[] btnViews;
    Panel currentView = null;
    public bool isSpeak = false;
    public bool isStart = false;
    IntPtr[] viewHandles;
    IntPtr[] niHandles;
    //    Graphics[] niGraphics;

    UCResults ucResults = new UCResults();
    FormToolKit frmToolKit = new FormToolKit();

    public delegate void InvokeDelegate();

    public FormMain()
    {
      InitializeComponent();
      RegisterEventHandlers();
      InitNIComponents();
      InitHardwares();
      InitOthers();
      InitCheckSystem();
    }

    private void init_config()
    {
      pbChinese.Visible = Configure.This.Language;
      pbEnglish.Visible = Configure.This.Language;
      btnSaveData.Visible = Configure.This.DataSheet;
      button_test.Visible = Configure.This.Test;
      panel_wizard.Visible = Configure.This.Wizard;
      for (int i = 0; i < Configure.This.ViewTeam.Count; i++ )
      {
        count_team[(int)Configure.This.ViewTeam[i]]++;
      }
    }
    private void InitNIComponents()
    {
//       FPS = new Label[] { lbFPS1, lbFPS2, lbFPS3, lbFPS4, lbFPS5, lbFPS6, lbFPS7 };
      plViews = new Panel[] { plView1, plView2, plView3, plView4, plView5, plView6, plView7, plView8 };
      btnViews = new KeyButton[] { btnView1, btnView2, btnView3, btnView4, btnView5, btnView6, btnView7, btnView8 };
      plView2.Visible = plView3.Visible = plView4.Visible = plView5.Visible = plView6.Visible = plView7.Visible = plView8.Visible = false;
      plView2.Location = plView3.Location = plView4.Location = plView5.Location = plView6.Location = plView7.Location = plView8.Location = plView1.Location;
      plView2.Size = plView3.Size = plView4.Size = plView5.Size = plView6.Size = plView7.Size = plView8.Size = plView1.Size;
      viewHandles = new IntPtr[] { plView1.Handle, plView2.Handle, plView3.Handle, plView4.Handle, plView5.Handle, plView6.Handle, plView7.Handle, plView8.Handle };
      niHandles = new IntPtr[viewHandles.Length];
      //niGraphics = new Graphics[viewHandles.Length];
      for (int i = Configure.This.CameraCount; i < (int)VIEW.VIEW_COUNT; i++ )
      {
#if !DEBUG
        btnViews[i].Visible = false;
#endif
      }
      MT_InitNI(null);
    }

    void InitOthers()
    {
      init_config();
      pbChinese.Image = GD.GetImg("Chinese");
      pbEnglish.Image = GD.GetImg("English");
      Speak.OnWizard -= this.OnWizard;
      Speak.OnWizard += this.OnWizard;
      Speak.OnUpdateWizardButtons -= this.RefreshWizardButtons;
      Speak.OnUpdateWizardButtons += this.RefreshWizardButtons;
      frmToolKit.OnSetup -= this.Setup;
      frmToolKit.OnSetup += this.Setup;
      frmToolKit.OnExitSystem -= this.OnExitSystem;
      frmToolKit.OnExitSystem += this.OnExitSystem;
      frmToolKit.OnUpdateButtons -= this.OnUpdateButtons;
      frmToolKit.OnUpdateButtons += this.OnUpdateButtons;
      frmToolKit.OnRefresh -= this.OnRefresh;
      frmToolKit.OnRefresh += this.OnRefresh;
      Speak.Init();
      ucResults.Init();
      frmToolKit.Init();
      plSettings.Controls.Add(ucResults);
      plSettings.Controls.Add(frmToolKit);
      frmToolKit.Visible = false;
      SwitchSetting(ucResults);
      OnTimerNow(null, null);
      StartTimerNow();
      restoreSettings();
      FaultyCount.ClearDataTable();
      lbWizard.Visible = Speak.speak;
      Speak.Wizard();
    }

    void OnRefresh(object sender, EventArgs e)
    {
      this.Refresh();
    }

    delegate void VoidOnWizard(string strcounter);
    public void OnWizard(string s)
    {
      if (lbWizard.InvokeRequired)
      {
        lbWizard.BeginInvoke(new VoidOnWizard(OnWizard), s);
      }
      else
      {
        lbWizard.BringToFront();
        lbWizard.Text = s;
      }
    }

    private void show_result()
    {
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        FilterList fl = GD.SettingsForView((VIEW)i);
        fl.AfterApply -= ucResults.OnDisplayResult;
        fl.AfterApply += ucResults.OnDisplayResult;
      }
    }
    private void MT_InitNI(object x)
    {
      IA.Init();
#if DEBUG
      Configure.This.CameraCount = (int)VIEW.VIEW_COUNT;
#endif
      for (VIEW i = VIEW.VIEW1; i < (VIEW)Configure.This.CameraCount; i++)
      {
#if HD_VERSION
        GD.SetZoom(i, -1);
#endif
        niHandles[(int)i] = IA.IA_SetupWindow(viewHandles[(int)i], i);
        //niGraphics[(int)i] = Graphics.FromHwnd(niHandles[(int)i]);
#if HD_VERSION
        string filename = string.Format("resources\\{0}.png", GD.GetImgStr("hd_background"))/*"resources\\hd_background.png"*/;
#else
        string filename = string.Format("resources\\{0}.png", GD.GetImgStr("background"))/*"resources\\background.png"*/;
#endif    
        filename = Path.GetFullPath(filename);
        FilterList fl = GD.SettingsForView(i);
#if DEBUG
          if (fl.ImageFile != null)
          {
            FileInfo fi = new FileInfo(fl.ImageFile);
            if (fi.Exists)
              filename = fl.ImageFile;
          }
#endif
        fl.AfterApply -= ucResults.OnDisplayResult;
        fl.AfterApply += ucResults.OnDisplayResult;

        FileInfo fi2 = new FileInfo(filename);
        if (fi2.Exists)
        {
          GD.ReadImage(i, filename);
          GD.SettingsForView(i).InitializeFilters(i);
          GD.CopyImage(i);
          GD.RefreshImage(i);
        }
      }
    }

    private void restoreSettings()
    {
      ReadInfo();
      ViewChanged(Settings.PS.LastActiveView);
    }

    void ReadInfo()
    {
      tbBatch.Text = Settings.PS.Batch;
      tbHeatNumber.Text = Settings.PS.HeatNumber;
      tbOrders.Text = Settings.PS.Orders;
      tbMaterial.Text = Settings.PS.Material;
      tbOperator.Text = Settings.PS.Operator;
      tbTestingData.Text = Settings.PS.TestingData;
    }

    private void InitHardwares()
    {
      FrameGrabber.OnImage -= this.OnImage;
      FrameGrabber.OnImage += this.OnImage;

#if !DEBUG
      bool mc = MachineController.Init();
      if(!mc)
        btnStartStop.Enabled = false;
      bool ifc = FrameGrabber.Init();
      if (!ifc)
      {
        btnLive.Enabled = false;
        canSnap = false;
        return;
      }
      canSnap = true;
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        if (!FrameGrabber.ViewUsable((VIEW)i))
        {
          string s = "影像" + (i + 1).ToString() + "初始化失败！请检查相机或设置。";
          Utils.MB.Warning(s);
        }
      }
#endif
    }


    //     volatile bool[] is_calculating = new bool []{false, false, false, false, false, false};
    //     private bool on_board_start(int idx_cam)
    //     {
    //       if (is_calculating[idx_cam])
    //       {
    //         // 软件紧急停止！
    //         software_halt();
    //         return false;
    //       }
    //       is_calculating[idx_cam] = true;
    //       return true;
    //     }
    // 
    //     public void software_halt()
    //     {
    //       MachineController.SetEmergencyHalt(MachineController.HaltReason.Software);
    //     }
    private void hardware_halt()
    {
      MachineController.SetEmergencyHalt(MachineController.State.Hardware);
    }

    //     private void on_board_end(int idx_cam)
    //     {
    //       is_calculating[idx_cam] = false;
    //     }

    //     List<string> doTime = new List<string>(100000);
    //     public void SaveTriggerTime()
    //     {
    //       string fileName = "d:\\test1.txt";
    //       FileStream fs = new FileStream(fileName, FileMode.Append);
    //       StreamWriter sw = new StreamWriter(fs);
    //       sw.WriteLine("记录时间：" /*+ Now(0)*/);
    //       foreach (string s in doTime)
    //       {
    //         sw.WriteLine(s);
    //       }
    //       doTime.Clear();
    //       sw.Flush();
    //       sw.Close();
    //       fs.Close();
    //     }

    Utils.TimerCounter time_refresh = new ImagingSortCharlie.Utils.TimerCounter();
    Utils.TimerCounter time_apply = new ImagingSortCharlie.Utils.TimerCounter();
    Utils.TimerCounter time_draw = new ImagingSortCharlie.Utils.TimerCounter();

    void apply_on_live(VIEW view)
    {
      FilterList fl = GD.SettingsForView(view);
      foreach (IFilter op in fl)
      {
        if (op is ImageBinarize && fl.Enable && op.Enabled)
        {
          op.Apply(null);
          break;
        }
      }
    }
    bool is_clear = false;
    int[] count_team = new int[(int)TEAM.TEAMCOUNT] { 0, 0, 0, 0, 0 };
    void apply_on_spinning(info_sender info_sender)
    {
      enLog.enter_function("apply_on_spinning");

      TEAM team = Configure.This.ViewTeam[(int)info_sender.view];
      bool isgood = false;
      if (!info_sender.is_empty)
        isgood = GD.ApplyFilters(info_sender.view, true);
      //计数
      if (info_sender.view == FrameGrabber.first_view)
      {
        Settings.PS.Total++;
        Settings.PS.ClearTotal++;
        if (Settings.PS.EnableClearPlate &&
             (Settings.PS.ClearTotal >= Settings.PS.ClearPlate))
        {
          if (!is_clear)
          {
            is_clear = true;
            clear_plate = true;
            MachineController.SetEmergencyHalt(MachineController.State.ClearPlate);
          }   
        }
        if (!is_clear)
        {
          tm_no_feed.Change(Settings.PS.NoFeedTime * 1000, System.Threading.Timeout.Infinite);
        }
      }

      if (!isgood)
      {
        if (teamState[(int)team, 1] == 1)
        {
          teamState[(int)team, 1] = 0;
          STATION station = Configure.This.ViewStation[(int)info_sender.view];
          MachineController.Failed(station);
          Settings.PS.BadCount[(int)team]++;
        }
        count_continuing_blow[(int)info_sender.view]++;
        if (Settings.PS.EnableConsecutiveBadParts &&
          count_continuing_blow[(int)info_sender.view] == Settings.PS.ConsecutiveBadParts)
        {
          MachineController.OutBlowCount(true);
          MachineController.SetEmergencyHalt(MachineController.State.OutBlowCount);
        }
      }
      else
      {
        count_continuing_blow[(int)info_sender.view] = 0;
      }
     // FrameGrabber.results[info_sender.number] = isgood;
     // FrameGrabber.applied[info_sender.number] = true;
//       enLog.error(info_sender.view.ToString() + ", apply, " + info_sender.number.ToString() + ", " + isgood.ToString());


      teamState[(int)team, 0]++;
      if (teamState[(int)team, 0] == count_team[(int)team])
        teamReset(team);
      ucResults.ValueChanged();
      enLog.exit_function("apply_on_spinning");
    }

    void apply_on_snap(VIEW view)
    {
      GD.ApplyFilters(view, true);
    }

    void locating(VIEW view)
    {
      GD.IsLocating = !GD.IsLocating;
      MachineController.Locating(Configure.This.ViewStation[(int)view], GD.IsLocating);
      FrameGrabber.Locating(view, GD.IsLocating);
      UpdateButtons();
    }

    private void OnImage(object sender, EventArgs e)
    {
      enLog.enter_function("OnImage");

      info_sender info_sender = (info_sender)sender;
      if (!info_sender.is_empty)
        GD.CopyImage(info_sender.view);
      try
      {
        if (GD.IsSpinning)//运行
          apply_on_spinning(info_sender);
        else
        {
          if (!info_sender.is_empty)
          {
            if (GD.IsLocating)//定位
              locating(info_sender.view);
            else
              if (GD.IsLive)//动态
                apply_on_live(info_sender.view);
              else//摄取
                apply_on_snap(info_sender.view);
          }
        }
        if (!info_sender.is_empty)
          GD.RefreshImage(info_sender.view);
//         RefreshFPS(FPS[(int)info_sender.view], FrameGrabber.FPS(info_sender.view).ToString("0.0"));
      }
      finally
      {
        if (!info_sender.is_empty)
          FrameGrabber.on_board_end((int)info_sender.view);
        enLog.exit_function("OnImage");
      }
    }

    System.Threading.Timer tm_no_feed =
      new System.Threading.Timer(no_feed, null,
        System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
    static void no_feed(object state)
    {
      if (Settings.PS.EnableCheckFeed)
      {
        MachineController.no_feed(true);
        MachineController.SetEmergencyHalt(MachineController.State.NoFeedTime);
      }
    }

    private void draw_gdi(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(draw_gdi));
        return;
      }
      //using (Graphics g = Graphics.FromHwnd(niHandles[(int)GD.CurrentView]) )
      if (GD.CurrentView != VIEW.NONE)
      {
        //         Graphics g = niGraphics[(int)GD.CurrentView];
        //         g.DrawRectangle(Pens.Red, 100, 100, 100, 100);
        //         g.DrawArc(Pens.Blue, new Rectangle(200, 200, 300, 300), 0, 360);
        //         g.DrawString("这是一个测试", this.Font, Brushes.Lime, new PointF(10, 10));
      }
    }
//     Label[] FPS = new Label[(int)STATION_INDEX.STATION_COUNT];
//     delegate void VoidRefreshFPS(Label lb, string str);
//     private void RefreshFPS(Label lb, string s)
//     {
//       if (lb.InvokeRequired)
//       {
//         VoidRefreshFPS dele = new VoidRefreshFPS(RefreshFPS);
//         lb.BeginInvoke(dele, lb, s);
//       }
//       else
//         lb.Text = s;
//     }

    private const int SITE_RESET = -1;
    //相机所在组的状态，0位置为此次触发后的目前进组数，1为本组的好坏状况
    int[,] teamState = new int[(int)TEAM.TEAMCOUNT, 2];
    //初始化某组的状态
    void teamReset(TEAM team)
    {
      teamState[(int)team, 0] = 0;
      teamState[(int)team, 1] = 1;
    }
    //连续吹气
    int[] count_continuing_blow = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };

    private void btnClose_Click(object sender, EventArgs e)
    {
#if !DEBUG
      if (Utils.MB.OKCancelQ(GD.GetString("16006")/*"您确定要退出系统吗？"*/))
      {
#endif
        ExitSystem();
#if !DEBUG

      }
#endif
    }

    void ExitSystem()
    {
      //this.Close();
      ExitSystem(false);
    }

    private void btnSetup_Click(object sender, EventArgs e)
    {
      if (IsSetting)
      {
        Message.appear(Speak.GetString("r22001"), Message.MessageType.Setting);
        Speak.Wizard(22000, 1);
      }
      UpdateViews();
      btnSetup.Text = GD.GetString("16002")/*"设置...(&S)"*/;
    }

    public void SwitchSetting(UserControl uc)
    {
      if (uc == frmToolKit)
      {
        if (!frmToolKit.Visible)
        {
          frmToolKit.RefreshFilters();
          frmToolKit.RefreshTools();
          AnimateShowTools();
        }
      }
      else
      {
        if (frmToolKit.Visible)
          AnimateHideTools();
      }
    }

    int showToolsPos = 0;
    System.Windows.Forms.Timer tmShowTools;
    System.Windows.Forms.Timer tmHideTools;

    private void AnimateShowTools()
    {
      btnSetup.Enabled = false;
      Cursor = Cursors.WaitCursor;
      if (tmShowTools != null && tmShowTools.Enabled)
        return;
      if (tmHideTools != null && tmHideTools.Enabled)
        return;
      if (frmToolKit.Visible)
        return;

      tmShowTools = new System.Windows.Forms.Timer();
      tmShowTools.Interval = 1;
      tmShowTools.Tick += new EventHandler(tmShowTools_Tick);
      tmShowTools.Start();
    }

    private void AnimateHideTools()
    {
      btnSetup.Enabled = false;
      Cursor = Cursors.WaitCursor;
      if (tmShowTools != null &&
        tmShowTools.Enabled)
        return;
      if (tmHideTools != null &&
        tmHideTools.Enabled)
        return;
      if (!frmToolKit.Visible)
        return;
      tmHideTools = new System.Windows.Forms.Timer();
      tmHideTools.Interval = 50;
      tmHideTools.Tick += new EventHandler(tmHideTools_Tick);
      tmHideTools.Start();
    }

    void tmShowTools_Tick(object sender, EventArgs e)
    {
      const int STEP = 100;
      System.Drawing.Rectangle rc = frmToolKit.ClientRectangle;
      System.Drawing.Point pt = frmToolKit.Location;
      if (showToolsPos == 0)
      {
        showToolsPos = -rc.Width;
        frmToolKit.Location = new System.Drawing.Point(
          showToolsPos,
          pt.Y);
        frmToolKit.Visible = true;
        frmToolKit.BringToFront();
      }
      else
      {
        if (pt.X + STEP > 0)
        {
          frmToolKit.Location = new System.Drawing.Point(0, 0);
          tmShowTools.Stop();
          btnSetup.Enabled = true;
          Cursor = Cursors.Default;
          showToolsPos = 0;
        }
        else
        {
          frmToolKit.Location = new System.Drawing.Point(showToolsPos, pt.Y);
          showToolsPos += STEP;
        }
      }
    }

    void tmHideTools_Tick(object sender, EventArgs e)
    {
      const int STEP = 80;
      System.Drawing.Rectangle rc = frmToolKit.ClientRectangle;
      System.Drawing.Point pt = frmToolKit.Location;
      {
        if (-pt.X > rc.Width)
        {
          frmToolKit.Visible = false;
          tmHideTools.Stop();
          btnSetup.Enabled = true;
          Cursor = Cursors.Default;
          showToolsPos = 0;
        }
        else
        {
          frmToolKit.Location = new System.Drawing.Point(showToolsPos, pt.Y);
          showToolsPos -= STEP;
        }
      }
    }

    private void btnView1_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW1);
    }

    private void btnView2_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW2);
    }

    private void btnView3_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW3);
    }

    private void btnView4_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW4);
    }

    private void btnView5_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW5);
    }

    private void btnView6_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW6);
    }

    private void btnView7_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW7);
    }

    private void btnView8_Click(object sender, EventArgs e)
    {
      ViewChanged(VIEW.VIEW8);
    }

    void ViewChanged(VIEW view)
    {
      int view_idx = (int)view;
      if (currentView == plViews[view_idx])
        return;
      if (GD.is_edit)
        frmToolKit.Cancel();
      VIEW vi = GD.CurrentView;
      currentView = plViews[view_idx];
      GD.CurrentView = view;
      Settings.PS.LastActiveView = view;
      if (GD.IsLive)
      {
        FrameGrabber.Stop(vi);
        FrameGrabber.Live(view);
      }
      //frmToolKit.RefreshFilters();
      UpdateViews();
    }

    void UpdateViews()
    {
      plOverView.Controls.Add(currentView);
      for (int i = 0; i < (int)VIEW.VIEW_COUNT; i++)
      {
        plViews[i].Visible = (currentView == plViews[i]);
      }
      UpdateButtons();
      SwitchSetting(IsSetting ? (UserControl)frmToolKit : (UserControl)ucResults);
      frmToolKit.DefaultSettings(true);
      frmToolKit.RefreshFilters();
      frmToolKit.RefreshTools();
      ucResults.View();
    }

    void OnUpdateButtons(object sender, EventArgs e)
    {
      UpdateButtons();
    }

    private delegate void VoidUpdateButtons();
    void UpdateButtons()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new VoidUpdateButtons(UpdateButtons));
        return;
      }
      for (int i = 0; i < (int)VIEW.VIEW_COUNT; i++)
      {
        btnViews[i].Checked = (currentView == plViews[i]);
#if !DEBUG
        btnViews[i].Enabled = FrameGrabber.ViewUsable((VIEW)i) && !GD.IsLocating;
#endif
        }

      //btnLanguage.Enabled = !IsSetting && !GD.IsLive && !GD.IsSpinning && !GD.IsLocating;
      pbChinese.Visible = !IsSetting && !GD.IsLive && !GD.IsSpinning && !GD.IsLocating && Configure.This.Language;
      pbEnglish.Visible = !IsSetting && !GD.IsLive && !GD.IsSpinning && !GD.IsLocating && Configure.This.Language;
      btnOneShot.Enabled = !GD.IsSpinning && !GD.IsLive && !GD.IsLocating && canSnap;
      btnSaveData.Visible = Configure.This.DataSheet;
      btnStartStop.Enabled = canSnap && !IsSetting && !GD.IsLive && !GD.IsLocating;
      btnLive.Enabled = canSnap && !GD.IsSpinning && !GD.IsLocating;
      btnLive.Checked = GD.IsLive;
      btnSetup.Enabled = !GD.IsSpinning && !GD.is_edit;
      btnClose.Enabled = !IsSetting && !GD.IsLive && !GD.IsSpinning && !GD.IsLocating;
      btnLocating.Enabled = canSnap && !GD.IsLive && !GD.IsSpinning;
      btnLocating.Checked = btnLocating.Enabled && GD.IsLocating;
      button_test.Visible = !GD.IsSpinning && Configure.This.Test;
      btnSaveData.Enabled = Configure.This.DataSheet && !GD.IsSpinning;
      frmToolKit.UpdateButtons(null, null);
    }

    private void btnZoomIn_Click(object sender, EventArgs e)
    {
      VIEW view = GD.CurrentView;
      int zm = GD.GetZoom(view);
      zm++;
      if (zm > 0)
        IA.IA_ShowScrollbar(view, true);
      GD.SetZoom(view, zm);
      GD.SettingsForView(view).Zoom = GD.GetZoom(view);
    }

    private void btnZoomOut_Click(object sender, EventArgs e)
    {
      VIEW view = GD.CurrentView;
      int zm = GD.GetZoom(view);
      zm--;
      if (zm == 0)
        IA.IA_ShowScrollbar(view, false);
      GD.SetZoom(view, zm);
      GD.SettingsForView(view).Zoom = GD.GetZoom(view);
    }
    DateTime dtBegin;
    DateTime dtLast;
    Timer tmRunTime = new Timer();
    private string run_time = "N/A";
    TimeSpan language_change_time;
    private void tmRunTime_Tick(object sender, EventArgs e)
    {
      dtLast = DateTime.Now;
      TimeSpan ts = dtLast - dtBegin;
      language_change_time = ts;
      lbSpinningTime.Text = string.Format("{0}:  {1}{2}/ {3:00}:{4:00}:{5:00}", GD.GetString("91019"),
        Math.Abs(ts.Days), GD.GetString("91018"), Math.Abs(ts.Hours),
        Math.Abs(ts.Minutes), Math.Abs(ts.Seconds));
    }

    const int UPDATETIME = 5;
    const int MINUTE = 60;
    Timer tmSpeed = new Timer();
    int LastCount = Settings.PS.Total;
    void OnSpeed(object sender, EventArgs e)
    {
      int count = Settings.PS.Total - LastCount;
      LastCount = Settings.PS.Total;
      int speed = count * (MINUTE / UPDATETIME);
      if (speed < 0)
        speed = 0;
      lbSpeed.Text = speed.ToString();
    }

    void start()
    {
      lbLastTime.Text = string.Format("{0}:  {1}{2}/ {3:00}:{4:00}:{5:00}", GD.GetString("91019"),
        Math.Abs(language_change_time.Days), GD.GetString("91018"), Math.Abs(language_change_time.Hours),
        Math.Abs(language_change_time.Minutes), Math.Abs(language_change_time.Seconds)); 
      LastCount = Settings.PS.Total;
      tmSpeed.Tick -= OnSpeed;
      tmSpeed.Tick += OnSpeed;
      tmSpeed.Interval = UPDATETIME * 1000;
      tmSpeed.Start();
      dtBegin = DateTime.Now;
      tmRunTime.Tick -= tmRunTime_Tick;
      tmRunTime.Tick += tmRunTime_Tick;
      tmRunTime.Interval = 500;
      tmRunTime.Start();
      Speak.Wizard(91000, 1);
      for (TEAM team = 0; team < TEAM.TEAMCOUNT; team++)
      {
        teamReset(team);
      }
      btnStartStop.Text = GD.GetString("16003")/*"停止(&O)"*/;
      tmBlinkStopButton.Interval = 500;
      tmBlinkStopButton.Tick -= onTmBlinkStopButton;
      tmBlinkStopButton.Tick += onTmBlinkStopButton;
      tmBlinkStopButton.Start();
      btnStartStop.ForeColor = Color.Red;
      btnStartStop.Font = new Font(btnStartStop.Font, FontStyle.Bold);
      MachineController.Start();
      FrameGrabber.Start();
      tm_no_feed.Change((5 + Settings.PS.NoFeedTime) * 1000, System.Threading.Timeout.Infinite);
      tmGear_ani.Enabled = true;
      tmGear_ani.Interval = 100;
      btnIsetting();
      show_result();
      for (int i = 0; i < (int)VIEW.VIEW_COUNT; i++)
      {
        count_continuing_blow[i] = 0;
      }
      isStart = true;
    }

    void stop()
    {
      tmRunTime.Stop();
      tm_no_feed.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
      run_time = string.Format("{0}:  {1}{2}/ {3:00}:{4:00}:{5:00}", GD.GetString("91019"),
        Math.Abs(language_change_time.Days), GD.GetString("91018"), Math.Abs(language_change_time.Hours),
        Math.Abs(language_change_time.Minutes), Math.Abs(language_change_time.Seconds));
      btnStartStop.Text = GD.GetString("16007")/*"开始(&O)"*/;
      tmBlinkStopButton.Stop();
      btnStartStop.ForeColor = Color.Black;
      btnStartStop.Font = this.Font;
//       check_trigger.stop();
      MachineController.Stop();
      FrameGrabber.Stop();
      tmGear_ani.Enabled = false;
      isStart = false;
//       flow_info.printf_counts();
    }
    DateTime time_first_view_begin;
    private void btnStartStop_Click(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(btnStartStop_Click));
        return;
      }
      // 准备启动系统，先确认。
      if (!isStart)
        if (!Utils.MB.OKCancel(Speak.GetString("r23001") + "\n" + Speak.GetString("r23004"),
          GD.GetString("91020"), FormMessage.MessageType.Info))
          return;
      if (!isStart)
      {
        time_first_view_begin = DateTime.Now;
        start();
      }
      else
        stop();
      UpdateButtons();
    }

    void halt_actions()
    {
      stop();
      UpdateButtons();
    }
    bool LockHalt = false;
    bool clear_plate = false;
    void Halt(MachineController.State haltreason)
    {
      if (LockHalt)
        return;
      if (!clear_plate)
      {
        FrameGrabber.stop_spinning();
      }
      LockHalt = true;
      if (clear_plate)
      {
        MachineController.send_only("2360", true);
        tm_no_feed.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
      }
      FormBlurMsg msg = new FormBlurMsg();
      msg.Prompt = GD.GetString(((int)haltreason).ToString());
      msg.ShowDialog(this);
      halt_actions();
      MachineController.StopLight(true);
      MachineController.OutBlowCount(false);
      if (clear_plate)
      {
        MachineController.send_only("2360", false);
        Settings.PS.ClearTotal = 0;
        clear_plate = false;
        is_clear = false;
        MachineController.clear_current();
      }
      MachineController.send_only("2362", false);
      LockHalt = false;
    }

    public void btnIsetting()
    {
      if (!btnSetup.Checked)
      {
        return;
      }
      btnSetup.Checked = !btnSetup.Checked;
      btnSetup_Click(null, null);
    }

    System.Windows.Forms.Timer tmBlinkStopButton = new System.Windows.Forms.Timer();
    private bool blinkStop = true;
    private void onTmBlinkStopButton(object sender, EventArgs e)
    {
      blinkStop = !blinkStop;
      if (blinkStop)
        btnStartStop.ForeColor = Color.Red;
      else
        btnStartStop.ForeColor = Color.Black;
    }

    private void btnLive_Click(object sender, EventArgs e)
    {
      if (!FrameGrabber.IsLive)
        FrameGrabber.Live(GD.CurrentView);
      else
        FrameGrabber.Stop();
      //MachineController.Dimming(FrameGrabber.IsLive);
      frmToolKit.EditBtnCorrection();
      UpdateButtons();
    }

    private void btnOneShot_Click(object sender, EventArgs e)
    {
      GD.OneShot();
    }

    private void btnLanguage_Click(object sender, EventArgs e)
    {
      this.Controls.Clear();
      if (Settings.Lang == 2052)
        Settings.Lang = 1033;
      else
        Settings.Lang = 2052;
      System.Threading.Thread.CurrentThread.CurrentUICulture =
        new CultureInfo(Settings.Lang);
      this.InitializeComponent();
      InitNIComponents();
      InitOthers();
    }

    private void btnSaveInfo_Click(object sender, EventArgs e)
    {
      Message.appear(Speak.GetString("r26001"), Message.MessageType.OK);
      Speak.Wizard(26000, 1);
      Settings.PS.Batch = tbBatch.Text;
      Settings.PS.HeatNumber = tbHeatNumber.Text;
      Settings.PS.Orders = tbOrders.Text;
      Settings.PS.Material = tbMaterial.Text;
      Settings.PS.Operator = tbOperator.Text;
      Settings.PS.TestingData = tbTestingData.Text;
    }

    private void btnSaveData_Click(object sender, EventArgs e)
    {
//       Message.appear(Speak.GetString("r25001"), Message.MessageType.OK);
//       Speak.Wizard(25000, 1);
      //FaultyCount.DataTabletoXML();
      DialogResult MsgBoxResult;
      MsgBoxResult = MessageBox.Show(Speak.GetString("r25002"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
      if (MsgBoxResult == DialogResult.OK)
      {
        FaultyCount.DataTabletoExcel();
        FaultyCount.DataTabletoPDF();
      }
      
    }


    private delegate void VoidDelegate();
    void TryToGetRectangle()
    {
      if (currentView.InvokeRequired)
      {
        currentView.Invoke(new VoidDelegate(TryToGetRectangle));
        return;
      }
      GD.rc = currentView.RectangleToScreen(currentView.ClientRectangle);
    }

    bool spawn = false;
    public void ExitSystem(bool _spawn)
    {
      spawn = _spawn;
      this.BeginInvoke(new InvokeDelegate(Exit));
    }

    void OnExitSystem(object sender, EventArgs e)
    {
      ExitSystem(true);
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
//       FormProgress.Stop();
      //       for (ViewIndex i = ViewIndex.VIEW1; i < ViewIndex.VIEWCOUNT; i++)
      //       {
      //         //niGraphics[(int)i].Dispose();
      //         GD.ShowImage(i, false);
      //       }
      if (spawn)
        System.Diagnostics.Process.Start(Application.ExecutablePath);
#if !DEBUG
      System.Threading.Thread.Sleep(3000);
#endif
      FormProgress.Stop();
    }

    private void Exit()
    {
#if !DEBUG
      FormProgress.Start("Closing...");
#endif
      while (!Settings.save_ps())
      {
        if (!Utils.MB.OKCancelW(GD.GetString("91035"), GD.GetString("91036")))
        {
          break;
        }
      }
      this.Visible = false;
#if !DEBUG
//       check_trigger.stop();
//       check_trigger.exit();
      MachineController.Stop();
      FrameGrabber.Stop();
      FrameGrabber.Exit();
      MachineController.Exit();
      tmCheckSystem.Stop();
#endif
      IA.IA_Free();
      Debug.Print("[FormMain] Tell 2 threads to exit...");

      //       for (ViewIndex i = ViewIndex.VIEW1; i < ViewIndex.VIEWCOUNT; i++)
      //       {
      //         //niGraphics[(int)i].Dispose();
      //         System.Threading.Thread.Sleep(10);
      //         GD.ShowImage(i, false);
      //       }
      this.Close();
    }


    private void RegisterEventHandlers()
    {
      MachineController.OnCritical -= this.OnCritical;
      MachineController.OnCritical += this.OnCritical;
      MachineController.OnState -= this.OnState;
      MachineController.OnState += this.OnState;
    }

    delegate void VoidOnCritical(MachineController.State haltreason);
    private void OnCritical(MachineController.State haltreason)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new VoidOnCritical(OnCritical), haltreason);
        return;
      }
      this.Halt(haltreason);
    }

    int state = 0;
    void OnState(object sender, EventArgs e)
    {
      if (sender is int)
        state = (int)sender;
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(OnState));
      }
      else
      {
        lb_state.Text = GD.GetString(state.ToString());
        lb_state.ForeColor = state == 0 ? Color.Lime : Color.Red;
      }
    }


    int piccount = 0;
    Image[] image = new Image[4] { 
                ImagingSortCharlie.Properties.Resources.gear_still1,                
                ImagingSortCharlie.Properties.Resources.gear_still2, 
                ImagingSortCharlie.Properties.Resources.gear_still3, 
                ImagingSortCharlie.Properties.Resources.gear_still4 };
    private void tmGear_ani_Tick(object sender, EventArgs e)
    {
      piccount++;
      int i = piccount % 4;
      pbGear.Image = image[i];
    }

    private void FormMain_Shown(object sender, EventArgs e)
    {
      if (this.Visible)
      {
        this.WindowState = FormWindowState.Maximized;
        FormProgress.Stop();
        this.Activate();
        TryToGetRectangle();
      }
    }

    private void FormMain_Resize(object sender, EventArgs e)
    {
      System.Drawing.Rectangle rc = ClientRectangle;
      System.Drawing.Rectangle rcPanel = plAll.ClientRectangle;
      int x, y;
      x = rc.Width - rcPanel.Width;
      y = rc.Height - rcPanel.Height;
      x /= 2;
      y /= 2;
      plAll.Location = new System.Drawing.Point(x, y);
    }

    private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Escape)
      {
        if (btnSetup.Checked == true && btnSetup.Enabled == true)
        {
          Setup(null, null);
        }
        if (btnSetup.Enabled == false)
        {
          frmToolKit.Cancel();
        }
      }
      else if (e.KeyChar == ' ')
      {

      }
    }

    public void Setup(object sender, EventArgs e)
    {
      btnSetup.Checked = false;
      btnSetup_Click(null, null);
    }

    System.Windows.Forms.Timer tmNow = new System.Windows.Forms.Timer();
    void StartTimerNow()
    {
      if (tmNow != null)
      {
        tmNow.Stop();
        tmNow.Dispose();
      }
      tmNow.Interval = 1000;
      tmNow.Tick -= OnTimerNow;
      tmNow.Tick += OnTimerNow;
      tmNow.Start();
    }
    void OnTimerNow(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now;
      if (Settings.Lang == 1033)
        lbDateTime.Text = new string(now.GetDateTimeFormats('r')[0].ToString().ToCharArray(), 5, 20);
      else
        lbDateTime.Text = now.ToString("D") + " " + now.ToString("t");
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
#if !DEBUG && NEED_REGISTER
      InitRegister();
#endif
    }

    private void InitRegister()
    {
      string sn = Authorization.Decrypt(Configure.This.SequenceNumber, "enmind12");
      if (!Authorization.MatchSN(sn))
      {
        FormRegister frmRegister = new FormRegister();
        frmRegister.OnExitRegister -= this.OnExitRegister;
        frmRegister.OnExitRegister += this.OnExitRegister;
        frmRegister.StartPosition = FormStartPosition.CenterParent;
        frmRegister.ShowDialog(this);
      }
    }

    void OnExitRegister(object sender, EventArgs e)
    {
      ExitSystem(false);
    }

    private void btnLocating_Click(object sender, EventArgs e)
    {
      GD.IsLocating = !GD.IsLocating;
      VIEW view = GD.CurrentView;
      MachineController.Locating(Configure.This.ViewStation[(int)view], GD.IsLocating);
      FrameGrabber.Locating(view, GD.IsLocating);
      UpdateButtons();
    }

    private void btnLast_Click(object sender, EventArgs e)
    {
      Speak.idx_wizard_back--;
      Speak.Wizard();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      Speak.idx_wizard_back++;
      Speak.Wizard();
    }

    private void btnWizard_Click(object sender, EventArgs e)
    {
      Speak.speak = !Speak.speak;
      btnWizard.Text = Speak.speak ? GD.GetString("16012")/*"关闭向导"*/ : GD.GetString("16011")/*"启用向导"*/;
      lbWizard.Visible = Speak.speak;
      RefreshWizardButtons(null, null);
    }
    void RefreshWizardButtons(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(RefreshWizardButtons));
        return;
      }
      btnNext.Enabled = (Speak.idx_wizard_back != Speak.count_current) && Speak.speak;
      btnLast.Enabled = (Speak.idx_wizard_back != 1) && Speak.speak;
    }
    System.Windows.Forms.Timer tmCheckSystem = new System.Windows.Forms.Timer();
    private void InitCheckSystem()
    {
      tmCheckSystem.Tick += new EventHandler(tmCheckSystem_Tick);
      tmCheckSystem.Interval = 60 * 60 * 1000;//一个小时检测一次
      tmCheckSystem.Start();
    }
    private void tmCheckSystem_Tick(object sender, EventArgs e)
    {
      string disk = "E";
      ulong s = IACalc.GetFreeSpace(disk);
      if (s < 1024 * 1024 * 1024)//系统盘少于1G报警
      {
        //Utils.MB.Warning(disk+GD.GetString("91012"));
        //software_halt();
        hardware_halt();
      }
      float l = IACalc.GetMemoryStatus();
      if (l < 0.1)
      {
        //Utils.MB.Warning(GD.GetString("91013"));
        //software_halt();
        hardware_halt();

      }

    }


    private void Language()
    {
      this.Controls.Clear();
      if (Settings.Lang == 2052)
        Settings.Lang = 1033;
      else
        Settings.Lang = 2052;
      Settings.save();
      System.Threading.Thread.CurrentThread.CurrentUICulture =
        new CultureInfo(Settings.Lang);
      this.InitializeComponent();
      IA.IA_Free();
      InitNIComponents();
      InitOthers();
      Message.appear(GD.GetString("91017"), Message.MessageType.Info);
      this.ResumeLayout(true);
      this.PerformLayout();
    }
    private void pbChinese_Click(object sender, EventArgs e)
    {
      Language();
    }

    private void pbEnglish_Click(object sender, EventArgs e)
    {
      Language();
    }

    private void FormMain_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.F12)
      {
        FormBlurMsg msg = new FormBlurMsg();
        msg.Prompt = "测试";
        msg.ShowDialog(this);
      }
    }

    private void button_test_Click(object sender, EventArgs e)
    {
      MachineController.send_only("2200", true);
      UCTest ucTest = new UCTest();
      ucTest.Show();
    }

  }
}
