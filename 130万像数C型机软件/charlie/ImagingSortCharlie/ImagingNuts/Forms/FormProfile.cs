using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace ImagingSortCharlie.Forms
{
  public partial class FormProfile : Form
  {
    ImagingSortCharlie.Forms.FormToolKit frmToolKit = new ImagingSortCharlie.Forms.FormToolKit();

    object fis;
    string current;
    public FormProfile(object input, string c)
    {
      InitializeComponent();
      fis = input;
      current = c;
      InitProfile();
    }

    public bool CopyFrom
    {
      get
      {
        return cbCreate.Checked;
      }
      set
      {
        cbCreate.Checked = value;
      }
    }
    public bool ApplyNow
    {
      get
      {
        return cbEnable.Checked;
      }
      set
      {
        cbEnable.Checked = value;
      }
    }
    public string ConfigName
    {
      get { return tbProfile.Text; }
      set { tbProfile.Text = value; }
    }
    public string SelectedConfig
    {
      get { return current; }
    }
    public void InitProfile()
    {
      //string dir = GD.AppPath();
      try
      {
        //DirectoryInfo di = new DirectoryInfo(dir);
        //FileInfo[] f = di.GetFiles("*.cfg");
        cbProfile.DataSource = fis;
        cbProfile.DisplayMember = "Name";
        cbProfile.ValueMember = "Name";

        cbProfile.SelectedValue = current;
      }
      catch
      {
      }
      //       List<string> profile = frmToolKit.RefreshProfile(System.Environment.CurrentDirectory);
      //       cbProfile.DataSource = profile;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (cbProfile.DataSource != null)
      {
        //FileInfo[] fi = (FileInfo[])cbProfile.DataSource;
        List<string> ls = (List<string>)cbProfile.DataSource;
        string cfg = tbProfile.Text ;
        foreach (string s in ls)
        {
          if (s.Equals(cfg, StringComparison.OrdinalIgnoreCase))
          {
            Utils.MB.Error(
              GD.GetString("91034"));
            return;
          }
        }
      }
      // PASSED
      if (cbProfile.SelectedIndex != -1)
      {
        string file_path = GD.AppPath();
        current = Path.Combine(file_path, cbProfile.SelectedItem + ".cfg");
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void cbCreate_CheckedChanged(object sender, EventArgs e)
    {
      cbProfile.Enabled = cbCreate.Checked;
    }

    private void FormProfile_Load(object sender, EventArgs e)
    {
      tbProfile.Select(0, 100);
      cbCreate.Checked = true;
      if (cbProfile.SelectedIndex == -1)
      {
        cbCreate.Enabled = false;
        cbCreate.Checked = false;
      }
    }
  }
}
