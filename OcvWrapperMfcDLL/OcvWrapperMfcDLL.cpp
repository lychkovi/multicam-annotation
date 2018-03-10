// OcvWrapperMfcDLL.cpp: ���������� ��������� ������������� ��� DLL.
//

#include "stdafx.h"
#include "OcvWrapperMfcDLL.h"

#include "ocvhelpers.h"         // �������������� OpenCV Mat <--> Microsoft HBITMAP


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


// �������� ������

VideoCapture cap;           // ������ ��� ������ ������ �� ����� �����
Mat prevFrame;              // ��������� ��������� �� ����� ����� ����
int prevFrameNum = 0;       // ����� ���������� ���������� �� ����� ����� �����

// �������������� �������

extern "C"  
{
    // ������� ������� ��������� ����� ���� �����
    __declspec(dllexport) int add(int a, int b) 
    {
        return a+b;
    }

    // ������� ������� ��������� �������� ���� �����
    __declspec(dllexport) int subtract(int a, int b) 
    {
        return a-b;
    }

    // ������� ��������� ����������� �� ����� �� �����
    __declspec(dllexport) HRESULT load_image(
        const char* filepath, /* out */ HBITMAP * hBitmap)
    {
        Mat img = imread(filepath, CV_LOAD_IMAGE_COLOR);
        if (img.empty())
            return S_FALSE;

        if (!copyMatToHbitmap(img, hBitmap))
            return S_FALSE;
        
        return S_OK;
    }

    // ������� ��������� ����������� � ���� �� �����
    __declspec(dllexport) HRESULT save_image(
        const char* filepath, HBITMAP hBitmap)
    {
        Mat img;
        if (!copyHbitmapToMat(hBitmap, img))
            return S_FALSE;

        if (!imwrite(filepath, img))
            return S_FALSE;

        return S_OK;
    }

    // ������� ������� ��� ��������� ������� �����
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

    // ������� ��������� ����� ����, ��������� �� ���� ������ ���� � 
    // ���������� ������ ����, ���������� ������ � ������� ������ � �������
    __declspec(dllexport) HRESULT open_video(
        const char* filepath, /* out */ HBITMAP * hBitmap, 
        /* out */ int* nframes, /* out */ double* fps)
    {
        // ��������� ����� ���� ��� ������
        cap.open(filepath);
        if (!cap.isOpened())
            return S_FALSE;

        // ��������� ������ ���� �� ����� �����
        cap >> prevFrame;
        if (prevFrame.empty())
            return S_FALSE;

        if (!copyMatToHbitmap(prevFrame, hBitmap))
            return S_FALSE;

        // ���������� ��������� ����� � ���������� ���������
        *nframes = (long) cap.get(CV_CAP_PROP_FRAME_COUNT);
        *fps = (double) cap.get(CV_CAP_PROP_FPS);

        return S_OK;
    }

    // ������� ��������� ����� ����, ����� �������� �������� open_video()
    __declspec(dllexport) HRESULT close_video()
    {
        cap.release();
        return S_OK;
    }
}
