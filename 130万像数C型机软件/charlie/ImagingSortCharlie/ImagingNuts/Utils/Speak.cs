using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetSpeech;
using System.Windows.Forms;
using ImagingSortCharlie.Data.Settings;

namespace ImagingSortCharlie.Utils
{
  public static class Speak
  {
    //     SVSFUnusedFlags = -512,
    //     SVSFDefault = 0,
    //     SVSFParseAutodetect = 0,
    //     SVSFlagsAsync = 1,
    //     SVSFPurgeBeforeSpeak = 2,
    //     SVSFIsFilename = 4,
    //     SVSFIsXML = 8,
    //     SVSFIsNotXML = 16,
    //     SVSFPersistXML = 32,
    //     SVSFNLPMask = 64,
    //     SVSFNLPSpeakPunc = 64,
    //     SVSFParseSapi = 128,
    //     SVSFParseSsml = 256,
    //     SVSFParseMask = 384,
    //     SVSFVoiceMask = 511,
    public static bool speak=false;
    public static SpVoice voice = new SpVoice();
    public delegate void WizardDelegate(string s);
    public static event WizardDelegate OnWizard;
    public static event EventHandler OnUpdateWizardButtons;

    public static void Init()
    {
      if (Settings.Lang == 2052)
        ReadInChinese();
      else
        ReadInEnglish();
    }
    public static void Read(string s)
    {
      if (Settings.Lang == 2052)
        ReadInChinese();
      else
        ReadInEnglish();
      ShutUp();
      SpeechVoiceSpeakFlags spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
      if (OnWizard != null)
      {
//         string show = "";
//         if (Settings.Lang == 2052)
//         {
//           char[] ch = s.ToCharArray();
//           for (int i = 0; i < s.Length; i++)
//           {
//             show += ch[i];
//             show += " ";
//           }
//         }
//         OnWizard(show);
        OnWizard(s);
      }
      if (!speak)
        return;
      try
      {
        voice.Speak(s, spFlags);
      }
      catch (System.Exception ex)
      {
        String error = ex.Message;
      }
    }

    //停下
    static void ShutUp()
    {
      if (!speak)
        return;
      SpeechVoiceSpeakFlags spFlags = SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak;
      try
      {
        voice.Speak(String.Empty, spFlags);
      }
      catch (System.Exception ex)
      {
        string error = ex.Message;
      }     
    }

    //语速
    static void SetRate(int rate)
    {
      //-10 ~ 10
      voice.Rate = rate;
    }

    //音量
    static void SetVolume(int volume)
    {
      //0 ~ 100
      voice.Volume = volume;
    }

    //换声
    static void SetVoice(string name, int idx)
    {
      string s = "name=" + name;
      try
      {
        voice.Voice = voice.GetVoices(s, "").Item(idx);
      }
      catch (System.Exception ex)
      {
        string error = ex.Message;
        //Utils.MB.Warning(GD.GetString("91011"));
        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
      }
    }

    public static int idx_wizard_front = 10000; 
    public static int idx_wizard_back = 1;
    public static int count_current = 9;

    static void ReadInEnglish()
    {
      SetVoice("VW Kate", 0);    
    }

    static void ReadInChinese()
    {
      SetVoice("ScanSoft Mei-Ling_Full_22kHz", 0);
    }
    public static void Wizard()
    {
      int n = idx_wizard_front + idx_wizard_back;
      string s = "r" + n.ToString();
      Read(GetString(s));
      if (OnUpdateWizardButtons != null)
      {
        OnUpdateWizardButtons(null, null);
      }
    }

    public static void Wizard(int iwf, int cc)
    {
      idx_wizard_front = iwf;
      idx_wizard_back = 1;
      count_current = cc;
      Wizard();
    }

    public static string GetString(string s)
    {
      return ImagingSortCharlie.Properties.Resources.ResourceManager.GetString(s + "_" + Settings.Lang.ToString());
    }

  }
}
