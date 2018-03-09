// OcvWrapperMfcDLL.cpp: ���������� ��������� ������������� ��� DLL.
//

#include "stdafx.h"
#include "OcvWrapperMfcDLL.h"

#include "CvvImage.h"               // OpenCV


#ifdef _DEBUG
#define new DEBUG_NEW
#endif

//
//TODO: ���� ��� ���������� DLL ����������� ������� � ������������ DLL MFC,
//		��� �������, ���������������� �� ������ DLL-����������, ������� ��������� ������ �
//		MFC, ������ ��������� ������ AFX_MANAGE_STATE �
//		����� ������ �������.
//
//		��������:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// ���� ���������� �������
//		}
//
//		�����, ����� ������ ������ ��� ����������� � ������
//		������� �� ������ MFC. ��� ��������, ���
//		�� ������ ���� ������ ���������� 
//		������� � �������������� ���� ����� ����������� ���������� �������,
//		��������� �� ������������ ����� ��������� ������ � MFC
//		DLL.
//
//		� ����������� ��������� MFC 33 � 58 ���������� �����
//		��������� ��������.
//

// COcvWrapperMfcDLLApp

BEGIN_MESSAGE_MAP(COcvWrapperMfcDLLApp, CWinApp)
END_MESSAGE_MAP()


// �������� COcvWrapperMfcDLLApp

COcvWrapperMfcDLLApp::COcvWrapperMfcDLLApp()
{
	// TODO: �������� ��� ��������,
	// ��������� ���� ������ ��� ������������� � InitInstance
}


// ������������ ������ COcvWrapperMfcDLLApp

COcvWrapperMfcDLLApp theApp;


// ������������� COcvWrapperMfcDLLApp

BOOL COcvWrapperMfcDLLApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

// ��������������� �������

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

// �������������� �������

HBITMAP * pBitmap = NULL;

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

    __declspec(dllexport) HRESULT load_image(/* out */ HBITMAP * hBitmap)
    {
        //cv::Mat aMat;
        //CBitmap *aCBitmap = IplImageToCBitmap((IplImage*) &aMat);

        IplImage* img = cvLoadImage("lena.jpg", CV_LOAD_IMAGE_COLOR);
        CBitmap* bmp = IplImageToCBitmap(img);
        pBitmap = hBitmap;
        *hBitmap = (HBITMAP)bmp->Detach();
        bmp->DeleteObject();
        delete bmp;
        bmp = NULL;

        cvReleaseImage(&img);
        return S_OK;
    }

    __declspec(dllexport) HRESULT release_image()
    {
        CBitmap* bmp;
        bmp = (CBitmap*) pBitmap;
        bmp->DeleteObject();
        //delete bmp;
        return true;
    }
}
