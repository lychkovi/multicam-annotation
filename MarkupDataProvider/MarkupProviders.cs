using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;      // работа с DataSet и др. элементами ADO.NET

namespace MarkupData
{
    /* MarkupProviderADO: Класс реализует представление разметки в памяти
     * с помощью реляционных таблиц класса ADO.NET DataSet и сохранение
     * разметки на диск в файл XML. */
    public class MarkupProviderADO: MarkupProvider
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
                { CategoriesTable.Columns["ID"] };

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

            DataColumn CategoryIdColumn = 
                new DataColumn("CategoryID", typeof(int));
            CategoryIdColumn.AllowDBNull = false;
            CategoryIdColumn.DefaultValue = 0; // категория "Unknown"

            DataColumn CommentColumn =
                new DataColumn("Name", typeof(string));
            CommentColumn.AllowDBNull = false;
            CommentColumn.DefaultValue = "";

            /* Создаем саму таблицу. */
            DataTable TagsTable = new DataTable("Tags");
            TagsTable.Columns.AddRange(new DataColumn[] { IdColumn, 
                CategoryIdColumn, CommentColumn });
            TagsTable.PrimaryKey = new DataColumn[] 
                { TagsTable.Columns["ID"] };

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
                { RecordingInfoTable.Columns["FileNameXML"] };

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
                new DataColumn[] { ViewsTable.Columns["ID"] };

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

            DataColumn FrameStartColumn =
                new DataColumn("FrameStart", typeof(int));
            FrameStartColumn.AllowDBNull = false;

            DataColumn FrameEndColumn =
                new DataColumn("FrameEnd", typeof(int));
            FrameEndColumn.AllowDBNull = false;

            DataColumn HasBoxColumn = new DataColumn("HasBox", typeof(bool));
            HasBoxColumn.ReadOnly = true;
            HasBoxColumn.AllowDBNull = false;

            DataTable TracesTable = new DataTable("Traces");
            TracesTable.Columns.AddRange(new DataColumn[] { IdColumn, 
                ViewIdColumn, TagIdColumn, FrameStartColumn, FrameEndColumn, 
                HasBoxColumn });
            TracesTable.PrimaryKey =
                new DataColumn[] { TracesTable.Columns["ID"] };

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

            DataColumn PosXColumn = 
                new DataColumn("PosX", typeof(int));
            DataColumn PosYColumn = 
                new DataColumn("PosY", typeof(int));
            DataColumn WidthColumn = 
                new DataColumn("Width", typeof(int));
            DataColumn HeightColumn = 
                new DataColumn("Height", typeof(int));
            PosXColumn.AllowDBNull = false;
            PosYColumn.AllowDBNull = false;
            WidthColumn.AllowDBNull = false;
            HeightColumn.AllowDBNull = false;

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
                FrameIdColumn, PosXColumn, PosYColumn, 
                WidthColumn, HeightColumn, IsOccludedColumn, 
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

            DataColumn PosXColumn = 
                new DataColumn("PosX", typeof(int));
            PosXColumn.AllowDBNull = false;

            DataColumn PosYColumn =
                new DataColumn("PosY", typeof(int));
            PosYColumn.AllowDBNull = false;

            DataColumn IsShadedColumn =
                new DataColumn("IsShaded", typeof(bool));
            IsShadedColumn.AllowDBNull = false;
            IsShadedColumn.DefaultValue = false;

            DataTable MarkersTable = new DataTable("Markers");
            MarkersTable.Columns.AddRange(new DataColumn[] 
                { TraceIdColumn, FrameIdColumn, PosXColumn, 
                    PosYColumn, IsShadedColumn });
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

