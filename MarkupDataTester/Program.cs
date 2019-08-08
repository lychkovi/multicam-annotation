// Программа для тестирования классов MarkupProvider

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MarkupData;       
    /* Класс MarkupProvider из библиотеки MarkupData в 
     * этом же решении MyVideoAnno. */

namespace MarkupDataTester
{
    class Program
    {
        /* Метод конструирует структуру RecordingInfo для последующей
         * инициализации объекта markup. */
        static void RecordingInfoInit(out RecordingInfo rec)
        {
            View View;
            View.ID = 0;
            View.FileNameAVI = "unknown.avi";
            View.FrameWidth = 720;
            View.FrameHeight = 576;
            View.IsColour = true;
            View.Comment = "";

            rec.FileNameXML = "unknown.xml";
            rec.FramesCount = 0;
            rec.Fps = 25.0;
            rec.Comment = "";
            rec.DateTime = DateTime.Now;
            rec.Views = new List<View>();
            rec.Views.Add(View);
        }

        /* Вспомогательный метод для вывода в консоль таблицы Categories. */
        static void CategoryPrintAll(MarkupProvider markup)
        {
            List<Category> list;

            list = markup.CategoryGetAll();

            foreach (Category c in list)
            {
                Console.WriteLine("ID = {0}, Name = {1}, Comment = {2}",
                    c.ID, c.Name, c.Comment);
            }
        }


        /* Вспомогательный метод для вывода в консоль категории по номеру. */
        static void CategoryPrintByID(MarkupProvider markup, int id)
        {
            Category category;
            bool found;

            found = markup.CategoryGetByID(id, out category);

            if (found)
            {
                Console.WriteLine("ID = {0}, Name = {1}, Comment = {2}",
                    category.ID, category.Name, category.Comment);
            }
        }

        /* Вспомагательный метод для добавления нового элемента в таблицу
         * Categories. */
        static int CategoryInsertOne(MarkupProvider markup, string Name)
        {
            Category c;

            c.ID = -1;
            c.Name = Name;
            c.Comment = "This is a mock category for testing";
            c.ColorRed = 255;
            c.ColorGreen = 255;
            c.ColorBlue = 255;

            return markup.CategoryCreate(c);
        }

        /* Метод тестирует просмотр категории, заданной номером. */
        static void TestCategorySelectByID(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            CategoryPrintByID(markup, 0);
        }

        /* Метод тестирует просмотр всех категорий в списке. */
        static void TestCategorySelect(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            CategoryPrintAll(markup);
        }

        /* Метод тестирует функцию добавления новой категории. */
        static void TestCategoryInsert(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            int ID = CategoryInsertOne(markup, "New");

            Console.WriteLine("New ID = {0}", ID);
            CategoryPrintAll(markup);
        }

        /* Метод тестирует удаление строки из таблицы категорий. */
        static void TestCategoryDelete(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            CategoryInsertOne(markup, "New1");
            CategoryInsertOne(markup, "New2");
            markup.CategoryDelete(1);

            CategoryPrintAll(markup);
        }

        /* Метод тестирует обновление строки в таблице категорий. */
        static void TestCategoryUpdate(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            CategoryInsertOne(markup, "New1");
            CategoryInsertOne(markup, "New2");

            List<Category> list;
            Category c;
            list = markup.CategoryGetAll();
            c = list[1];
            c.Name = "New3";
            markup.CategoryUpdate(c);

            CategoryPrintAll(markup);
        }

        /* Метод тестирует сохранение разметки в XML-файл. */
        static void TestXmlWrite(RecordingInfo rec)
        {
            string MarkupFilePath = "markup.xml";

            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            CategoryInsertOne(markup, "New1");
            CategoryInsertOne(markup, "New2");
            CategoryPrintAll(markup);
            if (markup.Save(MarkupFilePath))
                Console.WriteLine("Saved markup to " + MarkupFilePath);
            else
                Console.WriteLine("Problem while saving markup!");
        }

        /* Метод тестирует загрузку разметки из XML-файла. */
        static void TestXmlRead()
        {
            string MarkupFilePath = "markup.xml";

            MarkupProvider markup = new MarkupProviderADO();
            if (markup.Open(MarkupFilePath))
            {
                Console.WriteLine("Loaded markup from " + MarkupFilePath);
                CategoryPrintAll(markup);
            }
            else
            {
                Console.WriteLine("Propblem while loading markup!");
            }
        }

        /* Метод тестирует запрос к объединению двух таблиц */
        static void TestTraceBoxJoin(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            Trace trace = new Trace();
            trace.TagID = 0;
            trace.ViewID = 0;
            trace.FrameStart = 0;
            trace.FrameEnd = 0;
            trace.ID = markup.TraceCreate(trace);

            Box box = new Box();
            box.TraceID = trace.ID;
            box.FrameID = trace.FrameStart;
            box.PosX = 10;
            box.PosY = 30;
            box.Width = 15;
            box.Height = 25;
            markup.BoxCreate(box);

            List<Box> boxes = markup.BoxGetByView(0, 0);
            foreach (Box item in boxes)
            {
                Console.WriteLine("TraceID = {0}, FrameID = {1}, PosX = {2}",
                    item.TraceID, item.FrameID, item.PosX);
            }
        }

        /* Метод тестирует запрос к объединению двух таблиц */
        static void TestTraceMarkerJoin(RecordingInfo rec)
        {
            MarkupProvider markup = new MarkupProviderADO();
            markup.Init(rec);
            Trace trace = new Trace();
            trace.TagID = 0;
            trace.ViewID = 0;
            trace.FrameStart = 0;
            trace.FrameEnd = 0;
            trace.ID = markup.TraceCreate(trace);

            Marker marker = new Marker();
            marker.TraceID = trace.ID;
            marker.FrameID = trace.FrameStart;
            marker.PosX = 10;
            marker.PosY = 30;
            markup.MarkerCreate(marker);
            marker.PosX = 20;
            markup.MarkerUpdate(marker);

            List<Marker> markers = markup.MarkerGetByView(0, 0);
            foreach (Marker item in markers)
            {
                Console.WriteLine("TraceID = {0}, FrameID = {1}, PosX = {2}",
                    item.TraceID, item.FrameID, item.PosX);
            }
        }

        static void Main(string[] args)
        {
            RecordingInfo rec;
            RecordingInfoInit(out rec);
            Console.WriteLine("0 - Test Category SelectByID;");
            Console.WriteLine("1 - Test Category SelectAll;");
            Console.WriteLine("2 - Test Category Insert;");
            Console.WriteLine("3 - Test Category Delete;");
            Console.WriteLine("4 - Test Category Update;");
            Console.WriteLine("5 - Test saving markup to Xml;");
            Console.WriteLine("6 - Test loading markup from Xml;");
            Console.WriteLine("7 - Test query to Boxes JOIN Traces;");
            Console.WriteLine("8 - Test query to Markers JOIN Traces;");
            Console.Write("Your choise: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            switch (key.KeyChar)
            {
                case '0':
                    TestCategorySelectByID(rec);
                    break;
                case '1':
                    TestCategorySelect(rec);
                    break;
                case '2':
                    TestCategoryInsert(rec);
                    break;
                case '3':
                    TestCategoryDelete(rec);
                    break;
                case '4':
                    TestCategoryUpdate(rec);
                    break;
                case '5':
                    TestXmlWrite(rec);
                    break;
                case '6':
                    TestXmlRead();
                    break;
                case '7':
                    TestTraceBoxJoin(rec);
                    break;
                case '8':
                    TestTraceMarkerJoin(rec);
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
            Console.ReadKey();
        }
    }
}
