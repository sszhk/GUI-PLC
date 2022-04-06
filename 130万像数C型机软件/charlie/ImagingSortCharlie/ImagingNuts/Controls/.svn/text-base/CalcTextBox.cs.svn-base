using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ImagingSortCharlie
{
  public partial class CalcTextBox : MaskedTextBox
  {
    public CalcTextBox()
    {
      InitializeComponent();

      //       calc.Parent = Forms.FormMain.I;
      //       calc.Owner = Forms.FormMain.I;

      calc.ValueCleared += new SoftKeyboard.ValueChangedEvent(calc_ValueCleared);
      calc.NumberKeyPressed += new KeyPressEventHandler(calc_NumberKeyPressed);
      calc.Finished += new EventHandler(calc_Finished);
      calc.Canceled += new EventHandler(calc_Canceled);

      //old_text = this.Text;
    }

    void calc_Canceled(object sender, EventArgs e)
    {
      Cancel();
    }
    private string old_text = "";
    void Cancel()
    {
      this.Text = old_text;
    }
    void calc_Finished(object sender, EventArgs e)
    {
      //this.Text = calc.Value.ToString();
    }

    void calc_NumberKeyPressed(object sender, KeyPressEventArgs e)
    {
      if( e.KeyChar == (char)Keys.Enter)
      {
        calc.Text = this.Text;
      }
      if (e.KeyChar == 8)
      {
        if (this.Text.Length != 0)
        {
          this.Text = this.Text.Substring(0, Text.Length - 1);
        }
        if (this.Text.Length == 0)
          this.Text = "0";
        //old_text = this.Text;
        return;
      }
      if (this.Text.Length >= this.MaxLength)
        return;

      this.Text += e.KeyChar.ToString();
      //old_text = this.Text;
      //this.Text = this.Text.TrimStart(new char[] { ' ', '0' });
      //this.SelectionStart = this.Text.Length - 1;
    }

    void calc_ValueCleared(object sender, string value)
    {
      old_text = this.Text;
      this.Text = value;
      //old_text = this.Text;
    }

    //     public CalcTextBox(IContainer container, Control parent)
    //     {
    //       container.Add(this);
    // 
    //       InitializeComponent();
    // 
    //       if (parent != null)
    //       {
    //         calc.Visible = false;
    //         parent.Controls.Add(calc);
    //         calc.BringToFront();
    //       }
    //     }
    SoftKeyboard calc = new SoftKeyboard();
    private void LocateCalc()
    {
      Rectangle rc = this.RectangleToScreen(this.ClientRectangle);
      //rc = Parent.RectangleToScreen(rc);

      int l = rc.Left, t = rc.Bottom + 5;
      int r = l + calc.Width, b = t + calc.Height;
      if (b > SystemInformation.VirtualScreen.Bottom)
      {
        t = rc.Top - calc.Height - 5;
      }
      if (r > SystemInformation.VirtualScreen.Right)
      {
        l = rc.Left;
      }
      if (l > SystemInformation.VirtualScreen.Right)
      {
        l = rc.Right - calc.Width;
      }
      if (t < 0)
      {
        t = rc.Bottom + 5;
      }

      calc.Location = new Point(l, t);
    }
    private void CalcTextBox_Enter(object sender, EventArgs e)
    {
      //old_text = this.Text;
    }

    private void CalcTextBox_Leave(object sender, EventArgs e)
    {
      //       this.Text = calc.Value.ToString();
      if (!calc.Focused)
        calc.Visible = false;
      if (this.Text.Length == 0)
        this.Text = old_text;
//       this.Text = old_text;
    }

    private void CalcTextBox_Layout(object sender, LayoutEventArgs e)
    {
      //       LocateCalc();
    }

    private void CalcTextBox_Click(object sender, EventArgs e)
    {
      if (calc.Visible)
      {
        calc.Visible = false;
        //old_text = this.Text;
        if (this.Text.Length == 0)
          this.Text = old_text;
        return;
      }
      //string s = calc.Text;
      //old_text = this.Text;
      //this.Text = s;
      LocateCalc();
      calc.Visible = true;
    }

//     string defaultString = "";
//     /// <summary>
//     /// Gets or sets the background color of the control.
//     /// </summary>
//     /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
//     [DefaultValue(typeof(string), "0"), Category("Appearance"), Description("默认字符串")]
//     public string DefaultString
//     {
//       get { return defaultString; }
//       set
//       {
//         if (!defaultString.Equals(value))
//         {
//           defaultString = value;
//           //OnBackColorChanged(EventArgs.Empty);
//         }
//       }
//     }

    private void CalcTextBox_TextChanged(object sender, EventArgs e)
    {
//       calc.Text = this.Text;
    }

    private void CalcTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      //old_text = this.Text;
    }
    public bool ClearOnActive
    {
      get
      {
        return calc.ClearOnActive;
      }
      set
      {
        calc.ClearOnActive = value;
      }
    }
  }
}
