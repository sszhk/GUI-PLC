using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace ImagingSortCharlie.Utils
{
    public class TimerCounter
    {
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        private long startTime, stopTime;
        public long freq;

        // Constructor

        public TimerCounter()
        {
            startTime = 0;
            stopTime = 0;

            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported

                throw new Win32Exception();
            }
        }

        // Start the timer

        public void Start()
        {
            // lets do the waiting threads there work

            Thread.Sleep(0);

            QueryPerformanceCounter(out startTime);
            //System.Diagnostics.Debug.Print("Counting start...");
        }

        // Stop the timer

        public void Stop()
        {
            QueryPerformanceCounter(out stopTime);
            //System.Diagnostics.Debug.Print("Counted {0} milliseconds.", Duration*1000);
        }

        // Returns the duration of the timer (in seconds)

        public double Duration
        {
            get
            {
                return (double)(stopTime - startTime) / (double)freq;
            }
        }
    }
}