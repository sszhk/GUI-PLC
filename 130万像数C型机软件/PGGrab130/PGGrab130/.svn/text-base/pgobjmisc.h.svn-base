
void PGObj::show_setup(HWND win, bool show)
{
  if( !gui_ctx )
    return;

  CameraGUIError gerror;
  BOOL visible;
  gerror = pgrcamguiGetSettingsWindowState(
    gui_ctx,
    &visible);
  if( gerror != PGRCAMGUI_OK )
    return;
  if( !!visible == !!show )
  {
    return;
  }
  gerror = pgrcamguiToggleSettingsWindowState(
    gui_ctx, win/*::GetDesktopWindow()*/ /*ŒﬁœÏ”¶*/);
  if( gerror != PGRCAMGUI_OK )
  {

  }
}

struct PG_CAM_PROPERTY
{
  PG_CAM_PROPERTY() 
  {
    property_name = "n/a";
    property_enum = 0;
    property_value = 0;
    property_unit = "n/a";
    property_unit_abbr = "n/a";
    property_on_off = false;
    property_auto = false;
    property_one_push = false;
    property_succeeded = false;
    property_present = false;
    property_value_min = 0;
    property_value_max = 0;
    property_value_default = 0;
    property_value_can_auto = false;
    property_value_can_manual = false;
  }
  //   PG_CAM_PROPERTY(const PG_CAM_PROPERTY& c)
  //   {
  //     *this = c;
  //   }
  //   PG_CAM_PROPERTY& operator=(const PG_CAM_PROPERTY& c)
  //   {
  //     property_name       = c.property_name;
  //     property_enum       = c.property_enum;
  //     property_value      = c.property_value;
  //     property_unit       = c.property_unit;
  //     property_on_off     = c.property_on_off;
  //     property_auto       = c.property_auto;
  //     property_one_push   = c.property_one_push;
  //     property_succeeded  = c.property_succeeded;
  // 
  //     return *this;
  //   }
  string    property_name;
  long      property_enum;
  float     property_value;
  string    property_unit;
  string    property_unit_abbr;
  bool      property_on_off;
  bool      property_auto;
  bool      property_one_push;
  bool      property_succeeded;
  bool      property_present;
  float     property_value_min;
  float     property_value_max;
  float     property_value_default;
  bool      property_value_can_auto;
  bool      property_value_can_manual;
};
// 

template<class T>
string format(const T& v)
{
  stringstream ss;
  ss<<v;
  return ss.str();
}

string format(bool v)
{
  return v?"true":"false";
}
template<class T>
T parse(const string& s)
{
  T t;
  stringstream ss(s);
  ss>>t;
  return t;
};
template<>
bool parse(const string& s)
{
  if( s == "true" )
    return true;
  return false;
}

struct PG_CAM_ENUMS
{
  string _name;
  FlyCaptureProperty _enum;
  string _unit;
};
#define TO_TEXT(x)  #x
#define NAME_AND_ENUM(x, y) {(TO_TEXT(x)), x, (y)}

void link(TiXmlElement* root, LPCSTR key, LPCSTR value)
{
  TiXmlElement* node = new TiXmlElement(key);
  node->LinkEndChild(new TiXmlText(value));
  root->LinkEndChild(node);
}

