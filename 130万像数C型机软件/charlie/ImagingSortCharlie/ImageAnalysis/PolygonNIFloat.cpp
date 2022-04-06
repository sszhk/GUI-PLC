#include "StdAfx.h"
#include "IAMain.h"
#include "PolygonNIFloat.h"
#include "Utilities.h"
#include "algorithm"
#include "set"
#include "hi_res_timer.h"
#include "auto_filter.h"
#include "point.h"

static bool operator==(CPT& pt1, CPT& pt2)
{
  return pt1.x == pt2.x && pt1.y == pt2.y;
}

BestLine* PointWithIndex::fitLine = NULL;
void GetPoints(const PointsWithIndex& pts, auto_array<Point>& outpts, auto_array<PointFloat>& outputsf,
               float dx = 0, float dy = 0)
{
  outpts.Alloc(pts.size());
  outputsf.Alloc(pts.size());
  for (UINT i=0; i<pts.size(); i++)
  {
    outpts[i] = get_point(pts[i].first, dx, dy);
    outputsf[i] = pts[i].first; 
  }
}

void GetPointsInt(CPT* pts, int count, auto_array<Point>& output, 
                  int dx /*= 0*/, int dy/* = 0*/)
{
  GetPoints(pts, count, output, (float)dx, (float)dy);
}
void GetPoints(CPT* pts, int count, auto_array<Point>& output, 
               float dx /*= 0*/, float dy/* = 0*/)
{
  output.Alloc(count);
  for (int i=0; i<count; i++)
  {
    output[i] = get_point(pts[i], dx, dy);
  }
}

bool operator<(const pair<double, PointSeg>& p1, const pair<double, PointSeg>& p2)
{
  return p1.first < p2.first;
}

#define SAME_SIGNS(a, b) (((a) * (b)) >= 0)

double triangle_area(double x1, double x2, double x3,
                     double y1, double y2, double y3)
{
  return fabs((x1*y2 + x2*y3 + x3*y1 - x1*y3 - x2*y1 - x3*y2)/2);
}
double triangle_area(const points p, int idx)
{
  const point& p1 = p[(idx-1+p.size())%p.size()];
  const point& p2 = p[(idx)%p.size()];
  const point& p3 = p[(idx+1)%p.size()];
  return triangle_area(p1.x, p2.x, p3.x, p1.y, p2.y, p3.y);
}

