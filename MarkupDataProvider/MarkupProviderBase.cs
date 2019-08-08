using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;               // Rectangle

namespace MarkupData
{
    /* View: Сведения о видеозаписи с одной камеры. */
    public struct View
    {
        public int ID;             // идентификатор видеозаписи с камеры
        public int FrameWidth;     // ширина кадра камеры (пиксели)
        public int FrameHeight;    // высота кадра камеры (пиксели)
        public bool IsColour;      // признак цветного изображения камеры
        public string FileNameAVI; // имя AVI-файла видеозаписи
        public string Comment;     // комментарий к видеозаписи с камеры
    }

    /* RecordingInfo: Сведения о видеозаписи с нескольких камер. */
    public struct RecordingInfo
    {
        public string FileNameXML;  // Имя XML-файла со сведениями о записи
        public int FramesCount;     // Кол-во кадров видеозаписи
        public double Fps;          // Частота видеозаписи (кадр/с)
        public DateTime DateTime;   // дата и время создания видеозаписи
        public string Comment;      // Комментарий к видеозаписи
        public List<View> Views;    // сведения о записях отдельных камер

        public RecordingInfo(string myComment)// конструктор по умолчанию
        {
            FileNameXML = "";
            FramesCount = 0;
            Fps = 25.0;
            DateTime = DateTime.Now;
            Comment = myComment;
            Views = new List<View>();
        }
    }

    /* Category: Категория движущихся объектов. */
    public struct Category
    {
        public int ID;              // идентификатор категории
        public string Name;         // наименование категории
        public string Comment;      // комментарий
        public UInt16 ColorRed;
        public UInt16 ColorGreen;
        public UInt16 ColorBlue;      // RGB-цвет для выделения объектов
    }

    /* Tag: Метка конкретного экземпляра движущегося объекта в кадре. */
    public struct Tag
    {
        public int ID;              // идентификатор объекта
        public int CategoryID;      // идентификатор категории объекта
        public string Comment;      // комментарий
    }

    /* Trace: Траектория объекта или его элемента в кадре отдельной камеры */
    public struct Trace
    {
        public int ID;          // идентификатор траектории
        public int ViewID;      // идентификатор видеозаписи с одной камеры
        public int TagID;       // идентификатор объекта
        public int FrameStart;  // начальный кадр траектории
        public int FrameEnd;    // конечный кадр траектории
        public bool HasBox;     // индикатор наличия рамки объекта
    }

    /* Box: Рамка объекта на отдельном кадре. */
    public struct Box
    {
        public int TraceID;     // идентификатор траектории
        public int FrameID;     // номер кадра видеозаписи
        public int PosX;     // координаты ограничительной рамки
        public int PosY;
        public int Width;    // размеры ограничительной рамки
        public int Height;
        public bool IsShaded;   // признак затененности объекта
        public bool IsOccluded; // признак перекрытия объекта в кадре

        public Rectangle GetRectangle(double scale = 1.0)
        {
            Rectangle rect = new Rectangle();
            rect.X = (int) (PosX * scale);
            rect.Y = (int) (PosY * scale);
            rect.Width = (int) (Width * scale);
            rect.Height = (int) (Height * scale);
            return rect;
        }
    }

    /* Marker: Точечный маркер объекта или его элемента на отдельном кадре */
    public struct Marker
    {
        public int TraceID;     // идентификатор траектории
        public int FrameID;     // номер кадра видеозаписи
        public int PosX;     // координата по оси абсцисс
        public int PosY;     // координата по оси ординат
        public bool IsShaded;   // признак затененности объекта
    }

    /* Базовый класс для реализации поставщика данных разметки видео. */
    abstract public class MarkupProvider
    {
        abstract public void InitHeader(RecordingInfo rec);
            //--> Инициализируются только таблицы Views и RecInfo
            // содержимым структуры rec:
            // необходимо для создания XML-файла видеозаписи.
        abstract public void Init(RecordingInfo rec);
            //--> Кроме таблиц Views и RecInfo также инициализируются
            // таблицы Categories и Tags тэгами по умолчанию:
            // необходимо при создании нового XML-файла разметки.
        abstract public bool Save(string MarkupFilePath);
        abstract public bool Open(string MarkupFilePath);
        abstract public RecordingInfo GetHeader();
        abstract public bool CheckHeader(RecordingInfo rec);//<--> Views и RecInfo

        abstract public int CategoryCreate(Category category);
        abstract public void CategoryUpdate(Category category);
        abstract public void CategoryDelete(int categoryID);
        abstract public List<Category> CategoryGetAll();
        abstract public bool CategoryGetByID(
            int categoryID, out Category category);

        abstract public int TagCreate(Tag tag);
        abstract public void TagUpdate(Tag tag);
        abstract public void TagDelete(int tagID);
        abstract public List<Tag> TagGetAll();
        abstract public bool TagGetByID(int tagID, out Tag tag);

        abstract public int TraceCreate(Trace trace);
        abstract public void TraceUpdate(Trace trace);
        abstract public void TraceDelete(int traceID);
        abstract public List<Trace> TraceGetAll();
        abstract public bool TraceGetByID(int traceID, out Trace trace);

        abstract public void BoxCreate(Box box);
        abstract public void BoxUpdate(Box box);
        abstract public void BoxDelete(int traceID, int frameID);
        abstract public bool BoxGetByID(
            int traceID, int frameID, out Box box);
        abstract public List<Box> BoxGetByView(int frameID, int viewID);

        abstract public void MarkerCreate(Marker marker);
        abstract public void MarkerUpdate(Marker marker);
        abstract public void MarkerDelete(int traceID, int frameID);
        abstract public bool MarkerGetByID(
            int traceID, int frameID, out Marker marker);
        abstract public List<Marker> MarkerGetByView(
            int frameID, int viewID);
    }
}
