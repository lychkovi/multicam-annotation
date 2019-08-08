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

vector<VideoCapture> caps;  // массив объектов для чтения кадров из видео файла
vector<Mat> prevFrames;      // последний считанный из видео файла кадр
vector<int> prevFrameNums;   // номер последнего считанного из видео файла кадра

// Экспортируемые функции

extern "C"  
{
    // Пробная функция вычисляет сумму двух чисел
    __declspec(dllexport) int TestAdd(int a, int b) 
    {
        return a+b;
    }

    // Пробная функция вычисляет разность двух чисел
    __declspec(dllexport) int TestSubtract(int a, int b) 
    {
        return a-b;
    }

    // Функция загружает изображение из файла на диске
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

	// Функция масштабирует изображение
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

    // Функция сохраняет изображение в файл на диске
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

    // Пробная функция для обработки массива чисел
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

    // Функция открывает видео файл, считывает из него первый кадр и 
    // возвращает первый кадр, количество кадров и частоту кадров в секунду
    __declspec(dllexport) HRESULT VideoOpen(
        const char* filepath, 
        /* out */ int* videoHandleOut, 
        /* out */ HBITMAP * hBitmap, 
        /* out */ int* nframes, 
        /* out */ double* fps)
    {
        VideoCapture cap;

        // Открываем видео файл для чтения
        cap.open(filepath);
        if (!cap.isOpened())
            return S_FALSE;

        // Считываем первый кадр из видео файла
        Mat img;
        cap >> img;
        if (img.empty())
            return S_FALSE;

        if (!copyMatToHbitmap(img, hBitmap))
            return S_FALSE;

        // Обновляем переменные состояния проигрывателя
        int videoHandle = (int) caps.size();
        caps.push_back(cap);
        prevFrames.push_back(img);
        prevFrameNums.push_back(0);

        // Возвращаем параметры видео в вызывающую программу
        *nframes = (long) cap.get(CV_CAP_PROP_FRAME_COUNT);
        *fps = (double) cap.get(CV_CAP_PROP_FPS);
        *videoHandleOut = videoHandle;

        return S_OK;
    }

    // Функция перематывает видео до нужного момента времени (мс)
    __declspec(dllexport) HRESULT VideoSeek(int videoHandle, 
        double video_time_ms, 
        /* out */ HBITMAP* hBitmap, 
        /* out */ int* iframe) // нумерация iframe начинается с 1
    {
        if (videoHandle < 0 || videoHandle >= (int)caps.size())
            return S_FALSE;

        // Перемещаемся в нужную позицию видеофайла
        if (!caps[videoHandle].set(CV_CAP_PROP_POS_MSEC, video_time_ms))
            return S_FALSE;

        // Считываем кадр видео в текущей позиции
        Mat img;
        caps[videoHandle] >> img;
        if (img.empty())
            return S_FALSE;

        // Возвращаем считанный кадр и его номер в приложение
        if (!copyMatToHbitmap(img, hBitmap))
            return S_FALSE;
        *iframe = (int) caps[videoHandle].get(CV_CAP_PROP_POS_FRAMES);

        // Обновляем переменные состояния проигрывателя
        img.copyTo(prevFrames[videoHandle]);
        prevFrameNums[videoHandle] = *iframe;

        return S_OK;
    }

    // Функция запроса сведений о видеофайле
    __declspec(dllexport) HRESULT VideoGetInfo(int videoHandle,
        /* out */ int* nframes, 
        /* out */ double* fps)
    {
        if (videoHandle < 0 || videoHandle >= (int)caps.size())
            return S_FALSE;

        // Возвращаем параметры видео в вызывающую программу
        *nframes = (long) caps[videoHandle].get(CV_CAP_PROP_FRAME_COUNT);
        *fps = (double) caps[videoHandle].get(CV_CAP_PROP_FPS);

        return S_OK;
    }

    // Функция закрывает видео файл, ранее открытый функцией open_video()
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
