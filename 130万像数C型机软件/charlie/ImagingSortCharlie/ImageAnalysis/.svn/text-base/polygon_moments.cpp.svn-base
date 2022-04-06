#include "StdAfx.h"
#include "polygon_moments.h"
#define _USE_MATH_DEFINES
#include "math.h"
#include "float.h"
#include "nivision.h"

// bool operator==(const point& pt1, const point& pt2)
// {
//   return pt1.x == pt2.x && pt1.y == pt2.y;
// }
bool operator!=(const point& pt1, const point& pt2)
{
  return !(pt1==pt2);
}

void translate_points(const PointFloat* src, int count, points& pts, 
                      int dx/*=0*/, int dy/*=0*/)
{
  pts.clear();
  for (int i=0; i<count; i++)
  {
    pts.push_back(point(src[i].x+dx, src[i].y+dy));
  }
  if( pts.front() != pts.back() )
    pts.push_back(pts.front());
}


static void
complete_moments( moments* moms )
{
  double cx = 0, cy = 0;
  double mu20, mu11, mu02;

  if( !moms )
    return;
  //assert( moms != 0 );
  moms->inv_sqrt_m00 = 0;

  if( fabs(moms->m00) > DBL_EPSILON )
  {
    double inv_m00 = 1. / moms->m00;
    cx = moms->m10 * inv_m00;
    cy = moms->m01 * inv_m00;
    moms->inv_sqrt_m00 = sqrt( fabs(inv_m00) );

    moms->cx = cx;
    moms->cy = cy;
  }

  /* mu20 = m20 - m10*cx */
  mu20 = moms->m20 - moms->m10 * cx;
  /* mu11 = m11 - m10*cy */
  mu11 = moms->m11 - moms->m10 * cy;
  /* mu02 = m02 - m01*cy */
  mu02 = moms->m02 - moms->m01 * cy;

  moms->mu20 = mu20;
  moms->mu11 = mu11;
  moms->mu02 = mu02;

  /* mu30 = m30 - cx*(3*mu20 + cx*m10) */
  moms->mu30 = moms->m30 - cx * (3 * mu20 + cx * moms->m10);
  mu11 += mu11;
  /* mu21 = m21 - cx*(2*mu11 + cx*m01) - cy*mu20 */
  moms->mu21 = moms->m21 - cx * (mu11 + cx * moms->m01) - cy * mu20;
  /* mu12 = m12 - cy*(2*mu11 + cy*m10) - cx*mu02 */
  moms->mu12 = moms->m12 - cy * (mu11 + cy * moms->m10) - cx * mu02;
  /* mu03 = m03 - cy*(3*mu02 + cy*m01) */
  moms->mu03 = moms->m03 - cy * (3 * mu02 + cy * moms->m01);


  double mu11x = moms->mu11/moms->m00;
  double mu20x = moms->mu20/moms->m00;
  double mu02x = moms->mu02/moms->m00;
  double tangend = 2*mu11x/(mu20x-mu02x);
  double arctangend = atan2(2*mu11x, mu20x-mu02x);
  double arcdegree = arctangend*180/M_PI;
  double theta = arcdegree/2;

//   if(theta<=0)
//     theta = -theta;
//   else
//     theta = 180 - theta;

  moms->orientation = theta;
}

/***********************************************************************
 * 面积有正负之分
 * 逆时针点序，面积为正，反之为负（代表洞）
 ************************************************************************/
float polygon_area(const point* c, int size)
{
  if( size <=0 )
    return 0;
  if( c[size-1] != c[0] )
    return 0;
  float area = 0;
  for (int i=0; i<size-1; i++)
  {
    area += c[i].x*c[i+1].y - c[i+1].x*c[i].y;
  }
  area /= 2;
  return area;
}

