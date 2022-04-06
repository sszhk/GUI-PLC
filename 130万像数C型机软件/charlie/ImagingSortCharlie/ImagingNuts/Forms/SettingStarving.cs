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
  public partial class SettingStarving : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.CalcStarving filter = null;
    public SettingStarving(CalcStarving fil)
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
      filter.InputValues.Starving = cbStarving.Checked;
      float.TryParse(tb_min_starving.Text, out filter.InputValues.ExpMinStarving);
      float.TryParse(tb_max_starving.Text, out filter.InputValues.ExpMaxStarving);
      filter.StarvingOptions.MinStarving = (int)filter.InputValues.ExpMinStarving;
      filter.StarvingOptions.MaxStarving = (int)filter.InputValues.ExpMaxStarving;
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
      tb_min_starving.Text = filter.InputValues.ExpMinStarving.ToString();
      tb_max_starving.Text = filter.InputValues.ExpMaxStarving.ToString();
      rbWhiteHead.Checked = filter.StarvingOptions.IsWhite;
      rbBlackHead.Checked = !filter.StarvingOptions.IsWhite;
      cbStarving.Checked = filter.InputValues.Starving;
    }

    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }

    private void SettingStarving_Load(object sender, EventArgs e)
    {
      ReadData();
    }


    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.StarvingOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }

  }
}
