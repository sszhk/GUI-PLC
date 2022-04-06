#pragma once

/*
PointGrey Dragonfly (PG) 与 Coreco IFC (IFC) 比较：
1、PG采集到的图像，完全独立、分立，不需要手工分图
2、PG只有触发方式，或动态方式
*/

#include "windows.h"
typedef DWORD (WINAPI *PFNFRAMECALLBACK)(UINT cam, BYTE* buf, int width, int height);
DWORD WINAPI GrabInit(UINT cam, PFNFRAMECALLBACK callback);
void WINAPI GrabFree(DWORD handle);
// 切换触发方式，trigger没用，总是为0
BOOL WINAPI GrabSetTrigger(DWORD handle, int trigger);
int WINAPI GrabGetTrigger(DWORD handle);
int WINAPI GrabWidth(DWORD handle);
int WINAPI GrabHeight(DWORD handle);
int WINAPI GrabBpp(DWORD handle);
BOOL WINAPI GrabStart(DWORD handle);
BOOL WINAPI GrabStop(DWORD handle);
BOOL WINAPI GrabLive(DWORD handle);
BOOL WINAPI GrabSnap(DWORD handle);
void WINAPI GrabShowSetup(DWORD handle, HWND parent, bool show);