// OcvWrapperMfcDLL.h: ������� ���� ��������� ��� DLL OcvWrapperMfcDLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�������� stdafx.h �� ��������� ����� ����� � PCH"
#endif

#include "resource.h"		// �������� �������


// COcvWrapperMfcDLLApp
// ��� ���������� ������� ������ ��. OcvWrapperMfcDLL.cpp
//

class COcvWrapperMfcDLLApp : public CWinApp
{
public:
	COcvWrapperMfcDLLApp();

// ���������������
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
