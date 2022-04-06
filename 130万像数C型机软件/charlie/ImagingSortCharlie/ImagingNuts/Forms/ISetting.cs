using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
  public interface ISetting
  {
    void OnOK();
    void OnCancel();
    bool OnCorrection();
    UserControl GetControl();
    void ReadData();
    int GetCorrectionCount();
    void Default();
  }
