using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;       // для DllImport native C++

using MarkupData;

namespace Tracking
{
    // Базовый класс для всех отслеживателей
    public abstract class Tracker
    {
        abstract public bool TrackBox(Image srcImage, Image dstImage, 
            Box srcBox, out Box dstBox);
        abstract public bool TrackMarker(Image srcImage, Image dstImage, 
            Marker srcMarker, out Marker dstMarker);
        abstract public bool CanTrackBox();
        abstract public bool CanTrackMarker();
    }

    // Структура, содержащая все реализованные отслеживатели
    public class Trackers
    {
        public TldTracker TLD;

        // Конструктор инициализирует экземпляры всех отслеживателей
        public Trackers()
        {
            TLD = new TldTracker();
        }
    }

    // Интерфейс к функциям отслеживания OpenCV
    public static class OCV
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);

        [DllImport("OcvWrapperMfcDLL.dll")]
        private static extern int CalcOpticalFlowPyrLK(
            IntPtr hSrcBitmap, 
            IntPtr hDstBitmap, 
            int nlevels,
            int npts,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] srcPtsX,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] srcPtsY,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] out float[] dstPtsX,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] out float[] dstPtsY,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] out byte[] status,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] out float[] error
        );

        // Метод расчета оптического потока Лукаса-Канаде (разряженного)
        public static void OpticalFlowLK(
            Image srcImage,           // изображение с заданными точками
            Image dstImage,           // изображение для отыскания точек
            int nlevels,              // количество уровней пирамиды
            List<PointF> srcPts,      // координаты точек на srcImage
            out List<PointF> dstPts,  // координаты точек на dstImage
            out List<byte> dstStatus, // признаки успеха отслеживания точек
            out List<float> dstError, // погрешности отслеживания точек
            List<byte> srcStatus = null)// признаки наличия точек на srcImage
        {
            IntPtr hSrcBitmap, hDstBitmap;

            // Извлекаем битмапы из исходных изображений
            Bitmap srcBitmap = new Bitmap(srcImage);
            hSrcBitmap = srcBitmap.GetHbitmap();
            Bitmap dstBitmap = new Bitmap(dstImage);
            hDstBitmap = dstBitmap.GetHbitmap();

            // Конструируем массив признаков актуальных точек
            int nTotalPts = srcPts.Count();
            if (srcStatus == null)
            {
                srcStatus = new List<byte>();
                for (int i = 0; i < nTotalPts; i++)
                {
                    srcStatus.Add(1);
                }
            }

            // Подсчитываем количество актуальных точек
            int nAlivePts = 0;
            for (int i = 0; i < nTotalPts; i++)
            {
                if (srcStatus[i] == 1) nAlivePts++;
            }
            
            // Извлекаем массивы координат актуальных точек
            float[] srcAlivePtsX = new float[nAlivePts];
            float[] srcAlivePtsY = new float[nAlivePts];
            for (int i = 0, j = 0; i < nTotalPts; i++)
            {
                if (srcStatus[i] == 1)
                {
                    srcAlivePtsX[j] = srcPts[i].X;
                    srcAlivePtsY[j] = srcPts[i].Y;
                    j++;
                }
            }

            // Вызываем функцию из нативной библиотеки
            float[] dstAlivePtsX;
            float[] dstAlivePtsY;
            byte[] dstAliveStatus;
            float[] dstAliveError;
            int errcode = CalcOpticalFlowPyrLK(
                hSrcBitmap,
                hDstBitmap,
                nlevels,
                nAlivePts,
                srcAlivePtsX,
                srcAlivePtsY,
                out dstAlivePtsX,
                out dstAlivePtsY,
                out dstAliveStatus,
                out dstAliveError);
            if (errcode != 0)
                throw new Exception("Problem while image resizing!");

            // Сохраняем результаты в возвращаемые массивы
            dstPts = new List<PointF>();
            dstStatus = new List<byte>();
            dstError = new List<float>();
            for (int i = 0, j = 0; i < nTotalPts; i++)
            {
                PointF pt = new PointF();
                byte status;
                float error;
                if (srcStatus[i] == 1)
                {
                    pt.X = dstAlivePtsX[j];
                    pt.Y = dstAlivePtsY[j];
                    status = dstAliveStatus[j];
                    error = dstAliveError[j];
                    j++;
                }
                else
                {
                    pt.X = 0;
                    pt.Y = 0;
                    status = 0;
                    error = 0;
                }
                dstPts.Add(pt);
                dstStatus.Add(status);
                dstError.Add(error);
            }

            // Освобождаем память
            DeleteObject(hSrcBitmap);
            DeleteObject(hDstBitmap);
        }
    }
}
