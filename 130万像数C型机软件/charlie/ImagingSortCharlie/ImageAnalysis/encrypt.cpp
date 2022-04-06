#include <stdio.h>
#include <string.h>
#include "stdafx.h"
#include "IAMain.h"
#include "nb30.h"
#include "objbase.h"
#include "math.h"
using namespace std; 

// #define ALLOCATE_FROM_PROCESS_HEAP( bytes )		::HeapAlloc( ::GetProcessHeap(), HEAP_ZERO_MEMORY, bytes )
// #define DEALLOCATE_FROM_PROCESS_HEAP( ptr )		if( ptr ) ::HeapFree( ::GetProcessHeap(), 0, ptr )
// #define REALLOC_FROM_PROCESS_HEAP( ptr, bytes )	::HeapReAlloc( ::GetProcessHeap(), HEAP_ZERO_MEMORY, ptr, bytes )

#define  KEY 2487

#define F(x, y, z) (((x) & (y)) | ((~x) & (z)))
#define G(x, y, z) (((x) & (z)) | ((y) & (~z)))
#define H(x, y, z) ((x) ^ (y) ^ (z))
#define I(x, y, z) ((y) ^ ((x) | (~z)))

#define ROTATE_LEFT(x, n) (((x) << (n)) | ((x) >> (32-(n))))

#define FF(a, b, c, d, x, s, ac) { \
  (a) += F ((b), (c), (d)) + (x) + (UINT4)(ac); \
  (a) = ROTATE_LEFT ((a), (s)); \
  (a) += (b); \
}
#define GG(a, b, c, d, x, s, ac) { \
  (a) += G ((b), (c), (d)) + (x) + (UINT4)(ac); \
  (a) = ROTATE_LEFT ((a), (s)); \
  (a) += (b); \
}
#define HH(a, b, c, d, x, s, ac) { \
  (a) += H ((b), (c), (d)) + (x) + (UINT4)(ac); \
  (a) = ROTATE_LEFT ((a), (s)); \
  (a) += (b); \
}
#define II(a, b, c, d, x, s, ac) { \
  (a) += I ((b), (c), (d)) + (x) + (UINT4)(ac); \
  (a) = ROTATE_LEFT ((a), (s)); \
  (a) += (b); \
}

#ifndef PROTOTYPES
#define PROTOTYPES 1
#endif

#if PROTOTYPES
#define PROTO_LIST(list) list
#else
#define PROTO_LIST(list) ()
#endif

typedef unsigned char *POINTER; 
typedef unsigned short int UINT2;
typedef unsigned long int UINT4; 


#define MD 5
#define TEST_BLOCK_LEN 1000
#define TEST_BLOCK_COUNT 1000

#define S11 7
#define S12 12
#define S13 17
#define S14 22
#define S21 5
#define S22 9
#define S23 14
#define S24 20
#define S31 4
#define S32 11
#define S33 16
#define S34 23
#define S41 6
#define S42 10
#define S43 15
#define S44 21

typedef struct {
  UINT4 state[4];
  UINT4 count[2];
  unsigned char buffer[64];
} MD5_CTX;

