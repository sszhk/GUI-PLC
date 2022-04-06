using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ConfigApp.Windows
{
  public partial class uc_camera : UserControl
  {
    const int MAX_VIEW = 8;
    ComboBox[] cb_serials;
    ComboBox[] cb_teams;
    ComboBox[] cb_stations;
    Label[] lb_views;
    bool is_init = true;

    public uc_camera()
    {
      InitializeComponent();
      init();
    }

    void init()
    {
      cb_serials = new ComboBox[MAX_VIEW] { cb_serial1, cb_serial2, cb_serial3, cb_serial4, cb_serial5, cb_serial6, cb_serial7, cb_serial8 };
      cb_teams = new ComboBox[MAX_VIEW] { cb_team1, cb_team2, cb_team3, cb_team4, cb_team5, cb_team6, cb_team7, cb_team8 };
      cb_stations = new ComboBox[MAX_VIEW] { cb_station1, cb_station2, cb_station3, cb_station4, cb_station5, cb_station6, cb_station7, cb_station8 };
      lb_views = new Label[MAX_VIEW] { lb_view1, lb_view2, lb_view3, lb_view4, lb_view5, lb_view6, lb_view7, lb_view8 };
      tb_station_count.Value = Configure.This.StationCount;
      tb_team_count.Value = Configure.This.TeamCount;
      is_init = false;
//       if (Configure.This.CameraCount == 0)
//         get_list_camera();
      refresh();
    }

    void refresh()
    {
      for (int i = Configure.This.CameraCount; i < MAX_VIEW; i++)
      {
        cb_serials[i].Visible = false;
        cb_teams[i].Visible = false;
        cb_stations[i].Visible = false;
        lb_views[i].Visible = false;
      }
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        cb_serials[i].Visible = true;
        cb_serials[i].Items.Clear();
        for (int j = 0; j < Configure.This.CameraCount; j++)
        {
          cb_serials[i].Items.Add(Configure.This.ViewSerial[j]);
        }
        cb_serials[i].SelectedIndex = i;

        cb_teams[i].Visible = true;
        cb_teams[i].Items.Clear();
        for (int j = 0; j < Configure.This.TeamCount; j++)
        {
          if (!cb_teams[i].Items.Contains((Configure.TEAM)j))
            cb_teams[i].Items.Add((Configure.TEAM)j);
        }
        cb_teams[i].SelectedItem = Configure.This.ViewTeam[i];

        cb_stations[i].Visible = true;
        cb_stations[i].Items.Clear();
        for (int j = 0; j < Configure.This.StationCount; j++)
        {
          if (!cb_stations[i].Items.Contains((Configure.STATION)j))
            cb_stations[i].Items.Add((Configure.STATION)j);
        }
        cb_stations[i].SelectedItem = Configure.This.ViewStation[i];
        lb_views[i].Visible = true;
      }
    }

    void refresh_team()
    {
      Configure.This.ViewTeam.Clear();
      for (int i = 0; i < Configure.This.TeamCount; i++)
      {
        Configure.This.ViewTeam.Add((Configure.TEAM)i);
      }
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        cb_teams[i].Items.Clear();
        for (int j = 0; j < Configure.This.TeamCount; j++)
        {
          Configure.TEAM team = (Configure.TEAM)(j % (int)Configure.TEAM.TEAMCOUNT);
          if (!cb_teams[i].Items.Contains(team))
            cb_teams[i].Items.Add(team);
        }
        cb_teams[i].SelectedItem = Configure.This.ViewTeam[i % Configure.This.TeamCount];
      }
    }

    void refresh_station()
    {
      Configure.This.ViewStation.Clear();
      for (int i = 0; i < Configure.This.StationCount; i++)
      {
        Configure.This.ViewStation.Add((Configure.STATION)i);
      }
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        cb_stations[i].Items.Clear();
        for (int j = 0; j < Configure.This.StationCount; j++)
        {
          Configure.STATION station = (Configure.STATION)(j % (int)Configure.STATION.STATION_COUNT);
          if (!cb_stations[i].Items.Contains(station))
            cb_stations[i].Items.Add(station);
        }
        cb_stations[i].SelectedItem = Configure.This.ViewStation[i % Configure.This.StationCount];
      }
    }

    public void save()
    {
      Configure.This.ViewSerial.Clear();
      Configure.This.ViewTeam.Clear();
      Configure.This.ViewStation.Clear();
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        Configure.This.ViewSerial.Add((uint)cb_serials[i].SelectedItem);
        Configure.This.ViewTeam.Add((Configure.TEAM)cb_teams[i].SelectedItem);
        Configure.This.ViewStation.Add((Configure.STATION)cb_stations[i].SelectedItem);
      }
      const string file = "DefaultCamera.xml";
      for (int i = 0; i < Configure.This.CameraCount; i++ )
      {
        string file_name = Configure.This.ViewSerial[i].ToString();
        file_name += ".xml";
        string direct = Path.GetDirectoryName(Application.ExecutablePath);
        string path = Path.Combine(direct, file_name);
        File.Copy(file, file_name, true);
      }
    }

    void get_list_camera()
    {
      Configure.This.CameraCount = PGGrab.GetTotalCameras();
      Configure.This.ViewSerial.Clear();
      Configure.This.ViewTeam.Clear();
      Configure.This.ViewStation.Clear();
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        uint serial = (uint)PGGrab.GrabGetSerial(i);
        Configure.This.ViewSerial.Add(serial);
        Configure.This.ViewTeam.Add((Configure.TEAM)(i % Configure.This.TeamCount));
        Configure.This.ViewStation.Add((Configure.STATION)(i % Configure.This.StationCount));
      }
    }

    private void btn_refresh_Click(object sender, EventArgs e)
    {
      get_list_camera();
      refresh();
    }

    private void tb_team_count_ValueChanged(object sender, EventArgs e)
    {
      if (is_init)
        return;
      Configure.This.TeamCount = (int)tb_team_count.Value;
      refresh_team();
    }

    private void tb_station_count_ValueChanged(object sender, EventArgs e)
    {
      if (is_init)
        return;
      Configure.This.StationCount = (int)tb_station_count.Value;
      refresh_station();
    }


  }
}
