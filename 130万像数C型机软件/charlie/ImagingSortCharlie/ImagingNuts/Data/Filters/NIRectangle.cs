using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct NIRect
  {
    public int Top;
    public int Left;
    public int Height;
    public int Width;

    public bool Equals(NIRect rc)
    {
      return (Left == rc.Left) && 
        (Top == rc.Top) &&
        (Width == rc.Width) && 
        (Height == rc.Height);
    }
    public void Initialize()
    {
      Left = GD.WIDTH_IMAGE/2;
      Top = GD.HEIGHT_IMAGE/2;
      if (GD.WIDTH_IMAGE == 480)
      {
        Width = 200;
        Height = 100;
      }
      else
      {
        Width = 400;
        Height = 200;
      }
    }

    public void Initialize(int l, int t, int w, int h)
    {
      Left = l;
      Top = t;
      Width = w;
      Height = h;
    }

  }
}
