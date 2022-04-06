using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigApp.Windows
{
  public partial class form_main : Form
  {
    uc_camera camera = new uc_camera();
    uc_settings setting = new uc_settings();
    public form_main()
    {
      InitializeComponent();
      pl_camera.Controls.Add(camera);
      pl_setting.Controls.Add(setting);
      tb_sn.Text = regist.decrypt(Configure.This.SequenceNumber, "enmind12") ;
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
      string del = "-";
      string sn = tb_sn.Text.Replace(del, "");
      Configure.This.SequenceNumber = regist.encrypt(sn, "enmind12");
      setting.save();
      camera.save();
      Configure.save();
    }

    private void btn_exit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_get_Click(object sender, EventArgs e)
    {
      tb_mn.Text = regist.make_machine_number();
    }

  }
}
