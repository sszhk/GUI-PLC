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
* Filename:    tools_ni.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     
*
*/
#include "stdafx.h"
#include "tools_ni.h"
#include "NIDispose.h"
#include "NICalc.h"
#include "tools_geometry.h"
#include "Colors.h"

RotatedRect make_rotated_rect(int width, int height, 
                              PointFloat center, float angle)
{
  return imaqMakeRotatedRect((int)(center.y - height / 2), 
    (int)(center.x - width / 2), height, width, angle);
}

Rect make_rect(int width, int height, PointFloat center)
{
  return imaqMakeRect((int)(center.y - height / 2), 
    (int)(center.x - width / 2), height, width);
}

PointFloat point_to_pointfloat(const Point* source)
{
  return imaqMakePointFloat((float)source->x, (float)source->y);
}

BOOL remove_small(ImagePtr image, int erosions, int connectivity8)
{
  if (!image)
    return FALSE;
  int kernel[9] = {1,1,1, 1,1,1, 1,1,1};
  StructuringElement structuring_element;
  structuring_element.kernel = kernel;
  structuring_element.hexa = FALSE;
  structuring_element.matrixCols = 3;
  structuring_element.matrixRows = 3;
  RETURN_FALSE(imaqSetBorderSize(image, 1));
  RETURN_FALSE(imaqSizeFilter(image, image, connectivity8, erosions, 
    IMAQ_KEEP_LARGE, &structuring_element));
  RETURN_FALSE(imaqSetBorderSize(image, 0));
  return TRUE;
}

BOOL morphology(ImagePtr image, MorphologyMethod_enum morphology_method)
{
  if (!image)
    return FALSE;
  StructuringElement structuring_element;
  int kernel[9] = {1,1,1, 1,1,1, 1,1,1};
  structuring_element.kernel = kernel;
  structuring_element.hexa = FALSE;
  structuring_element.matrixCols = 3;
  structuring_element.matrixRows = 3;
  RETURN_FALSE(imaqSetBorderSize(image, 3));
  RETURN_FALSE(imaqMorphology(image, image, morphology_method, 
    &structuring_element));
  RETURN_FALSE(imaqSetBorderSize(image, 0));
  return TRUE;
}

BOOL keep_particle_between(ImagePtr image, MeasurementType measurement_type, 
                     float lower, float upper, int* num_particals)
{
  if (IsEmpty(image))
    return FALSE;
  ParticleFilterCriteria2 partical_filter_criteria;
  partical_filter_criteria.calibrated = FALSE;
  partical_filter_criteria.exclude = FALSE;
  partical_filter_criteria.parameter = measurement_type;
  partical_filter_criteria.lower = lower;
  partical_filter_criteria.upper = upper;
  ParticleFilterOptions particle_filter_options;
  particle_filter_options.connectivity8 = FALSE;
  particle_filter_options.rejectBorder = FALSE;
  particle_filter_options.rejectMatches = FALSE;
  return imaqParticleFilter3(image, image, &partical_filter_criteria, 
    1, &particle_filter_options, NULL, num_particals);
}

StraightEdgeReport* find_edge(ImagePtr image, 
                              const find_edge_options* options )
{
  if (IsEmpty(image))
    return NULL;
  FindEdgeOptions edge_options;
  edge_options.threshold = 1;
  edge_options.width = options->width;
  edge_options.steepness = options->steepness;
  edge_options.subsamplingRatio = options->gap;
  edge_options.showSearchArea = 0;
  edge_options.showSearchLines = 0;
  edge_options.showEdgesFound = 0;
  edge_options.showResult = options->show_results;
  return imaqFindEdge(image, options->search_rect, options->direction,
    &edge_options, NULL);
}

PointFloat* simple_edge(ImagePtr image, PointFloat first, PointFloat last)
{
  if (IsEmpty(image))
    return NULL;
  SimpleEdgeOptions simple_edge_options;
  simple_edge_options.type = IMAQ_RELATIVE;
  simple_edge_options.threshold = 40;
  simple_edge_options.hysteresis = 2;
  simple_edge_options.process = IMAQ_ALL;
  simple_edge_options.subpixel = TRUE;

  ROI *roi = imaqCreateROI();
  RegisterDispose dispose_roi(roi);
  int result = imaqAddLineContour(roi, imaqMakePointFromPointFloat(first),
    imaqMakePointFromPointFloat(last));
  ROIProfile* roi_profile = imaqROIProfile(image, roi);
  RegisterDispose dispose_roi_profile(roi_profile);
  if( !roi_profile )
    return NULL;
  return imaqSimpleEdge(image, roi_profile->pixels, 
    roi_profile->report.dataCount, &simple_edge_options, NULL);
}

