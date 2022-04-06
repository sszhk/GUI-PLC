#include "stdafx.h"
#include "IAMain.h"
#include "NICalc.h"
#include "NIROI.h"
#include "point.h"
#include "sub_image.h"
#include "NIDispose.h"
#include "Utilities.h"
#include "auto_array.h"
#include "shadow_image.h"
#include "tools_ni.h"
#include "cv/BlobResult.h"
#include "polygon_moments.h"
#include "auto_array.h"
#include "NIPoints.h"
#include "PolygonNIFloat.h"
#include "tools_geometry.h"
#include "hi_res_timer.h"
#include "log_entry.h"

// #define DRAW_LINE

#ifdef _DEBUG
#pragma comment(lib,"cv/cvblobslibd")
#pragma comment(lib,"cv/cxcore210d")
#pragma comment(lib,"cv/cv210d")
#pragma comment(lib,"cv/flannd")
#pragma comment(lib,"cv/zlibd")
#pragma comment(lib,"cv/opencv_lapackd")

#else
#pragma comment(lib,"cv/cvblobslib")
#pragma comment(lib,"cv/cxcore210")
#pragma comment(lib,"cv/cv210")
#pragma comment(lib,"cv/flann")
#pragma comment(lib,"cv/zlib")
#pragma comment(lib,"cv/opencv_lapack")
#endif

#pragma pack(push, 4)
struct ThreadDamageReport
{
  float damage;
  int top;
};
struct ThreadDamageOptions
{
  Rect rc;
  float threadDamage;
  BOOL isThreadDamage;
  int height;
  int width;
  float contrast;
  BOOL Rotated;
  BOOL correct;
  int offset;
};
#pragma pack(pop)

BOOL init_thread_damage_report(ThreadDamageReport* thread_damage_report)
{
  if(!thread_damage_report)
    return FALSE;
  thread_damage_report->damage = 9999.9f;
  thread_damage_report->top = HEIGHT_IMAGE;
  return TRUE;
}

#define EPSINON 0.00001
#define ABS(x) ((x)>0?(x):-(x))

typedef points line;
typedef std::vector<line> lines;

IplImage* get_ipl(ImagePtr img)
{
  int w,h;
  byte* data = (byte*)imaqImageToArray(img, IMAQ_NO_RECT, &w, &h);
  RegisterDispose dis_data(data);
  if( !data )
    return NULL;
  IplImage* ipl = cvCreateImage(cvSize(w,h), 8, 1);
  if( !ipl )
    return NULL;
  memcpy(ipl->imageData, data, w*h);

  return ipl;
}

BOOL get_imaq(IplImage*& ipl, ImagePtr img, bool free_ipl = true)
{
  BOOL result = imaqArrayToImage(img, ipl->imageData, ipl->width, ipl->height);
  if( free_ipl )
    cvReleaseImage(&ipl);
  return result;
}

BOOL get_contour(CBlob* blob, line& contour)
{
  contour.clear();
  if( blob->GetExternalContour() == NULL )
    return FALSE;

  CvSeqReader reader;
  t_chainCodeList chain = blob->GetExternalContour()->GetContourPoints();
  if( !chain )
    return FALSE;

  cvStartReadSeq( chain, &reader);
  CvPoint pt;
  for (int j=0; j<chain->total; j++)
  {
    CV_READ_SEQ_ELEM( pt, reader );
    contour.push_back(point(pt.x, pt.y));
  }
  return TRUE;
}

CvPoint get_left_most(const line& l)
{
  point pt(99999, 0);
  for (UINT i=0; i<l.size(); i++)
  {
    if( pt.x > l[i].x )
      pt = l[i];
  }
  return cvPoint(ROUND_TO_INT(pt.x), ROUND_TO_INT(pt.y));
}

CvPoint get_right_most(const line& l)
{
  point pt(-1, 0);
  for (UINT i=0; i<l.size(); i++)
  {
    if( pt.x < l[i].x )
      pt = l[i];
  }
  return cvPoint(ROUND_TO_INT(pt.x), ROUND_TO_INT(pt.y));
}

void fill_ipl(IplImage* ipl, byte gray = 0)
{
  if( !ipl || !ipl->imageData)
    return;
  memset(ipl->imageData, gray, ipl->imageSize);
}

void draw_blobs(CBlobResult& blobs, IplImage* ipl, byte gray=1, bool clear=true)
{
  if( !ipl || !ipl->imageData )
    return;
  if( clear )
    fill_ipl(ipl);
  for (int i=0; i<blobs.GetNumBlobs(); i++)
  {
    blobs.GetBlob(i)->FillBlob(ipl, cvScalar(gray));
  }
}

