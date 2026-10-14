namespace Furniture
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Furniture> furnitureList = new List<Furniture>
            {
                new Chair(1, "Stool", 1500m, 3) { Material = Material.Wood },
                new Table(2, "Dining", 8000m, 6) { Material = Material.Wood },
                new Wardrobe(3, "Closet", 12000m, 4) { Material = Material.Metal }
            };

            foreach (var item in furnitureList)
            {
                item.Describe();

                if (item is Chair chair)
                {
                    Console.WriteLine($"Chair is a stool.");
                    Console.WriteLine("Chair cannot be assembled.");
                }
                else if (item is Table table)
                {
                    Console.WriteLine($"Table seats {table.SeatsCount} people.");
                    if (table is IAssemblable assemblable)
                    {
                        assemblable.Assemble();
                    }
                }
                else if (item is Wardrobe wardrobe)
                {   if (wardrobe.IsSpacious)
                        Console.WriteLine("Wardrobe is spacious.");
                    else
                        Console.WriteLine("Wardrobe is not spacious.");
                }

                Console.WriteLine("-------------------------");
            }
        }
    }
}