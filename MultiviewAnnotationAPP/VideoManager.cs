// VideoManager: Класс реализует взаимодействие с источником видео. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiviewAnnotationAPP
{
    // VideoStatusPublic: Структура представляет публичное состояние источника видео
    public struct VideoStatusPublic
    {
        public bool isVideo;          // загружен ли видеофайл
        public int FrameCurrent;      // номер текущего кадра видеофайла

        public void Reset()
        {
            isVideo = false;
            FrameCurrent = -1;
        }
    }

    // VideoStatus: Стуркутра представляет полное состояние источника видео
    struct VideoStatus
    {
        public VideoStatusPublic pub; // публичные поля статуса
        public int Handle;            // идентификатор источника видео OpenCV
        public string FilePath;       // путь к загруженному видеофайлу
        public int FramesCount;       // количество кадров в видеофайле
        public double Fps;            // частота кадров видеофайла
        public double DurationMs;     // длительность видеофайла (мс)

        public void Reset()
        {
            pub.Reset();
            Handle = -1;
            FilePath = "";
            FramesCount = 0;
            Fps = 0;
            DurationMs = 0;
        }
    }

    // VideoControls: Структура представляет ссылки на элементы управления формы
    // для отображения состояния источника видео
    struct VideoControls
    {
        public TextBox txtVideoFilePath;
        public TextBox txtVideoFramesTotal;
        public TextBox txtVideoFps;
        public TextBox txtVideoDurationMs;
        public TextBox txtVideoPositionMs;
        public TextBox txtGoToFrame;
        public TextBox txtVideoFrameCurrent;
        public TrackBar tbrVideoSlider;
        public ToolStripStatusLabel statusVideo;
    }

    // VideoManager: Класс реализует функции взаимодействия с источником видео и
    // отображения его параметров на форме. 
    class VideoManager
    {
        private VideoStatus status;
        private VideoControls controls;

        // Метод сбрасывает состояние и элементы управления формы в начало,
        // его можно вызывать только после инициализации поля controls
        private void Reset()
        {
            status.Reset();
            UpdateControls();
        }

        // UpdateNavigator: Метод обновляет поля панели навигации по текущему статусу
        private void UpdateNavigator()
        {
            if (status.pub.isVideo)
            {
                controls.txtVideoFilePath.Text = status.FilePath;
                controls.txtVideoFramesTotal.Text = status.FramesCount.ToString();
                controls.txtVideoFps.Text = status.Fps.ToString();
                controls.txtVideoDurationMs.Text = status.DurationMs.ToString();
                double positionMs = status.pub.FrameCurrent * 1000.0 / status.Fps;
                controls.txtVideoPositionMs.Text = positionMs.ToString();
                controls.txtVideoFrameCurrent.Text = status.pub.FrameCurrent.ToString();
            }
            else
            {
                controls.txtVideoFilePath.Text = "";
                controls.txtVideoFramesTotal.Text = "";
                controls.txtVideoFps.Text = "";
                controls.txtVideoDurationMs.Text = "";
                controls.txtVideoPositionMs.Text = "";
                controls.txtVideoFrameCurrent.Text = "";
            }
        }

        // UpdateTrackBar: Метод обновляет состояние слайдера по текущему статусу
        private void UpdateTrackBar()
        {
            if (status.pub.isVideo)
            {
                controls.tbrVideoSlider.Enabled = true;
            }
            else
            {
                controls.tbrVideoSlider.Enabled = false;
            }
        }

        // UpdateStatusStrip: Метод обновляет состояние строки состояния на форме
        private void UpdateStatusStrip()
        {
            if (status.pub.isVideo)
            {
                controls.statusVideo.Text = "";
            }
            else
            {
                controls.statusVideo.Text = "No Video";
            }
        }

        // UpdateControls: Метод обновляет элементы управления по текущему статусу
        private void UpdateControls()
        {
            UpdateNavigator();
            UpdateTrackBar();
            UpdateStatusStrip();
        }

        // Метод сохраняет ссылки на элементы управления формы в поле controls
        public void InitializeComponent(VideoControls videoControls)
        {
            controls = videoControls;
            Reset();
        }

        // Метод возвращает публичное состояние источника видео
        public VideoStatusPublic GetStatus()
        {
            return status.pub;
        }
    }
}
