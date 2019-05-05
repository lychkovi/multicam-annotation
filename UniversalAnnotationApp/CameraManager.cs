// CameraManager: Класс реализует взаимодействие с видеофайлом. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;                       // Объект Image
using System.IO;                            // время создания файла
using System.Windows.Forms;                 // Элементы ToolStrip

using CameraData;                           // CameraProvider
using MarkupData;                           // структура RecordingInfo


namespace UniversalAnnotationApp
{
    public struct CameraManagerControls
    {
        public ToolStripStatusLabel statusVideo;
    }

    public interface ICamera
    {
        void CameraGuiBind(CameraManagerControls controls);
    }
    // Все Protected-методы, которые могут вызываться классами-наследниками
    abstract class CameraManagerBase
    {
        abstract public void CameraGuiBind(CameraManagerControls controls);

        abstract protected RecordingInfo CameraRecordingInfo { get; }
        abstract protected bool CameraOpen(RecordingInfo rec);
        abstract protected void CameraClose();
        abstract protected bool CameraIsOpened { get; }
        abstract protected RecordingInfo CameraCreate(
            string[] ViewFilePaths, string Comment);
        abstract protected void CameraLoadFrame(
            int FrameID, out List<Image> viewImages);
    }

    /* Класс обеспечивает доступ к флагам состояния слоя только через 
     * бизнес-логику обновления графического интерфейса. */
    class CameraManagerState
    {
        private bool m_IsOpened;            // признак открытия видеозаписи
        private CameraManagerControls m_Controls;

        private void m_ControlsUpdate()
        {
            if (m_Controls.statusVideo != null)
            {
                if (m_IsOpened)
                {
                    m_Controls.statusVideo.Text = "Video Opened";
                }
                else
                {
                    m_Controls.statusVideo.Text = "No Video";
                }
            }
        }

        public void GuiBind(CameraManagerControls controls)
        {
            m_Controls = controls;
            m_ControlsUpdate();
        }

        public bool IsOpened
        {
            get { return m_IsOpened; }

            set
            {
                m_IsOpened = value;
                m_ControlsUpdate();
            }
        }

        public CameraManagerState()
        {
            m_IsOpened = false;
        }
    }

    class CameraManager : CameraManagerBase
    {
        private CameraProvider m_cam;   // поставщик видео-данных
        private CameraManagerState m_state;    // признак открытой видеозаписи
        private RecordingInfo m_RecordingInfo; // открытая видеозапись
        private List<int> m_VideoHandles;   // номера открытых видеофайлов

        // Свойство для чтения сведения о видеозаписи верхними слоями
        override protected RecordingInfo CameraRecordingInfo 
        {
            get { return m_RecordingInfo; }
        }

        override protected bool CameraOpen(RecordingInfo rec) 
        {
            // По очереди открываем все файлы, определяем их параметры
            int VideoHandle;
            Image InitialFrame;
            int FramesCount;
            double Fps;

            // При необходимости закрываем текущю
            if (m_state.IsOpened)
                CameraClose();

            if (rec.Views.Count == 0)
                throw new Exception("Recording file must contain views!");

            for (int i = 0; i < rec.Views.Count; i++)
            {
                if (!m_cam.Open(rec.Views[i].FileNameAVI, out VideoHandle,
                    out InitialFrame, out FramesCount, out Fps))
                {
                    throw new Exception(string.Format(
                        "Unable to open {0}-th video!", i + 1));
                }
                if (FramesCount != rec.FramesCount)
                {
                    throw new Exception("Wrong Frames Count!");
                }
                if (Fps != rec.Fps)
                {
                    throw new Exception("Wrong FPS value!");
                }
                if (rec.Views[i].FrameWidth != InitialFrame.Width)
                {
                    throw new Exception("Wrong video frame size!");
                }
                m_VideoHandles.Add(VideoHandle);
            }

            // Сохраняем информацию о видеозаписи
            m_RecordingInfo = rec;
            m_state.IsOpened = true;
            return true;
        }

        override protected void CameraClose()
        {
            if (m_state.IsOpened)
            {
                for (int i = 0; i < m_VideoHandles.Count; i++)
                    m_cam.Close(m_VideoHandles[i]);
                m_state.IsOpened = false;
                m_VideoHandles.Clear();
            }
        }

        override protected bool CameraIsOpened 
        {
            get { return m_state.IsOpened; }
        }

        // Метод возвращает изображения всех видов указанного кадра
        override protected void CameraLoadFrame(
            int FrameID, out List<Image> viewImages)
        {
            viewImages = new List<Image>();
            if (m_state.IsOpened)
            {
                for (int i = 0; i < m_RecordingInfo.Views.Count; i++)
                {
                    Image image = m_cam.GetFrame(m_VideoHandles[i], FrameID);
                    viewImages.Add(image);
                }
            }
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
                throw new Exception("You must specify views!");
            
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
                    throw new Exception(string.Format(
                        "Unable to open {0}-th video!", i + 1));
                }
                if (i == 0)
                {
                    rec.FramesCount = FramesCount;
                    rec.Fps = Fps;
                }
                else if (FramesCount != rec.FramesCount)
                {
                    throw new Exception("FramesCount differs!");
                }
                else if (Fps != rec.Fps)
                {
                    throw new Exception("FPS value differs!");
                }
                MarkupData.View view = new MarkupData.View();
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

        // Подключение элементов управления формы
        override public void CameraGuiBind(CameraManagerControls controls)
        {
            m_state.GuiBind(controls);
        }

        /* Конструкутор класса по умолчанию. */
        public CameraManager()
        {
            m_cam = new CameraProviderVideo();
            m_state = new CameraManagerState();
            m_VideoHandles = new List<int>();
        }
    }
}
