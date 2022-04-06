#pragma once
#include "IAMain.h"

#pragma pack(push, 4)
struct FindShapeOptions
{
	Annulus ann;
	BOOL isWhite;
	BOOL DelTrench;
  BOOL isCrack;
  BOOL isMaxDiameter;
  BOOL isMinDiameter;
};
struct FindShapeReport
{
  float radius;
	PointFloat center;
  double roundness;
  float minDiameter;
  float maxDiameter;
  PointFloat minDiaPt1, minDiaPt2;
  PointFloat maxDiaPt1, maxDiaPt2;
  float crack;
  int num_particle;
};
#pragma pack(pop)

#define KERNEL_CRACK 6

BOOL find_shape(ImagePtr image,
					   const FindShapeOptions* so,
					   FindShapeReport* sr);

wstring mask_down(ImagePtr img, CPT& center, float radius, bool white_outside);

float SideCrack(ImagePtr img, Annulus an, int kernel);