#define SEPARATE_3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using ImagingSortCharlie.Utils.Devices;
using System.Reflection;
using ImagingSortCharlie.Data.Settings;
using System.Collections;
using System.IO;
using ImagingSortCharlie.Data;
using ImagingSortCharlie.Utils;

namespace ImagingSortCharlie.Hardware
{
  struct info_sender
  {
    public bool is_empty;
    public VIEW view;
    //public int number;
  }

//   struct result_view
//   {
//     public VIEW view;
//     public int number;
//   }

  public static class FrameGrabber
  {
    const int COUNT_RESULT = 200;
    public static VIEW first_view = VIEW.VIEW_COUNT;
    public delegate void InvokeDelegate();
    public static event EventHandler OnImage;
    public static event EventHandler OnMissing;
    static ImageEvent ev = new ImageEvent();
    private static volatile bool isLive = false;
    private static volatile bool isSpinning = false;
    public static bool IsLive { get { return isLive && !one_shot; } }
    public static bool IsOneShot { get { return one_shot; } }
    public static bool IsSpinning { get { return isSpinning; } }
    private static int CameraCount = 0;
    static IGrab[] cameras;
    // <CamSerial, ViewIndex>
    private static Dictionary<uint, VIEW> serial_view = new Dictionary<uint, VIEW>();
    //<ViewIndex, IGrab>
    private static Dictionary<VIEW, int> view_grab = new Dictionary<VIEW, int>();
    private const string CAM_CONFIG = "CamConfig.xml";

//    static int number_image = -1;
    static Timer[] tm_get_result = new Timer[COUNT_RESULT];
   // public static bool[] results = new bool[COUNT_RESULT];
    //public static bool[] applied = new bool[COUNT_RESULT];

    static bool init_serial_view()
    {
      bool result = true;
      serial_view.Clear();
      for (int i = 0; i < Configure.This.CameraCount; i++)
      {
        serial_view.Add(Configure.This.ViewSerial[i], (VIEW)i);
      }
      return result;
    }

    public static void Setup(VIEW view, IntPtr parent, bool show)
    {
      if (view_grab.ContainsKey(view))
      {
        cameras[view_grab[view]].ShowSetup((uint)parent.ToInt32(), show);
        string file_name = cameras[view_grab[view]].GetSerial(view_grab[view]).ToString() + ".xml";
        cameras[view_grab[view]].SaveSettings(GD.HomePath(file_name));
      }
    }

    public static bool Init()
    {
      bool result = false;
      if (!init_serial_view())
        return false;
      CameraCount = GrabSelector.Total;
      fps_time_counter = new ImagingSortCharlie.Utils.TimerCounter[CameraCount];
      duration = new double[CameraCount];
      cameras = new IGrab[CameraCount];
      for (int i = 0; i < CameraCount; i++)
      {
        duration[i] = 0;
        cameras[i] = GrabSelector.Instance((uint)i, Callback);
        if (cameras[i] != null && serial_view.ContainsKey(cameras[i].GetSerial(i)))
        {
          VIEW view = serial_view[cameras[i].GetSerial(i)];
          string file_name = cameras[i].GetSerial(i).ToString() + ".xml";
          cameras[i].LoadSettings(GD.HomePath(file_name), false);
          view_grab.Add(view, i);
          if (view < first_view)
            first_view = view;
          result = true;
          cameras[i].SetTrigger((int)IFCTriggerType.IFC_DEFAULT_TRIG);
          fps_time_counter[i] = new ImagingSortCharlie.Utils.TimerCounter();
        }
      }
      return result;
    }

    public static bool ViewUsable(VIEW view)
    {
      return view_grab.ContainsKey(view);
    }

    public static void Live(VIEW view)
    {
      if (isLive || isSpinning)
        return;
      MachineController.Dimming(true);
      if (view_grab.ContainsKey(view))
        isLive = cameras[view_grab[view]].Live();
      if (!isLive)
      {
        one_shot = false;
        MachineController.Dimming(false);
        return;
      }
      fps_time_counter[view_grab[(VIEW)view]].Start();
    }
    public static void Start()
    {
      if (isLive || isSpinning)
        return;
      //count_time_out = 0;
      for (int i = 0; i < CameraCount; i++)
      {
        if (cameras[i] != null && serial_view.ContainsKey(cameras[i].GetSerial(i)))
        {
          cameras[i].SetTrigger((int)IFCTriggerType.IFC_DEFAULT_TRIG);
          if (cameras[i].Start())
            isSpinning = true;
          else
            Utils.MB.Warning("相机" + i.ToString() + "初始化失败");
          fps_time_counter[i].Start();
        }
      }
    }
    public static void Locating(VIEW view, bool OnOff)
    {
      if (!view_grab.ContainsKey(view))
        return;
      if (OnOff)
      {
        cameras[view_grab[view]].SetTrigger((int)IFCTriggerType.IFC_DEFAULT_TRIG);
        cameras[view_grab[view]].Start();
      }
      else
        cameras[view_grab[view]].Stop();
    }
    public static void GrabOneFrame()
    {
      one_shot = true;
      cameras[view_grab[GD.CurrentView]].Snapshot();
    }
    public static void Stop()
    {
      for (int i = 0; i < CameraCount; i++)
      {
        if (cameras[i] != null)
          cameras[i].Stop();
      }
      MachineController.Dimming(false);
      stop_spinning();
      isLive = false;
    }

