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
 * Created:     2010-05-06  9:58
 * Filename:    overlay.h
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     
 *
 */
#ifndef OVERLAY_H
#define OVERLAY_H
#include "nivision.h"
#include "basic_types.h" 
#include "tools_geometry.h"
#include "tools_ni.h"

struct options_overlay_result
{
  points_rect points_rect;
  PointFloat point;
  RakeDirection direction;
  BOOL first;
  BOOL max;
};

BOOL draw_point(ImagePtr image, PointFloat point, RGBValue color);
BOOL overlay_result(ImagePtr image, const options_overlay_result& options);
BOOL overlay_result_max(ImagePtr image, const RotatedRect& rect,
                        const PointFloat& first, const PointFloat& last,
                        RakeDirection direction);
BOOL overlay_result_min(ImagePtr image, const RotatedRect& rect,
                        const PointFloat& first, const PointFloat& last,
                        RakeDirection direction);


#endif