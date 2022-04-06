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
* Created:     2010-04-15  15:42
* Filename:    IACalcTeeth.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     
*
*/
#include "stdafx.h"
#include "IAMain.h"
#include "NICalc.h"
#include "Utilities.h"
#include "PolygonNIFloat.h"
#include <fstream>
#include "assert.h"
#include "hi_res_timer.h"
#include "sub_image.h"
#include "algorithm"
#include "NIDispose.h"
#include "NIROI.h"
#include "polygon_moments.h"
#include "tools_ni.h"
#include "tools_geometry.h"
#include "shadow_image.h"
#include "overlay.h"

using namespace std;

#define COEFFICIENT 0.6495f
#define  DIFERENCE_DISTENCE 0.8

extern Point get_point(const PointFloat& pt1, float dx, float dy);

#pragma pack(push, 4)
struct TeethReport
{
  int teeth1;
  int teeth2;
  float pitch;
  float maxTeeth;
  float minTeeth;
  float outerDiameter;
  float innerDiameter;
  float midDiameter;
  float teethLenth;
  float helixAngle;
  float maxSlope;
  float minSlope;
  float ratio;
  float cylinder;
  float pilot;
  PointFloat left_most;
};
struct TeethOptions
{
  Rect rc;
  Rect rc_teeth;
  BOOL maxOutDiameter;
  BOOL minOutDiameter;
  BOOL midDiameter;
  BOOL bottomDiameter;
  BOOL pitch;
  BOOL length;
  BOOL slopeteeth;
  BOOL helixAngle;
  BOOL teethCount;
  BOOL ratio;
  BOOL cylinder;
  BOOL pilot;
  int kernel;
  BOOL angle_sens;
  float offset_x;
};
#pragma pack(pop)

void HelixAngle(TeethReport *report)
{
  if (!report)
    return;
  float d = (report->outerDiameter - report->innerDiameter)/2;
  float ll = report->pitch;
  report->helixAngle = (float)atan2((float)M_PI*d, ll);
  report->helixAngle = radian2degree(report->helixAngle);
}

//求中径的点，以及画出他们连线
void MakeMidLine(ImagePtr img, Points& midpoints, const Points& ptfs,
                 PointFloat spt, PointFloat ept, float distance, bool upordown)
{   
  if (ptfs.empty())
    return;

  midpoints.clear();
  PointFloat midpoint;
  float cosangle = (ept.x - spt.x)/point2point(spt, ept);
  float sinangle = (ept.y - spt.y)/point2point(spt, ept);
  for (int i = 0; i < (int)ptfs.size(); i++)
  {
    float dx = distance*sinangle/2;
    float dy = distance*cosangle/2; 
    midpoint.x = upordown?(ptfs[i].x - dx):(ptfs[i].x + dx);
    midpoint.y = upordown?(ptfs[i].y + dy):(ptfs[i].y - dy);
    midpoints.push_back(midpoint);
  }
}

