// PictureManager: Класс реализует функции для отображения кадра и разметки видео на 
// форме, а также функций для манипуляции с разметкой с помощью мыши. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiviewAnnotationAPP
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

    // PictureStatusPublic: Структура представляет публичное состояние картины
    struct PictureStatusPublic
    {
        public EditModeMajor modemajor;    // главный идентификатор состояния
        public EditModeMinor modeminor;    // вспомогательный идентификатор сост.
        public EditModeDir modedir;        // идентификатор направления просмотра

        public void Reset()
        {
            modemajor = EditModeMajor.VideoNone;
            modeminor = EditModeMinor.None;
            modedir = EditModeDir.None;
        }
    }

    // PictureStatus: Структура представляет полное состояние картины
    struct PictureStatus
    {
        public PictureStatusPublic pub;    // публичное состояние картины
        public bool isTraceSelected;       // выбрана ли какая-либо траектория
        public int nTraceSelected;         // номер выбранной траектории
        public bool isTraceVisible;  // видна ли выбранная траектория на тек. кадре
        public bool isPointSelected;       // выбрана ли характерная точка в кадре
        public int nPointSelected;         // номер выбранной храктерной точки
        public int BoundFirstCornerX;      // координаты 1-го угла рамки выделения
        public int BoundFirstCornerY;
        public int BoundSecondCornerX;     // координаты 2-го угла рамки выделения
        public int BoundSecondCornerY;

        public void Reset()
        {
            pub.Reset();
            isTraceSelected = false;
            nTraceSelected = -1;
            isTraceVisible = false;
            isPointSelected = false;
            nPointSelected = -1;
            BoundFirstCornerX = 0;
            BoundFirstCornerY = 0;
            BoundSecondCornerX = 0;
            BoundSecondCornerY = 0;
        }
    }

    // PictureControls: Структура представляет ссылки на элементы управления формы
    // для отображения состояния картины
    struct PictureControls
    {
        public ToolStripStatusLabel statusMode;
        public ToolStripStatusLabel statusAction;
    }

    // PictureManager: Класс реализует функции по работе с изображением кадра
    class PictureManager
    {
        private PictureStatus status;
        private PictureControls controls;

        // UpdateControls: Метод обновляет элементы управления по текущему статусу
        private void UpdateControls()
        {

        }

        // Метод сбрасывает состояние и элементы управления формы в начало,
        // его можно вызывать только после инициализации поля controls
        private void Reset()
        {
            status.Reset();
            UpdateControls();
        }

        // Метод сохраняет ссылки на элементы управления формы в поле controls
        public void InitializeComponent(PictureControls pictureControls)
        {
            controls = pictureControls;
            Reset();
        }

        // Метод возвращает публичное состояние источника видео
        public PictureStatusPublic GetStatus()
        {
            return status.pub;
        }
    }
}
