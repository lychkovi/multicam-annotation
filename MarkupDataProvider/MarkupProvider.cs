using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;      // работа с DataSet и др. элементами ADO.NET

namespace MarkupData
{
    /* MarkupProvider: Класс реализует представление разметки в памяти
     * с помощью реляционных таблиц класса ADO.NET DataSet и сохранение
     * разметки на диск в файл XML. */
    public class MarkupProvider: MarkupProviderBase
    {
        private DataSet m_data;

        /* Метод создания таблицы категорий объектов */
        private void m_CreateCategories()
        {
            /* Создаем все столбцы таблицы. */
            DataColumn IdColumn = new DataColumn("ID", typeof(int));
            IdColumn.ReadOnly = true;
            IdColumn.AllowDBNull = false;
            IdColumn.Unique = true;
            IdColumn.AutoIncrement = true;
            IdColumn.AutoIncrementSeed = 0;
            IdColumn.AutoIncrementStep = 1;

            DataColumn NameColumn = new DataColumn("Name", typeof(string));
            NameColumn.AllowDBNull = false;
            NameColumn.Unique = true;

            DataColumn CommentColumn = 
                new DataColumn("Comment", typeof(string));
            CommentColumn.AllowDBNull = false;
            CommentColumn.DefaultValue = "";

            DataColumn ColorRedColumn = 
                new DataColumn("ColorRed", typeof(UInt16));
            ColorRedColumn.AllowDBNull = false;
            ColorRedColumn.DefaultValue = 255;

            DataColumn ColorGreenColumn =
                new DataColumn("ColorGreen", typeof(UInt16));
            ColorGreenColumn.AllowDBNull = false;
            ColorGreenColumn.DefaultValue = 0;

            DataColumn ColorBlueColumn =
                new DataColumn("ColorBlue", typeof(UInt16));
            ColorBlueColumn.AllowDBNull = false;
            ColorBlueColumn.DefaultValue = 0;

            /* Создаем саму таблицу. */
            DataTable CategoriesTable = new DataTable("Categories");
            CategoriesTable.Columns.AddRange(new DataColumn[] { IdColumn, 
                NameColumn, CommentColumn, ColorRedColumn, ColorGreenColumn, 
                ColorBlueColumn });
            CategoriesTable.PrimaryKey = new DataColumn[] 
                { CategoriesTable.Columns[0] };

            /* Добавляем в таблицу нулевую категорию Unknown по умолчанию 
             * для новых объектов. */
            DataRow DefaultCategory = CategoriesTable.NewRow();
            DefaultCategory["Name"] = "Unknown";
            DefaultCategory["Comment"] = "Default category for new tags";
            CategoriesTable.Rows.Add(DefaultCategory);

            /* Сохраняем таблицу в объект DataSet в памяти. */
            m_data.Tables.Add(CategoriesTable);
        }

        /* Метод создания таблицы меток объектов. */
        private void m_CreateTags()
        {
            DataColumn IdColumn = new DataColumn("ID", typeof(int));
            IdColumn.ReadOnly = true;
            IdColumn.AllowDBNull = false;
            IdColumn.Unique = true;
            IdColumn.AutoIncrement = true;
            IdColumn.AutoIncrementSeed = 0;
            IdColumn.AutoIncrementStep = 1;

            DataColumn categoryIdColumn = 
                new DataColumn("CategoryID", typeof(int));
            categoryIdColumn.AllowDBNull = false;
            categoryIdColumn.DefaultValue = 0; // категория "Unknown"

            DataColumn CommentColumn =
                new DataColumn("Comment", typeof(string));
            CommentColumn.AllowDBNull = false;
            CommentColumn.DefaultValue = "";

            /* Создаем саму таблицу. */
            DataTable TagsTable = new DataTable("Tags");
            TagsTable.Columns.AddRange(new DataColumn[] { IdColumn, 
                categoryIdColumn, CommentColumn });
            TagsTable.PrimaryKey = new DataColumn[] { TagsTable.Columns[0] };

            /* Добавляем в таблицу нулевую метку Unknown по умолчанию 
             * для новых траекторий. */
            DataRow DefaultTag = TagsTable.NewRow();
            DefaultTag["Comment"] = "Unknown";
            TagsTable.Rows.Add(DefaultTag);

            /* Сохраняем таблицу в объект DataSet в памяти. */
            m_data.Tables.Add(TagsTable);
        }

