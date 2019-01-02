// Программа для тестирования класса MarkupProvider

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
        static void TestCategorySelect()
        {
            MarkupProvider markup = new MarkupProvider();
            CategoryPrintAll(markup);
        }

        /* Метод тестирует функцию добавления новой категории. */
        static void TestCategoryInsert()
        {
            MarkupProvider markup = new MarkupProvider();
            int ID = CategoryInsertOne(markup, "New");

            Console.WriteLine("New ID = {0}", ID);
            CategoryPrintAll(markup);
        }

        /* Метод тестирует удаление строки из таблицы категорий. */
        static void TestCategoryDelete()
        {
            MarkupProvider markup = new MarkupProvider();
            CategoryInsertOne(markup, "New1");
            CategoryInsertOne(markup, "New2");
            markup.CategoryDelete(1);

            CategoryPrintAll(markup);
        }

        /* Метод тестирует обновление строки в таблице категорий. */
        static void TestCategoryUpdate()
        {
            MarkupProvider markup = new MarkupProvider();
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

        static void Main(string[] args)
        {
            Console.WriteLine("1 - Test Category Select;");
            Console.WriteLine("2 - Test Category Insert;");
            Console.WriteLine("3 - Test Category Delete;");
            Console.WriteLine("4 - Test Category Update;");
            Console.Write("Your choise: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            switch (key.KeyChar)
            {
                case '1':
                    TestCategorySelect();
                    break;
                case '2':
                    TestCategoryInsert();
                    break;
                case '3':
                    TestCategoryDelete();
                    break;
                case '4':
                    TestCategoryUpdate();
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
            Console.ReadKey();
        }
    }
}
