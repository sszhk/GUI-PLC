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
struct DistanceReport
{
  float distance;
};
struct DistanceOptions
{
  BOOL angle_sensitive;
  BOOL inside;
  BOOL horizontal;
  Rect rc;
  BOOL ordinal;
};
#pragma pack(pop)


bool find_angle(ImagePtr img, const Rect* rc, RotatedRect* rr, 
                RakeDirection rake_direction, int threshold)
{
  if( !rc || !rr )
    return false;

  RakeOptions ro;
  ro.steepness = 2;
  ro.subpixelDivisions = 1;
  ro.subpixelType = IMAQ_QUADRATIC;
  ro.subsamplingRatio = 5;
  ro.threshold = threshold;
  ro.width = 4;
  ROIPtr roi = imaqCreateROI();
  RegisterDispose dis_roi(roi);
  if( !roi )
    return false;

  if(!imaqAddRectContour(roi, *rc))
    return false;

  RakeReport* report = imaqRake(img, roi, rake_direction, IMAQ_FIRST, &ro);
  RegisterDispose dis_rr(report);
  if( !report || !report->firstEdges)
  {
    debug_ni_err();
    SetMyError(L"ºÏ≤‚ ß∞‹");
    return false;
  }

  FitLineOptions flo = { 800, 1, 0 };
  BestLine* bl = imaqFitLine(report->firstEdges, report->numFirstEdges, &flo);
  RegisterDispose dis_bl(bl);
  if( !bl || !bl->valid )
  {
    debug_ni_err();
    SetMyError(L"ºÏ≤‚ ß∞‹");
    return false;
  }

  float dx = bl->end.x - bl->start.x;
  float dy = bl->end.y - bl->start.y;
  float angle = atan2f(dy, dx)*180/(float)M_PI;

  point center(rc->left+rc->width/2, rc->top+rc->height/2);
  point lt_corner(rc->left, rc->top);
  //lt_corner.rotate(center, angle);

  rr->left = ROUND_TO_INT(lt_corner.x);
  rr->top = ROUND_TO_INT(lt_corner.y);
  rr->angle = -angle;
  if( rake_direction == IMAQ_LEFT_TO_RIGHT || rake_direction == IMAQ_RIGHT_TO_LEFT )
    rr->angle += 90;
  rr->width = rc->width;
  rr->height = rc->height;

  return true;
}
void fill_color(int x, int y, int w, int h, ImagePtr img, const RGBValue* color)
{
  //   int width = 0, height = 0;
  //   imaqGetImageSize(img, &width, &height);

  for (int i=y; i<y+h; i++)
  {
    for (int j=x; j<x+w; j++)
    {
      Point pt = {j, i};
      PixelValue pv = { 255.0f };
      int result = imaqSetPixel(img, pt, pv);
      if( !result )
      {
        debug_ni_err();
      }
    }
  }
}

bool preprocess(sub_image& sub, ImagePtr src, 
                Rect& rc, const RGBValue* color,
                BOOL inside, BOOL horizontal
                )
{
  int l=0,t=0,r=0,b=0;
  int w = 0, h = 0;
  if( !imaqGetImageSize(src, &w, &h) )
    return false;
  r = l+w;
  b = t+h;

  bool doit = false;
  int dox=0, doy=0, dow=0, doh=0;

  //Rect boundary;
  if (normalize_rect(src, &rc, &rc))
    return false;

  const int THRES = 4;
  if( horizontal )
  {
    if(rc.left<THRES)                 // ◊Û±ﬂ‘ΩΩÁ
    {
      rc.left = 0;

      doit = true;
      dox = 0; doy=0;
      dow = 1; doh=rc.height;

    }
    else if(rc.left+rc.width>=r-THRES)  // ”“±ﬂ‘ΩΩÁ
    {
      rc.width = w-rc.left;

      doit = true;
      dox = rc.width-1; doy=0;
      dow = 1; doh = rc.height;

    }
  }
  else
  {
    if(rc.top<THRES)                  // …œ±ﬂ‘ΩΩÁ
    {
      rc.top = 0;

      doit = true;
      dox = 0; doy = 0;
      dow = rc.width; doh = 1;

    }
    else if(rc.top + rc.height>=b-THRES)// µ◊±ﬂ‘ΩΩÁ
    {
      rc.height = h - rc.top;

      doit = true;
      dox = 0, doy=rc.height-1;
      dow = rc.width; doh = 1;

    }
  }

  if( doit )
  {
    sub.get(src, &rc);
    fill_color(dox, doy, dow, doh, sub, color);
  }

  return doit;
}

