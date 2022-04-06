using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagingSortCharlie.Data.Filters;

namespace ImagingSortCharlie.Utils
{
  public static class Geo
  {
    public static float Point2Point(NIPointFloat a, NIPointFloat b)
    {
      float d;
      float dx, dy;

      dx = a.X - b.X;
      dy = a.Y - b.Y;
      d = (float)Math.Sqrt(dx*dx + dy*dy);
      return d;
    }

  }
}
