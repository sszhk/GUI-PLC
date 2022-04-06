#pragma  once
#include "string"
#include "NICalc.h"
#include "NIPoints.h"


#pragma pack(push, 4)
struct DiameterOptions
{
  Annulus ann;
  BOOL insideToOutside;
  BOOL isWhite;
  BOOL isMaxDiameter;
  BOOL isMinDiameter;
  BOOL isCrack;
};
struct DiameterReport
{
  PointFloat center;
  double radius;
  double roundness;
  float minDiameter;
  float maxDiameter;
  PointFloat minDiaPt1, minDiaPt2;
  PointFloat maxDiaPt1, maxDiaPt2;
  float crack;
  int num_particle;
};
#pragma pack(pop)

float FindDistance(ImagePtr img, const RotatedRect*, BOOL inside, BOOL horizontal, int threshold, PointFloat* first, PointFloat *last, bool displaystatus);
float FindHeadDistance(int idx, ContourID id, BOOL horizontal, int threshold, bool displaystatus);
void DrawSmallRect(ImagePtr img, PointFloat center, int size, const RGBValue* color);
void MakeFindEdgeOptions(FindEdgeOptions* options, int thres, int steep, int width, double subsamplingratio, BOOL showsimple, BOOL showall);
void DisplayPointFloats(ImagePtr img, const PointFloat* pts, int count, const RGBValue* colors, int numColor, float dx, float dy);
StraightEdgeReport* FindEdgeSimple(int idx, const RotatedRect*, RakeDirection dir, FindEdgeOptions* feo);
StraightEdgeReport* FindEdgeSimple(ImagePtr img, const RotatedRect* rc, 
                                   RakeDirection dir, FindEdgeOptions* feo);
BOOL GetBounding(int idx, ContourID id, Rect* bounding);
CircularEdgeReport* FindDiameter(int idx, const Annulus*, BOOL insideToOutside, int thres, BOOL showSimple = TRUE, BOOL ShowAll = TRUE, int gap = 10, SpokeReport** origReport =NULL);
CircularEdgeReport* FindDiameter(ImagePtr img, const Annulus* an, BOOL insideToOutside, int thres, BOOL showSimple = TRUE, BOOL ShowAll = TRUE, int gap = 10, SpokeReport** origReport =NULL);
PointFloat MidPoint(const PointFloat* p1, const PointFloat* p2);
PointFloat MirrorPoint(const PointFloat& start, const PointFloat& center);
Curve* get_max_curve(Curve* cvs, int num);
BOOL normalize_rect(ImagePtr img, const Rect* src, Rect* dst);
BOOL CalcDiameter(ImagePtr img, const DiameterOptions* dia_opt, DiameterReport *dia_rpt );
void DrawError(ImagePtr img, PointFloat center, int size, const RGBValue* color);

inline void OverlayDistance(ImagePtr img, const PointFloat& p1, const PointFloat& p2, const RGBValue& rgb)
{
  imaqOverlayLineWithArrow(img, 
    imaqMakePointFromPointFloat(p1), 
    imaqMakePointFromPointFloat(p2), 
    &rgb, TRUE, TRUE, NULL);
}
#define AUTO_NI_ERROR 9999

std::string debug_ni_err(int err_code = AUTO_NI_ERROR);

inline int GetSafeIndex(int idx, int total)
{
  //   if( total <= 0 )
  //     return 0;

  while(idx<0)
    idx += total;
  return (idx % total);
}

inline int GetCircleIndex(int idx, int total)
{
  int size2 = total/2;
  idx = (idx+size2);
  return GetSafeIndex(idx, total);
}



class PointPair
{
  PointFloat pt1, pt2;
  float distance;
  float Distance()
  {
    return point2point(pt1, pt2);
  }
public:
  float GetDistance() const
  {
    return distance;
  }
  PointPair()
  {
    pt1 = imaqMakePointFloat(0,0);
    pt2 = pt1;
    distance = Distance();
  }
  PointPair(const PointPair& pp)
  {
    *this = pp;
  }

  PointPair& operator=(const PointPair& pp)
  {
    pt1 = pp.pt1;
    pt2 = pp.pt2;
    distance = pp.distance;
    return *this;
  }
  PointPair(const PointFloat& p1, const PointFloat& p2)
  {
    pt1 = p1;
    pt2 = p2;
    distance = Distance();
  }
  PointFloat GetPt1() const {return pt1;}
  PointFloat GetPt2() const {return pt2;}
};

class PointPairSon: public PointPair
{
public:
  PointPairSon(const PointFloat& p1, const PointFloat& p2): PointPair(p1, p2) {}
  operator float() const
  {
    return PointPair::GetDistance();
  }
};

static bool operator>(const PointPair& pp1, const PointPair& pp2)
{
  return pp1.GetDistance()>pp2.GetDistance();
}
static bool operator<(const PointPair& pp1, const PointPair& pp2)
{
  return pp1.GetDistance()<pp2.GetDistance();
}
static bool operator==(const PointPair& pp1, const PointPair& pp2)
{
  return pp1.GetDistance()==pp2.GetDistance();
}

