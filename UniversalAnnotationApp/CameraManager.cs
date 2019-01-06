// CameraManager: Класс реализует взаимодействие с видеофайлом. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;                       // Объект Image
using System.IO;                            // время создания файла

using CameraData;                           // CameraProvider
using MarkupData;                           // структура RecordingInfo


namespace UniversalAnnotationApp
{
    // Все Protected-методы, которые могут вызываться классами-наследниками
    abstract class CameraManagerBase
    {
        abstract protected RecordingInfo CameraRecordingInfo { get; }
        abstract protected bool CameraOpen(RecordingInfo rec);
        abstract protected void CameraClose();
        abstract protected bool CameraIsOpened { get; }
        abstract protected RecordingInfo CameraCreate(
            string[] ViewFilePaths, string Comment);
    }

    class CameraManager : CameraManagerBase
    {
        private CameraProvider m_cam;   // поставщик видео-данных
        private bool m_IsOpened;        // признак открытой видеозаписи
        private RecordingInfo m_RecordingInfo; // открытая видеозапись

        // Свойство для чтения сведения о видеозаписи верхними слоями
        override protected RecordingInfo CameraRecordingInfo 
        {
            get
            {
                return m_RecordingInfo;
            }
        }

        override protected bool CameraOpen(RecordingInfo rec) 
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
            get { return m_IsOpened; }
        }

        /* Метод принимает на вход список путей к файлам видео отдельных
         * камер видеозаписи, проверяет пригодность этих файлов для
         * формирования единой видеозаписи. В случае их пригодности метод
         * заполняет и возвращает структуру описания видеозаписи. В случае
         * непригодности метод выбрасывает исключение. */
        override protected RecordingInfo CameraCreate(
            string[] ViewFilePaths, string Comment)
        {
            int nViews = ViewFilePaths.Length;
            if (nViews < 1)
                throw new NotSupportedException("You must specify views!");
            
            RecordingInfo rec = new RecordingInfo("");
            rec.Comment = Comment;

            // По очереди открываем все файлы, определяем их параметры
            int VideoHandle;
            Image InitialFrame;
            int FramesCount;
            double Fps;
            for (int i = 0; i < nViews; i++)
            {
                if (!m_cam.Open(ViewFilePaths[i], out VideoHandle,
                    out InitialFrame, out FramesCount, out Fps))
                {
                    throw new NotSupportedException(string.Format(
                        "Unable to open {0}-th video!", i + 1));
                }
                if (i == 0)
                {
                    rec.FramesCount = FramesCount;
                    rec.Fps = Fps;
                }
                else if (FramesCount != rec.FramesCount)
                {
                    throw new NotSupportedException("FramesCount differs!");
                }
                else if (Fps != rec.Fps)
                {
                    throw new NotSupportedException("FPS value differs!");
                }
                View view = new View();
                view.ID = i;
                view.Comment = string.Format("camera {0}", i);
                view.FileNameAVI = ViewFilePaths[i];
                view.FrameWidth = InitialFrame.Width;
                view.FrameHeight = InitialFrame.Height;
                view.IsColour = true;
                rec.Views.Add(view);
                m_cam.Close(VideoHandle);
            }
            // Определяем время создания первого файла видеозаписи
            FileInfo fileInfo = new FileInfo(ViewFilePaths[0]);
            rec.DateTime = fileInfo.CreationTime;

            // Возвращаем инициализированную структуру rec
            return rec;
        }

        /* Конструкутор класса по умолчанию. */
        public CameraManager()
        {
            m_cam = new CameraProviderVideo();
        }
    }

    
}
