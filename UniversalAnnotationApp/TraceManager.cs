/* TraceManager: Класс представляет сведения о номере текущего кадра, текущей
 * выбранной траектории, а также хранит состояние взаимодействия пользователя
 * с интерфейсом в соответствии со сценариями взаимодействия.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MarkupData;
using DisplayControlWpf;    // режимы работы и события дисплея


namespace UniversalAnnotationApp
{
    public class TraceManagerControls
    {
        // Панель навигации
        public GroupBox grpNavigation;
        public TextBox txtNaviCurrFrame;
        public Button btnNaviGotoFrame;
        public TextBox txtNaviGotoFrame;
        public TextBox txtNaviTotalFrames;
        public Button btnNaviPrevious;
        public Button btnNaviNext;
        public CheckBox chkNaviPlayReversed;
        public ComboBox cmbNaviPlaySpeed;
        public Button btnNaviPlayStop;
        public RadioButton radNaviBoxMajor;     // радиокнопки рамка-маркер
        public RadioButton radNaviMarkerMajor;
        public TrackBar trbNaviSlider;
        public Timer tmrPlayTimer;
    }

    public interface ITrace
    {
        void TraceGuiBind(TraceManagerControls controls);
        void TraceTraceCreate(); // для вызова из меню
        void TraceTraceDelete(); // для вызова из меню
    }

    abstract class TraceManagerBase : DisplayManager, ITrace
    {
        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        abstract protected bool TraceCameraOpen(RecordingInfo rec);
        abstract protected void TraceCameraClose();
        abstract protected bool TraceMarkupOpen(string MarkupFilePath);
        abstract protected void TraceMarkupClose();

        // Навигация
        abstract protected void TraceMoveToFrame(int frameID);

        // Создание/удаление траектории
        abstract public void TraceTraceCreate(); // для вызова из меню
        abstract public void TraceTraceDelete(); // для вызова из меню

        abstract public void TraceGuiBind(TraceManagerControls controls);
    }


    class TraceManager: TraceManagerBase
    {
        private TraceManagerControls m_gui;

        // Атрибуты текущей выбранной таектории
        private bool m_IsTraceSelected;
        private Trace m_CurrTrace;
        private Box m_CurrBox;       // если m_CurrTrace.HasBox == true
        private Marker m_CurrMarker; // если m_CurrTrace.HasBox == false
        private Tag m_CurrTag;
        private Category m_CurrCategory;
        private bool m_IsCategoryEdit;

        private int m_CurrFrameID;
        private bool m_IsBoxMajor;

        // Воспроизведение видеозаписи
        private bool m_IsPlaybackMode;
        private int m_PlayDir;       // (+1) - вперед, (-1) - назад
        private List<string> m_PlaySpeedCaptions;
        private List<double> m_PlaySpeedValues;
        private int m_PlaySpeedDefaultID;
        double m_PlaySpeed;
        private bool m_IsPlayTimerLocked;

        // Выбрать траекторию N
        private bool m_TraceSelect(int traceID)
        {
            // Обновляем поля объекта
            if (!MarkupTraceGetByID(traceID, out m_CurrTrace))
            {
                m_TraceUnSelect();
                return false;
            }

            if (m_CurrTrace.HasBox)
            {
                if (!MarkupBoxGetByID(traceID, m_CurrFrameID, out m_CurrBox))
                {
                    m_TraceUnSelect();
                    return false;
                }
            }
            else
            {
                if (!MarkupMarkerGetByID(
                    traceID, m_CurrFrameID, out m_CurrMarker))
                {
                    m_TraceUnSelect();
                    return false;
                }
            }

            if (!MarkupTagGetByID(m_CurrTrace.TagID, out m_CurrTag))
                throw new Exception("Tag not found!");

            if (!MarkupCategoryGetByID(
                m_CurrTag.CategoryID, out m_CurrCategory))
                throw new Exception("Category not found!");

            m_IsTraceSelected = true;

            // Обновляем состояние полей визуализации кадра
            DisplayCanvasModeID genMode;
            DisplayCanvasModeID selMode;
            genMode = DisplayCanvasModeID.FocusPoint;
            if (m_CurrTrace.HasBox)
                selMode = DisplayCanvasModeID.BoxUpdate;
            else
                selMode = DisplayCanvasModeID.MarkerUpdate;
            DisplayUpdate(genMode, true, traceID, selMode);

            // Обновляем состояние панелей свойств категории, рамки и т. п.
            m_ControlsUpdate();
            return true;
        }

        // Отменить выбор траектории
        private void m_TraceUnSelect()
        {
            if (m_IsTraceSelected)
            {
                m_IsTraceSelected = false;
                if (MarkupIsOpened)
                    DisplayUpdate(DisplayCanvasModeID.FocusPoint);
                else
                    DisplayUpdate(DisplayCanvasModeID.Passive);
                m_ControlsUpdate();
            }
        }

        // Обслуживание элементов графического интерфейса
        private void m_ControlsUpdate()
        {
            if (m_gui != null)
            {
                m_ControlNaviUpdate();
            }
        }

        private void m_ControlsInit()
        {
            if (m_gui != null)
            {
                m_ControlNaviInit();
            }
        }

        // ************************** Навигация *****************************
        private void m_PlaybackStop()
        {

        }

        override protected void TraceMoveToFrame(int frameID)
        {
            if (!CameraIsOpened)
                throw new Exception("Camera is not opened!");

            if (frameID < 0 || frameID >= CameraRecordingInfo.FramesCount)
                throw new Exception("Frame number is out of bounds!");

            if (frameID == m_CurrFrameID) return;

            DisplayLoadFrame(frameID);
            m_CurrFrameID = frameID;

            // Проверяем, присутствует ли текущая выбранная траектория на 
            // интересующем кадре и соответственно обновляем элементы 
            // управления на форме
            if (MarkupIsOpened && m_IsTraceSelected && 
                m_CurrTrace.FrameStart >= frameID && 
                m_CurrTrace.FrameEnd <= frameID)
            {
                m_TraceSelect(m_CurrTrace.ID);
            }
            else
            {
                m_TraceUnSelect();
            }
        }

        // Обработчик изменения полосы прокрутки времени
        private void m_ControlNavi_OnSliderChange(object sender, EventArgs e)
        {
            int frameID = m_gui.trbNaviSlider.Value;
            TraceMoveToFrame(frameID);
        }

        // Обработчик нажатия на кнопку перехода к заданному кадру
        private void m_ControlNavi_OnGotoFrameClick(
            object sender, EventArgs e)
        {
            int frameID;

            if (!int.TryParse(m_gui.txtNaviGotoFrame.Text, out frameID))
            {
                MessageBox.Show("Please, enter numeric integer value!");
                return;
            }

            try
            {
                TraceMoveToFrame(frameID);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        // Обработчик нажатия на кнопку "Next"
        private void m_ControlNavi_OnNextClick(object sender, EventArgs e)
        {
            int frameID = m_CurrFrameID + 1;
            if (frameID < CameraRecordingInfo.FramesCount)
                TraceMoveToFrame(frameID);
        }

        // Обработчик нажатия на кнопку "Previous"
        private void m_ControlNavi_OnPreviousClick(object sender,EventArgs e)
        {
            int frameID = m_CurrFrameID - 1;
            if (frameID >= 0)
                TraceMoveToFrame(frameID);
        }

        // Обработчик нажатия на галочку Play Direction Reversed
        private void m_ControlNavi_OnPlayReversedChecked(
            object sender, EventArgs e)
        {
            m_PlayDir = (m_gui.chkNaviPlayReversed.Checked) ? -1 : +1 ;
        }

        // Обновление содержания панели навигации на форме
        private void m_ControlNaviUpdate()
        {
            if (m_gui == null) return;

            if (CameraIsOpened)
            {
                m_gui.grpNavigation.Enabled = true;
                m_gui.txtNaviCurrFrame.Text = m_CurrFrameID.ToString();
                m_gui.trbNaviSlider.Scroll -=
                    new EventHandler(m_ControlNavi_OnSliderChange);
                m_gui.trbNaviSlider.Value = m_CurrFrameID;
                m_gui.trbNaviSlider.Scroll +=
                    new EventHandler(m_ControlNavi_OnSliderChange);
                if (m_IsPlaybackMode)
                {
                    m_gui.btnNaviGotoFrame.Enabled = false;
                    m_gui.btnNaviPrevious.Enabled = false;
                    m_gui.btnNaviNext.Enabled = false;
                    m_gui.btnNaviPlayStop.Text = "Stop";
                    m_gui.trbNaviSlider.Enabled = false;
                }
                else
                {
                    m_gui.btnNaviGotoFrame.Enabled = true;
                    m_gui.btnNaviPrevious.Enabled = (m_CurrFrameID >= 1);
                    m_gui.btnNaviNext.Enabled = (m_CurrFrameID <
                        CameraRecordingInfo.FramesCount - 1);
                    m_gui.btnNaviPlayStop.Text = "Play";
                    m_gui.trbNaviSlider.Enabled = true;
                }
            }
            else
            {
                m_gui.grpNavigation.Enabled = false;
                m_gui.trbNaviSlider.Enabled = false;
            }
        }

        // Начальная инициализация содержания панели навигации на форме
        private void m_ControlNaviInit()
        {
            if (m_gui == null) return;

            if (CameraIsOpened)
            {
                int nframes = CameraRecordingInfo.FramesCount;
                m_gui.txtNaviTotalFrames.Text = nframes.ToString();

                // Инициализируем полосу прокрутки
                if (m_gui.trbNaviSlider.Maximum != nframes)
                {
                    m_gui.trbNaviSlider.Scroll -= 
                        new EventHandler(m_ControlNavi_OnSliderChange);
                    m_gui.trbNaviSlider.Maximum = nframes;
                    m_gui.trbNaviSlider.LargeChange = Math.Max(1,nframes/20);
                    m_gui.trbNaviSlider.SmallChange = 1;
                    m_gui.trbNaviSlider.Scroll +=
                        new EventHandler(m_ControlNavi_OnSliderChange);
                }
            }
            m_ControlNaviUpdate();
        }

        // ***************** Общие манипуляции с траекторией ****************

        // Метод переводит пользовательский элемент управления в режим
        // ожидания ввода первого узла траектории
        private void m_TraceCreateBegin(bool hasBox)
        {
            if (!MarkupIsOpened || m_IsPlaybackMode) return;

            // На всякий случай снимаем выделение текущей траектории
            m_TraceUnSelect();

            // Изменяем состояние дисплея
            if (hasBox)
                DisplayUpdate(DisplayCanvasModeID.BoxCreate);
            else
                DisplayUpdate(DisplayCanvasModeID.MarkerCreate);
        }

        // Метод выполняет создание траектории на основе исходных данных,
        // полученных от обработчика события пользовательского элемента
        // управления
        private void m_TraceCreateEnd(DisplayCanvasEventArgs args)
        {
            if (!MarkupIsOpened || m_IsPlaybackMode) return;

            // Регистрируем новую траекторию в базе
            Trace trace = new Trace();
            trace.ViewID = args.ViewID;
            trace.TagID = 0;
            trace.FrameStart = m_CurrFrameID;
            trace.FrameEnd = m_CurrFrameID;
            trace.HasBox = args.HasBox;
            int traceID = MarkupTraceCreate(trace);

            // Регистрируем новую рамку или маркер траектории в базе
            if (args.HasBox)
            {
                Box box = new Box();
                box.TraceID = traceID;
                box.FrameID = m_CurrFrameID;
                box.PosX = args.clip.Left;
                box.PosY = args.clip.Top;
                box.Width = args.clip.Width;
                box.Height = args.clip.Height;
                box.IsOccluded = false;
                box.IsShaded = false;
                MarkupBoxCreate(box);
            }
            else
            {
                Marker marker = new Marker();
                marker.TraceID = traceID;
                marker.FrameID = m_CurrFrameID;
                marker.PosX = args.clip.Left;
                marker.PosY = args.clip.Top;
                marker.IsShaded = false;
                MarkupMarkerCreate(marker);
            }

            // Отмечаем созданную траекторию как текущую выбранную
            m_TraceSelect(traceID);
        }

        // Метод удаляет текущую выбранную траекторию
        private void m_TraceDelete()
        {
            if (!MarkupIsOpened || m_IsPlaybackMode) return;

            if (!m_IsTraceSelected) return;

            // Удаляем все узлы траектории
            for (int frameID = m_CurrTrace.FrameStart;
                frameID <= m_CurrTrace.FrameEnd; frameID++)
            {
                if (m_CurrTrace.HasBox)
                    MarkupBoxDelete(m_CurrTrace.ID, frameID);
                else
                    MarkupMarkerDelete(m_CurrTrace.ID, frameID);
            }

            // Удаляем саму траекторию
            MarkupTraceDelete(m_CurrTrace.ID);
            m_TraceUnSelect();
        }

        // Метод выделяет узел траектории на текущем поле вывода на основе
        // данных, полученных обработчиком события от пользовательского
        // элемента управления, и базы данных разметки. Приоритет выбора
        // рамки или маркера определяется радоикнопками Navi. 
        // 
        // Точка фокуса задается в полях (args.clip.Left, args.clip.Top)
        // Размеры области выделения маркеров задаются в полях
        // (args.clip.Width, args.clip.Height). 
        private void m_TraceNodeSelect(DisplayCanvasEventArgs args)
        {
            if (!MarkupIsOpened || m_IsPlaybackMode) return;

            // 1. Отыскиваем ближайшую рамку, в которую попадает точка фокуса
            // c координатами (args.clip.Left, args.clip.Top)
            List<Box> boxes;
            bool boxFound = false;
            int nearestBoxDistance = 0;
            Box nearestBox = new Box();

            boxes = MarkupBoxGetByView(m_CurrFrameID, args.ViewID);
            for (int i = 0; i < boxes.Count; i++)
            {
                // Вычисляем расстояние от точки фокуса до центра по 
                // отдельным осям
                int distX = Math.Abs(args.clip.Left - boxes[i].PosX 
                    - boxes[i].Width / 2);
                int distY = Math.Abs(args.clip.Top - boxes[i].PosY
                    - boxes[i].Height / 2);
                int dist = distX + distY;

                // Проверяем, попадает ли точка фокуса в рамку
                if (distX <= boxes[i].Width/2 && distY <= boxes[i].Height/2)
                {
                    if (!boxFound || dist < nearestBoxDistance)
                    {
                        nearestBoxDistance = dist;
                        nearestBox = boxes[i];
                        boxFound = true;
                    }
                }
            }

            // 2. Отыскиваем ближайший к точке фокуса маркер, попадающий
            // в радиус (args.clip.Width, args.clip.Height)
            List<Marker> markers;
            bool markerFound = false;
            int nearestMarkerDistance = 0;
            Marker nearestMarker = new Marker();

            markers = MarkupMarkerGetByView(m_CurrFrameID, args.ViewID);
            for (int i = 0; i < markers.Count; i++)
            {
                int distX = Math.Abs(args.clip.Left - markers[i].PosX);
                int distY = Math.Abs(args.clip.Top - markers[i].PosY);
                int dist = distX + distY;

                // Проверяем, попадает ли маркер в заданный радиус
                if (distX <= args.clip.Width && distY <= args.clip.Height)
                {
                    if (!markerFound || dist <= nearestMarkerDistance)
                    {
                        nearestMarkerDistance = dist;
                        nearestMarker = markers[i];
                        markerFound = true;
                    }
                }
            }

            // 3. Выбираем траекторию
            if (boxFound)
            {
                if (markerFound && !m_IsBoxMajor)
                {
                    m_TraceSelect(nearestMarker.TraceID);
                }
                else
                    m_TraceSelect(nearestBox.TraceID);
            }
            else if (markerFound)
                m_TraceSelect(nearestMarker.TraceID);
            else
                m_TraceUnSelect();
        }

        // Метод обновляет положение узла траектории на основе данных,
        // полученных из обработчика событий пользовательского элемента
        private void m_TraceNodeUpdate(DisplayCanvasEventArgs args)
        {
            if (!m_IsTraceSelected || m_IsPlaybackMode) return;

            if (m_CurrTrace.HasBox)
            {
                if (!MarkupBoxGetByID(m_CurrTrace.ID, m_CurrFrameID,
                    out m_CurrBox))
                    throw new Exception("Box not found!");
                m_CurrBox.PosX = args.clip.Left;
                m_CurrBox.PosY = args.clip.Top;
                m_CurrBox.Width = args.clip.Width;
                m_CurrBox.Height = args.clip.Height;
                MarkupBoxUpdate(m_CurrBox);
                // TODO: m_ControlBoxUpdate()
                m_ControlsUpdate();
            }
            else
            {
                if (!MarkupMarkerGetByID(m_CurrTrace.ID, m_CurrFrameID,
                    out m_CurrMarker))
                    throw new Exception("Marker not found!");
                m_CurrMarker.PosX = args.clip.Left;
                m_CurrMarker.PosY = args.clip.Top;
                MarkupMarkerUpdate(m_CurrMarker);
                // TODO: m_ControlMarkerUpdate()
                m_ControlsUpdate();
            }
        }

        // Обработчик событий от слоя DisplayManager для манипуляции с 
        // траекториями.
        private void m_TraceOnDisplayEvent(
            object sender, DisplayCanvasEventArgs args)
        {
            if (!MarkupIsOpened || m_IsPlaybackMode) return;

            switch (args.EventID)
            {
                case DisplayCanvasEventID.NodeCreated:
                    // Создан первый узел новой траектории
                    m_TraceCreateEnd(args);
                    break;
                case DisplayCanvasEventID.NodeUpdated:
                    // Изменен текущий узел выбранной траектории
                    if (m_IsTraceSelected)
                    {
                        // Обновляем координаты узла траектории
                        m_TraceNodeUpdate(args);
                    }
                    break;
                case DisplayCanvasEventID.FocusPointed:
                    // Проверяем, нет ли в окрестности выбранной точки
                    // каких-либо узлов траектории, а если есть, то 
                    // выбираем соответствующую траекторию
                    m_TraceNodeSelect(args);
                    break;
            }
        }

        // ******************* Открытые методы класса ********************
        // Такие четыре метода должны быть у всех слоев выше слоя Markup. 
        // Слои вызывают эти методы рекурсивно по цепочке
        override protected bool TraceCameraOpen(RecordingInfo rec)
        {
            if (CameraIsOpened)
                TraceCameraClose();

            if (DisplayCameraOpen(rec))
            {
                TraceMoveToFrame(0);
                m_ControlsInit();
                return true;
            }
            else
                return false;
        }

        override protected void TraceCameraClose()
        {
            if (CameraIsOpened)
            {
                m_PlaybackStop();
                TraceMarkupClose();
                DisplayCameraClose();
                m_CurrFrameID = -1;
                m_ControlsInit();
            }
        }

        override protected bool TraceMarkupOpen(string MarkupFilePath)
        {
            if (MarkupIsOpened)
                TraceMarkupClose();

            if (DisplayMarkupOpen(MarkupFilePath))
            {
                // Загружаем тэг и категорию по умолчанию
                MarkupCategoryGetByID(0, out m_CurrCategory);
                MarkupTagGetByID(0, out m_CurrTag);
                m_ControlsInit();
                return true;
            }
            else
                return false;
        }

        override protected void TraceMarkupClose()
        {
            if (MarkupIsOpened)
            {
                m_TraceUnSelect();
                DisplayMarkupClose();
                m_IsCategoryEdit = false;
                m_ControlsInit();
            }
        }

        // Создание/удаление траектории
        override public void TraceTraceCreate() // для вызова из меню
        {
            if (!MarkupIsOpened)
            {
                MessageBox.Show("Please open markup first!", "ERROR",
                    MessageBoxButtons.OK);
                return;
            }
            m_TraceCreateBegin(m_IsBoxMajor);
        }

        override public void TraceTraceDelete() // для вызова из меню
        {
            if (m_IsTraceSelected)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure that you want to delete this trace?",
                    "WARNING", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    m_TraceDelete();
            }
            else
            {
                MessageBox.Show("Please select trace first!", "ERROR",
                    MessageBoxButtons.OK);
            }
        }

        /* Регистрация эелементов управления: ползунка, кнопок, 
         * выпадающих списков и т. п. */
        override public void TraceGuiBind(TraceManagerControls controls)
        {
            m_gui = controls;

            // Панель Navigation
            m_gui.btnNaviGotoFrame.Click += 
                new EventHandler(m_ControlNavi_OnGotoFrameClick);
            m_gui.btnNaviPrevious.Click +=
                new EventHandler(m_ControlNavi_OnPreviousClick);
            m_gui.btnNaviNext.Click +=
                new EventHandler(m_ControlNavi_OnNextClick);

            m_gui.chkNaviPlayReversed.Checked = (m_PlayDir < 0);
            m_gui.chkNaviPlayReversed.CheckedChanged +=
                new EventHandler(m_ControlNavi_OnPlayReversedChecked);

            for (int i = 0; i < m_PlaySpeedValues.Count; i++)
                m_gui.cmbNaviPlaySpeed.Items.Add(m_PlaySpeedCaptions[i]);
            m_gui.cmbNaviPlaySpeed.SelectedIndex = m_PlaySpeedDefaultID;
            //m_gui.cmbNaviPlaySpeed.SelectedIndexChanged +=
            //    new EventHandler(m_ControlNavi_OnPlaySpeedChanged);
        }

        public TraceManager()
        {
            // Инициализируем все поля
            m_IsTraceSelected = false;
            m_CurrTrace = new Trace();
            m_CurrBox = new Box();
            m_CurrMarker = new Marker();
            m_CurrTag = new Tag();
            m_CurrCategory = new Category();
            m_IsCategoryEdit = false;
            m_IsBoxMajor = true;

            m_CurrFrameID = -1;

            // Параметры воспроизвдения
            m_IsPlaybackMode = false;
            m_IsPlayTimerLocked = false;
            m_PlayDir = 1;
            string[] playSpeedCaptions = new string[] 
                {"1/4x", "1/2x", "1x", "2x", "4x"};
            m_PlaySpeedCaptions = playSpeedCaptions.ToList();
            double[] playSpeedValues = new double[] 
                {0.25, 0.5, 1.0, 2.0, 4.0};
            m_PlaySpeedValues = playSpeedValues.ToList();
            m_PlaySpeedDefaultID = 2;
            m_PlaySpeed = m_PlaySpeedValues[m_PlaySpeedDefaultID];

            // Это должно инициализироваться методом TraceGuiBind
            m_gui = null;

            // Регистрируем обработчик событий для слоя DisplayManager
            DisplaySetCallback(m_TraceOnDisplayEvent);
        }
    }
}
