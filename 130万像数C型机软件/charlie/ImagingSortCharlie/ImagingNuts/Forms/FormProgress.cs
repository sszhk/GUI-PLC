using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Forms
{
  public partial class FormProgress : Form
  {
    public FormProgress()
    {
      InitializeComponent();
    }

    private void Progress_Load(object sender, EventArgs e)
    {

    }
    public static void Stop()
    {
      if (running)
        exiting = true;
    }
    public static void Start(string _prompt)
    {
      if (running)
        return;
      exiting = false;
      //title = _title;
      prompt = _prompt;
      System.Threading.Thread th = new System.Threading.Thread(thrd);
      th.IsBackground = true;
      th.Name = "Show Progress Dialog";
      th.Start();
    }
    private static volatile bool running = false;
    private static volatile bool exiting = false;
    private static string title = null, prompt = null;
    public static void thrd()
    {
      running = true;
      FormProgress prog = new FormProgress();
      if (title != null)
        prog.Text = title;
      if (prompt != null)
        prog.lbPrompt.Text = prompt;
      prog.Show();
      while (!exiting)
      {
        Application.DoEvents();
        System.Threading.Thread.Sleep(1);
      }

      //prog.lbPrompt.Text = GD.GetString("21001")/*"即将完成，请稍候……"*/;
      //System.Threading.Thread.Sleep(1000);
      Utils.TimerCounter tc = new ImagingSortCharlie.Utils.TimerCounter();
      tc.Start();
      while (true)
      {
        Application.DoEvents();
        System.Threading.Thread.Sleep(1);
        tc.Stop();
        if (tc.Duration > 1)
          break;
      }
      //prog.Hide();
      running = false;
    }

  }
}
