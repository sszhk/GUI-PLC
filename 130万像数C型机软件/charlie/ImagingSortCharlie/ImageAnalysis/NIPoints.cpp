#include "stdafx.h"
#include "IAMain.h"
#include "NIPoints.h"
#include "PolygonNIFloat.h"
#include "point.h"

void offset_point(PointFloat& pt1, float dx, float dy)
{
  pt1.x += dx;
  pt1.y += dy;
}

void offset_point(PointFloat& pt1, const Rect& rc)
{
  offset_point(pt1, (float)rc.left, (float)rc.top);
}
void offset_point(PointFloat& pt1, const RotatedRect& rc)
{
  offset_point(pt1, (float)rc.left, (float)rc.top);
}

Point get_point(CPT& pt1, float dx/* = 0*/, float dy /*= 0*/)
{
  Point pt = imaqMakePointFromPointFloat(pt1);
  pt.x += (int)dx;
  pt.y += (int)dy;
  return pt;
}

// PointFloat Offset(const PointFloat& pt, float dx/* =0 */, float dy/* =0 */)
// {
// 	PointFloat another = pt;
// 	another.x += dx;
// 	another.y += dy;
// 	return another;
// }
void draw_rect(ImagePtr img, const RotatedRect& rr, const RGBValue* color)
{
  point center(rr.left+rr.width/2, rr.top+rr.height/2);
  point lefttop(rr.left, rr.top), 
    leftbottom(rr.left, rr.top+rr.height), 
    righttop(rr.left+rr.width, rr.top), 
    rightbottom(rr.left+rr.width, rr.top+rr.height);
  lefttop.rotate(center, (float)-rr.angle);
  leftbottom.rotate(center, (float)-rr.angle);
  righttop.rotate(center, (float)-rr.angle);
  rightbottom.rotate(center, (float)-rr.angle);
  imaqOverlayLineWithArrow(img, lefttop.pt(), leftbottom.pt(), color, FALSE, FALSE, NULL);
  imaqOverlayLineWithArrow(img, lefttop.pt(), righttop.pt(), color, FALSE, FALSE, NULL);
  imaqOverlayLineWithArrow(img, righttop.pt(), rightbottom.pt(), color, FALSE, FALSE, NULL);
  imaqOverlayLineWithArrow(img, leftbottom.pt(), rightbottom.pt(), color, FALSE, FALSE, NULL);
}

void draw_rect(ImagePtr img, CPT& center, float radius, const RGBValue* color)
{
  Rect rc;
  rc.left = ROUND_TO_INT(center.x - radius);
  rc.top = ROUND_TO_INT(center.y - radius);
  rc.width = ROUND_TO_INT(radius*2);
  rc.height = ROUND_TO_INT(radius*2);
  imaqOverlayRect(img, rc, color, IMAQ_DRAW_VALUE, NULL);
}
void draw_circle(ImagePtr img, CPT& center, float radius, const RGBValue* color)
{
  Rect rc;
  rc.left = ROUND_TO_INT(center.x - radius);
  rc.top = ROUND_TO_INT(center.y - radius);
  rc.width = ROUND_TO_INT(radius*2);
  rc.height = ROUND_TO_INT(radius*2);
  imaqOverlayOval(img, rc, color, IMAQ_DRAW_VALUE, NULL);
}
void draw_polygon(ImagePtr img, CPT* contour, int num, const RGBValue* color)
{
  auto_array<Point> pts;
  GetPointsInt(contour, num, pts);
  imaqOverlayClosedContour(img, pts, num, color, IMAQ_DRAW_VALUE, NULL);
}

void draw_line(ImagePtr img, const PointFloat& start, const PointFloat& end, 
               int dx, int dy,
               const RGBValue* cl/* =&ColorRed*/, 
               BOOL endArrow/*=false*/, 
               BOOL startArrow/*=false*/)
{
  imaqOverlayLineWithArrow(img, 
    get_point(start, (float)dx, (float)dy), 
    get_point(end, (float)dx, (float)dy), 
    cl, 
    startArrow, 
    endArrow, 
    NULL);
}


void draw_line(ImagePtr img, BestLine* l, const RGBValue* cl/* =&ColorRed*/, BOOL endArrow/*=false*/)
{
  imaqOverlayLineWithArrow(img, get_point(l->start), get_point(l->end), cl, FALSE, endArrow, NULL);
}

float point2point(const Point& a, const Point& b)
{
  float d;
  float dx, dy;

  dx = (float)(a.x - b.x);
  dy = (float)(a.y - b.y);
  d = sqrt(dx*dx + dy*dy);
  return(d);
}

float point2point(const point& a, const point& b)
{
  return point2point(a.ptf(), b.ptf());
}

