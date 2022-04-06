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
 * Created:     2010-04-08  19:43
 * Filename:    IACalcAngle.cpp
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     计算角度
 *
 */
#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "PolygonNIFloat.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "algorithm"
#include "point.h"
#include "sub_image.h"
#include "tools_ni.h"
#include "tools_geometry.h"
#include "shadow_image.h"

#pragma pack(push, 4)
struct AngleReport
{
  float angle;
  float ratio;
};
struct AngleOptions
{
  //RotatedRect rc;
  Annulus annulus;
  BOOL direction;
};
#pragma pack(pop)

void get_boundary(const points& contour, Rect* boundary)
{
  float left = FLT_MAX, t = FLT_MAX, r = -FLT_MAX, b = -FLT_MAX;
  for (int i=0; i<(int)contour.size(); i++)
  {
    if( left > contour[i].x )
    {
      left = contour[i].x;
    }
    if( t > contour[i].y )
    {
      t = contour[i].y;
    }
    if( r < contour[i].x )
    {
      r = contour[i].x;
    }
    if( b < contour[i].y )
    {
      b = contour[i].y;
    }
  }
  boundary->left = (int)left;
  boundary->top = (int)t;
  boundary->width = (int)(r-left);
  boundary->height = (int)(b-t);
}

static void make_opt(CurveOptions& co, int thres)
{
  co.extractionMode = IMAQ_NORMAL_IMAGE;
  co.threshold = thres;
  co.filterSize = IMAQ_FINE;
  co.minLength = 400;
  co.rowStepSize = 15;
  co.columnStepSize = 15;
  co.maxEndPointGap = 10;
  co.onlyClosed = FALSE;
  co.subpixelAccuracy = FALSE;
}

//返回圆上角度为angle的点
PointFloat get_point_on_circle(const PointFloat* center, float radius, 
                               float angle)
{
  float angle_radian = angle * PIf / 180;
  float dx = radius * cos(angle_radian);
  float dy = -radius * sin(angle_radian);
  return imaqMakePointFloat(center->x + dx, center->y + dy);
}

struct track_options
{
  point center;
  point point;
  float angle;
  float length;
  RakeDirection direction;
};
//画出切线
BOOL draw_tangent(ImagePtr image, const track_options* options)
{
  if(!image)
    return FALSE;
  Point point_direction;
  int dx = (int)(sin(options->angle * PIf / 180) * options->length);
  int dy = (int)(cos(options->angle * PIf / 180) * options->length);
  dx = options->direction == IMAQ_TOP_TO_BOTTOM ? -dx : dx;
  dy = options->direction == IMAQ_TOP_TO_BOTTOM ? -dy : dy;
  point_direction.x = options->point.pt().x + dx;
  point_direction.y = options->point.pt().y + dy;
  return imaqOverlayLineWithArrow(image, point_direction, 
    options->point.pt(), &ColorLime, FALSE, TRUE, NULL);
}

StraightEdgeReport* find_edge_annulus(ImagePtr image, 
                                      Annulus annulus,
                                      bool start,
                                      RakeDirection direction)
{
  ENTER_FUNCTION;

  if (!image)
    return FALSE;
  float angle = start ? (float)annulus.startAngle : (float)annulus.endAngle;
  PointFloat point_center = point_to_pointfloat(&annulus.center);
  float radius = (float)annulus.innerRadius;
  PointFloat point_start = get_point_on_circle(&point_center, radius, angle);
  radius = (float)annulus.outerRadius;
  PointFloat point_end = get_point_on_circle(&point_center, radius, angle);
  PointFloat mid = point_mid(&point_start, &point_end);

  int width_rect = annulus.outerRadius - annulus.innerRadius;
  int height_rect = 100;
  RotatedRect rect_edge = 
    make_rotated_rect(width_rect, height_rect, mid, angle);

  track_options track_options;
  track_options.angle = angle;
  track_options.center = annulus.center;
  track_options.direction = direction;
  track_options.length = (float)annulus.outerRadius / 2;
  track_options.point = point_start;
  draw_tangent(image, &track_options);
  track_options.point = point_end;
  draw_tangent(image, &track_options);

  find_edge_options edge_options;
  edge_options.search_rect = rect_edge;
  edge_options.direction = direction;
  edge_options.width = 4;
  edge_options.steepness = 2;
  edge_options.gap = 5;
  edge_options.show_results = FALSE;

  return find_edge(image, &edge_options);
}


