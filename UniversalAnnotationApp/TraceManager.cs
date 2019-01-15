/* TraceManager: Класс представляет сведения о номере текущего кадра, текущей
 * выбранной траектории, а также хранит состояние взаимодействия пользователя
 * с интерфейсом в соответствии со сценариями взаимодействия.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MarkupData;
using TraceData;            // интерфейс ITraceStateHolder


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
        abstract protected int TraceFrameIncrementDir { get; } 
            // шаг приращения кадра при изменении траектории (+1) или (-1)

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
        abstract public void TraceStateHolderBind(ITraceStateHolder state);
    }


    class TraceManager: TraceManagerBase
    {
        private int m_FrameID;          // индекс текущего кадра
        private int m_FrameIncrementDir;  // шаг приращения кадра (+1/-1)
        private ITraceStateHolder m_state;// переменные состояния

        // Поля для регистрации функций обратного вызова
        private DisplayRefreshCallback m_DisplayRefreshCallback;

        // Метод перехода в начальное состояние
        private void m_ResetState()    
        {
            m_FrameIncrementDir = 0;

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

                if (m_state.IsTraceSelected)
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

        override protected int TraceFrameIncrementDir
        { // шаг приращения кадра при изменении траектории (+1) или (-1)
            get
            {
                return m_FrameIncrementDir;
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
            if (m_state.IsStateUpdate)
                throw new NotSupportedException("Select while edit!");

            m_state.IsTraceSelected = true;
            m_state.TraceSelectedID = TraceID;

            // TODO: Еще нужно обновить панель свойств текущей траектории
            /* ... */
        }

        override protected void TraceUnSelect() // отменить выбор траектории
        {
            if (m_state.IsStateUpdate)
                throw new NotSupportedException("Unselect while edit!");

            m_state.IsTraceSelected = false;

            // TODO: Еще нужно очистить панель свойств текущей траектории
            /* ... */
        }

        // Создание траектории
        override protected void TraceCreateBegin(bool isMarker)
        { // инициировать создание
            if (m_state.IsStateUpdate)
                TraceOnUpdateEnd(); // останавливаем редактирование

            TraceUnSelect();        // снимаем выделение траектории

            m_state.IsStateCreate = true;
            m_state.IsStateCreateStretch = false;
            m_state.IsStateCreateMarker = isMarker;
        }

        override protected void TraceCreateProceed()
        { // 1-ый угол рамки введен, ждем второй (не обязательно)
            if (!m_state.IsStateCreate)
                throw new NotSupportedException("Need to create trace!");

            m_state.IsStateCreateStretch = true;
        }

        /* Завершить создание траектории, индекс созданной траектории
         * указан в параметре TraceID. */
        override protected void TraceCreateEnd(int TraceID)
        {
            if (!m_state.IsStateCreate)
                throw new NotSupportedException("Need to create trace!");

            m_state.IsStateCreate = false;
            m_state.IsStateCreateStretch = false;
        }   

        // Редактирование траектории
        override public void TraceOnUpdateBegin(int StepDir)
        { // начать изменение 
            if (!m_state.IsTraceSelected)
                throw new NotSupportedException("Need to select trace!");

            m_state.IsStateCreate = false;
            m_state.IsStateUpdate = true;
            m_state.IsStateUpdateStretch = false;
            m_FrameIncrementDir = StepDir;

            /* TODO: Заблокировать ползунок */
        }

        override public void TraceOnAppendBegin(int StepDir)
        { // начать добавление в конец (частный случай изменения)
            if (!m_state.IsTraceSelected)
                throw new NotSupportedException("Need to select trace!");

            // TODO: 1. В зависимости от StepDir перейти к крайнему кадру
            // траектории
            /* ... */

            // 2. Инициировать режим редактирования
            TraceOnUpdateBegin(StepDir);

            // 3. Инициировать режим добалвения в конец
            m_state.IsStateUpdateAppend = true;
        }

        override protected void TraceBoxUpdateBegin()
        { // начать изменение рамки
            if (!m_state.IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            m_state.IsStateUpdateStretch = true;
        }

        override protected void TraceBoxUpdateEnd()
        {
            if (!m_state.IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            m_state.IsStateUpdateStretch = false;
        }

        // перейти к следующему кадру (обновить рамку траектории на
        // следующем кадре в разметке).
        override public void TraceOnUpdateProceed()
        {
            if (!m_state.IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            int FrameProceed = m_FrameID + m_FrameIncrementDir;
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
            if (!m_state.IsStateUpdate)
                throw new NotSupportedException("Need to edit trace!");

            m_state.IsStateUpdate = false;
            m_state.IsStateUpdateAppend = false;

            /* TODO: Разблокрировать ползунок. */
        }

        /* Обработчик события изменения положения ползунка. */
        override public void TraceOnSliderChange()
        {
            if (m_state.IsStateUpdate)
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

        override public void TraceStateHolderBind(ITraceStateHolder state)
        {
            m_state = state;
        }

        public TraceManager()
        {
            m_ResetState(); // Переход в начальное состояние
            m_FrameID = -1;
        }
    }
}
