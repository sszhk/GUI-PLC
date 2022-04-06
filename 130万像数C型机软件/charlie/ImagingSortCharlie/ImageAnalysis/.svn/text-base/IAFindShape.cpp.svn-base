#include "stdafx.h"
#include "auto_array.h"
#include "IAMain.h"
#include "Utilities.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "sub_image.h"
#include "point.h"
#include "PolygonNIFloat.h"
#include "IAFindShape.h"
#include <algorithm>
using namespace std;
#include "hi_res_timer.h"
#include "polygon_moments.h"
#include "shadow_image.h"
#include "tools_ni.h"

#define MAX_ECCENTRICITY(rad) ((float)((rad)*0.1f))  // in pixels
#define SQUARE_D(x) ((x)*(x))

#define ENABLE_MEASURE1x

#ifdef ENABLE_MEASURE1
#define DBG_MEASURE(x) x
#else
#define DBG_MEASURE(x)
#endif

#define ENABLE_MEASURE2x

#ifdef ENABLE_MEASURE2
#define DBG_MEASURE2(x) x
#else
#define DBG_MEASURE2(x)
#endif

float SideCrack(ImagePtr img, Annulus an, int kernel)
{
  int result = 0;
  if (!img)
    return 9999.9f;

  ImagePtr copy = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_copy(copy);
  if (!imaqDuplicate(copy, img))
    return 9999.9f;

  if (!imaqFillHoles(copy, copy, TRUE))
    return 9999.9f;
  if (!imaqSetBorderSize(copy, 1))
    return 9999.9f;
  result = imaqMorphology(copy, copy, IMAQ_ERODE, NULL);
  if (!result)
    return 9999.9f;
  if(!imaqSetBorderSize(copy, 0))
    return 9999.9f;
  point centroid;
  if(!imaqCentroid(copy, centroid.pf(), NULL))
    return 9999.9f;
  //draw_points(img, &centroid, 1, &ColorBlue);

#ifndef USE_FIND_DIAMETER
  kernel = 20;

  UINT numCurves ; 
  CurveOptions co;
  co.extractionMode = IMAQ_UNIFORM_REGIONS;
  co.threshold = 1;
  co.filterSize = IMAQ_FINE;
  co.minLength = 100;
  co.rowStepSize = 5;
  co.columnStepSize = 5;
  co.maxEndPointGap = 10;
  co.onlyClosed = FALSE;
  co.subpixelAccuracy = FALSE;
  Curve* curves = imaqExtractCurves(copy, NULL, &co, &numCurves);
  RegisterDispose dis_curves(curves);
  if( !numCurves || !curves )
    return 9999.9f;
  PT* pts = curves[0].points;
  int num = curves[0].numPoints;

#else
  kernel = 6;
  an.innerRadius = 0;
  an.outerRadius = ROUND_TO_INT(an.outerRadius*1.2f);
  CircularEdgeReport* cer = FindDiameter(copy, &an, TRUE, 1, FALSE, FALSE, 1);
  RegisterDisposeCER dis_cer(cer);
  if( !cer || !cer->numCoordinates)
    return 0;

  PT* pts = cer->coordinates;
  int num = cer->numCoordinates;
#endif

  moments moms;
  points contour;
  translate_points(pts, num, contour, 0, 0);
  polygon_moments(contour, &moms);
  centroid = point(moms.cx, moms.cy);
  //draw_points(copy, &centroid, 1, &ColorRed, 0, 0, true);

  vector<float> radius;
  //   for (UINT i = 0; i < numCurves; i++)
  //   {
  //draw_points(copy, pts, num, &ColorBlue);
  for (int j = 0; j < num; j++)
  {
    float r = point2point(centroid, pts[j]);
    radius.push_back(r);
  }
  //   }
  //std::reverse(radius.begin(), radius.end());

  UINT max_idx = 0;
  float max_var = -9999;
  float stdvar = 0;
  //float max_angle = 0;
  float max_dis = 0;
  for(UINT i=0; i<radius.size(); i++)
  {
    stdvar = 0;
    for (UINT j=i-kernel/2; j<i+kernel/2; j++)
    {
      int safe_idx = GetSafeIndex(j, radius.size());
      int ref_idx = GetSafeIndex(i-kernel/2, radius.size());
      float diff = radius[safe_idx]-radius[ref_idx];
      //if( diff > 0 )
      stdvar += SQUARE_D(diff);
      //else
      //  stdvar -= SQUARE_D(diff);
    }
    //     int r1 = GetSafeIndex(i-kernel/2, radius.size());
    //     int r2 = GetSafeIndex(i+kernel/2, radius.size());
    //     float d = radius[i];
    //     float d12 = (radius[r1]+radius[r2])/2;
    // //     stdvar += d12-d;
    // 
    //     float angle = /*180 - */Angle(pts[r2], pts[i], pts[r1]);
    //     if( d > d12 )
    //       angle = -angle;
    //     //cerr<<"distance "<<stdvar<<", angle "<<angle<<endl;
    //     stdvar *= angle;

    if( max_var < stdvar 
      /*&& radius[i] < (radius[r1]+radius[r2])/2*/ )
    {
      max_var = stdvar;
      max_idx = i;
      //max_angle = angle;
      //max_dis = d12-d;
    }
  }

  //   cerr<<"max_var="<<max_var
  //     <<", max_idx="<<max_idx
  //     //<<", max_angle="<<max_angle
  //     //<<", max_dis="<<max_dis
  //     <<endl;

  //   vector<float>diff;
  //   float d = fabsf(radius[0] - radius[radius.size() - 1]);
  //   diff.push_back(d);
  //   for (UINT i = 1; i < radius.size(); i++)
  //   {
  // 	  d = fabsf(radius[i] - radius[i - 1]);
  // 	  diff.push_back(d);
  //   }
  // 
  //   vector<float>sum_diff;
  //   for (UINT i = 0; i < diff.size(); i++)
  //   {
  // 	  stdvar = 0;
  // 	  UINT from = i;
  // 	  UINT to = i + kernel;
  // 	  for (UINT j = i; j < i + kernel; j++)
  // 	  {
  // 		  UINT safe_idx = GetSafeIndex(j, diff.size()); 
  // 		  stdvar += SQUARE_D(diff[safe_idx]);
  // 	  }
  // 	  sum_diff.push_back(stdvar);
  // 	  if (stdvar > max_var)
  // 	  {
  // 		  max_var = stdvar;
  // 		  max_idx = i;
  // 	  }
  //   }

//   draw_line(img, centroid.ptf(), pts[max_idx], 0, 0, &ColorRed, TRUE);
  draw_line(img, centroid.ptf(), pts[GetSafeIndex(max_idx-kernel/2, num)], 0, 0, &ColorRed, TRUE);

  //   draw_points(img, &pts[GetSafeIndex(max_idx-kernel/2, num)], 1, &ColorLime, 0, 0, true);
  //   draw_points(img, &pts[GetSafeIndex(max_idx, num)], 1, &ColorLime, 0, 0, true);
  //   draw_points(img, &pts[GetSafeIndex(max_idx+kernel/2, num)], 1, &ColorLime, 0, 0, true);
  //UINT safe_idx = GetSafeIndex(max_idx + kernel, diff.size()); 
  //draw_line(img, centroid, curves[0].points[safe_idx], 0, 0, &ColorRed, TRUE);
  return max_var;
}

