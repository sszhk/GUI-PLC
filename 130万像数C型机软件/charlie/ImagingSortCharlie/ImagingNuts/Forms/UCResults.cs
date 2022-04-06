using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Filters;
using ImagingSortCharlie.Data.Settings;
using ImagingSortCharlie.Hardware;

namespace ImagingSortCharlie.Forms
{
  public partial class UCResults : UserControl
  {
    private bool do_not_fire = false;
    Panel[] pl_team;

    public UCResults()
    {
      InitializeComponent();
      InitResults();
      //result_clear();
    }

    public void Init()
    {
      this.Controls.Clear();
      InitializeComponent();
      Hardware.MachineController.OnGoodNumChanged -= this.on_good_num_changed;
      Hardware.MachineController.OnGoodNumChanged += this.on_good_num_changed;
      Settings.OnResultChanged -= this.on_refresh_results;
      Settings.OnResultChanged += this.on_refresh_results;
      on_refresh_results(null, null);
      init_config();
    }

    void init_config()
    {
      pl_team = new Panel[] { pl_team1, pl_team2, pl_team3, pl_team4, pl_team5 };
      for (int i = Configure.This.TeamCount; i < (int)TEAM.TEAMCOUNT; i++)
      {
        pl_team[i].Visible = false;
      }
    }
    private void InitResults()
    {
      tbResults.Minimum = 1;
      pbResults.Minimum = 1;
      tbResults.Maximum = 1;
      pbResults.Maximum = 1;
    }

    delegate void VoidVoid();
    public void ValueChanged()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new VoidVoid(ValueChanged));
        return;
      }
      lbTeam1NGSum.Text = Settings.PS.BadCount[0].ToString();
      lbTeam2NGSum.Text = Settings.PS.BadCount[1].ToString();
      lbTeam3NGSum.Text = Settings.PS.BadCount[2].ToString();
      lbTeam4NGSum.Text = Settings.PS.BadCount[3].ToString();
      lbTeam5NGSum.Text = Settings.PS.BadCount[4].ToString();
      lbSumNumber.Text = Settings.PS.Total.ToString();
    }

    void on_good_num_changed(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(on_good_num_changed));
        return;
      }
      lbGoodSum.Text = Settings.PS.GoodSum.ToString();
      lbNGSum.Text = Settings.PS.BadSum.ToString();
    }

    delegate void VoidDelegateValueChanged(Label lb, string s);
    private void DelegateValueChanged(Label lb, string s)
    {
      if (lb.InvokeRequired)
      {
        lb.BeginInvoke(new VoidDelegateValueChanged(DelegateValueChanged), lb, s);
      }
      else
        lb.Text = s;
    }

    private void SetZero()
    {
      for (int i = 0; i < Settings.PS.BadCount.Length; i++)
      {
        Settings.PS.BadCount[i] = 0;
      }
      Settings.PS.BadSum = 0;
      Settings.PS.Total = 0;
      Hardware.MachineController.set_zero();
    }
