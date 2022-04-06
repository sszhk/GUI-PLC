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
  public partial class SettingThreads : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    CalcThreads filter = null;
    int CorrectionCount = 0;
    float mm_per_px_pitch = 0;
    const int CORRECTIONCOUNT = 1;
    public SettingThreads(CalcThreads fil)
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
      filter.InputValues.IsTeethsCount = cbTeethsCount.Checked;
      float.TryParse(tb_threads_min_count.Text, out filter.InputValues.threads_min_count);
      float.TryParse(tb_threads_max_count.Text, out filter.InputValues.threads_max_count);
      float.TryParse(tb_min_pitch.Text, out filter.InputValues.exp_min_pitch);
      float.TryParse(tb_max_pitch.Text, out filter.InputValues.exp_max_pitch);
      float.TryParse(tb_min_area.Text, out filter.ThreadsOptions.MinArea);
      float.TryParse(tb_max_area.Text, out filter.ThreadsOptions.MaxArea);
      filter.InputValues.is_pitch = cb_pitch.Checked;
    }
    public void OnCancel()
    {
      ReadData();
    }
    public int GetCorrectionCount()
    {
      return CorrectionCount;
    }
    public void Correction()
    {
      float.TryParse(tb_pitch_correction.Text, out filter.InputValues.pitch_correction);
    }
    public bool OnCorrection()
    {
      if (CorrectionCount == 0)
      {
        CorrectionCount = CORRECTIONCOUNT;
        mm_per_px_pitch = 0;
      }
      this.Correction();
      filter.Apply(null);
      if (!filter.Result || filter.ThreadsReport.pitch_max == 0)
      {
        return false;
      }
      mm_per_px_pitch += filter.InputValues.pitch_correction / filter.ThreadsReport.pitch_max;
  
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.InputValues.mm_per_px_pitch = mm_per_px_pitch / CORRECTIONCOUNT;
        lbl_ratio.Text = filter.InputValues.mm_per_px_pitch.ToString("0.000mm/pix");
      }
      GD.PreviewView(View);
      return true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tb_threads_min_count.Text = filter.InputValues.threads_min_count.ToString();
      tb_threads_max_count.Text = filter.InputValues.threads_max_count.ToString();
      cbTeethsCount.Checked = filter.InputValues.IsTeethsCount;
      rbWhiteHead.Checked = filter.ThreadsOptions.IsWhite;
      rbBlackHead.Checked = !filter.ThreadsOptions.IsWhite;
      tb_pitch_correction.Text= filter.InputValues.pitch_correction.ToString();
      tb_min_area.Text = filter.ThreadsOptions.MinArea.ToString();
      tb_max_area.Text = filter.ThreadsOptions.MaxArea.ToString();
      tb_min_pitch.Text = filter.InputValues.exp_min_pitch.ToString();
      tb_max_pitch.Text = filter.InputValues.exp_max_pitch.ToString();
      cb_pitch.Checked = filter.InputValues.is_pitch;
    }

    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {
        filter.Default();
        ReadData();
      }
    }


    private void SettingTeethsCount_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.ThreadsOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }


  }
}