//给2条线段上的点配对，画出连线
void FitMidPoints(ImagePtr img, const Points& upmidpoints,
                  const Points& downmidpoints, 
                  PointFloat sptf, PointFloat eptf, float distance, 
                  float& minslope, float &maxslope)
{
  if (upmidpoints.empty() || downmidpoints.empty())
    return;

  float cosangle=(eptf.x - sptf.x) / point2point(sptf, eptf);
  float sinangle=(eptf.y - sptf.y) / point2point(sptf, eptf);
  float dx = 2 * point2line(upmidpoints[0], sptf, eptf, false) * sinangle;
  float dy = 2 * point2line(upmidpoints[0], sptf, eptf, false) * cosangle;
  PointFloat pt;
  const float RIGHTANGLE = 90;
  float angle = 0;
  minslope = fabsf(angle - RIGHTANGLE);
  maxslope = 0;
  int j = 0;
  int i = 0;
  for (j = 0; j < (int)upmidpoints.size() - 1; j++)
  {
    pt.x = upmidpoints[j].x - dx;
    pt.y = upmidpoints[j].y + dy;
    for (i=0; i < (int)downmidpoints.size() - 1; i++)
    {
      if (fabsf(point2point(pt, downmidpoints[i]))<distance*DIFERENCE_DISTENCE)
      {
        if(i > 0)
        {
          int angleresult = imaqGetAngle(sptf, eptf, upmidpoints[j], downmidpoints[i], &angle);
          minslope = min(minslope, fabsf(angle - RIGHTANGLE));
          maxslope = max(maxslope, fabsf(angle - RIGHTANGLE));
        }
        imaqOverlayLine(img, get_point(upmidpoints[j]), get_point(downmidpoints[i]), &ColorRed, NULL);
        break;
      }
    }
    if (i != (int)downmidpoints.size()-1)
      break;
  }
  if (i != (int)downmidpoints.size() - 1 && fabsf(point2point(pt, downmidpoints[i + 1])) > distance)
    j++;

  while (j < (int)upmidpoints.size() && i < (int)downmidpoints.size() - 1)
  {
    if (i > 0)
    {
      int  angleresult = imaqGetAngle(sptf, eptf, upmidpoints[j], downmidpoints[i], &angle);
      minslope = min(minslope, fabsf(angle - RIGHTANGLE));
      maxslope = max(maxslope, fabsf(angle - RIGHTANGLE));
      angleresult = imaqGetAngle(sptf, eptf, upmidpoints[j], downmidpoints[i+1], &angle);
      minslope = min(minslope, fabsf(angle - RIGHTANGLE));
      maxslope = max(maxslope, fabsf(angle - RIGHTANGLE));
    }
    imaqOverlayLine(img, get_point(upmidpoints[j]), get_point(downmidpoints[i]), &ColorRed, NULL);
    imaqOverlayLine(img, get_point(upmidpoints[j]), get_point(downmidpoints[i+1]), &ColorRed, NULL);
    i++;
    j++;
  }
}

void SortPointsOfX(Points& ptf)
{
  if (ptf.empty())
    return;
  for (UINT i = 0; i < ptf.size(); i++)
  {
    for (UINT j = i + 1; j < ptf.size(); j++)
    {
      if (ptf[i].x > ptf[j].x)
      {
        PointFloat pt = ptf[i];
        ptf[i] = ptf[j];
        ptf[j] = pt;
      }
    }
  }
}

BOOL get_inflexion(const Points& source, int kernel, PointFloat& dest)
{
  if(source.empty())
    return FALSE;
  for (int i = kernel; i < (int)source.size() - kernel; i++)
  {
    float angle = Angle(source[i], source[i - kernel], source[i + kernel]);
    if ( angle - 160 < 0)
    {
      dest.x = source[i].x;
      dest.y = source[i].y;
      return TRUE;
    }
  }
  return FALSE;
}

struct interested_options
{
  int num_curves;
  RotatedRect rect_rotated;
  PointFloat start_axis;
  PointFloat end_axis;
};

struct interested_report
{
  PointFloat left_most;
  PointFloat left_axis;
  PointFloat max_up;
  PointFloat min_up;
  PointFloat max_down;
  PointFloat min_down;
  float major_diameter;
  float minor_diameter;
};

void get_points_interested(const Curve* curves,
                           const interested_options* options,
                           interested_report& report,
                           Points& line_front,
                           Points& line_up,
                           Points& line_down)
{
  ENTER_FUNCTION;

  report.left_axis = imaqMakePointFloat(0, 0);
  report.left_most = imaqMakePointFloat(0, 0);
  report.max_down = imaqMakePointFloat(0, 0);
  report.max_up = imaqMakePointFloat(0, 0);
  report.min_down = imaqMakePointFloat(0, 0);
  report.min_up = imaqMakePointFloat(0, 0);
  report.major_diameter = 0;
  report.minor_diameter = 0;

  if (!options || !curves || options->num_curves < 1)
    return;
  points_rect points_rect = rotate_rect(options->rect_rotated);

  float max_up = 0;
  float max_down = 0;
  float min_up = 9999.9f;
  float min_down = 9999.9f;
  float min_d_left = 9999.9f;
  float min_d_axis = 9999.9f;
  for (int i = 0; i < options->num_curves; i++)
  {
    for (UINT j = 0; j < curves[i].numPoints; j++)
    {
      PointFloat pt = curves[i].points[j];
      float d_left = point2line(pt, points_rect.left_top, points_rect.left_bottom, false);
      if(d_left < 0)
      {
        line_front.push_back(pt);
        float d_axis = point2line(pt, options->start_axis, options->end_axis, true); 
        if (d_axis < min_d_axis)
        {
          min_d_axis = d_axis;
          report.left_axis = pt;
        }
        if (d_left < min_d_left)
        {
          min_d_left = d_left;
          report.left_most = pt;
        }
      }
      else
      {
        float d_right = point2line(pt, points_rect.right_top, points_rect.right_bottom, false);
        if(d_right < 0)
        {
          float d_axis = point2line(pt, options->start_axis, options->end_axis, false);
          if(d_axis > 0)
          {
            line_up.push_back(pt);
            if (max_up < d_axis)
            {
              max_up = d_axis;
              report.max_up = pt;
            }
            if (min_up > d_axis)
            {
              min_up = d_axis;
              report.min_up = pt;
            }           
          }
          else
          {
            d_axis = -d_axis;
            line_down.push_back(pt);
            if (max_down < d_axis )
            {
              max_down = d_axis;
              report.max_down = pt;
            }
            if (min_down > d_axis)
            {
              min_down = d_axis;
              report.min_down = pt;
            }
          }
        }
      }
    }
  }
  report.major_diameter = max_up + max_down;
  report.minor_diameter = min_up + min_down ;
}

