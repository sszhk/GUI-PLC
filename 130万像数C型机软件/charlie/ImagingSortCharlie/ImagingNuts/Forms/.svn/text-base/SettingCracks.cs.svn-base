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

namespace ImagingSortCharlie.Forms
{
  public partial class SettingCracks : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.CalcCracks filter = null;
    public SettingCracks(CalcCracks fil)
    {
      filter = fil;
      View = fil.PublicOptions.View;
      InitializeComponent();
    }

    public UserControl GetControl()
    {
      return this;
    }
    public void OnOK()
    {
      filter.Name = tbName.Text;
      filter.CrackOptions.IsCenterCrack = cbCenterCrack.Checked;
      float.TryParse(tbCenterCrack.Text, out filter.CrackOptions.CenterCrack);
    }
    public void OnCancel()
    {
      ReadData();
    }
    public int GetCorrectionCount()
    {
      return 0;
    }
    public bool OnCorrection()
    {
      return true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbCenterCrack.Text = filter.CrackOptions.CenterCrack.ToString();
      cbCenterCrack.Checked = filter.CrackOptions.IsCenterCrack;
    }

    private void SettingCracks_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }



  }
}
