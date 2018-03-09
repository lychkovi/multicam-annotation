// OcvWrapperMfcDLL.h: главный файл заголовка для DLL OcvWrapperMfcDLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "включить stdafx.h до включения этого файла в PCH"
#endif

#include "resource.h"		// основные символы


// COcvWrapperMfcDLLApp
// Про реализацию данного класса см. OcvWrapperMfcDLL.cpp
//

class COcvWrapperMfcDLLApp : public CWinApp
{
public:
	COcvWrapperMfcDLLApp();

// Переопределение
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
