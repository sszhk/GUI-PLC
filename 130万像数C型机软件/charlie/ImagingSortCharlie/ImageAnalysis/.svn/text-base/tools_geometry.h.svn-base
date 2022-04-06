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
* Created:     2010-04-08  12:36
* Filename:    tools_geometry.h
* Author:      Louis
* Revisions:   initial
* 
* Purpose:     几何算法工具
*
*/

#ifndef TOOLS_GEOMETRY_H
#define TOOLS_GEOMETRY_H

#include "nivision.h"
#define _USE_MATH_DEFINES
#include "math.h"
#include "tools_ni.h"

#define  PI M_PI
#define  PIf ((float)M_PI)

//返回中点
PointFloat point_mid(const PointFloat* point_start, 
                     const PointFloat* point_end);
//返回关于中心点的对称点
PointFloat symmetrical_point(const PointFloat* center, 
                             const PointFloat* point_source);
float area_circle(float radius);
PointFloat rotate_point(const PointFloat* point, const PointFloat* center, 
                        float angle);


#endif