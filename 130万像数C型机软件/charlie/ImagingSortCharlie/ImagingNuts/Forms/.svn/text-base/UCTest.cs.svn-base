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
  public partial class UCTest : Form
  {
    public UCTest()
    {
      InitializeComponent();
      timer_read_start();
      timer_send_and_read_start(false,true);//只读取R点。
      read_XML_status();
    }
    const int MOTOR_TIME = 1000;
    const int TEST_CYCINDER = 1000;
#region --测试状态
    private void save_XML_status()
    {
      Data.Settings.Configure.This.label_X00 = label_X00.Text;
      Data.Settings.Configure.This.label_X10 = label_X10.Text;
      Data.Settings.Configure.This.label_X11 = label_X11.Text;
      Data.Settings.Configure.This.label_X12 = label_X12.Text;
      Data.Settings.Configure.This.label_X13 = label_X13.Text;
      Data.Settings.Configure.This.label_X15 = label_X15.Text;
      Data.Settings.Configure.This.label_X16 = label_X16.Text;
      Data.Settings.Configure.This.label_X17 = label_X17.Text;
      Data.Settings.Configure.This.label_X18 = label_X18.Text;
      Data.Settings.Configure.This.label_X19 = label_X19.Text;
      Data.Settings.Configure.This.label_X0B = label_X0B.Text;
    }
    private void read_XML_status()
    {
      label_X00.Text = Data.Settings.Configure.This.label_X00;
      label_X10.Text = Data.Settings.Configure.This.label_X10;
      label_X11.Text = Data.Settings.Configure.This.label_X11;
      label_X12.Text = Data.Settings.Configure.This.label_X12;
      label_X13.Text = Data.Settings.Configure.This.label_X13;
      label_X15.Text = Data.Settings.Configure.This.label_X15;
      label_X16.Text = Data.Settings.Configure.This.label_X16;
      label_X17.Text = Data.Settings.Configure.This.label_X17;
      label_X18.Text = Data.Settings.Configure.This.label_X18;
      label_X19.Text = Data.Settings.Configure.This.label_X19;
      label_X0B.Text = Data.Settings.Configure.This.label_X0B;
    }
#endregion
    private static void Sleep(int ms) { System.Threading.Thread.Sleep(ms); }
    private void button_exit_Click(object sender, EventArgs e)
    {
      timer_send_and_read_start(true, false);
      ImagingSortCharlie.Hardware.MachineController.send_only("2200",false);
      timer_stop();  
      save_XML_status();
      this.Visible = false;
      Application.DoEvents();
      this.Close();
    }
    private void timer_send_and_read_start(bool control,bool onoff)
    {
      send_and_read_status(control, "2354", 0, onoff, "2206", label_X17);
      send_and_read_status(control, "2356", 0,
        false, "2208", label_X13);
      send_and_read_status(control, "2350", 0, onoff, "2210", label_X11);
      send_and_read_status(control, "2352", 0, onoff, "2210", label_X12);
    }
    Timer timer_read = new Timer();//检测光电的timer,因为只需读取
    void timer_read_Tick(object sender,EventArgs e)
    {
      read_control();
    }
    void timer_read_start()
    {
      timer_read.Tick -= timer_read_Tick;
      timer_read.Tick += timer_read_Tick;
      timer_read.Interval = 500;
      timer_read.Start();
    }
    private void judge_label(string read_number,Label lb)
    {
      if (ImagingSortCharlie.Hardware.MachineController.only_read(read_number))
      {
        lb.BackColor = Color.Red;
      }
      else
        lb.BackColor = Color.Lime;
    }
    private void read_control()//只读取
    {
      judge_label("2340", label_X00);
      judge_label("2342", label_X10);
      judge_label("2344", label_X0B);
      judge_label("2346", label_X15);
      judge_label("2348", label_X16);
      judge_label("2358", label_X18);
      judge_label("2349", label_X19);
    }
    private void motor_control_start(Timer tm, KeyButton kb, 
      string output_control)
    {
      tm.Interval = MOTOR_TIME;
      tm.Start();
      kb.Enabled = false;
      ImagingSortCharlie.Hardware.MachineController.send_only(
        output_control, true);
      Sleep(MOTOR_TIME);
      ImagingSortCharlie.Hardware.MachineController.send_only(
        output_control, false);
    }
    private void motor_control_stop(Timer tm, KeyButton kb)
    {
      tm.Stop();
      kb.Enabled = true;
    }
    Timer timer_push_fullbox_motor_A = new Timer();
    private void timer_push_fullbox_motor_A_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_push_fullbox_motor_A, button_push_fullbox_motor_A);
    }
    private void button_push_fullbox_motor_A_Click(object sender, EventArgs e)
    {
      timer_push_fullbox_motor_A.Tick -= timer_push_fullbox_motor_A_Tick;
      timer_push_fullbox_motor_A.Tick += timer_push_fullbox_motor_A_Tick;
      motor_control_start(timer_push_fullbox_motor_A, button_push_fullbox_motor_A,
        "2222");
    }
    Timer timer_push_fullbox_motor_B = new Timer();
    void timer_push_fullbox_motor_B_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_push_fullbox_motor_B, button_push_fullbox_motor_B);
    }
    private void button_push_fullbox_motor_B_Click(object sender, EventArgs e)
    {
      timer_push_fullbox_motor_B.Tick -= timer_push_fullbox_motor_B_Tick;
      timer_push_fullbox_motor_B.Tick += timer_push_fullbox_motor_B_Tick;
      motor_control_start(timer_push_fullbox_motor_B, button_push_fullbox_motor_B,
        "2224");
    }
    Timer timer_fullbox_push_motor = new Timer();
    void timer_fullbox_push_motor_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_fullbox_push_motor, button_fullbox_push_motor);
    }

    private void button_fullbox_push_motor_Click(object sender, EventArgs e)
    {
      timer_fullbox_push_motor.Tick -= timer_fullbox_push_motor_Tick;
      timer_fullbox_push_motor.Tick += timer_fullbox_push_motor_Tick;
      motor_control_start(timer_fullbox_push_motor,button_fullbox_push_motor,
        "2212");
    }
    Timer timer_oscillation_motor = new Timer();
    void timer_oscillation_motor_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_oscillation_motor, button_oscillation_motor);
    }
    private void button_oscillation_motor_Click(object sender, EventArgs e)
    {
      timer_oscillation_motor.Tick -= timer_oscillation_motor_Tick;
      timer_oscillation_motor.Tick += timer_oscillation_motor_Tick;
      motor_control_start(timer_oscillation_motor, button_oscillation_motor,
        "2226");
    }
    Timer timer_transport_motor = new Timer();
    void timer_transport_motor_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_transport_motor, button_transport_motor);
    }
    private void button_transport_motor_Click(object sender, EventArgs e)
    {
      timer_transport_motor.Tick -= timer_transport_motor_Tick;
      timer_transport_motor.Tick += timer_transport_motor_Tick;
      motor_control_start(timer_transport_motor, button_transport_motor,
        "2230");
    }
    Timer timer_fullbox = new Timer();
    void timer_fullbox_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_fullbox, button_fullbox);
    }
    private void button_fullbox_Click(object sender, EventArgs e)
    {
      timer_fullbox.Tick -= timer_fullbox_Tick;
      timer_fullbox.Tick += timer_fullbox_Tick;
      motor_control_start(timer_fullbox, button_fullbox,
        "2228");
    }
    Timer timer_inverted_motor = new Timer();
    void timer_inverted_motor_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_inverted_motor, button_inverted_motor);
    }
    private void button_inverted_motor_Click(object sender, EventArgs e)
    {
      timer_inverted_motor.Tick -= timer_inverted_motor_Tick;
      timer_inverted_motor.Tick += timer_inverted_motor_Tick;
      motor_control_start(timer_inverted_motor, button_inverted_motor,
        "2220");
    }
    Timer timer_turnplate = new Timer();
    void timer_turnplate_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_turnplate, button_turnplate);
    }
    private void button_turnplate_Click(object sender, EventArgs e)
    {
      timer_turnplate.Tick -= timer_turnplate_Tick;
      timer_turnplate.Tick += timer_turnplate_Tick;
      motor_control_start(timer_turnplate, button_turnplate,
        "2202");
    }
    Timer timer_blow = new Timer();
    void timer_blow_Tick(object sender,EventArgs e)
    {
      motor_control_stop(timer_blow, button_blow);
    }
    private void button_blow_Click(object sender, EventArgs e)
    {
      timer_blow.Tick -= timer_blow_Tick;
      timer_blow.Tick += timer_blow_Tick;
      motor_control_start(timer_blow, button_blow,
        "2204");
    }
    Timer timer_storage_hopper_cylinder = new Timer();
    void timer_storage_hopper_cylinder_Tick(object sender, EventArgs e)
    {
      send_and_read_status(true, "2354", TEST_CYCINDER, true, "2206", label_X17);
    }
    private void send_and_read_status(bool control,
      string read, int time, bool onoff,string send,Label lb)
    {
      if (ImagingSortCharlie.Hardware.MachineController.send_and_read(
        control,send,time,onoff,read))
      {
        lb.BackColor = Color.Red;
      }
      else
      {
        lb.BackColor = Color.Lime;
      }
    }
    private void button_storage_hopper_cylinder_Click(object sender, EventArgs e)
    {
      timer_storage_hopper_cylinder.Tick -= timer_storage_hopper_cylinder_Tick;
      timer_storage_hopper_cylinder.Tick += timer_storage_hopper_cylinder_Tick;
      timer_storage_hopper_cylinder.Interval = 100;
      button_storage_hopper_cylinder.Checked = 
        !button_storage_hopper_cylinder.Checked;
      if (button_storage_hopper_cylinder.Checked)
      {
        timer_storage_hopper_cylinder.Start();
      }
      else
      {
        timer_storage_hopper_cylinder.Stop();
        send_and_read_status(true,"2354",TEST_CYCINDER, false,"2206",label_X17);
      }
    }
    Timer timer_inverted_hopper_cylinder = new Timer();
    void inverted_hopper_cylinder_Tick(object sender, EventArgs e)
    {
      send_and_read_status(true, "2356", TEST_CYCINDER, true, "2208", label_X13);
    }
    private void button_inverted_hopper_cylinder_Click(object sender, EventArgs e)
    {
      timer_inverted_hopper_cylinder.Tick -= inverted_hopper_cylinder_Tick;
      timer_inverted_hopper_cylinder.Tick += inverted_hopper_cylinder_Tick;
      timer_inverted_hopper_cylinder.Interval = 100;
      button_inverted_hopper_cylinder.Checked =
        !button_inverted_hopper_cylinder.Checked;
      if (button_inverted_hopper_cylinder.Checked)
      {
        timer_inverted_hopper_cylinder.Start();
      }
      else
      {
        timer_inverted_hopper_cylinder.Stop();
        send_and_read_status(true, "2356", TEST_CYCINDER, 
          false, "2208", label_X13);
      }
    }
    Timer timer_emptybox_cylinder = new Timer();
    void timer_emptybox_cylinder_Tick(object sender,EventArgs e)
    {
      send_and_read_status(true, "2350", TEST_CYCINDER, true, "2210", label_X11);
      send_and_read_status(true, "2352", TEST_CYCINDER, true, "2210", label_X12);
    }
    private void button_emptybox_cylinder_Click(object sender, EventArgs e)
    {
      timer_emptybox_cylinder.Tick -= timer_emptybox_cylinder_Tick;
      timer_emptybox_cylinder.Tick += timer_emptybox_cylinder_Tick;
      timer_emptybox_cylinder.Interval = 100;
      button_emptybox_cylinder.Checked = !button_emptybox_cylinder.Checked;
      if (button_emptybox_cylinder.Checked)
      {
        timer_emptybox_cylinder.Start();
      }
      else
      {
        timer_emptybox_cylinder.Stop();
        send_and_read_status(true, "2350", TEST_CYCINDER, 
          false, "2210", label_X11);
        send_and_read_status(true, "2352", TEST_CYCINDER, 
          false, "2210", label_X12);
      }
    }
    private void timer_stop()
    {
      timer_read.Stop();
      timer_turnplate.Stop();
      timer_blow.Stop();
      timer_storage_hopper_cylinder.Stop();
      timer_inverted_hopper_cylinder.Stop();
      timer_emptybox_cylinder.Stop();
      timer_push_fullbox_motor_A.Stop();
      timer_push_fullbox_motor_B.Stop();
      timer_fullbox_push_motor.Stop();
      timer_oscillation_motor.Stop();
      timer_transport_motor.Stop();
      timer_fullbox.Stop();
      timer_inverted_motor.Stop();
    }

  }
}
