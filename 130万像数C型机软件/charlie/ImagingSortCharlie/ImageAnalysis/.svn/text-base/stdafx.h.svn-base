// stdafx.h : 标准系统包含文件的包含文件，
// 或是经常使用但不常更改的
// 特定于项目的包含文件
//

#pragma once

#include "targetver.h"
#define _CRT_SECURE_NO_WARNINGS
#define WIN32_LEAN_AND_MEAN             // 从 Windows 头中排除极少使用的资料
// Windows 头文件:
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