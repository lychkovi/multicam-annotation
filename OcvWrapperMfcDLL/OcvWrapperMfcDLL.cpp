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


IplImage* HBITMAPToIplImage(HBITMAP hBitmap) 
{
    CDC dc;
    CDC memDC;
    IplImage* img;
    int bmpwidth, bmpheight;

    if (!dc.CreateDC("DISPLAY", NULL, NULL, NULL))
        return NULL;

    if (!memDC.CreateCompatibleDC(&dc))
        return NULL;
    
    HGDIOBJ pOldBitmap;
    pOldBitmap = memDC.SelectObject(hBitmap);

    CBitmap* bmp = new CBitmap();
    BITMAP pBitmapInfo;

    // ����� ������� �������
    bmp->Attach(hBitmap);
    bmp->GetBitmap(&pBitmapInfo);
    bmpwidth = pBitmapInfo.bmWidth;
    bmpheight = pBitmapInfo.bmHeight;
    img = cvCreateImage(cvSize(bmpwidth, bmpheight), 8, 4);

    // ������� ��������� BitmapInfoHeader, ������� ������ ������ ��� ���������� ������ �������
    struct { BITMAPINFO info; RGBQUAD moreColors[255]; } fbi;
    memset(&fbi.info, 0, sizeof(BITMAPINFO));
    fbi.info.bmiHeader.biSize = sizeof(fbi.info.bmiHeader);
    if (!GetDIBits(memDC.m_hDC, hBitmap, 0, bmpheight, NULL, &fbi.info, DIB_RGB_COLORS))
        return NULL;
    fbi.info.bmiHeader.biHeight = -fbi.info.bmiHeader.biHeight; // ����� ����� ����� ������

    // ��������� ���������� ����������� ������
    if (!GetDIBits(memDC.m_hDC, hBitmap, 0, bmpheight, img->imageData, &fbi.info, DIB_RGB_COLORS))
        return NULL;

    // ����������� ������
    bmp->Detach();
    bmp->DeleteObject();
    delete bmp;
    bmp = NULL;

    memDC.SelectObject(pOldBitmap);
    memDC.DeleteDC();
    dc.DeleteDC();

    return img;
}

// �������������� �������

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

    __declspec(dllexport) HRESULT save_image(
        const char* filepath, HBITMAP hBitmap)
    {

        IplImage* img = HBITMAPToIplImage(hBitmap);
        if (!img)
            return S_FALSE;

        if (!cvSaveImage(filepath, img))
            return S_FALSE;

        cvReleaseImage(&img);
        
        return S_OK;
    }

    __declspec(dllexport) void __stdcall detect_targets(
        double* inputValues, long inputValuesCount, 
        /* out */ double **outputValues, /* out */ long* outputValuesCount)
    {
        std::vector<double> outputVector;
        double outputValue;
        
        for (int i = 1; i < inputValuesCount; i++)
        {
            outputValue = inputValues[i-1] + inputValues[i];
            outputVector.push_back(outputValue);
        }

        *outputValuesCount = outputVector.size();
        auto size = (*outputValuesCount)*sizeof(double);

        *outputValues = static_cast<double*>(CoTaskMemAlloc(size));
        memcpy(*outputValues, outputVector.data(), size);
    }
}
