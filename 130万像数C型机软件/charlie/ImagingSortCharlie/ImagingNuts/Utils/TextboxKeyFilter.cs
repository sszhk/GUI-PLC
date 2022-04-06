using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ImagingSortCharlie.Utils
{
  public static class TextboxKeyFilter
  {
      public static bool IsNumber(char k)
      {
          if (!(Char.IsNumber((char)k) || k == '\b' 
              || k == '.' || k == (int)Keys.Delete))
          {
              return true;
          }
          return false;
      }
       
    }
}
