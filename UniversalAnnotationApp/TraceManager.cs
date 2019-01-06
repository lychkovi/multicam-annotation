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
    public interface ITrace
    {
        void TraceGuiBind();
        void TraceOnUpdateBegin(int StepDir);  // начать изменение 
        void TraceOnAppendBegin(int StepDir);
        void TraceOnUpdateProceed();
        void TraceOnUpdateEnd(); // завершить изменение траектории

        // Перемотка видео, инициированная слоем Timeline
        void TraceOnSliderChange();
        void TraceOnMoveNext();
        void TraceOnMovePrev();
    }

    // Типы указателей на функции обратного вызова верхних слоев приложения
    public delegate void DisplayRefreshCallback();

    abstract class TraceManagerBase : MarkupManager, ITrace
    {
        // Сведения о состоянии объекта TraceManager для верхних слоев
        // приложения. 
        abstract protected int TraceFrameID { get; } 
            // индекс текущего кадра
        abstract protected bool TraceIsTraceSelected { get; } 
            // траектория выбрана?
        abstract protected int TraceTraceSelectedID { get; } // её индекс
        abstract protected bool TraceIsStateCreate { get; }
        abstract protected bool TraceIsStateCreateMarker { get; }
            // инициировали создание
        abstract protected bool TraceIsStateCreateStretch { get; }
            // мышкой указали первый угол рамки, тянем второй
        abstract protected bool TraceIsStateUpdate { get; } 
            // инициировали изменение
        abstract protected int TraceUpdateStepDir { get; } 
            // шаг приращения кадра при изменении траектории (+1) или (-1)
        abstract protected bool TraceIsStateUpdateStretch { get; }
            // мышкой указали первый угол рамки, тянем второй
        abstract protected bool TraceIsStateUpdateAppend { get; }
            // признак изменения траектории в режиме Append

        // Методы регистрации функций обратного вызвоа для обновления
        // состояния слоев верхнего уровня при обработке событий от кнопок
        // графического интерфейса, ассоциированных с TraceManager.
        // Данные о состоянии TraceManager они могут считать из свойств выше
        abstract protected void TraceSetDisplayRefreshCallback(
            DisplayRefreshCallback fcn);

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        abstract protected bool TraceCameraOpen(RecordingInfo rec);
        abstract protected void TraceCameraClose();
        abstract protected bool TraceMarkupOpen(string MarkupFilePath);
        abstract protected void TraceMarkupClose();

        // Перемотка видео, инициированная слоем Timeline
        abstract public void TraceOnSliderChange();
        abstract public void TraceOnMoveNext();
        abstract public void TraceOnMovePrev();

        // Выбор траектории (инициируется слоем Display по событию мыши)
        abstract protected void TraceSelect(int TraceID); // выбр. траект. N
        abstract protected void TraceUnSelect(); // отменить выбор траект.

        // Создание траектории
        abstract protected void TraceCreateBegin(bool isMarker); // инициировать создание
        abstract protected void TraceCreateProceed(); // 1ый угол рамки ввели
        abstract protected void TraceCreateEnd(int TraceID);
            /* Завершитиь создание траектории, индекс созданной траектории
             * указан в параметре TraceID. */

        // Редактирование траектории
        abstract public void TraceOnUpdateBegin(int StepDir);// начать измен.
        abstract public void TraceOnAppendBegin(int StepDir);
            // начать добавление в конец (частный случай изменения)
        abstract protected void TraceBoxUpdateBegin(); // нач. измен. рамки
        abstract protected void TraceBoxUpdateEnd();
        abstract public void TraceOnUpdateProceed(); 
            // перейти к следующему кадру (обновить рамку траектории на
            // следующем кадре в разметке).
        abstract public void TraceOnUpdateEnd(); // завершить изм. траектории

        abstract public void TraceGuiBind();
    }


    class TraceManager: TraceManagerBase
    {
        private int m_FrameID;          // индекс текущего кадра
        private bool m_IsTraceSelected; // траектория выбрана?
        private int m_TraceSelectedID;  // индекс выбранной траектории
        private bool m_IsStateCreate;   // инициировали создание
        private bool m_IsStateCreateMarker; // создание маркера
        private bool m_IsStateCreateStretch; // указали первый угол рамки
        private bool m_IsStateUpdate; // инициировали изменение
        private int m_UpdateStepDir;  // шаг приращения кадра
        private bool m_IsStateUpdateStretch; // указали первый угол рамки
        private bool m_IsStateUpdateAppend; // признак изменения траектории

        // Поля для регистрации функций обратного вызова
        private DisplayRefreshCallback m_DisplayRefreshCallback;

        // Метод перехода в начальное состояние
        private void m_ResetState()    
        {
            // Выбор траектории
            m_IsTraceSelected = false;
            m_TraceSelectedID = -1;

            // Создание траектории
            m_IsStateCreate = false;
            m_IsStateCreateStretch = false;
            m_IsStateCreateMarker = false;

            // Редактирование траектории
            m_IsStateUpdate = false;
            m_UpdateStepDir = 0;
            m_IsStateUpdateStretch = false;
            m_IsStateUpdateAppend = false;

            // TODO: Очистить панель отображения свойств текущей выбранной
            // траектории. 
            /* ... */
        }

        // Перемотка видео, инициированная ползунком или кнопкой
        private void m_MoveToFrame(int FrameID)
        {
            if (FrameID != m_FrameID &&
                FrameID >= 0 &&
                FrameID < CameraRecordingInfo.FramesCount)
            {
                m_FrameID = FrameID;

                if (m_IsTraceSelected)
                {
                    // TODO: Выясняем, присутствует ли текущая выбранная
                    // траектория на новом кадре
                    /* ... */
                    if (true /* Не присутствует*/)
                    {
                        // Отменяем выбор траектории
                        TraceUnSelect();
                    }
                }

                // Обновляем состояние слоя Display
                if (m_DisplayRefreshCallback != null)
                    m_DisplayRefreshCallback();

                // TODO: Обновляем положения ползунка
                /* ... */
            }
        }

        // Сведения о состоянии объекта TraceManager для верхних слоев
        // приложения. 
        override protected int TraceFrameID // индекс текущего кадра
        {
            get
            {
                return m_FrameID;
            }
        }

        override protected bool TraceIsTraceSelected 
        { // траектория выбрана?
            get
            {
                return m_IsTraceSelected;
            }
        }

        override protected int TraceTraceSelectedID 
        { // индекс выбранной траектории
            get
            {
                return m_TraceSelectedID;
            }
        }

        override protected bool TraceIsStateCreate
        { // инициировали создание
            get
            {
                return m_IsStateCreate;
            }
        }

        override protected bool TraceIsStateCreateMarker
        {
            get
            {
                return m_IsStateCreateMarker;
            }
        }

        override protected bool TraceIsStateCreateStretch
        { // мышкой указали первый угол рамки, тянем второй
            get
            {
                return m_IsStateCreateStretch;
            }
        }

        override protected bool TraceIsStateUpdate
        { // инициировали изменение
            get
            {
                return m_IsStateUpdate;
            }
        }

        override protected int TraceUpdateStepDir
        { // шаг приращения кадра при изменении траектории (+1) или (-1)
            get
            {
                return m_UpdateStepDir;
            }
        }

        override protected bool TraceIsStateUpdateStretch
        { // мышкой указали первый угол рамки, тянем второй
            get
            {
                return m_IsStateUpdateStretch;
            }
        }

        override protected bool TraceIsStateUpdateAppend
        { // признак изменения траектории в режиме Append
            get
            {
                return m_IsStateUpdateAppend;
            }
        }

        // Методы регистрации функций обратного вызвоа для обновления
        // состояния слоев верхнего уровня при обработке событий от кнопок
        // графического интерфейса, ассоциированных с TraceManager.
        // Данные о состоянии TraceManager они могут считать из свойств выше
        override protected void TraceSetDisplayRefreshCallback(
            DisplayRefreshCallback fcn)
        {
            m_DisplayRefreshCallback = fcn;
        }

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        override protected bool TraceCameraOpen(RecordingInfo rec)
        {
            if (MarkupCameraOpen(rec))
            {
                m_ResetState();
                m_FrameID = 0;
                /* TODO: Инициализировать ползунок. */
                return true;
            }
            else
                return false;
        }

        override protected void TraceCameraClose()
        {
            MarkupCameraClose();
            m_ResetState();
            m_FrameID = -1;
            /* TODO: Деинициализировать ползунок. */
        }

        override protected bool TraceMarkupOpen(string MarkupFilePath)
        {
            if (MarkupOpen(MarkupFilePath))
            {
                m_ResetState();
                return true;
            }
            else
                return false;
        }

        override protected void TraceMarkupClose()
        {
            MarkupClose();
            m_ResetState();
        }

        override public void TraceOnMoveNext()
        {
            m_MoveToFrame(m_FrameID + 1);
        }

        override public void TraceOnMovePrev()
        {
            m_MoveToFrame(m_FrameID - 1);
        }

        // Выбор траектории (инициируется слоем Display по событию мыши)
        override protected void TraceSelect(int TraceID)
        { // выбрать траекторию N
            if (m_IsStateUpdate)
                throw new NotSupportedException("Select while edit!");

            m_IsTraceSelected = true;
            m_TraceSelectedID = TraceID;

            // TODO: Еще нужно обновить панель свойств текущей траектории
            /* ... */
        }

        override protected void TraceUnSelect() // отменить выбор траектории
        {
            if (m_IsStateUpdate)
                throw new NotSupportedException("Unselect while edit!");

            m_IsTraceSelected = false;

            // TODO: Еще нужно очистить панель свойств текущей траектории
            /* ... */
        }

        // Создание траектории
        override protected void TraceCreateBegin(bool isMarker)
        { // инициировать создание
            if (m_IsStateUpdate)
                TraceOnUpdateEnd(); // останавливаем редактирование

            TraceUnSelect();        // снимаем выделение траектории

            m_IsStateCreate = true;
            m_IsStateCreateStretch = false;
            m_IsStateCreateMarker = isMarker;
        }

        override protected void TraceCreateProceed()
        { // 1-ый угол рамки введен, ждем второй (не обязательно)
            if (!m_IsStateCreate)
                throw new NotSupportedException("Need to create trace!");

            m_IsStateCreateStretch = true;
        }

        /* Завершить создание траектории, индекс созданной траектории
         * указан в параметре TraceID. */
        override protected void TraceCreateEnd(int TraceID)
        {
            if (!m_IsStateCreate)
                throw new NotSupportedException("Need to create trace!");

            m_IsStateCreate = false;
            m_IsStateCreateStretch = false;
        }   

        // Редактирование траектории
        override public void TraceOnUpdateBegin(int StepDir)
        { // начать изменение 
            if (!m_IsTraceSelected)
                throw new NotSupportedException("Need to select trace!");

            m_IsStateCreate = false;
            m_IsStateUpdate = true;
            m_IsStateUpdateStretch = false;
            m_UpdateStepDir = StepDir;

            /* TODO: Заблокировать ползунок */
        }

        override public void TraceOnAppendBegin(int StepDir)
        { // начать добавление в конец (частный случай изменения)
            if (!m_IsTraceSelected)
                throw new NotSupportedException("Need to select trace!");

            // TODO: 1. В зависимости от StepDir перейти к крайнему кадру
            // траектории
            /* ... */

            // 2. Инициировать режим редактирования
            TraceOnUpdateBegin(StepDir);

            // 3. Инициировать режим добалвения в конец
            m_IsStateUpdateAppend = true;
        }

        override protected void TraceBoxUpdateBegin()
        { // начать изменение рамки
            if (!m_IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            m_IsStateUpdateStretch = true;
        }

        override protected void TraceBoxUpdateEnd()
        {
            if (!m_IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            m_IsStateUpdateStretch = false;
        }

        // перейти к следующему кадру (обновить рамку траектории на
        // следующем кадре в разметке).
        override public void TraceOnUpdateProceed()
        {
            if (!m_IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            int FrameProceed = m_FrameID + m_UpdateStepDir;
            if (FrameProceed >= 0 &&
                FrameProceed < CameraRecordingInfo.FramesCount)
            {
                // 1. Узнать коордианты рамки траектории на текущем кадре

                // 2. Вычислить прогнозные координаты рамки траектории на
                // кадре FrameProceed

                // 3. Сохранить их в баду данных

                // 4. Перейти к следующему кадру
                m_MoveToFrame(FrameProceed);
            }
        }
            
        override public void TraceOnUpdateEnd()
        { // завершить изменение траектории
            if (!m_IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            m_IsStateUpdate = false;
            m_IsStateUpdateAppend = false;

            /* TODO: Разблокрировать ползунок. */
        }

        /* Обработчик события изменения положения ползунка. */
        override public void TraceOnSliderChange()
        {
            if (m_IsStateUpdate)
                throw new NotSupportedException("Unable to navigate!");

            /* TODO: Вычислить номер кадра, к которому перейти. */
            int FrameProceed = 0;

            m_MoveToFrame(FrameProceed);
        }

        /* Регистрация эелементов управления: ползунка, кнопок, 
         * выпадающих списков и т. п. */
        override public void TraceGuiBind()
        {

        }

        public TraceManager()
        {
            m_ResetState(); // Переход в начальное состояние
            m_FrameID = -1;
        }
    }
}
