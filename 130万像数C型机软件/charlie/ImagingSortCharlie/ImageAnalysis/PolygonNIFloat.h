#pragma once

#include "NIPoints.h"
#include "float.h"
#define _USE_MATH_DEFINES
#include "math.h"
#include "auto_array.h"

const double EPSILON = DBL_EPSILON;
const double PI = M_PI;
const float PIf = ((float)M_PI);

const double INFINITE_DOUBLE = DBL_MAX;
const double INVALID_DOUBLE = INFINITE_DOUBLE-1;
const float INFINITE_FLOAT = FLT_MAX;
const float INVALID_FLOAT = INFINITE_FLOAT-1;
#define IS_DOUBLE_VALID(x) ((x)!=INVALID_DOUBLE)
#define IS_DOUBLE_INVALID(x) ((x)==INVALID_DOUBLE)
#define IS_FLOAT_VALID(x) ((x)!=INVALID_FLOAT)
#define IS_FLOAT_INVALID(x) ((x)==INVALID_FLOAT)
#define X_COEFFICIENT 0.2f
#define Y_COEFFICIENT 0.01f
#define STEEPNESS 0.6

static BOOL operator!=(const PointFloat& pt1, const PointFloat& pt2)
{
  return (pt1.x != pt2.x) || (pt1.y != pt2.y);
}

typedef vector<PointFloat> PolygonType;
typedef vector<PointFloat> Points;
typedef int IndexDistance;
typedef int Index;
typedef vector<Index> PointsIndex;
#define DEFAULT_POINT_TYPE float


#define SQUARE(x) ((float)((x)*(x)))
#define SQUARE_(x, y) (((y)x)*((y)x))
extern float Distance(const PointFloat& pt1, const PointFloat& pt2);
extern float Perimeter(const PolygonType* contour);
extern void GetPoints(CPT* pts, int count, 
                      auto_array<Point>& output, float dx = 0, float dy = 0);
extern void GetPointsInt(CPT* pts, int count, 
                      auto_array<Point>& output, int dx = 0, int dy = 0);

extern float radian2degree(float radian);
extern double radian2degree(double radian);
extern float degree2radian(float degree);
extern double degree2radian(double degree);
class PointWithIndex: public pair<PointFloat, int>
{
public:
  static BestLine* fitLine;
  PointWithIndex(const PointFloat& pt, int idx): pair(pt, idx) {}
  operator double() const
  {
    if( !fitLine )
      return 0;
    return point2line(first, fitLine);
  }
};
typedef vector<PointWithIndex> PointsWithIndex;

class PointSeg
{
  PointFloat start;
  PointFloat end;
public:
  PointSeg(const PointFloat& pt1, const PointFloat& pt2): start(pt1), end(pt2)
  {
  }
  PointSeg(const PointSeg& seg) {start=seg.start; end=seg.end;}
  PointSeg& operator=(const PointSeg& seg) {start=seg.start; end=seg.end; return *this;}
  PointFloat& S() {return start;}
  PointFloat& E() {return end;}
  double Distance(const PointFloat& pt) {return point2line(start, end, pt, false);}
};

typedef const PointFloat* CPointPtr;

extern void Enlighten(ImagePtr img, CPT& ptf, const RGBValue* color, bool cross=false);
BestLine* GuessLine(PointsWithIndex& pts);