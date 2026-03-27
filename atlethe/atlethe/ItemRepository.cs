using System.Collections.Generic;
using System.Linq;

namespace AthleteCatalog
{
    class ItemRepository
    {
        public List<Item> Items { get; private set; } = new List<Item>();

        public void Add(Item item) => Items.Add(item);

        public int GetNextId() => Items.Count == 0 ? 1 : Items.Max(i => i.Id) + 1;

        public List<Item> GetAllSortedByYear()
        {
            return Items.OrderBy(i => i.Year).ToList();
        }

        public void ReplaceAll(List<Item> items)
        {
            Items = items;
        }
    }
}
