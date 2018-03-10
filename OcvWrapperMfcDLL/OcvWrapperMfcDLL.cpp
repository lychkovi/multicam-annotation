// OcvWrapperMfcDLL.cpp: определяет процедуры инициализации для DLL.
//

#include "stdafx.h"
#include "OcvWrapperMfcDLL.h"

#include <opencv2/core/core.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/highgui/highgui.hpp>
using namespace cv;

// Вспомогательные функции OpenCV IplImage <--> Microsoft HBITMAP из CvvImage.cpp

extern CBitmap* IplImageToCBitmap(IplImage* img); 
extern IplImage* HBITMAPToIplImage(HBITMAP hBitmap); 


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
