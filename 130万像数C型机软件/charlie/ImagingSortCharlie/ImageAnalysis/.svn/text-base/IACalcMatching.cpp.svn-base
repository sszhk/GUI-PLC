#include "StdAfx.h"
#include "IAMain.h"
#include "NICalc.h"
#include "Utilities.h"
#include "NIDispose.h"
#include "IAFindShape.h"

#pragma pack(push, 4)
struct PatternReport
{
  float match;
};
struct PatternOptoins
{
  Annulus an;
};
#pragma pack(pop)

IA_EXPORT BOOL IA_Calc_Matching(const PublicOptions* po, const PatternOptoins* pat_opt, PatternReport* pat_rpt)
{
  if (!po || !pat_opt || !pat_rpt)
    return FALSE;

  ImagePtr img = GetImage(po->idx, DISPLAY);
  if (IsEmpty(img))
    return FALSE;

  int result = 0;
  ImagePtr ui_dst = NULL;
  ImagePtr scr = img;
  //ImagePtr tmp = NULL;
  ImagePtr tmp = GetImage(po->idx, DISPLAY);
  if (IsEmpty(img))
    return FALSE;
  ImagePtr ui_tmp = NULL;
  ui_dst = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_ui_dst(ui_dst);
//   tmp = imaqCreateImage(IMAQ_IMAGE_U8, 0);
//   RegisterDispose dis_tem(tmp);
  ui_tmp = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  RegisterDispose dis_ui_tem(ui_tmp);
  result = imaqUnwrapImage(ui_dst, scr, pat_opt->an, IMAQ_BASE_INSIDE, IMAQ_BILINEAR);

//   result = imaqReadFile(tmp, "Calibration\\1.png", NULL, NULL);
//   if (!tmp)
//     return FALSE;

  result = imaqUnwrapImage(ui_tmp, tmp, pat_opt->an, IMAQ_BASE_INSIDE, IMAQ_BILINEAR);

//   imaqWritePNGFile2(ui_tmp, "d:\\pic\\match\\ui_tmp.png", 750, NULL, FALSE);
//   imaqWritePNGFile2(ui_dst, "d:\\pic\\match\\ui_dst.png", 750, NULL, FALSE);

  //imaqWritePNGFile2(tmp, "d:\\tmp.png", 750, NULL, FALSE);
//   int w = 0;
//   int h = 0;
//   if( !imaqGetImageSize(ui_dst, &w, &h))
//     return FALSE;
//   byte *buffer = NULL;
//   Rect rc = imaqMakeRect(0, 0, h, w);
//   buffer = (byte*)imaqImageToArray(ui_dst, rc, NULL, NULL);
//   RegisterDispose dis_buffer(buffer);
// 
//   MatchPatternOptions mpo;
//   mpo.mode = IMAQ_MATCH_SHIFT_INVARIANT;
//   mpo.minContrast = 10 ;
//   mpo.subpixelAccuracy = FALSE ;
//   mpo.angleRanges = NULL;
//   mpo.numRanges = 0 ;
//   mpo.numMatchesRequested = 1 ;
//   mpo.matchFactor = 0 ;
//   mpo.minMatchScore = 800 ;
//   MatchPatternAdvancedOptions mpao;
//   mpao.subpixelIterations = 1000;
//   mpao.subpixelTolerance = 0;
//   mpao.initialMatchListLength = 0;
//   mpao.matchListReductionFactor = 5;
//   mpao.initialStepSize = 0;
//   mpao.searchStrategy = IMAQ_BALANCED;
//   mpao.intermediateAngularAccuracy = 0;

//   int num = 0;
// 
//   int l_dst =  imaqLearnPattern2(ui_dst, IMAQ_LEARN_ALL, NULL);
// 
//   PatternMatch *pm = imaqMatchPattern2(ui_tmp, ui_dst, NULL, NULL/*&mpao*/, IMAQ_NO_RECT, &num);
//   RegisterDispose dis_pm(pm);
//   char *err = imaqGetErrorText(GetLastError());

//   imaqThreshold(img, img, 0, 0, TRUE, 1);
//   mask_down(img, imaqMakePointFloat((float)pat_opt->an.center.x, (float)pat_opt->an.center.y), (float)pat_opt->an.outerRadius, false);
//   imaqFillHoles(img, img, TRUE);
//   imaqThreshold(img, img, 1, 1, TRUE, 255);

//   imaqThreshold(tmp, tmp, 0, 0, TRUE, 1);
//   mask_down(tmp, imaqMakePointFloat((float)pat_opt->an.center.x, (float)pat_opt->an.center.y), (float)pat_opt->an.outerRadius, false);
//   imaqFillHoles(tmp, tmp, TRUE);
//   imaqThreshold(tmp, tmp, 1, 1, TRUE, 255);

//   imaqWritePNGFile2(tmp, "d:\\pic\\match\\tmp.png", 750, NULL, FALSE);
//   imaqWritePNGFile2(img, "d:\\pic\\match\\img.png", 750, NULL, FALSE);

  //imaqLearnPattern2(tmp, IMAQ_LEARN_ALL, NULL);
//   PatternMatch *pm = imaqMatchPattern2(img, tmp, NULL, NULL, IMAQ_NO_RECT, NULL);
//   pat_rpt->match = pm->score;

  return TRUE;
}