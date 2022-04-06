using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ConfigApp
{
  static class Program
  {
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (!Configure.load())
      {
        MessageBox.Show("配置文件读取失败，请删除Configure.xml文件后运行本软件!");
        return;
      }
      Application.Run(new ConfigApp.Windows.form_main());
    }
  }
}