        /* CreateSchema: Удаление всего содержимого разметки и создание схемы 
         * всех таблиц с нуля (без заполнения данными), его нужно вызывать 
         * один раз в констукторе класса. */
        private void m_CreateSchema()
        {
            // Создаем все таблицы
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

        /* InitCategories: Добавляем категорию объектов по умолчанию. */
        private void m_InitCategories()
        {
            /* Добавляем в таблицу нулевую категорию Unknown по умолчанию 
             * для новых объектов. */
            DataRow DefaultCategory = m_data.Tables["Categories"].NewRow();
            DefaultCategory["Name"] = "Unknown";
            DefaultCategory["Comment"] = "Default category for new tags";
            m_data.Tables["Categories"].Rows.Add(DefaultCategory);
        }

        /* InitTags: Добавляем метку объекта по умолчнию. */
        private void m_InitTags()
        {
            /* Добавляем в таблицу нулевую метку Unknown по умолчанию 
             * для новых траекторий. */
            DataRow DefaultTag = m_data.Tables["Tags"].NewRow();
            DefaultTag["Name"] = "Unknown";
            m_data.Tables["Tags"].Rows.Add(DefaultTag);
        }

        /* InitRecordingInfo: Инициализируем таблицу RecordingInfo 
         * сведениями из переданной структуры. */
        private void m_InitRecordingInfo(RecordingInfo rec)
        {
            DataRow Info = m_data.Tables["RecordingInfo"].NewRow();
            Info["FileNameXML"] = rec.FileNameXML;
            Info["FramesCount"] = rec.FramesCount;
            Info["Fps"] = rec.Fps;
            Info["Comment"] = rec.Comment;
            Info["DateTime"] = rec.DateTime;
            m_data.Tables["RecordingInfo"].Rows.Add(Info);
        }

        /* InitViews: Инициализируем таблицу Views сведениями об отдельных
         * камерах из структуры описания видеозаписи. */
        private void m_InitViews(RecordingInfo rec)
        {
            //DataRow row;
            foreach (View item in rec.Views)
            {
                DataRow row = m_data.Tables["Views"].NewRow();
                row["FrameWidth"] = item.FrameWidth;
                row["FrameHeight"] = item.FrameHeight;
                row["IsColour"] = item.IsColour;
                row["FileNameAVI"] = item.FileNameAVI;
                row["Comment"] = item.Comment;
                m_data.Tables["Views"].Rows.Add(row);
            }
        }

        /* Инициализируются только таблицы Views и RecInfo, метод нужно
         * вызывать при создании нового XML-файла видеозаписи. */
        override public void InitHeader(RecordingInfo rec)
        {
            m_data.Clear();

            // Это нужно, чтобы сбросить счетчики инкрементирования индексов
            m_data.Reset();
            m_CreateSchema();

            m_InitRecordingInfo(rec);
            m_InitViews(rec);
        }

        /* Инициализация таблиц Views, RecInfo, Categories, Tags, метод
         * нужно вызывать при создании нового XML-файла разметки для 
         * заданной видеозаписи. */ 
        override public void Init(RecordingInfo rec)
        {
            InitHeader(rec);
            m_InitCategories();
            m_InitTags();
        }

        /* GetHeader: Метод возвращает структуру RecordingInfo, содержащую
         * таблицы RecordingInfo и Views данных. */
        override public RecordingInfo GetHeader()
        {
            RecordingInfo rec = new RecordingInfo("");
            if (m_data.Tables["RecordingInfo"].Rows.Count == 0)
                throw new Exception("XML file was not opened!");

            DataRow info = m_data.Tables["RecordingInfo"].Rows[0];
            rec.FileNameXML = (string) info["FileNameXML"];
            rec.FramesCount = (int) info["FramesCount"];
            rec.Fps = (double) info["Fps"];
            rec.DateTime = (DateTime) info["DateTime"];
            rec.Comment = (string) info["Comment"];
            
            DataRow[] rows = m_data.Tables["Views"].Select();
            for (int i = 0; i < rows.Length; i++)
            {
                View item = new View();
                DataRow row = rows[i];
                item.ID = (int)row["ID"];
                item.FileNameAVI = (string)row["FileNameAVI"];
                item.FrameWidth = (int)row["FrameWidth"];
                item.FrameHeight = (int)row["FrameHeight"];
                item.IsColour = (bool)row["IsColour"];
                item.Comment = (string)row["Comment"];
                rec.Views.Add(item);
            }

            return rec;
        }

        /* CheckHeader: Проверка соответствия загруженной разметки параметрам
         * файла видеозаписи, представленной структуруой rec. */
        override public bool CheckHeader(RecordingInfo rec)
        {
            RecordingInfo header = GetHeader();

            bool isOK = true;
            isOK = isOK && (header.FramesCount == rec.FramesCount);
            isOK = isOK && (header.Fps == rec.Fps);
            isOK = isOK && (header.Views.Count == rec.Views.Count);
            for (int i = 0; i < header.Views.Count; i++)
            {
                isOK = isOK && (header.Views[i].FrameWidth ==
                    rec.Views[i].FrameWidth);
                isOK = isOK && (header.Views[i].FrameHeight ==
                    rec.Views[i].FrameHeight);
            }
            return isOK;
        }

        /* Блок методов для работы с таблицей категорий объектов. */
        private Category m_CategoryRead(DataRow row)
        {
            Category category = new Category();
            category.ID = (int)row["ID"];
            category.Name = (string)row["Name"];
            category.Comment = (string)row["Comment"];
            category.ColorRed = (UInt16)row["ColorRed"];
            category.ColorGreen = (UInt16)row["ColorGreen"];
            category.ColorBlue = (UInt16)row["ColorBlue"];
            return category;
        }

        private void m_CategoryWrite(Category category, DataRow row)
        {
            row.BeginEdit();
            row["Name"] = category.Name;
            row["Comment"] = category.Comment;
            row.EndEdit();
        }

        override public int CategoryCreate(Category category)
        {
            int ID;
            DataRow row = m_data.Tables["Categories"].NewRow();
            ID = (int) row["ID"];
            m_CategoryWrite(category, row);
            m_data.Tables["Categories"].Rows.Add(row);
            return ID;
        }

        override public void CategoryUpdate(Category category)
        {
            // Фильтр для выбора интересующей строки
            string filterStr = 
                string.Format("ID = '{0}'", category.ID);

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
                m_CategoryWrite(category, row);
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

        override public List<Category> CategoryGetAll()
        {
            DataRow[] rows = m_data.Tables["Categories"].Select();
            List<Category> list = new List<Category>();
            for (int i = 0; i < rows.Length; i++)
            {
                Category item = m_CategoryRead(rows[i]);
                list.Add(item);
            }
            return list;
        }

        override public bool CategoryGetByID(
            int categoryID, out Category item)
        {
            string filterStr = string.Format("ID = '{0}'", categoryID);
            // более сложный фильтр: "ID = 0 AND Name = 'Unknown0'";
            DataRow[] rows =
                m_data.Tables["Categories"].Select(filterStr);
            if (rows.Length > 0)
            {
                item = m_CategoryRead(rows[0]);
                return true;
            }
            else
            {
                item = new Category();
                return false;
            }
        }

        /* Блок методов для работы с таблицей объектов. */
        private Tag m_TagRead(DataRow row)
        {
            Tag tag = new Tag();
            tag.ID = (int)row["ID"];
            tag.CategoryID = (int)row["CategoryID"];
            tag.Name = (string)row["Name"];
            return tag;
        }

        private void m_TagWrite(Tag tag, DataRow row)
        {
            row.BeginEdit();
            row["CategoryID"] = tag.CategoryID;
            row["Name"] = tag.Name;
            row.EndEdit();
        }

        override public int TagCreate(Tag tag)
        {
            int ID;
            DataRow row = m_data.Tables["Tags"].NewRow();
            ID = (int)row["ID"];
            m_TagWrite(tag, row);
            m_data.Tables["Tags"].Rows.Add(row);
            return ID;
        }

        override public void TagUpdate(Tag tag)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("ID = '{0}'", tag.ID);

            DataRow[] rows = m_data.Tables["Tags"].Select(filterStr);

            if (rows.Length == 0)
            {
                // Интересующая строка не найдена!
                throw new KeyNotFoundException();
            }
            else
            {
                // Обновляем содержание найденной строки
                DataRow row = rows[0];
                m_TagWrite(tag, row);
                m_data.AcceptChanges();
            }
        }

        override public void TagDelete(int tagID)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("ID = '{0}'", tagID);

            DataRow[] rows = m_data.Tables["Tags"].Select(filterStr);

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

        override public List<Tag> TagGetAll()
        {
            DataRow[] rows = m_data.Tables["Tags"].Select();
            List<Tag> list = new List<Tag>();
            for (int i = 0; i < rows.Length; i++)
            {
                Tag tag = m_TagRead(rows[i]);
                list.Add(tag);
            }
            return list;
        }

        override public bool TagGetByID(int tagID, out Tag tag)
        {
            // Фильтр для выбора интересующей строки
            string filterStr = string.Format("ID = '{0}'", tagID);
            DataRow[] rows = m_data.Tables["Tags"].Select(filterStr);
            if (rows.Length > 0)
            {
                tag = m_TagRead(rows[0]);
                return true;
            }
            else
            {
                tag = new Tag();
                return false;
            }
        }

        /* Блок методов для работы с таблицей траекторий. */
        private Trace m_TraceRead(DataRow row)
        {
            Trace trace = new Trace();
            trace.ID = (int)row["ID"];
            trace.ViewID = (int)row["ViewID"];
            trace.TagID = (int)row["TagID"];
            trace.FrameStart = (int)row["FrameStart"];
            trace.FrameEnd = (int)row["FrameEnd"];
            trace.HasBox = (bool)row["HasBox"];
            return trace;
        }

        private void m_TraceWrite(Trace trace, DataRow row)
        {
            row.BeginEdit();
            // Дополнительная защита для полей READ-ONLY
            if (row.RowState == DataRowState.Detached)
            {
                row["ViewID"] = trace.ViewID;
                row["TagID"] = trace.TagID;
                row["HasBox"] = trace.HasBox;
            }
            row["FrameStart"] = trace.FrameStart;
            row["FrameEnd"] = trace.FrameEnd;
            row.EndEdit();
        }

        override public int TraceCreate(Trace trace)
        {
            int ID;
            DataRow row = m_data.Tables["Traces"].NewRow();
            ID = (int)row["ID"];
            m_TraceWrite(trace, row);
            m_data.Tables["Traces"].Rows.Add(row);
            return ID;
        }

        override public void TraceUpdate(Trace trace)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("ID = '{0}'", trace.ID);

            DataRow[] rows = m_data.Tables["Traces"].Select(filterStr);

            if (rows.Length == 0)
            {
                // Интересующая строка не найдена!
                throw new KeyNotFoundException();
            }
            else
            {
                // Обновляем содержание найденной строки
                DataRow row = rows[0];
                m_TraceWrite(trace, row);
                m_data.AcceptChanges();
            }
        }

