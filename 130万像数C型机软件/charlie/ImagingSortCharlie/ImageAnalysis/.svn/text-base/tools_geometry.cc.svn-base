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
* Created:     2010-04-08  13:21
* Filename:    tools_geometry.cpp
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     
*
*/
#include "stdafx.h"
#include "tools_geometry.h"

PointFloat point_mid(const PointFloat* point_start, 
                     const PointFloat* point_end)
{
  PointFloat point_mid = imaqMakePointFloat(0, 0);
  if (point_start && point_end)
  {
    point_mid.x = (point_start->x + point_end->x) / 2;
    point_mid.y = (point_start->y + point_end->y) / 2;
  }
  return point_mid;
}

PointFloat symmetrical_point(const PointFloat* center, 
                             const PointFloat* point_source)
{
  PointFloat point_symmetrical = imaqMakePointFloat(0, 0);
  if (center && point_source)
  {
    point_symmetrical.x = 2 * center->x - point_source->x;
    point_symmetrical.y = 2 * center->y - point_source->y;
  }
  return point_symmetrical;
}

float area_circle(float radius)
{
  return PIf * radius * radius;
}

PointFloat rotate_point(const PointFloat* point, 
                        const PointFloat* center, 
                        float angle)
{
  PointFloat point_rotate = imaqMakePointFloat(0, 0); 
  if (point && center)
  {
    float angle_radian = angle * PIf / 180; 
    float dx = point->x - center->x;
    //float dy = point->y - center->y;  平面直角坐标系
    float dy = center->y - point->y; //计算机坐标系
    float cos_angle = cos(angle_radian);
    float sin_angle = sin(angle_radian);
    point_rotate.x = center->x + dx * cos_angle - dy * sin(angle_radian);
    //平面坐标系
    //point_rotate.y = center->y + dx * sin(angle_radian) + dy * cos_angle;
    point_rotate.y = center->y - (dx * sin(angle_radian) + dy * cos_angle);
  }
  return point_rotate;
}