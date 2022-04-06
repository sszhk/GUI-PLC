#include "stdafx.h"
#include "IAMain.h"
#include "sub_image.h"
#include "NIDispose.h"

#pragma pack(push, 4)
struct BinarizeOptions
{
  BOOL inverse;
};
#pragma pack(pop)

// <=thres = 0
// > thres = 255


IA_EXPORT void IA_Process_BinarizeManual(const PublicOptions* po, 
                                         const Rect* mask, 
                                         const BinarizeOptions* bo, 
                                         bool isfullscreen)
{
  if( !bo || !po || !mask )
    return;
  int result = 0;
  int idx = po->idx;
  ImagePtr back = GetImage(idx, BACK);
  ImagePtr img = GetImage(idx, DISPLAY);
  if( IsEmpty(back) || IsEmpty(img))
    return ;

  BOOL useNewValue = FALSE;
  float newValue = 1;

  StructuringElement se;
  int pKernel1[9] = {1,1,1,1,1,1,1,1,1};
  se.matrixCols = 3;
  se.matrixRows = 3;
  se.hexa = FALSE;
  se.kernel = pKernel1;

  if (mask && !isfullscreen )
  {
    sub_image sub(img, mask);
    if( !sub )
      return;

    imaqThreshold(sub, sub, (float)po->thres, 255, TRUE, 1);

    if(bo->inverse)
      imaqThreshold(sub, sub, 0, 0, TRUE, 1);
    // convert to fake binary so that we can see
    imaqThreshold(sub, sub, 1, 1, TRUE, 255);
    sub.Commit();
  }
  else
  {
    imaqThreshold(img, img, (float)po->thres, 255, TRUE, 1);
    if (bo->inverse)
      imaqThreshold(img, img, 0, 0, TRUE, 1);
    // convert to fake binary so that we can see
    imaqThreshold(img, img, 1, 1, TRUE, 255);
  }

}

IA_EXPORT int IA_Process_BinarizeAuto(int idx, Rect* mask1, bool isfullscreen)
{
  int result = 0;
  ImagePtr back = GetImage(idx, BACK);
  ImagePtr display = GetImage(idx, DISPLAY);
  if( IsEmpty(back) || IsEmpty(display))
    return -1;

  ThresholdData* report = NULL;
  //imaqAutoThreshold2(imgDst, imgDst, 2, IMAQ_THRESH_INTERCLASS, NULL);
  if( mask1 && !isfullscreen)
  {
    sub_image sub(back, mask1);
    if( !sub )
      return -1;
    report = imaqAutoThreshold2(sub, sub, 2, IMAQ_THRESH_INTERCLASS, NULL);
    imaqThreshold(sub, sub, 1, 1, TRUE, 255);
    sub.Commit(display);
  }
  else
  {
    report = imaqAutoThreshold2(display, display, 2, IMAQ_THRESH_INTERCLASS, NULL);
    imaqThreshold(display, display, 1, 1, TRUE, 255);
  }
  RegisterDispose dispose(report);
  if( !report )
    return -1;

  return (int)report->rangeMax;

}

IA_EXPORT void IA_Process_RemoveNoise(int idx, Rect* mask1)
{
  ImagePtr img = GetImage(idx, DISPLAY);
  if( IsEmpty(img) )
    return;

  Rect rc = IMAQ_NO_RECT;
  if( !mask1 )
    mask1 = &rc;

  sub_image sub(img, mask1);
  StructuringElement se;
  int result = 0;
  int pKernel[9] = {1,1,1,1,1,1,1,1,1};
  se.matrixCols = 3;
  se.matrixRows = 3;
  se.hexa = 0;
  se.kernel = pKernel;
  result = imaqSetBorderSize(sub, 1);
  result = imaqGrayMorphology(sub, sub, IMAQ_CLOSE, &se);
  result = imaqSetBorderSize(sub, 0);
  //   char* err = imaqGetErrorText(imaqGetLastError());

  sub.Commit();
  //imaqGrayMorphology(image, image, IMAQ_CLOSE, &structElem);
}


