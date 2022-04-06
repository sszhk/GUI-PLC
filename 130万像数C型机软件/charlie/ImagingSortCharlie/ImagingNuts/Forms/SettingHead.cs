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
  public partial class SettingHead : UserControl, ISetting
  {
    VIEW View = VIEW.NONE;
    Data.Filters.CalcHead filter = null;
    int CorrectionCount = 0;
//     float mm_per_px = 0;
//     float ThoseWeCantSee = 0;
    float mm_per_px_w = 0;
    float mm_per_px_d = 0;
    const int CORRECTIONCOUNT = 1;

    public SettingHead(CalcHead fil)
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
      float.TryParse(tbMaxWidth.Text, out filter.HeadInputValues.ExpMaxWidth);
      float.TryParse(tbMinWidth.Text, out filter.HeadInputValues.ExpMinWidth);
      float.TryParse(tbMaxDepth.Text, out filter.HeadInputValues.ExpMaxDepth);
      float.TryParse(tbMinDepth.Text, out filter.HeadInputValues.ExpMinDepth);
      float.TryParse(tbWidthCorrection.Text, out filter.HeadInputValues.WidthCorrection);
      float.TryParse(tbDepthCorrection.Text, out filter.HeadInputValues.DepthCorrection);
      filter.HeadInputValues.HeadWidth = cbWidth.Checked;
      filter.HeadInputValues.Depth = cbDepth.Checked;
    }
    public void OnCancel()
    {
      ReadData();
    }
    public int GetCorrectionCount()
    {
      return CorrectionCount;
    }
    public bool OnCorrection()
    {
      if (CorrectionCount == 0)
      {
        CorrectionCount = CORRECTIONCOUNT;
//         mm_per_px = 0;
//         ThoseWeCantSee = 0;
        mm_per_px_d = 0;
        mm_per_px_w = 0;
      }
      this.Correction();
      filter.Apply(null);
      if (!filter.Result || filter.HeadReport.width == -1 ||
        filter.HeadReport.depth == -1)
      {
        return false;
      }
      mm_per_px_w += filter.HeadInputValues.WidthCorrection / filter.HeadReport.width;
      mm_per_px_d += filter.HeadInputValues.DepthCorrection / filter.HeadReport.depth;
//       mm_per_px += filter.HeadInputValues.WidthCorrection / filter.HeadReport.width;
//       ThoseWeCantSee += CalcThoseWeCantSee(filter.HeadReport.depth, filter.HeadReport.width);
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.HeadInputValues.mm_per_px_w = mm_per_px_w / CORRECTIONCOUNT;
        filter.HeadInputValues.mm_per_px_d = mm_per_px_d / CORRECTIONCOUNT;
//         filter.HeadInputValues.mm_per_px = mm_per_px / CORRECTIONCOUNT;
//         filter.HeadInputValues.ThoseWeCantSee = ThoseWeCantSee / CORRECTIONCOUNT;
      }
      lbl_ratio_w.Text = filter.HeadInputValues.mm_per_px_w.ToString("0.000mm/pix");
      lbl_ratio_h.Text = filter.HeadInputValues.mm_per_px_d.ToString("0.000mm/pix");
      GD.PreviewView(View);
      return true;
    }
    private float CalcThoseWeCantSee(float headdepth, float headwidth)
    {
      float l = headdepth;
      float ratio = filter.HeadInputValues.WidthCorrection / headwidth;
      float mm = l * ratio;
      float a = filter.HeadInputValues.DepthCorrection - mm;
      return a;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMaxWidth.Text = filter.HeadInputValues.ExpMaxWidth.ToString();
      tbMinWidth.Text = filter.HeadInputValues.ExpMinWidth.ToString();
      tbMaxDepth.Text = filter.HeadInputValues.ExpMaxDepth.ToString();
      tbMinDepth.Text = filter.HeadInputValues.ExpMinDepth.ToString();
      tbWidthCorrection.Text = filter.HeadInputValues.WidthCorrection.ToString();
      tbDepthCorrection.Text = filter.HeadInputValues.DepthCorrection.ToString();
      cbDepth.Checked = filter.HeadInputValues.Depth;
      cbWidth.Checked = filter.HeadInputValues.HeadWidth;
    }

    private void SettingHead_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    public void Correction()
    {
      float.TryParse(tbWidthCorrection.Text, out filter.HeadInputValues.WidthCorrection);
      float.TryParse(tbDepthCorrection.Text, out filter.HeadInputValues.DepthCorrection);
    }
    public bool IsCorrectionValueChanged()
    {
      return tbWidthCorrection.Text != filter.HeadInputValues.WidthCorrection.ToString();
    }
    private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (Utils.TextboxKeyFilter.IsNumber(e.KeyChar))
        e.Handled = true;
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
