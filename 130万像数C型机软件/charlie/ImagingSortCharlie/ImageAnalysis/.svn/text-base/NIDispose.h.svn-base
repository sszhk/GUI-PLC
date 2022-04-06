#pragma once

template<class T>
class RegDelete
{
public:
  RegDelete(T o): object(o)
  {

  }
  virtual ~RegDelete()
  {
    delete object;
    object = NULL;
  }

private:
  T object;
};

template<class T>
class RegDeleteArray
{
public:
  RegDeleteArray(T o): object(o)
  {

  }
  virtual ~RegDeleteArray()
  {
    delete[] object;
    object = NULL;
  }

private:
  T object;
};

template<class T>
class RegisterDisposeBase
{
public:
  typedef int (__stdcall *PFN)(T obj);
  RegisterDisposeBase(T p, PFN op): object(NULL)
  {
    object = p;
    operation = op;
  }
  RegisterDisposeBase(PFN op): object(NULL), operation(op){}
  void Dispose()
  {
    operation(object);
    object = NULL;
  }
  void Register(T p)
  {
    Dispose();
    object = p;
  }
  void Detach()
  {
    object = NULL;
  }
  virtual ~RegisterDisposeBase()
  {
    Dispose();
  }
private:
  T object;
  PFN operation;
};

class RegisterDispose: public RegisterDisposeBase<void*>
{
public:
  RegisterDispose(void *p): RegisterDisposeBase(p, imaqDispose)  {}
  RegisterDispose(): RegisterDisposeBase(imaqDispose)  {}
  RegisterDispose(StraightEdgeReport* p): RegisterDisposeBase(imaqDispose) 
  {/*cerr<<"IMAGEANA.DLL: Use RegisterDisposeSER instead!"<<endl;*/ throw "Wrong dispose";}
  RegisterDispose(CircularEdgeReport* p): RegisterDisposeBase(imaqDispose) 
  {/*cerr<<"IMAGEANA.DLL: Use RegisterDisposeCER instead!"<<endl; */throw "Wrong dispose";}
};

class RegisterDisposeSER: public RegisterDisposeBase<StraightEdgeReport*>
{
public:
  RegisterDisposeSER(StraightEdgeReport* p): RegisterDisposeBase(p, imaqDisposeStraightEdgeReport)  {}
};

class RegisterDisposeCER: public RegisterDisposeBase<CircularEdgeReport*>
{
public:
  RegisterDisposeCER(): RegisterDisposeBase(imaqDisposeCircularEdgeReport){}
  RegisterDisposeCER(CircularEdgeReport* p): RegisterDisposeBase(p, imaqDisposeCircularEdgeReport){}
};

struct _IplImage;
typedef _IplImage IplImage;
extern "C" void cvReleaseImage( IplImage** image );

class dispose_ipl
{
  IplImage*& shadow;
public:
  dispose_ipl(IplImage*& ipl):shadow(ipl)
  {
  }
  ~dispose_ipl()
  {
    if( shadow )
      cvReleaseImage(&shadow);
  }
};