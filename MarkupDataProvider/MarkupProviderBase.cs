using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public bool HasBox;     // индикатор наличия рамки объекта
    }

    /* Box: Рамка объекта на отдельном кадре. */
    public struct Box
    {
        public int TraceID;     // идентификатор траектории
        public int FrameID;     // номер кадра видеозаписи
        public double BoundLeft;// координаты ограничительной рамки
        public double BoundTop;
        public double BoundRight;
        public double BoundBottom;
        public bool IsShaded;   // признак затененности объекта
        public bool IsOccluded; // признак перекрытия объекта в кадре
    }

    /* Marker: Точечный маркер объекта или его элемента на отдельном кадре */
    public struct Marker
    {
        public int TraceID;     // идентификатор траектории
        public int FrameID;     // номер кадра видеозаписи
        public double CenterX;  // координата по оси абсцисс
        public double CenterY;  // координата по оси ординат
        public bool IsShaded;   // признак затененности объекта
    }

    /* Базовый класс для реализации поставщика данных разметки видео. */
    abstract public class MarkupProvider
    {
        abstract public void InitPartial(RecordingInfo rec);
            //--> Инициализируются только таблицы Views и RecInfo
            // содержимым структуры rec:
            // необходимо для создания XML-файла видеозаписи.
        abstract public void Init(RecordingInfo rec);
            //--> Кроме таблиц Views и RecInfo также инициализируются
            // таблицы Categories и Tags тэгами по умолчанию:
            // необходимо при создании нового XML-файла разметки.
        abstract public bool Save(string MarkupFilePath);
        abstract public bool Open(string MarkupFilePath);
        abstract public bool Check(RecordingInfo rec);//<--> Views и RecInfo

        abstract public int CategoryInsert(Category category);
        abstract public void CategoryUpdate(Category category);
        abstract public void CategoryDelete(int categoryID);
        abstract public List<Category> CategorySelectAll();
    }
}
