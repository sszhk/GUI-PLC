#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "auto_array.h"
#include "Colors.h"
#include "sub_image.h"
#include "IAFindShape.h"
#include "point.h"
#include "map"
#include "shadow_image.h"
#include "tools_ni.h"

#define INVALID_COORD (-99999)
#define IS_INVALID_COORD(pt) (pt.x==INVALID_COORD)
#define IS_VALID_COORD(pt) (pt.x!=INVALID_COORD)


#pragma pack(push, 4)
struct HexagonOptions
{
  Annulus ann;
  BOOL insideToOutside;
  BOOL iswhite;
  BOOL maxDiagonal;
  BOOL minDiagonal;
  BOOL maxSubtense;
  BOOL minSubtense;
  BOOL concent;
  BOOL crack;
  BOOL open;
};
struct HexagonReport
{
  PointFloat center;
  float maxDiagonal;
  float minDiagonal;
  float minSubtense;
  float maxSubtense;
  float crack;
  int num_particle;
};
#pragma pack(pop)

IA_EXPORT void IA_CopyImage(int idx);

extern void extract_path(CPT* c, int num, points& e);

BOOL init_hexagon_report(HexagonReport* hexagon_report)
{
  if (!hexagon_report)
    return FALSE;
  hexagon_report->center = imaqMakePointFloat(0, 0);
  hexagon_report->crack = 9999.9f;
  hexagon_report->maxDiagonal = 0;
  hexagon_report->maxSubtense = 0;
  hexagon_report->minDiagonal = 0;
  hexagon_report->minSubtense = 0;
  hexagon_report->num_particle = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FindHexagon(const PublicOptions* po, 
                                   const HexagonOptions* ho,
                                   HexagonReport* hr)
{
  ENTER_FUNCTION;

  if (!po || !ho || !init_hexagon_report(hr))
    return FALSE;
  ClearMyError();

  int idx=po->idx;
  BOOL iswhite = ho->iswhite;

  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);

//   FindShapeOptions fo;
//   fo.ann = ho->ann; 
//   fo.isWhite = ho->iswhite;
//   fo.isCrack = ho->crack;
//   FindShapeReport fr;
//   if (!find_shape(img, &fo, &fr))
//   {
//     hr->num_particle = fr.num_particle;
//     img.copy_overlay();
//     img.detach();
//     return FALSE;
//   }
//   hr->crack = fr.crack;

  Annulus an = ho->ann;
  if (!ho->iswhite)
    RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));
  if (!ho->open)
  {
    RETURN_FALSE(morphology(img, IMAQ_CLOSE));
    if (!find_shape(img, ho->ann, hr->num_particle, TRUE))
    {
      img.detach();
      return false;
    }
  }
  else
    an.innerRadius = 1;
