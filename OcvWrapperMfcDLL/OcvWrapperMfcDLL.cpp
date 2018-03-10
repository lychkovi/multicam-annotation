// OcvWrapperMfcDLL.cpp: определяет процедуры инициализации для DLL.
//

#include "stdafx.h"
#include "OcvWrapperMfcDLL.h"

#include "CvvImage.h"               // OpenCV


#ifdef _DEBUG
#define new DEBUG_NEW
#endif

//
//TODO: если эта библиотека DLL динамически связана с библиотеками DLL MFC,
//		все функции, экспортированные из данной DLL-библиотеки, которые выполняют вызовы к
//		MFC, должны содержать макрос AFX_MANAGE_STATE в
//		самое начало функции.
//
//		Например:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// тело нормальной функции
//		}
//
//		Важно, чтобы данный макрос был представлен в каждой
//		функции до вызова MFC. Это означает, что
//		он должен быть первым оператором 
//		функции и предшествовать даже любым объявлениям переменных объекта,
//		поскольку их конструкторы могут выполнять вызовы к MFC
//		DLL.
//
//		В Технических указаниях MFC 33 и 58 содержатся более
//		подробные сведения.
//

// COcvWrapperMfcDLLApp

BEGIN_MESSAGE_MAP(COcvWrapperMfcDLLApp, CWinApp)
END_MESSAGE_MAP()


// создание COcvWrapperMfcDLLApp

COcvWrapperMfcDLLApp::COcvWrapperMfcDLLApp()
{
	// TODO: добавьте код создания,
	// Размещает весь важный код инициализации в InitInstance
}


// Единственный объект COcvWrapperMfcDLLApp

COcvWrapperMfcDLLApp theApp;


// инициализация COcvWrapperMfcDLLApp

BOOL COcvWrapperMfcDLLApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

// Вспомогательные функции

CBitmap* IplImageToCBitmap(IplImage* img) 
{
    CDC dc;
    CDC memDC;

    if (!dc.CreateDC("DISPLAY", NULL, NULL, NULL))
        return NULL;

    if (!memDC.CreateCompatibleDC(&dc))
        return NULL;

    CBitmap* bmp = new CBitmap();
    CBitmap* pOldBitmap;

    bmp->CreateCompatibleBitmap(&dc, img->width, img->height);
    pOldBitmap = memDC.SelectObject(bmp);

    CvvImage cvImage; // you will need OpenCV_2.2.0- to use CvvImage 
    cvImage.CopyOf(img);
    cvImage.Show(memDC.m_hDC, 0, 0, img->width, img->height, 0, 0);
    cvImage.Destroy();

    memDC.SelectObject(pOldBitmap);
    memDC.DeleteDC();
    dc.DeleteDC();

    return bmp;
}

// Экспортируемые функции

extern "C"  
{
    __declspec(dllexport) int add(int a, int b) 
    {
        return a+b;
    }

    __declspec(dllexport) int subtract(int a, int b) 
    {
        return a-b;
    }

    __declspec(dllexport) HRESULT load_image(
        const char* filepath, /* out */ HBITMAP * hBitmap)
    {
        IplImage* img = cvLoadImage(filepath, CV_LOAD_IMAGE_COLOR);
        if (!img)
            return S_FALSE;

        CBitmap* bmp = IplImageToCBitmap(img);
        cvReleaseImage(&img);
        if (!bmp)
            return S_FALSE;

        *hBitmap = (HBITMAP)bmp->Detach();
        bmp->DeleteObject();
        delete bmp;
        bmp = NULL;
        
        return S_OK;
    }
}
