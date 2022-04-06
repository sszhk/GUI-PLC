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
 * Filename:    Utilities.cpp
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     
 *
 */
#include "stdafx.h"
#include "auto_array.h"
#include "IAMain.h"
#include "Utilities.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "sub_image.h"
#include "point.h"
#include "hi_res_timer.h"



Curve* get_max_curve(Curve* cvs, int num)
{
  Curve* theone = NULL;
  int max = 0;
  for (int i=0; i<num; i++)
  {
    if( max < (int)cvs[i].numPoints )
    {
      theone = &cvs[i];
      max = cvs[i].numPoints;
    }
  }
  return theone;
}

float FindDistance(ImagePtr img, const RotatedRect* rrr, 
                   BOOL inside, BOOL horizontal, int threshold, 
                   PointFloat* first, PointFloat *last, bool displaystatus)
{
  if( IsEmpty(img) )
    return NULL;
  RotatedRect rr = *rrr;
  //   if( !GetToolRotatedRectangle(idx, id, &rr) )
  //     return -1;

  float distance = 0;
  //   PointFloat first, last;
  int result = 0;
  FindEdgeOptions feo;
  FindEdgeOptions* pfeo = &feo;
  MakeFindEdgeOptions(&feo, threshold, 2, 4, 5, displaystatus, FALSE);

  if( inside )
    result = imaqClampMin(img, rr, horizontal?IMAQ_LEFT_TO_RIGHT:IMAQ_TOP_TO_BOTTOM, &distance, pfeo, NULL, first, last);
  else
    result = imaqClampMax(img, rr, horizontal?IMAQ_LEFT_TO_RIGHT:IMAQ_TOP_TO_BOTTOM, &distance, pfeo, NULL, first, last);
  if (!result || distance > max(rr.width, rr.height))
  {
    distance = 0;
  }
  return distance;
}


void DrawSmallRect(ImagePtr img, PointFloat center, int size, const RGBValue* color)
{
  if( IsEmpty(img) )
    return;
  RGBValue rgb;
  if( !color )
    rgb = IMAQ_RGB_GREEN;
  else
    rgb = *color;
  Rect rc = imaqMakeRect((int)center.y - size/2, (int)center.x-size/2, size, size);
  imaqOverlayRect(img, rc, &rgb, IMAQ_DRAW_VALUE, NULL);
}

void DrawError(ImagePtr img, PointFloat center, int size, const RGBValue* color)
{
  if (IsEmpty(img))
    return;
  RGBValue rgb;
  if( !color )
    rgb = IMAQ_RGB_GREEN;
  else
    rgb = *color;
  float d = (float)size;
  PointFloat pt1 = imaqMakePointFloat(center.x - d, center.y - d);
  PointFloat pt2 = imaqMakePointFloat(center.x - d, center.y + d);
  PointFloat pt3 = imaqMakePointFloat(center.x + d, center.y - d);
  PointFloat pt4 = imaqMakePointFloat(center.x + d, center.y + d);
  imaqOverlayLine(img, get_point(pt1), get_point(pt4), color, NULL);
  imaqOverlayLine(img, get_point(pt2), get_point(pt3), color, NULL);
}

// 
// float Point2Point(const PointFloat& pt1, const PointFloat& pt2)
// {
//   float dx = pt2.x - pt1.x;
//   float dy = pt2.y - pt1.y;
//   return sqrt(dx*dx + dy*dy);
// }
// 
// float Point2Point(const Point& pt1, const Point& pt2)
// {
//   float dx = (float)pt2.x - pt1.x;
//   float dy = (float)pt2.y - pt1.y;
//   return sqrt(dx*dx + dy*dy);
// }


void MakeFindEdgeOptions(FindEdgeOptions* options, int thres, int steep, 
                         int width, double subsamplingratio, BOOL showsimple, 
                         BOOL showall)
{
  if( !options )
    return;
  options->threshold = thres;
  options->steepness = steep;
  options->width = width;
  options->subsamplingRatio = subsamplingratio;
  options->showEdgesFound = 
    options->showSearchArea = 
    options->showSearchLines = showall;
  options->showResult = showsimple;
}