        /* Метод создания таблицы общих сведений о видеозаписи. */
        private void m_CreateRecordingInfo()
        {
            DataColumn FileNameXmlColumn = 
                new DataColumn("FileNameXML", typeof(string));
            FileNameXmlColumn.AllowDBNull = false;
            FileNameXmlColumn.Unique = true;

            DataColumn FramesCountColumn = 
                new DataColumn("FramesCount", typeof(int));
            FramesCountColumn.AllowDBNull = false;

            DataColumn FpsColumn = new DataColumn("Fps", typeof(double));
            FpsColumn.AllowDBNull = false;
            FpsColumn.ReadOnly = true;

            DataColumn CommentColumn = 
                new DataColumn("Comment", typeof(string));
            CommentColumn.AllowDBNull = false;
            CommentColumn.DefaultValue = "";

            DataColumn AuthorColumn = 
                new DataColumn("Author", typeof(string));
            AuthorColumn.AllowDBNull = false;
            AuthorColumn.DefaultValue = "";

            DataColumn DateTimeColumn = 
                new DataColumn("DateTime", typeof(DateTime));
            DateTimeColumn.AllowDBNull = false;

            DataTable RecordingInfoTable = new DataTable("RecordingInfo");
            RecordingInfoTable.Columns.AddRange(new DataColumn [] 
                { FileNameXmlColumn, FramesCountColumn, FpsColumn, 
                    CommentColumn, AuthorColumn, DateTimeColumn });
            RecordingInfoTable.PrimaryKey = new DataColumn[] 
                { RecordingInfoTable.Columns[0] };

            m_data.Tables.Add(RecordingInfoTable);
        }

        /* Метод создания таблицы характеристик отдельных камер. */
        private void m_CreateViews()
        {
            DataColumn IdColumn = new DataColumn("ID", typeof(int));
            IdColumn.ReadOnly = true;
            IdColumn.AllowDBNull = false;
            IdColumn.Unique = true;
            IdColumn.AutoIncrement = true;
            IdColumn.AutoIncrementSeed = 0;
            IdColumn.AutoIncrementStep = 1;
            
            DataColumn FrameWidthColumn = 
                new DataColumn("FrameWidth", typeof(int));
            FrameWidthColumn.AllowDBNull = false;
            FrameWidthColumn.ReadOnly = true;

            DataColumn FrameHeightColumn =
                new DataColumn("FrameHeight", typeof(int));
            FrameHeightColumn.AllowDBNull = false;
            FrameHeightColumn.ReadOnly = true;

            DataColumn IsColourColumn =
                new DataColumn("IsColour", typeof(bool));
            IsColourColumn.ReadOnly = true;
            IsColourColumn.AllowDBNull = false;

            DataColumn FileNameAviColumn = 
                new DataColumn("FileNameAVI", typeof(string));
            FileNameAviColumn.AllowDBNull = false;

            DataColumn CommentColumn =
                new DataColumn("Comment", typeof(string));
            CommentColumn.AllowDBNull = false;
            CommentColumn.DefaultValue = "";

            DataTable ViewsTable = new DataTable("Views");
            ViewsTable.Columns.AddRange(new DataColumn[] { IdColumn, 
                FrameWidthColumn, FrameHeightColumn, IsColourColumn, 
                FileNameAviColumn, CommentColumn });
            ViewsTable.PrimaryKey = 
                new DataColumn[] { ViewsTable.Columns[0] };

            m_data.Tables.Add(ViewsTable);
        }

        /* Метод добавления новой таблицы траекторий. */
        private void m_CreateTraces()
        {
            DataColumn IdColumn = new DataColumn("ID", typeof(int));
            IdColumn.ReadOnly = true;
            IdColumn.AllowDBNull = false;
            IdColumn.Unique = true;
            IdColumn.AutoIncrement = true;
            IdColumn.AutoIncrementSeed = 0;
            IdColumn.AutoIncrementStep = 1;

            DataColumn ViewIdColumn = new DataColumn("ViewID", typeof(int));
            ViewIdColumn.ReadOnly = true;
            ViewIdColumn.AllowDBNull = false;

            DataColumn TagIdColumn = new DataColumn("TagID", typeof(int));
            TagIdColumn.AllowDBNull = false;
            TagIdColumn.DefaultValue = 0;

            DataColumn HasBoxColumn = new DataColumn("HasBox", typeof(bool));
            HasBoxColumn.ReadOnly = true;
            HasBoxColumn.AllowDBNull = false;

            DataTable TracesTable = new DataTable("Traces");
            TracesTable.Columns.AddRange(new DataColumn[] { IdColumn, 
                ViewIdColumn, TagIdColumn, HasBoxColumn });
            TracesTable.PrimaryKey = 
                new DataColumn[] { TracesTable.Columns[0] };

            m_data.Tables.Add(TracesTable);
        }

