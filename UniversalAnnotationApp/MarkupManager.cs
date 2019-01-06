using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MarkupData;                   // классы MarkupProvider


namespace UniversalAnnotationApp
{
    // Все Public-Методы (Метод регистрации графических элементов управления, 
    // а также методы обработки событий от элементов управления формы).
    // Могут вызываться прямо из методов формы пользовательского интерфейса.
    public interface IMarkup
    {
        void MarkupGuiBind();
    }

    // Все Protected-методы, которые могут вызываться классами-наследниками и 
    // Public-методы из соответствующего интерфейса. 
    abstract class MarkupManagerBase : CameraManager, IMarkup
    {
        // Такие два метода должны быть у всех слоев выше слоя Camera
        // Слои вызывают эти методы рекурсивно по цепочке
        abstract protected bool MarkupCameraOpen(RecordingInfo rec);
        abstract protected void MarkupCameraClose();

        abstract protected bool MarkupOpen(string MarkupFilePath);
        abstract protected void MarkupClose();

        abstract protected bool MarkupIsOpened { get; }
        abstract protected bool MarkupIsSaved { get; }

        abstract public void MarkupGuiBind();
    }

    // Реализации Public-, Private- методов, а также Private-члены, к которым
    // не могут обращаться классы-наследники.
    class MarkupManager : MarkupManagerBase
    {
        private MarkupProvider m_markup;    // поставщик данных
        private bool m_IsOpened;            // признак открытия файла
        private bool m_IsSaved;             // признак сохранения изменений

        override protected bool MarkupCameraOpen(RecordingInfo rec)
        {
            return CameraOpen(rec);
        }

        override protected void MarkupCameraClose()
        {
            CameraClose();
        }

        override protected bool MarkupOpen(string MarkupFilePath) 
        {
            throw new NotImplementedException();
        }

        override protected void MarkupClose()
        {
            throw new NotImplementedException();
        }

        override protected bool MarkupIsOpened
        {
            get 
            { 
                return m_IsOpened; 
            }
        }

        override protected bool MarkupIsSaved
        {
            get
            {
                return m_IsSaved; 
            }
        }


        override public void MarkupGuiBind() 
        { 
            
        }

        // Конструктор класса по умолчанию
        public MarkupManager()
        {
            m_markup = new MarkupProviderADO();
        }
    }
}