float point2point(CPT& a, CPT& b)
{
  float d;
  float dx, dy;

  dx = a.x - b.x;
  dy = a.y - b.y;
  d = sqrt(dx*dx + dy*dy);
  return(d);
}

float point2line(CPT& p, CPT& a, CPT& b, bool abs)
{//计算点到线段(a,b)的距离  
  float l; /* length of line ab */ 
  float r,s; 
  l = point2point(a,b);   
  if(l == 0.0) /* a = b */   
    return( point2point(a,p)); 
  r = ((a.y - p.y)*(a.y - b.y) - (a.x - p.x)*(b.x - a.x))/(l*l);   
  //   if(r > 1) /* perpendicular projection of P is on the forward extention of AB */    
  //     return(min(msDistancePointToPoint(p, b),msDistancePointToPoint(p, a)));  
  //   if(r < 0) /* perpendicular projection of P is on the backward extention of AB */   
  //     return(min(msDistancePointToPoint(p, b),msDistancePointToPoint(p, a)));  

  s = ((a.y - p.y)*(b.x - a.x) - (a.x - p.x)*(b.y - a.y))/(l*l);
  s *= l;
  return abs?fabs(s):s;
}

// double point2line(CPT& start, CPT& end, CPT& pt)
// {
//   return point2line(&pt, &start, &end);
// }
float point2line(CPT& pt, float a, float b, float c, bool abs)
{
  //   double a = bl->equation.a;
  //   double b = bl->equation.b;
  //   double c = bl->equation.c;
  float x = pt.x;
  float y = pt.y;
  float result = (a*x + b*y+c)/sqrtf(a*a+b*b);
  return abs?fabs(result):result;
}

float point2line(const PointFloat& pt,const BestLine* bl)
{
  // http://topic.csdn.net/t/20060223/13/4572823.html
  // 求P(x0, y0) 到直线Ax＋By＋C=0的距离
  // abs(Ax0+By0+c)   /   sqrt(A*A+B*B)  
  return point2line(pt, 
    (float)bl->equation.a, 
    (float)bl->equation.b,
    (float)bl->equation.c, false);
  //   double a = bl->equation.a;
  //   double b = bl->equation.b;
  //   double c = bl->equation.c;
  //   double x = pt.x;
  //   double y = pt.y;
  //   return /*abs*/(a*x + b*y+c)/sqrt(a*a+b*b);
}

float line2line(const Points& l1, const Points& l2)
{
  if( l1.empty() || l2.empty() )
    return 0;

  // p1 ---------------- p2
  //
  // p3 ---------------- p4
  const PointFloat& p1 = l1.front();
  const PointFloat& p2 = l1.back();
  const PointFloat& p3 = l2.front();
  const PointFloat& p4 = l2.back();

  float d1 = point2line(p1, p3, p4, false);
  float d2 = point2line(p2, p3, p4, false);
  return (d1+d2)/2;
}

float line2line(CPT& p1, CPT& p2, CPT& p3, CPT& p4)
{
  // p1 ---------------- p2
  //
  // p3 ---------------- p4
  float d1 = point2line(p1, p3, p4, false);
  float d2 = point2line(p2, p3, p4, false);
  return (d1+d2)/2;
}
//两条直线的交点
bool line_intersect(PT& node, CPT& s1, CPT& e1, CPT& s2, CPT& e2)
{
  if (e1.x == s1.x&&e2.x == s2.x)
  {
    return false;
  }
  if (e1.x == s1.x&&e2.x != s2.x)
  {
    node.x = e1.x;
    node.y = (e2.y - s2.y)/(e2.x - s2.x)*(node.x - s2.x) + s2.y;
  }
  if (e2.x == s2.x&&e1.x != s2.x)
  {
    node.x = e2.x;
    node.y = (e1.y - s1.y)/(e1.x - s1.x)*(node.x - s1.x) +s1.y; 
  }
  if (e1.x != s1.x&&e2.x != s2.x)
  {
    float k1 = (e1.y - s1.y)/(e1.x - s1.x);
    float k2 = (e2.y - s2.y)/(e2.x - s2.x);
    if (k1 == k2)
    {
      return false;
    }
    float b1 = e1.y - k1*e1.x;
    float b2 = e2.y - k2*e2.x;
    node.x = (b1 - b2)/(k2 - k1);
    node.y = k1*node.x + b1;
  }
  return true;
}

static BOOL SAME_SIGNS(double a, double b) {
  //   IN_FUNCTION;
  //   lg<<PARAM(a)<<PARAM(b)<<endl;
  return (a * b) >= 0; }

