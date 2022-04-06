#ifndef _SUBIMAGE_H
#define _SUBIMAGE_H
#include "NICalc.h"

class sub_image
{
public:
  //sub_image();
  sub_image();
  sub_image(ImagePtr _src, const Rect* _rc);
  void get(ImagePtr _src, const Rect* _rc);

  virtual ~sub_image();
  operator ImagePtr()
  {
    return sub;
  }
  void Commit(ImagePtr to = NULL);
  int width() const
  {
    return rcSrc.width;
  }
  int height() const
  {
    return rcSrc.height;
  }
private:
  ImagePtr src;
  ImagePtr sub;
  Rect rcSrc;
  static const Point ptOrig00;
};


#endif