static unsigned char PADDING[64] = {
  0x80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

static void Encode (unsigned char *output,UINT4 *input,unsigned int len)
{
  unsigned int i, j;

  for (i = 0, j = 0; j < len; i++, j += 4)
  {
    output[j] = (unsigned char)(input[i] & 0xff);
    output[j+1] = (unsigned char)((input[i] >> 8) & 0xff);
    output[j+2] = (unsigned char)((input[i] >> 16) & 0xff);
    output[j+3] = (unsigned char)((input[i] >> 24) & 0xff);
  }
}

static void Decode (UINT4 *output,unsigned char *input,unsigned int len) 
{
  unsigned int i, j;

  for (i = 0, j = 0; j < len; i++, j += 4)
    output[i] = ((UINT4)input[j]) | (((UINT4)input[j+1]) << 8) |
    (((UINT4)input[j+2]) << 16) | (((UINT4)input[j+3]) << 24);
}

static void md5_memcpy (POINTER output,POINTER input,unsigned int len)
{
  unsigned int i;

  for (i = 0; i < len; i++)
    output[i] = input[i];
}

static void md5_memset (POINTER output,int value,unsigned int len)
{
  unsigned int i;

  for (i = 0; i < len; i++)
    ((char *)output)[i] = (char)value;
}

void md5_init (MD5_CTX *context) /* context */
{
  context->count[0] = context->count[1] = 0;
  /* Load magic initialization constants. */
  context->state[0] = 0x67452301;
  context->state[1] = 0xefcdab89;
  context->state[2] = 0x98badcfe;
  context->state[3] = 0x10325476;
}

static void md5_transform (UINT4 state[4], unsigned char block[64])
{
  int i=0;
  UINT4 a = state[0], b = state[1], c = state[2], d = state[3], x[16];
  Decode (x, block, 64);
  FF (a, b, c, d, x[ 0], S11, 0xd76aa478); /* 1 */
  FF (d, a, b, c, x[ 1], S12, 0xe8c7b756); /* 2 */
  FF (c, d, a, b, x[ 2], S13, 0x242070db); /* 3 */
  FF (b, c, d, a, x[ 3], S14, 0xc1bdceee); /* 4 */
  FF (a, b, c, d, x[ 4], S11, 0xf57c0faf); /* 5 */
  FF (d, a, b, c, x[ 5], S12, 0x4787c62a); /* 6 */
  FF (c, d, a, b, x[ 6], S13, 0xa8304613); /* 7 */
  FF (b, c, d, a, x[ 7], S14, 0xfd469501); /* 8 */
  FF (a, b, c, d, x[ 8], S11, 0x698098d8); /* 9 */
  FF (d, a, b, c, x[ 9], S12, 0x8b44f7af); /* 10 */
  FF (c, d, a, b, x[10], S13, 0xffff5bb1); /* 11 */
  FF (b, c, d, a, x[11], S14, 0x895cd7be); /* 12 */
  FF (a, b, c, d, x[12], S11, 0x6b901122); /* 13 */
  FF (d, a, b, c, x[13], S12, 0xfd987193); /* 14 */
  FF (c, d, a, b, x[14], S13, 0xa679438e); /* 15 */
  FF (b, c, d, a, x[15], S14, 0x49b40821); /* 16 */

  GG (a, b, c, d, x[ 1], S21, 0xf61e2562); /* 17 */
  GG (d, a, b, c, x[ 6], S22, 0xc040b340); /* 18 */
  GG (c, d, a, b, x[11], S23, 0x265e5a51); /* 19 */
  GG (b, c, d, a, x[ 0], S24, 0xe9b6c7aa); /* 20 */
  GG (a, b, c, d, x[ 5], S21, 0xd62f105d); /* 21 */
  GG (d, a, b, c, x[10], S22, 0x2441453); /* 22 */
  GG (c, d, a, b, x[15], S23, 0xd8a1e681); /* 23 */
  GG (b, c, d, a, x[ 4], S24, 0xe7d3fbc8); /* 24 */
  GG (a, b, c, d, x[ 9], S21, 0x21e1cde6); /* 25 */
  GG (d, a, b, c, x[14], S22, 0xc33707d6); /* 26 */
  GG (c, d, a, b, x[ 3], S23, 0xf4d50d87); /* 27 */
  GG (b, c, d, a, x[ 8], S24, 0x455a14ed); /* 28 */
  GG (a, b, c, d, x[13], S21, 0xa9e3e905); /* 29 */
  GG (d, a, b, c, x[ 2], S22, 0xfcefa3f8); /* 30 */
  GG (c, d, a, b, x[ 7], S23, 0x676f02d9); /* 31 */
  GG (b, c, d, a, x[12], S24, 0x8d2a4c8a); /* 32 */

  HH (a, b, c, d, x[ 5], S31, 0xfffa3942); /* 33 */
  HH (d, a, b, c, x[ 8], S32, 0x8771f681); /* 34 */
  HH (c, d, a, b, x[11], S33, 0x6d9d6122); /* 35 */
  HH (b, c, d, a, x[14], S34, 0xfde5380c); /* 36 */
  HH (a, b, c, d, x[ 1], S31, 0xa4beea44); /* 37 */
  HH (d, a, b, c, x[ 4], S32, 0x4bdecfa9); /* 38 */
  HH (c, d, a, b, x[ 7], S33, 0xf6bb4b60); /* 39 */
  HH (b, c, d, a, x[10], S34, 0xbebfbc70); /* 40 */
  HH (a, b, c, d, x[13], S31, 0x289b7ec6); /* 41 */
  HH (d, a, b, c, x[ 0], S32, 0xeaa127fa); /* 42 */
  HH (c, d, a, b, x[ 3], S33, 0xd4ef3085); /* 43 */
  HH (b, c, d, a, x[ 6], S34, 0x4881d05); /* 44 */
  HH (a, b, c, d, x[ 9], S31, 0xd9d4d039); /* 45 */
  HH (d, a, b, c, x[12], S32, 0xe6db99e5); /* 46 */
  HH (c, d, a, b, x[15], S33, 0x1fa27cf8); /* 47 */
  HH (b, c, d, a, x[ 2], S34, 0xc4ac5665); /* 48 */

  II (a, b, c, d, x[ 0], S41, 0xf4292244); /* 49 */
  II (d, a, b, c, x[ 7], S42, 0x432aff97); /* 50 */
  II (c, d, a, b, x[14], S43, 0xab9423a7); /* 51 */
  II (b, c, d, a, x[ 5], S44, 0xfc93a039); /* 52 */
  II (a, b, c, d, x[12], S41, 0x655b59c3); /* 53 */
  II (d, a, b, c, x[ 3], S42, 0x8f0ccc92); /* 54 */
  II (c, d, a, b, x[10], S43, 0xffeff47d); /* 55 */
  II (b, c, d, a, x[ 1], S44, 0x85845dd1); /* 56 */
  II (a, b, c, d, x[ 8], S41, 0x6fa87e4f); /* 57 */
  II (d, a, b, c, x[15], S42, 0xfe2ce6e0); /* 58 */
  II (c, d, a, b, x[ 6], S43, 0xa3014314); /* 59 */
  II (b, c, d, a, x[13], S44, 0x4e0811a1); /* 60 */
  II (a, b, c, d, x[ 4], S41, 0xf7537e82); /* 61 */
  II (d, a, b, c, x[11], S42, 0xbd3af235); /* 62 */
  II (c, d, a, b, x[ 2], S43, 0x2ad7d2bb); /* 63 */
  II (b, c, d, a, x[ 9], S44, 0xeb86d391); /* 64 */

  state[0] += a;
  state[1] += b;
  state[2] += c;
  state[3] += d;

  md5_memset ((POINTER)x, 0, sizeof (x));
}

void md5_update (MD5_CTX *context, unsigned char *input, unsigned int inputLen)
{
  unsigned int i, index, partLen;
  index = (unsigned int)((context->count[0] >> 3) & 0x3F);

  if ((context->count[0] += ((UINT4)inputLen << 3)) < ((UINT4)inputLen << 3))
    context->count[1]++;
  context->count[1] += ((UINT4)inputLen >> 29);

  partLen = 64 - index;
  if (inputLen >= partLen)
  {
    md5_memcpy ((POINTER)&context->buffer[index], (POINTER)input, partLen);
    md5_transform (context->state, context->buffer);

    for (i = partLen; i + 63 < inputLen; i += 64)
      md5_transform (context->state, &input[i]);
    index = 0;
  }
  else
    i = 0;
  md5_memcpy ((POINTER)&context->buffer[index], (POINTER)&input[i], inputLen-i);
}

void md5_final (unsigned char digest[16], MD5_CTX *context)
{
  unsigned char bits[8];
  unsigned int index, padLen;

  Encode (bits, context->count, 8);

  index = (unsigned int)((context->count[0] >> 3) & 0x3f);
  padLen = (index < 56) ? (56 - index) : (120 - index);
  md5_update (context,(unsigned char*) PADDING, padLen);

  md5_update (context, bits, 8);
  Encode (digest, context->state, 16);

  md5_memset ((POINTER)context, 0, sizeof (*context));
}

// IA_EXPORT void get_mac(char *mac)
// {
//   IP_ADAPTER_INFO AdapterInfo[16]; // 定义网卡信息存贮区。
//   DWORD dwBufLen = sizeof(AdapterInfo); 
// 
//   DWORD dwStatus = GetAdaptersInfo( 
//     AdapterInfo, // [output] 指向接收数据缓冲指针
//     &dwBufLen); // [input] 缓冲区大小
//   //assert(dwStatus == ERROR_SUCCESS); // 此处是个trap，用来保证返回值有效
// 
//   PIP_ADAPTER_INFO pAdapterInfo = AdapterInfo; 
//   unsigned char *ch = pAdapterInfo->Address;
//   mac = (char*)ch;
//   //   do {
//   //     pAdapterInfo = pAdapterInfo->Next; 
//   //   }
//   //   while(pAdapterInfo); 
// }


// void   get_mac()   
// {
//   IP_ADAPTER_INFO     *pAdapterInfo;   
//   ULONG                         ulOutBufLen;   
//   pAdapterInfo   =   (IP_ADAPTER_INFO   *)   malloc(   sizeof(IP_ADAPTER_INFO)   );   
//   ulOutBufLen   =   sizeof(IP_ADAPTER_INFO);   
//   if   (GetAdaptersInfo(   pAdapterInfo,   &ulOutBufLen)   !=   ERROR_SUCCESS)   {   
//     delete(pAdapterInfo);   
//     pAdapterInfo   =   (IP_ADAPTER_INFO   *)   malloc   (   sizeof(ulOutBufLen)   );   
//   }   
//   }   

void encrypt_md5 (char *ch)
{
  //CNetworkAdapter cna;
  //get_mac();
  LPCTSTR mac = _T("00-22-FA-7D-75-1A");
  int nLen = WideCharToMultiByte( CP_ACP, 0, mac/*cna.GetAdapterAddress().c_str()*/, -1, NULL, 0, NULL, NULL );
  char *string = new char[nLen];
  WideCharToMultiByte( CP_ACP, 0, mac/*cna.GetAdapterAddress().c_str()*/, -1, string, nLen, NULL, NULL );

  MD5_CTX context;
  unsigned char digest[16];
  //char output1[32];
  static char output[33]={""};
  unsigned int len = strlen (string);
  int i;
  md5_init (&context);
  md5_update (&context, (unsigned char*)string, len);
  md5_final (digest, &context);

  for (i = 0; i < 16; i++)
  {
    //     sprintf(&(output1[2*i]),"%02x",(char)digest[i]);
    //     sprintf(&(output1[2*i+1]),"%02x",(char)(digest[i]<<4));
    sprintf(&ch[i], "%02x",(char)digest[i] );
    sprintf(&ch[2*i+1], "%02x",(char)(digest[i]<<4) );
    //     output1[2*i] = (char)digest[i];
    //     output1[2*i+1] = (char)(digest[i]<<4);
  }
  //   for(i=0;i<32;i++)
  //     ch[i] = output1[i];
  //   for(i=0;i<32;i++)
  //     output[i]=output1[i];
  //   return output;
}

// char* encrypt_md5_file(char *filename)
// {
//   static char output[33]={""};
//   FILE *file;
//   MD5_CTX context;
//   int i, len;
//   unsigned char buffer[1024], digest[16];
//   char output1[32];
//   if ((file = fopen (filename, "rb")) == NULL)
//   {
//     printf ("%s can't be openedn", filename);
//     return 0;
//   }
//   else
//   {
//     MD5Init (&context);
//     while (len = fread (buffer, 1, 1024, file))
//       MD5Update (&context, buffer, len);
//     MD5Final (digest, &context);
//     fclose (file);
//     for (i = 0; i < 16; i++)
//     {
//       sprintf(&(output1[2*i]),"%02x",(unsigned char)digest[i]);
//       sprintf(&(output1[2*i+1]),"%02x",(unsigned char)(digest[i]<<4));
//     }
//     for(i=0;i<32;i++)
//       output[i]=output1[i];
//     return output;
//   }
// }

char* encrypt_md5_key(char* text)
{
  char* key = "enmind";
  char digest[16];
  char output1[34];
  static char output[33]={""};
  MD5_CTX context;
  unsigned char k_ipad[65];
  unsigned char k_opad[65];
  unsigned char tk[16];
  int i;
  int text_len = strlen (text);
  int key_len=strlen(key);
  if (key_len > 64)
  {
    MD5_CTX tctx;
    md5_init(&tctx);
    md5_update(&tctx,(unsigned char*) key, key_len);
    md5_final(tk, &tctx);
    key = (char*)tk;
    key_len = 16;
  }

  for(i=0;i<65;i++)
    k_ipad[i]=(unsigned char)0;
  for(i=0;i<65;i++)
    k_opad[i]=(unsigned char)0;

  for(i=0;i<key_len;i++)
  {
    k_ipad[i]=(unsigned char)key[i];
    k_opad[i]=(unsigned char)key[i];
  }

  for (i=0; i<64; i++) 
  {
    k_ipad[i] ^= 0x36;
    k_opad[i] ^= 0x5c;
  }

  md5_init(&context); 
  md5_update(&context, k_ipad, 64); 
  md5_update(&context, (unsigned char*)text, text_len);
  md5_final((unsigned char*)digest, &context);

  md5_init(&context);
  md5_update(&context, k_opad, 64);
  md5_update(&context,(unsigned char*) digest, 16);
  md5_final((unsigned char*)digest, &context);
  for (i = 0; i < 16; i++)
  {
    sprintf(&(output1[2*i]),"%02x",(unsigned char)digest[i]);
    sprintf(&(output1[2*i+1]),"%02x",(unsigned char)(digest[i]<<4));
  }
  for(i=0;i<32;i++)
    output[i]=output1[i]; 
  char *sn = output;
  return sn;
}

enum RETVALUE
{
  SUCCESS,
  ERROR_API_CALL_FAILED,
  ERROR_FAILURE_WHILE_LOADING_LIBRARY,
  ERROR_OS_VERSION_NOT_SUPPORTED,
  ERROR_SOFTWAREKEY_NOT_FOUND,
  ERROR_CONVERSION_CHAR_2_WCHAR_FAILED
};


RETVALUE RetrieveMACAddress(BYTE pMACaddress[]);

#define MAC_DIM					6
BOOL GetMAC(BYTE mac[MAC_DIM])
{
  if( RetrieveMACAddress(mac) == SUCCESS )
    return TRUE;

  return FALSE;
}

RETVALUE RetrieveMACAddress(BYTE pMACaddress[MAC_DIM])
{
  RETVALUE		lReturnValue = SUCCESS;
  GUID			lGUID;
  OSVERSIONINFO	lVersionInfo;

  lVersionInfo.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);

  BOOL lResult = GetVersionEx(&lVersionInfo);	

  if (!lResult)
  {
    return ERROR_API_CALL_FAILED;
  }

  // Win 2000, Win XP, Win Me
  if	( true
    //     ( lVersionInfo.dwMajorVersion == 5 ) 
    //     || 
    //     ( 
    //     ( lVersionInfo.dwMajorVersion == 4 ) 
    //     && 
    //     ( lVersionInfo.dwMinorVersion == 90 ) 
    //     )
    )
  {
    typedef void (__stdcall* PROCFUNC)(UUID*); 

    HINSTANCE		lLib; 
    PROCFUNC UuidCreateSequential; 

    lLib = LoadLibrary(_T("RPCRT4")); 

    if (lLib != NULL) 
    { 
      UuidCreateSequential = (PROCFUNC) GetProcAddress(
        lLib, 
        "UuidCreateSequential"); 

      if (UuidCreateSequential != NULL) 
      {
        UuidCreateSequential(&lGUID); 
      }
      else
      {
        return ERROR_API_CALL_FAILED;
      }

      lResult = FreeLibrary(lLib); 
    }
    else
    {
      return ERROR_FAILURE_WHILE_LOADING_LIBRARY;
    }
  }
  else
    // Win NT (without any service pack)
    // NOTE: The string field  szCSDVersion of the structure OSVERSIONINFO 
    // give us information about the installed service pack. If the string 
    // is empty, then no service pack has been installed. If the string is 
    // something like "Service Pack 3", this means that service pack 3 
    // has been installed.
    if	( lVersionInfo.dwMajorVersion < 5 ) 
    {
      HRESULT h = CoCreateGuid(&lGUID);
    }
    else
    {
      return ERROR_OS_VERSION_NOT_SUPPORTED;
    }

    // Retrieve the MAC address 
    // (Bytes 2 through 7 of Data4 keeps the MAC address).
    for (int i=0;i<MAC_DIM;i++)
    {
      pMACaddress[i]=lGUID.Data4[i+2];
    }

    return lReturnValue;
}

