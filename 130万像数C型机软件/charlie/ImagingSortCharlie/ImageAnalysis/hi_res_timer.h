// hi_res_timer.h: interface for the hi_res_timer class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_HIRESTIMER_H__B777EF96_9BEC_4C52_83F5_819DF59E9AED__INCLUDED_)
#define AFX_HIRESTIMER_H__B777EF96_9BEC_4C52_83F5_819DF59E9AED__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class hi_res_timer  
{
private:
	LARGE_INTEGER startTime, stopTime;
	LARGE_INTEGER freq;

public:
	hi_res_timer()
	{
		startTime.QuadPart = 0;
		stopTime.QuadPart = 0;
		
		if (QueryPerformanceFrequency(&freq) == false)
		{
			// high-performance counter not supported
			
			//throw new Win32Exception();
		}
	}
	void Start()
	{
		// lets do the waiting threads there work
		
		//Sleep(0);
		
		QueryPerformanceCounter(&startTime);
		//System.Diagnostics.Debug.Print("Counting start...");
	}
	
	// Stop the timer
	
	void Stop()
	{
		QueryPerformanceCounter(&stopTime);
		//System.Diagnostics.Debug.Print("Counted {0} milliseconds.", Duration*1000);
	}
	
	// Returns the duration of the timer (in seconds)
	double GetMS() const
	{
		return (double)(stopTime.QuadPart - startTime.QuadPart)*1000 / (double)freq.QuadPart;
	}
// 	public double Duration
// 	{
// 		get
// 		{
// 			return (double)(stopTime - startTime) / (double)freq;
// 		}
//     }
};

class DbgTimeCount
{
#ifdef _DEBUG
  hi_res_timer timer;
  std::string word;
#endif
public:
  DbgTimeCount(const std::string s)
#ifdef _DEBUG
    :word(s) 
  {timer.Start();}
#else
  {

  }
#endif

#ifdef _DEBUG
  //~DbgTimeCount() {timer.Stop(); cerr<<word<<" costs: "<<timer.GetMS()<<" ms"<<endl;}
#endif
};

#endif // !defined(AFX_HIRESTIMER_H__B777EF96_9BEC_4C52_83F5_819DF59E9AED__INCLUDED_)
