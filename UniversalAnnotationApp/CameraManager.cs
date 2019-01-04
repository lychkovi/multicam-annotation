// CameraManager: Класс реализует взаимодействие с видеофайлом. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;                       // Объект Image

using CameraData;                           // CameraProvider
using MarkupData;                           // структура RecordingInfo


namespace UniversalAnnotationApp
{
    // Все Protected-методы, которые могут вызываться классами-наследниками
    abstract class CameraManagerBase
    {
        abstract protected RecordingInfo CameraRecordingInfo { get; }
        abstract protected bool CameraOpen(string RecordingFilePath);
        abstract protected void CameraClose();
        abstract protected bool CameraIsOpened { get; }
    }

    class CameraManager : CameraManagerBase
    {
        private CameraProvider m_cam;   // поставщик видео-данных
        private RecordingInfo m_RecordingInfo; // сведения о видеозаписи

        // Свойство для чтения сведения о видеозаписи верхними слоями
        override protected RecordingInfo CameraRecordingInfo 
        {
            get
            {
                return m_RecordingInfo;
            }
        }

        override protected bool CameraOpen(string RecordingFilePath) 
        {
            // Открываем видеофайл
            Image InitialFrame;
            int FramesCount;
            double Fps;
            int videoHandle;

            bool status = m_cam.Open("", out videoHandle, out InitialFrame, 
                out FramesCount, out Fps);
            return status;
        }

        override protected void CameraClose()
        {
            
        }

        override protected bool CameraIsOpened 
        {
            get { return false; }
        }

        /* Конструкутор класса по умолчанию. */
        public CameraManager()
        {
            m_cam = new CameraProviderVideo();
        }
    }

    
}
