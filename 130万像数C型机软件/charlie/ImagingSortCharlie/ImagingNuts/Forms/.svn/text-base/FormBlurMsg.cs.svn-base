using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Forms
{
  public partial class FormBlurMsg : Form
  {
    public FormBlurMsg()
    {
      InitializeComponent();
      Prompt = GD.GetString("22001")/*"故障"*/;
    }
    public string Prompt/*"故障"*/;
    Panel plBlur = new Panel();
    Bitmap bmp;
    private void darken_image()
    {
      if (bmp == null)
        return;
      Rectangle rc = new Rectangle(new Point(0, 0), bmp.Size);
      BitmapData bd = bmp.LockBits(rc, ImageLockMode.WriteOnly, bmp.PixelFormat);
      int w = bmp.Width;
      int h = bmp.Height;
      int bpp = 4; // 32bit
      unsafe
      {
        byte* data = (byte*)bd.Scan0.ToPointer();
        for (int i = 0; i < h; i++)
        {
          for (int j = 0; j < w; j++)
          {
            int value = 0;
            for (int k = 0; k < 3; k++)
            {
              value += (int)data[j * bpp + k];
              //               if (data[j * bpp + k] >= 50)
              //                 data[j * bpp + k] -= 50;
              //               else
              //                 data[j * bpp + k] = 0;
            }
            value /= 3;
            if (value < 50)
              value = 0;
            else
              value -= 50;
            for (int k = 0; k < 3; k++)
            {
              data[j * bpp + k] = (byte)value;
            }
          }
          data += bd.Stride;
        }
      }
      bmp.UnlockBits(bd);
    }
    private void blur_image()
    {
      if (bmp == null)
        return;
      Rectangle rc = new Rectangle(new Point(0, 0), bmp.Size);
      BitmapData bd = bmp.LockBits(rc, ImageLockMode.WriteOnly, bmp.PixelFormat);
      int w = bmp.Width;
      int h = bmp.Height;
      int bpp = 4; // 32bit
      float[,] gaussianBlur2 = new float[3, 3] { 
        { 1, 2, 1 }, 
        { 2, 4, 2 }, 
        { 1, 2, 1 } };
      float[,] gaussianBlur = new float[3, 3]{
        {0.045f, 0.122f, 0.045f}, 
        {0.122f, 0.332f, 0.122f}, 
        {0.045f, 0.122f, 0.045f}};
      float[,] averBlur = new float[3, 3]{
        {0.111f, 0.111f, 0.111f}, 
        {0.111f, 0.111f, 0.111f}, 
        {0.111f, 0.111f, 0.111f}};
      float[,] kernel = averBlur;

      unsafe
      {
        byte* data = (byte*)bd.Scan0.ToPointer();
        //         for (int y = 1; y < h-1; y++)
        //         {
        //           for (int x = 1; x < w-1; x++)
        for (int y = 0; y < h; y++)
        {
          for (int x = 0; x < w; x++)
          {
            /*
            (x-1,y-1)       *       kernel_value[row0][col0]
            (x  ,y-1)       *       kernel_value[row0][col1]
            (x+1,y-1)       *       kernel_value[row0][col2]
            (x-1,y  )       *       kernel_value[row1][col0]
            (x  ,y  )       *       kernel_value[row1][col1]
            (x+1,y  )       *       kernel_value[row1][col2]
            (x-1,y+1)       *       kernel_value[row2][col0]
            (x  ,y+1)       *       kernel_value[row2][col1]
            (x+1,y+1)       *       kernel_value[row2][col2]
             */
            float r = 0, g = 0, b = 0;
            r = data[x * bpp + y * bd.Stride + 0];
            g = data[x * bpp + y * bd.Stride + 1];
            b = data[x * bpp + y * bd.Stride + 2];

            //             for (int m = -1; m < 2; m++)
            //             {
            //               for (int n = -1; n < 2; n++)
            //               {
            //                 r += kernel[m + 1, n + 1] * data[(x + n) * bpp + (y + m) * bd.Stride + 0];
            //                 g += kernel[m + 1, n + 1] * data[(x + n) * bpp + (y + m) * bd.Stride + 1];
            //                 b += kernel[m + 1, n + 1] * data[(x + n) * bpp + (y + m) * bd.Stride + 2];
            //               }
            //             }
            //             r = Math.Max(r, 0);
            //             r = Math.Min(r, 255);
            //             g = Math.Max(g, 0);
            //             g = Math.Min(g, 255);
            //             b = Math.Max(b, 0);
            //             b = Math.Min(b, 255);

            r = g = b = (r + g + b) / 3 / 2;

            data[x * bpp + y * bd.Stride + 0] = (byte)r;
            data[x * bpp + y * bd.Stride + 1] = (byte)g;
            data[x * bpp + y * bd.Stride + 2] = (byte)b;
          }
          //data += bd.Stride;
        }
      }
      bmp.UnlockBits(bd);
    }
    Timer tm = new Timer();
    bool toggle = false;
    FormDarken darken = null;
    private void FormBlurMsg_Load(object sender, EventArgs e)
    {
      toggle = false;
      tm.Interval = 500;
      tm.Tick += new EventHandler(tm_Tick);
      tm.Start();
      lbPrompt.Text = Prompt;
      string ItemTime = "";
      if (Settings.Lang == 1033)
      {
        ItemTime = new string(DateTime.Now.GetDateTimeFormats('r')[0].ToString().ToCharArray(), 5, 20);
      }
      else
        ItemTime = DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("t");
      lbTime.Text = GD.GetString("22002")/*"故障时间："*/ + ItemTime;   
      if (Owner == null)
        return;
      Owner.BringToFront();
      Owner.TopMost = true;
      Owner.Refresh();
      darken = new FormDarken();
      Rectangle rc;
      rc = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
      darken.Location = new System.Drawing.Point(0, 0);
      darken.Size = rc.Size;
      darken.Show(Owner);
      darken.Enabled = false;
      Application.DoEvents();
// 
//       plBlur = new Panel();
//       Rectangle rc = Owner.DisplayRectangle;
//       int w = Owner.Width;
//       int h = Owner.Height;
//       //       bmp = new Bitmap(w, h);
//       //       using(Graphics g=Graphics.FromImage(bmp))
//       //       {
//       //         g.CopyFromScreen(rc.Location, new Point(0, 0), new Size(w, h));
//       //       }
//       CopyScreenFromForm();

      //darken_image();
//       blur_image();
//       plBlur.BackgroundImage = bmp;
//       plBlur.BackgroundImageLayout = ImageLayout.Center;
//       plBlur.Size = new Size(w, h);
//       Owner.Controls.Add(plBlur);
//       plBlur.BringToFront();
    }

    void tm_Tick(object sender, EventArgs e)
    {
      toggle = !toggle;
      pictureBox1.Visible = toggle;
      lbPrompt.Visible = toggle;
      //this.Visible = toggle;
    }

    private void FormBlurMsg_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Owner == null /*|| bmp == null*/)
        return;
      //Owner.Controls.Remove(plBlur);
      //bmp.Dispose();
      //plBlur.Dispose();
      if (darken != null)
        darken.Dispose();
      Owner.TopMost = false;
    }
    private void CopyScreenFromForm()
    {
      if (this.Owner == null)
        return;
      //
      // Capture snapshot of the form...
      if (Owner.IsHandleCreated)
      {
        //
        // Get DC of the form...
        IntPtr srcDc = GetDC(Owner.Handle);

        //
        // Create bitmap to store image of form...
        bmp = new Bitmap(Owner.ClientRectangle.Width, Owner.ClientRectangle.Height);

        //
        // Create a GDI+ context from the created bitmap...
        using (Graphics g = Graphics.FromImage(bmp))
        {
          //
          // Copy image of form into bitmap...
          IntPtr bmpDc = g.GetHdc();
          BitBlt(bmpDc, 0, 0, bmp.Width, bmp.Height, srcDc, 0, 0, 0x00CC0020 /* SRCCOPY */);

          //
          // Release resources...
          ReleaseDC(Owner.Handle, srcDc);
          g.ReleaseHdc(bmpDc);

          //
          // Blur...
          //Smooth(bmp, 1); //<-- uncomment this if you want a blurred effect
          //Grayscale(bmp);

          //
          // Apply translucent overlay...
          //g.FillRectangle(fillBrush, 0, 0, bmp.Width, bmp.Height);
        }//using

        //
        // Store bitmap so that it can be painted by asyncPanel...
        //background = bmp;

      }

    }


    #region Win32 Interop Methods
    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(
        IntPtr hdcDest, // handle to destination DC 
        int nXDest,     // x-coord of destination upper-left corner 
        int nYDest,     // y-coord of destination upper-left corner 
        int nWidth,     // width of destination rectangle 
        int nHeight,    // height of destination rectangle 
        IntPtr hdcSrc,  // handle to source DC 
        int nXSrc,      // x-coordinate of source upper-left corner 
        int nYSrc,      // y-coordinate of source upper-left corner 
        Int32 dwRop     // raster operation code 
    );

    [DllImport("user32.dll")]
    private extern static IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    #endregion

  }
}