BOOL get_angle(ImagePtr image, Annulus annulus, float *angle)
{
  ENTER_FUNCTION;

  if (!image)
    return FALSE;

  StraightEdgeReport* edge_angle_start = 
    find_edge_annulus(image, annulus, true, IMAQ_TOP_TO_BOTTOM);
  RegisterDisposeSER dis_edge_angle_start(edge_angle_start);

  StraightEdgeReport* edge_angle_end = 
    find_edge_annulus(image, annulus, false, IMAQ_BOTTOM_TO_TOP);
  RegisterDisposeSER dis_edge_angle_end(edge_angle_end);
  if (!edge_angle_start || !edge_angle_end)
    return FALSE;

  PointFloat point_intersection;
  RETURN_FALSE(imaqGetIntersection(edge_angle_start->start, 
    edge_angle_start->end,
    edge_angle_end->start, 
    edge_angle_end->end, 
    &point_intersection));

  RETURN_FALSE(imaqOverlayLineWithArrow(image, 
    imaqMakePointFromPointFloat(point_intersection),
    imaqMakePointFromPointFloat(edge_angle_start->end),
    &ColorRed, FALSE, TRUE, NULL));

  RETURN_FALSE(imaqOverlayLineWithArrow(image, 
    imaqMakePointFromPointFloat(point_intersection),
    imaqMakePointFromPointFloat(edge_angle_end->end),
    &ColorRed, FALSE, TRUE, NULL));

  RETURN_FALSE(imaqGetAngle(edge_angle_start->start, edge_angle_start->end,
    edge_angle_end->start, edge_angle_end->end, angle));

  return TRUE;
}

