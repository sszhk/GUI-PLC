using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagingSortCharlie.Forms
{
  public partial class FormMessage : Form
  {
    public enum MessageType
    {
      OK,
      Setting,
      Info,
      Warning,
      Error
    }
    public FormMessage(string cap,string title,MessageType i)
    {
      InitializeComponent();
      lbCap.Text = cap;
      this.Text = title;
      pictureBox2.Image = get_picture(i);
    }
    private Image get_picture(MessageType mt)
    {
      Image img;
      switch (mt)
      {
        case MessageType.OK:
          img = Properties.Resources.shadow_ok;
          break;

        case MessageType.Setting:
          img = Properties.Resources.shadow_process;
          break;
        case MessageType.Info:
          img = Properties.Resources.shadow_info;
          break;
        case MessageType.Warning:
          img = Properties.Resources.shadow_warning;
          break;
        case MessageType.Error:
          img = Properties.Resources.shadow_block;
          break;
        default:
          img = Properties.Resources.shadow_info;
          break;
      }
      return img;
    }
    private void btnOk_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }
  }
}