        /* Метод добавления новой таблицы рамок объектов в кадре. */
        private void m_CreateBoxes()
        {
            DataColumn TraceIdColumn =
                new DataColumn("TraceID", typeof(int));
            TraceIdColumn.ReadOnly = true;
            TraceIdColumn.AllowDBNull = false;

            DataColumn FrameIdColumn = 
                new DataColumn("FrameID", typeof(int));
            FrameIdColumn.ReadOnly = true;
            FrameIdColumn.AllowDBNull = false;

            DataColumn BoundLeftColumn = 
                new DataColumn("BoundLeft", typeof(double));
            DataColumn BoundTopColumn = 
                new DataColumn("BoundTop", typeof(double));
            DataColumn BoundRightColumn = 
                new DataColumn("BoundRight", typeof(double));
            DataColumn BoundBottomColumn = 
                new DataColumn("BoundBottom", typeof(double));
            BoundLeftColumn.AllowDBNull = false;
            BoundTopColumn.AllowDBNull = false;
            BoundRightColumn.AllowDBNull = false;
            BoundBottomColumn.AllowDBNull = false;

            DataColumn IsOccludedColumn = 
                new DataColumn("IsOccluded", typeof(bool));
            IsOccludedColumn.AllowDBNull = false;
            IsOccludedColumn.DefaultValue = false;

            DataColumn IsShadedColumn =
                new DataColumn("IsShaded", typeof(bool));
            IsShadedColumn.AllowDBNull = false;
            IsShadedColumn.DefaultValue = false;

            DataTable BoxesTable = new DataTable("Boxes");
            BoxesTable.Columns.AddRange(new DataColumn[] { TraceIdColumn, 
                FrameIdColumn, BoundLeftColumn, BoundTopColumn, 
                BoundRightColumn, BoundBottomColumn, IsOccludedColumn, 
                IsShadedColumn });
            BoxesTable.PrimaryKey = new DataColumn[] 
                { BoxesTable.Columns["TraceID"], 
                    BoxesTable.Columns["FrameID"] };

            m_data.Tables.Add(BoxesTable);
        }

        /* Метод добавления таблицы точечных маркеров объектов в кадре. */
        private void m_CreateMarkers()
        {
            DataColumn TraceIdColumn =
                new DataColumn("TraceID", typeof(int));
            TraceIdColumn.ReadOnly = true;
            TraceIdColumn.AllowDBNull = false;

            DataColumn FrameIdColumn = 
                new DataColumn("FrameID", typeof(int));
            FrameIdColumn.ReadOnly = true;
            FrameIdColumn.AllowDBNull = false;

            DataColumn CenterXColumn = 
                new DataColumn("CenterX", typeof(double));
            CenterXColumn.AllowDBNull = false;

            DataColumn CenterYColumn =
                new DataColumn("CenterY", typeof(double));
            CenterYColumn.AllowDBNull = false;

            DataColumn IsShadedColumn =
                new DataColumn("IsShaded", typeof(bool));
            IsShadedColumn.AllowDBNull = false;
            IsShadedColumn.DefaultValue = false;

            DataTable MarkersTable = new DataTable("Markers");
            MarkersTable.Columns.AddRange(new DataColumn[] 
                { TraceIdColumn, FrameIdColumn, CenterXColumn, 
                    CenterYColumn, IsShadedColumn });
            MarkersTable.PrimaryKey = new DataColumn[] 
                { MarkersTable.Columns["TraceID"], 
                    MarkersTable.Columns["FrameID"] };

            m_data.Tables.Add(MarkersTable);
        }

