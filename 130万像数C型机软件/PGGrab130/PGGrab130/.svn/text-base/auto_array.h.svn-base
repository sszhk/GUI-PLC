#pragma once

template<class T>
class auto_array
{
public:
  auto_array(): array(NULL), _size(0)
  {

  }
  auto_array(int s): array(NULL), _size(0)
  {
    Alloc(s);
  }
  BOOL Alloc(int s)
  {
    if( !s )
      return FALSE;
    Free();
    _size = s;
    array = new T [_size];
    return array != NULL;
  }
  void Free()
  {
    delete[] array;
    array = NULL;
    _size = 0;
  }
  virtual ~auto_array()
  {
    Free();
  }
  int size() const {return _size;}
  operator T*()
  {
    return array;
  }
  operator const T*() const {return array;}
  BOOL Empty() const
  {
    return _size != 0;
  }

  void Copy(const T* from, int count)
  {
    if( Empty() )
      return;
    if( size < count )
      return;
    memcpy(array, from, count*sizeof(T));
  }
  T& operator[](int idx)
  {
    return array[idx];
  }
private:
  T* array;
  int _size;
};