void DisplayPointFloats(ImagePtr img, const PointFloat* pts, int count, const RGBValue* colors, int numColor, float dx, float dy)
{
  if( IsEmpty( img ) ||
    !pts ||
    !count ||
    !colors ||
    !numColor)
    return;

  //stringstream ss;
  //ss << "Point: " << count;

  //imaqOverlayText(img, imaqMakePointFromPointFloat(pts[0]), ss.str().c_str(), colors, NULL, NULL);

  for(int i=0; i<count; i++)
  {
    const PointFloat& pt = pts[i];
    PointFloat ptx = imaqMakePointFloat(pt.x+dx, pt.y+dy);
    DrawSmallRect(img, ptx, 1, &colors[i%numColor]);
  }
}
StraightEdgeReport* FindEdgeSimple(ImagePtr img, const RotatedRect* rc, 
                                   RakeDirection dir, FindEdgeOptions* feo)
{
  if( IsEmpty(img) )
    return NULL;
  //   ROIPtr roi = GetROI(idx);
  //   RegisterDispose dispose_roi(roi);
  //   ContourInfo2* ci = imaqGetContourInfo2(roi, id);
  //   RegisterDispose dispose_ci(ci);
  //   if( !ci || ci->type != IMAQ_ROTATED_RECT)
  //     return NULL;
  //   RotatedRect* rr = ci->structure.rotatedRect;
  const RotatedRect& rr = *rc;
  //   if( !GetToolRotatedRectangle(idx, id, &rr) )
  //     return NULL;

  return imaqFindEdge(img, rr, dir, feo, NULL);
}
StraightEdgeReport* FindEdgeSimple(int idx, const RotatedRect* rc, 
                                   RakeDirection dir, FindEdgeOptions* feo)
{
  ImagePtr img = GetImage(idx, DISPLAY);
  return FindEdgeSimple(img, rc, dir, feo);
}

CircularEdgeReport* FindDiameter(ImagePtr img, const Annulus* an, BOOL insideToOutside, int thres, BOOL showSimple, BOOL ShowAll, int gap, SpokeReport** origReport)
{
  FindEdgeOptions feo;
  MakeFindEdgeOptions(&feo, thres, 2, 4, (double)gap, showSimple, ShowAll);

  CircularEdgeReport* report = imaqFindCircularEdge(img, *an, insideToOutside?IMAQ_INSIDE_TO_OUTSIDE:IMAQ_OUTSIDE_TO_INSIDE,
    &feo, NULL, origReport);
  return report;
}

CircularEdgeReport* FindDiameter(int idx, const Annulus* ann, BOOL insideToOutside, int thres, BOOL showSimple , BOOL ShowAll , int gap , SpokeReport** origReport )
{
  ImagePtr img = GetImage(idx, DISPLAY);
  if( IsEmpty(img) )
    return NULL;
  Annulus an = *ann;
  //   if( !GetToolAnnulus(idx, id, &an) )
  //     return NULL;
  return FindDiameter(img, &an, insideToOutside, thres, showSimple, ShowAll, gap, origReport);
}


BOOL GetBounding(int idx, ContourID id, Rect* rc)
{
  if( !rc )
    return FALSE;
  ROIPtr roi = imaqCreateROI();
  ROIPtr src_roi = GetROI(idx);
  if( !roi || !src_roi)
    return FALSE;
  RegisterDispose dis_roi(roi);
  RegisterDispose dis_src_roi(src_roi);
  imaqCopyContour(roi, src_roi, id);

  int result = imaqGetROIBoundingBox(roi, rc);
  return result!=0;
}

PointFloat MidPoint(const PointFloat* p1, const PointFloat* p2)
{
  return imaqMakePointFloat((p1->x+p2->x)/2, (p1->y+p2->y)/2);
}

PointFloat MirrorPoint(const PointFloat& start, const PointFloat& center)
{
  float dx = center.x - start.x;
  float dy = center.y - start.y;
  PointFloat pt = {center.x+dx, center.y+dy};
  return pt;
}

