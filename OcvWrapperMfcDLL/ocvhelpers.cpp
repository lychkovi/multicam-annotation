// ocvhelpers.cpp: Функции для преобразования OpenCV Mat <--> Microsoft HBITMAP

#include "stdafx.h"

#include "ocvhelpers.h"

// Вспомогательные функции OpenCV IplImage <--> Microsoft HBITMAP из CvvImage.cpp

extern CBitmap* IplImageToCBitmap(IplImage* img); 
extern IplImage* HBITMAPToIplImage(HBITMAP hBitmap); 

bool copyMatToHbitmap(const Mat& image, HBITMAP* hBitmap)
{
    // Преобразуем кадр видео файла в формат Microsoft
    IplImage iplim = image;
    CBitmap* bmp = IplImageToCBitmap(&iplim);
    if (!bmp)
        return false;

    // Возвращаем первый кадр в вызывающую программу
    *hBitmap = (HBITMAP)bmp->Detach();
    bmp->DeleteObject();
    delete bmp;
    bmp = NULL;
    return true;
}

bool copyHbitmapToMat(HBITMAP hBitmap, Mat& image)
{
    IplImage* iplim = HBITMAPToIplImage(hBitmap);
    if (!iplim)
        return false;
    image = Mat(iplim, true);
    cvReleaseImage(&iplim);
    return true;
}
