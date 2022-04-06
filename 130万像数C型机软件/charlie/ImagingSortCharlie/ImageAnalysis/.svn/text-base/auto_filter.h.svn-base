#ifndef _DATASTATTEMPLATE3_H
#define _DATASTATTEMPLATE3_H

#define SQUARE_D(x) ((x)*(x))

#include "map"
#include "vector"
#include "utility"
template <class Key, class Value, class List=vector<Key> >
class auto_filter
{
  typedef pair<Key, Value> pair_type;
  typedef vector<pair_type> list_type;
  typedef double stdvar_type;
  
  typedef Key list_type_it;
  typedef pair<stdvar_type, list_type_it> stdvar_pair_type;
  typedef map<stdvar_type, list_type_it> stdvar_if_removed;

  list_type the_list;
  List removed;

public:
  auto_filter()  { }
  void clear() {the_list.clear(); removed.clear();}
  void push_back(const Key k, const Value v) {the_list.push_back(pair_type(k,v));}
  int size() const {return the_list.size();}
  List get_removed() const {return removed;}

#define MIN_REASONABLE_ELEM_COUNT 3

  bool filter(stdvar_type tolerance)  // 0.2 = 20% (STDVAR/AVERAGE)
  {
    for (;;)
    {
      stdvar_if_removed stdvar_list;
      stdvar_type average = 0;
      stdvar_type stdvar = 0;

      average = calc_average(the_list);
      stdvar = calc_stdvar(the_list, average);
      if( passed(stdvar, average, tolerance) )
        return true;

      for (int i=0; i<(int)the_list.size(); i++)
      {
        pair_type& pt = the_list[i];
        list_type copy(the_list);
        copy.erase(copy.begin()+i);
        average = calc_average(copy);
        stdvar = calc_stdvar(copy, average);
        if( copy.size() < MIN_REASONABLE_ELEM_COUNT )
          return false;
        if( passed(stdvar, average, tolerance) )
        {
          removed.push_back(the_list[i].first);
          return true;
        }
        
        //stdvar_list.insert(stdvar_pair_type(stdvar, i));
        stdvar_list[stdvar] = i;
      }
      if( stdvar_list.empty() )
        return false;
      list_type_it front = stdvar_list.begin()->second;
      removed.push_back(the_list[front].first);
      the_list.erase(the_list.begin() + front);
    }
  }

private:
  static bool passed(stdvar_type stdvar, stdvar_type average, stdvar_type tolerance)
  {
    if( average == 0 )
      return true;
    return ((stdvar/average)<=tolerance);
  }
  static stdvar_type calc_average(const list_type& lst)
  {
    stdvar_type average = 0;
    if( lst.empty() )
      return 0;
    for (list_type::const_iterator i=lst.begin(); 
      i!=lst.end(); i++)
    {
      const pair_type& pt = *i;
      average += pt.second;
    }
    average /= lst.size();
    return average;
  }
  static stdvar_type calc_stdvar(const list_type& lst, stdvar_type average)
  {
    stdvar_type stdvar = 0;
    if( lst.empty() )
      return 0;
    for (list_type::const_iterator i=lst.begin(); 
      i!=lst.end(); i++)
    {
      const pair_type& pt = *i;
      //average += pt.second;
      stdvar += SQUARE_D(pt.second-average);
    }
    stdvar /= lst.size();
    return stdvar;
  }
};

