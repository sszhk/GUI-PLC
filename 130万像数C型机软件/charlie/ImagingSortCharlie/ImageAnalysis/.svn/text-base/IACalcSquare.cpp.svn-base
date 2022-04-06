/* 
* (c)2010 enMind Software Co., Ltd. All rights reserved.
*
* +===================| NON-DISCLOSURE STATEMENT |===================+
* | Everything related to developing is property of enMind Co., Ltd. |
* | enMind employees MUST NOT disclose any of that for any purpose   |
* | by any means without permission.                                 |
* | The non-disclosure materials are including but not limited to    |
* | the following:                                                   |
* |  . source code (C/C++/Javascript/HTML/C#/AS/CSS/Exe/Dll etc.);   |
* |  . documents (design documents etc.);                            |
* |  . diagrams & figures;                                           |
* |  . datasheets;                                                   |
* | This statement applies to all employees of enMind Co., Ltd.      |
* |                    http://www.enmind.com.cn                      |
* +==================================================================+
* 
* Created:     2010-04-08  13:22
* Filename:    IACalcSquare.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     计算四边形的对边对角
*
*/
#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "NIDispose.h"
#include "IAFindShape.h"
#include "point.h"
#include "tools_ni.h"
#include "tools_geometry.h"
#include "overlay.h"
#include "shadow_image.h"

#pragma pack(push, 4)
struct SquareReport
{
  PointFloat center;
  float maxDiagonal;
  float minDiagonal;
  float minSubtense;
  float maxSubtense;
  int num_particle;
};
struct SquareOptions
{
  Annulus annulus;
  BOOL insideToOutside;
  BOOL iswhite;
  BOOL MaxDiagonal;
  BOOL MinDiagonal;
  BOOL MaxSubtense;
  BOOL MinSubtense;
};
#pragma pack(pop)

