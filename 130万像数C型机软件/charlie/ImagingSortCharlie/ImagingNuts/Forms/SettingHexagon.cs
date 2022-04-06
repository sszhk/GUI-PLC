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
  public partial class SettingHexagon : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    Data.Filters.CalcHexagon filter = null;
    List<IFilter> lst = null;
    int CorrectionCount = 0;
    float mm_per_px_dia = 0;
    float mm_per_px_sub = 0;
    const int CORRECTIONCOUNT = 1;
    public SettingHexagon(CalcHexagon fil)
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
      float.TryParse(tbCrack.Text, out filter.InputValues.ExpCrack);
      filter.HexagonOptions.MaxDiagonal = cbMaxDiagonal.Checked;
      filter.HexagonOptions.MinDiagonal = cbMinDiagonal.Checked;
      filter.HexagonOptions.MaxSubtense = cbMaxSubtense.Checked;
      filter.HexagonOptions.MinSubtense = cbMinSubtense.Checked;
      filter.HexagonOptions.Concent = cbConcent.Checked;
      filter.HexagonOptions.Crack = cbCrack.Checked;
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
        mm_per_px_sub = 0;
        mm_per_px_dia = 0;
      }
      this.Correction();
      filter.Apply(null);
      if (!filter.Result || filter.HexagonReport.maxSubtense == 0)
      {
        return false;
      }
      mm_per_px_dia += filter.InputValues.MaxDiagonalCorrection / filter.HexagonReport.maxDiagonal;
      mm_per_px_sub += filter.InputValues.MaxSubtenseCorrection / filter.HexagonReport.maxSubtense;
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.InputValues.mm_per_px_dia = mm_per_px_dia / CORRECTIONCOUNT;
        filter.InputValues.mm_per_px_sub = mm_per_px_sub / CORRECTIONCOUNT;
        lbl_ratio_dia.Text = filter.InputValues.mm_per_px_dia.ToString("0.000mm/pix");
        lbl_ratio_sub.Text = filter.InputValues.mm_per_px_sub.ToString("0.000mm/pix");
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
      tbMaxDiagonalCorrection.Text = filter.InputValues.MaxDiagonalCorrection.ToString();
      tbMaxSubtenseCorrection.Text = filter.InputValues.MaxSubtenseCorrection.ToString();
      tbCrack.Text = filter.InputValues.ExpCrack.ToString();
      rbWhiteHead.Checked = filter.HexagonOptions.IsWhite;
      rbBlackHead.Checked = !filter.HexagonOptions.IsWhite;
      cbMaxSubtense.Checked = filter.HexagonOptions.MaxSubtense;
      cbMinSubtense.Checked = filter.HexagonOptions.MinSubtense;
      cbMaxDiagonal.Checked = filter.HexagonOptions.MaxDiagonal;
      cbMinDiagonal.Checked = filter.HexagonOptions.MinDiagonal;
      cbConcent.Checked = filter.HexagonOptions.Concent;
      cbCrack.Checked = filter.HexagonOptions.Crack;
      cb_open.Checked = filter.HexagonOptions.Open;
      lst = new List<IFilter>();
      cbConcentric.DataSource = null;
//       foreach (IFilter f in GD.SettingsForView(view))
//       {
//         if (f == filter)
//         {
//           break;
//         }
//         if (f is CalcHexagon ||
//           f is CalcDiameter)
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
      float.TryParse(tbMaxSubtenseCorrection.Text, out filter.InputValues.MaxSubtenseCorrection);
      float.TryParse(tbMaxDiagonalCorrection.Text, out filter.InputValues.MaxDiagonalCorrection);
    }

    private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
    {

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

    private void btnNothing_Click(object sender, EventArgs e)
    {
      cbConcentric.SelectedIndex = -1;
      filter.InputValues.SelectedConcentObject = "";
      cbConcentric.SelectedItem = null;
    }

    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.HexagonOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }

    private void SettingHexagon_Load(object sender, EventArgs e)
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

    private void cb_open_CheckedChanged(object sender, EventArgs e)
    {
      filter.HexagonOptions.Open = cb_open.Checked;
      GD.PreviewCurrent();
    }

  }
}
