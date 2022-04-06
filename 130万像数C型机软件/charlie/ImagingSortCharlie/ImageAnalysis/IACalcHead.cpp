#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "sub_image.h"
#include "auto_array.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "tools_ni.h"
#include "shadow_image.h"

#define IMGWIDTH 640

#pragma pack(push, 4)
struct HeadReport
{
  float width;
  float depth;
};
struct HeadOptions
{
  Rect rc;
};
#pragma pack(pop)

BOOL init_head_report(HeadReport *head_report)
{
  if (!head_report)
    return FALSE;
  head_report->depth = 0;
  head_report->width = 0;
  return TRUE;
}

IA_EXPORT BOOL IA_Calc_FindHead(const PublicOptions* po, const HeadOptions* ho, HeadReport* hr)
{
  ENTER_FUNCTION;

  if (!po || !ho || !init_head_report(hr))
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  bool displaystatus = po->isdisplaystatus;
  int threshold = po->thres;
  Rect rc = ho->rc;
  PointFloat first_width = imaqMakePointFloat(0, 0);
  PointFloat last_width = imaqMakePointFloat(0, 0);
  RotatedRect rrr = imaqMakeRotatedRectFromRect(rc);
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE; 
  shadow_image img(display);

  RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 1));
  RETURN_FALSE(imaqRejectBorder(img, img, TRUE));

  float angle = 0;
  PointFloat center = imaqMakePointFloat(0, 0);
  if (!locating(img, &angle, &center))
  {
    img.detach();
    return FALSE;
  }
  RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));

  hr->width = FindDistance(img, &rrr, FALSE, FALSE, threshold, &first_width, &last_width, displaystatus);
  hr->depth = FindDistance(img, &rrr, FALSE, TRUE, threshold, &first_width, &last_width, displaystatus);

//   ImagePtr img = GetImage(idx, DISPLAY);
//   if( IsEmpty(img) )
//   {
//     return FALSE;
//   }    
//   sub_image sub(img, &(rc));
//   float dis = IMGWIDTH;
// 
//   Curve* curves = NULL;
//   UINT numCurves = 0;
//   curves = imaqExtractCurves(sub, NULL, NULL, &numCurves);
//   RegisterDispose dis_curves(curves);
//   if( !numCurves || !curves)
//   {
//     return TRUE;
//   } 
//   int min_idx_last = 0;
//   int min_idx_first = 0;
//   for (UINT i = 0; i < numCurves; i++)
//   {
//     for (UINT j = 0; j < curves[i].numPoints; j++)
//     {
//       float d = curves[i].points[j].x;
//       if (dis > d)
//       {
//         dis = d;
//         min_idx_first = i;
//         min_idx_last = j;
//       }
//       //dis = min(dis, d);
//     }
//   } 
//   Point s = imaqMakePoint((int)curves[min_idx_first].points[min_idx_last].x + rrr.left, rrr.top);
//   Point e = imaqMakePoint((int)curves[min_idx_first].points[min_idx_last].x + rrr.left, rrr.top + rrr.height);
//   imaqOverlayLine(img, s, e, &ColorRed, NULL);
//   curves[min_idx_first].points[min_idx_last].x += rrr.left;
//   curves[min_idx_first].points[min_idx_last].y += rrr.top;
//   DrawSmallRect(img, curves[min_idx_first].points[min_idx_last], 5, &IMAQ_RGB_RED );
//   hr->depth = IMGWIDTH - dis;
//   if (hr->width != -1)
//     hr->width = fabsf(hr->width);

  return TRUE;
}