EdgeReport* edge_tool(ImagePtr image, 
                      point first, 
                      point last, 
                      int* num_edges)
{
  if(IsEmpty(image))
    return NULL;
  EdgeOptions edge_options;
  edge_options.steepness = 1;
  edge_options.width = 1;
  edge_options.threshold = 1;
  edge_options.subpixelDivisions = 9;
  edge_options.subpixelType = IMAQ_QUADRATIC;

  ROIPtr line = imaqCreateROI();
  RegisterDispose dis_line(line);
  int result = imaqAddLineContour(line, first.pt(), last.pt());
  ROIProfile* line_profile = imaqROIProfile(image, line);
  RegisterDispose dis_line_profile(line_profile);
  if (!line_profile)
    return NULL;
  return imaqEdgeTool(image, line_profile->pixels, 
    line_profile->report.dataCount, &edge_options, num_edges);
}

BOOL fill_holes_side(ImagePtr image)
{
  if(!image)
    return FALSE;
  int result = 0;
  int width = 0;
  int height = 0;
  RETURN_FALSE(imaqGetImageSize(image, &width, &height)); 
  Point first_line = imaqMakePoint(width - 1, 0);
  Point last_line = imaqMakePoint(width - 1, height);
  int num_edges = 0;
  EdgeReport* edge_report = edge_tool(image, first_line, last_line, 
    &num_edges);
  RegisterDispose dis_edge_report(edge_report);
  if (num_edges > 1)
  {
    RETURN_FALSE(imaqDrawLineOnImage(image, image, IMAQ_DRAW_VALUE, 
      imaqMakePointFromPointFloat(edge_report[0].coordinate),
      imaqMakePointFromPointFloat(edge_report[num_edges - 1].coordinate),
      1.0f));
    RETURN_FALSE(imaqFillHoles(image, image, TRUE));
  }
  return TRUE;
}

BOOL keep_particle_largest(ImagePtr image)
{
  if(IsEmpty(image))
    return FALSE;
  int result = 0;
  int num_particles = 0;
  RETURN_FALSE(imaqCountParticles(image,FALSE, &num_particles));
  if(num_particles == 0 )
    return FALSE;

  if( num_particles != 1 )
  {
    double max_area = 0;
    for (int i = 0; i < num_particles; i++)
    {
      double area = 0;
      RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, IMAQ_MT_AREA, &area));
      if( max_area < area )
        max_area = area;
    }
    float lower = (float)max_area - 0.1f;
    float upper = (float)max_area + 0.1f;
    int left = 0;
    RETURN_FALSE(keep_particle_between(image, IMAQ_MT_AREA, lower, upper, &left));
    if( left != 1 )
      return FALSE;
  }
  return TRUE;
}

BOOL locating(ImagePtr image, float* angle_particle, PointFloat* center)
{
  if(IsEmpty(image))
    return FALSE;
  RETURN_FALSE(remove_small(image, 10, TRUE));
  RETURN_FALSE(imaqFillHoles(image, image, TRUE));
  RETURN_FALSE (keep_particle_largest(image));
  RETURN_FALSE(fill_holes_side(image));
  int num_particles = 0;
  RETURN_FALSE(imaqCountParticles(image,TRUE, &num_particles));
  if (num_particles != 1)
    return FALSE;
  double angle = 0;
  RETURN_FALSE(imaqMeasureParticle(image, 0, FALSE, IMAQ_MT_ORIENTATION, 
    &angle));
  RETURN_FALSE(imaqCentroid(image, center, NULL));
  *angle_particle = (float)angle;

  return TRUE;
}

points_rect rotate_rect(const RotatedRect& rect)
{
  points_rect points;
  point center(rect.left + rect.width / 2, 
               rect.top + rect.height / 2);
  point left_top(rect.left, rect.top);
  point left_bottom(rect.left, rect.top + rect.height);
  point right_top(rect.left + rect.width, rect.top);
  point right_bottom(rect.left + rect.width, rect.top + rect.height);

  float angle = (float)rect.angle;
  points.left_top = rotate_point(left_top.pf(), center.pf(), angle);
  points.left_bottom = rotate_point(left_bottom.pf(), center.pf(), angle);
  points.right_top = rotate_point(right_top.pf(), center.pf(), angle);
  points.right_bottom = rotate_point(right_bottom.pf(), center.pf(), angle);
  return points;
}

