using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;         // элементы ToolStrip

using MarkupData;                   // классы MarkupProvider


namespace UniversalAnnotationApp
{
    // Ссылки на элементы управления формы
    public struct MarkupManagerControls
    {
        public ToolStripStatusLabel statusMarkup;
    }

    // Все Public-Методы (Метод регистрации графических элементов управления, 
    // а также методы обработки событий от элементов управления формы).
    // Могут вызываться прямо из методов формы пользовательского интерфейса.
    public interface IMarkup
    {
        void MarkupGuiBind(MarkupManagerControls controls);
    }

    // Все Protected-методы, которые могут вызываться классами-наследниками и 
    // Public-методы из соответствующего интерфейса. 
    abstract class MarkupManagerBase : CameraManager, IMarkup
    {
        // Такие два метода должны быть у всех слоев выше слоя Camera
        // Слои вызывают эти методы рекурсивно по цепочке
        abstract protected bool MarkupCameraOpen(RecordingInfo rec);
        abstract protected void MarkupCameraClose();

        abstract protected void MarkupCreate();
        abstract protected bool MarkupSave(string MarkupFilePath);
        abstract protected bool MarkupOpen(string MarkupFilePath);
        abstract protected void MarkupClose();

        abstract protected bool MarkupIsOpened { get; }
        abstract protected bool MarkupIsSaved { get; }

        abstract public void MarkupGuiBind(MarkupManagerControls controls);

        abstract public int MarkupCategoryCreate(Category category);
        abstract public void MarkupCategoryUpdate(Category category);
        abstract public void MarkupCategoryDelete(int categoryID);
        abstract public List<Category> MarkupCategoryGetAll();
        abstract public bool MarkupCategoryGetByID(
            int categoryID, out Category category);

        abstract public int MarkupTagCreate(Tag tag);
        abstract public void MarkupTagUpdate(Tag tag);
        abstract public void MarkupTagDelete(int tagID);
        abstract public List<Tag> MarkupTagGetAll();
        abstract public bool MarkupTagGetByID(int tagID, out Tag tag);

        abstract public int MarkupTraceCreate(Trace trace);
        abstract public void MarkupTraceUpdate(Trace trace);
        abstract public void MarkupTraceDelete(int traceID);
        abstract public List<Trace> MarkupTraceGetAll();
        abstract public bool MarkupTraceGetByID(
            int traceID, out Trace trace);

        abstract public void MarkupBoxCreate(Box box);
        abstract public void MarkupBoxUpdate(Box box);
        abstract public void MarkupBoxDelete(int traceID, int frameID);
        abstract public bool MarkupBoxGetByID(
            int traceID, int frameID, out Box box);
        abstract public List<Box> MarkupBoxGetByView(int frameID, int viewID);

        abstract public void MarkupMarkerCreate(Marker marker);
        abstract public void MarkupMarkerUpdate(Marker marker);
        abstract public void MarkupMarkerDelete(int traceID, int frameID);
        abstract public bool MarkupMarkerGetByID(
            int traceID, int frameID, out Marker marker);
        abstract public List<Marker> MarkupMarkerGetByView(
            int frameID, int viewID);
    }

    /* Класс обеспечивает доступ к флагам состояния слоя только через 
     * бизнес-логику обновления графического интерфейса. */
    class MarkupManagerState
    {
        private bool m_IsOpened;            // признак открытия файла
        private bool m_IsSaved;             // признак сохранения изменений
        private MarkupManagerControls m_Controls;

        private void m_ControlsUpdate()
        {
            if (m_Controls.statusMarkup != null)
            {
                if (m_IsOpened)
                {
                    if (m_IsSaved)
                    {
                        m_Controls.statusMarkup.Text = "";
                        m_Controls.statusMarkup.ForeColor =
                            System.Drawing.Color.Black;
                    }
                    else
                    {
                        m_Controls.statusMarkup.Text = "Markup Unsaved";
                        m_Controls.statusMarkup.ForeColor =
                            System.Drawing.Color.Red;
                    }
                }
                else
                {
                    m_Controls.statusMarkup.Text = "No Markup";
                    m_Controls.statusMarkup.ForeColor =
                        System.Drawing.Color.Black;
                }
            }
        }

        public void GuiBind(MarkupManagerControls controls)
        {
            m_Controls = controls;
            m_ControlsUpdate();
        }

        public bool IsOpened
        {
            get { return m_IsOpened; }

            set
            {
                m_IsOpened = value;
                m_ControlsUpdate();
            }
        }

        public bool IsSaved
        {
            get { return m_IsSaved; }

            set
            {
                m_IsSaved = value;
                m_ControlsUpdate();
            }
        }

        public MarkupManagerState()
        {
            m_IsOpened = false;
            m_IsSaved = false;   
        }
    }

    // Реализации Public-, Private- методов, а также Private-члены, к которым
    // не могут обращаться классы-наследники.
    class MarkupManager : MarkupManagerBase
    {
        private MarkupProvider m_markup;    // поставщик данных
        private MarkupManagerState m_state;     // флаги состояния + GUI

        // Методы работы с таблицей категорий объектов
        override public int MarkupCategoryCreate(Category category)
        {
            return m_markup.CategoryCreate(category);
        }

        override public void MarkupCategoryUpdate(Category category)
        {
            m_markup.CategoryUpdate(category);
        }

