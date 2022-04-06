#pragma once

#include "windows.h"
typedef DWORD (WINAPI *PFNFRAMECALLBACK)(UINT cam, BYTE* buf, int width, int height);
DWORD WINAPI GrabInit(UINT cam, PFNFRAMECALLBACK callback);
void WINAPI GrabFree(DWORD handle);
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
BOOL WINAPI GrabSaveSettings(DWORD handle, LPCSTR file);
BOOL WINAPI GrabLoadSettings(DWORD handle, LPCSTR file, BOOL apply_to_all);
int WINAPI GrabGetSerial(DWORD handle);