// BOOL keep_particle_between(ImagePtr img, MeasurementType mt, float low, float hi, int*num)
// {
//   if(!img)
//     return FALSE;
//   ParticleFilterCriteria2 pfc;
//   pfc.calibrated = FALSE;
//   pfc.exclude = FALSE;
//   pfc.parameter = mt;
//   pfc.lower = low;
//   pfc.upper = hi;
//   ParticleFilterOptions pfo;
//   pfo.connectivity8 = TRUE;
//   pfo.rejectBorder = FALSE;
//   pfo.rejectMatches = FALSE;
//   return imaqParticleFilter3(img, img, &pfc, 1, &pfo, NULL, num);
// }

BOOL init_find_shape_report(FindShapeReport* find_shape_report)
{
  if(!find_shape_report)
    return FALSE;
  find_shape_report->center = imaqMakePointFloat(0, 0);
  find_shape_report->crack = 9999.9f;
  find_shape_report->maxDiameter = 0;
  find_shape_report->maxDiaPt1 = imaqMakePointFloat(0, 0);
  find_shape_report->maxDiaPt2 = imaqMakePointFloat(0, 0);
  find_shape_report->minDiameter = 0;
  find_shape_report->minDiaPt1 = imaqMakePointFloat(0, 0);
  find_shape_report->minDiaPt2 = imaqMakePointFloat(0, 0);
  find_shape_report->radius = 0;
  find_shape_report->roundness = 9999.9f;
  find_shape_report->num_particle = 0;
  return TRUE;
}

