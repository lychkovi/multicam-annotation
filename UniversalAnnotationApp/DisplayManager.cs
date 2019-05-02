/* DisplayManager: Класс реализует отображение кадров видео с отдельных
 * камер видеозаписи, отображение разметки объектов на этих кадрах и 
 * обработку событий мыши на поля для вывода изображений. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MarkupData;
using TraceData;            // TraceStateHolderDummy


namespace UniversalAnnotationApp
{
    public struct DisplayManagerControls
    {
        public DisplayControlWin.DisplayControl DisplayCtrl;
    }

    public interface IDisplay
    {
        void DisplayGuiBind(DisplayManagerControls controls);
    }

    abstract class DisplayManagerBase : TraceManager, IDisplay
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
    }

    class DisplayManager: DisplayManagerBase
    {
        private DisplayManagerControls m_gui;
        private bool m_IsDisplay;   // признак инициализированного дисплея
        private int m_FrameID;      // номер текущего буферизованного кадра
        //private TraceStateHolderDummy dummy; // временный объект (для отладки)

        // инициализация полей вывода изображений
        private void m_Init()
        {
            // TODO: 1. Создание всех полей для вывода изображений,
            // регистрация обработчиков мыши для этих полей

            m_FrameID = -1;         // кадр еще не буферизован
            m_IsDisplay = true;

            // 2. Перерисовка изображений
            m_Refresh();
        }

        // отрисовка изображений на форме
        private void m_Refresh()
        {
            if (m_IsDisplay)
            {
                if (m_FrameID != TraceFrameID)
                {
                    // TODO: Запрашиваем нужный кадр от слоя Camera и 
                    // буферизуем во внутренних полях Display

                    if (false /*m_FrameID != TraceFrameID*/)
                        throw new NotSupportedException("Bad buffering!");
                }

                // TODO: Извлекаем изображения видео из буферов

                if (MarkupIsOpened)
                {
                    // TODO: Наносим разметку на кадр

                    // TODO: При необходимости наносим рамку новой 
                    // создаваемой траектории

                }

                // TODO: Визуализируем изображения на форме

            }
        }

        // Удаление полей для вывода изображений
        private void m_Clear()
        {
            // TODO: 1. Удаляем все поля для вывода изображений

            m_FrameID = -1;
            m_IsDisplay = false;
        }

        // Такие четыре метода должны быть у всех слоев выше слоя Markup
        // Слои вызывают эти методы рекурсивно по цепочке
        override protected bool DisplayCameraOpen(RecordingInfo rec)
        {
            if (TraceCameraOpen(rec))
            {
                m_Init();
                return true;
            }
            else
                return false;
        }

        override protected void DisplayCameraClose()
        {
            TraceCameraClose();
            m_Clear();
        }

        override protected bool DisplayMarkupOpen(string MarkupFilePath)
        {
            if (TraceMarkupOpen(MarkupFilePath))
            {
                m_Refresh();
                return true;
            }
            else
                return false;
        }

        override protected void DisplayMarkupClose()
        {
            TraceMarkupClose();
            m_Refresh();
        }

        // Метод перерисовки кадров на форме
        override protected void DisplayRefresh()
        {
            m_Refresh();
        }

        // Метод привязки элементов управления форму к объекту DisplayManager
        override public void DisplayGuiBind(DisplayManagerControls controls)
        {
            m_gui = controls;
        }

        public DisplayManager()
        {
            //dummy = new TraceStateHolderDummy();
        }
    }
}
