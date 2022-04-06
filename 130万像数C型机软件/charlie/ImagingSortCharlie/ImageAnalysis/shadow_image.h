#ifndef _SHADOW_IMAGE_H_
#define _SHADOW_IMAGE_H_
#include "basic_types.h"
//#include "NIDispose.h"
class shadow_image
{
  ImagePtr target;
  ImagePtr shadow;
public:
  shadow_image(ImagePtr t)
  {
    target = t;
    shadow = imaqCreateImage(IMAQ_IMAGE_U8, 0);
    imaqDuplicate(shadow, t);
  }
  ~shadow_image()
  {
    if( shadow )
    {
      if( target )
        imaqDuplicate(target, shadow);
      imaqDispose(shadow);
    }
  }
  void detach()
  {
    imaqDispose(shadow);
    shadow = NULL;
  }
  BOOL copy_overlay()
  {
    return imaqCopyOverlay(target, shadow, NULL);
  }
  operator ImagePtr()
  {
    return shadow;
  }
};

#endif // SHADOW_IMAGE_H_