BOOL find_shape(ImagePtr image,
                const FindShapeOptions* so,
                FindShapeReport* sr)
{
  ENTER_FUNCTION;

  if(IsEmpty(image) || !so || !init_find_shape_report(sr))
    return FALSE;

  BOOL is_white = so->isWhite;
  BOOL deltrench = so->DelTrench;
  PointFloat center = sr->center;
  Annulus an = so->ann;

  float threshold = (is_white == 1 ? (float)255 : (float)0);
  RETURN_FALSE(imaqThreshold( image, image, threshold, threshold, TRUE, 1 ));

  int num_particle = 0;
  int result = find_shape(image, so->ann, num_particle, FALSE);

  if (!result || num_particle != 1)
  {
    sr->num_particle = num_particle;
    double left = 0;
    double top = 0;
    double width = 0;
    double height = 0;
    for (int i = 0; i < num_particle; i++)
    {
      RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, IMAQ_MT_BOUNDING_RECT_LEFT, &left));
      RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, IMAQ_MT_BOUNDING_RECT_TOP, &top));
      RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, IMAQ_MT_BOUNDING_RECT_WIDTH, &width));
      RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, IMAQ_MT_BOUNDING_RECT_HEIGHT, &height));
      RETURN_FALSE(imaqOverlayRect(image, 
        imaqMakeRect((int)top, (int)left, (int)height, (int)width), 
        &ColorRed, IMAQ_DRAW_VALUE, NULL));
    }
    return FALSE;
  }
  RETURN_FALSE(imaqFillHoles(image, image, TRUE));

  point centroid;
  RETURN_FALSE(imaqCentroid(image, centroid.pf(), NULL));
  // 质心作为圆心
  an.center.x = ROUND_TO_INT(centroid.x);
  an.center.y = ROUND_TO_INT(centroid.y);
  an.innerRadius = 0;
  an.outerRadius = so->ann.outerRadius;
  an.startAngle = 0;
  an.endAngle = 360;

  if (so->isCrack == 1)
    sr->crack = SideCrack(image, an, KERNEL_CRACK);

  DiameterOptions dia_opt;
  dia_opt.ann = an;
  dia_opt.isWhite = so->isWhite;
  dia_opt.insideToOutside = TRUE;
  DiameterReport dia_rpt;
  if (!CalcDiameter(image, &dia_opt, &dia_rpt))
    return FALSE;

  sr->center = dia_rpt.center;
  sr->maxDiameter = dia_rpt.maxDiameter;
  sr->maxDiaPt1 = dia_rpt.maxDiaPt1;
  sr->maxDiaPt2 = dia_rpt.maxDiaPt2;
  sr->minDiameter = dia_rpt.minDiameter;
  sr->minDiaPt1 = dia_rpt.minDiaPt1;
  sr->minDiaPt2 = dia_rpt.minDiaPt2;
  sr->radius = (float)dia_rpt.radius;
  sr->roundness = dia_rpt.roundness;

  return TRUE;
}