BOOL init_angle_report(AngleReport* angle_report)
{
  if (!angle_report)
    return FALSE;
  angle_report->angle = 0;
  angle_report->ratio = 9999.9f;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FindAngle(const PublicOptions* po, const AngleOptions *ao, AngleReport *ar)
{
  ENTER_FUNCTION;

  if (!po || !ao || !init_angle_report(ar))
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  bool displaystayus = po->isdisplaystatus;
  int thres = po->thres;
  BOOL direction = ao->direction;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);

  return get_angle(img, ao->annulus, &ar->angle);

//   int result = 0;
//   ar->ratio = -1;
//   ClearMyError();
//   RotatedRect rc = ao->rc;
//   FindEdgeOptions feo;
//   //ZeroMemory(&feo, sizeof(feo));
//   MakeFindEdgeOptions(&feo, 1, 2, 4, 5, displaystayus, FALSE);
// 
//   StraightEdgeReport* edge1 = FindEdgeSimple(
//     idx, 
//     &rc, 
//     (direction == TRUE)?IMAQ_LEFT_TO_RIGHT:IMAQ_TOP_TO_BOTTOM,
//     &feo);
//   RegisterDisposeSER d1(edge1);
// 
//   if( !edge1 )
//   {
//     return FALSE;
//   }
//   StraightEdgeReport* edge2 = FindEdgeSimple(
//     idx,
//     &rc,
//     (direction == TRUE)?IMAQ_RIGHT_TO_LEFT:IMAQ_BOTTOM_TO_TOP,
//     &feo);
//   RegisterDisposeSER d2(edge2);
//   if(!edge2)
//   {
//     SetMyError(L"First edge not found");
//     return FALSE;
//   }
//   if( !edge1 || edge1->straightness == 0.0f )
//   {
//     SetMyError(L"First edge not found");
//     return FALSE;
//   }
//   if( !edge2 || edge2->straightness == 0.0f )
//   {
//     SetMyError(L"Second edge not found");
//     return FALSE;
//   }
//   if( !imaqGetAngle(edge1->start, edge1->end, 
//     edge2->start, edge2->end, &(ar->angle)) )
//   {
//     return FALSE;
//   }
// 
//   if( fabsf(ar->angle) <= FLT_EPSILON )
//   {
//     ar->ratio = -1;
//     return TRUE;
//   }
//   
//   point intersect;
//   result = imaqGetIntersection(edge1->start,
//     edge1->end,
//     edge2->start, 
//     edge2->end,
//     &intersect);
//   if(!result || intersect.x == FLT_MAX)
//   {
//     return FALSE;
//   }
// 
//   point start, end;
//   result = imaqGetBisectingLine(edge1->start, edge1->end,
//     edge2->start, edge2->end, &start, &end);
//   if( !result )
//     return FALSE;
//   draw_line(img, start, end);
// 
//   ROIPtr line = imaqCreateROI();
//   RegisterDispose dis_line(line);
//   result = imaqAddLineContour(line, 
//     imaqMakePointFromPointFloat(start), 
//     imaqMakePointFromPointFloat(end));
//   ROIProfile* line_prof = imaqROIProfile(img, line);
//   RegisterDispose dis_line_prof(line_prof);
//   /*
//   LevelType   type;       //Determines how the function evaluates the threshold and hysteresis values.
//   int         threshold;  //The pixel value at which an edge occurs.
//   int         hysteresis; //A value that helps determine edges in noisy images.
//   EdgeProcess process;    //Determines which edges the function looks for.
//   int         subpixel;   //Set this element to TRUE to find edges with subpixel accuracy by interpolating between points to find the crossing of the given threshold.
//   */
//   SimpleEdgeOptions seo = {IMAQ_RELATIVE, 99, 2, IMAQ_FIRST, TRUE};
//   int num_edges = 0;
//   PointFloat* edges = imaqSimpleEdge(img, line_prof->pixels, 
//     line_prof->report.dataCount,
//     &seo, 
//     &num_edges);
//   RegisterDispose dis_edges(edges);
//   if( !edges || !num_edges )
//   {
//     SetMyError(L"No edge found while calculating keenness.");
//     return FALSE;
//   }
//   draw_points(img, edges, num_edges, &ColorMagenta, 0, 0, true);
// 
//   point P = edges[0];
//   float theta_2 = ar->angle/2;
//   //float tg = tan(degree2radian(theta_2));
//   float sin_th = sin(degree2radian(theta_2));
//   float cos_th = cos(degree2radian(theta_2));
//   float AP = point2point(intersect, edges[0]);
//   if( fabsf(1-sin_th) < FLT_EPSILON )
//     return FALSE;
//   float coeff = (sin_th/(1-sin_th));
//   float R = AP*coeff;
//   float k = atan2(end.y-start.y, end.x-start.x);
//   float sin_k = sin(k);
//   float cos_k = cos(k);
//   point O;
//   O.x = P.x + R*cos_k;
//   O.y = P.y + R*sin_k;
//   draw_points(img, &O, 1, &ColorBlue, 0, 0, true);
//   //draw_circle(img, O, R, &ColorLime);
// 
//   ar->ratio = R;/*(AP/(AP+R))*100;*/


//   Enlighten(img, edge1->start, &ColorSteelBlue, true);
//   Enlighten(img, edge2->start, &ColorSteelBlue, true);
//   auto_array<Point> pts(edge1->numCoordinates);
//   GetPoints(edge1->coordinates, edge1->numCoordinates, pts, .0f, .0f);
//   imaqOverlayPoints(img, pts, edge1->numCoordinates, 
//     &ColorSteelBlue, 1, IMAQ_POINT_AS_CROSS, NULL, NULL);
// 
//   auto_array<Point> pts22(edge2->numCoordinates);
//   GetPoints(edge2->coordinates, edge2->numCoordinates, pts22, .0f, .0f);
//   imaqOverlayPoints(img, pts22, pts22.size(), &ColorOrangeRed, 1, 
//     IMAQ_POINT_AS_CROSS, NULL, NULL);
//   
// 
//   // 尖锐度
//   PT pt;
//   if( !imaqGetIntersection(edge1->start, edge1->end, 
//     edge2->start, edge2->end, &pt) )
//   {
//     return FALSE;
//   }
// 
//   Enlighten(img, pt, &ColorYellowGreen, true);
// 
//   points tri1;
//   tri1.push_back(pt);
//   tri1.push_back(edge1->coordinates[0]);
//   tri1.push_back(edge2->coordinates[0]);
//   tri1.push_back(pt);
//   points tri2;
//   tri2.push_back(pt);
//   tri2.push_back(edge1->coordinates[edge1->numCoordinates-1]);
//   tri2.push_back(edge2->coordinates[edge2->numCoordinates-1]);
//   tri2.push_back(pt);
// 
//   float a1, a2;
//   if(!imaqGetPolygonArea(&tri1[0], tri1.size(), &a1))
//   {
// //    format_points(&tri1[0], tri1.size());
//     return FALSE;
//   }
//   if(!imaqGetPolygonArea(&tri2[0], tri2.size(), &a2))
//   {
// //    format_points(&tri2[0], tri2.size());
//     return FALSE;
//   }
//   if( a1 < a2 )
//   {
//     swap(a1,a2);
//     swap(tri1, tri2);
//   }
//   // tri1: 大三角形 a1
//   // tri2: 小三角形 a2
// 
//   auto_array<Point> pts1, pts2;
//   GetPoints(&tri1[0], tri1.size(), pts1, .0f, .0f);
//   GetPoints(&tri2[0], tri2.size(), pts2, .0f, .0f);
//   imaqOverlayOpenContour(img, pts1, tri1.size(), &ColorLime, NULL);
//   imaqOverlayOpenContour(img, pts2, tri2.size(), &ColorOrangeRed, NULL);
//   Enlighten(img, edge1->coordinates[0], &ColorOrangeRed, true);
//   Enlighten(img, edge2->coordinates[0], &ColorSteelBlue, true);
// 
//   //   Rect boundary;
//   //   get_boundary(tri2, &boundary);
//   //   SubImage sub(img, &boundary);
//   ROIPtr roi = imaqCreateROI();
//   RegisterDispose dis_roi(roi);
//   auto_array<Point> contour;
//   GetPointsInt(&tri2[0], tri2.size(), contour);
//   result = imaqAddClosedContour(roi, contour, tri2.size());
//   //imaqSetWindowROI(idx, roi);
// 
//   if( !result )
//   {
//     return TRUE;
//   }    
//   ImagePtr copy = imaqCreateImage(IMAQ_IMAGE_U8, 0);
//   RegisterDispose dis_copy(copy);
//   imaqDuplicate(copy, img);
//   ImagePtr mask = imaqCreateImage(IMAQ_IMAGE_U8, 0);
//   RegisterDispose dis_mask(mask);
//   result = imaqROIToMask(mask, roi, 255, copy, NULL);
// 
//   //imaqWriteJPEGFile(mask, "C:\\mask.jpg", 750, NULL);
//   ThresholdData* td = imaqAutoThreshold2(copy, copy, 2, IMAQ_THRESH_INTERCLASS, mask);
//   RegisterDispose dis_td(td);
//   imaqThreshold(copy, copy, 1, 1, TRUE, 255);
//   //sub.Commit();
// 
//   //   imaqWritePNGFile2(mask, "C:\\mask.png", 750, NULL, FALSE);
//   //   imaqWritePNGFile2(copy, "C:\\copy.png", 750, NULL, FALSE);
// 
//   HistogramReport* hr = imaqHistogram(copy, 2, 0, 255, mask);
//   RegisterDispose dis_hr(hr);
//   if( !hr || hr->histogramCount!=2 )
//   {
//     return TRUE;
//   }  
// 
//   float black = (float)hr->histogram[0];
//   float white = (float)hr->numPixels;/*(float)hr->histogram[1];*/
//   if( white <= FLT_EPSILON )
//   {
//     return TRUE;
//   }  
//   ar->ratio = 100*(black/white);

  //  draw_points(img, )


  //   Points pts;
  //   pts.insert(pts.end(), edge1->coordinates, edge1->coordinates+edge1->numCoordinates);
  //   Points temp;
  //   temp.insert(temp.end(), edge2->coordinates, edge2->coordinates+edge2->numCoordinates);
  //   // 角度边的点序很关键
  //   std::reverse(temp.begin(), temp.end());
  // 
  //   pts.insert(pts.end(), temp.begin(), temp.end());
  //   pts.push_back(edge1->coordinates[0]);
  // 
  //   float area = 0;
  //   if( !imaqGetPolygonArea(&pts[0], pts.size(), &area))
  //     return -1;

  //   if( a1 <= FLT_EPSILON )
  //   {
  //     ratio = -1;
  //   }
  //   else
  //   {
  //     ratio = a2/a1;/*area/tri_area*/;
  //     ratio *= 100;
  //     if (ratio > 100)
  //     {
  //       ratio = 100;
  //     }
  //   }

//   return TRUE;
}