BOOL fill_circle(ImagePtr image, 
                 const PointFloat& center, 
                 const float& radius, 
                 BOOL is_white)
{
  if(IsEmpty(image))
    return FALSE;

  int top = (int)(center.y - radius);
  int left = (int)(center.x - radius);
  float value_pixel = is_white ? (float)255 : (float)0;
  Rect rect = imaqMakeRect(top, left, (int)(2 * radius), (int)(2 * radius));
  RETURN_FALSE(imaqDrawShapeOnImage(image, image, rect, IMAQ_PAINT_VALUE, 
    IMAQ_SHAPE_OVAL, value_pixel));
  return TRUE;
}

BOOL find_shape(ImagePtr image, Annulus annulus, int& count, BOOL opend)
{
  float area_low = PIf * annulus.innerRadius * annulus.innerRadius;
  float area_up = PIf * annulus.outerRadius * annulus.outerRadius;
  float bounding_low = 2 * (float)annulus.innerRadius;
  float bounding_up = 2 * (float)annulus.outerRadius;

  int result = false;
  if (!opend)
  {
    result = keep_particle_between(image, IMAQ_MT_PARTICLE_AND_HOLES_AREA, area_low, 
      area_up, &count);
    if(!result || count == 0)
      return FALSE;
  }
  result = keep_particle_between(image, IMAQ_MT_BOUNDING_RECT_HEIGHT, 
    bounding_low, bounding_up, &count);
  if(!result || count == 0)
    return FALSE;
  result = keep_particle_between(image, IMAQ_MT_BOUNDING_RECT_WIDTH, 
    bounding_low, bounding_up, &count);
  if(!result || count == 0)
    return FALSE;
  return true;
}

CircularEdgeReport* find_diameter(ImagePtr image, 
                                 const edge_circle_options* options, 
                                 SpokeReport** spoke_report)
{
  FindEdgeOptions options_find;
  options_find.threshold = 1;
  options_find.showEdgesFound = FALSE;
  options_find.showResult = FALSE;
  options_find.showSearchArea = FALSE;
  options_find.showSearchLines = FALSE;
  options_find.steepness = 2;
  options_find.subsamplingRatio = options->gap;
  options_find.width = options->smoothing;

  return imaqFindCircularEdge(image, options->annulus, 
    IMAQ_OUTSIDE_TO_INSIDE, &options_find, NULL, spoke_report);
}


BOOL make_visible(ImagePtr img)
{
  return (imaqThreshold(img, img, 1, 1, TRUE, 255));
}

BOOL make_calculatible(ImagePtr img)
{
  return (imaqThreshold(img, img, 255, 255, TRUE, 1));
}

BOOL make_inverse(ImagePtr img)
{
  return imaqThreshold(img, img, 0, 0, TRUE, 255);
}

BOOL mask_down(ImagePtr image, Rect rect)
{
  if( IsEmpty(image) )
    return FALSE;

  int result = 0;
  ImagePtr mask = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_mask(mask);
  int width = 0;
  int height = 0;
  RETURN_FALSE(imaqGetImageSize(image, &width, &height));
  RETURN_FALSE(imaqSetImageSize(mask, width, height));
  PixelValue pv = {0};
  RETURN_FALSE(imaqFillImage(mask, pv, NULL));
  result = imaqDrawShapeOnImage(mask, mask, rect, IMAQ_PAINT_VALUE, 
    IMAQ_SHAPE_RECT, 255.0f);
  pv.grayscale = 0;
  RETURN_FALSE(imaqAnd(image, image, mask));

  return TRUE;
}

RakeReport* rake(ImagePtr image, RakeDirection direction, EdgeProcess process, Rect rect)
{
  RakeOptions rake_option;
  rake_option.steepness = 2;
  rake_option.subpixelDivisions = 1;
  rake_option.subpixelType = IMAQ_QUADRATIC;
  rake_option.subsamplingRatio = 5;
  rake_option.threshold = 40;
  rake_option.width = 4;
  ROIPtr roi_rake = imaqCreateROI();
  RegisterDispose dis_roi_rake(roi_rake);
  if( !roi_rake )
    return NULL;
  if(!imaqAddRectContour(roi_rake, rect))
    return NULL;
  return imaqRake(image, roi_rake, direction, process, &rake_option);
}