wstring find_fine_circle(ImagePtr img, point& center, float& radius)
{
  hi_res_timer timer;
  wstringstream ss;

  Annulus an;
  an.innerRadius = 0;
  an.outerRadius = ROUND_TO_INT(radius);
  an.center = imaqMakePointFromPointFloat(center.ptf());
  an.startAngle = 0;
  an.endAngle = -180;                                         
  CircularEdgeReport * cer = NULL;          
  {
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    cer = FindDiameter(img, &an, FALSE, 1, FALSE, FALSE, 2);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"find_fine_circle::FindDiameter costs "<<timer.GetMS()<<endl);
  }
  RegisterDisposeCER dis_cer(cer);
  if (!cer || !cer->coordinates)
    return ss.str();

  FitEllipseOptions feo;
  feo.maxIterations = 1;
  feo.minScore = 1000;
  feo.pixelRadius = 2;
  feo.rejectOutliers = TRUE;
  /*
  int    rejectOutliers; //Whether to use every given point or only a subset of the points to fit the circle.
  double minScore;       //Specifies the required quality of the fitted circle.
  double pixelRadius;    //The acceptable distance, in pixels, that a point determined to belong to the circle can be from the circumference of the circle.
  int    maxIterations;  //Specifies the number of refinement iterations you allow the function to perform on the initial subset of points.
  */
  FitCircleOptions fco = {TRUE, 900, 2, 1};
  //BestEllipse2 * be = NULL;
  BestCircle2* bc = NULL;
  {
    DBG_MEASURE(ss<<"fit points "<<cer->numCoordinates<<endl);
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    //     be = imaqFitEllipse2(cer->coordinates, 
    //       cer->numCoordinates, &feo );
    //     ss<<"fit used points "<<be->numPointsUsed<<endl;
    bc = imaqFitCircle2(cer->coordinates, cer->numCoordinates, &fco);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"find_fine_circle::imaqFitCircle2 costs "<<timer.GetMS()<<endl);
  }
  RegisterDispose dis_bc(bc);
  //   RegisterDispose dis_be(be);
  //   report->outerRadius = ROUND_TO_INT(point2point(be->majorAxisEnd, 
  //     be->majorAxisStart)/2);

  center = bc->center;
  radius = (float)bc->radius+1;
  //   report->outerRadius = ROUND_TO_INT(bc->radius)+1;
  //   report->innerRadius = 0;
  //   report->center = imaqMakePointFromPointFloat(bc->center);
  //   report->startAngle = 0;
  //   report->endAngle = 360;
  return ss.str();
}

wstring mask_down(ImagePtr img, CPT& center, float radius, bool white_outside)
{
  wstringstream ss;
  hi_res_timer timer;

  if( IsEmpty(img) )
    return ss.str();

  //   ROIPtr roi = imaqCreateROI();
  //   RegisterDispose dis_roi(roi);
  int result = 0;
  //   {
  //     //DbgTimeCount dbg("mask_down::imaqAddAnnulusContour");
  //     result = imaqAddAnnulusContour(roi, *ann);
  //   }

  ImagePtr mask = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_mask(mask);
  int w = 0;
  int h = 0;
  {
    //DbgTimeCount dbg("mask_down::imaqGetImageSize");
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    result = imaqGetImageSize(img, &w, &h);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"mask_down::imaqGetImageSize "<<timer.GetMS()<<endl);
  }
  if( !result )
    return ss.str();
  {
    //DbgTimeCount dbg("mask_down::imaqSetImageSize");
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    result = imaqSetImageSize(mask, w, h);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"mask_down::imaqSetImageSize "<<timer.GetMS()<<endl);
  }
  PixelValue pv = {white_outside?1.0f:0};
  {
    //DbgTimeCount dbg("mask_down::imaqFillImage");
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    result = imaqFillImage(mask, pv, NULL);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"mask_down::imaqFillImage "<<timer.GetMS()<<endl);

  }
  Rect rc = imaqMakeRect(
    ROUND_TO_INT(center.y-radius),
    ROUND_TO_INT(center.x-radius),
    ROUND_TO_INT(radius*2),
    ROUND_TO_INT(radius*2));
  {
    //DbgTimeCount dbg("mask_down::imaqDrawShapeOnImage");
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    result = imaqDrawShapeOnImage(mask, mask, rc, 
      IMAQ_PAINT_VALUE, IMAQ_SHAPE_OVAL, white_outside?0:255.0f);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"mask_down::imaqDrawShapeOnImage "<<timer.GetMS()<<endl);

  }
  //   {
  //     DbgTimeCount dbg("imaqROIToMask");
  //     result = imaqROIToMask(mask, roi, 255, img, NULL);
  //   }

  //if( white_outside )
  //   {
  //     DbgTimeCount dbg("imaqInverse");
  //     result = imaqInverse(mask, mask, NULL);
  //   }
  //imaqInverse(mask, mask,NULL);
  pv.grayscale = white_outside?1.0f:0;
  {
    //DbgTimeCount dbg("mask_down::imaqFillImage");
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    //result = imaqFillImage(img, pv, mask);
    imaqCopyOverlay(mask, img, NULL);
    result = imaqAnd(img, img, mask);
    imaqCopyOverlay(img, mask, NULL);
    DBG_MEASURE(DBG_MEASURE(timer.Stop()));
    DBG_MEASURE(ss<<"mask_down::imaqFillImage "<<timer.GetMS()<<endl);
  }
  return ss.str();
}
BOOL remove_small(ImagePtr img)
{
  if( IsEmpty(img) )
    return FALSE;
  int kernel[9] = {
    1,1,1,
    1,1,1,
    1,1,1
  };
  int result = 0;
  StructuringElement se;
  se.kernel = kernel;
  se.hexa = FALSE;
  se.matrixCols = 3;
  se.matrixRows = 3;
  RETURN_FALSE(imaqSetBorderSize(img, 1));
  RETURN_FALSE(imaqSizeFilter(img, img, TRUE, 1, IMAQ_KEEP_LARGE, &se));
  RETURN_FALSE(imaqSetBorderSize(img, 0));
  return TRUE;
}
BOOL draw_destructive_circle(ImagePtr img, float grayscale, CPT& center, 
                             float radius, bool fill=false)
{
  Rect rc = imaqMakeRect(
    ROUND_TO_INT(center.y-radius), 
    ROUND_TO_INT(center.x - radius),
    ROUND_TO_INT(radius*2),
    ROUND_TO_INT(radius*2));
  return imaqDrawShapeOnImage(img, img, rc, 
    fill?IMAQ_PAINT_VALUE:IMAQ_DRAW_VALUE, IMAQ_SHAPE_OVAL, grayscale);
}