void save_xml(LPCSTR file, PG_CAM_PROPERTY* s, int count)
{
  TiXmlDocument doc;
  doc.LinkEndChild(new TiXmlDeclaration( "1.0", "utf-8", ""));
  TiXmlElement* root = new TiXmlElement("PG_CAM_PROPERTIES");
  doc.LinkEndChild(root);
  root->LinkEndChild(new TiXmlComment("Point Grey Camera Properties"));
  for (int i=0; i<count; i++)
  {
    TiXmlElement* e = new TiXmlElement("PG_CAM_PROPERTY");
    root->LinkEndChild(e);
    {
      link(e, "property_name"    , s[i].property_name.c_str());
      link(e, "property_enum"    , format(s[i].property_enum).c_str());
      link(e, "property_value"   , format(s[i].property_value).c_str());
      link(e, "property_unit"    , s[i].property_unit.c_str());
      link(e, "property_unit_abbr", s[i].property_unit_abbr.c_str());
      link(e, "property_on_off"  , format(s[i].property_on_off).c_str());
      link(e, "property_auto"    , format(s[i].property_auto).c_str());
      link(e, "property_one_push", format(s[i].property_one_push).c_str());     
      link(e, "property_succeeded", format(s[i].property_succeeded).c_str());  

      link(e, "property_present", format(s[i].property_present).c_str());  
      link(e, "property_value_min", format(s[i].property_value_min).c_str());  
      link(e, "property_value_max", format(s[i].property_value_max).c_str());  
      link(e, "property_value_default", format(s[i].property_value_default).c_str());  
      link(e, "property_value_can_auto", format(s[i].property_value_can_auto).c_str());  
      link(e, "property_value_can_manual", format(s[i].property_value_can_manual).c_str());  
    }
  }
  doc.SaveFile(file);
}

void load_xml(LPCSTR file, PG_CAM_PROPERTY*& s, int& count)
{
  count = 0;

  TiXmlDocument doc;
  if( !doc.LoadFile(file) )
    return;
  TiXmlHandle h(&doc);
  TiXmlHandle hsub(0);
  TiXmlElement* e = h.FirstChildElement().Element();
  string value = e->Value();
  if( value != "PG_CAM_PROPERTIES" )
    return;
  e = h.FirstChildElement().FirstChildElement("PG_CAM_PROPERTY").Element();
  for( ; e; e=e->NextSiblingElement())
  {
    value = e->Value();
    count++;
  }
  if( !count )
    return;
  s = new PG_CAM_PROPERTY[count];
  hsub = h.FirstChildElement().FirstChildElement("PG_CAM_PROPERTY");
  e = hsub.Element();
  for (int i=0; e && i<count; i++, e=e->NextSiblingElement())
  {
    hsub = TiXmlHandle(e);
    TiXmlElement* sub = hsub.FirstChildElement().Element();
    // property_name
    s[i].property_name = sub->GetText();
    sub = sub->NextSiblingElement();
    // property_enum
    s[i].property_enum = parse<long>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_value
    s[i].property_value = parse<float>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_unit
    s[i].property_unit = (sub->GetText());
    sub = sub->NextSiblingElement();
    // property_unit
    s[i].property_unit_abbr = (sub->GetText());
    sub = sub->NextSiblingElement();
    // property_on_off
    s[i].property_on_off = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_auto
    s[i].property_auto = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_one_push
    s[i].property_one_push = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_succeeded
    s[i].property_succeeded = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();

    // property_present;
    // property_value_min;
    // property_value_max;
    // property_value_default;
    // property_value_can_auto;
    // property_value_can_manual;

    // property_present
    s[i].property_present = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_value_min
    s[i].property_value_min = parse<float>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_value_max
    s[i].property_value_max = parse<float>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_value_default
    s[i].property_value_default = parse<float>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_value_can_auto
    s[i].property_value_can_auto = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();
    // property_value_can_manual
    s[i].property_value_can_manual = parse<bool>(sub->GetText());
    sub = sub->NextSiblingElement();
  }
}
typedef PGRFLYCAPTURE_API FlyCaptureError (PGRFLYCAPTURE_CALL_CONVEN
                                           *PFN_SETCAMERA)(
                                           FlyCaptureContext  context,
                                           FlyCaptureProperty cameraProperty,
                                           bool               bOnePush,
                                           bool               bOnOff,
                                           bool               bAuto,
                                           float              fValue );

bool PGObj::load_settings(LPCSTR file, bool apply_to_all)
{
  PFN_SETCAMERA fun = flycaptureSetCameraAbsPropertyEx;
  if( apply_to_all )
    fun = flycaptureSetCameraAbsPropertyBroadcastEx;

  if( !ctx )
    return false;
  PG_CAM_PROPERTY* cam_settings = NULL;
  int count = 0;
  load_xml(file, cam_settings, count);
  for (int i=0; i<count; i++)
  {
    if( !cam_settings[i].property_succeeded )
      continue;
    FlyCaptureError error = fun(ctx,
      (FlyCaptureProperty)cam_settings[i].property_enum, 
      cam_settings[i].property_one_push, 
      cam_settings[i].property_on_off, 
      cam_settings[i].property_auto, 
      cam_settings[i].property_value);
    if (error != FLYCAPTURE_OK )
    {
    }
  }
  delete[] cam_settings;

  return true;
}

