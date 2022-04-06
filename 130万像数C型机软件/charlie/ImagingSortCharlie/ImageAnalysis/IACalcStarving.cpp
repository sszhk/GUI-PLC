#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "NIDispose.h"
#include "IAFindShape.h"
#define _USE_MATH_DEFINES
#include "math.h"
#include "point.h"
#include "shadow_image.h"
#include "tools_ni.h"

#pragma pack(push, 4)
struct StarvingReport
{
  float starving;
  int num_particle;
};
struct StarvingOptions
{
  Annulus ann;
  int max_starving;
  int min_starving;
  BOOL isWhite;
};
#pragma pack(pop)

BOOL init_starving_report(StarvingReport* starving_report)
{
  if(!starving_report)
    return FALSE;
  starving_report->starving = 9999.9f;
  return TRUE;
}
IA_EXPORT BOOL IA_Calc_FindStarving(const PublicOptions* po, 
                                    const StarvingOptions *so, 
                                    StarvingReport *sr)
{
  ENTER_FUNCTION;

  if (!po || !so || !init_starving_report(sr))
    return FALSE;
  ClearMyError();

  int idx = po->idx;
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(display) )
    return FALSE;
  shadow_image img(display);

  if(IsEmpty(img))
    return FALSE;

  if (!so->isWhite)
    RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));

  int count = 0;
  BOOL result = FALSE;
  result = find_shape(img, so->ann, count, FALSE);

  if (!result || count != 1)
  {
    sr->num_particle = count;
    double left = 0;
    double top = 0;
    double width = 0;
    double height = 0;
    for (int i = 0; i < count; i++)
    {
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_LEFT, &left));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_TOP, &top));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_WIDTH, &width));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_HEIGHT, &height));
      RETURN_FALSE(imaqOverlayRect(img, 
        imaqMakeRect((int)top, (int)left, (int)height, (int)width), 
        &ColorRed, IMAQ_DRAW_VALUE, NULL));
    }
    img.copy_overlay();
    img.detach();
    return FALSE;
  }

  RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));
  RETURN_FALSE(imaqRejectBorder(img, img, TRUE));
  RETURN_FALSE(remove_small(img, 1, TRUE));

  if (IsEmpty(img))
  {
    sr->starving = 0;
    img.detach();
    return TRUE;
  }
//   RETURN_FALSE(fill_circle(img, centroid, (float)so->ann.innerRadius, FALSE));

  double area = 0;
  sr->starving = 0;
  int idx_starving = 0;
  RETURN_FALSE(imaqCountParticles(img, TRUE, &count));
  if (count == 0)
  {
    sr->starving = 0;
    img.detach();
    return TRUE;
  }
  for (int  i = 0; i < count; i ++)
  {   
    RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
      IMAQ_MT_AREA, &area));
    float a = (float)area;
    if(a > so->min_starving && a < so->max_starving)
    {
      double left_rr = 0;
      double top_rr = 0;
      double width_rr = 0;
      double height_rr = 0;

      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_BOUNDING_RECT_LEFT, &left_rr));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_BOUNDING_RECT_TOP, &top_rr));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_BOUNDING_RECT_WIDTH, &width_rr));
      RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, 
        IMAQ_MT_BOUNDING_RECT_HEIGHT, &height_rr));
      RETURN_FALSE(imaqOverlayRect(img, 
        imaqMakeRect((int)top_rr, (int)left_rr, (int)height_rr, (int)width_rr), 
        &ColorRed, IMAQ_DRAW_VALUE, NULL));

    }
    if (sr->starving < a && a < so->max_starving)
    {
      sr->starving = a;
      idx_starving = i;
    }

//     if (sr->starving < a)
//     {
//       sr->starving = a;
//       idx_starving = i;
//     }
  } 

//   double left_rr = 0;
//   double top_rr = 0;
//   double width_rr = 0;
//   double height_rr = 0;
// 
//   RETURN_FALSE(imaqMeasureParticle(img, idx_starving, FALSE, 
//     IMAQ_MT_BOUNDING_RECT_LEFT, &left_rr));
//   RETURN_FALSE(imaqMeasureParticle(img, idx_starving, FALSE, 
//     IMAQ_MT_BOUNDING_RECT_TOP, &top_rr));
//   RETURN_FALSE(imaqMeasureParticle(img, idx_starving, FALSE, 
//     IMAQ_MT_BOUNDING_RECT_WIDTH, &width_rr));
//   RETURN_FALSE(imaqMeasureParticle(img, idx_starving, FALSE, 
//     IMAQ_MT_BOUNDING_RECT_HEIGHT, &height_rr));
//   RETURN_FALSE(imaqOverlayRect(img, 
//     imaqMakeRect((int)top_rr, (int)left_rr, (int)height_rr, (int)width_rr), 
//     &ColorRed, IMAQ_DRAW_VALUE, NULL));

  img.copy_overlay();
  img.detach();
  return TRUE;
}