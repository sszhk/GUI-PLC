using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ImagingSortCharlie.Forms;
namespace ImagingSortCharlie.Utils
{
    public static class MB
    {
        public static bool OKCancel(string cap, string title, FormMessage.MessageType i)
        {
//             return DialogResult.OK ==
//                 MessageBox.Show(cap, title, MessageBoxButtons.OKCancel, i);
          FormMessage frm = new FormMessage(cap, title, i);
          return DialogResult.OK == frm.ShowDialog();
        }

        public static bool OKCancelSnap()
        {
          return OKCancel(GD.GetString("91001"), GD.GetString("91002"), FormMessage.MessageType.Info);
        }
        public static bool OKCancelQ(string cap)
        {
          return OKCancel(cap, GetString("s1002", GD.GetString("91003")), FormMessage.MessageType.Info);
        }

//         public static void Info(string cap, string title, MessageBoxIcon i)
//         {
//             //MessageBox.Show(cap, title, MessageBoxButtons.OK, i);
//           Forms.Message.appear(cap, Forms.Message.MessageType.OK);
//         }

        public static void OKI(string cap)
        {
            //Info(cap, GD.GetString("91004"), MessageBoxIcon.Information);
          Forms.Message.appear(cap, Forms.Message.MessageType.Setting);
        }

        public static void OK(string cap)
        {
            //Info(cap, GD.GetString("91005"), MessageBoxIcon.Information);
          Forms.Message.appear(cap, Forms.Message.MessageType.Info);
        }

        public static bool OKCancelW(string cap, string title)
        {
          return OKCancel(cap, title, FormMessage.MessageType.Warning);
        }

        public static void Error(string cap)
        {
          Forms.Message.appear(cap, Forms.Message.MessageType.Error); 
          //MessageBox.Show(cap, GD.GetString("91006"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string GetString(string key, string def)
        {
            return def;
        }
        public static void WarningDlg(string cap)
        {
          Form frm = null;
          if (Form.ActiveForm != null)
            frm = Form.ActiveForm;
          else
            frm = null;
          if (frm != null && frm.InvokeRequired)
          {
            frm = null;
          }
          MessageBox.Show(frm, cap, GetString("s2000", GD.GetString("91007")), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Warning(string cap)
        {
          Form frm = null;
          if (Form.ActiveForm != null)
            frm = Form.ActiveForm;
          else
            frm = null;
          if( frm != null && frm.InvokeRequired )
          {
            frm = null;
          }
//           MessageBox.Show(frm, cap, GetString("s2000", GD.GetString("91007")), 
//               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          Forms.Message.appear(cap, Forms.Message.MessageType.Warning);
        }

        public static void Correction(string cap, bool isok)
        {
          if (isok)
          {
            Forms.Message.appear(cap, Forms.Message.MessageType.Info);
            // MessageBox.Show(cap, GD.GetString("91008"), MessageBoxButtons.OK,MessageBoxIcon.None);
            return;
          }
          Forms.Message.appear(cap, Forms.Message.MessageType.Error);
          //MessageBox.Show(cap, GD.GetString("91008"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool YesNo(string cap, string title, MessageBoxIcon i)
        {
            return DialogResult.Yes ==
                MessageBox.Show(cap, title, MessageBoxButtons.YesNo, i);
        }

        public static DialogResult YesNoCancelQ(string cap)
        {
            return MessageBox.Show(cap, "", MessageBoxButtons.YesNoCancel, 
                MessageBoxIcon.Question);
        }

        public static bool YesNoQ(string cap)
        {
            return YesNo(cap, GetString("s1002", GD.GetString("91003")), MessageBoxIcon.Question);
        }

        public static bool YesNoW(string cap, string title)
        {
            return YesNo(cap, title, MessageBoxIcon.Exclamation);
        }

    }
}
