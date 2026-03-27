namespace AthleteCatalog
{
    class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // sportág
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; } // születési év
        public bool IsFavorite { get; set; }

        public Item(int id, string name, string category, string description, int rating, int year, bool fav)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            Rating = rating;
            Year = year;
            IsFavorite = fav;
        }

        public string ToConsoleString()
        {
            return $"{Id}. {Name} ({Category}) - {Year}, Rating: {Rating}, Fav: {IsFavorite}";
        }
    }
}
