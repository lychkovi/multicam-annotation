/* DisplayManager: Класс реализует отображение кадров видео с отдельных
 * камер видеозаписи, отображение разметки объектов на этих кадрах и 
 * обработку событий мыши на поля для вывода изображений. 
 *
 * Внимание!!! Класс не реализует изменение разметки в базе данных, а
 * только передает соообщения о манипуляциях мышью пользователем в класс
 * TraceManager для внесения изменений в базу. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;       // Класс Image

using MarkupData;
using DisplayControlWpf;


namespace UniversalAnnotationApp
{
    public struct DisplayManagerControls
    {
        public DisplayControlWin.DisplayControl Ctrl;
    }

    // Класс представляет информацию для обслуживания отдельного
    // поля вывода изображения
    public struct ViewerData
    {
        public int ViewID;        // идентификатор текущего зума
        public int ZoomID;     // индекс текущего выбранного зума
        public Image ZoomedImage; // зуммированное изображение (без разметки)
    }

    public interface IDisplay
    {
        void DisplayGuiBind(DisplayManagerControls controls);
    }

    public delegate void displayCallback(
        object sender, DisplayCanvasEventArgs args);

    abstract class DisplayManagerBase : MarkupManager, IDisplay
    {
        // Метод привязки элементов управления формы к объекту DisplayManager
        abstract public void DisplayGuiBind(DisplayManagerControls controls);

        // Метод регистрации функции обратного вызова для передачи сообщений 
        // на верхний слой TraceManager
        abstract protected void DisplaySetCallback(displayCallback cbFcn);

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        abstract protected bool DisplayCameraOpen(RecordingInfo rec);
        abstract protected void DisplayCameraClose();
        abstract protected bool DisplayMarkupOpen(string MarkupFilePath);
        abstract protected void DisplayMarkupClose();

        // Метод перерисовки кадров на форме
        abstract protected void DisplayUpdate(
            DisplayCanvasModeID generalViewMode,
            bool isTraceSelected = false,
            int selectedTraceID = -1,
            DisplayCanvasModeID selectedViewMode =
                DisplayCanvasModeID.Passive);
        abstract protected void DisplayLoadFrame(int frameIndex);
    }

    class DisplayManager: DisplayManagerBase
    {
        private DisplayManagerControls m_gui; // связь с формой
        private displayCallback m_RaiseEvent; // связь с TraceManager

        private bool m_IsTraceSelected;     // наличие выбранной траектории
        private int m_SelectedTraceID;      // номер выбранной траектории
        private DisplayCanvasModeID m_SelectedViewMode; 
            // режим работы поля вывода выбранной траектории
        private DisplayCanvasModeID m_GeneralViewMode;
        // режим работы остальных полей вывода

        private int m_BufferedFrameID;      // номер буферизованного кадра
        private List<Image> m_ViewBuffers;   
            // буферы видов текущего кадра (по количеству видов)
        private ViewerData[] m_Viewers;
            // зуммированные мзображения для отдельных полей вывода 
            // изображений, а также индексы текущих выбранных зума и вида 
            // (по колву полей вывода)
        private double[] m_ZoomValues;  // ряд стандартных значений зума
        private string[] m_ZoomCaptions;// подписи значений зума
        private int m_ZoomDefaultID;        // индекс начального зума

        // Обработчик события от поля вывода разметки
        private void m_OnDisplayCanvasEvent(
            object sender, DisplayCanvasEventArgs e)
        {
            int viewID = m_Viewers[e.ControlID].ViewID;
            int zoomID = m_Viewers[e.ControlID].ZoomID;
            double zoomValue = m_ZoomValues[zoomID];

            DisplayCanvasEventArgs eTraceArgs;
            eTraceArgs = new DisplayCanvasEventArgs(e.EventID);
            eTraceArgs.ControlID = -1;      // не используется далее
            eTraceArgs.ViewID = viewID;
            eTraceArgs.clip.X = (int)(e.clip.X * zoomValue);
            eTraceArgs.clip.Y = (int)(e.clip.Y * zoomValue);
            eTraceArgs.clip.Width = (int)(e.clip.Width * zoomValue);
            eTraceArgs.clip.Height = (int)(e.clip.Height * zoomValue);
            eTraceArgs.HasBox = e.HasBox;

            // Обработку события и изменение состояния слоя DisplayManager
            // будет выполнять слой TraceManager
            m_RaiseEvent(sender, eTraceArgs);
        }

        // Метод обновляет изображение разметки в указанном поле визуализации
        // в зависимости от текущего режима, указанного в полях объекта this
        private void m_UpdateMarkup(int iviewer)
        {
            if (!CameraIsOpened)
                throw new Exception("Camera is not opened!");

            int viewID = m_Viewers[iviewer].ViewID;
            int zoomID = m_Viewers[iviewer].ZoomID;
            double zoomValue = m_ZoomValues[zoomID];
            double scale = 1.0 / zoomValue;

            // Признак выделения текущей выбранной траектории
            bool isSelectedBox = m_IsTraceSelected &&
                m_SelectedViewMode == DisplayCanvasModeID.BoxUpdate;

            Image markedFrame = 
                (Image) m_Viewers[iviewer].ZoomedImage.Clone();

            // Наносим разметку
            if (MarkupIsOpened)
            {
                Rectangle rect;
                List<Box> boxes;
                boxes = MarkupBoxGetByView(m_BufferedFrameID, viewID);

                Brush brush = new SolidBrush(Color.Red);
                Pen pen = new Pen(brush);
                Graphics g = Graphics.FromImage(markedFrame);
                foreach (Box box in boxes)
                if (!isSelectedBox || m_SelectedTraceID != box.TraceID)
                {
                    rect = box.GetRectangle(scale);
                    g.DrawRectangle(pen, rect);
                }
            }

            // Визуализируем изображение
            if (m_gui.Ctrl != null)
                m_gui.Ctrl.SetViewerImage(iviewer, markedFrame);
        }

        private void m_UpdateMarkups()
        {
            for (int i = 0; i < m_Viewers.Length; i++)
            {
                m_UpdateMarkup(i);
            }
        }

        // Метод буферизует новый кадр, зуммирует его и обновляет разметку
        void m_UpdateCamera(int frameID)
        {
            if (!CameraIsOpened)
                throw new Exception("DisplayManager: Camera is not opened!");

            // При необходимости буферизуем кадр
            if (m_BufferedFrameID != frameID)
            {
                CameraLoadFrame(frameID, out m_ViewBuffers);
                m_BufferedFrameID = frameID;
            }

            m_UpdateViews();
        }

        // Метод обновляет зуммированный кадр в буфере поля вывода и 
        // обновляет его разметку.
        private void m_UpdateView(int iviewer)
        {
            if (!CameraIsOpened)
                throw new Exception("DisplayManager: Camera is not opened!");

            int nview = m_Viewers[iviewer].ViewID;
            int nzoom = m_Viewers[iviewer].ZoomID;
            double scale = 1.0 / m_ZoomValues[nzoom];

            // Масштабируем изображение по текущему зуму
            CameraImageResize(m_ViewBuffers[nview], scale, 
                out m_Viewers[iviewer].ZoomedImage);

            m_UpdateMarkup(iviewer);
        }

        private void m_UpdateViews()
        {
            for (int i = 0; i < m_Viewers.Length; i++)
            {
                m_UpdateView(i);
            }
        }

        // Обработчик события от выпадающего списка
        private void m_OnDisplayListEvent(
            object sender, DisplayListEventArgs e)
        {
            switch (e.EventID)
            {
                case DisplayListEventID.ViewChanged:
                    m_Viewers[e.ControlID].ViewID = e.ListItemID;
                    m_UpdateView(e.ControlID);
                    break;
                case DisplayListEventID.ZoomChanged:
                    m_Viewers[e.ControlID].ZoomID = e.ListItemID;
                    m_UpdateView(e.ControlID);
                    break;
            }

            DisplayUpdate(m_GeneralViewMode, 
                m_IsTraceSelected, 
                m_SelectedTraceID,
                m_SelectedViewMode);
        }

        // Сброс полей в начальное состояние
        private void m_Reset(bool resetBuffer = true)
        {
            m_IsTraceSelected = false;
            m_SelectedTraceID = -1;
            m_SelectedViewMode = DisplayCanvasModeID.Passive;
            m_GeneralViewMode = DisplayCanvasModeID.Passive;
            if (resetBuffer) m_BufferedFrameID = -1;
        }

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        override protected bool DisplayCameraOpen(RecordingInfo rec)
        {
            if (CameraIsOpened)
                DisplayCameraClose();

            if (!MarkupCameraOpen(rec))
                return false;

            if (m_gui.Ctrl != null)
            {
                // Составляем список названий видов
                int nviews = CameraRecordingInfo.Views.Count;
                List<string> viewCaptions = new List<string>();
                for (int j = 0; j < nviews; j++)
                {
                    string name = CameraRecordingInfo.Views[j].Comment;
                    viewCaptions.Add(name);
                }

                // Резревируем память на буферы полей визуализации
                int nviewers = m_gui.Ctrl.GetViewersCount();
                if (m_Viewers.Count() != nviewers)
                    m_Viewers = new ViewerData[nviewers];

                // Инициализируем состояние каждого поля визуализации
                for (int i = 0; i < nviewers; i++)
                {
                    int viewID = i < nviews ? i : nviews - 1;
                    int zoomID = m_ZoomDefaultID;
                    m_gui.Ctrl.SetViewerListOfViews(i, viewCaptions, viewID);
                    m_gui.Ctrl.SetViewerListOfZooms(
                        i, m_ZoomCaptions.ToList(), zoomID);
                    m_Viewers[i].ViewID = viewID;
                    m_Viewers[i].ZoomID = zoomID;
                }
            }

            // Сброс полей в начальное состояние и буферизация первого кадра
            m_Reset(true);
            m_UpdateCamera(0);
            return true;
        }

        override protected void DisplayCameraClose()
        {
            if (CameraIsOpened)
            {
                MarkupCameraClose();
                // Очищаем все поля для вывода изображений
                if (m_gui.Ctrl != null)
                {
                    for (int i = 0; i < m_Viewers.Count(); i++)
                        m_gui.Ctrl.DelViewerImage(i);
                }
                m_Reset();
            }
        }

        override protected bool DisplayMarkupOpen(string MarkupFilePath)
        {
            if (MarkupIsOpened)
                DisplayMarkupClose();

            if (MarkupOpen(MarkupFilePath))
            {
                m_Reset(false);
                m_UpdateMarkups();
                DisplayUpdate(DisplayCanvasModeID.FocusPoint);
                return true;
            }
            else
                return false;
        }

        override protected void DisplayMarkupClose()
        {
            if (MarkupIsOpened)
            {
                MarkupClose();
                m_Reset(false);
                m_UpdateMarkups();
            }
        }

        // Метод перерисовки кадров на форме
        override protected void DisplayUpdate(
            DisplayCanvasModeID generalViewMode,
            bool isTraceSelected = false,
            int selectedTraceID = -1,
            DisplayCanvasModeID selectedViewMode = 
                DisplayCanvasModeID.Passive)
        {
            if (!CameraIsOpened)
                throw new Exception("DisplayManager: Camera is not opened!");

            // Проверять наличие выбранной траектории на текущем кадре здесь
            // не будем - это должна делать вызывающая функция из уровня 
            // TraceManager. 

            // Сохраняем настройки режимов во внутренние поля объекта
            m_IsTraceSelected = isTraceSelected;
            m_SelectedTraceID = selectedTraceID;
            m_GeneralViewMode = generalViewMode;
            m_SelectedViewMode = selectedViewMode;

            // Узнаем координаты рамки текущей выбранной траектории
            int selectedViewID = -1;
            Box box = new Box();
            bool isSuccess = false;
            if (isTraceSelected)
            {
                isSuccess = MarkupBoxGetByID(
                    m_SelectedTraceID, m_BufferedFrameID, out box);

                if (isSuccess)
                {
                    Trace trace;
                    MarkupTraceGetByID(box.TraceID, out trace);
                    selectedViewID = trace.ViewID;
                }
            }

            // Задаем режимы работы для полей вывода
            if (m_gui.Ctrl != null)
            for (int i = 0; i < m_Viewers.Length; i++)
            {
                if (isSuccess && m_Viewers[i].ViewID == selectedViewID)
                {
                    int zoomID = m_Viewers[i].ZoomID;
                    double scale = 1.0 / m_ZoomValues[zoomID];
                    Rectangle rect = box.GetRectangle(scale);
                    m_gui.Ctrl.SetViewerMode(i, m_SelectedViewMode, rect);
                }
                else
                    m_gui.Ctrl.SetViewerMode(i, m_GeneralViewMode);
            }

            // Обновляем разметку кадра
            m_UpdateMarkups();
        }

        // Метод буферизует все виды для указанного кадра
        override protected void DisplayLoadFrame(int frameID)
        {
            if (!CameraIsOpened)
                throw new Exception("DisplayManager: Camera is not opened!");

            // Буферизуем кадр
            m_UpdateCamera(frameID);

            // Обновляем режимы полей визуализации
            DisplayUpdate(m_GeneralViewMode, m_IsTraceSelected, 
                m_SelectedTraceID, m_SelectedViewMode);
        }

        // Метод привязки элементов управления формы к объекту DisplayManager
        override public void DisplayGuiBind(DisplayManagerControls controls)
        {
            m_gui = controls;

            // Подключаем пользовательский элемент управления
            if (m_gui.Ctrl != null)
            {
                // Инициализируем информацию для обслуживания полей вывода
                int nviewers = m_gui.Ctrl.GetViewersCount();
                m_Viewers = new ViewerData[nviewers];

                // Регистрируем обработчик событий от полей вывода
                m_gui.Ctrl.RunCanvasEvent += new UserCanvasControl.
                    canvasEventHandler(m_OnDisplayCanvasEvent);

                // Регистрируем обработчик событий от списка
                m_gui.Ctrl.RunListEvent += new UserCanvasControl.
                    listEventHandler(m_OnDisplayListEvent);
            }
        }

        // Метод регистрации функции обратного вызова для передачи сообщений 
        // на верхний слой TraceManager
        override protected void DisplaySetCallback(displayCallback cbFcn)
        {
            m_RaiseEvent = cbFcn;
        }

        // Инициализация всех полей начальными значениями
        public DisplayManager()
        {
            m_gui = new DisplayManagerControls();
            m_ViewBuffers = new List<Image>();
            m_ZoomValues = new double[] {0.5, 1.0, 2.0, 3.0, 4.0 };
            m_ZoomCaptions = new string[] { "1/2x", "1x", "2x", "3x", "4x" };
            m_ZoomDefaultID = 1; // 1x
            m_Reset();
        }
    }
}
