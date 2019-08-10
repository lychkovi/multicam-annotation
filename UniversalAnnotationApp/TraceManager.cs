/* TraceManager: Класс представляет сведения о номере текущего кадра, текущей
 * выбранной траектории, а также хранит состояние взаимодействия пользователя
 * с интерфейсом в соответствии со сценариями взаимодействия.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MarkupData;


namespace UniversalAnnotationApp
{
    public class TraceManagerControls
    {
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

        private bool m_IsTraceSelected;
        private Trace m_CurrTrace;
        private Box m_CurrBox;       // если m_CurrTrace.HasBox == true
        private Marker m_CurrMarker; // если m_CurrTrace.HasBox == false
        private Tag m_CurrTag;
        private Category m_CurrCategory;
        private bool m_IsCategoryEdit;

        private int m_CurrFrameID;
        private bool m_IsPlaybackMode;

        // Выбор траектории 
        private void m_TraceSelect(int TraceID)
        { // выбрать траекторию N

        }

        // Отменить выбор траектории
        private void m_TraceUnSelect()
        {

        }

        // Обслуживание элементов графического интерфейса
        private void m_ControlsUpdate()
        {
        }

        private void m_ControlsInit()
        {
        }

        // ************************** Навигация *****************************
        private void m_PlaybackStop()
        {

        }

        override protected void TraceMoveToFrame(int frameID)
        {

        }

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
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
        }

        override public void TraceTraceDelete() // для вызова из меню
        {
        }

        /* Регистрация эелементов управления: ползунка, кнопок, 
         * выпадающих списков и т. п. */
        override public void TraceGuiBind(TraceManagerControls controls)
        {

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

            m_CurrFrameID = -1;
            m_IsPlaybackMode = false;

            // Это должно инициализироваться методом TraceGuiBind
            m_gui = null;
        }
    }
}
