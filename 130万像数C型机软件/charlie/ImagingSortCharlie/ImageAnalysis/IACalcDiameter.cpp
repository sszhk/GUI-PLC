#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "NIPoints.h"
#include "PolygonNIFloat.h"
#include "NIDispose.h"
//#include "NIROI.h"
//#include "set"
#include "point.h"
#include "sub_image.h"
#include "NIROI.h"
#include "IAFindShape.h"
// #include "auto_filter.h"
// #include "algorithm"
#include "hi_res_timer.h"
#include "shadow_image.h"


// ImagePtr try_to_find_shape(const PublicOptions* po,
//                        const DiameterOptions* dia_opt,
//                        DiameterReport* dia_rpt);
// BOOL calc_diameter    (ImagePtr result_img,
//                        const PublicOptions* po,
//                        const DiameterOptions* dia_opt,
//                        DiameterReport* dia_rpt);


// inline int safe_cyc_idx(int idx, int count)
// {
//   if( count <=0 )
//     return 0;
//   while( idx < 0 )
//     idx += count;
//   return idx%count;
// }

inline point symmetric_point(const point& pt, const point& center, bool isfitmax, float r)
{
  float dx = center.x - pt.x;
  float dy = center.y - pt.y;
  if (!isfitmax)
  {
    PointFloat ptf = imaqMakePointFloat((float)pt.x, (float)pt.y);
    PointFloat cer = imaqMakePointFloat((float)center.x, (float)center.y);
    float d = point2point(ptf, cer);
    if (d != 0)
    {
      dx *= (r/d);
      dy *=(r/d);
    }
  }
  return point(center.x+dx, center.y+dy);
}

void filter_pairs(points& contour, 
                  const point& center, 
                  const float radius,
                  float tolerance)  // 0.2 : <= 20%
{
  for (int i=0; i<(int)contour.size(); i++)
  {
    float d = point2point(contour[i], center);
    float abs_d = fabsf(d-radius);
    float ratio = abs_d/radius;
    if( ratio >= tolerance )
    {
      contour.erase(contour.begin()+i);
      i--;
    }
  }
  //   typedef auto_filter<int /*index*/, float /*distance*/> filter_type;
  //   filter_type filter;
  //   for (int i=0; i<(int)contour.size(); i++)
  //   {
  //     const point& pt = contour[i];
  //     filter.push_back(i, point2point(pt, center));
  //   }
  //   if( !filter.filter(tolerance) )
  //     return;
  //   vector<int> removed = filter.get_removed();
  //   if( removed.empty() )
  //     return;
  //   sort(removed.begin(), removed.end(), greater<int>());
  //   for (int i=0; i<(int)removed.size(); i++)
  //   {
  //     contour.erase(contour.begin()+removed[i]);
  //   }
}

void match_pairs(const points& contour, 
                 const point& center,
                 point_pairs& pairs,
                 bool isfitmax,
                 float r)
{
  if( contour.empty() )
  {
    pairs.clear();
    return;
  }

  hi_res_timer timer;
  //timer.Start();
  point empty(-FLT_MAX,-FLT_MAX);
  pairs.clear();
  pairs.assign(contour.size(), point_pair(empty, empty));
  //timer.Stop();
  //lg<<enDebug<<T("assign costs ")<<timer.GetMS()<<T(" ms")<<endl;

  // pairs：点对的列表
  // first：配对点的前者，second：后者。
  // 配对点的连线【基本上】通过圆心
  int size = (int)contour.size();
  int search_pts = size/4;
  if( search_pts < 2 )
    search_pts = 2;
  if( search_pts > 5 )
    search_pts = 5;
  //lg<<enDebug<<T("contours=")<<pairs.size()<<T(", search_pts=")<<search_pts<<endl;

  //   int rev_count = 0;
  //   int sym_count = 0;
  // 
  //   double tcount = 0;
  for (int i=0; i<size; i++)
  {
    const point* pt1 = (point*)&contour[i];
    const point* pt2 = NULL;

    // 是否已经过逆向配对？
    if( pairs[i].first.x != -FLT_MAX )
    {
      //       rev_count++;
      continue;
    }
    else
      pairs[i].first = *pt1;

    bool found = false;
    for (int j=i+size/2-search_pts; j<i+size/2+search_pts; j++)
    {
      int safe_j = GetSafeIndex(j, size);
      pt2 = /*(const point*)*/&contour[safe_j];
      //       timer.Start();
      float d = point2line(center.ptf(), pt1->ptf(), pt2->ptf(), true);
      //       timer.Stop();
      //       tcount += timer.GetMS();
      if( d <= 1.0f )
      {
        found = true;
        pairs[i].second = *pt2;
        // 同时逆向配对成功，提高效率
        pairs[safe_j].first = *pt2;
        pairs[safe_j].second = *pt1;
        break;
      }
    }

    if( !found )
    {
      //       sym_count++;
      pairs[i].second = symmetric_point(*pt1, center, isfitmax, r);
      //pairs[i].second = pairs[i].first;
    }
  }
  //   lg<<enDebug<<T("rev_count=")<<rev_count
  //     <<T("sym_count=")<<sym_count
  //     <<endl;
  //   lg<<enDebug<<T("time ms=")<<tcount<<endl;
}

#define EXPAND_SEARCH_REGION(x) ((x)*1.3f)

// C#接口
IA_EXPORT BOOL IA_Calc_FindDiameter(const PublicOptions* po,
                                    const DiameterOptions* dia_opt,
                                    DiameterReport* dia_rpt)
{
  ENTER_FUNCTION;

  if (!po || !dia_opt || !dia_rpt)
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  BOOL is_white = dia_opt->isWhite;
  Annulus an = dia_opt->ann;

  ImagePtr display = GetImage(idx, DISPLAY);
  if (!display)
    return FALSE;
  shadow_image img(display);

  FindShapeOptions fo;
  fo.ann = dia_opt->ann;
  fo.isWhite = dia_opt->isWhite;
  fo.isCrack = dia_opt->isCrack;
  fo.isMaxDiameter = TRUE;
  fo.isMinDiameter = TRUE;
  FindShapeReport fr;
  if (!find_shape(img, &fo, &fr))
  {
    dia_rpt->num_particle = fr.num_particle;
    img.copy_overlay();
    img.detach();
    return FALSE;
  }
  dia_rpt->center = fr.center;
  dia_rpt->maxDiameter = fr.maxDiameter;
  dia_rpt->minDiameter = fr.minDiameter;
  dia_rpt->maxDiaPt1 = fr.maxDiaPt1;
  dia_rpt->maxDiaPt2 = fr.maxDiaPt2;
  dia_rpt->minDiaPt1 = fr.minDiaPt1;
  dia_rpt->minDiaPt2 = fr.minDiaPt2;
  dia_rpt->radius = (double)fr.radius;
  dia_rpt->roundness = fr.roundness;
  dia_rpt->crack = fr.crack;

  draw_circle(img, imaqMakePointFloat((float)fr.center.x, (float)fr.center.y), fr.radius, &ColorRed);
  if (dia_opt->isMaxDiameter == TRUE)
    draw_line(img, dia_rpt->maxDiaPt1, dia_rpt->maxDiaPt2, 0, 0, &ColorYellowGreen, TRUE, TRUE);
  if (dia_opt->isMinDiameter == TRUE)
    draw_line(img, dia_rpt->minDiaPt1, dia_rpt->minDiaPt2, 0, 0, &ColorBlue, TRUE, TRUE);
  RETURN_FALSE(img.copy_overlay());
  img.detach();
  return TRUE;
}

