// OcvWrapperMfcDLL.cpp: ���������� ��������� ������������� ��� DLL.
//

#include "stdafx.h"
#include "OcvWrapperMfcDLL.h"

#include <opencv2/core/core.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/highgui/highgui.hpp>
using namespace cv;

// ��������������� ������� OpenCV IplImage <--> Microsoft HBITMAP �� CvvImage.cpp

extern CBitmap* IplImageToCBitmap(IplImage* img); 
extern IplImage* HBITMAPToIplImage(HBITMAP hBitmap); 


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
        Mat img = imread(filepath, CV_LOAD_IMAGE_COLOR);
        if (img.empty())
            return S_FALSE;

        IplImage iplim = img;
        CBitmap* bmp = IplImageToCBitmap(&iplim);
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
        IplImage* iplim = HBITMAPToIplImage(hBitmap);
        if (!iplim)
            return S_FALSE;
        Mat img = Mat(iplim, true);
        cvReleaseImage(&iplim);

        if (!imwrite(filepath, img))
            return S_FALSE;

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