BOOL find_and_remove_border(ImagePtr copy)
{
  const int W = WIDTH_IMAGE;
  const int H = HEIGHT_IMAGE;
  const int xw = 40;
  const int xh = 40;
  // >= 70%
  ImagePtr mask = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  //   ROIPtr roi = imaqCreateROI();
  RegisterDispose dis_mask(mask);
  //   RegisterDispose dis_roi(roi);
  int result = 0;
  RETURN_FALSE(imaqSetImageSize(mask, W, H));
  PixelValue pv = {0};
  RETURN_FALSE(imaqFillImage(mask, pv, NULL));
  Rect rc[4] = {{0, 0, xh, xw},{0, W-xw, xh, xw},{H-xh, 0, xh, xw},{H-xh, W-xw, xh, xw}};
  for( int i=0; i<4; i++ )
  {
    RETURN_FALSE(imaqDrawShapeOnImage(mask, mask, rc[i], IMAQ_PAINT_VALUE, 
      IMAQ_SHAPE_RECT, 1.0f));
  }
  //   result = imaqAddRectContour(roi, imaqMakeRect(0, 0, xh, xw));
  //   result = imaqAddRectContour(roi, imaqMakeRect(0, W-xw, xh, xw));
  //   result = imaqAddRectContour(roi, imaqMakeRect(H-xh, 0, xh, xw));
  //   result = imaqAddRectContour(roi, imaqMakeRect(H-xh, W-xw, xh, xw));
  //   result = imaqROIToMask(mask, roi, 255, copy, NULL);
  //result = imaqInverse(mask, mask, NULL);
  //   imaqDuplicate(copy, mask);
  //   imaqThreshold(copy, copy, 255,255,TRUE, 1);
  //   return;
  HistogramReport* his = imaqHistogram(copy, 2, 0, 1, mask);
  RegisterDispose dis_his(his);
  if( !his || !his->numPixels )
    return FALSE;
  float ratio = (float)his->histogram[1]/his->numPixels;
  if( ratio < 0.5f )
    return TRUE;
  //result = imaqMask(copy, copy, mask);

  Annulus ann;
  ann.startAngle = 0;
  ann.endAngle = 360;
  ann.center = imaqMakePoint(W/2, H/2);
  ann.innerRadius = 200;
  ann.outerRadius = ROUND_TO_INT(sqrt((float)W/2*W/2+H/2*H/2))+1;

  /*
  int    threshold;        //Specifies the threshold for the contrast of an edge.
  int    width;            //The number of pixels that the function averages to find the contrast at either side of the edge.
  int    steepness;        //The span, in pixels, of the slope of the edge projected along the path specified by the input points.
  double subsamplingRatio; //The number of pixels that separates two consecutive search lines.
  int    showSearchArea;   //If TRUE, the function overlays the search area on the image.
  int    showSearchLines;  //If TRUE, the function overlays the search lines used to locate the edges on the image.
  int    showEdgesFound;   //If TRUE, the function overlays the locations of the edges found on the image.
  int    showResult;       //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.
  */
  FindEdgeOptions feo = {1,4,2,8,FALSE, FALSE,FALSE,FALSE};
  CircularEdgeReport* cer = NULL;
  {
    //DbgTimeCount dbg("find_and_remove_border::imaqFindCircularEdge");
    cer = imaqFindCircularEdge(copy, ann, 
      IMAQ_OUTSIDE_TO_INSIDE, &feo, NULL, NULL);
  }
  RegisterDisposeCER dis_cer(cer);
  if( !cer )
    return FALSE;
  ann.center = imaqMakePointFromPointFloat(cer->center);
  ann.innerRadius = 0;
  ann.outerRadius = ROUND_TO_INT(cer->radius);
  {
    //DbgTimeCount dbg("find_and_remove_border::mask_down");
    mask_down(copy, point(ann.center).ptf(), (float)ann.outerRadius, false);
  }
  return TRUE;
  //draw_destructive_circle(copy, 0, cer->center, (float)cer->radius);
}
BOOL visible_copy(int idx, ImagePtr copy) 
{
  RETURN_FALSE(imaqThreshold(copy, copy, 0, 0, TRUE, 255));
  RETURN_FALSE(restore_image(idx, DISPLAY, copy));
  return TRUE;
}