float MinDistance(const Points& ptfs, PointFloat s, PointFloat e)
{
  if (ptfs.empty())
    return 0.0;
  float d_min = 9999;
  for (int i = 0; i < (int)ptfs.size(); i ++)
  {
    float d = point2line(ptfs[i], s, e, true);
    if (d_min > d)
      d_min = d;
  }
  return d_min;
}

void find_top_points(const Points& points, 
                     const PointFloat& start_axis,
                     const PointFloat& end_axis, 
                     int kernel,
                     Points& toppoints)
{
  ENTER_FUNCTION;

  if (points.empty())
    return;

  toppoints.clear();
  for (int i = kernel; i < (int)points.size()- kernel - 1; i++)
  {
    float d_bp = point2line(points[i], start_axis, end_axis, true);
    bool find_top = true;
    for (int j=i-kernel;j<i+kernel;j++)
    {
      float d = point2line(points[j], start_axis, end_axis, true);
      if (d > d_bp)
        find_top = false;
    }
    if (find_top)
    {
      float angle = Angle(points[i], points[i- kernel], points[i + kernel]);
      if (angle < 120 && angle > 30)
        toppoints.push_back(points[i]);
    }
  }
}

void filter_same_points(Points& points)
{
  if(points.empty())
    return;
  const float EPSILON_X = 0.00001f;
  for (int i = (int)points.size() - 1; i > 0; i--)
  {
    if(fabsf(points[i - 1].x - points[i].x) < EPSILON_X)
      points.erase(points.begin() + i);
  }
}

BOOL get_most_left(const Curve* curves, int num_curves, RotatedRect rect, 
                   PointFloat& most_right)
{
  if(!curves || num_curves == 0)
    return FALSE;
  points_rect points_rect = rotate_rect(rect);
  float min_dis = 9999.9f;
  for (int i = 0; i < num_curves; i++)
  {
    for (UINT j = 0; j < curves[i].numPoints; j++)
    {
      PointFloat pt = curves[i].points[j];
      float d = point2line(pt, points_rect.left_top, points_rect.left_bottom, TRUE);
      if (min_dis > d)
      {
        min_dis = d;
        most_right = pt;
      }
    }
  }
  return TRUE;
}

float apply_bottom_diameter(const Points& line_up, const Points& line_down,
                            float x_left, float x_right, 
                            PointFloat& bottom_up, PointFloat& bottom_down)
{
  if (line_down.empty() || line_up.empty())
    return 0;
  float distance = 0;
  float max_up = 0;
  int idx_up = 0;
  for (int i = 0; i < (int)line_up.size(); i++)
  {
    if (line_up[i].x < x_right && line_up[i].x > x_left && line_up[i].y > max_up)
    {
      idx_up = i;
      max_up = line_up[i].y;
    }
  }
  float min_down = 999.9f;
  int idx_down = 0;
  for (int i = 0; i < (int)line_down.size(); i++)
  {
    if (line_down[i].x < x_right && line_down[i].x > x_left && line_down[i].y < min_down)
    {
      idx_down = i;
      min_down = line_down[i].y;
    }
  }
  bottom_up = line_up[idx_up];
  bottom_down = line_down[idx_down];
  distance = bottom_down.y - bottom_up.y;
  return distance;
}

