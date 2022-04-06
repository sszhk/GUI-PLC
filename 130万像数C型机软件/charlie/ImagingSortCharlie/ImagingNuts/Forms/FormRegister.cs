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
  public partial class FormRegister : Form
  {
    public FormRegister()
    {
      InitializeComponent();
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
      string s = tbSN1.Text + tbSN2.Text + tbSN3.Text + tbSN4.Text;
      string sn = Authorization.Encrypt(s, "enmind12");
      if (Authorization.MatchSN(s))
      {
        Configure.This.SequenceNumber = sn;
        Configure.save();
        Utils.MB.OK(GD.GetString("19001")/*"注册成功"*/);
        this.Close();
      }
      else
      {
        System.Threading.Thread.Sleep(5000);
        Utils.MB.OK(GD.GetString("19002")/*"注册失败,请重新注册"*/);
      }
    }

    private void btnGetMachineNumber_Click(object sender, EventArgs e)
    {
      string mn = Authorization.MakeMachineNumber();
      tbMachineNumber.Text = mn;
    }

    public event EventHandler OnExitRegister;
    private void btnExit_Click(object sender, EventArgs e)
    {
      if (OnExitRegister != null)
      {
        OnExitRegister(null, null);
      }
      this.Close();
    }

  }
}