bool PGObj::save_settings(LPCSTR file)
{
  if(!ctx)
    return false;
  //_T;
  const PG_CAM_ENUMS properties[] = {
    NAME_AND_ENUM(FLYCAPTURE_BRIGHTNESS, "%"),
    NAME_AND_ENUM(FLYCAPTURE_AUTO_EXPOSURE, "EV"),
    NAME_AND_ENUM(FLYCAPTURE_SHARPNESS, "none"),
    NAME_AND_ENUM(FLYCAPTURE_WHITE_BALANCE, "n/a"),
    NAME_AND_ENUM(FLYCAPTURE_HUE, "n/a"),
    NAME_AND_ENUM(FLYCAPTURE_SATURATION, "n/a"),
    NAME_AND_ENUM(FLYCAPTURE_GAMMA, "none"),
    NAME_AND_ENUM(FLYCAPTURE_IRIS, "none"),
    NAME_AND_ENUM(FLYCAPTURE_FOCUS, "n/a"),
    NAME_AND_ENUM(FLYCAPTURE_ZOOM, "n/a"),
    NAME_AND_ENUM(FLYCAPTURE_PAN, "none"),
    NAME_AND_ENUM(FLYCAPTURE_TILT, "none"),
    NAME_AND_ENUM(FLYCAPTURE_SHUTTER, "ms"),
    NAME_AND_ENUM(FLYCAPTURE_GAIN, "dB"),
    NAME_AND_ENUM(FLYCAPTURE_TRIGGER_DELAY, "n/a")
    //FLYCAPTURE_FRAME_RATE
  };
  int count = sizeof(properties)/sizeof(properties[0]);
  PG_CAM_PROPERTY* cam_settings = new PG_CAM_PROPERTY[count];
  FlyCaptureError error = FLYCAPTURE_OK;
  //char unit[256]={0}, unit_abbr[256]={0};
  const char *unit=NULL, *unit_abbr = NULL;

  for (int i=0; i<count; i++)
  {
    cam_settings[i].property_name = properties[i]._name;
    cam_settings[i].property_enum = (long)properties[i]._enum;
    error = flycaptureGetCameraAbsPropertyEx(ctx, 
      properties[i]._enum, 
      &cam_settings[i].property_one_push, 
      &cam_settings[i].property_on_off, 
      &cam_settings[i].property_auto, 
      &cam_settings[i].property_value);
    if (error != FLYCAPTURE_OK )
    {
      cam_settings[i].property_succeeded = false;
      continue;
    }
    error = flycaptureGetCameraPropertyRange(ctx,
      properties[i]._enum,
      &cam_settings[i].property_present,
      (long*)&cam_settings[i].property_value_min,
      (long*)&cam_settings[i].property_value_max,
      (long*)&cam_settings[i].property_value_default,
      &cam_settings[i].property_value_can_auto,
      &cam_settings[i].property_value_can_manual);
    if (error != FLYCAPTURE_OK )
    {
      cam_settings[i].property_succeeded = false;
      continue;
    }
    error = flycaptureGetCameraAbsPropertyRange(ctx,
      properties[i]._enum,
      &cam_settings[i].property_present,
      &cam_settings[i].property_value_min,
      &cam_settings[i].property_value_max,
      &unit,
      &unit_abbr);
    if (error != FLYCAPTURE_OK )
    {
      cam_settings[i].property_succeeded = false;
      continue;
    }
    if( unit[0] )
      cam_settings[i].property_unit = unit;

    if( unit_abbr[0] )
      cam_settings[i].property_unit_abbr = unit_abbr;


    cam_settings[i].property_succeeded = true;
  }

  bool ok = false;
  save_xml(file, cam_settings, count);

  delete[] cam_settings;

  return ok;
}
