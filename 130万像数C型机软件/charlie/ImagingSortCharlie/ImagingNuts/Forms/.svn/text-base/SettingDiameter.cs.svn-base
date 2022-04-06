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
  public partial class SettingDiameter : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    Data.Filters.CalcDiameter filter = null;
    int CorrectionCount = 0;
    float mm_per_px = 0;
    const int CORRECTIONCOUNT = 1;
    List<IFilter> lst = null;
    public SettingDiameter(CalcDiameter fil)
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
      float.TryParse(tbMaxMaxRadius.Text, out filter.InputValues.ExpMaxMaxRadius);
      float.TryParse(tbMaxMinRadius.Text, out filter.InputValues.ExpMaxMinRadius);
      float.TryParse(tbMinMaxRadius.Text, out filter.InputValues.ExpMinMaxRadius);
      float.TryParse(tbMinMinRadius.Text, out filter.InputValues.ExpMinMinRadius);
      float.TryParse(tbRoundness.Text, out filter.InputValues.ExpRoundness);
      float.TryParse(tbConcent.Text, out filter.InputValues.ExpConcent);
      float.TryParse(tbMaxRadiusCorrection.Text, out filter.InputValues.MaxRadiusCorrection);
      float.TryParse(tbCrack.Text, out filter.InputValues.ExpCrack);
      filter.DiameterOptions.MaxRadius = cbExpectedMaxRadius.Checked;
      filter.DiameterOptions.MinRadius = cbExpectedMinRadius.Checked;
      filter.InputValues.Roundness = cbRoundness.Checked;
      filter.InputValues.Concent = cbConcent.Checked;
      filter.DiameterOptions.IsWhite = rbWhiteHead.Checked;
      filter.DiameterOptions.IsCrack = cbCrack.Checked;
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
        mm_per_px = 0;
      }
      this.Correction();
      GD.PreviewCurrent();
      if (!filter.Result || filter.DiameterReport.maxDiameter == 0)
      {
        return false;
      }
      mm_per_px += (filter.InputValues.MaxRadiusCorrection / filter.DiameterReport.maxDiameter);
      CorrectionCount--;
      if (CorrectionCount == 0)
      {
        filter.InputValues.mm_per_px = mm_per_px / CORRECTIONCOUNT;
      }
      lbl_ratio.Text = filter.InputValues.mm_per_px.ToString("0.000mm/pix");
      GD.PreviewView(view);
      return true;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMaxMaxRadius.Text = filter.InputValues.ExpMaxMaxRadius.ToString();
      tbMaxMinRadius.Text = filter.InputValues.ExpMaxMinRadius.ToString();
      tbMinMaxRadius.Text = filter.InputValues.ExpMinMaxRadius.ToString();
      tbMinMinRadius.Text = filter.InputValues.ExpMinMinRadius.ToString();
      tbRoundness.Text = filter.InputValues.ExpRoundness.ToString();
      tbConcent.Text = filter.InputValues.ExpConcent.ToString();
      tbMaxRadiusCorrection.Text = filter.InputValues.MaxRadiusCorrection.ToString();
      tbCrack.Text = filter.InputValues.ExpCrack.ToString();
      cbRoundness.Checked = filter.InputValues.Roundness;
      cbExpectedMaxRadius.Checked = filter.DiameterOptions.MaxRadius;
      cbExpectedMinRadius.Checked = filter.DiameterOptions.MinRadius;
      cbConcent.Checked = filter.InputValues.Concent;
      rbWhiteHead.Checked = filter.DiameterOptions.IsWhite;
      rbBlackHead.Checked = !filter.DiameterOptions.IsWhite;
      cbCrack.Checked = filter.DiameterOptions.IsCrack;
      lst = new List<IFilter>();
      cbConcentric.DataSource = null;
      //filter.InputValues.SelectedConcentObject = filter.ConcentricityID;
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
      float maxradiuscorrectionresult;
      if (float.TryParse(tbMaxRadiusCorrection.Text, out  maxradiuscorrectionresult))
      {
        filter.InputValues.MaxRadiusCorrection = maxradiuscorrectionresult;
      }
    }
    
    public void Default()
    {
      if (Utils.MB.OKCancelQ(GD.GetString("91009")))
      {

        filter.Default();
        ReadData();
      }
    }

    private void SettingDiameter_Load(object sender, EventArgs e)
    {
      ReadData();
    }

    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.DiameterOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
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
//校正要最大外径；