wstring find_and_remove_trench(ImagePtr copy, PT& center, float& radius)
{
  // 查找最佳半径时，在粗略半径基础上的外扩比例
#define FIND_FINEST_CIRCLE_RADIUS 1.2f
  // 查找沟时，在最佳半径基础上的外扩比例
#define FIND_TRENCH_CIRCLE_RADIUS 1.05f

  // 查找沟时，安全区域的角度范围
#define SAFE_ZONE1_START_DEG 55
#define SAFE_ZONE1_END_DEG (180-65)
#define SAFE_ZONE2_START_DEG 180
#define SAFE_ZONE2_END_DEG 360

  wstringstream ss;
  hi_res_timer timer;
  ImagePtr copy_copy = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_copycopy(copy_copy);
  {
    DBG_MEASURE(DBG_MEASURE(timer.Start()));
    imaqDuplicate(copy_copy, copy);
    DBG_MEASURE(timer.Stop());
    DBG_MEASURE(ss<<"find_and_remove_trench::imaqDuplicate costs "<<timer.GetMS()<<endl);
  }

  point finest_center = center;
  float finest_radius = radius * FIND_FINEST_CIRCLE_RADIUS;
  {
    DBG_MEASURE(timer.Start());
    ss<<find_fine_circle(copy, 
      finest_center, 
      finest_radius);
    DBG_MEASURE(timer.Stop());
    DBG_MEASURE(ss<<"find_and_remove_trench::find_fine_circle costs "<<timer.GetMS()<<endl);
  }
  center = finest_center.ptf();
  draw_circle(copy, finest_center.ptf(), finest_radius, &ColorRed);
  // 	draw_points(copy, &finest_center, 1, &ColorRed, 0, 0, true);

  //point center = finest_center;
  //draw_points(copy, &finest_center, 1, &ColorChocolate, 0, 0, true);

  float walk_radius = FIND_TRENCH_CIRCLE_RADIUS*finest_radius;
  int count = ROUND_TO_INT(walk_radius*M_PI*2); // perimeter
  float dr = (float)(walk_radius - finest_radius);
  //draw_circle(copy, center, walk_radius, &ColorBlue);

  vector<int> last_index;
  vector<int> first_index;
  bool was_white = false;

  DBG_MEASURE(timer.Start());
  int last_t = -1;
  for (int i=0; i<count; i+=2)
  {
    float rad = (float)(i*M_PI/(count/2));
    float deg = radian2degree(rad);
    //cerr<<deg<<endl;
    // safe zone :)
    if( deg > SAFE_ZONE1_START_DEG && deg < SAFE_ZONE1_END_DEG )
      continue;
    if( deg > SAFE_ZONE2_START_DEG && deg < SAFE_ZONE2_END_DEG )
      continue;
    float x = walk_radius * cosf(rad) + center.x;
    float y = walk_radius * sinf(rad) + center.y;
    PixelValue pv;
    imaqGetPixel(copy_copy, point(x,y).pt(), &pv);

    if( pv.grayscale != 0 )
    {
      if( !was_white )
        first_index.push_back(i);
      //cerr<<"here! "<<deg<<endl;
      //       if( (i-1) != last_t )
      //       {
      draw_destructive_circle(copy, 0, point(x,y).ptf(), dr, true);
      //draw_circle(copy, point(x,y), dr, &ColorLime);
      //         last_t = i;
      //       }
      was_white = true;
      //       Rect rc = imaqMakeRect(
      //         ROUND_TO_INT(y-dr), 
      //         ROUND_TO_INT(x-dr), 
      //         (int)floor(2*dr), 
      //         (int)floor(2*dr));
      //       imaqDrawShapeOnImage(copy, copy, rc, 
      //         IMAQ_PAINT_VALUE, IMAQ_SHAPE_OVAL, 0);
    }
    else 
    {
      if( was_white )
        last_index.push_back(i);

      was_white = false;
    }
    //draw_points(copy, &point(x,y), 1, &ColorGold, 0, 0, true);
  }
#define DIFF_ANGLE 5
  // 顺延“涂黑”，确保沟的根部清除干净
  //   for (int i=0; i<(int)first_index.size(); i++)
  //   {
  //     for( int j=first_index[i]; j>=(first_index[i]-count*DIFF_ANGLE/360); j--)
  //     {
  //       int idx = safe_cyc_idx(j, count);
  //       float rad = (float)(idx*M_PI/(count/2));
  //       float deg = radian2degree(rad);
  //       //cerr<<deg<<endl;
  //       // safe zone :)
  //       if( deg > SAFE_ZONE1_START_DEG && deg < SAFE_ZONE1_END_DEG )
  //         continue;
  //       if( deg > SAFE_ZONE2_START_DEG && deg < SAFE_ZONE2_END_DEG )
  //         continue;
  //       float x = walk_radius * cosf(rad) + center.x;
  //       float y = walk_radius * sinf(rad) + center.y;
  //       draw_destructive_circle(copy, 0, point(x,y), dr, true);
  //     }
  //   }
//   for (int i=0; false && i<(int)last_index.size(); i++)
//   {
//     for( int j=last_index[i]; j<(last_index[i]+count*DIFF_ANGLE/360); j++)
//     {
//       int idx = GetSafeIndex(j, count);
//       float rad = (float)(idx*M_PI/(count/2));
//       float deg = radian2degree(rad);
//       //cerr<<deg<<endl;
//       // safe zone :)
//       if( deg > SAFE_ZONE1_START_DEG && deg < SAFE_ZONE1_END_DEG )
//         continue;
//       if( deg > SAFE_ZONE2_START_DEG && deg < SAFE_ZONE2_END_DEG )
//         continue;
//       float x = walk_radius * cosf(rad) + center.x;
//       float y = walk_radius * sinf(rad) + center.y;
//       draw_destructive_circle(copy, 0, point(x,y), dr, true);
//     }
//   }
  DBG_MEASURE(timer.Stop());
  DBG_MEASURE(ss<<"find_and_remove_trench::fill trench costs "<<timer.GetMS()<<endl);

  center = finest_center.ptf();
  radius = finest_radius;

  return ss.str();
  //   Annulus mask_an = an;
  //   an.outerRadius = ROUND_TO_INT(walk_radius);
  //   mask_down(copy, &an, false);
}


