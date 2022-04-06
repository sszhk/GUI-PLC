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
 * Created:     2010-05-06  10:00
 * Filename:    overlay.cc
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     
 *
 */

#include "stdafx.h"
#include "overlay.h"
#include "Colors.h"
#include "NICalc.h"

BOOL draw_point(ImagePtr image, PointFloat point, RGBValue color)
{
  if(IsEmpty(image))
    return FALSE;

  Rect rect = imaqMakeRect(M_OFFSET(point.y), M_OFFSET(point.x), 
    OVERLAY_RESULT_SIZE, OVERLAY_RESULT_SIZE);

  return imaqOverlayOval(image, rect, &color, IMAQ_PAINT_VALUE, NULL);
}

BOOL overlay_result(ImagePtr image, const options_overlay_result& options)
{
  if(IsEmpty(image))
    return FALSE;
  points_rect points_rect = options.points_rect;
  PointFloat point = options.point;

  PointFloat start_arrow = imaqMakePointFloat(0, 0);
  PointFloat end_arrow = imaqMakePointFloat(0, 0);
  PointFloat start_perpendicular = imaqMakePointFloat(0, 0);
  PointFloat end_perpendicular = imaqMakePointFloat(0, 0);

  if (options.direction == IMAQ_LEFT_TO_RIGHT)
  {
    if (options.first)
      start_arrow = point_mid(&points_rect.left_top, &points_rect.left_bottom);
    else
      start_arrow = point_mid(&points_rect.right_top, &points_rect.right_bottom);
    RETURN_FALSE(imaqGetPerpendicularLine(points_rect.left_top, 
      points_rect.right_top, point, &point, &start_perpendicular, NULL));  
    RETURN_FALSE(imaqGetPerpendicularLine(points_rect.left_bottom, 
      points_rect.right_bottom, point, &point, &end_perpendicular, NULL));    
  }
  else
  {
    if (options.first)
      start_arrow = point_mid(&points_rect.left_top, &points_rect.right_top);
    else
      start_arrow = point_mid(&points_rect.left_bottom, &points_rect.right_bottom);
    RETURN_FALSE(imaqGetPerpendicularLine(points_rect.left_top, 
      points_rect.left_bottom, point, &point, &start_perpendicular, NULL));  
    RETURN_FALSE(imaqGetPerpendicularLine(points_rect.right_top, 
      points_rect.right_bottom, point, &point, &end_perpendicular, NULL));    
  }
  end_arrow = point_mid(&start_perpendicular, &end_perpendicular);

  RGBValue color = options.max ? ColorRed : ColorLime;
  RETURN_FALSE(draw_point(image, point, color));
  if (!options.max)
    start_arrow = symmetrical_point(&end_arrow, &start_arrow);
  RETURN_FALSE(imaqOverlayLine(image, 
    imaqMakePointFromPointFloat(start_perpendicular), 
    imaqMakePointFromPointFloat(end_perpendicular), &color, NULL));
  RETURN_FALSE(imaqOverlayLineWithArrow(image, 
    imaqMakePointFromPointFloat(start_arrow), 
    imaqMakePointFromPointFloat(end_arrow), &color, FALSE, TRUE, NULL));

  return TRUE;
}

BOOL overlay_result_max(ImagePtr image, 
                        const RotatedRect& rect,
                        const PointFloat& first,
                        const PointFloat& last,
                        RakeDirection direction)
{
  if(IsEmpty(image))
    return FALSE;

  options_overlay_result options_overlay_result;
  options_overlay_result.points_rect = rotate_rect(rect);
  options_overlay_result.max = TRUE;
  options_overlay_result.direction = direction;
  options_overlay_result.first = TRUE;
  options_overlay_result.point = first;
  RETURN_FALSE(overlay_result(image, options_overlay_result));
  options_overlay_result.first = FALSE;
  options_overlay_result.point = last;
  RETURN_FALSE(overlay_result(image, options_overlay_result));
  return TRUE;
}

BOOL overlay_result_min(ImagePtr image, 
                        const RotatedRect& rect,
                        const PointFloat& first,
                        const PointFloat& last,
                        RakeDirection direction)
{
  if(IsEmpty(image))
    return FALSE;

  options_overlay_result options_overlay_result;
  options_overlay_result.points_rect = rotate_rect(rect);
  options_overlay_result.max = FALSE;
  options_overlay_result.direction = direction;
  options_overlay_result.first = TRUE;
  options_overlay_result.point = first;
  RETURN_FALSE(overlay_result(image, options_overlay_result));
  options_overlay_result.first = FALSE;
  options_overlay_result.point = last;
  RETURN_FALSE(overlay_result(image, options_overlay_result));
  return TRUE;
}