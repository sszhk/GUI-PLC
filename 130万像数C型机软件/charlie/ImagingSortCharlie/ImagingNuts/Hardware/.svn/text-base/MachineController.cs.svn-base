using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagingSortCharlie.Utils.Devices;
using System.Threading;
using ImagingSortCharlie.Data.Filters;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Settings;
using System.Drawing;
using System.IO.Ports;
using ImagingSortCharlie.Utils;
namespace ImagingSortCharlie.Hardware
{
  public static class MachineController
  {
    static int current_in_box = 0;//当前盒子支数
    static int last_change_box = 0;//上次换盒的良品支数

    static SerialPort serialPort = new SerialPort("COM1", 115200, Parity.Odd, 8, StopBits.One);

    private static ManualResetEvent pause = new ManualResetEvent(false);
    private static ManualResetEvent exit = new ManualResetEvent(false);
    private static Thread threadObj = null;
    private const int DURATION = 50;

    public static event EventHandler OnState;

    //紧急停止
    public enum State //状态
    {
      Normol = 0,//正常
      Depression = 2105,//16005气压不足
      Stucked = 16014,//16014卡料停止
      Manual = 2106,//16004紧急停止已经按下
      Locked = 2108,//16015吹气堵料
      OutBlowCount = 1700,//16016连续不良品超量
      StorageFull = 2402,//16020储料斗满料
      StorageCylinder = 2404,//16021储料斗气缸故障
      Invertedfault = 2406,//16022倒料桶故障
      PushEmptyCylinder = 2408,//16023推空盒气缸故障
      PullEmptyFault = 2412,//16024送空盒故障
      PullFullBox = 2414,//16025送满盒故障
      PullFullBoxOutTime = 2416,//16026送满盒超限
      FeedOutTime = 2418,//16027补料超时
      BufferArrived = 2060,//储料盒满
      WaitEmpty = 2066,//等待空盒
      BoxFull = 2068,//满盒超限   
      Software = 16013,//影响触发频繁
      ClearPlate=16028,//擦玻璃
      NoFeedTime=16030,//无料时间
      Hardware = 91012//硬盘空间不足
    };
    public static EventHandler OnGoodNumChanged;
    public delegate void CriticalDelegate(State halt_reason);
    public static event CriticalDelegate OnCritical;

    private const int TIME_SLEEP_THREAD = 10;
    private const int TIME_COM_SAFE = 5;
    private static void Sleep(int ms) { System.Threading.Thread.Sleep(ms); }

    static bool check_state(int single, bool is_halt)
    {
      get_num_good();
      if (ReadSignal(single.ToString()))
      {
        send_state(single);
        if (is_halt && GD.IsSpinning)
          SetEmergencyHalt((State)single);
        return true;
      }
      return false;
    }

    static void check_state()
    {
      if (check_state(2105, true))
        return ;
      if (check_state(2106, true))
        return;
      if (check_state(2108, true))
        return;
      if (check_state(2402, true))
        return;
      if (check_state(2404, true))
        return;
      if (check_state(2406, true))
        return;
      if (check_state(2408, true))
        return;
      if (check_state(2412, true))
        return;
      if (check_state(2414, true))
        return;
      if (check_state(2416, true))
        return;
      if (check_state(2418, true))
        return;
      if (check_state(2060, false))
        return;
      if (check_state(2066, false))
        return;
      if (check_state(2068, false))
        return;
      send_state((int)State.Normol);
    }

    static void send_state(int state)
    {
      if (OnState != null)
      {
        OnState(state, null);
      }
    }
    private static void Run()
    {
      while (true)
      {
        pause.WaitOne();
        if (exit.WaitOne(0, false))
          break;
        check_state();
        HeartBeat();
//         get_num_good();
        System.Threading.Thread.Sleep(TIME_SLEEP_THREAD);
      }
      Stop();
      System.Diagnostics.Debug.Print("[MachineController] exiting...");
      PCI1710.Free();
    }

