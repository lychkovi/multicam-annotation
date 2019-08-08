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

vector<VideoCapture> caps;  // ������ �������� ��� ������ ������ �� ����� �����
vector<Mat> prevFrames;      // ��������� ��������� �� ����� ����� ����
vector<int> prevFrameNums;   // ����� ���������� ���������� �� ����� ����� �����

// �������������� �������

extern "C"  
{
    // ������� ������� ��������� ����� ���� �����
    __declspec(dllexport) int TestAdd(int a, int b) 
    {
        return a+b;
    }

    // ������� ������� ��������� �������� ���� �����
    __declspec(dllexport) int TestSubtract(int a, int b) 
    {
        return a-b;
    }

    // ������� ��������� ����������� �� ����� �� �����
    __declspec(dllexport) HRESULT ImageLoad(
        const char* filepath, /* out */ HBITMAP * hBitmap)
    {
        Mat img = imread(filepath, CV_LOAD_IMAGE_COLOR);
        if (img.empty())
            return S_FALSE;

        if (!copyMatToHbitmap(img, hBitmap))
            return S_FALSE;
        
        return S_OK;
    }

	// ������� ������������ �����������
	__declspec(dllexport) HRESULT ImageResize(HBITMAP hBmpSrc, 
		double scale, /* out */ HBITMAP * hBitmap)
	{
		Mat src;
        if (!copyHbitmapToMat(hBmpSrc, src))
            return S_FALSE;

		Mat dst;
		try
		{
			resize(src, dst, Size(), scale, scale);
		}
		catch (...)
		{
			return S_FALSE;
		}
		        
		if (!copyMatToHbitmap(dst, hBitmap))
            return S_FALSE;
        
        return S_OK;
	}

    // ������� ��������� ����������� � ���� �� �����
    __declspec(dllexport) HRESULT ImageSave(
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
    __declspec(dllexport) void __stdcall TestAddArray(
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
    __declspec(dllexport) HRESULT VideoOpen(
        const char* filepath, 
        /* out */ int* videoHandleOut, 
        /* out */ HBITMAP * hBitmap, 
        /* out */ int* nframes, 
        /* out */ double* fps)
    {
        VideoCapture cap;

        // ��������� ����� ���� ��� ������
        cap.open(filepath);
        if (!cap.isOpened())
            return S_FALSE;

        // ��������� ������ ���� �� ����� �����
        Mat img;
        cap >> img;
        if (img.empty())
            return S_FALSE;

        if (!copyMatToHbitmap(img, hBitmap))
            return S_FALSE;

        // ��������� ���������� ��������� �������������
        int videoHandle = (int) caps.size();
        caps.push_back(cap);
        prevFrames.push_back(img);
        prevFrameNums.push_back(0);

        // ���������� ��������� ����� � ���������� ���������
        *nframes = (long) cap.get(CV_CAP_PROP_FRAME_COUNT);
        *fps = (double) cap.get(CV_CAP_PROP_FPS);
        *videoHandleOut = videoHandle;

        return S_OK;
    }

    // ������� ������������ ����� �� ������� ������� ������� (��)
    __declspec(dllexport) HRESULT VideoSeek(int videoHandle, 
        double video_time_ms, 
        /* out */ HBITMAP* hBitmap, 
        /* out */ int* iframe) // ��������� iframe ���������� � 1
    {
        if (videoHandle < 0 || videoHandle >= (int)caps.size())
            return S_FALSE;

        // ������������ � ������ ������� ����������
        if (!caps[videoHandle].set(CV_CAP_PROP_POS_MSEC, video_time_ms))
            return S_FALSE;

        // ��������� ���� ����� � ������� �������
        Mat img;
        caps[videoHandle] >> img;
        if (img.empty())
            return S_FALSE;

        // ���������� ��������� ���� � ��� ����� � ����������
        if (!copyMatToHbitmap(img, hBitmap))
            return S_FALSE;
        *iframe = (int) caps[videoHandle].get(CV_CAP_PROP_POS_FRAMES);

        // ��������� ���������� ��������� �������������
        img.copyTo(prevFrames[videoHandle]);
        prevFrameNums[videoHandle] = *iframe;

        return S_OK;
    }

    // ������� ������� �������� � ����������
    __declspec(dllexport) HRESULT VideoGetInfo(int videoHandle,
        /* out */ int* nframes, 
        /* out */ double* fps)
    {
        if (videoHandle < 0 || videoHandle >= (int)caps.size())
            return S_FALSE;

        // ���������� ��������� ����� � ���������� ���������
        *nframes = (long) caps[videoHandle].get(CV_CAP_PROP_FRAME_COUNT);
        *fps = (double) caps[videoHandle].get(CV_CAP_PROP_FPS);

        return S_OK;
    }

    // ������� ��������� ����� ����, ����� �������� �������� open_video()
    __declspec(dllexport) HRESULT VideoClose(int videoHandle)
    {
        if (videoHandle < 0 || videoHandle >= (int)caps.size())
            return S_FALSE;

        caps[videoHandle].release();
        prevFrames[videoHandle].release();
        prevFrameNums[videoHandle] = -1;
        return S_OK;
    }
}
