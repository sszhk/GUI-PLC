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
* Created:     2010-04-08  12:38
* Filename:    tools_ni.h
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     NIÀ„∑®π§æﬂ
*
*/

#ifndef TOOLS_NI_H
#define TOOLS_NI_H

#include "ni/NIMV.h"
#include "basic_types.h"
#include "nimachinevision.h"
#include "point.h"

struct filter_options
{
  MeasurementType measurement_type;
  float lower;
  float upper;
  BOOL exclude;
};

struct find_edge_options
{
  RotatedRect search_rect;
  RakeDirection direction;
  int width;
  int steepness;
  int gap;
  BOOL show_results;
};

struct points_rect
{
  PointFloat left_top;
  PointFloat right_top;
  PointFloat left_bottom;
  PointFloat right_bottom;
};

struct edge_circle_options
{
  Annulus annulus;
  int gap;
  int smoothing;
};

RotatedRect make_rotated_rect(int width, int height, 
                              PointFloat center, float angle);
Rect make_rect(int width, int height, PointFloat center);
PointFloat point_to_pointfloat(const Point* source);
BOOL remove_small(ImagePtr image, int erosions, int connectivity8);
BOOL morphology(ImagePtr image, MorphologyMethod_enum morphology_method);
BOOL keep_particle_between(ImagePtr image, MeasurementType measurement_type, 
                     float lower, float upper, int* num_particals);
StraightEdgeReport* find_edge(ImagePtr image, 
                              const find_edge_options* options );
PointFloat* simple_edge(ImagePtr image, PointFloat first, PointFloat last);
EdgeReport* edge_tool(ImagePtr image, point first, point last, 
                      int* num_edges);
BOOL keep_particle_largest(ImagePtr image);
BOOL locating(ImagePtr image, float* angle_particle, PointFloat* center);
points_rect rotate_rect(const RotatedRect& rect);
BOOL fill_circle(ImagePtr image, const PointFloat& center, 
                 const float& radius, BOOL is_white);
BOOL find_shape(ImagePtr image, Annulus annulus, int& count, BOOL opend);
CircularEdgeReport* find_diameter(ImagePtr img, 
                                 const edge_circle_options* options, 
                                 SpokeReport** spoke_report);
BOOL make_visible(ImagePtr img);
BOOL make_calculatible(ImagePtr img);
BOOL make_inverse(ImagePtr img);
BOOL mask_down(ImagePtr image, Rect rect);
RakeReport* rake(ImagePtr image, RakeDirection direction, EdgeProcess process, Rect rect);


#ifdef _DEBUG
static __inline void TRACE(char *fmt, ...)
{
  va_list args;
  char buf[1024];
  va_start(args, fmt);
  vsprintf(buf, fmt, args);
  OutputDebugStringA(buf);
  // FILE *fp = fopen("d:\\test.txt","a");
  // fwrite(buf,strlen(buf),1,fp);
  // fclose(fp);
}
#else
static __inline void TRACE(char *fmt, ...) {}
#endif


#endif