    public static bool Init()
    {
      enLog.enter_function("MachineController.Init");
      bool result = true;
      result = serialPortOpen();
      Stop();
      threadObj = new Thread(Run);
      threadObj.Name = "Emergency&Stuck";
      threadObj.Start();
      //       exit.Reset();
      //       pause.Reset();
      if (!PCI1710.Init())
      {
#if !DEBUG
               Utils.MB.Warning(GD.GetString("23001")/*"PCI 1710 初始化失败！请检查板卡。"*/);
               enLog.exit_function("MachineController.Init");
               return false;
#else
        result = false;
#endif
      }
      for (int i = 0; i < (int)STATION.STATION_COUNT; i++)
      {
        PCI1710.SetDO(i, ONOFF(false));
      }
      exit.Reset();
      pause.Set();
      enLog.exit_function("MachineController.Init");
      return result;
    }
    public static void Exit()
    {
      enLog.enter_function("MachineController.Exit");
      exit.Set();
      pause.Set();
      threadObj.Join();
      serialPortClose();
      enLog.exit_function("MachineController.Exit");
    }

    public static void clear_current()
    {
      current_in_box = 0;
    }

    #region - PCI1710 控制 -
 //   static System.Threading.Timer tmSendBad;
//     struct view_time
//     {
//       public STATION stat;
//       public long long_time;
//     }
//     static int[] di_count = new int[(int)VIEW.VIEW_COUNT] {0,0,0,0,0,0,0,0 };
//     static int[] di_count_on = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
//     static int[] count_false = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
//     static bool[] bool_sw = new bool[] {false, };
//     static int retry_count = 0;
//       static public void StopSendBad(object obj)
//     {
//       view_time vt = (view_time)obj;
//       if (PCI1710.GetDI((int)vt.stat) == PCI1710_DIGITAL_SWITCH.PCI1710_SW_ON)
//       {
//         di_count[(int)vt.stat]++;
//         retry_count = 0;
//       }
//       else
//       {
//         if(retry_count < 3)
//         {
//           SendBad(vt.stat);
//           retry_count++;
//         }
//         enLog.debug(vt.stat.ToString() + "第几次DO=" + (FrameGrabber.signal_count[(int)vt.stat]).ToString() + ",重试次数=" + retry_count.ToString());
//       }
//       enLog.debug(vt.stat.ToString() + ",DI数量 = " + (di_count[(int)vt.stat]).ToString());
//       if (FrameGrabber.signal_count[(int)vt.stat] != di_count[(int)vt.stat])
//         enLog.warning(vt.stat.ToString() + "发送DO=" + (FrameGrabber.signal_count[(int)vt.stat]).ToString() + " 接收DI=" + (di_count[(int)vt.stat]).ToString());
//       PCI1710.SetDO((int)vt.stat, ONOFF(false));
//       count_false[(int)vt.stat]++;
//       enLog.debug(vt.stat.ToString() + "OFF信号数号=" + (count_false[(int)vt.stat]).ToString());
//       if (FrameGrabber.signal_count[(int)vt.stat] != count_false[(int)vt.stat])
//         enLog.warning("DO ON和OFF不一致，DO=" + (FrameGrabber.signal_count[(int)vt.stat]).ToString() + (count_false[(int)vt.stat]).ToString());
//       long temp = 0;
//       TimerCounter tc = new TimerCounter();
//       TimerCounter.QueryPerformanceCounter(out temp);
//       double time_split = (double)(temp - vt.long_time)*1000/(double)tc.freq;
//       if(time_split<10 || time_split > 50)
//         enLog.warning("发送ON到OFF信号时间间隔：" + time_split.ToString());
//       //enLog.debug("发送ON到OFF信号时间间隔："+time_split.ToString());
//     }
    static object[] lock_sendbad = new object[] {
      new object(),
      new object(),
      new object(),
      new object()
    };
    public static void SendBad(STATION station)
    {
      lock(lock_sendbad[(int)station])
      {
        PCI1710.DO_ON((int)station);
        System.Threading.Thread.Sleep(Settings.PS.SetBadTime);
        PCI1710.DO_OFF((int)station);
      }
    }

    private static PCI1710_DIGITAL_SWITCH GetCount()
    {
      return PCI1710.GetDI(3);
    }

//     static public void send_bad()
//     {
//       PCI1710.SetDO(0, ONOFF(true));
//       tmSendBad = new System.Threading.Timer(StopSendBad, 0,
//         Settings.PS.SetBadTime, Timeout.Infinite);
//     }
// 
//     static public void send_good()
//     {
//       PCI1710.SetDO(1, ONOFF(true));
//       tmSendBad = new System.Threading.Timer(StopSendBad, 1,
//         Settings.PS.SetBadTime, Timeout.Infinite);
//     }

    #endregion