void extract_path(CPT* c, int num, points& e)
{
  // 好主意：2009-12-31 3：58
  // 连续3个点构成的三角形面积 < 一定值或比例
  // 若上述条件为真，则删除中间点
  // 否则保留，继续往后看……

  double aaa = triangle_area(316, 319, 326, 120, 135, 162);

  //get_points(c, num, e);
  if( num < 3 )
  {
    get_points(c, num, e);
    return;
  }

  points x;
  get_points(c, num, x);
  //AGAIN1:
  // 求2阶导
  std::vector<double> derive;
  double k0 = 0, k1 = 0;
  for (UINT i=0; i<x.size()-1; i++)
  {
    k1 = atan2(x[i+1].y-x[i].y, x[i+1].x-x[i].x);
    k1 = fmod(k1, M_PI);
    if( i==0 )
    {
    }
    else
    {
      derive.push_back(k1 - k0);
    }
    k0 = k1;
  }

  points y;
  y.push_back(x[0]);
  for (UINT i=0; i<derive.size(); i++)
  {
    if( fabs(derive[i]) > DBL_EPSILON )
      y.push_back(x[i+1]);
  }
  y.push_back(*x.rbegin());
  //   if( y.size() != x.size() )
  //   {
  //     x = y;
  //     goto AGAIN1;
  //   }
  e = points(y);
  typedef std::pair<double, points::iterator> tri_area_pair;
  typedef std::multimap<double, points::iterator> tri_area_type;
  tri_area_type tri;
  bool erased = true;
  int iterates = 0;
  while(erased)
  {
    erased = false;
    for (UINT i=1; i<e.size(); i++)
    {
      // 面积判定
      //       double a = triangle_area(e, i);
      //       //cerr<<i<<" "<<a<<endl;
      //       if( a < 5 )
      //       {
      //         e.erase(e.begin()+i);
      //         i--;
      //         erased = true;
      //       }
      //       else
      {
        // 距离判定
        double d = e[i-1].distance(e[i]);
        //cerr<<i<<" "<<d<<endl;
        if( d<3 )
        {
          point mid = e[i-1].midpoint(e[i]);
          //e.erase(e.begin()+i-1);
          e.erase(e.begin()+i);
          //e.insert(e.begin()+i, mid);
          i--;
          erased = true;
        }
      }
      //tri.insert(tri_area_pair(a, e.begin()+i));
    }
    iterates++;
  }
  //cerr<<"area and distance iterates: "<< iterates <<endl;

  // 角度判定
  erased = true;
  while (erased)
  {
    erased = false;
    for (UINT i=1; i<e.size(); i++)
    {
      double ag = e[i].angle_as_p2(e[i-1], e[(i+1)%e.size()]);
      if( fabs(ag-180) < 15 )
      {
        e.erase(e.begin()+i);
        i--;
        erased = true;
      }
    }
  }

  return;

  typedef std::list<PointFloat> point_list;
  point_list pts;
  for (int i=0; i<num; i++)
  {
    pts.push_back(c[i]);
  }
  typedef point_list::iterator point_it;
  double epsilon = 0.5;
AGAIN:
  while(true)
  {
    int before = pts.size();
    point_it p0 = pts.begin(), p1, p2;
    p1 = p0;
    p1++;
    p2 = p1;
    p2++;
    while(p2!=pts.end())
    {
      double a1 = atan2(p1->y-p0->y, p1->x-p0->x)*180/M_PI;
      double a2 = atan2(p2->y-p0->y, p2->x-p0->x)*180/M_PI;
      if( fabs(a1-a2) < epsilon ) // 度
      {
        //p0 = pts.erase(p1);
        //if( p0 == pts.end() )
        //  break;
        //p1 = p0;
        //p1++;
        p1 = pts.erase(p1);
        if( p1 == pts.end() )
          break;
        p2 = p1;
        p2++;
        if( p2 == pts.end() )
          break;
      }
      else
      {
        p0 = p1;
        p1++;
        p2++;
      }
    }
    int after = pts.size();
    if( after == before )
      break;
  }
  int before = pts.size();
  point_list newlist;
  for (point_it it=pts.begin(); it!=pts.end();)
  {
    point_it next = it;
    next++;
    if( next != pts.end() )
    {
      point p1 = *it;
      point p2 = *next;
      // 8邻域
      if( fabs(p2.x - p1.x) <=1 &&
        fabs(p2.y - p1.y ) <=1 )
      {
        pts.insert(it, imaqMakePointFloat((p1.x+p2.x)/2, (p1.y+p2.y)/2));
        pts.erase(it);
        it = pts.erase(next);
        it--;
        continue;
      }
    }
    it++;
  }
  int after = pts.size();
  if(before != after)
  {
    epsilon = 1;
    goto AGAIN;
  }

  for (point_it it=pts.begin(); it!=pts.end(); it++)
  {
    e.push_back(*it);
  }
}

BestLine* GuessLine(PointsWithIndex& pts)
{
  typedef pair<double, PointSeg> PAIR;
  vector<PAIR> distances;
  for (UINT i=0; i<pts.size()-1; i++)
  {
    for (UINT j=i+1; j<pts.size(); j++)
    {
      //if( pts[i].first == pts[j].first )
      //  continue;
      PointSeg seg(pts[i].first, pts[j].first);
      double distance = 0;
      for (UINT k=0; k<pts.size(); k++)
      {
        distance += fabs(seg.Distance(pts[k].first));
      }
      distances.push_back(PAIR(distance, seg));
    }
  }
  sort(distances.begin(), distances.end());
  PointSeg& ps = distances.front().second;

  PointFloat pfs[] = {ps.S(), ps.E()};
  BestLine* bl = imaqFitLine(pfs, 2, NULL);
  return bl;
}

