// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// 
// namespace ImagingSortCharlie.Data.Settings
// {
//   [Serializable]
//   public class OperationRecords
//   {
//     #region - 静态 -
//     private const string FILE = "OperationRecords.xml";
//     public static OperationRecords I = new OperationRecords();
//     public static void Save()
//     {
//       Xml.XMLUtil<OperationRecords>.SaveBinXml(FILE, I);
//     }
//     public static bool Load()
//     {
//       I = Xml.XMLUtil<OperationRecords>.LoadBinXml(FILE);
//       if (I == null)
//       {
//         I = new OperationRecords();
//         return false;
//       }
//       return true;
//     }
//     public static void PushZeroTime()
//     {
//       I.LastZeroLocal.Add(DateTime.Now);
//       Save();
//     }
//     public static DateTime LastZero()
//     {
//       if (I.LastZeroLocal.Count == 0)
//         return DateTime.MinValue;
//       return I.LastZeroLocal.Last();
//     }
//     public static void PushStartTime()
//     {
//       I.StartTime.Add(DateTime.Now);
//       Save();
//     }
//     public static void PushStopTime()
//     {
//       I.StopTime.Add(DateTime.Now);
//       Save();
//     }
//     public static BeginEnd StuckBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.StuckTime.Add(be);
//       Save();
//       return be;
//     }
//     public static void End(BeginEnd be)
//     {
//       be.End = DateTime.Now;
//       Save();
//     }
//     public static BeginEnd HaltBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.HaltTime.Add(be);
//       Save();
//       return be;
//     }
// 
//     public static BeginEnd OutBlowCountBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.OutBlowCountTime.Add(be);
//       Save();
//       return be;
//     }
// 
//     public static BeginEnd DepressionBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.DepressionTime.Add(be);
//       Save();
//       return be;
//     }
// 
//     public static BeginEnd HardwareBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.HardwareTime.Add(be);
//       Save();
//       return be;
//     }
// 
//     public static BeginEnd SoftwareBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.SoftwareTime.Add(be);
//       Save();
//       return be;
//     }
//     public static BeginEnd BufferFullBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.BufferFullTime.Add(be);
//       Save();
//       return be;
//     }
// 
//     public static BeginEnd LockedBegin()
//     {
//       BeginEnd be = new BeginEnd();
//       be.Begin = DateTime.Now;
//       I.LockedTime.Add(be);
//       Save();
//       return be;
//     }
//     #endregion
// 
//     public List<DateTime> LastZeroLocal = new List<DateTime>();
//     public List<DateTime> StartTime = new List<DateTime>();
//     public List<DateTime> StopTime = new List<DateTime>();
//     public List<BeginEnd> StuckTime = new List<BeginEnd>();
//     public List<BeginEnd> HaltTime = new List<BeginEnd>();
//     public List<BeginEnd> OutBlowCountTime = new List<BeginEnd>();
//     public List<BeginEnd> DepressionTime = new List<BeginEnd>();
//     public List<BeginEnd> BufferFullTime = new List<BeginEnd>();
//     public List<BeginEnd> LockedTime = new List<BeginEnd>();
//     public List<BeginEnd> SoftwareTime = new List<BeginEnd>();
//     public List<BeginEnd> HardwareTime = new List<BeginEnd>();
//   }
//   [Serializable]
//   public class BeginEnd
//   {
//     public DateTime Begin = DateTime.MinValue;
//     public DateTime End = DateTime.MaxValue;
//   }
// }
