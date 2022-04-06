#pragma once

#include "map"
#include "set"
#include "vector"
#include "utility"

#define INVALID_STEP_IDX -1
#define INVALID_DISTANCE FLT_MAX
#define INVALID_START start_type(INVALID_STEP_IDX, 0, 0, INVALID_DISTANCE)
#define INVALID_END end_type(INVALID_STEP_IDX, 0, 0, INVALID_DISTANCE)
//#define MIN_STEP_DISTANCE 3 // px

typedef std::set<float> yseries;
typedef std::map<int, yseries> step_point_map;
typedef std::pair<int, yseries> step_point_pairs;
typedef std::vector<float> distance_list_type;

class smaller_point_y
{
public:
  bool operator ()(const point& pt1, const point& pt2) {return pt1.y<pt2.y;}
};

typedef std::set<point, smaller_point_y> points_sorted_y;
typedef std::map<int, points_sorted_y> step_point_map_iii;
typedef step_point_map_iii::value_type step_point_pairs_iii;

class calc_steps_helper
{
public:
  struct step_edge
  {
    int idx;
    float y1;
    float y2;
    float distance;
    step_edge(): idx(INVALID_STEP_IDX), y1(0), y2(0), distance(INVALID_DISTANCE){}
    step_edge(int _idx, float _y1, float _y2, float _d) { idx=_idx;y1=_y1; y2=_y2;distance=_d; }
    step_edge(const step_edge& se) {*this = se;}
  };
  struct step_report
  {
//     float x1;
//     float x2;
//     float y1;
//     float y2;
    float width;
    float height;

    point x11;
    point x12;
    point x21;
    point x22;

    point y1;
    point y2;
//     bool is_gap;
  };
private:
  // <台阶起始索引，结束索引>
  typedef int start_idx;
  typedef int end_idx;
  typedef float distance_type;
  //typedef std::pair<start_idx, distance_type> start_type;
  //typedef std::pair<end_idx, distance_type> end_type;
  typedef step_edge start_type;
  typedef step_edge end_type;
  typedef std::pair<start_type, end_type> step_type;
  // typedef set<step_region> step_type;
  // <台阶序号，台阶索引对列表>
  typedef int step_idx;
  typedef std::map<step_idx, step_type> steps_type;
  typedef steps_type::value_type steps_element;
  typedef bool insert_succeeded;
  typedef std::pair<steps_type::iterator, insert_succeeded> steps_inserter;
  steps_type steps;
  step_idx current_step;

public:
  calc_steps_helper():current_step(0) {}
  size_t size() const {return steps.size();}
  bool get_step(int idx, step_edge& start, step_edge& end)
  {
//     steps_type::iterator i = steps.find(idx);
//     if(i == steps.end())
//       return false;
    if( idx < 0 || idx >= (int)size() )
      return false;
    int count = 0;
    steps_type::iterator i=steps.begin();
    for (;
      i!=steps.end(); i++, count++)
    {
      if( count == idx )
      {
        break;
      }
    }
    if( i == steps.end() )
      return false;

    start_type& startelem = i->second.first;
    end_type& endelem = i->second.second;
    start = startelem;
    end = endelem;
    float d1 = startelem.distance;
    float d2 = endelem.distance;
    return true;
  }
  void add_step_1(start_idx idx, float y1, float y2)
  {
    start_type start(idx, y1, y2, y2-y1);
    steps[current_step].first = start;
//     steps_inserter& in = steps.insert(steps_element(current_step, 
//       step_type(start, INVALID_END)));
//     if( !in.second )  // already exists
//     {
//       // it     step_type   start_type
//       in.first->second     .first     = start;
//     }
  }
  void add_step_2(end_idx idx, float y1, float y2)
  {
    end_type end(idx, y1, y2, y2-y1);
    steps[current_step].second = end;
//     steps_inserter& in = steps.insert(steps_element(current_step, 
//       step_type(INVALID_START, end)));
//     if( !in.second )  // already exists
//     {
//       // it     step_type   end_idx
//       in.first->second     .second     = end;
//     }
    current_step++;
  }
  void post_process(int MIN_STEP_DISTANCE)
  {
//     if( steps.size()>=2 )
//     {
//       step_type& first = steps.begin()->second;
//       step_type& last = steps.rbegin()->second;
//       if( first.first.idx == INVALID_STEP_IDX )
//       {
//         first.first.idx = 0;
//       }
//       if( last.second.idx == INVALID_STEP_IDX )
//       {
//         last.second.idx = ??;
//       }
//     }

    // 台阶最小距离筛选
    steps_type::iterator i = steps.begin();
    while(i!=steps.end())
    {
      step_type& elem = i->second;
      start_type& start = elem.first;
      end_type& end = elem.second;
      if( start.idx != INVALID_STEP_IDX &&
        end.idx != INVALID_STEP_IDX )
      {
        step_idx delta = end.idx - start.idx;
        if( delta < MIN_STEP_DISTANCE )
        {
#ifdef _DEBUG
          //cerr<<"erased steps with delta "<<delta<<endl;
#endif
          steps.erase(i);
          i = steps.begin();
          continue;
        }
      }
      i++;
    }
  }

  int make_report(
    const step_point_map& pp,
    int left_most_idx,
    step_report* report)
  {
    int count = 0;

    // 跳过第一个
    steps_type::iterator i=steps.begin();
    end_type& prev_edge = i->second.second;
    i++;
    
    for (;
      i!=steps.end();
      i++, count++)
    {
      steps_type::iterator nexti = i;
      nexti++;
      
      start_type& next_edge = (nexti==steps.end())?
                              i->second.second:
                              nexti->second.first;

      step_type& edge = i->second;
      start_type& start = edge.first;
      end_type& end = edge.second;

      int x1 = (start.idx+prev_edge.idx)/2+left_most_idx;
      int x2 = (end.idx+next_edge.idx)/2+left_most_idx;
      step_point_map::const_iterator finder = pp.find(x1);
      if( finder != pp.end() )
      {
        report[count].x11 = point((float)x1, *finder->second.begin());
        report[count].x12 = point((float)x1, *finder->second.rbegin());
      }
//       else
//       {
//         cerr<<"make_report error, unexpected x1."<<endl;
//       }
      finder = pp.find(x2);
      if( finder != pp.end() )
      {
        report[count].x21 = point((float)x2, *finder->second.begin());
        report[count].x22 = point((float)x2, *finder->second.rbegin());
      }
//       else
//       {
//         cerr<<"make_report error, unexpected x2."<<endl;
//       }
      int width = x2 - x1;
      report[count].width = (float)width;

      prev_edge = i->second.second;
      
      // 直径计算（上下最大夹距）
      float max_height = 0;
      float roof = -FLT_MAX, bottom = FLT_MAX;
      for (int i=start.idx; i<=end.idx; i++)
      {
        int x = i+left_most_idx;
        step_point_map::const_iterator it=pp.find(x);
        if( it == pp.end() )
          continue;
        float r = *it->second.rbegin();
        float b = *it->second.begin();
        if( roof < r )
        {
          roof = r;
          report[count].y1 = point((float)x, r);
        }
        if( bottom > b )
        {
          bottom = b;
          report[count].y2 = point((float)x, b);
        }
      }
      max_height = roof-bottom;
      report[count].height = max_height;
    }

    return count;
  }

};