// template<class Key, class Value>
// class AutoFilterII
// {
// public:
//   typedef int OrigIndex;
//   typedef pair<Key, Value> PairType;
//   typedef list<PairType> ListType;
//   typedef vector<OrigIndex> RemovedList;
//   typedef typename ListType::iterator ListIT;
//   
//   typedef float StdVarType;
//   //typedef pair<Key, OrigIndex> StdVarValue;
//   typedef PairType StdVarValue;
//   typedef pair<StdVarType, StdVarValue> StdVarPair;
//   typedef list<StdVarPair> StdVarList;
//   typedef typename StdVarList::iterator StdVarIT;
// 
//   static bool Greater(const StdVarPair& svp1,
//     const StdVarPair& svp2)
//   {
//     return svp1.first > svp2.first;
//   }
// 
//   AutoFilterII(): average(0), stdvar(0) {}
// 
//   void clear() {theList.clear();}
//   int size() const {return theList.size();}
//   PairType& operator[](int idx) { return theList[idx];}
//   void push_back(const PairType& pair) {theList.push_back(pair);}
// 
//   bool filter(float percentThres, RemovedList& removed)
//   {
//     StdVarList stdlist;
//     //int idx = 0;
//     for (ListIT i=theList.begin(); i!=theList.end(); i++/*, idx++*/)
//     {
//       ListType copy(theList);
//       PairType& p = *i;
//       Key key = (Key)p.first;
//       remove(copy, p);
//       float aver = get_average(copy);
//       float var = get_stdvar(copy, aver);
//       if( passed(aver, var, percentThres) )
//       {
//         removed.push_back(p.second);
//         return true;
//       }
//       int idx = p.second;
//       stdlist.push_back(StdVarPair(var, /*StdVarValue(key, idx)*/ p));
//     }
//     {
//       //DbgTimeCount dbg("sort");
//       stdlist.sort();
//     }
// 
//     while(true)
//     {
//       if( passed(percentThres) )
//         return true;
//       if( theList.size() < 3 )
//         return false;
// 
//       removed.push_back(stdlist.front().second.second);
//       remove(theList, stdlist.front().second);
//       stdlist.pop_front();
//     }
//   }
// 
// private:
//   static void remove(ListType& lst, PairType& p)
//   {
//     lst.remove(p);
// //     for (ListIT i=lst.begin(); i!=lst.end(); i++)
// //     {
// //       if( i->first == s )
// //       {
// //         lst.erase(i);
// //         return;
// //       }
// //     }
//   }
//   static float get_average(ListType& copy)
//   {
//     float average = 0;
//     if( copy.size() == 0 )
//     {
//       return average;
//     }
// 
//     for (ListIT i=copy.begin(); i!=copy.end(); i++)
//     {
//       const PairType& pair = *i;
//       average += pair.first;
//     }
//     average /= copy.size();
//     
//     return average;
//   }
//   static float get_stdvar(ListType& copy, float average)
//   {
//     float stdvar = 0;
//     if( copy.size() == 0 )
//     {
//       return stdvar;
//     }
// 
//     stdvar = 0;
//     for (ListIT i=copy.begin(); i!=copy.end(); i++)
//     {
//       const PairType& pair = *i;
//       stdvar += SQUARE_D(pair.first-average);
//     }
//     stdvar /= copy.size();
// 
//     return stdvar;
//   }
// 
//   bool passed(float avr, float stdvar, float tolerance)
//   {
//     float err = abs(stdvar / average);
// 
//     return err <= tolerance;
//   }
//   bool passed(float percentViber)
//   {
//     average = get_average(theList);
//     stdvar = get_stdvar(theList, average);
//     return passed(average, stdvar, percentViber);
//   }
// 
//   float average, stdvar;
//   ListType theList;
// };
// 
// // KeyType = float
// // ObjectType = MyClass
// // PairType = pair<float, MyClass>
// template<class KeyType, typename ObjectType, class ItemType=pair<float, ObjectType>,
// class InputType=vector<ObjectType> >
// class AutoFilter
// {
// public:
//   typedef list<ItemType> ListType;
//   typedef pair<float, float> StdPair;
//   typedef list<StdPair> StdVarList;
//   typedef typename ListType::iterator IT;
//   typedef typename ListType::const_iterator CIT;
// 
//   //typedef typename list<ObjectType> InputType;
//   typedef typename InputType::const_iterator CIT_Input;
// 
//   AutoFilter():average(-1),stdvar(-1) {}
//   AutoFilter(ObjectType input[], int count):average(-1),stdvar(-1)
//   {
//     Init(input, count);
//   }
// 
//   AutoFilter(const InputType& input):average(-1),stdvar(-1)
//   {
//     Init(input);
//   }
//   void Init(ObjectType input[], int count)
//   {
//     data.clear();
//     for (int i=0; i<count; i++)
//     {
//       data.push_back(ItemType((KeyType)input[i], input[i]));
//     }
//   }
//   void Init(const InputType& input)
//   {
//     data.clear();
//     for (CIT_Input i = input.begin(); i!=input.end(); i++)
//     {
//       data.push_back(ItemType((KeyType)*i, *i));
//     }
//   }
// 
//   void clear()
//   {
//     data.clear();
//   }
//   void push_back(const ObjectType& o)
//   {
//     data.push_back(ItemType((KeyType)o, o));
//   }
//   void merge_with(const AutoFilter<KeyType, ObjectType, ItemType, InputType>& lst)
//   {
//     for (CIT i=lst.data.begin(); i!=lst.data.end(); i++)
//     {
//       data.push_back(*i);
//     }
//   }
//   int size() const
//   {
//     return data.size();
//   }
// 
//   KeyType Average()
//   {
//     if( average == -1 )
//       average = Average(data);
//     return (KeyType)average;
//   }
//   KeyType StdVar()
//   {
//     if( average == -1 )
//       Average();
//     if( stdvar == -1 )
//       stdvar = StdVar(data, average);
//     return (KeyType)stdvar;
//   }
// 
//   static void Remove(ListType& data, float key)
//   {
//     for (CIT i=data.begin(); i!=data.end(); i++)
//     {
//       if( (*i).first == key )
//       {
//         data.erase(i);
//         break;
//       }
//     }
//   }
//   bool Filter(float percentViber)
//   {
//     //percentViber /= 100;
// 
//     while(true)
//     {
//       if( Passed(percentViber) )
//         return true;
//       if( data.size() < 3 )
//         return false;
// 
//       StdVarList stdlist;
//       for (CIT i=data.begin(); i!=data.end(); i++)
//       {
//         const ItemType& p = *i;
//         ListType copy(data);
//         float key = (float)p.first;
//         Remove(copy, key);
//         float aver = Average(copy);
//         float var = StdVar(copy, aver);
//         stdlist.push_back(StdPair(var, key));
//       }
//       stdlist.sort();
// 
//       Remove(data, stdlist.begin()->second);
//     }
//   }
//   void Commit(InputType& output)
//   {
//     typedef InputType::iterator IT_Input;
//     output.clear();
//     for (CIT i=data.begin(); i!=data.end(); i++)
//     {
//       output.push_back(i->second);
//     }
//   }
//   static float StdVar(const ListType& data, float avr)
//   {
//     //float avr = Average(data);
//     float std = 0;
//     for (CIT i = data.begin(); i!=data.end(); i++)
//     {
//       float err = ((*i).first - avr);
//       std += err*err;
//     }
//     std /= data.size();
//     return std;
//   }
//   static float Average(const ListType& data)
//   {
//     float avr = 0;
//     for (CIT i = data.begin(); i!=data.end(); i++)
//     {
//       avr += (float)(*i).first;
//     }
//     avr /= data.size();
//     return avr;
//   }
// 
//   //   ListType& operator->()
//   //   {
//   //     return data;
//   //   }
// private:
//   bool Passed(float percentViber)
//   {
//     average = Average(data);
//     stdvar = StdVar(data, average);
// 
//     float err = abs(stdvar / average);
// 
//     return err <= percentViber;
//   }
//   ListType data;
// 
//   float average;
//   float stdvar;
// };

#endif