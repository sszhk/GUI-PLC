#pragma once

#define VIEW1 0
#define VIEW2 1
#define VIEW3 2
#define VIEW4 3
#define VIEW5 4
#define VIEW6 5
#define VIEW7 6
#define VIEW8 7
#define VIEW_COUNT 8
#define BACK 0
#define DISPLAY 1
#define TYPE_COUNT 2

extern ImagePtr GetImage(int idx, int type);
extern BOOL IsEmpty(ImagePtr img);
extern void SetMyError(LPCTSTR err);
extern void ClearMyError();
extern void DrawSmallRect(ImagePtr img, PointFloat center, int size, const RGBValue* color = NULL);
