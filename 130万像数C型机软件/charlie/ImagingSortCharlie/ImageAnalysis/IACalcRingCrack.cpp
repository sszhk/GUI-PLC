#include "StdAfx.h"
#include "IAMain.h"
#include "NICalc.h"
#include "Utilities.h"
#include "NIDispose.h"
#include "PolygonNIFloat.h"

#pragma pack(push, 4)
struct RingCrackReport
{
  float area;
};
struct RingCrackOptoins
{
  Annulus an;
};
#pragma pack(pop)

void switch_point(PointFloat s_pt, PointFloat &d_pt, Annulus an, float w)
{
  float r = (float)an.outerRadius;
  float dx = cos((float)(s_pt.x * 2 * PIf / w)) * r;
  float dy = sin((float)(s_pt.x * 2 * PIf / w)) * r;
  d_pt.x = an.center.x + dx;
  d_pt.y = an.center.y + dy;
}

IA_EXPORT BOOL IA_Calc_RingCrack(const PublicOptions* po, const RingCrackOptoins* rco, RingCrackReport* rcr)
{
  if (!po || !rco || !rcr)
    return FALSE;

  ImagePtr img = GetImage(po->idx, DISPLAY);
  if (IsEmpty(img))
    return FALSE;

  int result = 0;
  ImagePtr dst = NULL;
  ImagePtr scr = img;
  dst = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_dst(dst);
  result = imaqUnwrapImage(dst, scr, rco->an, IMAQ_BASE_INSIDE, IMAQ_BILINEAR);
  int w = 0;
  int h = 0;
  if( !imaqGetImageSize(dst, &w, &h))
    return FALSE;
//   byte *buffer = NULL;
//   Rect rc = imaqMakeRect(0, 0, h, w);
//   buffer = (byte*)imaqImageToArray(dst, rc, NULL, NULL);
//   RegisterDispose dis_buffer(buffer);
  
  result = imaqThreshold(dst, dst, 0, 0, TRUE, 1);
  int num_part = 0;
  result = imaqCountParticles(dst, TRUE, &num_part);
  //result = imaqThreshold(dst, dst, 0, 0, TRUE, 255);

  if(!num_part)
    return FALSE;
  double center_x = 0;
  double center_y = 0;
  double area = 0;
  double max_area = 0;
  int max_idx = 0;
  for (int  i = 0; i < num_part; i++)
  {
    result = imaqMeasureParticle(dst, i, FALSE, 
      IMAQ_MT_PARTICLE_AND_HOLES_AREA, &area);
    if (area > max_area)
    {
      max_area = area;
      max_idx = i;
    }
  }
  result = imaqMeasureParticle(dst, max_idx, FALSE, 
    IMAQ_MT_CENTER_OF_MASS_X, &center_x);
  result = imaqMeasureParticle(dst, max_idx, FALSE, 
    IMAQ_MT_CENTER_OF_MASS_Y, &center_y);

  rcr->area = (float)max_area;
  PointFloat center_pt = imaqMakePointFloat((float)center_x, (float)center_y);
  PointFloat pt = imaqMakePointFloat(0, 0);
  switch_point(center_pt, pt, rco->an, (float)w);
  imaqOverlayLine(img, get_point(pt), rco->an.center, &ColorRed, NULL);

  return TRUE;
}