/* OcvWrapper: Класс реализует интерфейс с библиотекой C++, подключающей 
 * OpenCV. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;       // для DllImport native C++


namespace VideoAnnotationAPP
{
    public static class OcvWrapper
    {
        // Тестовые функции
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TestSubtract(int a, int b);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TestAdd(int a, int b);

        [DllImport("OcvWrapperMfcDLL.dll")]
        public static extern void TestAddArray(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] double[] inputValues,
            int inputValuesCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] out double[] outputValues,
            out int outputValuesCount
        );

        // Функции для работы с файлом изображения
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImageLoad(String filepath, out IntPtr hBitmap);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImageSave(String filepath, IntPtr hBitmap);

        // Функции для работы с видеофайлом
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VideoOpen(String filepath, 
            out int videoHandle, 
            out IntPtr hBitmap,
            out int nframes, out double fps);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VideoSeek(int videoHandle, 
            double video_time_ms, out IntPtr hBitmap,
            out int iframe);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VideoClose(int videoHandle);
    }
}