BOOL init_square_report(SquareReport* square_report)
{
  if(!square_report)
    return FALSE;
  square_report->center = imaqMakePointFloat(0, 0);
  square_report->maxDiagonal = 0;
  square_report->maxSubtense = 0;
  square_report->minDiagonal = 0;
  square_report->minSubtense = 0;
  square_report->num_particle = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FindSquare(const PublicOptions* po, const SquareOptions *so, SquareReport *sr)
{
  ENTER_FUNCTION;

  if (!po || !so || !init_square_report(sr))
    return FALSE;
  ClearMyError();

  const int ARC = 100; 
  int idx = po->idx;
  bool displaystayus = po->isdisplaystatus;
  int thres = po->thres;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);
  int result = 0;

  ImagePtr image_hole = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_image_hole(image_hole);
  RETURN_FALSE(imaqDuplicate(image_hole, img));

  int count = 0;
  if (!so->iswhite)
  {
    RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 1)); 
  }
  else
    RETURN_FALSE(imaqThreshold(image_hole, image_hole, 0, 0, TRUE, 1));
 
  RETURN_FALSE(imaqFillHoles(img, img, TRUE));

  result = find_shape(img, so->annulus, count, FALSE);
  if (!result || count != 1)
  {
    sr->num_particle = count;
    double left = 0;
    double top = 0;
    double width = 0;
    double height = 0;
    for (int i = 0; i < count; i++)
    {
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_LEFT, &left));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_TOP, &top));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_WIDTH, &width));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_HEIGHT, &height));
      RETURN_FALSE(imaqOverlayRect(img, 
        imaqMakeRect((int)top, (int)left, (int)height, (int)width), 
        &ColorRed, IMAQ_DRAW_VALUE, NULL));
    }
    img.copy_overlay();
    img.detach();
    return FALSE;
  }   
  point centroid;
  RETURN_FALSE(imaqCentroid(img, centroid.pf(), NULL));
  sr->center = centroid.ptf();
  //DrawSmallRect(img, centroid, 5, &ColorYellow);
  RETURN_FALSE(imaqThreshold(img, img, (float)so->iswhite, (float)so->iswhite, TRUE, 255));

  result = find_shape(image_hole, so->annulus, count, FALSE);

  RotatedRect rr = imaqMakeRotatedRect(0, 0, HEIGHT_IMAGE, WIDTH_IMAGE, 0);
  FindEdgeOptions edgeOptions;
  edgeOptions.threshold = 40;
  edgeOptions.width = 4;
  edgeOptions.steepness = 2;
  edgeOptions.subsamplingRatio = 3;
  edgeOptions.showSearchArea = 0;
  edgeOptions.showSearchLines = 0;
  edgeOptions.showEdgesFound = 0;
  edgeOptions.showResult = 0;
  StraightEdgeReport* straightEdgeReport = 
    imaqFindEdge(img, rr, IMAQ_LEFT_TO_RIGHT, &edgeOptions, NULL);
  RegisterDisposeSER dis_straightEdgeReport(straightEdgeReport);
  if(!straightEdgeReport || straightEdgeReport->numCoordinates <= 0)
  {
    img.detach();
    return FALSE;
  }

  float angle = 0;
  RETURN_FALSE(imaqGetAngle(straightEdgeReport->start, 
    straightEdgeReport->end, imaqMakePointFloat(0, 0), 
    imaqMakePointFloat(100, 0), &angle));

  if(count == 1)
  {
    double area_hole = 0;
    RETURN_FALSE(imaqMeasureParticle(image_hole, 0, FALSE, IMAQ_MT_AREA, &area_hole));
    float radius_hole = sqrtf((float)area_hole / PIf);
    PointFloat point_center_to_line = imaqMakePointFloat(0, 0);
    RETURN_FALSE(imaqGetPerpendicularLine(straightEdgeReport->start, 
      straightEdgeReport->end, centroid.ptf(), centroid.pf(), &point_center_to_line, NULL));
    rr.width = rr.height = (int)radius_hole * 2;
    rr.top = (int)point_center_to_line.y - (int)radius_hole;
    rr.left = (int)point_center_to_line.x - (int)radius_hole;
    rr.angle = angle;

    StraightEdgeReport* ser = imaqFindEdge(img, rr, IMAQ_TOP_TO_BOTTOM, &edgeOptions, NULL);
    RegisterDisposeSER dis_ser(ser);
    if(ser && ser->numCoordinates > 0)
    {   
      RETURN_FALSE(imaqGetAngle(ser->start, ser->end, 
        imaqMakePointFloat(0, 0), imaqMakePointFloat(100, 0), &angle));
    }
  }

  float d[] = {0, 0};
  PointFloat first[2];
  PointFloat last[2];
  RakeDirection rd[] = {IMAQ_LEFT_TO_RIGHT, IMAQ_TOP_TO_BOTTOM};

  //最大对边
  RotatedRect sr_oppo_flat_max = make_rotated_rect(
    (int)so->annulus.outerRadius * 2, (int)so->annulus.outerRadius * 2, 
    centroid.ptf(), angle);
  RETURN_FALSE(imaqClampMax(img, sr_oppo_flat_max, IMAQ_LEFT_TO_RIGHT, 
    &d[0], &edgeOptions, NULL, &first[0], &last[0]));
  RETURN_FALSE(imaqClampMax(img, sr_oppo_flat_max, IMAQ_TOP_TO_BOTTOM, 
    &d[1], &edgeOptions, NULL, &first[1], &last[1]));
  int max_idx = d[0] > d[1] ? 0 : 1;
  sr->maxSubtense = d[max_idx];
  if (so->MaxSubtense)
    RETURN_FALSE(overlay_result_max(img, sr_oppo_flat_max, first[max_idx], 
    last[max_idx], rd[max_idx]));

  //最小对边
  RotatedRect sr_oppo_flat_min[2];
  sr_oppo_flat_min[0] = make_rotated_rect((int)sr->maxSubtense- ARC, 
    (int)so->annulus.outerRadius * 2, centroid.ptf(), angle);
  RETURN_FALSE(imaqClampMin(img, sr_oppo_flat_min[0], IMAQ_TOP_TO_BOTTOM, 
    &d[0], &edgeOptions, NULL, &first[0], &last[0]));
  sr_oppo_flat_min[1] = make_rotated_rect((int)so->annulus.outerRadius * 2,
    (int)sr->maxSubtense - ARC, centroid.ptf(), angle);
  RETURN_FALSE(imaqClampMin(img, sr_oppo_flat_min[1], IMAQ_LEFT_TO_RIGHT, 
    &d[1], &edgeOptions, NULL, &first[1], &last[1])); 
  int min_idx = d[0] < d[1] ? 0 : 1;
  max_idx = d[0] >= d[1] ? 0 : 1;
  sr->minSubtense = d[min_idx];
  if (so->MinSubtense)
    RETURN_FALSE(overlay_result_min(img, sr_oppo_flat_min[min_idx], 
    first[min_idx], last[min_idx], rd[max_idx]));

  //对角
  edgeOptions.showResult = 0;
  int l = (int)(sqrt((float)2) * sr->maxSubtense);
  RotatedRect sr_oppo_angle = make_rotated_rect(l, l, centroid.ptf(), angle + 45);
  RETURN_FALSE(imaqClampMax(img, sr_oppo_angle, IMAQ_LEFT_TO_RIGHT, &d[0], 
    &edgeOptions, NULL, &first[0], &last[0]));
  RETURN_FALSE(imaqClampMax(img, sr_oppo_angle, IMAQ_TOP_TO_BOTTOM, &d[1], 
    &edgeOptions, NULL, &first[1], &last[1]));
  min_idx = d[0] < d[1] ? 0 : 1;
  max_idx = d[0] >= d[1] ? 0 : 1;
  sr->minDiagonal = d[min_idx];
  sr->maxDiagonal = d[max_idx];
  if (so->MaxDiagonal)
    RETURN_FALSE(imaqOverlayLine(img, get_point(first[max_idx]), 
    get_point(last[max_idx]), &ColorBlue, NULL));
  if(so->MinDiagonal)
    RETURN_FALSE(imaqOverlayLine(img, get_point(first[min_idx]), 
    get_point(last[min_idx]), &ColorOrange, NULL));

  img.copy_overlay();
  img.detach();
  return TRUE;
}