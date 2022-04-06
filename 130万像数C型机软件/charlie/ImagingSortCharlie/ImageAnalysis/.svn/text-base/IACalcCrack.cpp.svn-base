#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include <nivision.h>
#include "NIPoints.h"
#include "PolygonNIFloat.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "sub_image.h"
#include "time.h"
#include "IAFindShape.h"
#include "point.h"
#include "algorithm"
#include "polygon_moments.h"
#include "shadow_image.h"
#include "tools_ni.h"
#include "tools_geometry.h"

#define INVALID_COORD (-99999)
#define IS_INVALID_COORD(pt) (pt.x==INVALID_COORD)
#define IS_VALID_COORD(pt) (pt.x!=INVALID_COORD)
#define SQUARE_D(x) ((x)*(x))

#pragma pack(push, 4)
struct CrackOptions
{
  Annulus ann;
  float centercrack;
  BOOL is_white;
  BOOL is_center_crack;
};
struct CrackReport
{
  PointFloat center;
  float centercrack;
  float sidecrack;
};
#pragma pack(pop)

//处理高于XX亮度的点
void SpeckBits(byte *bmpdata, float mean_gray, int width, int height)
{
  ENTER_FUNCTION;

  if (bmpdata==NULL)
  {
    return;
  }
  //srand( (unsigned)time(NULL) );

  //   int bd = 0;
  //   for (int i = 0; i < width*height; i++)
  //   {
  //     bd += *(bmpdata+ i);
  //   }
  //   bd /= (width*height);
  for (int i = 0; i < width*height; i++)
  {
    if (*(bmpdata+ i) > (byte)mean_gray)
    {
      //byte rd = 100 + rand() % 20;
      *(bmpdata+i) = (byte)mean_gray;
    }
  }
}

//投影
float* Shadow(byte* bmpdata, byte mean_gray, int width,int height, int kernel)
{
  ENTER_FUNCTION;

  if (bmpdata==NULL)
  {
    return NULL;
  }
  float *shadow = new float[width];

  for (int i=0; i<width; i++)
  {
    //shadow[i] = 0;
    int temp=0;
    int sum = 0;
    for(int j=0; j<height; j++)
    {
      int d = bmpdata[i+j*width]-mean_gray;
      sum += bmpdata[i+j*width];
      int sq = d*d;
      if( d < 0 )
        sq = -sq;
      temp += sq;
      /*((int)bmpdata[i+j*width]-mean_gray);*/
      //*(bmpdata+i + j*width);
    }
    if (mean_gray ==0)
    {
      mean_gray = 1;
    }
    if (height == 0)
    {
      height = 1;
    }
    //*(shadow+i) = (float)temp / mean_gray;
    float ratio = (float)height * kernel / sum ;
    *(shadow+i) = (float)temp * ratio;
  }
  return shadow;
}

// 设置核
float* Kernel(byte kernel, float *shadow, int kernelcount)
{
  ENTER_FUNCTION;

  if (shadow==NULL)
  {
    return NULL;
  }
  float* kernellist = new float[kernelcount / kernel];
  for (int i = 0; i < kernelcount/kernel; i ++)
  {
    float temp = 0;
    for (int j = i*kernel; j < i*kernel+kernel; j++)
    {
      temp += *( shadow+j);
    }
    *(kernellist+i) = temp;
  }
  return kernellist;
}

// 查找符合一定要求的投影
float* Diff(float* ker,int kernelcount)
{
  ENTER_FUNCTION;

  if (ker==NULL)
  {
    return NULL;
  }
  float* diff = new float[kernelcount - 1];
  *diff = (float) *ker;
  for (int i = 1; i < kernelcount-1;i++ )
  {
    *(diff+i) = (float)(*(ker+i) - *(ker + i - 1));
  }
  *diff = (float)(*ker - *(ker + kernelcount - 1));
  return diff;
}