BOOL init_teeth_report(TeethReport* teeth_report)
{
  if (!teeth_report)
    return FALSE;
  teeth_report->helixAngle = 0.0;
  teeth_report->innerDiameter = 0.0;
  teeth_report->maxSlope = 0.0;
  teeth_report->maxTeeth = 0.0;
  teeth_report->midDiameter = 0.0;
  teeth_report->minSlope = 0.0;
  teeth_report->minTeeth = 0.0;
  teeth_report->outerDiameter = 0.0;
  teeth_report->pitch = 0.0;
  teeth_report->ratio = 9999.9f;
  teeth_report->teeth1 = 0;
  teeth_report->teeth2 = 0;
  teeth_report->teethLenth = 0.0;
  teeth_report->cylinder = 0.0;
  teeth_report->pilot = 0.0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FindTeeth(const PublicOptions* po,  const TeethOptions* to, TeethReport* tr)
{
  ENTER_FUNCTION;

  if (!po || !to || !init_teeth_report(tr))
    return FALSE;
  ClearMyError();

  ImagePtr display = GetImage(po->idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);

  int result = 0;
  int kernel = to->kernel;

  RotatedRect rr = imaqMakeRotatedRectFromRect(to->rc);//大的选择区域
  RotatedRect rr_teeth = imaqMakeRotatedRectFromRect(to->rc_teeth);//小的选择区域
  point center(rr.left+(float)rr.width/2, rr.top+(float)rr.height/2);//大矩形中心
  point lefttop(rr.left, rr.top);//左上角点
  point leftbottom(rr.left, rr.top+rr.height);//坐下角点
  point righttop(rr.left+rr.width, rr.top);//右上角点
  point rightbottom(rr.left+rr.width, rr.top+rr.height);//右下角点

  sub_image sub(img, &(to->rc));
  if (IsEmpty(sub))
    return FALSE;
  PixelValue WHITE = {255.0f};
  RETURN_FALSE(imaqFillImage(img, WHITE, NULL));
  float angle = 0.0;
  PointFloat mid_center = imaqMakePointFloat(0, 0);

  RETURN_FALSE(imaqThreshold(sub, sub, 0, 0, TRUE, 255));
  RETURN_FALSE(locating(sub, &angle, &mid_center)); //得出角度和质心。

  if (!to->angle_sens)
    angle = 0.01f;
  else
  {
    if( angle > 90 )
      angle = angle - 180;
    if (angle > 45 || angle < -45)
      angle = 0.01f;
    else
    {
      ImagePtr sub_back = imaqCreateImage(IMAQ_IMAGE_U8, 0);
      RegisterDispose dis_sub_back(sub_back);
      if (sub_back)
        result = imaqDuplicate(sub_back, sub);
      if (result && !IsEmpty(sub_back))
      {
        int num_edges = 0;
        point first_divide(rr.width - 1, 0);
        point last_divide(rr.width - 1, rr.height);
        EdgeReport* edges = edge_tool(sub_back, first_divide, last_divide, 
          &num_edges);//查找右边界点。
        RegisterDispose dis_edges(edges);
        if (edges && num_edges > 1)
        {
          if (angle < 90)
          {
            last_divide = edges[num_edges - 1].coordinate; 
            first_divide = rotate_point(first_divide.pf(), last_divide.pf(), angle);
          }
          else
          {
            first_divide = edges[0].coordinate;
            last_divide = rotate_point(last_divide.pf(), first_divide.pf(), angle);
          }
          result = imaqDrawLineOnImage(sub_back, sub_back, IMAQ_DRAW_VALUE, 
            first_divide.pt(), last_divide.pt(), 0.0f);//什么意思
          if (result && keep_particle_largest(sub_back))
          {
            int num_particles = 0;
            result = imaqCountParticles(sub_back,TRUE, &num_particles);
            if (result && num_particles == 1)
            {
              double angle_particle = 0;
              RETURN_FALSE(imaqMeasureParticle(sub_back, 0, FALSE, IMAQ_MT_ORIENTATION, 
                &angle_particle));
              angle = (float)angle_particle;
              if( angle > 90 )
                angle = angle - 180;
              RETURN_FALSE(imaqCentroid(sub_back, &mid_center, NULL));
            }
          }
        }
      }
    }
  }

  RETURN_FALSE(imaqThreshold(sub, sub, 0, 0, TRUE, 255));
  sub.Commit();
  mid_center.x += to->rc.left; //转移坐标到整个图像上
  mid_center.y += to->rc.top;

  //轴线
  PointFloat s, e;
  e.x = (float)(to->rc.width + to->rc.left);
  e.y = (e.x - mid_center.x) * (float)tan(-angle * PI / 180) + mid_center.y;
  s.x = (float)to->rc.left;
  s.y = (s.x - mid_center.x) * (float)tan(-angle * PI / 180) + (float)mid_center.y;
  DO_IF_DEBUG(imaqOverlayLine(img, get_point(s), get_point(e), &ColorYellow, NULL));

  rr.angle = rr_teeth.angle = angle;//校正选择角度 
  rr_teeth.top = (int)mid_center.y - rr_teeth.height / 2;//对rr_teeth定位

  UINT numCurves = 0;
  CurveOptions co;
  co.extractionMode = IMAQ_NORMAL_IMAGE;
  co.threshold = 40;
  co.filterSize = IMAQ_FINE;
  co.minLength = 20;
  co.rowStepSize = 5;
  co.columnStepSize = 5;
  co.maxEndPointGap = 50;
  co.onlyClosed = FALSE;
  co.subpixelAccuracy = TRUE;
  Curve* curves = imaqExtractCurves(img, NULL, &co, &numCurves);//求所有点吗？
  RegisterDispose dis_curves(curves);
  if( !numCurves || !curves )
    return FALSE;

//   跟踪牙部最左边点
//   if (to->offset_x > FLT_EPSILON)
//   {
//     RETURN_FALSE(get_most_left(curves, numCurves, rr, tr->left_most));
//     rr_teeth.left = (int)(tr->left_most.x + to->offset_x);
//   }

  interested_options interested_options;
  interested_options.num_curves = numCurves;
  interested_options.start_axis = s;
  interested_options.end_axis = e;
  interested_options.rect_rotated = rr_teeth;
  Points line_front;
  Points line_up;
  Points line_down;
  interested_report interested_report;
  get_points_interested(curves, &interested_options, interested_report,
    line_front, line_up, line_down); 
  tr->left_most.x = interested_report.left_most.x;
  tr->left_most.y = interested_report.left_most.y;

  tr->maxTeeth = tr->outerDiameter = interested_report.major_diameter;
  if (to->maxOutDiameter)
    RETURN_FALSE(overlay_result_max(img, rr_teeth, interested_report.max_up,
    interested_report.max_down, IMAQ_TOP_TO_BOTTOM));
  tr->innerDiameter = interested_report.minor_diameter;
  //   if (to->bottomDiameter)
  //     RETURN_FALSE(overlay_result_min(img, rr_teeth, interested_report.min_up,
  //     interested_report.min_down, IMAQ_TOP_TO_BOTTOM));

  if (line_front.size() == 0 )
  {
    img.copy_overlay();
    img.detach();
    return TRUE;  
  }

  //长度
  PointFloat rightmost;
  rightmost.x = (float)(to->rc.width + to->rc.left);
  rightmost.y = (rightmost.x - interested_report.left_most.x) * 
    (float)tan(-angle * PI / 180) + interested_report.left_most.y;

  if (to->length)
  {
    tr->teethLenth = point2point(interested_report.left_most, rightmost);
    RETURN_TRUE(imaqOverlayLine(img, get_point(interested_report.left_most), 
      get_point(rightmost), &ColorLime, NULL));
  }

  //尖锐度 最小值为3
  if (to->ratio)
  {
    point center_peak(interested_report.left_axis);
    float ww = 30;
    float hh = (float)tr->outerDiameter;
    RotatedRect rr_peak = imaqMakeRotatedRect((int)(interested_report.left_axis.y-hh/2), 
      (int)(interested_report.left_axis.x),(int)hh, (int)ww, rr.angle);
    ROIPtr roi_peak = imaqCreateROI();
    RegisterDispose dis_roi_peak(roi_peak);
    RETURN_TRUE(imaqAddRotatedRectContour2(roi_peak, rr_peak));
    if (!roi_peak)
    {
      img.copy_overlay();
      img.detach();
      return TRUE;  
    }
    ImagePtr mask_peak = imaqCreateImage(IMAQ_IMAGE_U8, 0);
    RegisterDispose dis_mask(mask_peak);
    RETURN_TRUE(imaqROIToMask(mask_peak, roi_peak, 255, NULL, NULL));
    HistogramReport* hr = imaqHistogram(img, 2, 0, 255, mask_peak);
    RegisterDispose dis_hr(hr);
    if( !hr || hr->histogramCount != 2)
    {
      img.copy_overlay();
      img.detach();
      return TRUE;  
    }
    point right_peak(center_peak.x+ww/2, center_peak.y);
    right_peak.rotate(center_peak, (float)-rr.angle);
    float R = sqrtf(hr->histogram[0]/PIf);
    DO_IF_DEBUG(draw_circle(img, right_peak.ptf(), R, &ColorMagenta));
    tr->ratio = (float)hr->histogram[0]/10;
  }

  if( line_up.size() == 0 || line_down.size() == 0)
  {
    img.copy_overlay();
    img.detach();
    return TRUE;  
  }

  SortPointsOfX(line_up);
  SortPointsOfX(line_down);

  if(to->cylinder)
  {
    point inflexion_up;
    point inflexion_down;
    if(get_inflexion(line_up, kernel, inflexion_up.ptf()) && 
      get_inflexion(line_down, kernel, inflexion_down.ptf()))
    {
      point left_most_up;
      point left_most_down;
      RETURN_FALSE(imaqGetPerpendicularLine(lefttop.ptf(), righttop.ptf(), 
        interested_report.left_most, &interested_report.left_most, 
        left_most_up.pf(), NULL));
      RETURN_FALSE(imaqGetPerpendicularLine(leftbottom.ptf(), rightbottom.ptf(), 
        interested_report.left_most, &interested_report.left_most, 
        left_most_down.pf(), NULL));
      float cylinder_up = point2line(inflexion_up.ptf(), left_most_up.ptf(), left_most_down.ptf(), true);
      float cylinder_down = point2line(inflexion_down.ptf(), left_most_up.ptf(), left_most_down.ptf(), true);
      tr->cylinder = (cylinder_up + cylinder_down) / 2;

      point inflexion_up_end;
      point inflexion_down_end;
      RETURN_FALSE(imaqGetPerpendicularLine(left_most_up.ptf(), left_most_down.ptf(), 
        inflexion_up.ptf(), inflexion_up.pf(), inflexion_up_end.pf(), NULL));
      RETURN_FALSE(imaqOverlayLineWithArrow(img, inflexion_up.pt(), 
        inflexion_up_end.pt(), &ColorMagenta, FALSE, TRUE, NULL));

      RETURN_FALSE(imaqGetPerpendicularLine(left_most_up.ptf(), left_most_down.ptf(), 
        inflexion_down.ptf(), inflexion_down.pf(), inflexion_down_end.pf(), NULL));
      RETURN_FALSE(imaqOverlayLineWithArrow(img, inflexion_down.pt(), 
        inflexion_down_end.pt(), &ColorMagenta, FALSE, TRUE, NULL));
      RETURN_FALSE(imaqOverlayLine(img, left_most_up.pt(), left_most_down.pt(), 
        &ColorMagenta, NULL));
      DrawSmallRect(img, inflexion_up.ptf(), 3, &ColorRed);
      DrawSmallRect(img, inflexion_down.ptf(), 3, &ColorRed);
    }
  }

  Points uptoppoints; 
  Points downtoppoints;
  Points upmidpoints;
  Points downmidpoints;
  //   int kernel = (int)((tr->outerDiameter - tr->innerDiameter) / 2);
  find_top_points(line_up, s, e, kernel, uptoppoints);

//   if (!to->angle_sens)
//   {
//     angle = -0.01f;
//     e.x = (float)(to->rc.width + to->rc.left);
//     e.y = (e.x - mid_center.x) * (float)tan(-angle * PI / 180) + mid_center.y;
//     s.x = (float)to->rc.left;
//     s.y = (s.x - mid_center.x) * (float)tan(-angle * PI / 180) + (float)mid_center.y;
//   }

  find_top_points(line_down, s, e, kernel, downtoppoints);
  filter_same_points(uptoppoints);
  filter_same_points(downtoppoints);

  tr->teeth1 = uptoppoints.size();
  tr->teeth2 = downtoppoints.size();

  if (uptoppoints.size() > 0 && downtoppoints.size() > 0)
  {
    tr->minTeeth = MinDistance(uptoppoints, s, e) + MinDistance(downtoppoints, s, e);
    //      point relief_up_end_up;
    //      point relief_up_end_down;
    //      RETURN_FALSE(imaqGetPerpendicularLine(lefttop, righttop, 
    //        interested_report.left_most, &interested_report.left_most, 
    //        &relief_up_end_up, NULL));
    //      RETURN_FALSE(imaqGetPerpendicularLine(leftbottom, rightbottom, 
    //        interested_report.left_most, &interested_report.left_most, 
    //        &relief_up_end_down, NULL));
    //      float relief_up_up = point2line(uptoppoints[0], relief_up_end_up, relief_up_end_down, true);
    //      float relief_up_down = point2line(downtoppoints[0], relief_up_end_up, relief_up_end_down, true);
    //      point relief_up_start = relief_up_up < relief_up_down ? uptoppoints[0] : downtoppoints[0];
    //      tr->cylinder = relief_up_up < relief_up_down ? relief_up_up : relief_up_down;
    //      point relief_up_end; 
    //      RETURN_FALSE(imaqGetPerpendicularLine(relief_up_end_up, relief_up_end_down, 
    //        relief_up_start, &relief_up_start, &relief_up_end, NULL));
    //      RETURN_FALSE(imaqOverlayLineWithArrow(img, relief_up_start.pt(), 
    //        relief_up_end.pt(), &ColorMagenta, FALSE, TRUE, NULL));

    //     float relief_down_up = point2line(uptoppoints[uptoppoints.size() - 1], 
    //       righttop, rightbottom, true);
    //     float relief_down_down = point2line(downtoppoints[downtoppoints.size() - 1], 
    //       righttop, rightbottom, true);
    //     tr->pilot = relief_down_up < relief_down_down ? relief_down_up : relief_down_down;
    //     point relief_down_start = relief_down_up < relief_down_down ? 
    //       uptoppoints[uptoppoints.size() - 1] : downtoppoints[downtoppoints.size() - 1];
    //     point relief_down_end;
    //     relief_down_end.x = (float)(to->rc.left + to->rc.width);
    //     relief_down_end.y = relief_down_start.y - 
    //       (relief_down_end.x - relief_down_start.x) * tanf(angle * PIf / 180);
    //     RETURN_FALSE(imaqOverlayLineWithArrow(img, relief_down_start.pt(), 
    //       relief_down_end.pt(), &ColorMagenta, FALSE, TRUE, NULL));
  }

  if (uptoppoints.size() > 1 && downtoppoints.size() > 1)
  {
    if (to->bottomDiameter && !to->angle_sens)
    {
      float x_left = max(uptoppoints[0].x, downtoppoints[0].x);
      float x_right = min(uptoppoints[uptoppoints.size() - 1].x, 
        downtoppoints[downtoppoints.size() - 1].x);
      tr->innerDiameter = apply_bottom_diameter(line_up, line_down, x_left,
        x_right, interested_report.min_up, interested_report.min_down);
      rr_teeth.left = (int)x_left;
      rr_teeth.width = (int)(x_right - x_left);
      RETURN_FALSE(overlay_result_min(img, rr_teeth, interested_report.min_up,
        interested_report.min_down, IMAQ_TOP_TO_BOTTOM));
    }

    float pitch_up = (uptoppoints[uptoppoints.size() - 1].x - uptoppoints[0].x) / (uptoppoints.size() - 1);
    float pitch_down = (downtoppoints[downtoppoints.size() - 1].x - downtoppoints[0].x) / (downtoppoints.size() - 1);
    tr->pitch = (pitch_up + pitch_down) / 2 * (float)cos(angle * PI / 180);
    tr->midDiameter = tr->maxTeeth - COEFFICIENT * tr->pitch;

    MakeMidLine(img, upmidpoints, uptoppoints, s, e, (tr->maxTeeth - tr->innerDiameter)/3, true);
    MakeMidLine(img, downmidpoints, downtoppoints, s, e, (tr->maxTeeth - tr->innerDiameter)/3, false);
    if (to->slopeteeth)
      FitMidPoints(img, upmidpoints, downmidpoints, s, e, 
      tr->pitch, tr->minSlope, tr->maxSlope);
    HelixAngle(tr);
  }
  img.copy_overlay();
  img.detach();
  return TRUE;
}
