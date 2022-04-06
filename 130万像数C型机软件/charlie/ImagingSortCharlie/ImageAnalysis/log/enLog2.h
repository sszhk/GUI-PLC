#pragma once

// #ifdef DLL
//   #define ENLOG2_PORTS  __declspec(dllexport)
// #else
//   #ifdef STATIC
//     #define ENLOG2_PORTS  extern
//   #else
//     #define ENLOG2_PORTS  __declspec(dllimport)
//   #endif
// #endif
#define ENLOG2_PORTS  extern

#ifdef __cplusplus
extern "C" {
#endif

#define LV_ALL      LV_INFO
#define LV_INFO     0
#define LV_DEBUG    1
#define LV_WARNING  2
#define LV_ERROR    3
#define LV_FATAL    4
#define LV_LAST     LV_FATAL
#define LV_CLOSE    99

#ifndef BOOL
  typedef int BOOL;
#endif
ENLOG2_PORTS BOOL enlog2_init(const char* filename);
ENLOG2_PORTS void enlog2_finish();
ENLOG2_PORTS void enlog2_set_level(int level);
ENLOG2_PORTS int  enlog2_get_level();
ENLOG2_PORTS BOOL enlog2_should_print(int level);
//ENLOG2_PORTS void enlog2_log(int level, const char* fmt, ...);

ENLOG2_PORTS void LOG_INFO(const char* fmt, ...);
ENLOG2_PORTS void LOG_DEBUG(const char* fmt, ...);
ENLOG2_PORTS void LOG_WARNING(const char* fmt, ...);
ENLOG2_PORTS void LOG_ERROR(const char* fmt, ...);
ENLOG2_PORTS void LOG_FATAL(const char* fmt, ...);

#ifdef __cplusplus
};
#endif
