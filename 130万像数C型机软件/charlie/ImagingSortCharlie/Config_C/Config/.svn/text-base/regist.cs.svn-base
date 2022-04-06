using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace ConfigApp
{
  class regist
  {
    [DllImport("IMAGEANA.DLL")]
    public extern static void MakeNumber(ref int mn_1, ref int mn_2, ref int mn_3, ref int mn_4);

    public static string encrypt(string s, string key)
    {
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();
      //把字符串放到byte数组中  
      //原来使用的UTF8编码，我改成Unicode编码了，不行  
      byte[] inputByteArray = Encoding.Default.GetBytes(s);
      //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt); 

      //建立加密对象的密钥和偏移量  
      //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
      //使得输入密码必须输入英文文本  
      des.Key = ASCIIEncoding.ASCII.GetBytes(key);
      des.IV = ASCIIEncoding.ASCII.GetBytes(key);
      MemoryStream ms = new MemoryStream();
      CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
      //Write  the  byte  array  into  the  crypto  stream  
      //(It  will  end  up  in  the  memory  stream)  
      cs.Write(inputByteArray, 0, inputByteArray.Length);
      cs.FlushFinalBlock();
      //Get  the  data  back  from  the  memory  stream,  and  into  a  string  
      StringBuilder ret = new StringBuilder();
      foreach (byte b in ms.ToArray())
      {
        //Format  as  hex  
        ret.AppendFormat("{0:X2}", b);
      }
      ret.ToString();
      return ret.ToString();
    }

    public static string decrypt(string s, string key)
    {
      if (s.Length != 80)
        return "";
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();

      //Put  the  input  string  into  the  byte  array  
      byte[] inputByteArray = new byte[s.Length / 2];
      for (int x = 0; x < s.Length / 2; x++)
      {
        int i = (Convert.ToInt32(s.Substring(x * 2, 2), 16));
        inputByteArray[x] = (byte)i;
      }

      //建立加密对象的密钥和偏移量，此值重要，不能修改  
      des.Key = ASCIIEncoding.ASCII.GetBytes(key);
      des.IV = ASCIIEncoding.ASCII.GetBytes(key);
      MemoryStream ms = new MemoryStream();
      CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
      //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
      cs.Write(inputByteArray, 0, inputByteArray.Length);
      cs.FlushFinalBlock();

      //Get  the  decrypted  data  back  from  the  memory  stream  
      //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
      StringBuilder ret = new StringBuilder();

      return System.Text.Encoding.Default.GetString(ms.ToArray());
    }

    public static string make_machine_number()
    {
      const int PART = 4;
      int[] num = new int[PART] {0, 0, 0, 0 };
      MakeNumber(ref num[0], ref num[1], ref num[2], ref num[3]);
      string mn = null;
      for (int i = 0; i < PART; i++ )
      {
        mn += num[i].ToString();
        if (i == PART - 1)
          break;
        mn += "-";
      }
      return mn;
    }

  }
}