//     void OnTimerNow()
//     {
//       DateTime now = DateTime.Now;
//       if (Settings.Lang == 1033)
//         lbDateTime.Text = new string(now.GetDateTimeFormats('r')[0].ToString().ToCharArray(), 5, 20);
//       else
//         lbDateTime.Text = now.ToString("D") + " " + now.ToString("t");
//     }
    private void ShowResultList(FilterList fl)
    {
      int selected = 0;
      if (lvResults.FocusedItem != null)
        selected = lvResults.FocusedItem.Index;

      lvResults.BeginUpdate();
      lvResults.Items.Clear();

      int current = pbResults.Value - 1;
      List<ResultValue> lst = fl.GetResultHistory(current);
      if (lst == null || lst.Count == 0)
      {
        lvResults.EndUpdate();
        return;
      }
      ListViewItem lvTime = new ListViewItem(GD.GetString("18011")/*"时间"*/);
      string ItemTime="";
      if (Settings.Lang == 1033)
      {
        ItemTime = new string(lst[0].Time.GetDateTimeFormats('r')[0].ToString().ToCharArray(), 5, 20);
      }
      else
        ItemTime = lst[0].Time.ToString("D") + " " + lst[0].Time.ToString("t");
      lvTime.SubItems.Add(ItemTime);
      lvTime.ForeColor = Color.Gray;
      lvResults.Items.Add(lvTime);
      foreach (ResultValue rv in lst)
      {
        if (rv.Key == GD.GetString("18011")/*"计算时间"*/)
        {
          ListViewItem lvi = new ListViewItem(rv.Name);
          lvi.Tag = rv.Owner;
          lvResults.Items.Add(lvi);
        }
        else
        {
          ListViewItem lvi = new ListViewItem(rv.Key);
          lvi.Tag = rv.Owner;
          lvi.SubItems.Add(rv.Value);
          lvi.ForeColor = rv.Color;
          lvResults.Items.Add(lvi);
        }
      }
      lvResults.EndUpdate();
      //this.Controls.Add(lvResults);
    }

    private void tbResults_ValueChanged(object sender, EventArgs e)
    {
      if (do_not_fire)
        return;
      if (tbResults.Value<pbResults.Minimum ||
        tbResults.Value>pbResults.Maximum)
      {
        return;
      }
      pbResults.Value = (int)tbResults.Value;
      RefreshResults(null, null);
    }
    private void result_clear()
    {
      lvResults.Items.Clear();
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        FilterList fl = GD.SettingsForView((VIEW)i);
        fl.ClearHistory();
        pbResults.Maximum = Math.Max(1, fl.ResultsCount());
        tbResults.Maximum = Math.Max(1, fl.ResultsCount());
      }
      //FilterList fl = GD.SettingsForView(GD.CurrentView);
      
      pbResults.Minimum = 1;
      tbResults.Minimum = 1;
      pbResults.Value = 1;
      tbResults.Value = 1;
    }
    string before = GD.GetString("18007");
    private void lbZero_click(object sender, EventArgs e)
    {
      if (GD.IsLiveOrSpinning)
      {
        Utils.MB.Warning(GD.GetString("18004")/*"系统目前正在运转，请停止后再尝试归零。"*/);
        return;
      }
      if (!Utils.MB.OKCancelQ(GD.GetString("18005")/*"您确认要将统计数据归零吗？"*/+
          System.Environment.NewLine +
          System.Environment.NewLine +
          GD.GetString("18006")/*"注：归零的时间将被记录，并且该操作无法恢复。"*/
          ))
        return;
      DateTime now = DateTime.Now;
      //enLog.WriteLine(enLevel.EL_DEBUG, "#Zeroing statistics on " + now.ToString("F"));

      //OperationRecords.PushZeroTime();
      //FrameGrabber.signal_count = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
      SetZero();
      ValueChanged();
      result_clear();
      string s = before; /*"<无>"*/ ;
//       if (OperationRecords.LastZero() != DateTime.MinValue)
//       {
//         s = OperationRecords.LastZero().ToString("F");
//       }
      string p = GD.GetString("18008")/*"点击这里，确认归零（上次归零时间："*/ +
        s + "）";
      if (p != toolTip2.GetToolTip(btnReset))
        toolTip2.SetToolTip(btnReset, p);
      Settings.save_ps();
      string ItemTime = "";
      if (Settings.Lang == 1033)
      {
        ItemTime = new string(DateTime.Now.GetDateTimeFormats('r')[0].ToString().ToCharArray(), 5, 20);
      }
      else
        ItemTime = DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("t");
      before = ItemTime;
      Utils.MB.OKI(GD.GetString("18009")/*"统计数据已归零。"*/+
        System.Environment.NewLine +
        GD.GetString("18010")/*"最近一次归零时间："*/+
        ItemTime);
    }

    private void pbResults_Scroll(object sender, EventArgs e)
    {
      if (do_not_fire)
        return;
      do_not_fire = true;
      tbResults.Value = pbResults.Value;
      do_not_fire = false;
      RefreshResults(null, null);
    }

    public void RefreshResults(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(RefreshResults));
        return;
      }
      if (GD.CurrentView != VIEW.NONE)
      {
        FilterList fl = GD.SettingsForView(GD.CurrentView);
        ShowResultList(fl);
      }
      lb_header.rearrange(lvResults);
      lb_header.Refresh();
    }

    private void btnExpand_Click(object sender, EventArgs e)
    {
      if (pl_result.Height == 247)
      {
        pl_result.Height = 430;
      }
      else
      {
        pl_result.Height = 247;
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
//       if (!Utils.MB.OKCancelQ(GD.GetString("18013")/*"您确认要清除测量数据吗？"*/+
//         System.Environment.NewLine +
//         System.Environment.NewLine +
//         GD.GetString("18014")/*"该操作不会影响统计数据。"*/))
//         return;
//       lvResults.Items.Clear();
//       ViewIndex idx = GD.CurrentView;
//       FilterList fl = GD.SettingsForView(idx);
//       fl.ClearHistory();
//       pbResults.Minimum = 1;
//       tbResults.Minimum = 1;
//       pbResults.Maximum = Math.Max(1, fl.ResultsCount());
//       tbResults.Maximum = Math.Max(1, fl.ResultsCount());
//       pbResults.Value = 1;
    }

    public void View()
    {
        VIEW idx = GD.CurrentView;
        switch (idx)
        {
          case VIEW.VIEW1 : lbView.Text = GD.GetString("18001");
            break;
          case VIEW.VIEW2 : lbView.Text = GD.GetString("18002");
            break;
          case VIEW.VIEW3 : lbView.Text = GD.GetString("18003");
            break;
          case VIEW.VIEW4 : lbView.Text = GD.GetString("18016");
            break;
          case VIEW.VIEW5: lbView.Text = GD.GetString("18017");
            break;
          case VIEW.VIEW6: lbView.Text = GD.GetString("18018");
            break;
          default:
            break;
        }
        char[] c = { '.' };
        tbProfile.Text = Settings.ProfileName.Split(c)[0];
        update_results();
        point_to_recent();
    }

    private void update_results()
    {
      FilterList fl = GD.SettingsForView(GD.CurrentView);
      pbResults.Maximum = Math.Max(fl.ResultsCount(), pbResults.Minimum);
      tbResults.Maximum = Math.Max(fl.ResultsCount(), pbResults.Minimum);
      do_not_fire = true;
      tbResults.Value = tbResults.Maximum;
      do_not_fire = false;
    }

    private void point_to_recent()
    {
      pbResults.Value = pbResults.Maximum;
      RefreshResults(null, null);
    }

    public void OnDisplayResult(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(OnDisplayResult), sender, e);
        return;
      }

      if (sender is FilterList)
      {
        update_results();
        point_to_recent();
      }
    }

    public void on_refresh_results(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new EventHandler(on_refresh_results));
        return;
      }
      ValueChanged();
      on_good_num_changed(null, null);
    }

  }
}
