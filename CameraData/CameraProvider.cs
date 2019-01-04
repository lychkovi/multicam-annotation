/* Проект реализует классы поставщиков данных видеозаписей. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;       // для DllImport native C++
using System.Drawing;                       // объект Image


namespace CameraData
{
    /* Абстрактный родительский класс для всех классов чтения кадров. */
    abstract public class CameraProvider
    {
        abstract public bool Open(string VideoFilePath, out int VideoHandle,
            out Image InitialFrame, out int FramesCount, out double Fps);
        abstract public Image GetFrame(int VideoHandle, int FrameID);
        abstract public void Close(int VideoHandle);
    }

    /* Класс, реализующий чтение кадров из видеофайлов. */
    public class CameraProviderVideo: CameraProvider
    {
        // Функции для работы с видеофайлом
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int VideoOpen(String filepath,
            out int videoHandle,
            out IntPtr hBitmap,
            out int nframes, out double fps);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int VideoSeek(int videoHandle,
            double video_time_ms, out IntPtr hBitmap,
            out int iframe);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int VideoGetInfo(int videoHandle,
            out int nframes, 
            out double fps);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int VideoClose(int videoHandle);

        // Открытие источника видео
        override public bool Open(string VideoFilePath, out int VideoHandle,
            out Image InitialFrame, out int FramesCount, out double Fps)
        {
            // Открываем видеофайл
            IntPtr hBitmap;
            int errcode = VideoOpen(VideoFilePath,
                out VideoHandle, out hBitmap, out FramesCount, out Fps);

            if (errcode != 0)
            {
                // Что-то пошло не так...
                InitialFrame = null;
                return false;
            }
            else
            {
                // Преобразуем кадр в нужный формат
                InitialFrame = Image.FromHbitmap(hBitmap);
                return true;
            }
        }

        override public Image GetFrame(int VideoHandle, int FrameID)
        {
            int nframes;
            double fps;
            double seek_time_ms;    // времянная метка запрошенного кадра
            IntPtr hBitmap;
            int iFrameOut;          // индекс полученного кадра
            int errcode;
            Image FrameOut;

            // Узнаем частоту кадров видеозаписи
            errcode = VideoGetInfo(VideoHandle, out nframes, out fps);
            if (errcode != 0)
                throw new IndexOutOfRangeException("Bad video handle!");

            // Проверяем, что индекс кадра входит в пределы видеозаписи
            if (FrameID < 0 || FrameID >= nframes)
                throw new IndexOutOfRangeException("FrameID out of bounds!");

            // Вычисляем время интересующего кадра
            seek_time_ms = 1000.0 * FrameID / fps;

            // Запрашиваем интересующий кадр у OpenCV
            errcode = VideoSeek(VideoHandle, seek_time_ms, 
                out hBitmap, out iFrameOut);

            if (errcode != 0)
                throw new NotSupportedException("VideoSeek failed!");

            if (iFrameOut != FrameID + 1) /* iFrameOut нумеруется с 1 */
                throw new NotSupportedException("Wrong frame received!");

            // Преобразуем кадр к нужному формату и возвращаем
            FrameOut = Image.FromHbitmap(hBitmap);
            return FrameOut;
        }

        override public void Close(int VideoHandle)
        {
            VideoClose(VideoHandle);
        }
    }
}