        override public void TraceDelete(int traceID)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("ID = '{0}'", traceID);

            DataRow[] rows = m_data.Tables["Traces"].Select(filterStr);

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

        override public List<Trace> TraceGetAll()
        {
            DataRow[] rows = m_data.Tables["Traces"].Select();
            List<Trace> list = new List<Trace>();
            for (int i = 0; i < rows.Length; i++)
            {
                Trace trace = m_TraceRead(rows[i]);
                list.Add(trace);
            }
            return list;
        }

        override public bool TraceGetByID(int traceID, out Trace trace)
        {
            // Фильтр для выбора интересующей строки
            string filterStr = string.Format("ID = '{0}'", traceID);
            DataRow[] rows = m_data.Tables["Traces"].Select(filterStr);
            if (rows.Length > 0)
            {
                trace = m_TraceRead(rows[0]);
                return true;
            }
            else
            {
                trace = new Trace();
                return false;
            }
        }

        /* Блок методов для работы с таблицей рамок объектов. */
        private Box m_BoxRead(DataRow row)
        {
            Box box = new Box();
            box.TraceID = (int)row["TraceID"];
            box.FrameID = (int)row["FrameID"];
            box.PosX = (int)row["PosX"];
            box.PosY = (int)row["PosY"];
            box.Width = (int)row["Width"];
            box.Height = (int)row["Height"];
            box.IsShaded = (bool)row["IsShaded"];
            box.IsOccluded = (bool)row["IsOccluded"];
            return box;
        }