string debug_ni_err(int err_code /* =9999 */)
{
  if( err_code == AUTO_NI_ERROR )
    err_code = imaqGetLastError();
  char* err = imaqGetErrorText(err_code);
  RegisterDispose dis_err(err);

#ifdef _DEBUG
  if( err_code != ERR_SUCCESS)
  {
    //cerr<<"NI error: "<<err<<endl;
  }
#endif

  string ret(err);
  return ret;
}

BOOL init_diameter_report(DiameterReport* diameter_report)
{
  if (!diameter_report)
    return FALSE;
  diameter_report->center = imaqMakePointFloat(0, 0);
  diameter_report->crack = 9999.9f;
  diameter_report->roundness = 9999.9f;
  diameter_report->num_particle = 0;
  diameter_report->radius = 0;
  diameter_report->maxDiameter = 0;
  diameter_report->maxDiaPt1 = imaqMakePointFloat(0, 0);
  diameter_report->maxDiaPt2 = imaqMakePointFloat(0, 0);
  diameter_report->minDiameter = 0;
  diameter_report->minDiaPt1 = imaqMakePointFloat(0, 0);
  diameter_report->minDiaPt2 = imaqMakePointFloat(0, 0);
  return TRUE;
}

BOOL CalcDiameter(ImagePtr img, const DiameterOptions* dia_opt, DiameterReport *dia_rpt )
{
  hi_res_timer timer;
  if (!img || !dia_opt || !init_diameter_report(dia_rpt))
  {
    return FALSE;
  }
  BOOL is_white = dia_opt->isWhite;
  Annulus an = dia_opt->ann;
  int result = 0; 
  int knl[] = {1,1,1, 1,1,1, 1,1,1};
  StructuringElement se;
  se.matrixCols = 3;
  se.matrixRows = 3;
  se.hexa = FALSE;
  se.kernel = knl;
  if (is_white == FALSE)
  {
    result = imaqThreshold(img, img, 0, 0, TRUE, 1);
  }
  result = imaqSetBorderSize(img, 1);
  result = imaqMorphology(img, img, IMAQ_CLOSE, &se);
  result = imaqThreshold(img, img, (float)is_white, (float)is_white, TRUE, 255);

  SpokeReport* spoke = NULL;
  CircularEdgeReport* cer = FindDiameter(img, &an, dia_opt->insideToOutside, 1, 
    FALSE, FALSE, 2, &spoke);
  RegisterDispose dis_spoke(spoke);
  RegisterDisposeCER dis_cer(cer);

  if( !cer || !spoke || !cer->coordinates)
  {
//    SetMyError(T("未找到合适的圆形"));
    return FALSE; 
  }    

  points orig_contour, filtered_contour;
  point_pairs diagnols;
  get_points(cer->coordinates, cer->numCoordinates, orig_contour);
  filtered_contour = orig_contour;
  //   timer.Start();
  match_pairs(filtered_contour, an.center, diagnols, false, (float)cer->radius);
  //   timer.Stop();
  //   lg<<enDebug<<T("match_pairs costs ")<<timer.GetMS()<<T(" ms")<<endl;

  PointFloat minPt1, minPt2, maxPt1, maxPt2;
  float minDia = FLT_MAX;
  float maxDia = -FLT_MAX;
  for (int i=0; i<(int)diagnols.size(); i++)
  {
    const point_pair& pp = diagnols[i];
    float d = point2point(pp.first, pp.second);
    if (d == 0)
    {
      continue;
    }
    if( minDia > d )
    {
      minDia = d;
      minPt1 = pp.first.ptf();
      minPt2 = pp.second.ptf();
    }
    if( maxDia < d )
    {
      maxDia = d;
      maxPt1 = pp.first.ptf();
      maxPt2 = pp.second.ptf();
    }
  }
  dia_rpt->maxDiameter = maxDia;
  dia_rpt->minDiameter = minDia;
  dia_rpt->maxDiaPt1 = maxPt1;
  dia_rpt->maxDiaPt2 = maxPt2;
  dia_rpt->minDiaPt1 = minPt1;
  dia_rpt->minDiaPt2 = minPt2;
  dia_rpt->roundness = cer->roundness;
  dia_rpt->center = cer->center;
  dia_rpt->radius = cer->radius;

  return TRUE;
}




