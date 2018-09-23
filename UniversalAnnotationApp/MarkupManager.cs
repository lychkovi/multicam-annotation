using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalAnnotationApp
{
    interface IMarkupPublic
    {
        void MarkupGuiBind();
    }

    abstract class MarkupManagerBase : CameraManager, IMarkupPublic
    {
        abstract protected void MarkupOpen(string XmlFilePath);
        abstract public void MarkupGuiBind();
    }

    class MarkupManager : MarkupManagerBase
    {
        override protected void MarkupOpen(string XmlFilePath) 
        { 

        }

        override public void MarkupGuiBind() 
        { 
            CameraOpen(""); 
        }
    }
}
