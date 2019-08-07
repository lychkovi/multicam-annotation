/* DisplayManager: Класс реализует отображение кадров видео с отдельных
 * камер видеозаписи, отображение разметки объектов на этих кадрах и 
 * обработку событий мыши на поля для вывода изображений. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;       // Класс Image

using MarkupData;


namespace UniversalAnnotationApp
{
    public struct DisplayManagerControls
    {
        public DisplayControlWin.DisplayControl DisplayCtrl;
    }

    // Класс представляет информацию для обслуживания отдельного
    // поля вывода изображения
    public struct ViewerData
    {
        public Image zoomedImage; // зуммированное изображение (без разметки)
        public int zoomIndex;     // индекс текущего выбранного зума
        public int viewID;        // идентификатор текущего зума
    }

    public interface IDisplay
    {
        void DisplayGuiBind(DisplayManagerControls controls);
    }

    abstract class DisplayManagerBase : MarkupManager, IDisplay
    {
        // Метод привязки элементов управления форму к объекту DisplayManager
        abstract public void DisplayGuiBind(DisplayManagerControls controls);

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        abstract protected bool DisplayCameraOpen(RecordingInfo rec);
        abstract protected void DisplayCameraClose();
        abstract protected bool DisplayMarkupOpen(string MarkupFilePath);
        abstract protected void DisplayMarkupClose();

        // Метод перерисовки кадров на форме
        abstract protected void DisplayRefresh();
        abstract protected void DisplayLoadFrame(int frameIndex);

    }

    class DisplayManager: DisplayManagerBase
    {
        private DisplayManagerControls m_gui;
        private bool m_IsOpened;   // признак инициализированного дисплея
        private int m_BufferedFrameID;      // номер текущего буферизованного кадра
        private Image[] m_BufferedFrameViews;   
            // буферизованные изображения видов текущего кадра
        private ViewerData[] m_ViewerDatas;
            // зуммированные мзображения для отдельных полей вывода изображений, 
            // а также индексы текущих выбранных зума и вида
        private double[] m_ZoomSeries;  // ряд стандартных значений зума
        private int m_ActiveViewerIndex;    // индекс активного поля вывода

        // Инициализация полей вывода изображений
        private void m_OnCameraOpened()
        {
            // Выделение памяти для буферизации всех видов
            int nviews = CameraRecordingInfo.Views.Count;
            m_BufferedFrameViews = new Image[nviews];

            // Создаём список поддерживаемых значений зума
            m_ZoomSeries = new double[3];
            m_ZoomSeries[0] = 1.0;
            m_ZoomSeries[1] = 2.0;
            m_ZoomSeries[2] = 0.5;

            // Загружаем список видов и список зумов в поля вывода
            if (m_gui.DisplayCtrl != null)
            {
                // Создаём список названий видов
                List<string> viewCaptions = new List<string>();
                string caption;
                for (int i = 0; i < nviews; i++)
                {
                    caption = "View" + i.ToString();
                    viewCaptions.Add(caption);
                }

                // Создаём список названий зумов
                List<string> zoomCaptions = new List<string>();
                for (int i = 0; i < m_ZoomSeries.Count(); i++)
                {
                    caption = m_ZoomSeries[i].ToString() + "x";
                    zoomCaptions.Add(caption);
                }

                // Загружаем списки названий видов и зумов в каждое поле
                int iview = 0;
                int currZoomIndex = 0;
                for (int i = 0; i < m_ViewerDatas.Count(); i++) /* по полям вывода */
                {
                    // Загружаем список видов
                    m_gui.DisplayCtrl.SetViewerListOfViews(i, viewCaptions, iview);
                    m_ViewerDatas[i].viewID = iview;
                    if (iview < nviews - 1)
                        ++iview;
                    // Загружаем список значений зумов
                    m_gui.DisplayCtrl.SetViewerListOfZooms(i, zoomCaptions, currZoomIndex);
                    m_ViewerDatas[i].zoomIndex = currZoomIndex;
                }
            }

            // Буферизуем все виды первого кадра из видеофайлов
            m_IsOpened = true;
            DisplayLoadFrame(0);
        }

        // Обработчик событий от пользовательского элемента управления
        private void m_OnViewerEvent(object sender, 
            DisplayControlWpf.DisplayCanvasEventArgs e)
        {

        }

        // Метод обновляет зуммированный кадр поля вывода и перерисовывает его
        private void m_ViewerUpdateSettings(int iviewer)
        {
            // Обновляем зуммированное изображение поля вывода
            int nview = m_ViewerDatas[iviewer].viewID;
            int nzoom = m_ViewerDatas[iviewer].zoomIndex;
            double zoomValue = m_ZoomSeries[nzoom];

            if (zoomValue == 1.0)
            {
                m_ViewerDatas[iviewer].zoomedImage = (Image)m_BufferedFrameViews[nview];
            }
            else
                throw new NotImplementedException("Unsupported zoom value!");

            // Обновляем изображение элемента управления
            m_ViewerRedraw(iviewer);
        }

        // Метод перерисовывает изображение на отдельном поле вывода
        private void m_ViewerRedraw(int iviewer)
        {
            // Наносим разметку на изображение
            Image buffer = (Image) m_ViewerDatas[iviewer].zoomedImage.Clone();

            // Выдаём размеченное изображение в поле вывода
            if (m_gui.DisplayCtrl != null)
            {
                m_gui.DisplayCtrl.SetViewerImage(iviewer, buffer);
            }
        }

        // отрисовка изображений на форме
        private void m_Refresh()
        {
            if (m_IsOpened)
            {
                for (int i = 0; i < m_ViewerDatas.Count(); i++)
                {
                    m_ViewerRedraw(i);
                }
            }
        }

        // Удаление полей для вывода изображений
        private void m_OnCameraClosed()
        {
//-- Перепроверить
            if (m_IsOpened)
            {
                // TODO: 1. Удаляем все поля для вывода изображений
                if (m_gui.DisplayCtrl != null)
                {
                    for (int i = 0; i < m_ViewerDatas.Count(); i++)
                        m_gui.DisplayCtrl.DelViewerImage(i);
                }

                m_BufferedFrameID = -1;
                m_ActiveViewerIndex = -1;
                m_IsOpened = false;
            }

//-- Перепроверить
        }

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        override protected bool DisplayCameraOpen(RecordingInfo rec)
        {
//-- Перепроверить
            if (MarkupCameraOpen(rec))
            {
                m_OnCameraOpened();
                return true;
            }
            else
                return false;
//-- Перепроверить
        }

        override protected void DisplayCameraClose()
        {
//-- Перепроверить
            MarkupCameraClose();
            m_OnCameraClosed();
//-- Перепроверить
        }

        override protected bool DisplayMarkupOpen(string MarkupFilePath)
        {
//-- Перепроверить
            if (MarkupOpen(MarkupFilePath))
            {
                m_Refresh();
                return true;
            }
            else
                return false;
//-- Перепроверить
        }

        override protected void DisplayMarkupClose()
        {
//-- Перепроверить
            MarkupClose();
            m_Refresh();
//-- Перепроверить
        }

        // Метод перерисовки кадров на форме
        override protected void DisplayRefresh()
        {
            m_Refresh();
        }

        // Метод буферизует все виды для указанного кадра
        override protected void DisplayLoadFrame(int frameIndex)
        {
//-- Перепроверить
            // Загружаем изображения видов кадра из видеофайлов
            List<Image> viewImages;
            CameraLoadFrame(frameIndex, out viewImages);
            for (int i = 0; i < viewImages.Count; i++)
            {
                m_BufferedFrameViews[i] = viewImages[i];
            }
            m_BufferedFrameID = frameIndex;

            // Обновляем содержимое всех полей вывода
            for (int i = 0; i < m_ViewerDatas.Count(); i++)
            {
                m_ViewerUpdateSettings(i);
            }
//-- Перепроверить
        }

        // Метод привязки элементов управления формы к объекту DisplayManager
        override public void DisplayGuiBind(DisplayManagerControls controls)
        {
//-- Перепроверить
            m_gui = controls;

            // Подключаем пользовательский элемент управления
            if (m_gui.DisplayCtrl != null)
            {
                // Инициализируем информацию для обслуживания полей вывода
                int nviewers = m_gui.DisplayCtrl.GetViewersCount();
                m_ViewerDatas = new ViewerData[nviewers];
                for (int i = 0; i < nviewers; i++)
                {
                    m_ViewerDatas[i].viewID = -1;
                    m_ViewerDatas[i].zoomedImage = null;
                    m_ViewerDatas[i].zoomIndex = -1;
                }

                // Регистрируем обработчик событий от полей вывода
                m_gui.DisplayCtrl.RunEvent += new DisplayControlWpf.
                    UserCanvasControl.canvasEventHandler(m_OnViewerEvent);
            }
//-- Перепроверить
        }

        // Инициализация всех полей начальными значениями
        public DisplayManager()
        {
            m_gui = new DisplayManagerControls();
//-- Перепроверить
            m_BufferedFrameID = -1;
//-- Перепроверить
        }
    }
}
