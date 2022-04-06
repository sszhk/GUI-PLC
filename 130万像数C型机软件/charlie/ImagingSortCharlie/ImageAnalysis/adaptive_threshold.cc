#include "stdafx.h"
#include "IAMain.h"

// #define WIDTH_IMAGE  640
// #define HEIGHT_IMAGE 480
#define S (WIDTH_IMAGE/8)
#define T (0.15f)

void adaptiveThreshold(unsigned char* input, unsigned char* bin)
{
  unsigned long* integralImg = 0;
  int i, j;
  long sum=0;
  int count=0;
  int index;
  int x1, y1, x2, y2;
  int s2 = S/2;

  // create the integral image
  integralImg = (unsigned long*)malloc(WIDTH_IMAGE*HEIGHT_IMAGE*sizeof(unsigned long));

  for (i=0; i<WIDTH_IMAGE; i++)
  {
    // reset this column sum
    sum = 0;

    for (j=0; j<HEIGHT_IMAGE; j++)
    {
      index = j*WIDTH_IMAGE+i;

      sum += input[index];
      if (i==0)
        integralImg[index] = sum;
      else
        integralImg[index] = integralImg[index-1] + sum;
    }
  }

  // perform thresholding
  for (i=0; i<WIDTH_IMAGE; i++)
  {
    for (j=0; j<HEIGHT_IMAGE; j++)
    {
      index = j*WIDTH_IMAGE+i;

      // set the SxS region
      x1=i-s2; x2=i+s2;
      y1=j-s2; y2=j+s2;

      // check the border
      if (x1 < 0) x1 = 0;
      if (x2 >= WIDTH_IMAGE) x2 = WIDTH_IMAGE-1;
      if (y1 < 0) y1 = 0;
      if (y2 >= HEIGHT_IMAGE) y2 = HEIGHT_IMAGE-1;

      count = (x2-x1)*(y2-y1);

      // I(x,y)=s(x2,y2)-s(x1,y2)-s(x2,y1)+s(x1,x1)
      sum = integralImg[y2*WIDTH_IMAGE+x2] -
        integralImg[y1*WIDTH_IMAGE+x2] -
        integralImg[y2*WIDTH_IMAGE+x1] +
        integralImg[y1*WIDTH_IMAGE+x1];

      if ((long)(input[index]*count) < (long)(sum*(1.0-T)))
        bin[index] = 0;
      else
        bin[index] = 255;
    }
  }

  free (integralImg);
}