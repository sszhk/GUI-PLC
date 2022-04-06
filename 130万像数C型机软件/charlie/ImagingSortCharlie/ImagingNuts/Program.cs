using System;
using System.Windows.Forms;
using System.IO;
using ImagingSortCharlie.Forms;
using ImagingSortCharlie.Data.Settings;
using System.Threading;
using System.Diagnostics;
using System.Security.Permissions;
using System.Reflection;

namespace ImagingSortCharlie
{
  static class Program
  {
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // 编译release后，自动生成AssemblyInfo.cs中的revision号码文件
      // 如“2859.revision”，供后面的安装脚本使用
      // 改文件位于C#项目目录，和文件“ImagingSortCharlie.csproj”同一个目录
      if (args.Length == 2)
      {
#if !DEBUG
        if (args[0].Trim().Equals("/rev", StringComparison.OrdinalIgnoreCase))
        {
          make_revision(args[1]);
        }
#endif
        return;
      }

      if (!FirstRun())
      {
        Utils.MB.WarningDlg("程序已经运行，请关闭后重试。");
        return;
      }

      System.IO.Directory.SetCurrentDirectory(
        Path.GetDirectoryName(Application.ExecutablePath));
       
      string file_name = "charlie.log";
      enLog.log_init(file_name);
      enLog.log_set_level(log_level.LV_ALL);
      enLog.info("charlie @ revision {0}", get_build());

      enLog.info("登记DUMP处理");
      reg_crash();
      enLog.info("读取配置");
      if (!Configure.load())
      {
        Utils.MB.WarningDlg("读取配置文件失败，请使用备用文件，我们为由此带来的不便表示歉意，如有疑问请与我们联系");
        return ;
      }
      if (!Settings.load())
        Utils.MB.WarningDlg("读取配置文件失败，将恢复上次运行前备份的配置文件，您有可能需要更改检测工具的设置，我们为由此带来的不便表示歉意，如有疑问请与我们联系");
      else
      {
        if (!Settings.load_ps())
          Utils.MB.WarningDlg("读取配置文件失败，将恢复上次运行前备份的配置文件，您有可能需要更改检测工具的设置，我们为由此带来的不便表示歉意，如有疑问请与我们联系");
      }

      enLog.info("按照配置中的要求设置LOG级别：{0}", Configure.This.LogLevel.ToString());
      enLog.log_set_level(Configure.This.LogLevel);

      enLog.info("按照配置设置语言：{0}", Settings.Lang);
      System.Threading.Thread.CurrentThread.CurrentUICulture =
        new System.Globalization.CultureInfo(Settings.Lang);
#if !DEBUG
      FormProgress.Start(null);
#endif
      thread_main = Utils.MiniDump.GetCurrentThreadId();
      Application.Run(new FormMain());
      enLog.log_set_level(log_level.LV_ALL);
      enLog.info("程序正常退出");
      unreg_crash();
      single_instance.ReleaseMutex();
      enLog.log_finish();
      if (File.Exists(file_name))
      {
        long size_file = new FileInfo(file_name).Length;
        if (size_file > 10000000000)
          File.Delete(file_name);
      }
    }
    static uint thread_main = 0;
    static string dmpfile = "crash.dmp";
    static int get_build()
    {
      Assembly asm = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
      return fvi.ProductBuildPart;
    }
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
    static void reg_crash()
    {
#if !DEBUG
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
      Application.ThreadException += Application_ThreadException;
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      //object x = null;
      //x.ToString();
      //       Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
      //       RegisterFilter(dmpfile, 2);
      //       SetFilterOptions(1);
#endif
    }
    static void unreg_crash()
    {
#if !DEBUG
      Application.ThreadException -= Application_ThreadException;
      AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
      //UnregisterFilter();
#endif
    }
    //     [DllImport("clrdump.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    //     static extern Int32 CreateDump(Int32 ProcessId, string FileName,
    //         Int32 DumpType, Int32 ExcThreadId, IntPtr ExtPtrs);
    //     [DllImport("clrdump.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    //     static extern Int32 RegisterFilter(string FileName, Int32 DumpType);
    //     [DllImport("clrdump.dll", SetLastError = true)]
    //     static extern Int32 UnregisterFilter();
    //     [DllImport("clrdump.dll")]
    //     static extern Int32 SetFilterOptions(Int32 Options);