IA_EXPORT BOOL IA_Calc_FindShape(const PublicOptions* po,
                                 const FindShapeOptions* so,
                                 FindShapeReport* sr
                                 /*int idx, int thres, bool is_white, 
                                 PointFloat* center,
                                 double* radius*/)
{
  ENTER_FUNCTION;

  if( !po || !so || !init_find_shape_report(sr) )
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  BOOL is_white = so->isWhite;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);

  if (is_white)
  {
    if(!find_shape(img, so, sr))
    {
      img.detach();
      return FALSE;
    }
    draw_circle(img, imaqMakePointFloat((float)sr->center.x, (float)sr->center.y), sr->radius, &ColorRed);
    if (so->isMaxDiameter == 1)
      draw_line(img, sr->maxDiaPt1, sr->maxDiaPt2, 0, 0, &ColorYellowGreen, TRUE, TRUE);
    if (so->isMinDiameter == 1)
      draw_line(img, sr->minDiaPt1, sr->minDiaPt2, 0, 0, &ColorBlue, TRUE, TRUE);
    return TRUE;
  }
  
  wstringstream ss;
  RETURN_FALSE(imaqThreshold( img, img, 0, 0, TRUE, 1 ));
  // 去除小颗粒
  RETURN_FALSE(remove_small(img));
  RETURN_FALSE(find_and_remove_border(img));

  point IMAGE_CENTER(WIDTH_IMAGE/2, HEIGHT_IMAGE/2);
  bool found = false;
  float found_radius = ((float)so->ann.innerRadius+so->ann.outerRadius)/2;
  point found_center;
  int iterations = 0;
  const int MAX_ITERATIONS = 1;
  for(int k=0; k<MAX_ITERATIONS; k++, iterations++) 
  {
    // 全局查找形状
    int num_part = 0;
    found = false;
    RETURN_FALSE(imaqCountParticles(img, TRUE, &num_part));
    if( !num_part )
    {
      RETURN_FALSE(visible_copy(idx, img));
      return FALSE;
    }
    double center_x = 0, center_y = 0;
    double area = 0;
    for (int i=0; i<num_part; i++)
    {
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_CENTER_OF_MASS_X, &center_x));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_CENTER_OF_MASS_Y, &center_y));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_PARTICLE_AND_HOLES_AREA, &area));

      double radius = sqrt(area/M_PI);
      point _center(center_x, center_y);
      found_center = _center;
 
      if( abs(center_y-IMAGE_CENTER.y) < 100 && 
        abs(radius-so->ann.outerRadius)/so->ann.outerRadius < 0.2 ) 
      {
        double ratio = abs(found_radius-radius)/found_radius;
        ss<<mask_down(img, _center.ptf(), (float)radius*1.2f, false);
        found = true;
        found_radius = (float)radius;
        if( ratio < 0.01)
          k = MAX_ITERATIONS;
        break;
      }
    }
    if( !found ) 
    {
      img.detach();
      return FALSE;
    }
  }
  point finest_center = found_center;
  float finest_radius = found_radius;

  ss<<find_and_remove_trench(img, finest_center.ptf(), finest_radius);

  sr->radius = finest_radius;
  sr->center = finest_center.ptf();
  float mask_radius = finest_radius*1.05f;
  mask_down(img, finest_center.ptf(), mask_radius, false); 

  if (so->isCrack == 1)
    sr->crack = SideCrack(img, so->ann, KERNEL_CRACK);

  if (so->isMaxDiameter || so->isMinDiameter )
  {
    DiameterOptions dia_opt;
    dia_opt.ann = so->ann;
    dia_opt.ann.center = imaqMakePoint((int)finest_center.x, (int)finest_center.y);
    dia_opt.insideToOutside = FALSE;
    dia_opt.isWhite = so->isWhite;
    DiameterReport dia_rpt;
    if (CalcDiameter(img, &dia_opt, &dia_rpt))
    {
      sr->center = dia_rpt.center;
      sr->maxDiameter = dia_rpt.maxDiameter;
      sr->maxDiaPt1 = dia_rpt.maxDiaPt1;
      sr->maxDiaPt2 = dia_rpt.maxDiaPt2;
      sr->minDiameter = dia_rpt.minDiameter;
      sr->minDiaPt1 = dia_rpt.minDiaPt1;
      sr->minDiaPt2 = dia_rpt.minDiaPt2;
      sr->radius = (float)dia_rpt.radius;
      sr->roundness = dia_rpt.roundness;
      if (so->isMaxDiameter)
        draw_line(img, sr->maxDiaPt1, sr->maxDiaPt2, 0, 0, &ColorYellowGreen, TRUE, TRUE);
      if (so->isMinDiameter)
        draw_line(img, sr->minDiaPt1, sr->minDiaPt2, 0, 0, &ColorBlue, TRUE, TRUE);
    }
  }
  RETURN_FALSE(imaqThreshold( img, img, 0, 0, TRUE, 255 ));
  return TRUE;
}

