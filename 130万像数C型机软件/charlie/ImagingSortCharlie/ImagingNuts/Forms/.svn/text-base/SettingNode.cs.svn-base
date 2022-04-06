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
  public partial class SettingNode : UserControl, ISetting
  {
    VIEW view = VIEW.NONE;
    CalcNode filter = null;
    public SettingNode(CalcNode fil)
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
      int.TryParse(tbMinNodeCount.Text, out filter.InputValues.ExpMinNodeCount);
      int.TryParse(tbMaxNodeCount.Text, out filter.InputValues.ExpMaxNodeCount);
      float.TryParse(tbMinNodeArea.Text, out filter.InputValues.ExpMinNodeArea);
      float.TryParse(tbMaxNodeArea.Text, out filter.InputValues.ExpMaxNodeArea);
      filter.InputValues.NodeArea = cbNodeArea.Checked;
      filter.InputValues.NodeCount = cbNodesCount.Checked;
      filter.NodeOptions.Similarity = (int)tbSimilarity.Value;
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
      filter.NodeOptions.Correction = true;
      filter.Apply(null);
      filter.NodeOptions.Correction = false;
      bool result = filter.Result;
      GD.PreviewCurrent();
      return result;
    }
    public void ReadData()
    {
      tbName.Text = filter.Name;
      tbMinNodeCount.Text = filter.InputValues.ExpMinNodeCount.ToString();
      tbMaxNodeCount.Text = filter.InputValues.ExpMaxNodeCount.ToString();
      tbMinNodeArea.Text = filter.InputValues.ExpMinNodeArea.ToString();
      tbMaxNodeArea.Text = filter.InputValues.ExpMaxNodeArea.ToString();
      cbNodesCount.Checked = filter.InputValues.NodeCount;
      cbNodeArea.Checked = filter.InputValues.NodeArea;
      tbSimilarity.Value = filter.NodeOptions.Similarity;
      rbWhiteHead.Checked = filter.NodeOptions.IsWhite;
      rbBlackHead.Checked = !filter.NodeOptions.IsWhite;
    }

    private void SettingNode_Load(object sender, EventArgs e)
    {
      ReadData();
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



    private void tbSimilarity_ValueChanged(object sender, EventArgs e)
    {
      filter.NodeOptions.Similarity = (int)tbSimilarity.Value;
      GD.PreviewCurrent();
    }

    private void rbWhiteHead_Click(object sender, EventArgs e)
    {
      filter.NodeOptions.IsWhite = rbWhiteHead.Checked;
      GD.PreviewCurrent();
    }

  }
}
