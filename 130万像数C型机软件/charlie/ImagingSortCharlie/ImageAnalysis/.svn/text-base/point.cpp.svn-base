#include "stdafx.h"
#include "point.h"
#include "nivision.h"

point::point(const PointFloat& pt)
{
  *this = pt;
}

point& point::operator=(const PointFloat& pt)
{
  x = pt.x;
  y = pt.y;
  return *this;
}
bool operator<(const point& pt1, const point& pt2)
{
  if( pt1.x < pt2.x )
    return true;
  else if( pt1.x == pt2.x )
    return pt1.y < pt2.y;
  return false;
}
point::point(const Point& pt):x((float)pt.x), y((float)pt.y){}
Point point::pt() const
{
  Point pt = {(int)(x+0.5f), (int)(y+0.5f)};
  return pt;
}

void get_points(const PointFloat* pts, int num, points& contour)
{
  contour.clear();
  contour.assign(pts, pts+num);
}

bool operator==(const point& pt1, const point& pt2)
{
  return pt1.x == pt2.x && pt1.y == pt2.y;
}