void draw_defect(ImagePtr img, const PointFloat* pt, 
                 const RGBValue* color, int size, bool quan = false)
{
  int o = size/10;
  Rect rc1, rc2, rc3, rc4;
  Point center = point(*pt).pt();

  rc1 = imaqMakeRect(center.y-size+o, center.x, size-o, 1/*o*2+1*/);
  rc2 = imaqMakeRect(rc1.top+rc1.height, rc1.left+rc1.width, rc1.width, rc1.height);
  rc3 = imaqMakeRect(rc2.top+rc2.height, rc1.left, rc1.height, rc1.width);
  rc4 = imaqMakeRect(rc2.top, rc1.left-rc2.width, rc2.height, rc2.width);
  o++;
  rc1.height -= o;
  rc2.left += o; rc2.width -= o;
  rc3.top += o; rc3.height -= o;
  rc4.width -= o;

  imaqOverlayRect(img, rc1, color, IMAQ_PAINT_VALUE, NULL);
  imaqOverlayRect(img, rc2, color, IMAQ_PAINT_VALUE, NULL);
  imaqOverlayRect(img, rc3, color, IMAQ_PAINT_VALUE, NULL);
  imaqOverlayRect(img, rc4, color, IMAQ_PAINT_VALUE, NULL);

  if( quan )
  {
    int r1 = ROUND_TO_INT(size/3);
    int r2 = ROUND_TO_INT(size*2/3);
    rc1 = imaqMakeRect(center.y-r1, center.x-r1, r1*2+1, r1*2+1);
    rc2 = imaqMakeRect(center.y-r2, center.x-r2, r2*2+1, r2*2+1);
    imaqOverlayOval(img, rc1, color, IMAQ_DRAW_VALUE, NULL);
    imaqOverlayOval(img, rc2, color, IMAQ_DRAW_VALUE, NULL);
  }
}

void get_points_safe(point* dest, const point* src, int size, int idx, int total)
{
  if( size <= 0 )
    return;
  int from = GetSafeIndex(idx, size);
  int to = GetSafeIndex(idx+total, size);
  if( to < from )
  {
    int part1 = size - from - 1;
    int part2 = to + 1;
    memcpy(dest, src+from, sizeof(point)*part1);
    memcpy(dest+part1, src, sizeof(point)*part2);
  }
  else
  {
    memcpy(dest, src+from, sizeof(point)*total);
  }
}

void get_points_safe(point* dest, const line& one, int idx, int total)
{
  if( one.empty() )
    return;
  get_points_safe(dest, &one[0], one.size(), idx, total);
}

int judge_crack(CBlobResult blobs, Rect rc, PointFloat& ptf_draw)
{
  const int MIN_DAMAGE = 2000;
  int max_damage = 0;
  for (int i=0; i<blobs.GetNumBlobs(); i++)
  {
    CBlob* blobi = blobs.GetBlob(i);
    CvRect rc1 = blobi->GetBoundingBox();
    for (int j=0; j<blobs.GetNumBlobs(); j++)
    {
      if( i == j )
        continue;
      CBlob* blobj = blobs.GetBlob(j);
      CvRect rc2 = blobj->GetBoundingBox();
      // 条件1：A的外接矩形和B的在Y方向上有重合
      // 条件2：A的右边与B的左边很接近
      // 条件3：A的左边小于B的左边
      if( (rc1.y > rc2.y && rc1.y < (rc2.y+rc2.height) ||
        (rc1.y+rc1.height > rc2.y && rc1.y+rc1.height < (rc2.y+rc2.height))) &&
        abs(rc1.x+rc1.width-rc2.x) < 20 &&
        rc1.x < rc2.x )
      {
        if(rc1.y + rc1.height + 1 == rc.top + rc.height || rc2.y + rc2.height + 1 == rc.top + rc.height )
          continue;
        if(rc1.y == rc.top || rc2.y == rc.top )
          continue;

        int left_draw = min(rc1.x + rc1.width, rc2.x + rc2.width);
        int right_draw = max(rc1.x, rc2.x);
        int y_draw = (rc1.y + rc1.height / 2 + rc2.y + rc2.height / 2) / 2;
        int damage = MIN_DAMAGE + abs(right_draw - left_draw) * rc1.height;
        if(damage > max_damage)
        {
          max_damage = damage;
          ptf_draw = imaqMakePointFloat((float)(left_draw + right_draw) / 2, (float)y_draw);
        }
      }
    }
  }
  return max_damage;

}

typedef vector<int> sum_kernel;
typedef vector<int> y_border;