    public static void stop_spinning()
    {
      isSpinning = false;
    }
    public static void Stop(VIEW view)
    {
      if (view_grab.ContainsKey(view))
        cameras[view_grab[view]].Stop();
      MachineController.Dimming(false);
      isLive = false;
      isSpinning = false;
    }
    public static void Exit()
    {
      enLog.enter_function("FrameGrabber.Exit");
      for (int i = 0; i < CameraCount; i++)
      {
        if (cameras[i] != null)
        {
          cameras[i].Free();
          cameras[i] = null;
        }
      }
      Debug.Print("[FrameGrabber] Setting exit signal for thread");
      enLog.exit_function("FrameGrabber.Exit");
    }
    private static void AsyncCB(IAsyncResult result)
    {
    }
    private static unsafe DelegateGrabCallback Callback = new DelegateGrabCallback(OnFrame);
    private static void Missing(uint board_idx)
    {
      if (OnMissing != null)
        OnMissing(null, null);
    }

    volatile static bool[] is_calculating = new bool[(int)VIEW.VIEW_COUNT] { false, false, false, false, false, false, false, false };
    public static bool on_board_start(int idx_cam)
    {
      if (is_calculating[idx_cam])
      {
        // 软件紧急停止！
        //software_halt();
        return false;
      }
      is_calculating[idx_cam] = true;
      return true;
    }

    public static void on_board_end(int idx_cam)
    {
      is_calculating[idx_cam] = false;
    }

    public static void software_halt()
    {
      MachineController.SetEmergencyHalt(MachineController.State.Software);
    }

 //   static int count_time_out = 0;
//     public static int[] signal_count = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
//     public static int[] result_count = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
//     public static int[] image_count = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
//     static void get_result(object obj)
//     {
//       //const int COUNT_TIME_OUT = 10;
//       result_view rv = (result_view)obj;
//       if (!applied[rv.number])
//       {
//         count_time_out++;
// //         if (count_time_out > COUNT_TIME_OUT)
// //           software_halt();
//       }
//         
//       bool result = results[rv.number];
//       if (!result)
//       {
//         STATION station = Configure.This.ViewStation[(int)rv.view];
//         MachineController.Failed(station);
//       }
//     }

    //static object lock_on_image = new object();
//     static void on_image(VIEW view, int time)
//     {
//       const int TIME_GET_RESULT = 400;
//       number_image++;
//       number_image %= COUNT_RESULT;
//       results[number_image] = false;
//       applied[number_image] = false;
//       result_view rv = new result_view();
//       rv.view = view;
//       rv.number = number_image;
//       //       //因不发送DO信号而改动
//       time = 0;
//       int translate_time = (time + 1000) % 1000;
//       if (translate_time >= TIME_GET_RESULT)
//         return;
//       tm_get_result[number_image] =
//         new Timer(get_result, rv, TIME_GET_RESULT - translate_time, Timeout.Infinite);
//     }
//     static void on_image(VIEW view, int time)
//     {
//       STATION station = Configure.This.ViewStation[(int)view];
//       MachineController.Failed(station);
//       signal_count[(int)view]++;
//     }
    static int[] num_images = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
    static object lock_calc = new object();
    static int[] num_apply_locked = new int[(int)VIEW.VIEW_COUNT] { 0, 0, 0, 0, 0, 0, 0, 0 };
    private static unsafe void OnFrame(uint cam_serial, byte* p, int width, int height)
    {
      if (one_shot)
        one_shot = false;
      VIEW view = VIEW.VIEW1;
      try
      {
        view = serial_view[cam_serial];
      }
      catch (KeyNotFoundException)
      {
        return;
      }
      int view_idx = (int)view;
//       lock(lock_on_image)
//       {
//         if(GD.IsSpinning)
//           on_image(view, width);
//         info_sender.number = number_image;
//         info_sender.view = view;
//         info_sender.is_empty = false;
//       }
      fps_time_counter[view_grab[view]].Stop();
      duration[view_grab[view]] = fps_time_counter[view_grab[view]].Duration;
      fps_time_counter[view_grab[view]].Start();
      info_sender info_sender;
      info_sender.view = view;
      info_sender.is_empty = false;
      lock(lock_calc)
      {
        if (!on_board_start(view_idx))
        {
          num_apply_locked[view_idx]++;
          info_sender.is_empty = true;

          if (OnImage != null)
            OnImage(info_sender, null);
          return;
        }

        IA.IA_AqcuiredImage(p, view_idx, view_idx);
        if (OnImage != null)
          OnImage(info_sender, null);
      }
   }
    private static Utils.TimerCounter[] fps_time_counter;
    public static double FPS(VIEW view)
    {
      int idx = view_grab[view];
      if (duration[idx] <= double.Epsilon)
        return 0.0;
      return 1 / duration[idx];
    }

    static double[] duration;
    static bool one_shot = false;
  }


  ////////////////////////////////////////////////////////////////////////////
  public enum IFCTriggerType
  {
    IFC_ERR_TRIG = -1,
    IFC_SOFT_TRIG = 0,
    IFC_EXT0_TRIG,
    IFC_EXT1_TRIG,
    IFC_EXT2_TRIG,
    IFC_EXT3_TRIG,
    IFC_EXT_AUTO_TRIG,
    IFC_DEFAULT_TRIG = IFC_EXT_AUTO_TRIG
  }

  public class ImageEvent : EventArgs
  {
    private byte[] buffer = null;
    public byte[] Buffer { get { return buffer; } set { buffer = value; } }
  }
}