// 初步定位中心对称形状，为后面的计算做准备，关键的一步
// ImagePtr try_to_find_shape(const PublicOptions* po,
//                            const DiameterOptions* dia_opt,
//                            DiameterReport* dia_rpt
//                            /*int idx, int thres, bool is_white, 
//                            PointFloat* center,
//                            double* radius*/)
// {
//   if( !po || !dia_opt || !dia_rpt )
//     return NULL;
// 
//   int idx = po->idx;
//   int thres = po->thres;
//   BOOL is_white = dia_opt->isWhite;
//   PointFloat* center = &dia_rpt->center;
//   double* radius = &dia_rpt->radius;
// 
//   ClearMyError();
//   ImagePtr img = GetImage(idx, DISPLAY);
//   if( IsEmpty(img) )
//     return FALSE;
// 
//   ImagePtr copy = save_image(idx, DISPLAY);
//   RegisterDispose dis_copy(copy);
//   if( IsEmpty(copy) )
//     return NULL;
// 
//   int result = 0;
//   if( is_white)
//     result = imaqThreshold( copy, copy, (float)thres, 255, TRUE, 1 );
//   else
//     result = imaqThreshold( copy, copy, 0, (float)thres, TRUE, 1 );
// 
//   int kernel[9] = {
//     1,1,1, 
//     1,1,1, 
//     1,1,1};
//     //StructuringElement se = {3,3,FALSE, kernel};
//     StructuringElement se;
//     se.hexa = FALSE;
//     se.kernel = kernel;
//     se.matrixCols = se.matrixRows = 3;
// 
//     result = imaqFillHoles(copy, copy, TRUE);
//     result = imaqSetBorderSize(copy, 1);
//     result = imaqSizeFilter(copy, copy, TRUE, 1, IMAQ_KEEP_LARGE, &se);
//     result = imaqSetBorderSize(copy, 0);
//     //debug_ni_err();
// 
//     // 全部变白
//     result = imaqThreshold( copy, copy, 1, 1, TRUE, 255 );
// 
//     //restore_image(idx, DISPLAY, copy);
// 
//     //return NULL;
// 
//     int num_part = 0;
//     result = imaqCountParticles(copy, TRUE, &num_part);
// 
//     if( !result || !num_part )
//     {
//       SetMyError(L"未找到目标");
//       return NULL;
//     }
// 
//     // 搜索半径从理想直径的2/3处到4/3处
//     double delta = dia_opt->ann.outerRadius / 3;
//     double min_radius = dia_opt->ann.outerRadius - delta; // 50
//     double max_radius = dia_opt->ann.outerRadius + delta;//200;
//     double min_radius = dia_opt->ann.innerRadius;
//     double max_radius = dia_opt->ann.outerRadius;
//     min_radius = max(0, min_radius);
// 
//     double x = 0, y = 0;
//     // 等效圆半径
//     double rad = 0;
//     // 搜索半径
//     double search_rad = 0;
//     // 找到圆周上的最大半径
//     double max_rad = 0;
//     double w = 0, h = 0;
//     double area = 0;
// 
//     bool found = false;
//     for (int i=0; i<num_part; i++)
//     {
//       result = imaqMeasureParticle(copy, i, FALSE, IMAQ_MT_AREA, &area);
//       rad = sqrt(area/M_PI);
//       if( rad < min_radius || 
//         rad > max_radius )
//         continue;
// 
//       result = imaqMeasureParticle(copy, i, FALSE, IMAQ_MT_CENTER_OF_MASS_X, &x);
//       result = imaqMeasureParticle(copy, i, FALSE, IMAQ_MT_CENTER_OF_MASS_Y, &y);
//       result = imaqMeasureParticle(copy, i, FALSE, IMAQ_MT_BOUNDING_RECT_HEIGHT, &h);
//       result = imaqMeasureParticle(copy, i, FALSE, IMAQ_MT_BOUNDING_RECT_WIDTH, &w);
// 
//       search_rad = (w>h)?h/2:w/2;
// 
//       Annulus an;
//       // 质心作为圆心
//       an.center.x = ROUND_TO_INT(x);
//       an.center.y = ROUND_TO_INT(y);
//       an.innerRadius = 0;
//       an.outerRadius = ROUND_TO_INT(search_rad)*3/2;
//       an.startAngle = 0;
//       an.endAngle = 360;
// 
//       //draw_circle(img, point((float)x, (float)y), (float)an.outerRadius, &ColorLime);
// 
//       CircularEdgeReport* cer = FindDiameter(copy, &an, TRUE, thres, FALSE, FALSE, 20);
//       RegisterDisposeCER dis_cer(cer);
//       if( !cer )
//         continue;
//       if( point2point(cer->center, point(an.center)) > MAX_ECCENTRICITY(rad) ||
//         /*fabs(cer->radius-dia_opt->ann.outerRadius) > delta*/
//         cer->radius > dia_opt->ann.outerRadius ||
//         cer->radius < dia_opt->ann.innerRadius)
//       {
//         continue;
//       }
//       // 查找最大半径点，用于截图
//       max_rad = -DBL_MAX;
//       for (int i=0; i<cer->numCoordinates; i++)
//       {
//         float d = point2point(cer->coordinates[i], cer->center);
//         if( max_rad < d )
//         {
//           max_rad = d;
//         }
//       }
//       *center = cer->center;
//       *radius = cer->radius;
// 
//       found = true;
//       break;
//     }
//     if( !found )
//       return NULL;
// 
//     *radius = max_rad;
// 
//     // #ifndef _DEBUG
//     //restore_image(idx, DISPLAY, copy);
// 
//     Annulus an;
//     an.center = imaqMakePointFromPointFloat(*center);
//     an.innerRadius = 0;
//     an.outerRadius = ROUND_TO_INT(max_rad+1.5);
//     mask_down(copy, &an, false);
// 
//     // #else
//     //restore_image(idx, DISPLAY, copy);
//     // #endif
//     //   draw_points(img, center, 1, &ColorLime, 0, 0, true);
//     //   draw_circle(img, *center, (float)*radius, &ColorMagenta);
// 
//     dis_copy.Detach();
// 
//     return copy;
// }

