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
  public partial class SettingSquare : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    Data.Filters.CalcSquare filter = null;
    List<IFilter> lst = null;
    int CorrectionCount = 0;
    float mm_per_px_oppo_flat = 0;
    float mm_per_px_oppo_angle = 0;
    const int CORRECTIONCOUNT = 1;
    public SettingSquare(CalcSquare fil)
    {
      filter = fil;
      view = fil.PublicOptions.View;
      InitializeComponent();
    }

    public UserControl GetControl()
    {
      return this;
    }
    public void OnOK()
    {
      filter.Name = tbName.Text;
      float.TryParse(tbMaxMaxDiagonal.Text, out filter.InputValues.ExpMaxMaxDiagonal);
      float.TryParse(tbMaxMinDiagonal.Text, out filter.InputValues.ExpMaxMinDiagonal);
      float.TryParse(tbMinMinDiagonal.Text, out filter.InputValues.ExpMinMinDiagonal);
      float.TryParse(tbMinMaxDiagonal.Text, out filter.InputValues.ExpMinMaxDiagonal);
      float.TryParse(tbMaxMaxSubtense.Text, out filter.InputValues.ExpMaxMaxSubtense);
      float.TryParse(tbMaxMinSubtense.Text, out filter.InputValues.ExpMaxMinSubtense);
      float.TryParse(tbMinMaxSubtense.Text, out filter.InputValues.ExpMinMaxSubtense);
      float.TryParse(tbMinMinSubtense.Text, out filter.InputValues.ExpMinMinSubtense);
      float.TryParse(tbConcent.Text, out filter.InputValues.ExpConcent);
      float.TryParse(tbMaxDiagonalCorrection.Text, out filter.InputValues.MaxSubtenseCorrection);
      float.TryParse(tb_max_oppo_angle_correction.Text, out filter.InputValues.max_oppo_angle_correction);
      filter.SquareOptions.MaxDiagonal = cbMaxDiagonal.Checked;
      filter.SquareOptions.MinDiagonal = cbMinDiagonal.Checked;
      filter.SquareOptions.MaxSubtense = cbMaxSubtense.Checked;
      filter.SquareOptions.MinSubtense = cbMinSubtense.Checked;
      filter.InputValues.Concent = cbConcent.Checked;
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
        mm_per_px_oppo_flat = 0;
        mm_per_px_oppo_angle = 0;
      }
      this.Correction();
      filter.Apply(null);
      if (!filter.Result || filter.SquareReport.maxSubtense == 0
        ||filter.SquareReport.maxDiagonal==0)
      {
        return false;
      }
      mm_per_px_oppo_flat += filter.InputValues.MaxSubtenseCorrection / filter.SquareReport.maxSubtense;
      mm_per_px_oppo_angle += filter.InputValues.max_oppo_angle_correction / filter.SquareReport.maxDiagonal;
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.InputValues.mm_per_px_flat = mm_per_px_oppo_flat / CORRECTIONCOUNT;
        filter.InputValues.mm_per_px_angle = mm_per_px_oppo_angle / CORRECTIONCOUNT;
        lb_ratio_dig.Text = filter.InputValues.mm_per_px_angle.ToString("0.000mm/pix");
        lb_ratio_sub.Text = filter.InputValues.mm_per_px_flat.ToString("0.000mm/pix");

      }
      GD.PreviewView(view);
      return true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMaxMaxDiagonal.Text = filter.InputValues.ExpMaxMaxDiagonal.ToString();
      tbMaxMinDiagonal.Text = filter.InputValues.ExpMaxMinDiagonal.ToString();
      tbMinMaxDiagonal.Text = filter.InputValues.ExpMinMaxDiagonal.ToString();
      tbMinMinDiagonal.Text = filter.InputValues.ExpMinMinDiagonal.ToString();
      tbMaxMaxSubtense.Text = filter.InputValues.ExpMaxMaxSubtense.ToString();
      tbMaxMinSubtense.Text = filter.InputValues.ExpMaxMinSubtense.ToString();
      tbMinMaxSubtense.Text = filter.InputValues.ExpMinMaxSubtense.ToString();
      tbMinMinSubtense.Text = filter.InputValues.ExpMinMinSubtense.ToString();
      tbConcent.Text = filter.InputValues.ExpConcent.ToString();
      tbMaxDiagonalCorrection.Text = filter.InputValues.MaxSubtenseCorrection.ToString();
      tb_max_oppo_angle_correction.Text = filter.InputValues.max_oppo_angle_correction.ToString();
      rbWhiteHead.Checked = filter.SquareOptions.IsWhite;
      rbBlackHead.Checked = !filter.SquareOptions.IsWhite;
      cbMaxSubtense.Checked = filter.SquareOptions.MaxSubtense;
      cbMinSubtense.Checked = filter.SquareOptions.MinSubtense;
      cbMaxDiagonal.Checked = filter.SquareOptions.MaxDiagonal;
      cbMinDiagonal.Checked = filter.SquareOptions.MinDiagonal;
      cbConcent.Checked = filter.InputValues.Concent;
      lst = new List<IFilter>();
      cbConcentric.DataSource = null;
//       foreach (IFilter f in GD.SettingsForView(view))
//       {
//         if (f == filter)
//         {
//           break;
//         }
//         if (f is CalcHexagon ||
//           f is CalcDiameter ||
//           f is CalcSquare)
//         {
//           lst.Add(f);
//         }
//       }
      lst = GD.SettingsForView(view).ConcentricityList(filter);
      if (lst != null && lst.Count != 0)
      {
        cbConcentric.DataSource = lst;
        cbConcentric.DisplayMember = "Name";
        IFilter fil = GD.SettingsForView(view).Search(filter.InputValues.SelectedConcentObject);
        if (fil != null)
        {
          cbConcentric.SelectedItem = fil;
        }
        else
        {
          cbConcentric.SelectedIndex = -1;
          filter.InputValues.SelectedConcentObject = "";
        }
      }
    }
    public void Correction()
    {
      float.TryParse(tbMaxDiagonalCorrection.Text, out filter.InputValues.MaxSubtenseCorrection);
      float.TryParse(tb_max_oppo_angle_correction.Text, out filter.InputValues.max_oppo_angle_correction);
    }

    private void Test()
    {
      if (GD.IsSnapping)
        return;
      GD.PreviewView(view);
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
      filter.SquareOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }

    private void SettingSquare_Load(object sender, EventArgs e)
    {
      filter.InputValues.SelectedConcentObject = null;
      ReadData();
    }

    private void cbConcentric_SelectionChangeCommitted(object sender, EventArgs e)
    {
      if (cbConcentric.SelectedItem == null)
      {
        filter.InputValues.SelectedConcentObject = "";
        return;
      }
      IFilter fil = (IFilter)cbConcentric.SelectedItem;
      filter.InputValues.SelectedConcentObject = fil.ID;
      GD.PreviewCurrent();
    }

    private void btnNothing_Click(object sender, EventArgs e)
    {
      cbConcentric.SelectedIndex = -1;
      filter.InputValues.SelectedConcentObject = "";
      cbConcentric.SelectedItem = null;
    }

    
  }
}
