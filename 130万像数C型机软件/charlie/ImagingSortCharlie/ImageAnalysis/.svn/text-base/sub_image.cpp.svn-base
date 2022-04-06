	#include "stdafx.h"
#include "IAMain.h"
#include "sub_image.h"
#include "NIDispose.h"

BOOL normalize_rect(ImagePtr img, const Rect* src, Rect* dst)
{
  ENTER_FUNCTION;

  if( IsEmpty(img) || !src || !dst)
    return FALSE;
  int left = src->left;
  int top= src->top;
  int width = src->width;
  int height = src->height;
  int right = left + src->width;
  int bottom = top + src->height;
  if( !imaqGetImageSize(img, &width, &height) )
    return FALSE;

  left = max(left, 0);
  top = max(top, 0);
  right = min(right, width);
  bottom = min(bottom, height);

  dst->left = left;
  dst->top = top;
  dst->width = right -left;
  dst->height = bottom -top;

  return TRUE;
}

const Point sub_image::ptOrig00 = imaqMakePoint(0,0);
// sub_image::sub_image(): src(NULL), sub(NULL)
// {
// 
// }
void sub_image::get(ImagePtr _src, const Rect* _rc)
{
  ENTER_FUNCTION;

  imaqDispose(sub);
  sub = NULL;
  if( !_rc )
    this->rcSrc = IMAQ_NO_RECT;
  else
    rcSrc = *_rc;

  if( IsEmpty(_src))
    return;

  src = _src;
  if( !normalize_rect(src, &rcSrc, &rcSrc) )
    return;

  sub = imaqCreateImage(IMAQ_IMAGE_U8, 0);
  if( !imaqSetImageSize(sub, rcSrc.width, rcSrc.height) )
  {
    imaqDispose(sub);
    sub = NULL;
    return;
  }
  imaqCopyRect(sub, src, rcSrc, ptOrig00);

}
sub_image::sub_image(): src(NULL), sub(NULL)
{

}
sub_image::sub_image(ImagePtr _src, const Rect* _rc):src(_src), rcSrc(*_rc), sub(NULL)
{
  get(_src, _rc);
}
sub_image::~sub_image()
{
  RegisterDispose dispose_sub(sub);
  sub = NULL;
}
void sub_image::Commit(ImagePtr to /* = NULL */)
{
  if( !sub || !src )
    return;
  if( to == NULL )
    to = src;
  int result = imaqCopyRect(to, sub, IMAQ_NO_RECT, imaqMakePoint(rcSrc.left, rcSrc.top));
}