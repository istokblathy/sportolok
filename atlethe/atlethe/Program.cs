using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AthleteCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ItemRepository repo = new ItemRepository();

            string templateFolder = "template";
            string outputFolder = Path.Combine("outputs", "mappa1");
            Directory.CreateDirectory(outputFolder);

            bool running = true;

            while (running)
            {
                ShowMenu();
                Console.Write("Választás: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddItem(repo); break;
                    case "2": ListItems(repo); break;
                    case "3": SearchByName(repo); break;
                    case "4": FilterByCategory(repo); break;
                    case "5": SaveCsv(repo); break;
                    case "6": LoadCsv(repo); break;
                    case "7":
                        HtmlExporter.ExportAll(repo, templateFolder, outputFolder);
                        Console.WriteLine("HTML export elkészült.");
                        break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Ismeretlen menüpont."); break;
                }

                if (running)
                {
                    Console.WriteLine("\nENTER a folytatáshoz...");
                    Console.ReadLine();
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Athlete Catalog ===");
            Console.WriteLine("1 - Sportoló hozzáadása");
            Console.WriteLine("2 - Lista megjelenítése");
            Console.WriteLine("3 - Keresés név alapján");
            Console.WriteLine("4 - Szűrés sportág szerint");
            Console.WriteLine("5 - CSV Mentés");
            Console.WriteLine("6 - CSV Betöltés");
            Console.WriteLine("7 - HTML export");
            Console.WriteLine("0 - Kilépés");
        }

        static void AddItem(ItemRepository repo)
        {
            Console.Write("Név: ");
            string name = Console.ReadLine();

            Console.Write("Sportág (Category): ");
            string category = Console.ReadLine();

            Console.Write("Leírás: ");
            string description = Console.ReadLine();

            int rating = ReadInt("Értékelés (1-10): ");
            int year = ReadInt("Születési év: ");

            bool isFav = ReadBool("Kedvenc? (i/n): ");

            Item item = new Item(repo.GetNextId(), name, category, description, rating, year, isFav);
            repo.Add(item);

            Console.WriteLine("Sportoló hozzáadva.");
        }

        static int ReadInt(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                if (int.TryParse(Console.ReadLine(), out int v))
                    return v;

                Console.WriteLine("Érvénytelen szám.");
            }
        }

        static bool ReadBool(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string i = Console.ReadLine().ToLower();

                if (i == "i" || i == "y") return true;
                if (i == "n") return false;

                Console.WriteLine("i vagy n.");
            }
        }

        static void ListItems(ItemRepository repo)
        {
            Console.WriteLine("=== Sportolók listája ===");

            var list = repo.GetAllSortedByYear();

            foreach (var item in list)
                Console.WriteLine(item.ToConsoleString());
        }

        static void SearchByName(ItemRepository repo)
        {
            Console.Write("Keresett név: ");
            string term = Console.ReadLine().ToLower();

            var results = repo.Items
                .Where(i => i.Name.ToLower().Contains(term))
                .ToList();

            foreach (var item in results)
                Console.WriteLine(item.ToConsoleString());
        }

        static void FilterByCategory(ItemRepository repo)
        {
            Console.Write("Sportág: ");
            string cat = Console.ReadLine().ToLower();

            var results = repo.Items
                .Where(i => i.Category.ToLower() == cat)
                .OrderByDescending(i => i.Rating)
                .ToList();

            foreach (var item in results)
                Console.WriteLine(item.ToConsoleString());
        }

        static void SaveCsv(ItemRepository repo)
        {
            Console.Write("Fájlnév: ");
            string file = Console.ReadLine();

            CsvStorage.Save(file, repo.Items);
            Console.WriteLine("Mentve.");
        }

        static void LoadCsv(ItemRepository repo)
        {
            Console.Write("Fájlnév: ");
            string file = Console.ReadLine();

            if (!File.Exists(file))
            {
                Console.WriteLine("Nincs ilyen fájl.");
                return;
            }

            var items = CsvStorage.Load(file);
            repo.ReplaceAll(items);

            Console.WriteLine("Betöltve.");
        }
    }
}
