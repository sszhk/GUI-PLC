#pragma once

#include "vector"
// #include "set"
// #include "map"
#include "algorithm"
#include "point.h"


// typedef std::set<float> polygon_y;
// typedef std::pair<float, polygon_y > polygon_subtense;
// typedef std::map<float, polygon_y > polygon;

struct moments
{
  // m00 is the area of the polygon
  double  m00, m10, m01, m20, m11, m02, m30, m21, m12, m03; /* spatial moments */
  double  mu20, mu11, mu02, mu30, mu21, mu12, mu03; /* central moments */
  double  inv_sqrt_m00; /* m00 != 0 ? 1/sqrt(m00) : 0 */
  double  orientation; // in degrees
  double  cx, cy;      // centroid coordinates
};

void polygon_moments( const points& contour, moments* moms );
float polygon_area(const point* c, int size);
// #ifdef PointFloat

void translate_points(const PointFloat* src, int count, points& pts, int dx=0, int dy=0);
// #endif

#define POLYGON_AREA(moms) (moms->m00)