IntersectResult LineIntersect(const PointFloat& p1, 
                              const PointFloat& p2, 
                              const PointFloat& p3, 
                              const PointFloat& p4, 
                              PointFloat& inter)
{
  //   IN_FUNCTION;
  //   lg<<PARAM(p1.x)<<PARAM(p1.y)<<PARAM(p2.x)<<PARAM(p2.y)<<PARAM(p3.x)<<PARAM(p3.y)
  //     <<PARAM(p4.x)<<PARAM(p4.y)<<PARAM(inter.x)<<PARAM(inter.y)<<endl;
  //inter = new Coord(0, 0);

  double x1, y1, x2, y2, x3, y3, x4, y4;
  x1 = p1.x; y1 = p1.y;
  x2 = p2.x; y2 = p2.y;
  x3 = p3.x; y3 = p3.y;
  x4 = p4.x; y4 = p4.y;

  double a1, a2, b1, b2, c1, c2; /* Coefficients of line eqns. */
  double r1, r2, r3, r4;         /* 'Sign' values */
  double denom, offset, num;     /* Intermediate values */

  /* Compute a1, b1, c1, where line joining points 1 and 2
  * is "a1 x  +  b1 y  +  c1  =  0".
  */

  a1 = y2 - y1;
  b1 = x1 - x2;
  c1 = x2 * y1 - x1 * y2;

  /* Compute r3 and r4.
  */


  r3 = a1 * x3 + b1 * y3 + c1;
  r4 = a1 * x4 + b1 * y4 + c1;

  /* Check signs of r3 and r4.  If both Coord 3 and Coord 4 lie on
  * same side of line 1, the line segments do not intersect.
  */

  if (r3 != 0 &&
    r4 != 0 &&
    SAME_SIGNS(r3, r4))
    return (DONT_INTERSECT);

  /* Compute a2, b2, c2 */

  a2 = y4 - y3;
  b2 = x3 - x4;
  c2 = x4 * y3 - x3 * y4;

  /* Compute r1 and r2 */

  r1 = a2 * x1 + b2 * y1 + c2;
  r2 = a2 * x2 + b2 * y2 + c2;

  /* Check signs of r1 and r2.  If both Coord 1 and Coord 2 lie
  * on same side of second line segment, the line segments do
  * not intersect.
  */

  if (r1 != 0 &&
    r2 != 0 &&
    SAME_SIGNS(r1, r2))
    return (DONT_INTERSECT);

  /* Line segments intersect: compute intersection Coord. 
  */

  denom = a1 * b2 - a2 * b1;
  if (denom == 0)
    return (COLLINEAR);
  offset = denom < 0 ? -denom / 2 : denom / 2;

  /* The denom/2 is end get rounding instead of truncating.  It
  * is added or subtracted end the numerator, depending upon the
  * sign of the numerator.
  */

  num = b1 * c2 - b2 * c1;
  inter.x = (float)((num < 0 ? num - offset : num + offset) / denom);

  num = a2 * c1 - a1 * c2;
  inter.y = (float)((num < 0 ? num - offset : num + offset) / denom);

  return (DO_INTERSECT);
}

void Enlighten(ImagePtr img, CPT& ptf, const RGBValue* color, bool cross)
{
  Point pt = imaqMakePointFromPointFloat(ptf);
  //          pt.x += dx;
  //          pt.y += dy;
  imaqOverlayPoints(img, &pt, 
    1, 
    color,
    1, 
    cross?IMAQ_POINT_AS_CROSS:IMAQ_POINT_AS_PIXEL, 
    NULL, NULL);
}
float degree2radian(float degree)
{
  return degree*PIf/180;
}
double degree2radian(double degree)
{
  return degree*PIf/180;
}
float radian2degree(float radian)
{
  return radian*180/PIf;
}
double radian2degree(double radian)
{
  return radian*180/PI;
}

float SumDistance(const Points& ptfs)
{
  float distance = 0;
  for (UINT i=1; i<ptfs.size(); i++)
  {
    distance += point2point(ptfs[i], ptfs[i-1]);
  }
  return distance;
}

//三角形的顶角
float Angle(CPT& p1, CPT& p2, CPT& p3)
{
  float a = point2point(p2, p3);
  float b = point2point(p1, p2);
  float c = point2point(p1, p3);
  float m = (b*b + c*c - a*a)/(2*b*c);
  float angle = acos(m);
  return angle*180/PIf;
}

void draw_points(ImagePtr img, CPT* pts, int num, const RGBValue* cl, 
                 int dx, int dy,
                 bool cross)
{
  auto_array<Point> ptsx;
  GetPointsInt(pts, num, ptsx, dx, dy);
  imaqOverlayPoints(img, ptsx, num, cl, 1, 
    cross?IMAQ_POINT_AS_CROSS:IMAQ_POINT_AS_PIXEL, NULL, NULL);
}

