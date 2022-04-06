// TestMFCDlg.h : 头文件
//

#pragma once


// CTestMFCDlg 对话框
class CTestMFCDlg : public CDialog
{
  DWORD cam0;
  static DWORD WINAPI cam_callback(UINT idx, BYTE* data, int width, int height);
// 构造
public:
	CTestMFCDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_TESTMFC_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
  afx_msg void OnBnClickedSettings();
  virtual BOOL DestroyWindow();
};
