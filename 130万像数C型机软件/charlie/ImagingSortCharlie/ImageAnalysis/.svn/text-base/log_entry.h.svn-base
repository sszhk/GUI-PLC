#ifndef _LOG_ENTRY_H
#define _LOG_ENTRY_H

#include "log/enLog2.h"

class log_entry
{
  const char* fn;
public:
  log_entry(const char* function_name): fn(function_name)
  {
    LOG_INFO("+%s", fn);
  }
  ~log_entry()
  {
    LOG_INFO("-%s", fn);
  }
};

#define ENTER_FUNCTION log_entry le##__FUNCTION__(__FUNCTION__);

#endif