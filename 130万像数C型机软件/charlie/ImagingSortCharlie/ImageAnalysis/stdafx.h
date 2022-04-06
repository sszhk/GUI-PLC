// stdafx.h : ��׼ϵͳ�����ļ��İ����ļ���
// ���Ǿ���ʹ�õ��������ĵ�
// �ض�����Ŀ�İ����ļ�
//

#pragma once

#include "targetver.h"
#define _CRT_SECURE_NO_WARNINGS
#define WIN32_LEAN_AND_MEAN             // �� Windows ͷ���ų�����ʹ�õ�����
// Windows ͷ�ļ�:
#include <windows.h>
#include "list"
#include "vector"
#include "iostream"
#include "sstream"
#include "tchar.h"

using namespace std;

#define RETURN_FALSE(x) if((x)==0) return FALSE
#define RETURN_TRUE(x) if((x)==0) return TRUE

#ifdef _DEBUG
#define DO_IF_DEBUG(x) x
#define DO_IF_RELEASE(x)
#define IF_DEBUG_ELSE(x,y) x
#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>
#else
#define DO_IF_DEBUG(x)
#define DO_IF_RELEASE(x) x
#define IF_DEBUG_ELSE(x,y) y
#endif

// extern enLog* plog;
// #define lg (*plog)
// #define WIDEN2(x) L ## x
// #define WIDEN(x) WIDEN2(x)
// 
// //#define IN_FUNCTION function_life __fl(lg, WIDEN(__FUNCTION__));
// #define IN_FUNCTION1(c) function_life __fl(lg, WIDEN(__FUNCTION__), CLASS(c));
// #define IN_FUNCTION