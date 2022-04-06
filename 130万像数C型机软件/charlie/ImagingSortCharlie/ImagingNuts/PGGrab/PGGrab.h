#pragma once

/*
PointGrey Dragonfly (PG) �� Coreco IFC (IFC) �Ƚϣ�
1��PG�ɼ�����ͼ����ȫ����������������Ҫ�ֹ���ͼ
2��PGֻ�д�����ʽ����̬��ʽ
*/

#include "windows.h"
typedef DWORD (WINAPI *PFNFRAMECALLBACK)(UINT cam, BYTE* buf, int width, int height);
DWORD WINAPI GrabInit(UINT cam, PFNFRAMECALLBACK callback);
void WINAPI GrabFree(DWORD handle);
// �л�������ʽ��triggerû�ã�����Ϊ0
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