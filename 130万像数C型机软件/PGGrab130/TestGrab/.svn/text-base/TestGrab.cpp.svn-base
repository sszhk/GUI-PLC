// TestGrab.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "conio.h"
#include "..\PGGrab130\pggrab.h"

int frames = 0;
DWORD WINAPI on_frame(UINT cam, BYTE* buf, int width, int height)
{
  printf("Got new image on camera #%d, width=%d, height=%d, frames=%d\n", cam, width, height, frames++);
  return 0;
}
void test_settings()
{
  DWORD handle = GrabInit(0, on_frame);
  BOOL result = GrabSaveSettings(handle, "settings.xml");
  result = GrabLoadSettings(handle, "settings.xml", false);
  GrabFree(handle);
}
void test();
int _tmain(int argc, _TCHAR* argv[])
{
  DWORD handle[4] = {0,0,0,0};
  printf("trying to initialize...\n");
  for (int i=0; i<4; i++)
  {
    handle[i] = GrabInit(i, on_frame);
  }
  for (int i=0; i<4; i++)
  {
    GrabFree(handle[i]);
  }
}

void test()
{
  DWORD handle[4] = {0,0,0,0};
  printf("trying to initialize...\n");
  for (int i=0; i<4; i++)
  {
    handle[i] = GrabInit(i, on_frame);
    if( !handle[i] )
    {
      printf("camera #%d init failed.\n", i);
      goto END;
    }
    else
    {
      printf("camera #%d init OK.\n", i);
    }
    if( !GrabSetTrigger(handle[i], 0) )
    {
      printf("camera #%d trigger set failure.\n", i);
      goto END;
    }
    if( !GrabStart(handle[i]))
    {
      printf("camera #%d start failure.\n", i);
      goto END;
    }
  }
  while(true)
  {
    int key = getch();
    if( key == 13 )
      break;
    switch(key)
    {
    case 32:
      GrabSnap(handle[0]);
      break;
    case '1':
      GrabLive(handle[0]);
      break;
    case '2':
      GrabSetTrigger(handle[0], 0);
      break;
    case 's':
    case 'S':
      GrabShowSetup(handle[0], GetDesktopWindow(), true);
      break;
    case 'h':
    case 'H':
      GrabShowSetup(handle[0], GetDesktopWindow(), false);
      break;
    }
  }
END:
  printf("trying to free everything... press enter\n");
  getchar();
  for (int i=0; i<4; i++)
  {
    GrabStop(handle[i]);
    GrabFree(handle[i]);
  }
  printf("over\n");
  getchar();
  //   if( !PGObj::total_init() )
  //   {
  //     printf("Init failed.\n");
  //     getchar();
  //     return 0;
  //   }
  //   else
  //   {
  //     printf("%d cameras found.\n", PGObj::total_cameras());
  //   }
  //   
  //   PGObj obj[4];
  //   for (int i=0; i<4; i++)
  //   {
  //     printf("initializing camera #%d\n", i);
  //     if( !obj[i].init(i, on_frame) )
  //     {
  //       printf("camera #%d init failed.\n", i);
  //       getchar();
  //       goto END;
  //     }
  //     if( !obj[i].mode_trigger() )
  //     {
  //       printf("camera does not support external trigger mode.\n");
  //     }
  //     if( !obj[i].start(15) )
  //     {
  //       printf("start capture failed.\n");
  //     }
  //   }
  //   printf("Blank: soft trigger\n");
  //   printf("1: live mode\n");
  //   printf("2: external trigger mode\n");
  //   printf("Enter: exit\n");
  // 
  //   int key = 0;
  //   while(true)
  //   {
  //     key = getch();
  //     if( key == 13 )
  //       break;
  //     switch(key)
  //     {
  //     case 32:
  //       obj[0].soft_trigger_once();
  //       break;
  //     case '1':
  //       obj[0].mode_live();
  //       break;
  //     case '2':
  //       obj[0].mode_trigger();
  //       break;
  //     }
  //   }
  // 
  // END:
  //   for(int i=0; i<4; i++)
  //     obj[i].stop();
  //   printf("Trying to free, press enter...\n");
  //   getchar();
  // 	return 0;
}