        /* Метод добавления связей между таблицами в базе. */
        private void m_CreateRelations()
        {
            // Создаем связь Categories <-- Tags (один - много)
            DataRelation dr = new DataRelation("1Category--MTags", 
                m_data.Tables["Categories"].Columns["ID"],    // один
                m_data.Tables["Tags"].Columns["CategoryID"]); // много
            m_data.Relations.Add(dr);

            // Создаем связь Tags <-- Traces (один - много)
            dr = new DataRelation("1Tag--MTraces",
                m_data.Tables["Tags"].Columns["ID"],
                m_data.Tables["Traces"].Columns["TagID"]);
            m_data.Relations.Add(dr);

            // Создаем связь Views <-- Traces (один - много)
            dr = new DataRelation("1View--MTraces",
                m_data.Tables["Views"].Columns["ID"],
                m_data.Tables["Traces"].Columns["ViewID"]);
            m_data.Relations.Add(dr);

            // Создаем связь Traces <-- Boxes (один - много)
            dr = new DataRelation("1Trace--MBoxes",
                m_data.Tables["Traces"].Columns["ID"],
                m_data.Tables["Boxes"].Columns["TraceID"]);
            m_data.Relations.Add(dr);

            // Создаем связь Traces <-- Markers (один - много)
            dr = new DataRelation("1Trace--MMarkers",
                m_data.Tables["Traces"].Columns["ID"],
                m_data.Tables["Markers"].Columns["TraceID"]);
            m_data.Relations.Add(dr);
        }

        /* Reset: Удаление всего содержимого разметки и создание схемы 
         * всех таблиц с нуля (без заполнения данными), его нужно вызывать 
         * при закрытии файла разметки. */
        override public void Reset()
        {
            // Удаляем всю информацию о разметке из памяти
            m_data.Clear();

            // Заново создаем все таблицы
            m_CreateRecordingInfo();
            m_CreateViews();
            m_CreateCategories();
            m_CreateTags();
            m_CreateTraces();
            m_CreateBoxes();
            m_CreateMarkers();

            // Создаем связи между таблицами
            m_CreateRelations();
        }

        /* Инициализация таблиц Views и RecInfo, его нужно вызывать при
         * создании нового файла разметки для заданной видеозаписи. */
        override public bool Init(RecordingInfo rec)
        {
            Reset();
            return false;
        }

        /* Save: Сохранение разметки в XML-файл */
        override public bool Save(string MarkupFilePath)
        {
            return false;
        }

        /* Open: Загрузка разметки из XML-файла. */
        override public bool Open(string MarkupFilePath)
        {
            return false;
        }

        /* Check: Проверка соответствия загруженной разметки параметрам
         * файла видеозаписи. */
        override public bool Check(RecordingInfo rec)
        {
            return false;
        }

        /* Блок методов для работы с таблицей категорий объектов. */
        override public int CategoryInsert(Category category)
        {
            int ID;
            DataRow row = m_data.Tables["Categories"].NewRow();
            ID = (int) row["ID"];
            row["Name"] = category.Name;
            row["Comment"] = category.Comment;
            m_data.Tables["Categories"].Rows.Add(row);
            return ID;
        }

        override public void CategoryUpdate(Category category)
        {
            // Фильтр для выбора интересующей строки
            string filterStr = 
                string.Format("ID = '{0}'", category.ID.ToString());

            DataRow[] rows = m_data.Tables["Categories"].Select(filterStr);

            if (rows.Length == 0)
            {
                // Интересующая строка не найдена!
                throw new KeyNotFoundException();
            }
            else
            {
                // Обновляем содержание найденной строки
                DataRow row = rows[0];
                row.BeginEdit();
                row["Name"] = category.Name;
                row["Comment"] = category.Comment;
                row.EndEdit();
                m_data.AcceptChanges();
            }
        }

        override public void CategoryDelete(int categoryID)
        {
            // Фильтр для выбора интересующей строки
            string filterStr = 
                string.Format("ID = '{0}'", categoryID);

            DataRow[] rows = m_data.Tables["Categories"].Select(filterStr);

            if (rows.Length == 0)
            {
                // Интересующая строка не найдена!
                throw new KeyNotFoundException();
            }
            else
            {
                // Удаляем найденную строку
                rows[0].Delete();
                m_data.AcceptChanges();
            }
        }

        override public List<Category> CategorySelectAll()
        {
            DataRow[] rows = m_data.Tables["Categories"].Select();
            List<Category> list = new List<Category>();
            for (int i = 0; i < rows.Length; i++)
            {
                Category item = new Category();
                DataRow row = rows[i];
                item.ID = (int) row["ID"];
                item.Name = (string) row["Name"];
                item.Comment = (string) row["Comment"];
                item.ColorRed = (UInt16) row["ColorRed"];
                item.ColorGreen = (UInt16) row["ColorGreen"];
                item.ColorBlue = (UInt16) row["ColorBlue"];
                list.Add(item);
            }
            return list;
        }

        public MarkupProvider()
        {
            m_data = new DataSet();
            Reset();
        }
    }
}
