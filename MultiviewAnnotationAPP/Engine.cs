using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiviewAnnotationAPP
{
    // Структура представляет публичное состояние всех трех компонентов
    struct GlobalStatusPublic
    {
        public VideoStatusPublic video;
        public MarkupStatusPublic markup;
        public PictureStatusPublic picture;
    }

    // Engine: Класс реализует обработчики всех событий формы; обращения
    // к элементам управления формы осуществляется через вспомогательные 
    // классы VideoManager, MarkupManager, PictureManager, StatusManager,
    // реализующие отдельные части функционала приложения. 
    class Engine
    {
        VideoManager videoManager;
        MarkupManager markupManager;
        PictureManager pictureManager;

        private GlobalStatusPublic GetGlobalStatus()
        {
            GlobalStatusPublic status;
            status.video = videoManager.GetStatus();
            status.markup = markupManager.GetStatus();
            status.picture = pictureManager.GetStatus();
            return status;
        }

        public Engine()
        {
            videoManager = new VideoManager();
            markupManager = new MarkupManager();
            pictureManager = new PictureManager();
        }

        public void InitVideoManager(VideoControls controls)
        {
            videoManager.InitializeComponent(controls);
        }

        public void InitMarkupManager(MarkupControls controls)
        {
            markupManager.InitializeComponent(controls);
        }

        public void InitPictureManager(PictureControls controls)
        {
            pictureManager.InitializeComponent(controls);
        }

        // Метод перематывает видео к нужному кадру и соответственно
        // обновляет состояние интерфейса на форме
        public bool VideoMoveTo(int iframe)
        {
            GlobalStatusPublic status;

            status = GetGlobalStatus();
            // videoManager.<Method>(ref status, out frame, out frameNumber);
            // markupManager.<Method>(ref status, in frameNumber, out markupData);
            // pictureManager.<Method>(ref status, in frame, in markupData);
            return true;
        }
    }
}
