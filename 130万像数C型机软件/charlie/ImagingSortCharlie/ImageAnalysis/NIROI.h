#pragma once

extern ROIPtr GetROI(int idx);
template<class T>
static BOOL GetTool(int idx, ContourID id, T* object, ContourType type)
{
  ROIPtr roi = GetROI(idx);
  if( !roi )
    return 0;
  RegisterDispose dispose_roi(roi);
  int count = imaqGetContourCount(roi);
  ContourInfo2* ci = imaqGetContourInfo2(roi, id);
  if( !ci )
    return FALSE;
  RegisterDispose dispose_ci(ci);
  if( ci->type == type )
  {
    memcpy(object, (void*)ci->structure.annulus, sizeof(T));
  }
  else
    return FALSE;

  return TRUE;
}

#define GetToolRotatedRectangle(idx, id, object) GetTool(idx, id, object, IMAQ_ROTATED_RECT)
#define GetToolRectangle(idx, id, object) GetTool(idx, id, object, IMAQ_RECT)
#define GetToolAnnulus(idx, id, object) GetTool(idx, id, object, IMAQ_ANNULUS)


template<class T>
class AddImaqTool
{
public:
  typedef ContourID (__stdcall *PFN)(ROIPtr, T object);
  AddImaqTool(PFN o)
  {
    operation = o;
  }
  ContourID operator()(ROIPtr roi, const T* object)
  {
    return operation(roi, *object);
  }
private:
  PFN operation;
};


template<class T>
static void* GetPFN() { return imaqAddRotatedRectContour2; }

template<>
static void* GetPFN<RotatedRect*>() { return imaqAddRotatedRectContour2; }

template<>
static void* GetPFN<Annulus*>() { return imaqAddAnnulusContour; }

template<>
static void* GetPFN<Rect*>() { return imaqAddRectContour; }

template<class T>
ContourID AddTool(ROIPtr roi, const T* object)
{
  if( !roi )
    return 0;
  typedef AddImaqTool<T> ThisType;
  typedef AddImaqTool<T>::PFN ThisTypePFN;
  ThisTypePFN pfn = (ThisTypePFN)GetPFN<T*>();
  ThisType addTool(pfn);

  ContourID id = addTool(roi, object);

  return id;
}

template<class T>
ContourID AddTool(int idx, const T* object)
{
  ROIPtr roi = GetROI(idx);
  if( !roi )
    return 0;
  ContourID id = AddTool(roi, object);
  imaqSetWindowROI(idx, roi);

  return id;
}

template<class T>
BOOL GetBoundingGeneral(const T* tool, Rect* bounding)
{
  ROIPtr roi = imaqCreateROI();
  RegisterDispose dis_roi(roi);
  if( !roi )
    return FALSE;

  if( 0 == AddTool(roi, tool) )
  {
    return FALSE;
  }
  int result = imaqGetROIBoundingBox(roi, bounding);
  if( !result )
    return FALSE;

  return TRUE;
}