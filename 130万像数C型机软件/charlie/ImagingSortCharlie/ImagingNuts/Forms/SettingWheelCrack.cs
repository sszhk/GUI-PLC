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
  public partial class SettingWheelCrack : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.CalcWheelCrack filter = null;
    public SettingWheelCrack(CalcWheelCrack fil)
    {
      filter = fil;
      View = fil.PublicOptions.View;
      InitializeComponent();
    }
     
    private void SettingWheelCrack_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    public UserControl GetControl()
    {
      return this;
    }
    public void OnOK()
    {
      filter.Name = tbName.Text;
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
    }
    public void Default()
    {

    }
  }
}
