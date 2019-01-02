// CameraManager: Класс реализует взаимодействие с видеофайлом. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;       // для DllImport native C++


namespace UniversalAnnotationApp
{
    // Все Protected-методы, которые могут вызываться классами-наследниками
    abstract class CameraManagerBase
    {
        abstract protected bool CameraOpen(string RecordingFilePath);
        abstract protected bool CameraClose();
        abstract protected bool CameraIsOpened { get; }
    }

    class CameraManager : CameraManagerBase
    {
        // Функции для работы с видеофайлом
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int m_VideoOpen(String filepath,
            out int videoHandle,
            out IntPtr hBitmap,
            out int nframes, out double fps);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int m_VideoSeek(int videoHandle,
            double video_time_ms, out IntPtr hBitmap,
            out int iframe);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int m_VideoClose(int videoHandle);

        override protected bool CameraOpen(string RecordingFilePath) 
        {
            // Открываем видеофайл
            IntPtr hBitmap;
            int videoFramesCount;
            double videoFps;
            int videoHandle;

            int status = m_VideoOpen("", out videoHandle, out hBitmap, out videoFramesCount, out videoFps);
            return (status == 0);
        }

        override protected bool CameraClose()
        {
            return true;
        }

        override protected bool CameraIsOpened 
        {
            get { return false; }
        }
    }

    
}
