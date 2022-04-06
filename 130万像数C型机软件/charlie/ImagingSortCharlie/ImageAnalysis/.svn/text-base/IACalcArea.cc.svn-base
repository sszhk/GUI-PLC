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
 * Created:     2010-07-14  10:23
 * Filename:    IACalcArea.cc
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     
 *
 */

#include "stdafx.h"
#include "IAMain.h"
#include "NICalc.h"
#include "NIROI.h"
#include "point.h"
#include "sub_image.h"
#include "NIDispose.h"
#include "Utilities.h"
#include "shadow_image.h"

#pragma pack(push, 4)
struct area_report
{
  float area;
};
struct area_options
{
  BOOL is_white;
  Rect rect;
};
#pragma pack(pop)

BOOL init_area_report(area_report* report)
{
  if (!report)
    return FALSE;
  report->area = 0;
  return TRUE;
}

//#define ERROR_RETURN return -1;
IA_EXPORT BOOL IA_Calc_Area(const PublicOptions* public_options, 
                            const area_options* area_options, 
                            area_report* area_report)
{
  if (!public_options || !area_options || !init_area_report(area_report))
    return FALSE;
  ClearMyError();

  int idx = public_options->idx;
  int threshold = public_options->thres;
  bool displaystatus = public_options->isdisplaystatus;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image image(display);
  sub_image sub(image, &area_options->rect);

  HistogramReport* hr = imaqHistogram(sub, 2, 0, 255, NULL);
  RegisterDispose dis_hr(hr);
  if (!hr)
    return FALSE;
  if(area_options->is_white)
    area_report->area = (float)hr->histogram[1];
  else
    area_report->area = (float)hr->histogram[0];
  return TRUE;
}