    static void change_box()
    {
      last_change_box = Settings.PS.GoodSum;
      clear_current();
      SetChangeBox(false);
      System.Threading.Timer tm_stop =
        new System.Threading.Timer(stop_change_box, null, 100, Timeout.Infinite);
    }

    static void stop_change_box(object state)
    {
      SetChangeBox(false);
    }

    private static void check_box_change()
    {
      if (Settings.PS.EnablePack
        && current_in_box == Settings.PS.PackCount
        && last_change_box != Settings.PS.GoodSum)
      {
        change_box();
      }
    }


    public static void SetEmergencyHalt(State haltReason)
    {
      //Stop();
      if (OnCritical != null)
      {
        OnCritical.Invoke(haltReason);
      }
    }

    public static void Start()
    {
      //       exit.Reset();
      //       pause.Set();

      MachineStart(true);
      SoftwareStart(true);
    }

    public static void Stop()
    {
      //       pause.Reset();
      MachineStart(false);
      no_feed(false);
    }


    public static void OnImage(int site)
    {

    }
    // 当任何一路影像计算失败/不达标时
    public static void Failed(STATION station)
    {
      SendBad(station);
    }

    // 简单的转换，少些点字 ^_^
    private static PCI1710_DIGITAL_SWITCH ONOFF(bool on) { return on ? PCI1710_DIGITAL_SWITCH.PCI1710_SW_ON : PCI1710_DIGITAL_SWITCH.PCI1710_SW_OFF; }

    static System.Threading.Timer tmStopLight;
    private static void ScheduleStopLight()
    {
      tmStopLight = new System.Threading.Timer(OnStopLight, null, DURATION, Timeout.Infinite);
    }
    private static void OnStopLight(object state)
    {
      StopLight(false);
    }

    #region -通讯端口-
    public static void MachineStart(bool OnOff)
    {
      SendSignal("1600", OnOff);
    }

    static void SoftwareStart(bool OnOff)
    {
      SendSignal("1601", OnOff);
    }

    public static void StopLight(bool OnOff)
    {
//       SendSignal("2200", OnOff);
//       if (OnOff)
//         ScheduleStopLight();
    }


    public static void Locating(STATION station, bool OnOff)
    {
      switch (station)
      {
        case STATION.STATION1: SendSignal("2112", OnOff);
          break;
        case STATION.STATION2: SendSignal("2113", OnOff);
          break;
        case STATION.STATION3: SendSignal("2114", OnOff);
          break;
        case STATION.STATION4: SendSignal("2124", OnOff);
          break;
        default:
          break;
      }
    }

    public static void UsingCount(bool OnOff)
    {
      SendSignal("2102", OnOff);
    }

    static bool heart_beat = true;
    static void HeartBeat()
    {
      SendSignal("1730", heart_beat);
      heart_beat = !heart_beat;
    }

    public static void Dimming(bool OnOff)
    {
      SendSignal("2100", OnOff);
    }

    public static void OutBlowCount(bool OnOff)
    {
      SendSignal("1700", OnOff);
    }

    public static void set_zero()
    {
      clear_current();
      Send2Signal("1500", 50);
    }

    public static void SetChangeBox(bool on)
    {
      SendSignal("2103", on);
    }

    public static void no_feed(bool on)
    {
      SendSignal("2362", on);
    }

    public static bool save_count(int count)
    {
      string count_hex = count.ToString("X4");
      char[] data1 = count_hex.ToCharArray(2, 2);
      char[] data2 = count_hex.ToCharArray(0, 2);
      count_hex = new string(data1) + new string(data2);
      string signal = "%01#WDD3273032730" + count_hex + "**\r";
      byte[] data = UnicodeToEncodeing(signal);
      string result = ReadSignal(data);
      return result == "%01$WD13";
    }

    static void get_num_good()
    {
      string signal = "%01#RDD3275032751**" + "\r";
      byte[] data = UnicodeToEncodeing(signal);
      string result = ReadSignal(data);
      int num_good = 0;
      if (result != null && result.Length == 16)
      {
        //串口字符串的解析方式
        char[] ch = result.ToCharArray(6, 8);
        char[] newchar = new char[8] { ch[6], ch[7], ch[4], ch[5], ch[2], ch[3], ch[0], ch[1] };
        int.TryParse(new string(newchar),
          System.Globalization.NumberStyles.HexNumber, null, out num_good);
      }

      if (num_good == Settings.PS.GoodSum)
        return;
      Settings.PS.GoodSum = num_good;
      Settings.PS.BadSum = Settings.PS.Total - Settings.PS.GoodSum;
      current_in_box++;
      check_box_change();
      if (OnGoodNumChanged != null)
      {
        OnGoodNumChanged(null, null);
      }
    }