void
polygon_moments( const points& contour, moments* moms )
{
  //   int is_float = CV_SEQ_ELTYPE(contour) == CV_32FC2;
  if( contour.empty() )
    return;
  if( contour.front() != contour.back() )
  {
    //cerr<<"polygon_moments 错误: 图形必须关闭"<<endl;
    return;
  }

  //   if( contour->total )
  //   {
  //     CvSeqReader reader;
  double a00, a10, a01, a20, a11, a02, a30, a21, a12, a03;
  double xi, yi, xi2, yi2, xi_1, yi_1, xi_12, yi_12, dxy, xii_1, yii_1;
  //int lpt = contour.size();

  a00 = a10 = a01 = a20 = a11 = a02 = a30 = a21 = a12 = a03 = 0;

  int idx = 0;
  const point& start = contour[idx];
  //cvStartReadSeq( contour, &reader, 0 );

  //     if( !is_float )
  //     {
  //       xi_1 = ((CvPoint*)(reader.ptr))->x;
  //       yi_1 = ((CvPoint*)(reader.ptr))->y;
  //     }
  //     else
  //     {
  //       xi_1 = ((CvPoint2D32f*)(reader.ptr))->x;
  //       yi_1 = ((CvPoint2D32f*)(reader.ptr))->y;
  //     }
  //     CV_NEXT_SEQ_ELEM( contour->elem_size, reader );
  xi_1 = start.x;
  yi_1 = start.y;

  xi_12 = xi_1 * xi_1;
  yi_12 = yi_1 * yi_1;

  for(idx = 0; idx<(int)contour.size(); idx++)
  {
    //       if( !is_float )
    //       {
    //         xi = ((CvPoint*)(reader.ptr))->x;
    //         yi = ((CvPoint*)(reader.ptr))->y;
    //       }
    //       else
    //       {
    //         xi = ((CvPoint2D32f*)(reader.ptr))->x;
    //         yi = ((CvPoint2D32f*)(reader.ptr))->y;
    //       }
    //       CV_NEXT_SEQ_ELEM( contour->elem_size, reader );
    xi = contour[idx].x;
    yi = contour[idx].y;

    xi2 = xi * xi;
    yi2 = yi * yi;
    dxy = xi_1 * yi - xi * yi_1;
    xii_1 = xi_1 + xi;
    yii_1 = yi_1 + yi;

    a00 += dxy;
    a10 += dxy * xii_1;
    a01 += dxy * yii_1;
    a20 += dxy * (xi_1 * xii_1 + xi2);
    a11 += dxy * (xi_1 * (yii_1 + yi_1) + xi * (yii_1 + yi));
    a02 += dxy * (yi_1 * yii_1 + yi2);
    a30 += dxy * xii_1 * (xi_12 + xi2);
    a03 += dxy * yii_1 * (yi_12 + yi2);
    a21 +=
      dxy * (xi_12 * (3 * yi_1 + yi) + 2 * xi * xi_1 * yii_1 +
      xi2 * (yi_1 + 3 * yi));
    a12 +=
      dxy * (yi_12 * (3 * xi_1 + xi) + 2 * yi * yi_1 * xii_1 +
      yi2 * (xi_1 + 3 * xi));

    xi_1 = xi;
    yi_1 = yi;
    xi_12 = xi2;
    yi_12 = yi2;
  }

  double db1_2, db1_6, db1_12, db1_24, db1_20, db1_60;

  if( fabs(a00) > FLT_EPSILON )
  {
    if( a00 > 0 )
    {
      db1_2 = 0.5;
      db1_6 = 0.16666666666666666666666666666667;
      db1_12 = 0.083333333333333333333333333333333;
      db1_24 = 0.041666666666666666666666666666667;
      db1_20 = 0.05;
      db1_60 = 0.016666666666666666666666666666667;
    }
    else
    {
      db1_2 = -0.5;
      db1_6 = -0.16666666666666666666666666666667;
      db1_12 = -0.083333333333333333333333333333333;
      db1_24 = -0.041666666666666666666666666666667;
      db1_20 = -0.05;
      db1_60 = -0.016666666666666666666666666666667;
    }

    /*  spatial moments    */
    moms->m00 = a00 * db1_2;
    moms->m10 = a10 * db1_6;
    moms->m01 = a01 * db1_6;
    moms->m20 = a20 * db1_12;
    moms->m11 = a11 * db1_24;
    moms->m02 = a02 * db1_12;
    moms->m30 = a30 * db1_20;
    moms->m21 = a21 * db1_60;
    moms->m12 = a12 * db1_60;
    moms->m03 = a03 * db1_20;

    complete_moments( moms );
  }
  //   }
}
