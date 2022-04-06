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
  public partial class SettingThreadLocating : UserControl, ISetting
  {
    public SettingThreadLocating()
    {
      InitializeComponent();
    }

    VIEW View = VIEW.NONE;
    CalcThreadLocating filter = null;
    const int CORRECTIONCOUNT = 1;
    public SettingThreadLocating(CalcThreadLocating fil)
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
      rbWhiteHead.Checked = filter.ThreadLocatingOptions.IsWhite;
      rbBlackHead.Checked = !filter.ThreadLocatingOptions.IsWhite;
    }

    private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
    {

    }
    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }

    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.ThreadLocatingOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }

    private void SettingThreadLocating_Load(object sender, EventArgs e)
    {
      ReadData();
    }

  }
}