//   an.center = imaqMakePointFromPointFloat(fr.center);
  PointFloat center_img = imaqMakePointFloat(0, 0);
  RETURN_FALSE(imaqCentroid(img, &center_img, NULL));
  an.center = imaqMakePointFromPointFloat(center_img);
  if (ho->crack)
    hr->crack = SideCrack(img, an, KERNEL_CRACK);
  SpokeReport* origReport = NULL;
  edge_circle_options edge_options;
  edge_options.annulus = an;
  edge_options.gap = 1;
  edge_options.smoothing = 1;
  CircularEdgeReport* report = find_diameter(img, &edge_options, &origReport);
  RegisterDisposeCER dis_report(report);
  RegisterDispose dis_orig(origReport);
  if( !report || !origReport )
  {
    img.detach();
    return FALSE;
  }
  hr->center = report->center;

  Point pt = imaqMakePointFromPointFloat(hr->center);
  if( point2point(an.center, pt) > 2 )
  {
    an.center = pt;
    dis_report.Dispose();
    dis_orig.Dispose();
    edge_options.annulus = an;
    report = find_diameter(img, &edge_options, &origReport);
    dis_report.Register(report);
    dis_orig.Register(origReport);
    if( !report || !origReport )
    {
      img.detach();
      return FALSE;
    }
  }

  if( &hr->center )
    hr->center = report->center;

  if(report->numCoordinates < 24 )
  {
    img.detach();
    return FALSE;
  }

  if (!ho->iswhite)
    RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));

  auto_array<PointFloat> hexagon(origReport->numSpokeLines);
  for (int i=0; i<hexagon.size(); i++)
  {
    hexagon[i].x = INVALID_COORD;
    hexagon[i].y = INVALID_COORD;
  }
  for (int i=0; i<origReport->numLinesWithEdges; i++)
  {
    int _idx = origReport->linesWithEdges[i];
    hexagon[_idx] = origReport->firstEdges[i];
  }

  // 重要数据！
  int total = hexagon.size();
  int half = total/2;
  int numEdge = total/6;

  // 计算最大对角
  // 后面所有的计算都依赖于此
  float maxDiag = -1;
  int idx0 = 0;
  float minDiag = 999;
  PointFloat p1MaxDiag, p2MaxDiag;
  PointFloat p1MinDiag, p2MinDiag;
  PointFloat p1MidDiag, p2MidDiag;
  for (int i=0; i<half; i++)
  {
    PointFloat& p1 = hexagon[i];
    PointFloat& p2 = hexagon[GetCircleIndex(i, total)];
    if( IS_INVALID_COORD(p1) ||
      IS_INVALID_COORD(p2) )
    {
      continue;
    }
    float d = point2point(p1, p2);
    if( maxDiag < d )
    {
      maxDiag = d;
      p1MaxDiag = p1;
      p2MaxDiag = p2;
      idx0 = i;
    }
  }
  hr->maxDiagonal = maxDiag;

  // 计算最小对角
  int tryIdx[] = {0, 1, -1, 2, -2};
  int tries = 5;
  int idx1 = GetSafeIndex(idx0+numEdge, total);
  int idx2 = GetSafeIndex(idx1+numEdge, total);
  float d2, d3;
  PointFloat p1, p2, p3, p4;
  {
    bool found = false;
    for (int i=0; i<tries; i++)
    {
      int DIAG2 = GetSafeIndex(idx1+tryIdx[i], total);
      p1 = hexagon[DIAG2];
      p2 = hexagon[GetCircleIndex(DIAG2, total)];
      if( IS_VALID_COORD(p1) &&
        IS_VALID_COORD(p2) 
        )
      {
        found = true;
        break;
      }
    }
    if( !found )
    {
      img.detach();
      return FALSE;
    }

    found = false;
    for (int i=0; i<tries; i++)
    {
      int DIAG3 = GetSafeIndex(idx2+tryIdx[i], total);
      p3 = hexagon[DIAG3];
      p4 = hexagon[GetCircleIndex(DIAG3, total)];
      if( IS_VALID_COORD(p3) &&
        IS_VALID_COORD(p4)
        )
      {
        found = true;
        break;
      }
    }
    if( !found )
    {
      img.detach();
      return FALSE;
    }

    d2 = point2point(p1, p2);
    d3 = point2point(p3, p4);
    if( d2 > d3 )
    {
      p1MidDiag = p1;
      p2MidDiag = p2;
      p1MinDiag = p3;
      p2MinDiag = p4;
      hr->minDiagonal = d3;
    }
    else
    {
      p1MinDiag = p1;
      p2MinDiag = p2;
      p1MidDiag = p3;
      p2MidDiag = p4;

      hr->minDiagonal = d2;
      float swap = d3;
      d3 = d2;
      d2 = swap;
    }
  }
  auto_array<float> Ps(6);
  Ps[0] = point2point(p1MaxDiag, p1);
  Ps[1] = point2point(p1, p3);
  Ps[2] = point2point(p3, p2MaxDiag);
  Ps[3] = point2point(p2MaxDiag, p2);
  Ps[4] = point2point(p2, p4);
  Ps[5] = point2point(p4, p1MaxDiag);
  float perimeter = 0;
  float aver_edge = 0;
  for (int i=0; i<6; i++)
  {
    perimeter += Ps[i];
  }
  aver_edge = perimeter/6;