// void paint_white(ImagePtr img, point pt)
// {
// 	int row = 0;
// 	int col = 0;
// 	byte* buffer = (byte*)imaqImageToArray(img, IMAQ_NO_RECT , &col, &row);
// 	int x = (int)pt.x;
// 	for(int i = 0; i < row; i++)
// 	{
// 		buffer[i*col + x] = 0;
// 	}
// 	imaqArrayToImage(img, buffer, col, row);
// }
// 
// void filter_points(ImagePtr img, points &ptfs, float num, PointFloat an_center, PointFloat cer_center, float radius, float outerradius)
// {
// 	if (ptfs.empty())
// 	{
// 		return;
// 	}
// 	float var = num;
// 	vector<int>idx;
// 	vector<float>dis;
// 	for (UINT i = 0; i < ptfs.size(); i++)
// 	{
// 		float d = fabsf(point2point(an_center, ptfs[i]) - outerradius);
// 		dis.push_back(d);
// 		if ( d < var )
// 		{
// 			idx.push_back(i);
// 		}
// 	}
// 	std::set<int> del;
// 	for (UINT i = 0; i < idx.size(); i++)
// 	{
// 		for (UINT j = idx[i]; j < ptfs.size(); j++)
// 		{
// 			float d = fabsf(point2point(cer_center, ptfs[j]) - radius);
// 			if (d < var)
// 				break;
// 			draw_points(img, &ptfs[j], 2, &ColorRed, 0, 0, true);
// 			del.insert(j);
// 		}
// 		for (int j = idx[i]; j >= 0; j--)
// 		{
// 			float d = fabsf(point2point(cer_center, ptfs[j]) - radius);
// 			if (d < var)
// 				break;
// 			draw_points(img, &ptfs[j], 2, &ColorBlue, 0, 0, true);
// 			del.insert(j);
// 		}
// 	}
// 	if (del.empty())
// 	{
// 		return;
// 	}
// 	int index = *(del.rbegin());
// 	paint_white(img, imaqMakePointFromPointFloat(ptfs[index]));
// 	while(!del.empty())
// 	{
// 		index = *(del.rbegin());
// 		ptfs.erase(ptfs.begin() + index);
// 		del.erase(*(del.rbegin()));
// 	}
// }
