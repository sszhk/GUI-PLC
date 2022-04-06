#pragma once

#include "Colors.h"
#include "NICalc.h"
#include "point.h"
typedef PointFloat PT;
typedef const PT CPT;
typedef vector<PointFloat> Points;
extern float point2point(const point& a, const point& b);
extern float point2point(const Point& a, const Point& b);
extern float point2point(CPT& a, CPT& b);
extern float point2line(CPT& p, CPT& a, CPT& b, bool abs);
extern float point2line(CPT& pt, float a, float b, float c, bool abs);
extern float point2line(CPT& pt, const BestLine* bl);
extern float line2line(const Points& l1, const Points& l2);
extern float line2line(CPT& p1, CPT& p2, CPT& p3, CPT& p4);
extern bool line_intersect(PT& node, CPT& s1, CPT& e1, CPT& s2, CPT& e2);
extern float SumDistance(const Points& ptfs);
extern float Angle(CPT& p1, CPT& p2, CPT& p3);

extern void draw_line(ImagePtr img, const PointFloat& start, const PointFloat& end, 
					 int dx = 0, int dy = 0,
					 const RGBValue* cl=&ColorRed, 
           BOOL endArrow=FALSE,
           BOOL startArrow=FALSE);
extern void draw_line(ImagePtr img, BestLine* l, const RGBValue* cl/* =&ColorRed*/, BOOL endArrow/*=false*/);
extern void draw_circle(ImagePtr img, CPT& center, float radius, const RGBValue* color);
extern void draw_polygon(ImagePtr img, CPT* contour, int num, const RGBValue* color);
extern void draw_rect(ImagePtr img, CPT& center, float radius, const RGBValue* color);
extern void draw_points(ImagePtr img, CPT* pts, int num, 
                       const RGBValue* cl, int dx=0, int dy=0, bool cross=false);
extern Point get_point(CPT& pt1, float dx = 0, float dy = 0);
extern void offset_point(PointFloat& pt1, float dx, float dy);
extern void offset_point(PointFloat& pt1, const Rect& rc);
extern void offset_point(PointFloat& pt1, const RotatedRect& rc);

typedef enum 
{
  DONT_INTERSECT,
  DO_INTERSECT,
  COLLINEAR,
  PARALLEL,
} IntersectResult;

extern IntersectResult LineIntersect(const PointFloat& p1, 
                                     const PointFloat& p2, 
                                     const PointFloat& p3, 
                                     const PointFloat& p4, 
                                     PointFloat& inter);