//   DrawSmallRect(img, p1MaxDiag, 5, &ColorRed);
//   DrawSmallRect(img, p2MaxDiag, 5, &ColorRed);
//   DrawSmallRect(img, p1MidDiag, 5, &ColorRed);
//   DrawSmallRect(img, p2MidDiag, 5, &ColorRed);
//   DrawSmallRect(img, p1MinDiag, 5, &ColorRed);
//   DrawSmallRect(img, p2MinDiag, 5, &ColorRed);

  // 拟合六角边
  auto_array<BestLine*> fitEdges(6);
  RegisterDispose dispose_bestlines[6];

  for (int i=0; i<6; i++)
  {
    auto_array<PointFloat> tempEdge(numEdge+1);
    int _count = 0;
    for (int j=0; j<=numEdge; j++)
    {
      PointFloat& _pt = hexagon[GetSafeIndex(idx0+i*numEdge+j, total)];
      if(IS_VALID_COORD(pt))
      {
        tempEdge[_count++] = _pt;
      }
    }
    if( !_count )  // 至少有一条边没有找到
    {
      img.detach();
      return FALSE;
    }

    FitLineOptions flo = {500,3,0};
    fitEdges[i] = imaqFitLine(tempEdge, _count, &flo);
    dispose_bestlines[i].Register(fitEdges[i]);
    if( !fitEdges[i] )
    {
      img.detach();
      return FALSE;
    }
  }

  int validCount = 0;
  for (int i=0; i<6; i++)
  {
    // 检查临边合法性
    //BestLine* l0 = fitEdges[GetSafeIndex(i-1, 6)];
    BestLine* l1 = fitEdges[GetSafeIndex(i, 6)];
    BestLine* l2 = fitEdges[GetSafeIndex(i+1, 6)];

    float d1 = point2point(l1->end, l2->start);
    float _d2 = point2point(l1->start, l2->start);
    float _d3 = point2point(l1->end, l2->end);
    float d4 = point2point(l1->start, l2->end);
    float MAX_DISTANCE_ERROR = aver_edge/*/5*/;
    if( d1 <= MAX_DISTANCE_ERROR  ||
      _d2 <= MAX_DISTANCE_ERROR  ||
      _d3 <= MAX_DISTANCE_ERROR  ||
      d4 <= MAX_DISTANCE_ERROR
      )
    {
      validCount++;
    }
  }
  if( validCount < 2 )
  {
    SetMyError(_T("边偏离误差过大"));
    img.detach();
    return FALSE;
  }

  // 找对边
  PointFloat p1MaxSub = imaqMakePointFloat(0, 0);
  PointFloat p2MaxSub = imaqMakePointFloat(0, 0);
  PointFloat p1MinSub = imaqMakePointFloat(0, 0);
  PointFloat p2MinSub = imaqMakePointFloat(0, 0);
  float minSubD = 999;
  float maxSubD = -1;
  auto_array<PointFloat> fitVertex(6);
  for (int i=0; i<6; i++)
  {
    PointFloat center = report->center;
    PointFloat _p1 = imaqMakePointFloat(0, 0);
    PointFloat _p2 = imaqMakePointFloat(0, 0);
    PointFloat inter = imaqMakePointFloat(0, 0);
    BestLine* l1 = fitEdges[i];
    BestLine* l2 = fitEdges[GetSafeIndex(i+1, 6)];
    BestLine* opposite = fitEdges[GetSafeIndex(i+3, 6)];
    //PointFloat interVer;
    if(!imaqGetIntersection(l1->start, l1->end, l2->start, l2->end, &fitVertex[i]))
      return FALSE;

    double d = 0;
    if( !imaqGetPerpendicularLine(l1->start, l1->end, center, &p1, &p2, &d) )
      continue;
    if( !imaqGetIntersection(p1, p2, opposite->start, opposite->end, &inter) )
      continue;
    d = point2point(inter, p2);
    if( minSubD > d )
    {
      minSubD = (float)d;
      p1MinSub = inter;
      p2MinSub = p2;
    }
    if( maxSubD < d )
    {
      maxSubD = (float)d;
      p1MaxSub = inter;
      p2MaxSub = p2;
    }
  }
  hr->minSubtense = minSubD;
  hr->maxSubtense = maxSubD;

  if(ho->maxDiagonal)
    OverlayDistance(img, p1MaxDiag, p2MaxDiag, IMAQ_RGB_BLUE);
  if(ho->minDiagonal)
    OverlayDistance(img, p1MinDiag, p2MinDiag, IMAQ_RGB_RED);
  if(ho->maxSubtense)
    OverlayDistance(img, p1MaxSub, p2MaxSub, ColorPurple);
  if(ho->minSubtense)
    OverlayDistance(img, p1MinSub, p2MinSub, IMAQ_RGB_YELLOW);

  img.copy_overlay();
  img.detach();
  return TRUE;
}