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
 * Created:     2010-06-17  9:45
 * Filename:    IACalcMarking.cc
 * Author:      Louis
 * Revisions:   initial
 * 
 * Purpose:     
 *
 */

#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "IAFindShape.h"
#include "NIDispose.h"
#include "sub_image.h"
#include "tools_ni.h"
#include "tools_geometry.h"
#include "shadow_image.h"

#pragma pack(push, 4)
struct marking_report
{
  int count_marking;
  float max_area_marking;
  float min_area_marking;  
  float max_roundness;
  float min_roundness;
};
struct marking_options
{
  Annulus annulus;
  BOOL is_white;
  float max_area_marking;
  float min_area_marking;  
  float max_roundness;
  float min_roundness;
};
#pragma pack(pop)

BOOL init_marking_report(marking_report* marking_report)
{
  if(!marking_report)
    return FALSE;
  marking_report->count_marking = 0;
  marking_report->max_area_marking = 0;
  marking_report->min_area_marking = 999999.9f;
  return TRUE;
}
IA_EXPORT BOOL IA_Calc_FindMarking(const PublicOptions* public_options, 
                                   marking_options *marking_options, 
                                   marking_report *marking_report)
{
  ENTER_FUNCTION;

  if(!public_options || !marking_options || !init_marking_report(marking_report))
    return FALSE;

  int idx = public_options->idx;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image image(display);
  int result = 0;

  if(marking_options->is_white)
    RETURN_FALSE(imaqThreshold(image, image, 0, 0, TRUE, 255));

  int num_particle = 0;
  result = find_shape(image, marking_options->annulus, num_particle, FALSE);
  if(!result || num_particle == 0)
  {
    image.detach();
    return FALSE;
  }

  ImagePtr image_find_aperture = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_image_find_aperture(image_find_aperture);
  RETURN_FALSE(imaqDuplicate(image_find_aperture, image));
  RETURN_FALSE(imaqThreshold(image_find_aperture, image_find_aperture, 
    0, 0, TRUE, 1));
  float area_low = PIf * marking_options->annulus.innerRadius * marking_options->annulus.innerRadius;
  float area_up = PIf * marking_options->annulus.outerRadius * marking_options->annulus.outerRadius;
  result = keep_particle_between(image_find_aperture, 
    IMAQ_MT_PARTICLE_AND_HOLES_AREA, area_low, area_up, &num_particle);

  if(!result || num_particle == 0)
  {
    image.detach();
    return FALSE;
  }
  PointFloat center = imaqMakePointFloat(0, 0);
  RETURN_FALSE(imaqCentroid(image_find_aperture, &center, NULL));
  double area_aperture = 0;
  RETURN_FALSE(imaqMeasureParticle(image_find_aperture, 0, FALSE, 
    IMAQ_MT_AREA, &area_aperture));
  float radius_aperture = sqrtf((float)area_aperture / PIf);
  imaqThreshold(image, image, 1, 1, TRUE, 255);
  RETURN_FALSE(fill_circle(image, center, radius_aperture, TRUE));
  imaqThreshold(image, image, 0, 0, TRUE, 1);
  RETURN_FALSE(remove_small(image, 3, FALSE));

  result = keep_particle_between(image, IMAQ_MT_PARTICLE_AND_HOLES_AREA, 
    marking_options->min_area_marking, 
    marking_options->max_area_marking, &num_particle);

  if(num_particle != 2)
  {
    image.detach();
    return FALSE;
  }
  double area_marking = 0;
  double top = 0;
  double left= 0;
  double width = 0;
  double height = 0;
  double x_center[2];
  double y_center[2];
  RGBValue color = ColorRed;
  Rect rect_particle = imaqMakeRect(0, 0, 0, 0);
  for (int  i = 0; i < num_particle; i ++)
  {
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, IMAQ_MT_AREA, 
      &area_marking));
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_TOP, &top));
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_LEFT, &left));
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_WIDTH, &width));
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_HEIGHT, &height));
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, 
      IMAQ_MT_CENTER_OF_MASS_X, &x_center[i]));
    RETURN_FALSE(imaqMeasureParticle(image, i, FALSE, 
      IMAQ_MT_CENTER_OF_MASS_Y, &y_center[i]));

    rect_particle.top = (int)top ;
    rect_particle.left = (int)left;
    rect_particle.width = (int)width;
    rect_particle.height = (int)height;   
    RETURN_FALSE(imaqOverlayRect(image, rect_particle, &color, 
      IMAQ_DRAW_VALUE, NULL));
    marking_report->count_marking ++;
  }
//   if(!marking_options->is_white)
//     RETURN_FALSE(imaqThreshold(image, image, 0, 0, TRUE, 255));
  point center_marking[2];
  center_marking[0] = imaqMakePointFloat((float)x_center[0], (float)y_center[0]);
  center_marking[1] = imaqMakePointFloat((float)x_center[1], (float)y_center[1]);
  RETURN_FALSE(imaqOverlayLine(image, center_marking[0].pt(),
    center_marking[1].pt(), &color, NULL));
  image.copy_overlay();
  image.detach();
  return TRUE;
}