bool get_sum_kernel(line border, int kernel, y_border& y_down, point& left, point& right, sum_kernel& sum)
{
  const int MIN_WIDTH = 50;

  UINT t = border.size();
  PointFloat* p = border[0].pf();
  UINT lm, rm;
  for (UINT j=0; j<t; j++)
  {
    if( left.x > p[j].x )
    {
      left = p[j];
      lm = j;
    }

    if( right.x < p[j].x )
    {
      right = p[j];
      rm = j;
    }
  }

  int dsize = abs((int)(rm - lm));
  int usize = t - dsize;
  auto_array<point> down(dsize);
  auto_array<point> up(usize);
  get_points_safe(down, border, lm, dsize);
  get_points_safe(up, border, rm, usize);

  int w = (int)right.x - (int)left.x + 1;
  if(w < MIN_WIDTH)
    return false;
  y_down.resize(w);
  //沿x方向找y值最大的边界
  for (int j = 0; j < dsize; j++)
  {
    if((int)down[j].y > y_down[(int)down[j].x - (int)left.x])
      y_down[(int)down[j].x - (int)left.x] = (int)down[j].y;
  }

  //按核取和
  sum.resize(w - kernel + 1);
  for (int j = 0; j < w - kernel + 1; j++)
  {
    sum[j] = 0;
    for (int k = 0; k < kernel; k++)
    {
      sum[j] += y_down[j + k];
    }
  }
  return true;
}

int get_damage(ImagePtr img, int kernel, sum_kernel sum, point left, Rect rc, 
               float damage, y_border down_y,   PointFloat& ptf_draw)
{
  const int HEIGHT_DAMAGE = 55;
  const int WIDTH_DAMAGE = 40;
  const int SIGNAL_HEIGHT = 20;
  const int HEIGHT_DAMAGE_DOWN = 50;

  int max_damage = 0;

  int diff1 = sum[kernel] - sum[0];
  Point pt1;
  pt1.x = kernel / 2 + (int)left.x;
  pt1.y = (int)left.y + diff1;
  bool in_up = true;//是不是渐增
  int max_diff = diff1;
  int min_diff = 0;
  int start = 0;
  int end = 0;
  bool found_up_top = false;//是不是正在找上顶点

  for (int j = 1; j < (int)sum.size() - kernel - 1; j++)
  {
    int diff2 = sum[j + kernel] - sum[j];
    Point pt2;
    pt2.x = j + kernel / 2 + (int)left.x;
    pt2.y = (int)left.y + diff2;
#ifdef DRAW_LINE
    imaqOverlayLine(img, pt1, pt2, &ColorBlue, NULL);
#endif
    if(diff2 > diff1)
    {
      if(!in_up)
      {
        if(diff2 > max_diff)
        {
          max_diff = diff2;
          if(!found_up_top)
            start = j;
        }
      }
      else
      {
        in_up = false;
        if((diff1 < 0 || diff2 < 0) && !found_up_top && diff1 < -SIGNAL_HEIGHT)
        {
          int w_d = j - start;
          min_diff = diff1;
          int h_d = max_diff - min_diff;
          int d = 0;
          if(h_d < HEIGHT_DAMAGE || w_d > WIDTH_DAMAGE || max_diff < SIGNAL_HEIGHT || min_diff > -SIGNAL_HEIGHT)  
            d = 0;
          else
          {
            h_d -= HEIGHT_DAMAGE;
            w_d -= 10;
            d = w_d * abs(w_d) + h_d * h_d;
          }
          if(d > damage)
          {
            Point pt;
            pt.x = start + kernel / 2 + (int)left.x;
            pt.y = (int)left.y + diff2;
#ifdef DRAW_LINE
            imaqOverlayLine(img, pt, pt2, &ColorRed, NULL);
#endif

            Point pt0;
            pt0.x = /*(start + j) / 2*/j + kernel / 2 + (int)left.x;
            pt0.y = down_y[/*(start + j) / 2*/j + kernel / 2];
#ifdef DRAW_LINE
            draw_defect(img, &point_to_pointfloat(&pt0), &ColorRed, 13, true);
#endif
          }
          if(d > max_damage)
          {
            max_damage = d;
            ptf_draw.x = /*(start + j) / 2*/j + kernel / 2 + left.x;
            ptf_draw.y = (float)down_y[/*(start + j) / 2*/j + kernel / 2];
          }
          start = j;
          found_up_top = true;
          max_diff = min_diff = diff1;
        }
      }
    }
    else if(diff2 < diff1)
    {
      if(!in_up)
      {
        in_up = true;
        if((diff1 > 0 || diff2 > 0) && found_up_top && max_diff > SIGNAL_HEIGHT)
        {
          int w_d = j - start;
          max_diff = diff1;
          int h_d = max_diff - min_diff;
          int d = 0;
          if(h_d < HEIGHT_DAMAGE_DOWN || w_d > WIDTH_DAMAGE || max_diff < SIGNAL_HEIGHT || min_diff > -SIGNAL_HEIGHT)  
            d = 0;
          else
          {
            h_d -= HEIGHT_DAMAGE_DOWN;
            w_d -= 10;
            d = w_d * abs(w_d) + h_d * h_d;
          }
          if(d > damage)
          {
            Point pt;
            pt.x = start + kernel / 2 + (int)left.x;
            pt.y = (int)left.y + diff2;
#ifdef DRAW_LINE
            imaqOverlayLine(img, pt, pt2, &ColorRed, NULL);
#endif

            Point pt0;
            pt0.x = /*(start + j) / 2*/j + kernel / 2 + (int)left.x;
            pt0.y = down_y[/*(start + j) / 2*/j + kernel / 2];
#ifdef DRAW_LINE
            draw_defect(img, &point_to_pointfloat(&pt0), &ColorRed, 13, true);
#endif
          }
          if(d > max_damage)
          {
            max_damage = d;
            ptf_draw.x = /*(start + j) / 2*/j + kernel / 2 + left.x;
            ptf_draw.y = (float)down_y[/*(start + j) / 2*/j + kernel / 2];
          }
          start = j;
          found_up_top = false;
          max_diff = min_diff = diff1;
        }
      }
      else
      {
        if(min_diff > diff2)
        {
          min_diff = diff2;
          if(found_up_top)
            start = j;
        }
      }

    }
    pt1 = pt2;
    diff1 = diff2;
  }
  return max_damage;
}

