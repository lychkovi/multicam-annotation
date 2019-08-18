/* TraceManager: Класс представляет сведения о номере текущего кадра, текущей
 * выбранной траектории, а также хранит состояние взаимодействия пользователя
 * с интерфейсом в соответствии со сценариями взаимодействия.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;       // class Image

using MarkupData;
using DisplayControlWpf;    // режимы работы и события дисплея


namespace UniversalAnnotationApp
{
    public enum TrackingMethodID
    {
        OnlyAttrUpdate,     // только обновление свойств IsOccluded и 
                            // IsShaded для существующей траектории
        Manual,             // простое копирование текущей рамки
        KLT,                // Tomasi-Lucas-Kanade
        TLD                 // часть алгоритма Track-Learn-Detect
    }

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
        public CheckBox chkNaviPlayReverse;
        public ComboBox cmbNaviPlaySpeed;
        public Button btnNaviPlayStop;
        public RadioButton radNaviBoxMajor;     // радиокнопки рамка-маркер
        public RadioButton radNaviMarkerMajor;
        public TrackBar trbNaviSlider;
        public Timer tmrPlayTimer;

        // Панель отслеживания объекта
        public GroupBox grpTracking;
        public ComboBox cmbTrackingMethod;
        public CheckBox chkTrackingReverse;
        public CheckBox chkTrackingIsOccluded;
        public CheckBox chkTrackingIsShaded;
        public Button btnTrackingSeekExtent;
        public Button btnTrackingTruncate;
        public Button btnTrackingTrack;

        // Панель траектории
        public GroupBox grpTrace;
        public TextBox txtTraceID;
        public ComboBox cmbTraceTagID;
        public TextBox txtTraceFrameStart;
        public TextBox txtTraceFrameEnd;

        // Панель узла траектории Box/Marker
        public GroupBox grpNode;
        public TextBox txtNodePosX;
        public TextBox txtNodePosY;
        public TextBox txtNodeWidth;
        public TextBox txtNodeHeight;
        public CheckBox chkNodeIsOccluded;
        public CheckBox chkNodeIsShaded;

        // Панель тэга
        public GroupBox grpTag;
        public ComboBox cmbTagID;
        public TextBox txtTagName;
        public ComboBox cmbTagCategoryID;
        public Button btnTagNew;
        public Button btnTagDelete;
        public Button btnTagEditSave;

        // Панель категории
        public GroupBox grpCategory;
        public ComboBox cmbCategoryID;
        public TextBox txtCategoryName;
        public Button btnCategoryNew;
        public Button btnCategoryDelete;
        public Button btnCategoryEditSave;
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

        // Отслеживание объекта на видеозаписи
        private List<TrackingMethodID> m_TrackingMethodValues;
        private List<string> m_TrackingMethodNames;
        private int m_TrackingMethodDefaultID;
        private TrackingMethodID m_TrackingMethod;
        private int m_TrackingDir;   // (+1) - вперед, (-1) - назад
        private bool m_TrackingIsOccluded;
        private bool m_TrackingIsShaded;

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
            }
            m_ControlsUpdate();
        }

        // Обслуживание элементов графического интерфейса
        private void m_ControlsUpdate()
        {
            if (m_gui != null)
            {
                m_ControlNaviUpdate();
                m_ControlTrackingUpdate();
                m_ControlCategoryUpdate();
            }
        }

        private void m_ControlsInit()
        {
            if (m_gui != null)
            {
                m_ControlNaviInit();
                m_ControlTrackingInit();
                m_ControlCategoryInit();
            }
        }

        // ************************** Навигация *****************************
        private void m_PlaybackStart()
        {

        }

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
            if (m_IsTraceSelected && 
                frameID >= m_CurrTrace.FrameStart && 
                frameID <= m_CurrTrace.FrameEnd)
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
        private void m_ControlNavi_OnPlayReverseChange(
            object sender, EventArgs e)
        {
            m_PlayDir = (m_gui.chkNaviPlayReverse.Checked) ? (-1) : (+1);
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

        // ***************** Отслеживание объекта на видеозаписи ************

        // Метод переходит к крайнему узлу траектории
        private void m_TrackingSeekExtent()
        {
            if (!m_IsTraceSelected || m_IsPlaybackMode) return;
            
            if (m_TrackingDir > 0)
                TraceMoveToFrame(m_CurrTrace.FrameEnd);
            else
                TraceMoveToFrame(m_CurrTrace.FrameStart);
        }

        // Метод обрезает хвост траектории, начиная со следующего кадра
        private void m_TrackingTruncate()
        {
            if (!m_IsTraceSelected || m_IsPlaybackMode) return;

            DialogResult result = MessageBox.Show(
                "Are you sure that you want to truncate selected trace " +
                (m_TrackingDir > 0 ? "forward?" : "backward?"), 
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            // Удаляем все узлы траектории, начиная со следующего кадра до
            // конца траектории
            for (int i = m_CurrFrameID + m_TrackingDir;
                i <= m_CurrTrace.FrameEnd && i >= m_CurrTrace.FrameStart;
                i += m_TrackingDir)
            {
                if (m_CurrTrace.HasBox)
                    MarkupBoxDelete(m_CurrTrace.ID, i);
                else
                    MarkupMarkerDelete(m_CurrTrace.ID, i);
            }

            // Изменяем границы траектории
            if (m_TrackingDir > 0)
                m_CurrTrace.FrameEnd = m_CurrFrameID;
            else
                m_CurrTrace.FrameStart = m_CurrFrameID; 
            MarkupTraceUpdate(m_CurrTrace);

            m_ControlsUpdate();
        }

        // Метод реализует расчет положения рамки объекта на следующем кадре
        // по её известным координатам на текущем кадре
        private void m_TrackingTrack()
        {
            bool onlyAttrUpdate = false;
            if (m_TrackingMethod == TrackingMethodID.OnlyAttrUpdate)
                onlyAttrUpdate = true;

            // 1. Проверяем выход следующего кадра за границы видеозаписи
            int nextFrameID = m_CurrFrameID + m_TrackingDir;
            if (!onlyAttrUpdate && (nextFrameID < 0 ||
                nextFrameID >= CameraRecordingInfo.FramesCount))
                return;

            // 2. Применяем атрибуты рамки объекта
            if (m_CurrTrace.HasBox && m_gui != null)
            {
                m_CurrBox.IsOccluded = m_TrackingIsOccluded;
                m_CurrBox.IsShaded = m_TrackingIsShaded;
                MarkupBoxUpdate(m_CurrBox);
            }
            
            // 3. Запрашиваем изображение текущего и следующего кадров из
            // видеозаписи для последующего отслеживания объекта
            List<Image> currFrame, nextFrame;
            Image currImage, nextImage;
            switch (m_TrackingMethod)
            {
                case TrackingMethodID.OnlyAttrUpdate:
                case TrackingMethodID.Manual:
                    // Обрабатывать изображения не потребуется
                    currImage = new Bitmap(1, 1);
                    nextImage = new Bitmap(1, 1);
                    break;
                default:
                    // Нужно загрузить изображения
                    CameraLoadFrame(m_CurrFrameID, out currFrame);
                    CameraLoadFrame(nextFrameID, out nextFrame);
                    currImage = currFrame[m_CurrTrace.ViewID];
                    nextImage = nextFrame[m_CurrTrace.ViewID];
                    break;
            }

            // 4. Вычисляем координаты следущего узла траектории
            Box nextBox = new Box();
            Marker nextMarker = new Marker();
            try
            {
                switch (m_TrackingMethod)
                {
                    case TrackingMethodID.OnlyAttrUpdate:
                        // Ничего не надо делать
                        break;
                    case TrackingMethodID.Manual:
                        // Копируем координаты рамки на текущем кадре
                        if (m_CurrTrace.HasBox)
                        {
                            nextBox = m_CurrBox;
                            nextBox.FrameID = nextFrameID;
                        }
                        else
                        {
                            nextMarker = m_CurrMarker;
                            nextMarker.FrameID = nextFrameID;
                        }
                        break;
                    case TrackingMethodID.TLD:
                        if (m_CurrTrace.HasBox)
                            throw new Exception("TLD not implemented!");
                        else
                            throw new Exception("Trace should have box!");
                    default:
                        throw new Exception("Unsupported method!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // 5. Сохраняем изменения в базу
            // признак изменения внутреннего узла траектории
            bool updateNodeInner =
                (nextFrameID >= m_CurrTrace.FrameStart &&
                    nextFrameID <= m_CurrTrace.FrameEnd);

            // признак добавления нового узла в конец траектории
            bool addNodeToEnd =
                (nextFrameID == m_CurrTrace.FrameEnd + 1);

            // признак добавления нового узла в начало траектории
            bool addNodeToStart =
                (nextFrameID == m_CurrTrace.FrameStart - 1);

            if (!onlyAttrUpdate) try
            {
                if (updateNodeInner)
                {
                    // Нужно обновить существующий узел траектории
                    if (m_CurrTrace.HasBox)
                        MarkupBoxUpdate(nextBox);
                    else
                        MarkupMarkerUpdate(nextMarker);
                }
                else
                {
                    if (addNodeToEnd || addNodeToStart)
                    {
                        // Создаем новый узел траектории
                        if (m_CurrTrace.HasBox)
                            MarkupBoxCreate(nextBox);
                        else
                            MarkupMarkerCreate(nextMarker);

                        // Обновляем границы траектории
                        if (addNodeToStart)
                            m_CurrTrace.FrameStart--;
                        else if (addNodeToEnd)
                            m_CurrTrace.FrameEnd++;
                        MarkupTraceUpdate(m_CurrTrace);
                    }
                    else
                        throw new Exception("Inconsistent trace detected!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // 6. Переходим к следующему кадру
            if (updateNodeInner || !onlyAttrUpdate)
                TraceMoveToFrame(nextFrameID);
            else
                m_ControlsUpdate();
        }

        // Обработчик изменения атрибута Is Occluded
        private void m_ControlTracking_OnOccludedChange(
            object sender, EventArgs e)
        {
            m_TrackingIsOccluded = m_gui.chkTrackingIsOccluded.Checked;
        }

        // Обработчик изменения атрибута Is Shaded
        private void m_ControlTracking_OnShadedChange(
            object sender, EventArgs e)
        {
            m_TrackingIsShaded = m_gui.chkTrackingIsShaded.Checked;
        }

        // Обработчик изменения направления отслеживания
        private void m_ControlTracking_OnReverseChanged(
            object sender, EventArgs e)
        {
            m_TrackingDir = m_gui.chkTrackingReverse.Checked ? (-1) : (+1);
            m_ControlTrackingUpdate();
        }

        // Обработчик выбора метода отслеживания из выпадающего списка
        private void m_ControlTracking_OnMethodChanged(
            object sender, EventArgs e)
        {
            int index = m_gui.cmbTrackingMethod.SelectedIndex;
            m_TrackingMethod = m_TrackingMethodValues[index];
        }

        // Обработчик нажатия на кнопку "Seek Trace End/Start"
        private void m_ControlTracking_OnSeekExtentClick(
            object sender, EventArgs e)
        {
            m_TrackingSeekExtent();
        }

        // Обработчик нажатия на кнопку "Truncate Trace End/Start"
        private void m_ControlTracking_OnTruncateClick(
            object sender, EventArgs e)
        {
            m_TrackingTruncate();
        }

        // Обработчик нажатия на кнопку "Track"
        private void m_ControlTracking_OnTrackClick(
            object sender, EventArgs e)
        {
            m_TrackingTrack();
        }

        // Обновление содержания панели отслеживания на форме
        private void m_ControlTrackingUpdate()
        {
            if (m_gui == null) return;

            if (!m_IsTraceSelected || m_IsPlaybackMode)
            {
                m_gui.grpTracking.Enabled = false;
            }
            else
            {
                m_gui.grpTracking.Enabled = true;

                // Если находимся на краю траектории, отключаем две кнопки
                bool isTraceExtent;
                bool isCameraExtent;
                if (m_TrackingDir < 0)
                {
                    isTraceExtent = (m_CurrFrameID == m_CurrTrace.FrameStart);
                    isCameraExtent = (m_CurrFrameID == 0);
                }
                else
                {
                    isTraceExtent = (m_CurrFrameID == m_CurrTrace.FrameEnd);
                    isCameraExtent =
                        (m_CurrFrameID == CameraRecordingInfo.FramesCount - 1);
                }
                m_gui.btnTrackingTruncate.Enabled = !isTraceExtent;
                m_gui.btnTrackingTrack.Enabled = !isCameraExtent;

                // Обновляем надписи на кнопках
                if (m_TrackingDir < 0)
                {
                    m_gui.btnTrackingSeekExtent.Text = "Seek Trace Start";
                    m_gui.btnTrackingTruncate.Text = "Truncate Trace Start";
                }
                else
                {
                    m_gui.btnTrackingSeekExtent.Text = "Seek Trace End";
                    m_gui.btnTrackingTruncate.Text = "Truncate Trace End";
                }
            }
        }

        // Начальная инициализация содержания панели отслеживания на форме
        private void m_ControlTrackingInit()
        {
            if (m_gui == null) return;

            m_ControlTrackingUpdate();
        }

        // ************************** Панель тэга ***************************

        // Начальная инициализация содержания панели тэга на форме
        private void m_ControlTagInit()
        {
        }

        // ************************ Панель категории ************************

        // Обработчик события изменения выпадающего списка категорий
        private void m_ControlCategory_OnCategoryIdChange(
            object sender, EventArgs e)
        {
            int categoryID;

            string [] words = m_gui.cmbCategoryID.Text.Split(' ');
            if (!int.TryParse(words[0], out categoryID))
            {
                MessageBox.Show("Wrong category string!", "ERROR!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MarkupCategoryGetByID(categoryID, out m_CurrCategory))
            {
                m_ControlCategoryUpdate();
            }
            else
            {
                MessageBox.Show("Missing category ID!", "ERROR!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Вход в режим редактирования текущей категории
        private void m_ControlCategoryEditBegin()
        {
            m_gui.txtCategoryName.ReadOnly = false;
            m_gui.btnCategoryEditSave.Text = "Save";
            m_IsCategoryEdit = true;
        }

        // Выход из режима редактирования текущей категории
        private void m_ControlCategoryEditEnd()
        {
            m_gui.txtCategoryName.ReadOnly = true;
            m_gui.btnCategoryEditSave.Text = "Edit";
            m_IsCategoryEdit = false;
        }

        // Обновление содержания панели категории на форме
        private void m_ControlCategoryUpdate()
        {
            if (m_gui == null) return;

            if (MarkupIsOpened && !m_IsPlaybackMode)
            {
                m_gui.grpCategory.Enabled = true;
                m_gui.cmbCategoryID.Text = string.Format("{0} ({1})", 
                    m_CurrCategory.ID, m_CurrCategory.Name);
                m_gui.txtCategoryName.Text = m_CurrCategory.Name;
                if (m_CurrCategory.ID == 0)
                    m_gui.btnCategoryDelete.Enabled = false; //нельзя удалять
                else
                    m_gui.btnCategoryDelete.Enabled = true;
            }
            else
                m_gui.grpCategory.Enabled = false;

            // В любом случае выходим из режима редактирования категории
            if (m_IsCategoryEdit)
                m_ControlCategoryEditEnd();
        }

        // Начальная инициализация содержания панели категории на форме
        private void m_ControlCategoryInit()
        {
            if (m_gui == null) return;

            if (MarkupIsOpened)
            {
                // Заполняем список номеров категорий
                List<Category> categories = MarkupCategoryGetAll();
                m_gui.cmbCategoryID.Items.Clear();
                foreach (Category category in categories)
                {
                    m_gui.cmbCategoryID.Items.Add(string.Format("{0} ({1})",
                        category.ID, category.Name));
                }
            }
            m_ControlCategoryUpdate();
        }

        // Обработчик нажатия на кнопку "New"
        private void m_ControlCategory_OnNewClick(object sender, EventArgs e)
        {
            // Регистрируем новую запись в базе
            m_CurrCategory.Name = "";
            m_CurrCategory.ID = MarkupCategoryCreate(m_CurrCategory);

            // Обновляем списки категорий в выпадающих списках
            m_ControlTagInit();
            m_ControlCategoryInit();

            // Переводим панель категории в режим редактирования
            m_gui.txtCategoryName.Text = m_CurrCategory.Name;
            m_ControlCategoryEditBegin();
        }

        // Обработчик нажатия на кнопку "Edit/Save"
        private void m_ControlCategory_OnEditSaveClick(
            object sender, EventArgs e)
        {
            if (m_IsCategoryEdit)
            {
                // Редактирование завершено - сохраняем иземенения в базу
                try
                {
                    Category newCategory = m_CurrCategory;
                    newCategory.Name = m_gui.txtCategoryName.Text;
                    MarkupCategoryUpdate(newCategory);
                    m_CurrCategory = newCategory;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "ERROR!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Выходим из режима редактирования
                m_ControlCategoryEditEnd();
                m_ControlCategoryInit();
            }
            else
            {
                // Входим в режим редактирования
                m_ControlCategoryEditBegin();
            }
        }

        // Обработчик нажатия на кнопку "Del"
        private void m_ControlCategory_OnDelClick(object sender, EventArgs e)
        {
            if (m_CurrCategory.ID == 0)
            {
                MessageBox.Show("Unable to delete category 0!", "ERROR!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                MarkupCategoryDelete(m_CurrCategory.ID);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновляем текущую выбранную траекторию
            List<Category> categories = MarkupCategoryGetAll();
            if (categories.Count == 0)
                throw new Exception("No categories remaining!");
            m_CurrCategory = categories[categories.Count - 1];

            // Обновляем списки категорий в выпадающщих списках
            m_ControlTagInit();
            m_ControlCategoryInit();
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
                // Загружаем тэг и категорию по умолчанию
                MarkupCategoryGetByID(0, out m_CurrCategory);
                MarkupTagGetByID(0, out m_CurrTag);
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

            m_gui.chkNaviPlayReverse.Checked = (m_PlayDir < 0);
            m_gui.chkNaviPlayReverse.CheckedChanged +=
                new EventHandler(m_ControlNavi_OnPlayReverseChange);

            for (int i = 0; i < m_PlaySpeedValues.Count; i++)
                m_gui.cmbNaviPlaySpeed.Items.Add(m_PlaySpeedCaptions[i]);
            m_gui.cmbNaviPlaySpeed.SelectedIndex = m_PlaySpeedDefaultID;
            //m_gui.cmbNaviPlaySpeed.SelectedIndexChanged +=
            //    new EventHandler(m_ControlNavi_OnPlaySpeedChanged);

            // Панель Tracking
            for (int i = 0; i < m_TrackingMethodValues.Count; i++)
                m_gui.cmbTrackingMethod.Items.Add(m_TrackingMethodNames[i]);
            m_gui.cmbTrackingMethod.SelectedIndex = 
                m_TrackingMethodDefaultID;
            m_gui.cmbTrackingMethod.SelectedIndexChanged +=
                new EventHandler(m_ControlTracking_OnMethodChanged);

            m_gui.chkTrackingReverse.Checked = (m_TrackingDir < 0);
            m_gui.chkTrackingReverse.CheckedChanged +=
                new EventHandler(m_ControlTracking_OnReverseChanged);
            m_gui.chkTrackingIsOccluded.Checked = m_TrackingIsOccluded;
            m_gui.chkTrackingIsOccluded.CheckedChanged +=
                new EventHandler(m_ControlTracking_OnOccludedChange);
            m_gui.chkTrackingIsShaded.Checked = m_TrackingIsShaded;
            m_gui.chkTrackingIsShaded.CheckedChanged +=
                new EventHandler(m_ControlTracking_OnShadedChange);

            m_gui.btnTrackingSeekExtent.Click += 
                new EventHandler(m_ControlTracking_OnSeekExtentClick);
            m_gui.btnTrackingTruncate.Click +=
                new EventHandler(m_ControlTracking_OnTruncateClick);
            m_gui.btnTrackingTrack.Click +=
                new EventHandler(m_ControlTracking_OnTrackClick);

            // Панель Category
            m_gui.cmbCategoryID.SelectedIndexChanged += 
                new EventHandler(m_ControlCategory_OnCategoryIdChange);
            m_gui.btnCategoryNew.Click += 
                new EventHandler(m_ControlCategory_OnNewClick);
            m_gui.btnCategoryDelete.Click +=
                new EventHandler(m_ControlCategory_OnDelClick);
            m_gui.btnCategoryEditSave.Click +=
                new EventHandler(m_ControlCategory_OnEditSaveClick);

            // Обновлям все элементы управления
            m_ControlsInit();
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

            // Параметры отслеживания объекта
            m_TrackingIsOccluded = false;
            m_TrackingIsShaded = false;
            m_TrackingDir = (+1);
            string[] trackingMethodNames = new string[] {
                "AttrUpdate", "Manual", "TLD", "KLT"
            };
            m_TrackingMethodNames = trackingMethodNames.ToList();
            m_TrackingMethodValues = new List<TrackingMethodID>();
            m_TrackingMethodValues.Add(TrackingMethodID.OnlyAttrUpdate);
            m_TrackingMethodValues.Add(TrackingMethodID.Manual);
            m_TrackingMethodValues.Add(TrackingMethodID.TLD); 
            m_TrackingMethodValues.Add(TrackingMethodID.KLT);
            m_TrackingMethodDefaultID = 1;
            m_TrackingMethod = 
                m_TrackingMethodValues[m_TrackingMethodDefaultID];

            // Это должно инициализироваться методом TraceGuiBind
            m_gui = null;

            // Регистрируем обработчик событий для слоя DisplayManager
            DisplaySetCallback(m_TraceOnDisplayEvent);
        }
    }
}