    static void serialPortClose()
    {
      lock (com)
      {
        if (serialPort.IsOpen)
        {
          try
          {
            serialPort.Close();
          }
          catch (System.Exception /*ex*/)
          {
            //Utils.MB.Warning(ex.Message);
          }
        }
      }
    }

    static bool serialPortOpen()
    {
      bool result = true;
      lock (com)
      {
        if (!serialPort.IsOpen)
        {
          try
          {
            serialPort.Open();
          }
          catch (System.Exception ex)
          {
            string error = ex.Message;
#if !DEBUG
            Utils.MB.Warning(error);
#endif
            result = false;
          }
        }
      }
      if (result)
      {
        serialPort.ReadTimeout = 500;
        serialPort.WriteTimeout = 500;
      }
      return result;
    }

    static object com = new object();
    static void SendSignal(byte[] data)
    {
      lock (com)
      {
        if (serialPort.IsOpen)
        {
          serialPort.Write(data, 0, data.Length);
          Sleep(TIME_COM_SAFE);
        }
      }
    }
    static void SendSignal(string s, bool OnOff)
    {
      int i = OnOff ? 1 : 0;
      string signal = "%01#WCSR" + s + i.ToString() + "**" + "\r";
      byte[] data = UnicodeToEncodeing(signal);
      SendSignal(data);
    }

    static void SendSignal(string s)
    {

      string signal = "%01#WCSR" + s + "**" + "\r";
      byte[] data = UnicodeToEncodeing(signal);
      lock (com)
      {
        if (!serialPort.IsOpen)
          return;
        serialPort.DiscardInBuffer();
        serialPort.Write(data, 0, data.Length);
        //5ms为测试安全时间，待定
        Sleep(TIME_COM_SAFE);
      }
    }
    static bool ReadSignal(string s)
    {
      string signal = "%01#RCSR" + s + "**" + "\r";
      byte[] data = UnicodeToEncodeing(signal);
      string result = "";
      lock (com)
      {
        if (!serialPort.IsOpen)
          return true;
        serialPort.DiscardInBuffer();
        serialPort.Write(data, 0, data.Length);
        //5ms为测试安全时间，待定
        Sleep(TIME_COM_SAFE);
        try
        {
          result = serialPort.ReadTo("\r");
        }
        catch (System.Exception /*ex*/)
        {
          //System.Diagnostics.Debug.Write(ex.Message);
        }

        Sleep(TIME_COM_SAFE);
      }
      return result == "%01$RC120";
    }

    static string ReadSignal(byte[] data)
    {
      string result = "";
      lock (com)
      {
        if (serialPort.IsOpen)
        {
          serialPort.DiscardInBuffer();
          serialPort.Write(data, 0, data.Length);
          Sleep(TIME_COM_SAFE);
          try
          {
            result = serialPort.ReadTo("\r");
          }
          catch (System.Exception)
          {
            result = null;
          }
          Sleep(TIME_COM_SAFE);
        }
      }
      return result;
    }

    static void Send2Signal(string s, int time)
    {
      SendSignal(s, true);
      Sleep(time);
      SendSignal(s, false);
    }
    static bool SendReadSignal(bool control, string send, bool onoff,
      int time, string read)
    {
      if (control)
      {
        SendSignal(send, onoff);
        if (time != 0)
        {
          Sleep(time);
        }
      }
      return ReadSignal(read);
    }
    static byte[] UnicodeToEncodeing(string s)
    {
      UTF8Encoding en = new UTF8Encoding();
      return en.GetBytes(s);
    }
    #endregion
    #region--通讯方式
    /// <summary>
    /// 主要是测试按钮和各个马达所用
    /// </summary>
    /// <param name="output_number"></param>
    /// <param name="OnOff"></param>
    public static void send_only(string send_number, bool OnOff)
    {
      SendSignal(send_number, OnOff);
    }
    public static bool only_read(string read_number)
    {
      return ReadSignal(read_number);
    }
    public static bool send_and_read(bool control, string send,
      int time, bool onoff, string read)
    {
      return SendReadSignal(control, send, onoff, time, read);
    }
    #endregion
  }
}
