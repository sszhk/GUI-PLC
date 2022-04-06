using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagingSortCharlie.Forms
{
  public partial class Message : Form
  {
    public enum MessageType
    {
      OK,
      Setting,
      Info,
      Warning,
      Error
    }

    //static Message I = new Message();
    static int bottom = 0;
    private Message()
    {
      InitializeComponent();
    }
//     public Message(string title, string content)
//     {
//       InitializeComponent();
//       Title = title;
//       Content = content;
//       appear();
//     }
    public string Title { set { lb_title.Text = value; } }
    public string Content { set { lb_content.Text = value; } }
    public Image Picture { set { pictureBox2.Image = value;} }
    const int DEFAULT_TIMEOUT = 5000; // 5 secs
    const double OPAC_FROM = 0.0, OPAC_TO = 0.90;
    const double OPAC_STEP = 0.15;
    const int DEFAULT_OPAC_TIME = 100; // 100 ms
    double opa = 0;

    Timer t_time_to_go = new Timer();
    private void set_timeout(int timeout)
    {
      t_time_to_go.Dispose();
      t_time_to_go = new Timer();
      t_time_to_go.Interval = timeout;
      t_time_to_go.Tick += new EventHandler(t_Tick);
      t_time_to_go.Start();
    }
    void t_opacity_Tick(object sender, EventArgs e)
    {
      opa += OPAC_STEP;
      this.Opacity = opa;
      if( this.Opacity >= OPAC_TO )
      {
        this.Opacity = OPAC_TO;
        t_opacity.Stop();
      }
    }
    void t_disappear_Tick(object sender, EventArgs e)
    {
      opa -= OPAC_STEP;
      this.Opacity = opa;
      if (this.Opacity <= OPAC_FROM)
      {
        t_opacity.Stop();
        hide();
      }
    }
    void check_size()
    {
      Rectangle rc;
      rc = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
      //if( !FormMain.I.IsHandleCreated )
      //Rectangle rc = FormMain.I.RectangleToScreen(FormMain.I.ClientRectangle);

      using(Graphics g=this.CreateGraphics())
      {
        //int height = (int)g.MeasureString(lb_content.Text, lb_content.Font).Height+2;
          //+lb_content.Font.Height;
        Size s = lb_content.GetPreferredSize(lb_content.MaximumSize);
        int height = s.Height;
        int h = lb_content.Height;
        int dh = height - h;
        this.Height += dh;
      }
      bottom += this.Height + MARGIN;
      this.Location = new Point(
        rc.Right - this.Width - MARGIN,
        rc.Bottom - bottom);
    }
    Timer t_opacity = new Timer();
    private void set_appear()
    {
      check_size();

      opa = OPAC_FROM;
      this.Opacity = opa;
      this.Show();
      t_opacity.Dispose();
      t_opacity = new Timer();
      t_opacity.Interval = DEFAULT_OPAC_TIME;
      t_opacity.Tick += new EventHandler(t_opacity_Tick);
      t_opacity.Start();
    }
    private void set_disappear()
    {
      opa = this.Opacity;
      t_opacity.Dispose();
      t_opacity = new Timer();
      t_opacity.Interval = DEFAULT_OPAC_TIME;
      t_opacity.Tick += new EventHandler(t_disappear_Tick);
      t_opacity.Start();
    }

//     public static void appear(int timeout)
//     {
//       Message I = new Message();
//       I.set_appear();
//       I.set_timeout(timeout);
//     }

    void t_Tick(object sender, EventArgs e)
    {
      t_time_to_go.Stop();
      disappear();
    }
    public static void appear(string title, string content, Image img)
    {
      Message I = new Message();
      I.Title = title;
      I.Picture = img;
      I.Content = content;
      I.set_appear();
      I.set_timeout(DEFAULT_TIMEOUT);
    }
    private static string get_title(MessageType mt)
    {
      string title;
      switch (mt)
      {
        case MessageType.OK:
          title = GD.GetString("91004");//"成功";
          break;

        case MessageType.Setting:
          title = GD.GetString("91014");//"设置";
          break;
        case MessageType.Info:
          title = GD.GetString("91015");//"提示";
          break;
        case MessageType.Warning:
          title = GD.GetString("91007");//"警告";
          break;
        case MessageType.Error:
          title = GD.GetString("91006");//"错误";
          break;
        default:
          title = GD.GetString("91015");//"提示";
          break;
      }
      return title;
    }
    private static Image get_picture(MessageType mt)
    {
      Image img;
      switch (mt)
      {
        case MessageType.OK:
          img = Properties.Resources.shadow_ok;
          break;

        case MessageType.Setting:
          img = Properties.Resources.shadow_process;
          break;
        case MessageType.Info:
          img = Properties.Resources.shadow_info;
          break;
        case MessageType.Warning:
          img = Properties.Resources.shadow_warning;
          break;
        case MessageType.Error:
          img = Properties.Resources.shadow_block;
          break;
        default:
          img = Properties.Resources.shadow_info;
          break;
      }
      return img;
    }
    public static void appear(string content, MessageType mt)
    {
      Image img = get_picture(mt);
      string title = get_title(mt);
      appear(title, content, img);
    }
//     public static void appear(string title, string content)
//     {
//       appear(title, content, get_picture(MessageType.Info));
//     }
    public void disappear()
    {
      set_disappear();
    }

    const int MARGIN = 10;
    private void Message_Load(object sender, EventArgs e)
    {
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      t_opacity.Dispose();
      t_time_to_go.Dispose();
      hide();
    }

    private void Message_MouseEnter(object sender, EventArgs e)
    {
      t_opacity.Stop();
      t_time_to_go.Stop();
      this.Opacity = 1;
    }

    private void Message_MouseLeave(object sender, EventArgs e)
    {
      t_time_to_go.Start();
    }
    private void hide()
    {
      //bottom -= this.Height - MARGIN;
      Rectangle rc;
      rc = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
      bottom = Math.Min(bottom, rc.Bottom - this.Bottom-MARGIN);

      this.Close();
    }
    private void Message_FormClosed(object sender, FormClosedEventArgs e)
    {
    }
  }
}
