using System.Collections.Generic;
using System.IO;

namespace AthleteCatalog
{
    static class CsvStorage
    {
        public static void Save(string file, List<Item> items)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                foreach (var i in items)
                {
                    sw.WriteLine($"{i.Id};{i.Name};{i.Category};{i.Description};{i.Rating};{i.Year};{i.IsFavorite}");
                }
            }
        }

        public static List<Item> Load(string file)
        {
            List<Item> list = new List<Item>();

            foreach (var line in File.ReadAllLines(file))
            {
                var p = line.Split(';');
                list.Add(new Item(
                    int.Parse(p[0]),
                    p[1],
                    p[2],
                    p[3],
                    int.Parse(p[4]),
                    int.Parse(p[5]),
                    bool.Parse(p[6])
                ));
            }

            return list;
        }
    }
}
