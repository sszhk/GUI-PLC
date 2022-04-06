using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ImagingSortCharlie.Data.Filters;
using System.Xml.Serialization;
using ImagingSortCharlie.Data.Settings;

public static class IACalc
{
  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindAngle([MarshalAs(UnmanagedType.Struct)] 
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref AngleOptions ao,
    [MarshalAs(UnmanagedType.Struct)]
      ref AngleReport ar);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindDiameter([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref DiameterOptions dop,
    [MarshalAs(UnmanagedType.Struct)]
      ref DiameterReport dr);
  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindMarking([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref MarkingOptions mp,
    [MarshalAs(UnmanagedType.Struct)]
      ref MarkingReport mr);

  [DllImport("IMAGEANA.DLL")]

  public extern static bool IA_Calc_FindDistance([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref DistanceOptions dop,
    [MarshalAs(UnmanagedType.Struct)]
      ref DistanceReport dr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindHead([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref HeadOptions ho,
    [MarshalAs(UnmanagedType.Struct)]
      ref HeadReport hr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindTeeth([MarshalAs(UnmanagedType.Struct)] 
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref TeethOptions to,
    [MarshalAs(UnmanagedType.Struct)]
      ref TeethReport tr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindNode([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref NodeOptions no,
    [MarshalAs(UnmanagedType.Struct)] 
      ref NodeReport nr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindHexagon([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref HexagonOptions ho,
    [MarshalAs(UnmanagedType.Struct)] 
      ref HexagonReport hr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindSquare([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref SquareOptions so,
    [MarshalAs(UnmanagedType.Struct)] 
      ref SquareReport sr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindCrack([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref CrackOptions co,
    [MarshalAs(UnmanagedType.Struct)] 
      ref CrackReport cr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindStarving([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref StarvingOptions so,
    [MarshalAs(UnmanagedType.Struct)] 
      ref StarvingReport sr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindShape([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref FindShapeOptions so,
    [MarshalAs(UnmanagedType.Struct)] 
      ref FindShapeReport sr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_RingCrack([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref RingCrackOptions rco,
    [MarshalAs(UnmanagedType.Struct)] 
      ref RingCrackReport rcr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_Matching([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref MatchingOptions mo,
    [MarshalAs(UnmanagedType.Struct)] 
      ref MatchingReport mr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_ThreadDamage([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref ThreadDamageOptions tdo,
    [MarshalAs(UnmanagedType.Struct)] 
      ref ThreadDamageReport tdr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_Threads([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref ThreadsOptions to,
    [MarshalAs(UnmanagedType.Struct)]
      ref ThreadsReport tr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FindThreadLocating([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref ThreadLocatingOptions tlo,
    [MarshalAs(UnmanagedType.Struct)]
      ref ThreadLocatingReport tlr);
  [DllImport("IMAGEANA.DLL")]

  public extern static bool IA_Calc_Cushion([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref CushionOptions co,
    [MarshalAs(UnmanagedType.Struct)]
      ref CushionReport cr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_FillArea([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref FillAreaOptions fo,
    [MarshalAs(UnmanagedType.Struct)]
      ref FillAreaReport fr);

  [DllImport("IMAGEANA.DLL")]
  public extern static bool IA_Calc_Area([MarshalAs(UnmanagedType.Struct)]
      ref PublicOptions po,
    [MarshalAs(UnmanagedType.Struct)]
      ref AreaOptions ao,
    [MarshalAs(UnmanagedType.Struct)]
      ref AreaReport ar);


  #region -检测内存
  [StructLayout(LayoutKind.Sequential)]
  public struct MEMORY_INFO
  {
    public uint dwLength;
    public uint dwMemoryLoad;
    public uint dwTotalPhys;
    public uint dwAvailPhys;
    public uint dwTotalPageFile;
    public uint dwAvailPageFile;
    public uint dwTotalVirtual;
    public uint dwAvailVirtual;
  }
  [DllImport("kernel32.dll")]
  private static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
  out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes,
  out ulong lpTotalNumberOfFreeBytes);
  [DllImport("kernel32")]
  public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
  public static ulong GetFreeSpace(string driveDirectoryName)
  {
    ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;
    if (!driveDirectoryName.EndsWith(":\\"))
    {
      driveDirectoryName += ":\\";
    }
    GetDiskFreeSpaceEx(driveDirectoryName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
    return freeBytesAvailable;
  }
  public static float GetMemoryStatus()
  {
    MEMORY_INFO MemInfo;
    MemInfo = new MEMORY_INFO();
    GlobalMemoryStatus(ref MemInfo);

    long totalMb = Convert.ToInt64(MemInfo.dwTotalPhys.ToString()) / 1024 / 1024;
    long avaliableMb = Convert.ToInt64(MemInfo.dwAvailPhys.ToString()) / 1024 / 1024;
    float rate = (float)avaliableMb / (float)totalMb;
    return rate;
  }
  #endregion

}

namespace ImagingSortCharlie.Data.Filters
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct BinarizeOptions
  {
    public bool Inverse;//反色
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct AngleReport
  {
    public float angle;
    public float ratio;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct AngleOptions
  {
    //public NIRotatedRect RotatedRectangle;
    public NIAnnulus Annulus;
    public bool Horizontal;
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct DiameterOptions
  {
    public NIAnnulus Annulus;
    public bool InsideToOutside;
    public bool IsWhite;
    public bool MaxRadius;
    public bool MinRadius;
    public bool IsCrack;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct DiameterReport
  {
    public NIPointFloat center;
    public double radius;
    public double roundness;
    public float minDiameter;
    public float maxDiameter;
    public NIPointFloat minDiaPt1, minDiaPt2;
    public NIPointFloat maxDiaPt1, maxDiaPt2;
    public float crack;
    public int particle;
  }
  public struct MarkingOptions
  {
    public NIAnnulus Annulus;
    public bool IsWhite;
    public float MaxMarkingArea;
    public float MinMarkingArea;
    public float MaxRoundness;
    public float MinRoundness;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct MarkingReport
  {
    public int count_marking;
    public float max_area_marking;
    public float min_area_marking;
    public float max_roundness;
    public float min_roundness;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct DistanceReport
  {
    public float distance;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct DistanceOptions
  {
    public bool AngleSensitive;
    public bool InsideToOutside;
    public bool Horizontal;
    public NIRect Rectangle;
    public bool ordinal;
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct ThreadsReport
  {
    public int threads;
    public float pitch_max;
    public float pitch_min;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct ThreadsOptions
  {
    public bool IsWhite;
    public NIRect Rectangle;
    public float MinArea;
    public float MaxArea;

    //public NIAnnulus Annulus;
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct HeadReport
  {
    public float width;
    public float depth;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct HeadOptions
  {
    public NIRect Rectangle;
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct TeethOptions
  {
    public NIRect Rectangle;
    public NIRect TeethRectangle;
    //public NIRect RatioRectangle;
    public bool MaxOutDiameter;
    public bool MinOutDiameter;
    public bool MidDiameter;
    public bool BottomDiameter;
    public bool Pitch;
    public bool Length;
    public bool Slopeteeth;
    public bool HelixAngle;
    public bool TeethCount;
    public bool Ratio;
    public bool Cylinder;
    public bool Pilot;
    public int Kernel;
    public bool AngleSens;
    public float Offset_X;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct TeethReport
  {
    public int teeth1;
    public int teeth2;
    public float avr_teeth_distance;
    public float maxTeeth;
    public float minTeeth;
    public float outerDiameter;
    public float innerDiameter;
    public float midDiameter;
    public float teethLenth;
    public float helixAngle;
    public float maxSlope;
    public float minSlope;
    public float ratio;
    public float cylinder;
    public float pilot;
    public NIPointFloat left_most;
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct CrackOptions
  {
    public NIAnnulus Annulus;
    public float CenterCrack;
    public bool IsWhite;
    public bool IsCenterCrack;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct CrackReport
  {
    public NIPointFloat center;
    public float centerCrack;
    public float sideCrack;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct WheelCrackOptions
  {
    public NIAnnulus Annulus;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct WheelCrackReport
  {
    public float wheelCrack;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct StarvingOptions
  {
    public NIAnnulus Annulus;
    public int MaxStarving;
    public int MinStarving;
    public bool IsWhite;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct StarvingReport
  {
    public float starving;
    public int particle;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct HexagonOptions
  {
    public NIAnnulus Annulus;
    public bool InsideToOutside;
    public bool IsWhite;
    public bool MaxDiagonal;
    public bool MinDiagonal;
    public bool MaxSubtense;
    public bool MinSubtense;
    public bool Concent;
    public bool Crack;
    public bool Open;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct HexagonReport
  {
    public NIPointFloat center;
    public float maxDiagonal;
    public float minDiagonal;
    public float minSubtense;
    public float maxSubtense;
    public float Crack;
    public int particle;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct SquareOptions
  {
    public NIAnnulus Annulus;
    public bool InsideToOutside;
    public bool IsWhite;
    public bool MaxDiagonal;
    public bool MinDiagonal;
    public bool MaxSubtense;
    public bool MinSubtense;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct SquareReport
  {
    public NIPointFloat center;
    public float maxDiagonal;
    public float minDiagonal;
    public float minSubtense;
    public float maxSubtense;
    public int particle;
  }


  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct NodeOptions
  {
    public NIRotatedRect RotatedRectangle;
    public bool IsWhite;
    public int Similarity;
    public float correctionArea;
    public bool Correction;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct NodeReport
  {
    public int nodesCount;
    public float maxNodeArea;
    public float minNodeArea;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct FindShapeOptions
  {
    public NIAnnulus Annulus;
    public bool IsWhite;
    public bool DelTrench;
    public bool Crack;
    public bool MaxRadius;
    public bool MinRadius;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct FindShapeReport
  {
    public float radius;
    public NIPointFloat center;
    public double roundness;
    public float minDiameter;
    public float maxDiameter;
    public NIPointFloat minDiaPt1, minDiaPt2;
    public NIPointFloat maxDiaPt1, maxDiaPt2;
    public float Crack;
    public bool result;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct RingCrackOptions
  {
    public NIAnnulus Annulus;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct RingCrackReport
  {
    public float ringCrack;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct MatchingOptions
  {
    public NIAnnulus Annulus;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct MatchingReport
  {
    public float alikeness;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct ThreadDamageReport
  {
    public float damege;
    public int top;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct ThreadDamageOptions
  {
    public NIRect Rectangle;
    public float ThreadDamage;
    public bool IsThreadDamaged;
    public int Height;
    public int Width;
    public float Contrast;
    public bool Rotated;
    public bool Correct;
    public int Offset;
  };

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct CushionReport
  {
    public float distance_up;
    public float distance_down;
    public float distance_average;
    public float width;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct CushionOptions
  {
    public NIRect Rectangle;
    public bool IsWhite;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct ThreadLocatingReport
  {
    public NIPointFloat center;
    public float x_right;
    public float width;
    public float angle;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct ThreadLocatingOptions
  {
    public NIRect Rectangle;
    public bool IsWhite;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct FillAreaReport
  {
    public NIPointFloat center;
    public int prticle;
  }
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct FillAreaOptions
  {
    public NIAnnulus Annulus;
    public bool IsWhite;
  }

  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct AreaReport
  {
    public float area;
  };
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct AreaOptions
  {
    public bool IsWhite;
    public NIRect Rectangle;
  };

  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct PublicOptions
  {
    public VIEW View;
    public int Threshold;
    public bool DisplayStatus;

    public PublicOptions(VIEW view, int threshold, bool displaystatus)
    {
      View = view;
      Threshold = threshold;
      DisplayStatus = displaystatus;
    }
  };

}