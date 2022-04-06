using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigApp.Windows
{
  public partial class uc_settings : UserControl
  {
    public uc_settings()
    {
      InitializeComponent();
      read();
    }

    void read()
    {
      cb_angle.Checked = Configure.This.Angle;
      cb_area.Checked = Configure.This.Area;
      cb_binarize.Checked = Configure.This.Binarize;
      cb_center_crack.Checked = Configure.This.CenterCrack;
      cb_check_feed.Checked = Configure.This.CheckFeed;
      cb_clear_plate.Checked = Configure.This.ClearPlate;
      cb_consecutive_bad_parts.Checked = Configure.This.ConsecutiveBadParts;
      cb_cushion.Checked = Configure.This.Cushion;
      cb_diameter.Checked = Configure.This.Diameter;
      cb_distance.Checked = Configure.This.Distance;
      cb_fill_area.Checked = Configure.This.FillArea;
      cb_head.Checked = Configure.This.Head;
      cb_hexagon.Checked = Configure.This.Hexagon;
      cb_language.Checked = Configure.This.Language;
      cb_locate_teeth.Checked = Configure.This.ThreadLocate;
      cb_marking.Checked = Configure.This.Marking;
      cb_pack.Checked = Configure.This.Pack;
      cb_record.Checked = Configure.This.DataSheet;
      cb_square.Checked = Configure.This.Square;
      cb_starving.Checked = Configure.This.Starving;
      cb_teeth.Checked = Configure.This.Thread;
      cb_teeth_count.Checked = Configure.This.ThreadCount;
      cb_teeth_damage.Checked = Configure.This.ThreadDamage;
      cb_test.Checked = Configure.This.Test;
      cb_weld.Checked = Configure.This.Weld;
      cb_wizard.Checked = Configure.This.Wizard;
    }

    public void save()
    {
      Configure.This.Language = cb_language.Checked;
      Configure.This.Wizard = cb_wizard.Checked;
      Configure.This.DataSheet = cb_record.Checked;
      Configure.This.Test = cb_test.Checked;
      Configure.This.ConsecutiveBadParts = cb_consecutive_bad_parts.Checked;
      Configure.This.Pack = cb_pack.Checked;
      Configure.This.ClearPlate = cb_clear_plate.Checked;
      Configure.This.CheckFeed = cb_check_feed.Checked;
      Configure.This.Binarize = cb_binarize.Checked;
      Configure.This.Distance = cb_distance.Checked;
      Configure.This.Angle = cb_angle.Checked;
      Configure.This.Diameter = cb_diameter.Checked;
      Configure.This.Hexagon = cb_hexagon.Checked;
      Configure.This.Square = cb_square.Checked;
      Configure.This.Weld = cb_weld.Checked;
      Configure.This.Starving = cb_starving.Checked;
      Configure.This.FillArea = cb_fill_area.Checked;
      Configure.This.CenterCrack = cb_center_crack.Checked;
      Configure.This.Marking = cb_marking.Checked;
      Configure.This.Thread = cb_teeth.Checked;
      Configure.This.ThreadLocate = cb_locate_teeth.Checked;
      Configure.This.ThreadDamage = cb_teeth_damage.Checked;
      Configure.This.Cushion = cb_cushion.Checked;
      Configure.This.Area = cb_area.Checked;
      Configure.This.ThreadCount = cb_teeth_count.Checked;
      Configure.This.Head = cb_head.Checked;
    }

  }
}
