// CameraManager: Класс реализует взаимодействие с видеофайлом. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UniversalAnnotationApp
{
    abstract class CameraManagerBase
    {
        abstract protected void CameraOpen(string RecordingFilePath);
        abstract protected bool CameraIsOpened { get; }
    }

    class CameraManager : CameraManagerBase
    {
        override protected void CameraOpen(string RecordingFilePath) 
        { 

        }

        override protected bool CameraIsOpened 
        {
            get { return false; }
        }
    }

    
}
