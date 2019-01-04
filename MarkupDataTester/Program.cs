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

            list = markup.CategorySelectAll();

            foreach (Category c in list)
            {
                Console.WriteLine("ID = {0}, Name = {1}, Comment = {2}",
                    c.ID, c.Name, c.Comment);
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

            return markup.CategoryInsert(c);
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
            list = markup.CategorySelectAll();
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

        static void Main(string[] args)
        {
            RecordingInfo rec;
            RecordingInfoInit(out rec);

            Console.WriteLine("1 - Test Category Select;");
            Console.WriteLine("2 - Test Category Insert;");
            Console.WriteLine("3 - Test Category Delete;");
            Console.WriteLine("4 - Test Category Update;");
            Console.WriteLine("5 - Test saving markup to Xml;");
            Console.WriteLine("6 - Test loading markup from Xml;");
            Console.Write("Your choise: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            switch (key.KeyChar)
            {
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
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
            Console.ReadKey();
        }
    }
}
