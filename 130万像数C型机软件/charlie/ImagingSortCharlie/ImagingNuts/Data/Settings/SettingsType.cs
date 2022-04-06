using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagingSortCharlie.Data.Settings
{
  [Serializable]
  public enum TEAM
  {
    TEAM1 = 0,
    TEAM2,
    TEAM3,
    TEAM4,
    TEAM5,
    TEAMCOUNT
  }

  public enum VIEW
  {
    NONE = -1,
    VIEW1 = 0,
    VIEW2,
    VIEW3,
    VIEW4,
    VIEW5,
    VIEW6,
    VIEW7,
    VIEW8,
    VIEW_COUNT
  }

  public enum STATION
  {
    STATION1 = 0,
    STATION2,
    STATION3,
    STATION4,
    STATION_COUNT
  }

}