void ArrayToCircle(ImagePtr img, float* diff, int kernel,int w, Annulus an)
{
  ENTER_FUNCTION;

  if (!img || !diff)
    return ;

  float maxwave = -9999;
  float minwave = 9999;
  for (int i = 0; i < w/kernel -1; i++)
  {
    float d = *(diff + i);
    if (d > maxwave)
      maxwave = d;
    if (d < minwave)
      minwave = d;
  }
  float d = maxwave - minwave;
  d = an.outerRadius/d;

  //float r = *(diff + w/kernel - 2) + an.outerRadius;
  float r = (*(diff + w/kernel - 2) - minwave) * d;
  float dx = cos((float)((w / kernel - 2) * kernel * 2 * PIf / w)) * r;
  float dy = sin((float)((w / kernel - 2) * kernel * 2 * PIf / w)) * r;
  float dx1 = 0;
  float dy1 = 0;
  float dx2 = 0;
  float dy2 = 0;
  PointFloat pt = imaqMakePointFloat(an.center.x + dx,an.center.y + dy);
  for (int i=0; i<w/kernel-1; i++)
  {
    //r = *(diff+i) + an.outerRadius     /*(an.innerRadius+an.outerRadius)/2*/;
    r = (*(diff + i) - minwave) * d;
    dx = cos((float)(i*kernel*2*PIf/w))*r;
    dy = sin((float)(i*kernel*2*PIf/w))*r;
    imaqOverlayLine(img, get_point(pt), get_point(imaqMakePointFloat(an.center.x + dx,an.center.y + dy)), &ColorRed, NULL);
    pt = imaqMakePointFloat(an.center.x + dx,an.center.y + dy);
  }
}

//将点转到圆上，并显示出波动情况
float Judge(ImagePtr img, float* diff, int kernel,int w, 
            Annulus an, bool displaystatus ,float crack)
{
  ENTER_FUNCTION;

  if (!img || !diff)
  {
    return 0;
  }
  float maxwave = 0;
  int maxidx = 0;
  float r = *(diff + w/kernel - 2) + an.outerRadius;
  float dx = cos((float)((w / kernel - 2) * kernel * 2 * PIf / w)) * r;
  float dy = sin((float)((w / kernel - 2) * kernel * 2 * PIf / w)) * r;
  float dx1 = 0;
  float dy1 = 0;
  float dx2 = 0;
  float dy2 = 0;
  PointFloat pt = imaqMakePointFloat(an.center.x + dx,an.center.y + dy);
  for (int i=0; i<w/kernel-1; i++)
  {
    r = *(diff+i) + an.outerRadius     /*(an.innerRadius+an.outerRadius)/2*/;
    dx = cos((float)(i*kernel*2*PIf/w))*r;
    dy = sin((float)(i*kernel*2*PIf/w))*r;
    //     if (displaystatus)
    //     {
    //       imaqOverlayLine(img, get_point(pt), get_point(imaqMakePointFloat(an.center.x + dx,an.center.y + dy)), &ColorLime, NULL);
    //     }
    //最高头裂波形图尖值
    if (fabs(*(diff+i))>maxwave)
    {
      maxwave = fabs(*(diff+i));
      maxidx = i;
    }
    pt = imaqMakePointFloat(an.center.x + dx,an.center.y + dy);

    //DrawSmallRect(img,pt,2,&IMAQ_RGB_BLUE);
    //     if (fabs(*(diff+i))>crack)
    //     {
    //       dx1 = cos((float)(i*kernel*2*PIf/w))*an.outerRadius;
    //       dy1 = sin((float)(i*kernel*2*PIf/w))*an.outerRadius;
    //       dx2 = cos((float)(i*kernel*2*PIf/w))*an.innerRadius;
    //       dy2 = sin((float)(i*kernel*2*PIf/w))*an.innerRadius;
    //       PointFloat pt1 = imaqMakePointFloat(an.center.x + dx1, an.center.y + dy1);
    //       PointFloat pt2 = imaqMakePointFloat(an.center.x + dx2, an.center.y + dy2);
    //       if (displaystatus)
    //       {
    //         imaqOverlayLine(img, get_point(pt1), an.center/*get_point(pt2)*/, &ColorRed, NULL);
    //       }
    //     }
  }     /*(an.innerRadius+an.outerRadius)/2*/;
  dx = cos((float)(maxidx*kernel*2*PIf/w))*an.outerRadius;
  dy = sin((float)(maxidx*kernel*2*PIf/w))*an.outerRadius;
  PointFloat pt1 = imaqMakePointFloat(an.center.x + dx,an.center.y + dy);
  dx = cos((float)(maxidx*kernel*2*PIf/w))*an.innerRadius;
  dy = sin((float)(maxidx*kernel*2*PIf/w))*an.innerRadius;
  PointFloat pt2 = imaqMakePointFloat(an.center.x + dx,an.center.y + dy);

  imaqOverlayLine(img, get_point(pt1), get_point(pt2), &ColorRed, NULL);
  return maxwave;
}
#define USE_FIND_DIAMETER1

void draw_histogram(ImagePtr,float data[], 
                    int num, float left, 
                    float bottom,
                    float height, const RGBValue*);
extern BOOL remove_small(ImagePtr img);

