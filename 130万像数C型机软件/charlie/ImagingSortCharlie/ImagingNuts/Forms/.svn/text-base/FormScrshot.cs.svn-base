using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Settings;
using System.IO;

namespace ImagingSortCharlie.Forms
{
  public partial class FormScrshot : Form
  {
    public FormScrshot(string path)
    {
      InitializeComponent();
      SelectedPath = path;
    }

    public string SelectedPath
    {
      get
      {
        return tbSnapPath.Text;
      }
      set
      {
        tbSnapPath.Text = value;
      }
    }

    private void ChangeSnap()
    {
      if (tbSnapPath.Text != Settings.PS.ScreenshotName)
      {
        try
        {
          string shotsnap1 = string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath, Settings.PS.ScreenshotName,
            GD.extension_orig);
          System.IO.FileInfo f = new FileInfo(shotsnap1);
          f.MoveTo(string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath,
            tbSnapPath.Text, GD.extension_orig));
          string shotsnap2 = string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath,
            Settings.PS.ScreenshotName, GD.extension_screen);
          System.IO.FileInfo f1 = new FileInfo(shotsnap2);
          f1.MoveTo(string.Format("{0}\\{1}{2}", Settings.PS.ScreenshotPath,
            tbSnapPath.Text, GD.extension_screen));
          Settings.PS.ScreenshotName = tbSnapPath.Text;
        }
        catch (Exception theException)
        {
          String errorMessage;
          errorMessage = "Error: ";
          errorMessage = String.Concat(errorMessage, theException.Message);
          errorMessage = String.Concat(errorMessage, " Line: ");
          errorMessage = String.Concat(errorMessage, theException.Source);
          MessageBox.Show(errorMessage, "Error");
          tbSnapPath.Text = Settings.PS.ScreenshotName;
          return;
        }
      }
    }
    private void btnOK_Click(object sender, EventArgs e)
    {
      ChangeSnap();
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }
  }
}
