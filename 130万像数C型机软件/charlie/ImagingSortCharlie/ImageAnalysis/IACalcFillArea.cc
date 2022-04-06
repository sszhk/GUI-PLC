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
 * Created:     2010-04-27  14:46
 * Filename:    IACalcFillArea.cc
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     
 *
 */

#include "stdafx.h"
#include "IAMain.h"
#include "NIDispose.h"
#include "shadow_image.h"
#include "sub_image.h"
#include "NIROI.h"
#include "tools_ni.h"
#include "tools_geometry.h"
#include "Colors.h"

#pragma pack(push, 4)
struct fill_area_report
{
  PointFloat center;
  int num_particle;
};

struct fill_area_options
{
  Annulus annulus;
  BOOL is_white;
};
#pragma pack(pop)

BOOL init_fill_area_report(fill_area_report* fill_area_report)
{
  if(!fill_area_report)
    return FALSE;
  fill_area_report->center = imaqMakePointFloat(0, 0);
  fill_area_report->num_particle = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FillArea(const PublicOptions* public_options,
                                const fill_area_options* fill_area_options,
                                fill_area_report* fill_area_report)
{
  ENTER_FUNCTION;

  if(!public_options ||
    !fill_area_options ||
    !init_fill_area_report(fill_area_report))
    return FALSE;

  int idx = public_options->idx;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image image(display);
  int result = 0;

  ImagePtr image_locating = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_image_locating(image_locating);
  RETURN_FALSE(imaqDuplicate(image_locating, image));
  RETURN_FALSE(imaqThreshold(image_locating, image_locating, 0, 0, TRUE, 255));

  int num_particle = 0;
  result = find_shape(image_locating, fill_area_options->annulus, num_particle, FALSE);

  if (!result || num_particle != 1)
  {
    fill_area_report->num_particle = num_particle;
    double left = 0;
    double top = 0;
    double width = 0;
    double height = 0;
    for (int i = 0; i < num_particle; i++)
    {
      RETURN_FALSE(imaqMeasureParticle(image_locating, i, FALSE, IMAQ_MT_BOUNDING_RECT_LEFT, &left));
      RETURN_FALSE(imaqMeasureParticle(image_locating, i, FALSE, IMAQ_MT_BOUNDING_RECT_TOP, &top));
      RETURN_FALSE(imaqMeasureParticle(image_locating, i, FALSE, IMAQ_MT_BOUNDING_RECT_WIDTH, &width));
      RETURN_FALSE(imaqMeasureParticle(image_locating, i, FALSE, IMAQ_MT_BOUNDING_RECT_HEIGHT, &height));
      RETURN_FALSE(imaqOverlayRect(image, 
        imaqMakeRect((int)top, (int)left, (int)height, (int)width), 
        &ColorRed, IMAQ_DRAW_VALUE, NULL));
    }
    image.copy_overlay();
    image.detach();
    return FALSE;
  }
  RETURN_FALSE(imaqFillHoles(image_locating, image_locating, TRUE)); 
  RETURN_FALSE(imaqCentroid(image_locating, &fill_area_report->center, NULL));
  RETURN_FALSE(imaqThreshold(image_locating, image_locating, 0, 0, TRUE, 255));

  RETURN_FALSE(fill_circle(image, fill_area_report->center, 
    (float)fill_area_options->annulus.outerRadius,
    fill_area_options->is_white));

  return TRUE;
}