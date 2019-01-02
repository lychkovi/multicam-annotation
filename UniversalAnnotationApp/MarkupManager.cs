using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        abstract public bool MarkupCameraOpen(string RecordingFilePath);
        abstract public bool MarkupCameraClose(); 

        abstract protected bool MarkupOpen(string MarkupFilePath);
        abstract protected bool MarkupClose();

        abstract protected bool MarkupIsOpened { get; }

        abstract public void MarkupGuiBind();
    }

    // Реализации Public-, Private- методов, а также Private-члены, к которым
    // не могут обращаться классы-наследники.
    class MarkupManager : MarkupManagerBase
    {
        public override bool MarkupCameraOpen(string RecordingFilePath)
        {
            CameraOpen(RecordingFilePath);
            throw new NotImplementedException();
        }

        public override bool MarkupCameraClose()
        {
            CameraClose();
            throw new NotImplementedException();
        }

        override protected bool MarkupOpen(string XmlFilePath) 
        {
            throw new NotImplementedException();
        }

        protected override bool MarkupClose()
        {
            throw new NotImplementedException();
        }

        override protected bool MarkupIsOpened
        {
            get { return false; }
        }


        override public void MarkupGuiBind() 
        { 
            CameraOpen("");
        }
    }
}