        private void m_BoxWrite(Box box, DataRow row)
        {
            row.BeginEdit();
            // Дополнительная защита для полей READ-ONLY
            if (row.RowState == DataRowState.Detached)
            {
                row["TraceID"] = box.TraceID;
                row["FrameID"] = box.FrameID;
            }
            row["PosX"] = box.PosX;
            row["PosY"] = box.PosY;
            row["Width"] = box.Width;
            row["Height"] = box.Height;
            row["IsShaded"] = box.IsShaded;
            row["IsOccluded"] = box.IsOccluded;
            row.EndEdit();
        }

        override public void BoxCreate(Box box)
        {
            DataRow row = m_data.Tables["Boxes"].NewRow();
            m_BoxWrite(box, row);
            m_data.Tables["Boxes"].Rows.Add(row);
        }

        override public void BoxUpdate(Box box)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("TraceID = '{0}' AND FrameID = '{1}'", 
                box.TraceID, box.FrameID);

            DataRow[] rows = m_data.Tables["Boxes"].Select(filterStr);

            if (rows.Length == 0)
            {
                // Интересующая строка не найдена!
                throw new KeyNotFoundException();
            }
            else
            {
                // Обновляем содержание найденной строки
                DataRow row = rows[0];
                m_BoxWrite(box, row);
                m_data.AcceptChanges();
            }
        }

        override public void BoxDelete(int traceID, int frameID)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("TraceID = '{0}' AND FrameID = '{1}'",
                traceID, frameID);

            DataRow[] rows = m_data.Tables["Boxes"].Select(filterStr);

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

        override public bool BoxGetByID(
            int traceID, int frameID, out Box box)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("TraceID = '{0}' AND FrameID = '{1}'",
                traceID, frameID);
            DataRow[] rows = m_data.Tables["Boxes"].Select(filterStr);
            if (rows.Length > 0)
            {
                box = m_BoxRead(rows[0]);
                return true;
            }
            else
            {
                box = new Box();
                return false;
            }
        }

        override public List<Box> BoxGetByView(int frameID, int viewID)
        {
            // Запрос к объединению двух таблиц
            DataTable boxes = m_data.Tables["Boxes"];
            DataTable traces = m_data.Tables["Traces"];

            var query =
                from box in boxes.AsEnumerable()
                join trace in traces.AsEnumerable()
                on box.Field<int>("TraceID") equals
                    trace.Field<int>("ID")
                where box.Field<int>("FrameID") == frameID
                && trace.Field<int>("ViewID") == viewID
                select new
                {
                    TraceID = box.Field<int>("TraceID"),
                    PosX = box.Field<int>("PosX"),
                    PosY = box.Field<int>("PosY"),
                    Width = box.Field<int>("Width"),
                    Height = box.Field<int>("Height"),
                    IsShaded = box.Field<bool>("IsShaded"),
                    IsOccluded = box.Field<bool>("IsOccluded")
                };

            List<Box> list = new List<Box>();
            foreach (var elem in query)
            {
                Box item = new Box();
                item.TraceID = elem.TraceID;
                item.FrameID = frameID;
                item.PosX = elem.PosX;
                item.PosY = elem.PosY;
                item.Width = elem.Width;
                item.Height = elem.Height;
                item.IsShaded = elem.IsShaded;
                item.IsOccluded = elem.IsOccluded;
                list.Add(item);
            }
            return list;
        }

        /* Блок методов для работы с таблицей маркеров объектов. */
        private Marker m_MarkerRead(DataRow row)
        {
            Marker marker = new Marker();
            marker.TraceID = (int)row["TraceID"];
            marker.FrameID = (int)row["FrameID"];
            marker.PosX = (int)row["PosX"];
            marker.PosY = (int)row["PosY"];
            marker.IsShaded = (bool)row["IsShaded"];
            return marker;
        }

        private void m_MarkerWrite(Marker marker, DataRow row)
        {
            row.BeginEdit();
            // Дополнительная защита для полей READ-ONLY
            if (row.RowState == DataRowState.Detached)
            {
                row["TraceID"] = marker.TraceID;
                row["FrameID"] = marker.FrameID;
            }
            row["PosX"] = marker.PosX;
            row["PosY"] = marker.PosY;
            row["IsShaded"] = marker.IsShaded;
            row.EndEdit();
        }

        override public void MarkerCreate(Marker marker)
        {
            DataRow row = m_data.Tables["Markers"].NewRow();
            m_MarkerWrite(marker, row);
            m_data.Tables["Markers"].Rows.Add(row);
        }

        override public void MarkerUpdate(Marker marker)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("TraceID = '{0}' AND FrameID = '{1}'",
                marker.TraceID, marker.FrameID);

            DataRow[] rows = m_data.Tables["Markers"].Select(filterStr);

            if (rows.Length == 0)
            {
                // Интересующая строка не найдена!
                throw new KeyNotFoundException();
            }
            else
            {
                // Обновляем содержание найденной строки
                DataRow row = rows[0];
                m_MarkerWrite(marker, row);
                m_data.AcceptChanges();
            }
        }

        override public void MarkerDelete(int traceID, int frameID)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("TraceID = '{0}' AND FrameID = '{1}'",
                traceID, frameID);

            DataRow[] rows = m_data.Tables["Markers"].Select(filterStr);

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

        override public bool MarkerGetByID(
            int traceID, int frameID, out Marker marker)
        {
            // Фильтр для выбора интересующей строки
            string filterStr =
                string.Format("TraceID = '{0}' AND FrameID = '{1}'",
                traceID, frameID);
            DataRow[] rows = m_data.Tables["Markers"].Select(filterStr);
            if (rows.Length > 0)
            {
                marker = m_MarkerRead(rows[0]);
                return true;
            }
            else
            {
                marker = new Marker();
                return false;
            }
        }

        override public List<Marker> MarkerGetByView(
            int frameID, int viewID)
        {
            // Запрос к объединению двух таблиц
            DataTable markers = m_data.Tables["Markers"];
            DataTable traces = m_data.Tables["Traces"];

            var query =
                from marker in markers.AsEnumerable()
                join trace in traces.AsEnumerable()
                on marker.Field<int>("TraceID") equals
                    trace.Field<int>("ID")
                where marker.Field<int>("FrameID") == frameID
                && trace.Field<int>("ViewID") == viewID
                select new
                {
                    TraceID = marker.Field<int>("TraceID"),
                    PosX = marker.Field<int>("PosX"),
                    PosY = marker.Field<int>("PosY"),
                    IsShaded = marker.Field<bool>("IsShaded")
                };

            List<Marker> list = new List<Marker>();
            foreach (var elem in query)
            {
                Marker item = new Marker();
                item.TraceID = elem.TraceID;
                item.FrameID = frameID;
                item.PosX = elem.PosX;
                item.PosY = elem.PosY;
                item.IsShaded = elem.IsShaded;
                list.Add(item);
            }
            return list;
        }

        // Метод сохранения разметки в XML-файл
        override public bool Save(string MarkupFilePath)
        {
            try
            {
                m_data.WriteXml(MarkupFilePath);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Метод загрузки разметки из XML-файла
        override public bool Open(string MarkupFilePath)
        {
            try
            {
                // Очищаем все таблицы
                m_data.Clear();
                m_data.Reset();
                m_CreateSchema();
                m_data.ReadXml(MarkupFilePath);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public MarkupProviderADO()
        {
            m_data = new DataSet();
            m_CreateSchema();
        }
    }
}
