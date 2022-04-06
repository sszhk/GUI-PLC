using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagingSortCharlie.Data
{
  public class RingList<T>
  {
    private const int DEFAULT_SIZE = 10;
    private int capacity = DEFAULT_SIZE;
    private int count = 0;
    private const int RESET = -1;
    private int current = RESET;
    private T[] data;
    public RingList()
    {
      Realloc(capacity);
    }
    public RingList(int cnt)
    {
      Realloc(cnt);
    }
    public void Realloc(int cnt)
    {
      if (cnt == 0)
      {
        throw new Exception("Invalid size");
      }
      data = null;
      capacity = cnt;
      count = 0;
      data = new T[capacity];
      for (int i = 0; i < capacity; i++)
      {
        data[i] = default(T);
      }
      current = RESET;
    }
    public bool IsEmpty()
    {
      return count == 0;
    }
    public void Clear()
    {
      for (int i = 0; i < capacity; i++)
      {
        data[i] = default(T);
      }
      count = 0;
      current = RESET;
    }
    public void Push(T t)
    {
      current++;
      current %= capacity;
      count++;
      count = Math.Min(capacity, count);
      data[current] = default(T);
      data[current] = t;
    }
    public int Count
    {
      get 
      {
        return count;
      }
    }
    public T Current
    {
      get
      {
        return data[current];
      }
    }
    public int CurrentIndex
    {
      get
      {
        return current;
      }
    }
    public T GetItem(int idx)
    {
      if (IsEmpty())
        return default(T);
      while (idx < 0)
        idx += count;
      int i = (idx+current+1) % count;
      //i = count - 1 - i;
      return data[i];
    }
  }
}
