using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagingSortCharlie.Data.Settings;
using System.Threading;
using ImagingSortCharlie.Hardware;
using ImagingSortCharlie.Utils;

namespace ImagingSortCharlie.Data
{
  public struct result_info
  {
    public bool result;
    public int idx;
    public VIEW view;
    public long on_grab_time;
    public long on_frame_time;
    public long in_lock_time;
    public long finish_apply_time;
  }

  class sort_results
  {
    bool result;
    bool[] results;
    int views_left;
    bool got_result;
    long trigger_time;
    long[] on_grab_time;
    long[] on_frame_time;
    long[] in_lock_time;
    long[] finish_apply_time;
    long[] over_time;

//     long[] start_time;
//     long[] end_time;
    public Timer schedule;

    public sort_results(int count_view)
    {
      reset(count_view);
    }

    public void printf_result(VIEW view)
    {
      int idx = (int)view;
      TimerCounter tmp = new TimerCounter();
      string s = view.ToString() + " : " + results[idx].ToString();
      s += ", on_grab: " + ((double)(on_grab_time[idx] - trigger_time) / tmp.freq * 1000).ToString("0.00");
      s += ", in_frame: " + ((double)(on_frame_time[idx] - on_grab_time[idx]) / tmp.freq * 1000).ToString("0.00");
      s += ", in_lock: " + ((double)(in_lock_time[idx] - on_frame_time[idx]) / tmp.freq * 1000).ToString("0.00");
      s += ", apply_time: " + ((double)(finish_apply_time[idx] - in_lock_time[idx]) / tmp.freq * 1000).ToString("0.00");
      s += ", send_time: " + ((double)(over_time[idx] - finish_apply_time[idx]) / tmp.freq * 1000).ToString("0.00");
      //enLog.error(s);
      System.Console.WriteLine(s);
    }

    public void send_result(result_info info)
    {
      int idx = (int)info.view;
      results[idx] = info.result;
      if(!info.result)
        result = false;
      views_left--;

      on_grab_time[idx] = info.on_grab_time;
      on_frame_time[idx] = info.on_frame_time;
      in_lock_time[idx] = info.in_lock_time;
      finish_apply_time[idx] = info.finish_apply_time;
      TimerCounter.QueryPerformanceCounter(out over_time[idx]);
//       printf_result(info.view);
      if (got_result)
      {
        System.Console.WriteLine(info.idx.ToString() + "have_got");
        printf_result(info.view);
      }
    }

    public bool get_result(int idx)
    {
      got_result = true;
      if (views_left != 0)
      {
        for (int i = 0; i < 8; i++)
        {
          printf_result((VIEW)i);
        }
      }
      return result && views_left == 0;
    }

    public void reset(int count_view)
    {
      result = true;
      views_left = count_view;
      got_result = false;
      trigger_time = 0;
      results = new bool[count_view];
      on_grab_time = new long[count_view];
      on_frame_time = new long[count_view];
      in_lock_time = new long[count_view];
      finish_apply_time = new long[count_view];
      over_time = new long[count_view];

      for (int i = 0; i < count_view; i++)
      {
        results[i] = false;
        on_grab_time[i] = 0;
        on_frame_time[i] = 0;
        in_lock_time[i] = 0;
        finish_apply_time[i] = 0;
        over_time[i] = 0;
      }
    }

//     public void on_image(VIEW view)
//     {
//       long t = 0;
//       TimerCounter.QueryPerformanceCounter(out t);
//       TimerCounter temp = new TimerCounter();
//       start_time[(int)view] = t;
//     }

    public void on_trigger(long time)
    {
      trigger_time = time;
    }

  }

  public static class flow_info
  {
    const int SCHEDULE_TIME = 2300;
    const int COUNT_RESULTS = 300;
    static List<sort_results> results ;
    static int[] count_images;
    static int count_cams = 0;
    static int flow_trigger = -1;
    static object lock_trigger = new object();

    public static void init()
    {
      set_zero();
    }

    public static bool get_result(int idx)
    {
      idx %= COUNT_RESULTS;
      return results[idx].get_result(idx);
    }

    public static void send_results(result_info info)
    {
      int diff = info.idx - flow_trigger;
      if(diff > 0)
      {
        System.Console.WriteLine("error");
      }
      int idx = info.idx % COUNT_RESULTS;
      results[idx].send_result(info);
    }


    public static void set_zero()
    {
      count_cams = Configure.This.CameraCount;
      results = new List<sort_results>(COUNT_RESULTS);
      for (int i = 0; i < COUNT_RESULTS; i++)
      {
        results.Add(new sort_results(count_cams));
      }

      flow_trigger = -1;
      count_images = new int[count_cams];
      for(int i = 0; i < count_cams; i++)
      {
        count_images[i] = -1;
      }
    }

    struct schedule_info
    {
      public int flow;
      public TimerCounter tm;
    }

    public static void on_trigger(long trigger_time)
    {
      flow_trigger++;
      int idx = flow_trigger % COUNT_RESULTS;
      results[idx].reset(count_cams);
      results[idx].on_trigger(trigger_time);
      schedule_info si = new schedule_info();
      si.flow = flow_trigger;
      si.tm = new TimerCounter();
      si.tm.Start();
      results[idx].schedule = new Timer(send_signal, si/*flow_trigger*/, SCHEDULE_TIME, Timeout.Infinite);
    }

    static void send_signal(object obj)
    {
      schedule_info si = (schedule_info)obj;
      int idx = si.flow;
      bool result = get_result(idx);
      if (result)
        MachineController.send_good();
      else
      {
        System.Console.WriteLine(count_images[0].ToString());
        System.Console.WriteLine(count_images[7].ToString());
        MachineController.send_bad();
      }
      si.tm.Stop();
      System.Console.WriteLine(idx.ToString() + result.ToString());
    }

    public static void on_image(VIEW view, long trigger_time)
    {
      lock(lock_trigger)
      {
        count_images[(int)view]++;
        if (count_images[(int)view] > flow_trigger)
          on_trigger(trigger_time);
      }
//       int idx = count_images[(int)view] % COUNT_RESULTS;
//       results[idx].on_image(view);
    }

    public static int get_idx_image(VIEW view)
    {
      return count_images[(int)view];
    }

    public static void printf_counts()
    {
      for(int i = 0; i < count_cams; i++)
      {
        System.Console.WriteLine(((VIEW)i).ToString() + ":" + count_images[i].ToString());
      }
    }

  }

}