    static Mutex single_instance;
    static bool FirstRun()
    {
      bool absent = false;
      single_instance = new Mutex(true, "ImagingSort", out absent);
      return absent;
    }
    static void CurrentDomain_UnhandledException(object sender,
      UnhandledExceptionEventArgs e)
    {
      exception_processing(e.ExceptionObject, "Unhandled domain exception");
    }
    static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
      exception_processing(e.Exception, "Thread exception");
      //       MessageBox.Show(System.Environment.NewLine + e.Exception.ToString(), 
      //         "Abnormal software exception");
    }
    static void exception_processing(object e, string reason)
    {
      
      try
      {
        string endl = System.Environment.NewLine;
        Hardware.MachineController.Stop();
        //Hardware.MachineController.SetLighting(true);
        Forms.FormProgress.Start("正在记录异常...");
        DateTime start = DateTime.Now;
        dmpfile = string.Format("C.{0:00}-{1:00}-{2:00} {3:00}.{4:00}.{5:00}.{6}.dmp",
          start.Year,
          start.Month,
          start.Day,
          start.Hour,
          start.Minute,
          start.Second,
          get_build()
          );
        Utils.MiniDump.TryDump(dmpfile, Utils.MiniDump.MiniDumpType.Normal);
        write_down_exception(e, reason);
        Forms.FormProgress.Stop();
//         bool spawn = (Utils.MB.Error(
//           "抱歉，软件运行过程中发生未捕获的异常。" + endl +
//           "异常原因已经记录，请通知技术支持人员处理。" + endl + endl +
//           "软件即将被关闭，点击【确认】重新运行，点击【取消】退出" + endl
//           //"未捕获的异常"
//           ));
        MessageBox.Show("抱歉，软件运行过程中发生未捕获的异常。" + endl +
        "异常原因已经记录，请通知技术支持人员处理。" + endl + endl +
        "软件即将被关闭，点击【确认】退出" + endl, "未捕获的异常",
        MessageBoxButtons.OK,MessageBoxIcon.Error);

        //Forms.FormMain.I.ExitSystem(spawn);
//         if (spawn)
//           Application.Restart();
      }
      catch
      {
      }
      finally
      {
        Application.Exit();
      }
    }
    static void write_down_exception(object info, string reason)
    {
      string nl = System.Environment.NewLine;
      try
      {
        StreamWriter sw = new StreamWriter("dump.log", true);
        sw.WriteLine(
          DateTime.Now.ToString("F") + nl +
          "Main线程：" + thread_main.ToString() + nl +
          "异常线程：" + Utils.MiniDump.GetCurrentThreadId().ToString() + nl +
          "    原因：" + reason + nl +
          "  提交号：" + get_build().ToString() + nl +
          "转储文件：" + dmpfile + nl +
          "异常类型：" + info.GetType().ToString() + nl +
          "异常详细：" + info.ToString() + nl +
          nl);
        sw.Close();
      }
      catch
      {

      }
    }
    //     public static void Dump()
    //     {
    //       IntPtr pEP = System.Runtime.InteropServices.Marshal.GetExceptionPointers();
    //       CreateDump(
    //       System.Diagnostics.Process.GetCurrentProcess().Id,
    //       dmpfile,
    //       (Int32)MiniDumpDemo.MINIDUMP_TYPE.MiniDumpNormal,
    //       System.Threading.Thread.CurrentThread.ManagedThreadId,
    //       pEP);
    //     }
    private static void make_revision(string path)
    {
      System.IO.Directory.SetCurrentDirectory(path);
      DirectoryInfo di = new DirectoryInfo(".");
      FileInfo[] fis = di.GetFiles("*.revision");
      if (fis.Length != 0)
      {
        foreach (FileInfo fi in fis)
        {
          fi.Delete();
        }
      }

      try
      {
        FileStream fs = new FileStream(
          get_build().ToString() + ".revision",
          FileMode.CreateNew);
        fs.Close();
        MessageBox.Show("当前revision：" + get_build().ToString(), "revision");
      }
      catch
      {
        MessageBox.Show("创建revision信息失败！");
      }
    }
  }
}
