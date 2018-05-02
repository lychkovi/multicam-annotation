// GuiContext: Класс представляет состояние графического интерфейса для формы
// Form3.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoAnnotationAPP
{
    // Идентификаторы состояний основного режима графического интерфейса
    enum EditModeMajor
    {
        VideoNone,                  // нет видео
        VideoView,                  // просмотр видео
        TraceCreate,                // создание новой траектории
        TraceView,                  // просмотр траектории
        TraceEdit,                  // редактирование траектории
        TraceAppend                 // достраивание траектории в конец
    }

    // Идентификаторы состояний вспомогательного режима графического 
    // интерфейса (для операций с мышью)
    enum EditModeMinor
    {
        None,
        SpecifyFirstCorner,         // ввод первого угла рамки выделения
        SpecifySecondCorner,        // ввод второго угла рамки выделения
        SelectPoint,                // выбор характерной точки
        AddPoint,                   // добавление новой характерной точки
        ReboundInit,                // инициализация редактирования рамки
        ReboundLeft,                // перемещение левой границы рамки
        ReboundTop,                 // перемещение правой границы рамки
        ReboundLeftTop,             // перемещение левого верхнего угла
        ReboundRight,
        ReboundBottom,
        ReboundRightBottom,
        ReboundLeftBottom,
        ReboundRightTop,
        ReboundMove                 // премещение всей рамки выделения
    }

    // Идентификаторы направления прокрутки видео при редактировании
    // траектории
    enum EditModeDir
    {
        None,                       // направление не определено
        Forward,                    // прокрутка вперед
        Backward                    // прокрутка назад
    }

    struct GuiContext
    {
        public bool isVideo;               // загружен ли видеофайл
        public string VideoFilePath;       // путь к загруженному видеофайлу
        public int VideoFramesCount;       // количество кадров в видеофайле
        public double VideoFps;            // частота кадров видеофайла
        public double VideoDurationMs;     // длительность видеофайла (мс)
        public int VideoFrameCurrent;      // номер текущего кадра видеофайла

        public bool isMarkupFile;          // связана ли разметка с файлом на диске
        public string MarkupFilePath;      // путь к файлу разметки
        public bool isMarkupUnsaved;       // несохраненные изменения в разметке

        public bool isTraceSelected;       // выбрана ли какая-либо траектория
        public int nTraceSelected;         // номер выбранной траектории
        public bool isTraceVisible;  // видна ли выбранная траектория на тек. кадре
        public bool isPointSelected;       // выбрана ли характерная точка в кадре
        public int nPointSelected;         // номер выбранной храктерной точки
        public int BoundFirstCornerX;      // координаты 1-го угла рамки выделения
        public int BoundFirstCornerY;
        public int BoundSecondCornerX;     // координаты 2-го угла рамки выделения
        public int BoundSecondCornerY;

        public EditModeMajor modemajor;    // главный идентификатор состояния
        public EditModeMinor modeminor;    // вспомогательный идентификатор сост.
        public EditModeDir modedir;        // идентификатор направления просмотра

        // Констуркутор по умолчанию
        public void Reset()
        {
            isVideo = false;
            VideoFilePath = "";
            VideoFramesCount = 0;
            VideoDurationMs = 0;
            VideoFrameCurrent = -1;

            isMarkupFile = false;
            MarkupFilePath = "";
            isMarkupUnsaved = false;

            isTraceSelected = false;
            nTraceSelected = -1;
            isTraceVisible = false;
            isPointSelected = false;
            nPointSelected = -1;
            BoundFirstCornerX = 0;
            BoundFirstCornerY = 0;
            BoundSecondCornerX = 0;
            BoundSecondCornerY = 0;

            modemajor = EditModeMajor.VideoNone;
            modeminor = EditModeMinor.None;
            modedir = EditModeDir.None;
        }
    }
}
