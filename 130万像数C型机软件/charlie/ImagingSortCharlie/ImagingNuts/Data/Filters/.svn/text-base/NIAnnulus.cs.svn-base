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
  public struct NIAnnulus
  {
    public NIPoint Center;
    public int InnerRadius;
    public int OuterRadius;
    public double StartAngle;
    public double EndAngle;

    public void Initialize()
    {
      this.Center = new NIPoint(GD.WIDTH_IMAGE/2,GD.HEIGHT_IMAGE/2);
      this.StartAngle = 0;
      this.EndAngle = 360;
      if (GD.WIDTH_IMAGE == 640)
      {
        this.InnerRadius = 50;
        this.OuterRadius = 100;
      }
      else
      {
        this.InnerRadius = 100;
        this.OuterRadius = 200;

      }
      
    }
  }

  [Serializable]
  public enum AnnulusDirection
  {
    InnerToOuter = 0,
    OuterToInner
  }

}
