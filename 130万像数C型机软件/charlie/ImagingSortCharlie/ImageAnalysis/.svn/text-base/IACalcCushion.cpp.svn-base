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
* Created:     2010-04-13  9:32
* Filename:    IACalcCushion.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     
* ¼ì²âµ¯µæ´íÂÒ
* Ãû´Ê½âÊÍ£º
* Cusion = µ¯µæ
*
*/

#include "stdafx.h"
#include "IAMain.h"
#include "Colors.h"
#include "tools_ni.h"
#include "sub_image.h"
#include "NIDispose.h"
#include "shadow_image.h"

#pragma pack(push, 4)
struct cushion_report
{
  float distance_up;
  float distance_down;
  float distance_average;
  float width;
};
struct cushion_options
{
  Rect rect;
  BOOL white;
};
#pragma pack(pop)

BOOL init_cushion_report(cushion_report* cushion_report)
{
  if (!cushion_report)
    return FALSE;
  cushion_report->distance_up = 0;
  cushion_report->distance_down = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_Cushion(const PublicOptions* public_options, 
                               const cushion_options *cushion_options, 
                               cushion_report *cushion_report)
{
  ENTER_FUNCTION;

  if (!public_options || 
    !cushion_options || 
    !init_cushion_report(cushion_report))
    return FALSE;
  ClearMyError();

//   const int WIDTH_IMAGE = 640;
//   const int HEIGHT_IMAGE = 480;
  const int STEP = 3;//½«±ß½çÉÏÏÂ¸÷ÒÆ¶¯STEP¸öÏñËØ

  ImagePtr display = GetImage(public_options->idx, DISPLAY);
  if( IsEmpty(display))
    return FALSE;
  shadow_image image(display);

  sub_image image_sub(image, &cushion_options->rect);
  if (IsEmpty(image_sub))
    return FALSE;
  int result = 0;
  cushion_report->distance_up = 0;
  cushion_report->distance_down = 0;
  float angle = 0;
  PointFloat center;
  if (!cushion_options->white)
    RETURN_FALSE(imaqThreshold(image_sub, image_sub, 0, 0, TRUE, 255));
  RETURN_FALSE(locating(image_sub, &angle, &center));
  if (!cushion_options->white)
    RETURN_FALSE(imaqThreshold(image_sub, image_sub, 0, 0, TRUE, 255));
  image_sub.Commit();

  //¼ÆËãµ¯µæ
  FindEdgeOptions edgeOptions;
  edgeOptions.threshold = 1;
  edgeOptions.width = 1;
  edgeOptions.steepness = 1;
  edgeOptions.subsamplingRatio = 5;
  edgeOptions.showSearchArea = 0;
  edgeOptions.showSearchLines = 0;
  edgeOptions.showEdgesFound = 0;
  edgeOptions.showResult = 1;

  PointFloat first_up = imaqMakePointFloat(0, 0);
  PointFloat first_down = imaqMakePointFloat(0, 0);
  float witdh = 0;
  RETURN_FALSE(imaqClampMax(image, 
    imaqMakeRotatedRectFromRect(cushion_options->rect), 
    IMAQ_TOP_TO_BOTTOM, &witdh, &edgeOptions, NULL, &first_up, &first_down));

//   first_up.y -= STEP;
//   first_down.y += STEP;
//   first_up.y = first_up.y > 0 ? first_up.y : 0;
//   first_down.y = first_down.y < HEIGHT_IMAGE ? first_down.y : HEIGHT_IMAGE;
// 
//   PointFloat last_up = imaqMakePointFloat((float)WIDTH_IMAGE, first_up.y);
//   PointFloat* edges_up = simple_edge(image, first_up, last_up);
//   RegisterDispose dispose_edges_up(edges_up);
//   if( !edges_up )
//   {
//     image.detach();
//     return FALSE;
//   }
// 
//   PointFloat last_down = imaqMakePointFloat((float)WIDTH_IMAGE, first_down.y);
//   PointFloat* edges_down = simple_edge(image, first_down, last_down);
//   RegisterDispose dispose_edges_down(edges_down);
//   if( !edges_down )
//   {
//     image.detach();
//     return FALSE;
//   }

//   RETURN_FALSE(imaqOverlayLineWithArrow(image, 
//     imaqMakePoint(cushion_options->rect.left, (int)edges_up[0].y), 
//     imaqMakePointFromPointFloat(edges_up[0]), 
//     &ColorLime, FALSE, TRUE, NULL));
//   RETURN_FALSE(imaqOverlayLineWithArrow(image, 
//     imaqMakePoint(cushion_options->rect.left, (int)edges_down[0].y), 
//     imaqMakePointFromPointFloat(edges_down[0]), 
//     &ColorLime, FALSE, TRUE, NULL));

//   cushion_report->distance_up = 640 - edges_up[0].x;
//   cushion_report->distance_down = 640 - edges_down[0].x;

  RETURN_FALSE(imaqOverlayLineWithArrow(image, 
    imaqMakePoint(cushion_options->rect.left, (int)first_up.y), 
    imaqMakePointFromPointFloat(first_up), 
    &ColorLime, FALSE, TRUE, NULL));
  RETURN_FALSE(imaqOverlayLineWithArrow(image, 
    imaqMakePoint(cushion_options->rect.left, (int)first_down.y), 
    imaqMakePointFromPointFloat(first_down), 
    &ColorLime, FALSE, TRUE, NULL));

  cushion_report->distance_up = WIDTH_IMAGE - first_up.x;
  cushion_report->distance_down = WIDTH_IMAGE - first_down.x;
  cushion_report->distance_average = 
    (cushion_report->distance_up + cushion_report->distance_down) / 2;
  cushion_report->width = witdh;

  return TRUE;
}