BOOL init_crack_report(CrackReport* crack_report)
{
  if (!crack_report)
    return FALSE;
  crack_report->center = imaqMakePointFloat(0, 0);
  crack_report->centercrack = 9999.9f;
  crack_report->sidecrack = 9999.9f;
  return TRUE;
}
IA_EXPORT BOOL IA_Calc_FindCrack(const PublicOptions* po, const CrackOptions*co,  CrackReport*cr)
{
  ENTER_FUNCTION;

  if (!po || !co || !init_crack_report(cr))
    return FALSE;
  ClearMyError();

  if (co->is_center_crack == FALSE)
    return TRUE;
  int result = 0;
  int idx=po->idx;
  int thres=po->thres;
  bool displaystatus=po->isdisplaystatus;
  BOOL is_white = co->is_white;
  Annulus an = co->ann;

  ImagePtr back = save_image(idx, DISPLAY);
  RegisterDispose dis_back(back);
  if( IsEmpty(back))
    return FALSE;

  int count_partical = 0;
  if (find_shape(back, an, count_partical, FALSE))
  {
    PointFloat center_partical = imaqMakePointFloat(0, 0);
    RETURN_FALSE(imaqCentroid(back, &center_partical, NULL));
    an.center = imaqMakePointFromPointFloat(center_partical);
  }

  IA_CopyImage(idx);
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display))
    return FALSE;
  copy_overlay_from(back, idx, DISPLAY);
  shadow_image img(display);

  const byte kernel = 2;

  //BOOL report = SideCrack(img, an);
  //   FindShapeOptions fo;
  //   fo.ann = an;
  //   //fo.isWhite = dia_opt->isWhite;
  //   FindShapeReport fr;
  //   if (!find_shape(po, &fo, &fr))
  //   {
  // 	  restore_image(po->idx, DISPLAY, copy);
  // 	  return FALSE;
  //   }
  //   restore_image(po->idx, DISPLAY, copy);
  //   an.center = imaqMakePointFromPointFloat(fr.center);

  //   if( !GetToolAnnulus(idx, id, &an) )
  //     return NULL;

  ImagePtr dst = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  ImagePtr src = img; 
  RegisterDispose dis_dst(dst);
  RETURN_FALSE(imaqSetBorderSize(src, 1));
  RETURN_FALSE(imaqGrayMorphology(src, src, IMAQ_POPEN, NULL));
  RETURN_FALSE(imaqUnwrapImage(dst, src, an, IMAQ_BASE_INSIDE, 
    /*IMAQ_ZERO_ORDER*/IMAQ_BILINEAR));

  RETURN_FALSE(remove_small(dst));
  HistogramReport* hr = imaqHistogram(dst, 256, 0, 255, NULL);
  RegisterDispose dis_hr(hr);
  if( !hr )
    return FALSE;

  int w = 0;
  int h = 0;
  RETURN_FALSE(imaqGetImageSize(dst, &w, &h));
  byte* buffer = NULL;
  Rect rc = imaqMakeRect(0, 0, h, w);
  buffer = (byte*)imaqImageToArray(dst, rc, NULL, NULL);
  RegisterDispose dis_buffer(buffer);
  SpeckBits(buffer, hr->mean, w, h);
  float *shadow = Shadow(buffer, (byte)hr->mean, w, h, kernel);
  //ArrayToCircle(img, shadow, 1, w, an);
  float *kernellist = Kernel(kernel, shadow, w);
  float *diff = Diff(kernellist, w/kernel);
  cr->centercrack = Judge(img, diff, kernel, w, an, displaystatus, co->centercrack );

  //draw_histogram(img, shadow, w, 10, 480, 200, &ColorRed);

  delete []shadow;
  delete []kernellist;
  delete []diff;
  //Enlighten(img, pt, &ColorRed); 
  //   return TRUE;
  return TRUE;
}
void draw_histogram(ImagePtr img, 
                    float data[], int num, 
                    float left, float bottom, 
                    float height,
                    const RGBValue* cl)
{
  auto_array<float> d(num);
  memcpy(d, data, sizeof(float)*num);
  float max = -FLT_MAX;
  float min = FLT_MAX;
  for (int i=0; i<num; i++)
  {
    if( max < d[i] )
      max = d[i];
    if( min > d[i] )
      min = d[i];
  }
  float dur = max - min;
  if( fabsf(dur) < FLT_EPSILON )
    return;

  for (int i=0; i<num; i++)
  {
    // 比例
    d[i] = (d[i]-min)/dur;

    point pt1(i+left, bottom);
    point pt2(i+left, bottom-d[i]*height);
    draw_line(img, pt1.ptf(), pt2.ptf(), 0, 0, cl);
  }

}



