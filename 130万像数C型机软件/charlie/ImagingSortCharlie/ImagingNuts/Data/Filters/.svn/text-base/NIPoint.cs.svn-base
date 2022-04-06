using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct NIPointFloat
  {
    public float X;
    public float Y;

    public NIPointFloat(float x, float y) { X = x; Y = y; }
    public override string ToString()
    {
      return string.Format("X={0}, Y={1}", X, Y);
    }

    public void MakePointFromPointfloat(NIPoint pt)
    {
        X = (float)pt.X;
        Y = (float)pt.Y;
    }
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct NIPoint
  {
    public int X;
    public int Y;

    public NIPoint(int _x, int _y) { X = _x; Y = _y; }

    public void MakePointFromPointfloat(NIPointFloat ptf)
    {
        X = (int)ptf.X;
        Y = (int)ptf.Y;
    }
  }
}