        override public void MarkupCategoryDelete(int categoryID)
        {
            m_markup.CategoryDelete(categoryID);
        }

        override public List<Category> MarkupCategoryGetAll()
        {
            return m_markup.CategoryGetAll();
        }

        override public bool MarkupCategoryGetByID(
            int categoryID, out Category category)
        {
            return m_markup.CategoryGetByID(categoryID, out category);
        }

        // Методы работы с таблицей объектов
        override public int MarkupTagCreate(Tag tag)
        {
            return m_markup.TagCreate(tag);
        }

        override public void MarkupTagUpdate(Tag tag)
        {
            m_markup.TagUpdate(tag);
        }

        override public void MarkupTagDelete(int tagID)
        {
            m_markup.TagDelete(tagID);
        }

        override public List<Tag> MarkupTagGetAll()
        {
            return m_markup.TagGetAll();
        }

        override public bool MarkupTagGetByID(int tagID, out Tag tag)
        {
            return m_markup.TagGetByID(tagID, out tag);
        }

        // Методы работы с таблицей траекторий объектов
        override public int MarkupTraceCreate(Trace trace)
        {
            return m_markup.TraceCreate(trace);
        }

        override public void MarkupTraceUpdate(Trace trace)
        {
            m_markup.TraceUpdate(trace);
        }

        override public void MarkupTraceDelete(int traceID)
        {
            m_markup.TraceDelete(traceID);
        }

        override public List<Trace> MarkupTraceGetAll()
        {
            return m_markup.TraceGetAll();
        }

        override public bool MarkupTraceGetByID(
            int traceID, out Trace trace)
        {
            return m_markup.TraceGetByID(traceID, out trace);
        }

        // Методы работы с таблицей рамок объектов
        override public void MarkupBoxCreate(Box box)
        {
            m_markup.BoxCreate(box);
        }

        override public void MarkupBoxUpdate(Box box)
        {
            m_markup.BoxUpdate(box);
        }

        override public void MarkupBoxDelete(int traceID, int frameID)
        {
            m_markup.BoxDelete(traceID, frameID);
        }

        override public bool MarkupBoxGetByID(
            int traceID, int frameID, out Box box)
        {
            return m_markup.BoxGetByID(traceID, frameID, out box);
        }

        override public List<Box> MarkupBoxGetByView(int frameID, int viewID)
        {
            return m_markup.BoxGetByView(frameID, viewID);
        }

        // Методы работы с таблицей маркеров объектов
        override public void MarkupMarkerCreate(Marker marker)
        {
            m_markup.MarkerCreate(marker);
        }

        override public void MarkupMarkerUpdate(Marker marker)
        {
            m_markup.MarkerUpdate(marker);
        }

        override public void MarkupMarkerDelete(int traceID, int frameID)
        {
            m_markup.MarkerDelete(traceID, frameID);
        }

        override public bool MarkupMarkerGetByID(
            int traceID, int frameID, out Marker marker)
        {
            return m_markup.MarkerGetByID(traceID, frameID, out marker);
        }

        override public List<Marker> MarkupMarkerGetByView(
            int frameID, int viewID)
        {
            return m_markup.MarkerGetByView(frameID, viewID);
        }

        // Методы файловых операций
        override protected bool MarkupCameraOpen(RecordingInfo rec)
        {
            if (!CameraOpen(rec))
                return false;

            // Инициализируем новую пустую разметку для видеозаписи
            MarkupCreate();
            return true;
        }

        override protected void MarkupCameraClose()
        {
            CameraClose();

            if (MarkupIsOpened)
                MarkupClose();
        }

        /* Метод создания пустой разметки для структуры описания
         * видеозаписи, открытой перед вызовом метода. */
        override protected void MarkupCreate()
        {
            if (!CameraIsOpened)
                throw new Exception("You should open recording first!");
            
            m_markup.Init(CameraRecordingInfo);
            m_state.IsOpened = true;
            m_state.IsSaved = true;
        }

        override protected bool MarkupSave(string MarkupFilePath)
        {
            if (MarkupIsOpened)
            {
                if (!m_markup.Save(MarkupFilePath))
                    return false;
                else
                {
                    m_state.IsSaved = true;
                    return true;
                }
            }
            else
                return false;
        }

        override protected bool MarkupOpen(string MarkupFilePath) 
        {
            if (!CameraIsOpened)
                throw new Exception("You need to open recording first!");

            // Загружаем разметку из XML-файла
            if (!m_markup.Open(MarkupFilePath))
                throw new Exception("Unable to load markup from XML!");

            // Проверяем соответствие загруженной разметке открытой
            // видеозаписи
            if (!m_markup.CheckHeader(CameraRecordingInfo))
                throw new Exception("Markup does not match recording!");

            m_state.IsOpened = true;
            return true;
        }

        override protected void MarkupClose()
        {
            if (MarkupIsOpened)
                m_state.IsOpened = false;
        }

        override protected bool MarkupIsOpened
        {
            get { return m_state.IsOpened; }
        }

        override protected bool MarkupIsSaved
        {
            get { return m_state.IsSaved; }
        }

        override public void MarkupGuiBind(MarkupManagerControls controls) 
        {
            m_state.GuiBind(controls);
        }

        // Конструктор класса по умолчанию
        public MarkupManager()
        {
            m_markup = new MarkupProviderADO();
            m_state = new MarkupManagerState();
        }
    }
}
