#pragma once

#include "float.h"
#define _USE_MATH_DEFINES
#include "math.h"

struct PointFloat_struct;
typedef struct PointFloat_struct PointFloat;
struct Point_struct;
typedef struct Point_struct Point;
typedef vector<PointFloat> Points;

#ifndef SQUARE_D
#define SQUARE_D(x) ((x)*(x))
#endif

struct point
{
  float x;
  float y;
  point():x(0),y(0){}
  point(double _x, double _y) {x=(float)_x;y=(float)_y;}
  point(float _x, float _y) {x=_x;y=_y;}
  point(int _x, int _y) {x=(float)_x; y=(float)_y;}
  PointFloat& ptf()
  {
    return *(PointFloat*)this;
  }
  const PointFloat& ptf() const
  {
    return *(const PointFloat*)this;
  }
  double distance(const point& pt)
  {
    return sqrt(SQUARE_D(x-pt.x)+SQUARE_D(y-pt.y));
  }
  point midpoint(const point& pt)
  {
    return point((x+pt.x)/2, (y+pt.y)/2);
  }
  double angle_as_p2(const point& p1, const point& p3)
  {
    double a1 = atan2(p1.y-y, p1.x-x)*180/M_PI;
    double a2 = atan2(p3.y-y, p3.x-x)*180/M_PI;
    double da = a2-a1;
    if( da < 0 )
      da += 360;
    return da;
  }
  Point pt() const;
  //   const PointFloat& CPT() const
  //   {
  //     return *(PointFloat*)this;
  //   }
  const PointFloat* pf() const
  {
    return (const PointFloat*)this;
  }
  PointFloat* pf()
  {
    return (PointFloat*)this;
  }
  point(const point& pt) {*this =pt;}
  point& operator=(const point& pt)
  {
    x = pt.x;
    y = pt.y;
    return *this;
  }
  point(const PointFloat& pt);
  point(const Point& pt);
  point& operator=(const PointFloat& pt);
  point& rotate(const point& at, float angle)
  {
    rotate_point(at.x, at.y, angle, x, y);
    return *this;
  }
  point& offset(int cx, int cy)
  {
    offset(float(cx), float(cy));
    return *this;
  }
  point& offset(float cx, float cy)
  {
    x += cx;
    y += cy;
    return *this;
  }
  point& offset(const point& pt)
  {
    return offset(pt.x, pt.y);
  }
  //////////////////////////////////////////////////////////////////////////

  static void rotate_point(float cx, float cy, float angle, float& x, float& y)
  {
    x -= cx;
    y -= cy;

    float radian = (float)(angle*M_PI/180);
    float sinx = sin(radian);
    float cosx = cos(radian);

    float xx = x;
    float yy = y;

    x = xx * cosx - yy * sinx;
    y = yy * cosx + xx * sinx;

    x += cx;
    y += cy;
  }
  static void rotate_point(float cx, float cy, float angle, int& xx, int& yy)
  {
    float x = (float)xx, y = (float)yy;
    rotate_point(cx, cy, angle, x, y);
    xx = (int)(x+0.5);
    yy = (int)(y+0.5);
  }
};
extern bool operator==(const point& pt1, const point& pt2);
extern bool operator<(const point& pt1, const point& pt2);

typedef std::pair<point, point> point_pair;
typedef std::vector<point_pair> point_pairs;

typedef std::vector<point> points;
extern void fit_line_iii(const points& pts, int from, int to, float& a, float& b, float& c);

void get_points(const PointFloat* pts, int num, points& contour);
void match_pairs(const points& contour, 
                 const point& center,
                 point_pairs& pairs,
                 bool isfitmax,
                 float r);
void filter_pairs(points& contour, 
                  const point& center, 
                  const float radius,
                  float tolerance);  // 0.2 : <= 20%
void format_points(const PointFloat* pts, int num);
void format_points(const Points& pts);

// inline Point point_int(const point& pt)
// {
//   Point pt1={(int)(pt.x+0.5f), (int)(pt.y+0.5f)};
//   return pt1;
// }
