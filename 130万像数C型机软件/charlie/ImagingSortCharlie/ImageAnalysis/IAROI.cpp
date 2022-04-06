#include "stdafx.h"
#include "IAMain.h"
#include "nivision.h"
#include "nimachinevision.h"
#include "NICalc.h"
#include "NIROI.h"
#include "NIDispose.h"
#include "Colors.h"
#include "Utilities.h"
// 对ROI*做过任何更改后，必须调用imaqSetWindowROI更新界面

ROIPtr GetROI(int idx)
{
  return imaqGetWindowROI(idx);
}

IA_EXPORT ContourID IA_ROI_AddRotatedRectangle(int idx, const RotatedRect* rc,
                                               Rect* boundary)
{
  GetBoundingGeneral(rc, boundary);
  return AddTool(idx, rc);
}

IA_EXPORT BOOL IA_ROI_GetRotatedRectangle(int idx, ContourID id, RotatedRect* rc,
                                          Rect* boundary)
{
  GetBoundingGeneral(rc, boundary);
  return GetTool(idx, id, rc, IMAQ_ROTATED_RECT);
}

IA_EXPORT ContourID IA_ROI_AddAnnulus(int idx, const Annulus* an,
                                      Rect* boundary)
{
  GetBoundingGeneral(an, boundary);
  return AddTool(idx, an);
}

IA_EXPORT BOOL IA_ROI_GetAnnulus(int idx, ContourID id, Annulus* an,
                                 Rect* boundary)
{
  GetBoundingGeneral(an, boundary);
  return GetTool(idx, id, an, IMAQ_ANNULUS);
}

IA_EXPORT ContourID IA_ROI_AddRectangle(int idx, const Rect* rc,
                                        Rect* boundary)
{
  GetBoundingGeneral(rc, boundary);
  return AddTool(idx, rc);
}

IA_EXPORT BOOL IA_ROI_GetRectangle(int idx, ContourID id, Rect* rc,
                                   Rect* boundary)
{
  GetBoundingGeneral(rc, boundary);
  return GetTool(idx, id, rc, IMAQ_RECT);
}

IA_EXPORT BOOL IA_ROI_Remove(int idx, ContourID id)
{
  ROIPtr roi = GetROI(idx);
  if( !roi )
    return FALSE;
  RegisterDispose dispose_roi(roi);
  //IA_OverlayClear(idx);
  int result = imaqRemoveContour(roi, id);
  imaqSetWindowROI(idx, roi);

  IMAQ_ALL_CONTOURS;

  return result != 0;
}

IA_EXPORT void IA_ROI_SetColor(int idx, ContourID id, int color)
{
  RGBValue cl = ColorSteelBlue;
  cl.R = (color>>16)&0xFF;
  cl.G = (color>>8)&0xFF;
  cl.B = (color)&0xFF;

  ROIPtr roi = GetROI(idx);
  if( !roi )
    return;
  RegisterDispose dis_roi(roi);
  imaqSetContourColor(roi, id, &cl);

  imaqSetWindowROI(idx, roi);
}

IA_EXPORT BOOL IA_ROI_GetBoundary(int idx, ContourID id, Rect* rc)
{
  return GetBounding(idx, id, rc);

}