int locate(ImagePtr image, Rect rect)
{
  Rect rect_rake = rect;
  int min_y = HEIGHT_IMAGE;
  rect_rake.height = rect_rake.top;
  rect_rake.top = 0;
  RakeReport* report = rake(image, IMAQ_TOP_TO_BOTTOM, IMAQ_FIRST, rect_rake);
  RegisterDispose dis_rr(report);
  if( !report || !report->firstEdges)
    return 0;
  for (unsigned i = 0; i < report->numFirstEdges; i++)
  {
    int y = (int)report->firstEdges[i].y;
    if(y < min_y)
      min_y = y;
  }
  return min_y;
}

IA_EXPORT BOOL IA_Calc_ThreadDamage(const PublicOptions* po, 
                                    const ThreadDamageOptions* tdo, 
                                    ThreadDamageReport* tdr)
{
  ENTER_FUNCTION;

  if (!po || !tdo || !init_thread_damage_report(tdr))
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);
  Rect rc = tdo->rc;

  const int MIN_MEAN = 10;
  HistogramReport* hr = imaqHistogram(img, 256, 0, 255, NULL);
  RegisterDispose dis_hr(hr);
  if(!hr || hr->mean < MIN_MEAN)
    return FALSE;

  //定位用，找最高点
  tdr->top = locate(img, rc);
  if(!tdo->correct && tdr->top != 0)
    rc.top = tdr->top + tdo->offset;

  mask_down(img, rc);
  make_calculatible(img);
  remove_small(img, 1, FALSE);
  imaqThreshold(img, img, 0, 0, TRUE, 1);
  remove_small(img, 1, FALSE);
  imaqThreshold(img, img, 0, 0, TRUE, 255);

  IplImage* ipl = get_ipl(img);
  dispose_ipl dis_ipl(ipl);
  CBlobResult blobs(ipl, NULL, 0);

  int max_damage = 0;
  PointFloat ptf_draw = imaqMakePointFloat(0, 0);
  //判断有没有断线
  max_damage = judge_crack(blobs, rc, ptf_draw);

  // 提取满足条件的边界点，准备计算
  lines all;
  for (int i=0; i<blobs.GetNumBlobs(); i++)
  {
    all.push_back(line());
    get_contour(blobs.GetBlob(i), all.back());
  }

  const int KERNEL = 15;
  for (UINT i=0; i<all.size(); i++)
  {
    point left(99999, 0), right(-1, 0);
    sum_kernel sum;
    y_border down_y;
    if(!get_sum_kernel(all[i], KERNEL, down_y, left, right, sum))
      continue;
    PointFloat ptf_max;
    int d = get_damage(img, KERNEL, sum, left, rc, tdo->threadDamage, down_y, ptf_max);
    if(d > max_damage)
    {
      ptf_draw = ptf_max;
      max_damage = d;
    }

#ifdef DRAW_LINE
    Point pt_f;
    Point pt_l;
    pt_f.x = (int)left.x;
    pt_l.x = (int)right.x;
    pt_f.y = pt_l.y = (int)left.y;
    imaqOverlayLine(img, pt_f, pt_l, &ColorLime, NULL);
#endif
  }
  if(max_damage > tdo->threadDamage)
    draw_defect(img, &ptf_draw, &ColorRed, 13, true);
  tdr->damage = (float)max_damage;

  img.copy_overlay();
  img.detach();

  return TRUE;
}