BOOL init_distance_report(DistanceReport* distance_report)
{
  if (!distance_report)
    return FALSE;
  distance_report->distance = 0;
  return TRUE;
}

//#define ERROR_RETURN return -1;
IA_EXPORT BOOL IA_Calc_FindDistance(const PublicOptions* po, const DistanceOptions* dop, DistanceReport* dr)
{
  if (!po || !dop || !init_distance_report(dr))
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  int threshold = po->thres;
  bool displaystatus = po->isdisplaystatus;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);

  Rect rc = dop->rc;
  BOOL angle_sensitive = dop->angle_sensitive;
  BOOL horizontal = dop->horizontal;
  BOOL inside = dop->inside;

  RakeDirection rake_direction = IMAQ_LEFT_TO_RIGHT;
  if (horizontal && !dop->ordinal)
    rake_direction = IMAQ_RIGHT_TO_LEFT;
  else if(!horizontal && dop->ordinal)
    rake_direction = IMAQ_TOP_TO_BOTTOM;
  else if(!horizontal && !dop->ordinal)
    rake_direction = IMAQ_BOTTOM_TO_TOP;
  //   if( !GetToolRectangle(idx, id, &rc) )
  //     ERROR_RETURN;

  RotatedRect rr = imaqMakeRotatedRectFromRect(rc);
  if( angle_sensitive )
  {
    sub_image sub(img, &rc);
    RETURN_FALSE(!IsEmpty(sub));
    float angle = 0;
    if( !find_angle(img, &rc, &rr, rake_direction, threshold) )
      return FALSE;
  }

  sub_image sub;
  sub_image orig;
  if( preprocess(sub, img, rc, &ColorWhite, inside, horizontal) )
  {
    orig.get(img, &rc);
    sub.Commit();
    rr = imaqMakeRotatedRectFromRect(rc);
  }

  dr->distance = -1;
  //   PointFloat first, last;
  int result = 0;
  FindEdgeOptions feo;
  FindEdgeOptions* pfeo = &feo;
  pfeo->threshold = threshold;
  pfeo->steepness = 2;
  pfeo->width = 1;
  pfeo->subsamplingRatio = 5;
  pfeo->showEdgesFound = 
    pfeo->showSearchArea = 
    pfeo->showSearchLines = FALSE;
  pfeo->showResult = displaystatus;
  //MakeFindEdgeOptions(&feo, threshold, 2, 4, 5, displaystatus, FALSE);

  PointFloat first = imaqMakePointFloat(0, 0);
  PointFloat last = imaqMakePointFloat(0, 0);
  if( inside )
  {
    RETURN_FALSE(imaqClampMin(img, rr, rake_direction, 
      &dr->distance, pfeo, NULL, &first, &last));
  }
  else
    RETURN_FALSE(imaqClampMax(img, rr, rake_direction, 
    &dr->distance, pfeo, NULL, &first, &last));

#ifndef _DEBUG
  if( orig.operator ImagePtr() )
    orig.Commit();
#endif

  if(dr->distance == 0 )
  {
    SetMyError(L"CLAMP ß∞‹");
    //ERROR_RETURN;
    return FALSE;
  }
  dr->distance = fabsf(dr->distance);
  return TRUE;
}

