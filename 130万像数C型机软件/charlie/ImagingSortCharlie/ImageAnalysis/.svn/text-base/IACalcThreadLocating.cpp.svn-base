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
* Created:     2010-04-12  22:10
* Filename:    IACalcThreadLocating.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     对所选区域内牙部进行定位，返回中点
*
*/
#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "IAFindShape.h"
#include "NIDispose.h"
#include "sub_image.h"
#include "tools_ni.h"
#include "shadow_image.h"

#pragma pack(push, 4)
struct ThreadLocatingReport
{
  PointFloat center;
  float x_right;
  float width;
  float angle;
};
struct ThreadLocatingOptions
{
  Rect rc;
  BOOL isWhite;
};
#pragma pack(pop)

BOOL init_thread_locating_report(ThreadLocatingReport* thread_locating_report)
{
  if(!thread_locating_report)
    return FALSE;
  thread_locating_report->angle = 0;
  thread_locating_report->center = imaqMakePointFloat(0, 0);
  thread_locating_report->width = 0;
  thread_locating_report->x_right = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FindThreadLocating(const PublicOptions* po, const ThreadLocatingOptions *to, ThreadLocatingReport *tr)
{
  ENTER_FUNCTION;

  if (!po || !to || !init_thread_locating_report(tr))
    return FALSE;
  ClearMyError();

  ImagePtr display = GetImage(po->idx, DISPLAY);
  if( IsEmpty(display))
    return FALSE;
  shadow_image img(display);

  double angle = 0;
  tr->angle = 0;
  tr->width = 0;
  tr->center.x = 0;
  tr->center.y = 0;

  sub_image sub(img, &to->rc);
  RETURN_FALSE (!IsEmpty(sub));
  if (!to->isWhite)
    RETURN_FALSE(imaqThreshold(sub, sub, 0, 0, TRUE, 255));
  RETURN_FALSE(locating(sub, &tr->angle, &tr->center));

  FindEdgeOptions options_edge;
  options_edge.threshold = 1;
  options_edge.width = 4;
  options_edge.steepness = 2;
  options_edge.subsamplingRatio = 3;
  options_edge.showSearchArea = 0;
  options_edge.showSearchLines = 0;
  options_edge.showEdgesFound = 0;
  options_edge.showResult = 0;
  PointFloat first = imaqMakePointFloat(0, 0);
  PointFloat last = imaqMakePointFloat(0, 0);
  RotatedRect rect_rotate = imaqMakeRotatedRect(0, 0, to->rc.height, to->rc.width, 0);
  RETURN_FALSE(imaqClampMax(sub, rect_rotate, IMAQ_LEFT_TO_RIGHT, 
    &tr->width, &options_edge, NULL, &first, &last));
  last.x += to->rc.left;
  last.y += to->rc.top;

  if (!to->isWhite)
    RETURN_FALSE(imaqThreshold(sub, sub, 0, 0, TRUE, 255));
  sub.Commit();
  tr->center.x += to->rc.left;
  tr->center.y += to->rc.top;
  tr->x_right = last.x;
  DrawSmallRect(img, tr->center, 5, &ColorRed);
//   DrawSmallRect(img, last, 5, &ColorRed);

  return TRUE;
}