using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagingSortCharlie.Utils.Devices;
using System.Threading;

namespace ImagingSortCharlie.Data
{
  public static class check_trigger
  {
    const int SLEEP_TRIGGER = 10;
    static ManualResetEvent pause_event = new ManualResetEvent(false);
    static ManualResetEvent exit_event = new ManualResetEvent(false);
    private static Thread thread_trigger = null;
    static PCI1710_DIGITAL_SWITCH last = PCI1710_DIGITAL_SWITCH.PCI1710_SW_OFF;

    public static void init()
    {
      stop();
      thread_trigger = new Thread(get_trigger);
      thread_trigger.Name = "get_trigger";
      thread_trigger.Start();
    }

    public static void start()
    {
      exit_event.Reset();
      pause_event.Set();
      last = PCI1710_DIGITAL_SWITCH.PCI1710_SW_OFF;
    }

    public static void stop()
    {
      pause_event.Reset();
    }

    public static void exit()
    {
      exit_event.Set();
      pause_event.Set();
      thread_trigger.Join();
    }

    static void get_trigger()
    {
      while(true)
      {
        pause_event.WaitOne();
        if (exit_event.WaitOne(0, false))
          break;

        if(PCI1710.GetDI(0) == PCI1710_DIGITAL_SWITCH.PCI1710_SW_ON)
        {
          if(last == PCI1710_DIGITAL_SWITCH.PCI1710_SW_OFF)
          {
            last = PCI1710_DIGITAL_SWITCH.PCI1710_SW_ON;
//             flow_info.on_trigger();
          }
        }
        else
        {
          if (last == PCI1710_DIGITAL_SWITCH.PCI1710_SW_ON)
            last = PCI1710_DIGITAL_SWITCH.PCI1710_SW_OFF;
        }
        System.Threading.Thread.Sleep(SLEEP_TRIGGER);
      }

    }

  }
}
