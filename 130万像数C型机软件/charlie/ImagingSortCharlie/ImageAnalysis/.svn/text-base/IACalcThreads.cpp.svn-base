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
* Created:     2010-04-13  10:26
* Filename:    IACalcTeethsCount.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     计算内牙的数量以及牙距
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
#include "map"
#include "shadow_image.h"

#pragma pack(push, 4)
struct threads_report
{
  int threads;
  float pitch_max;
  float pitch_min;
};
struct threads_options
{
  BOOL white;
  Rect rect;
  float area_min;
  float area_max;
  //Annulus annulus;
};
#pragma pack(pop)

//求每列列中每个像素值的个数，并返回每个像素值最多一列的个数
void most_y_row(ImagePtr image, int num_particles, vector<int> &most_num_y)
{
  most_num_y.clear();
  if (!image || num_particles <= 0)
    return;
  int width_image = 0;
  int height_image = 0;
  byte* array_image = (byte*)imaqImageToArray(image, IMAQ_NO_RECT, 
    &width_image, &height_image);
  RegisterDispose dis_array_image(array_image);
  if (!array_image)
    return;
  for (int i = 0; i < num_particles; i++)
  {
    most_num_y.push_back(0);
  }
  for (int i = 0; i < width_image; i++)
  {
    vector<int> num_y(num_particles);
    for (int j = 0; j < height_image; j++)
    {
      int pixel = array_image[i + j * width_image];
      if (pixel != 0)
        num_y[pixel - 1]++;
    }
    for (int j = 0; j < num_particles; j++)
    {
      if (num_y[j] > most_num_y[j])
        most_num_y[j] = num_y[j];
    }
  }
}

BOOL overlay_text_threads(ImagePtr image, 
                  Point point_overlay, 
                  const char* text, 
                  const RGBValue* color)
{
  OverlayTextOptions overlay_text_options;
  overlay_text_options.fontName = "Arial";
  overlay_text_options.backgroundColor = IMAQ_RGB_TRANSPARENT;
  overlay_text_options.bold = 0;
  overlay_text_options.italic = 0;
  overlay_text_options.angle = 0;
  overlay_text_options.fontSize = 15;
  overlay_text_options.angle = 0;
  overlay_text_options.horizontalTextAlignment = IMAQ_LEFT;
  overlay_text_options.underline = 0;
  overlay_text_options.strikeout = 0;
  overlay_text_options.verticalTextAlignment = IMAQ_TOP;
  return imaqOverlayText(image, point_overlay, text, color, 
    &overlay_text_options, NULL);
}

BOOL init_threads_report(threads_report* threads_report)
{
  if(!threads_report)
    return FALSE;
  threads_report->pitch_max = 0;
  threads_report->pitch_min = 999999.9f;
  threads_report->threads = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_Threads(const PublicOptions* public_options, 
                               const threads_options* threads_options, 
                               threads_report* threads_report)
{
  ENTER_FUNCTION;

  if (!public_options || !threads_options || !init_threads_report(threads_report))
    return FALSE;
  ClearMyError();

  ImagePtr display = GetImage(public_options->idx, DISPLAY);
  if(IsEmpty(display))
    return FALSE;
  shadow_image image(display);

  sub_image image_sub(image, &threads_options->rect);
  if (IsEmpty(image_sub))
    return FALSE;
  RETURN_FALSE(remove_small(image_sub, 2, TRUE));
  int num_particles = 0;
  //   result = imaqCountParticles(image_sub, TRUE, &num_particles);
  RETURN_FALSE(imaqLabel2(image_sub, image_sub, TRUE, &num_particles));
  double area = 0;
  double top = 0;
  double left= 0;
  double width = 0;
  double height = 0;
  Point point_overlay = imaqMakePoint(0, 0);
  Rect rect_particle = imaqMakeRect(0, 0, 0, 0);
  RGBValue color = ColorRed;
  vector<int> particle_left;     //筛选后剩下的particle的索引
  char text[256];

  for (int i = 0; i < num_particles; i++)
  {
    RETURN_FALSE(imaqMeasureParticle(image_sub, i, FALSE, 
      IMAQ_MT_PARTICLE_AND_HOLES_AREA, &area));
    RETURN_FALSE(imaqMeasureParticle(image_sub, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_TOP, &top));
    RETURN_FALSE(imaqMeasureParticle(image_sub, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_LEFT, &left));
    RETURN_FALSE(imaqMeasureParticle(image_sub, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_WIDTH, &width));
    RETURN_FALSE(imaqMeasureParticle(image_sub, i, FALSE, 
      IMAQ_MT_BOUNDING_RECT_HEIGHT, &height));
    rect_particle.top = (int)top + threads_options->rect.top;
    rect_particle.left = (int)left + threads_options->rect.left;
    rect_particle.width = (int)width;
    rect_particle.height = (int)height;
    point_overlay.x = rect_particle.left;
    point_overlay.y = rect_particle.top;
    color = ColorRed;
    float area_particle = (float)area;
    if (area_particle <= threads_options->area_max && 
      area_particle >= threads_options->area_min)
    {
      particle_left.push_back(i);
      color = ColorLime;
      threads_report->threads++;
    }    
    RETURN_FALSE(imaqOverlayRect(image, rect_particle, &color, 
      IMAQ_DRAW_VALUE, NULL));
    sprintf_s(text, 256, "%6.1f", area_particle);
    RETURN_FALSE(overlay_text_threads(image, point_overlay, text, &color));
  }

  vector<int> highest_particle;
  most_y_row(image_sub, num_particles, highest_particle);
  for (int i = 0; i < (int)particle_left.size(); i++)
  {
    int idx_particle_left = particle_left[i];
    if (threads_report->pitch_max < highest_particle[idx_particle_left])
      threads_report->pitch_max = (float)highest_particle[idx_particle_left];
    if (threads_report->pitch_min > highest_particle[idx_particle_left])
      threads_report->pitch_min = (float)highest_particle[idx_particle_left];
  }

  return TRUE;
}
