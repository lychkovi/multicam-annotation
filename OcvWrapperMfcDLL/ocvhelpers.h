// ocvhelpers.h: Объявления функций преобразования OpenCV Mat <--> Microsoft HBITMAP

#ifndef OCV_HELPERS_H
#define OCV_HELPERS_H

#include <opencv2/core/core.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/highgui/highgui.hpp>
using namespace cv;

extern bool copyMatToHbitmap(const Mat& image, HBITMAP* hBitmap);
extern bool copyHbitmapToMat(HBITMAP hBitmap, Mat& image);

#endif
