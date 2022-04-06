#include "stdafx.h"
#include "IAMain.h"
#include "Utilities.h"
#include "IAFindShape.h"
#include "NIDispose.h"
#include "sub_image.h"

#define MAX_AREA 307200

#pragma pack(push, 4)
struct NodeReport
{
  int nodesCount;
  float maxNodeArea;
  float minNodeArea;   
};
struct NodeOptions
{
  RotatedRect rc;
  BOOL isWhite; 
  int Similarity;
  float correctionArea; 
  BOOL correction;
};
#pragma pack(pop)
extern BOOL remove_small(ImagePtr img);

BOOL init_node_report(NodeReport* node_report)
{
  if(!node_report)
    return FALSE;
  node_report->maxNodeArea = 0;
  node_report->minNodeArea = 0;
  node_report->nodesCount = 0;
  return TRUE;
}
IA_EXPORT BOOL IA_Calc_FindNode(const PublicOptions* po, NodeOptions *no, NodeReport *nr)
{
  ENTER_FUNCTION;

  if (!po || !no || !init_node_report(nr))
    return FALSE;
  ClearMyError();

  nr->nodesCount = 0;
  nr->maxNodeArea = 0;
  nr->minNodeArea = MAX_AREA;
  int idx = po->idx;
  bool displaystayus = po->isdisplaystatus;
  int thres = po->thres;
  ImagePtr img = GetImage(idx, DISPLAY);
  ImagePtr copy = save_image(idx, DISPLAY);
  RegisterDispose dis_copy(copy);
  if( IsEmpty(img) || IsEmpty(copy))
    return FALSE;
  int result = 0;

  if (!no->isWhite)
  {
    RETURN_FALSE(imaqRejectBorder(img, img, TRUE));
    RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));
  }
  
  RETURN_FALSE(imaqSetBorderSize(img, 1));
  RETURN_FALSE(imaqMorphology(img, img, IMAQ_ERODE, NULL));
//   RETURN_FALSE(imaqMorphology(img, img, IMAQ_ERODE, NULL));
  RETURN_FALSE(imaqSetBorderSize(img, 0));  
 
  double a = 0;
  double center_x = 0, center_y = 0;      
  int count = 0;
  //如果是校正
  if (no->correction)
  {
    sub_image sub(img, &imaqMakeRectFromRotatedRect(no->rc));
    if(IsEmpty(sub))
      return FALSE;
    RETURN_FALSE(imaqRejectBorder(sub, sub, TRUE));
    RETURN_FALSE(imaqCountParticles(sub, TRUE, &count));  
    no->correctionArea = 0;
    for (int  i = 0; i < count; i ++)
    {
       RETURN_FALSE(imaqMeasureParticle(sub, i, FALSE, IMAQ_MT_AREA, &a));
       no->correctionArea += (float)a;
    }
    if (no->correctionArea == 0)
      return FALSE;
    return TRUE;
  }

  int count_particle = 0;
  RETURN_FALSE(imaqCountParticles(img, TRUE, &count_particle));

  if (count_particle == 0)
  {
    RETURN_FALSE(imaqThreshold(img, img, 0, 0, TRUE, 255));
    RETURN_FALSE(restore_image(idx, DISPLAY, copy));
    return FALSE;
  }

  for (int  i = 0; i < count_particle; i ++)
  {   
    RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_AREA, &a));
    float similarity = 0;
    if (((float)a - no->correctionArea) > FLT_EPSILON)
      similarity = no->correctionArea / (float)a;
    else
      similarity = (float)a / no->correctionArea;

    if( similarity * 100 < no->Similarity)
      continue;

    nr->nodesCount ++;
    float fa = (float)a;
    if (fa > nr->maxNodeArea)
      nr->maxNodeArea = fa;
    if (fa < nr->minNodeArea)
      nr->minNodeArea =fa;

    double left_rr = 0;
    double top_rr = 0;
    double width_rr = 0;
    double height_rr = 0;

    RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_LEFT, &left_rr));
    RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_TOP, &top_rr));
    RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_WIDTH, &width_rr));
    RETURN_FALSE(imaqMeasureParticle(img, i, FALSE, IMAQ_MT_BOUNDING_RECT_HEIGHT, &height_rr));
    RETURN_FALSE(imaqOverlayRect(img, 
      imaqMakeRect((int)top_rr, (int)left_rr, (int)height_rr, (int)width_rr), 
      &ColorRed, IMAQ_DRAW_VALUE, NULL));
  }
  RETURN_FALSE(imaqThreshold(img, img, (float)no->isWhite, (float)no->isWhite, TRUE, 255));

  RETURN_FALSE(imaqCopyOverlay(copy, img, NULL));
  RETURN_FALSE(restore_image(idx, DISPLAY, copy));

  if (nr->maxNodeArea == 0 || nr->minNodeArea == MAX_AREA)
    return FALSE;
  return TRUE;
}