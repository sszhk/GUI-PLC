// DataStat.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
// #include "DataStat.h"
#include <math.h>
#include "set"
#include "map"
#include "vector"
#include "list"
#include "utility"
#include "auto_filter.h"

#define DATASTAT_API

void test_filter()
{
  auto_filter<int, float> filter;
  float x[] = {51, 52, 52, 
    54, 52, 42, 53, 55, 48, 54 };
  for(int i=0; i<10; i++)
  {
    filter.push_back(i, x[i]);
  }
  
  filter.filter(0.03);
}
// BOOL APIENTRY DllMain( HANDLE hModule, 
//                        DWORD  ul_reason_for_call, 
//                        LPVOID lpReserved
// 					 )
// {
//     switch (ul_reason_for_call)
// 	{
// 		case DLL_PROCESS_ATTACH:
// 		case DLL_THREAD_ATTACH:
// 		case DLL_THREAD_DETACH:
// 		case DLL_PROCESS_DETACH:
// 			break;
//     }
//     return TRUE;
// }

// DataStat.cpp : 定义 DLL 应用程序的导出函数。
//
// 
// typedef pair<float, float> PairType;
// typedef list<PairType> ListType;
// typedef list<float>::iterator FloatIT;
// 
//  
// DATASTAT_API 
// float GetAvr(float data[], int size)
// {
// 	float avr = 0;
// 	if( size <= 0 )
// 		return 0;
// 	for (int i=0; i<size; i++)
// 	{
// 		avr += data[i];
// 	}
// 	return avr / size;
// }
// 
//  
// DATASTAT_API 
// float GetStdVar(float data[], int size)
// {
// 	if( size <= 0 )
// 		return 0;
// 	float avr = GetAvr(data, size);
// 	float stdvar = 0;
// 	for (int i=0; i<size; i++)
// 	{
// 		float err = data[i] - avr;
// 		stdvar += err*err;
// 	}
// 	return stdvar / size;
// }

// float innerAvr(list<float>& data)
// {
//   float avr = 0;
//   for (FloatIT i = data.begin(); i!=data.end(); i++)
//   {
//     avr += *i;
//   }
//   avr /= data.size();
//   return avr;
// }
// 
// float innerStdVar(list<float>& data)
// {
// 	float avr = innerAvr(data);
// 	float std = 0;
// 	for (FloatIT i = data.begin(); i!=data.end(); i++)
// 	{
// 		float err = (*i - avr);
// 		std += err*err;
// 	}
// 	std /= data.size();
// 	return std;
// }
// // inline int comparePairType(PairType p1, PairType p2)
// // {
// // 	return (int)ceilf(p1.first - p2.first);
// // }
// static BOOL operator<(PairType p1, PairType p2)
// {
// 	return p1.first < p2.first;
// }
// static BOOL operator>(PairType p1, PairType p2)
// {
// 	return p1.first > p2.first;
// }
// static BOOL operator==(PairType p1, PairType p2)
// {
// 	return p1.first == p2.first;
// }
// 
// class IPass
// {
// public:
// 	virtual BOOL Pass(list<float>& set, float data, float* stdvar=NULL ) = 0;
// };
// 
// class PassByAbsStdVar: public IPass
// {
// public:
// 	virtual BOOL Pass(list<float>& set, float MIN_STD_VAR, float* stdvar)
// 	{
// 		float std = innerStdVar(set);
// 		if( stdvar )
// 			*stdvar = std;
// 		return std<=MIN_STD_VAR;
// 	}
// };
// 
// class PassByStdVarRatio: public IPass
// {
// public:
// 	virtual BOOL Pass(list<float>& set, float ratio, float* stdvar)
// 	{
// 		float std = innerStdVar(set);
// 		float avr = innerAvr(set);
// 		if( stdvar )
// 			*stdvar = std;
// 		if( avr <= 0.001 )
// 			return FALSE;
// 		return (std/avr) <= ratio;
// 	}
// };
// 
// IPass* PASS_CONITION = NULL;
// 
// BOOL innerFilter(list<float>& data, list<float>& removal, float condition)
// {
// 	if(!PASS_CONITION)
// 		return FALSE;
// 	if( data.size() < 3 )
// 		return PASS_CONITION->Pass(data, condition, NULL);
// 
// 	ListType stdlist;
// 	for (FloatIT i = data.begin(); i!=data.end(); i++)
// 	{
// 		list<float> copy(data);
// 		float value = *i;
// 		copy.remove(value);
// // 		float std = innerStdVar(copy);
// // 		if( std <= MIN_STD_VAR )
// // 		{
// // 			removal.push_back(value);
// // 			return TRUE;
// // 		}
// 		float std = 0;
// 		if(PASS_CONITION->Pass(copy, condition, &std))
// 		{
// 			removal.push_back(value);
// 			return TRUE;
// 		}
// 		stdlist.push_back(PairType(std, value));
// 	}
// 
// 	// 排序
// 	stdlist.sort();
// 
// 	// 尝试删除最小
// 	PairType firstElement = *stdlist.begin();
// 	removal.push_back(firstElement.second);
// 	data.remove(firstElement.second);
// // 	float std = innerStdVar(data);
// // 	if( std <= MIN_STD_VAR )
// // 		return TRUE;
// 	if( PASS_CONITION->Pass(data, condition) )
// 		return TRUE;
// 	return innerFilter(data, removal, condition);
// }
// 
// int innerFilterVariation(float data[], float removal[], int count, float condition)
// {
// 	list<float> lst;
// 	for (int i=0; i<count; i++)
// 	{
// 		lst.push_back(data[i]);
// 	}
// 	if( PASS_CONITION->Pass(lst, condition) )
// 		return 0;
// 	
// 	list<float> r;
// 	if( !innerFilter(lst, r, condition) )
// 		return -1;
// 	
// 	int idx = 0;
// 	for (FloatIT it = r.begin(); it!=r.end(); it++)
// 	{
// 		removal[idx++] = *it;
// 	}
// 	return r.size();
// }
// 
// 
// PassByStdVarRatio byRatio;
// void FilterByPercent()
// {
//   PASS_CONITION = &byRatio;
// }

// DATASTAT_API int FilterVariationByAbsStdVar(float data[], float removal[], int count, float MAX_STD_VAR)
// {
// // 	PassByAbsStdVar byAbsStdVar;
// // 	PASS_CONITION = &byAbsStdVar;
// // 	return innerFilterVariation(data, removal, count, MAX_STD_VAR);
//   return 0;
// }

 
// DATASTAT_API int FilterVariationByRatio(float data[], float removal[], int count, float RATIO)
// {
// // 	PassByStdVarRatio byRatio;
// // 	PASS_CONITION = &byRatio;
// // 	return innerFilterVariation(data, removal, count, RATIO);
// }