int CharToInt(char* s, int start, int end)
{
  int num = 0;
  int count  = end - start + 1;
  for (int i = 0; i < count; i++)
  {
    int n = s[start + i] - '0';
    if (n > 9)
      n = s[start + i] - 'a' + 10;
    num += n * (int)pow((float)16, count - 1 - i);
  }
  num +=KEY;
  return num;
}

IA_EXPORT void MakeNumber(int &mn_1, int &mn_2, int &mn_3, int &mn_4)
{
  BYTE mac[MAC_DIM];
  BOOL result = GetMAC(mac);
  char ch[13];
  for (int i = 0; i < 6; i++)
  {
    sprintf(&(ch[i*2]),"%02x",(unsigned char)mac[i]);
  }
  mn_1 = CharToInt(ch, 0, 2);
  mn_2 = CharToInt(ch, 3, 5);
  mn_3 = CharToInt(ch, 6, 8);
  mn_4 = CharToInt(ch, 9, 11);
}



IA_EXPORT BOOL match_sn(char* const * sn)
{
//   BYTE mac[MAC_DIM];
//   BOOL result = GetMAC(mac);
//   char c[13];
//   for (int i = 0; i < 6; i++)
//   {
//     sprintf(&(c[i*2]),"%02x",(unsigned char)mac[i]);
//   }
  int mn[4];
  MakeNumber(mn[0], mn[1], mn[2], mn[3]);
  char c[17];
  for (int i = 0; i < 4; i++)
  {
    sprintf(&(c[i*4]),"%04d",mn[i]);
  } 
  char *ch = encrypt_md5_key(c);
  for (int i = 0; i < 32; i++)
  {
    char c1 = *(ch + i);
    char c2 = *(*sn +i);
    if (c1 != c2)
      return FALSE;
  }
  return TRUE;
}