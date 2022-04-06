using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Utils.Devices;

namespace CamConfig
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
      lv.Columns[0].Width = 130;
      lv.Columns[1].Width = 140;
      lv.Columns[2].Width = 100;
    }
    //Dictionary<uint, uint> display_dict = new Dictionary<uint, uint>();
    //Dictionary<uint, uint> cam_dict = new Dictionary<uint, uint>();
    List<uint> views = new List<uint>();
    List<uint> serials = new List<uint>();

    const int MAX_VIEW = 7;
    private void search_cams()
    {
      lv.Items.Clear();
      lv.Enabled = false;
      lb_info.Text = "Searching...";
      Application.DoEvents();

      views.Clear();
      serials.Clear();
      //cam_dict.Clear();
      int total = PGGrab.GrabTotalCameras();
      if (total == 0)
      {
        lv.Enabled = false;
        lb_info.Text = "No cameras found";
        return;
      }
      lb_info.Text = string.Format("Totally {0} cameras were found", total);
      lv.Enabled = true;
      //string key = "", value = "";
      uint view = 0, serial = 0;
      for (int i = 0; i < MAX_VIEW/*total*/; i++)
      {
        view = (uint)i;
        serial = (uint)PGGrab.GrabGetSerial(i);
        //cam_dict.Add(serial, view);
        //display_dict.Add(view, serial);
        views.Add(view);
        serials.Add(serial);
      }
    }
    private void reload()
    {
      lv.Items.Clear();
      lb_info.Text = "";

      int total = serials.Count;
      if( total == 0 )
      {
        lv.Enabled = false;
        lb_info.Text = "No cameras found";
        return;
      }
      lb_info.Text = string.Format("Totally {0} cameras were found", total);
      lv.Enabled = true;
      for (int i = 0; i < total; i++ )
      {
        string key = "", value = "";
        uint serial = serials[i];
        //if (serial != 0)
        key = serial.ToString();
        value = string.Format("View{0}", views[i] + 1);
        ListViewItem item = lv.Items.Add(value);
        item.UseItemStyleForSubItems = false;
        item.SubItems.Add(key, Color.Blue, lv.BackColor, 
          new Font(lv.Font, FontStyle.Bold));
        item.SubItems.Add(serial != 0 ? "True" : "False");
      }
    }
    Timer t = new Timer();
    private void FormMain_Load(object sender, EventArgs e)
    {
      t.Interval = 200;
      t.Tick += new EventHandler(t_Tick);
      t.Start();
    }

    void t_Tick(object sender, EventArgs e)
    {
      t.Stop();
      //search_cams();
      load_xml();
      reload();
    }

    private void btn_refresh_Click(object sender, EventArgs e)
    {
      search_cams();
      reload();
    }
    private bool dirty = false;
    const string CAM_CONFIG = "CamConfig.xml";
    private bool load_xml()
    {
      try
      {
        CamConfig config = ImagingSortCharlie.Xml.XMLUtil<CamConfig>.LoadXml(CAM_CONFIG);
        if (config == null)
          return false;
        serials.Clear();
        views.Clear();
        serials.AddRange(config.Serials);
        views.AddRange(config.Views);
        return true;
      }
      catch(Exception e)
      {
        MessageBox.Show("Load from CamConfig.xml error."+Environment.NewLine+e.Message);
        return false;
      }
    }
    private bool save_xml()
    {
      CamConfig config = new CamConfig();
      config.Serials.AddRange(serials);
      config.Views.AddRange(views);
      bool old = dirty;
      try
      {
        dirty = false;
        return ImagingSortCharlie.Xml.XMLUtil<CamConfig>.SaveXml(CAM_CONFIG, config);
      }
      catch (System.Exception ex)
      {
        MessageBox.Show(ex.Message);
        dirty = old;
        return false;
      }
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
      save_xml();
    }
    bool is_moving = false;
    int first = 0;
    private void lv_MouseDown(object sender, MouseEventArgs e)
    {
      ListViewHitTestInfo hit = lv.HitTest(e.X, e.Y);
      if (hit.SubItem == null)
        return;
      first = hit.Item.Index;
      is_moving = true;
    }
    private void swap_text(ListViewItem.ListViewSubItem s1, 
      ListViewItem.ListViewSubItem s2)
    {
      string s = s1.Text;
      s1.Text = s2.Text;
      s2.Text = s;
    }
    private void lv_MouseMove(object sender, MouseEventArgs e)
    {
      if (!is_moving)
        return;
      ListViewHitTestInfo hit = lv.HitTest(e.X, e.Y);
      if (hit.Item == null || hit.Item.Index == first )
        return;
      int second = hit.Item.Index;
      uint u1 = serials[first];
      uint u2 = serials[second];
      //swap_text(lv.Items[first].SubItems[1], lv.Items[second].SubItems[1]);
      serials[first] = u2;
      serials[second] = u1;
      first = second;
      for (int i=0; i<serials.Count; i++)
      {
        if( i == first )
        {
          lv.Items[i].BackColor = lv.Items[i].SubItems[1].BackColor = Color.Yellow;
          
        } else if( i==second )
        {
          //lv.Items[i].BackColor = lv.Items[i].SubItems[1].BackColor = Color.Yellow;
        }
        else
        {
          lv.Items[i].BackColor = lv.Items[i].SubItems[1].BackColor = lv.BackColor;
        }
        if( lv.Items[i].SubItems[1].Text != serials[i].ToString())
        {
          lv.Items[i].SubItems[1].Text = serials[i].ToString();
        }
        lv.Items[i].SubItems[2].Text = serials[i] == 0 ? "False" : "True";
      }
      dirty = true;
    }

    private void lv_MouseUp(object sender, MouseEventArgs e)
    {
      first = 0;
      is_moving = false;
    }

    private void lv_SelectedIndexChanged(object sender, EventArgs e)
    {
      foreach (ListViewItem i in lv.Items)
        i.Focused = false;
      lv.SelectedItems.Clear();
    }

    private void lv_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!dirty)
        return;
      DialogResult dr = MessageBox.Show(
        "Document has been changed, would you like to save it?"+Environment.NewLine+
        "Press Yes to save, No to discard"+Environment.NewLine+
        "Press Cancel to stay still",
        "Quit",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if( dr == DialogResult.Yes )
      {
        save_xml();
      }
      if (dr == DialogResult.Cancel)
        e.Cancel = true;
    }

  }
}
