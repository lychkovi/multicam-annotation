// OcvWrapperMfcDLL.cpp: определяет процедуры инициализации для DLL.
//

#include "stdafx.h"
#include "OcvWrapperMfcDLL.h"

#include "ocvhelpers.h"         // Преобразование OpenCV Mat <--> Microsoft HBITMAP


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


// Хранимые данные

VideoCapture cap;           // объект для чтения кадров из видео файла
Mat prevFrame;              // последний считанный из видео файла кадр
int prevFrameNum = 0;       // номер последнего считанного из видео файла кадра

// Экспортируемые функции

extern "C"  
{
    // Пробная функция вычисляет сумму двух чисел
    __declspec(dllexport) int add(int a, int b) 
    {
        return a+b;
    }

    // Пробная функция вычисляет разность двух чисел
    __declspec(dllexport) int subtract(int a, int b) 
    {
        return a-b;
    }

    // Функция загружает изображение из файла на диске
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

    // Функция сохраняет изображение в файл на диске
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

    // Пробная функция для обработки массива чисел
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

    // Функция открывает видео файл, считывает из него первый кадр и 
    // возвращает первый кадр, количество кадров и частоту кадров в секунду
    __declspec(dllexport) HRESULT open_video(
        const char* filepath, /* out */ HBITMAP * hBitmap, 
        /* out */ int* nframes, /* out */ double* fps)
    {
        // Открываем видео файл для чтения
        cap.open(filepath);
        if (!cap.isOpened())
            return S_FALSE;

        // Считываем первый кадр из видео файла
        cap >> prevFrame;
        if (prevFrame.empty())
            return S_FALSE;

        if (!copyMatToHbitmap(prevFrame, hBitmap))
            return S_FALSE;

        // Возвращаем параметры видео в вызывающую программу
        *nframes = (long) cap.get(CV_CAP_PROP_FRAME_COUNT);
        *fps = (double) cap.get(CV_CAP_PROP_FPS);

        return S_OK;
    }

    // Функция закрывает видео файл, ранее открытый функцией open_video()
    __declspec(dllexport) HRESULT close_video()
    {
        cap.release();
        return S_OK;
    }
}
