using Restoraunt;

public class CsvRepository
{
    private string _basePath;
    public CsvRepository(string basePath) { _basePath = basePath; }

    public List<Chef> GetChefs()
    {
        List<Chef> result = new List<Chef>();
        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "chefs.csv"));
        if (lines.Length < 2) return result;
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length < 3) continue;
            Chef ch = new Chef();
            ch.Id = int.Parse(parts[0]);
            ch.FullName = parts[1];
            ch.Specialty = parts[2];
            
            result.Add(ch);
        }
        return result;
    }

    public List<Category> GetCategories()
    {
        List<Category> result = new List<Category>();
        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "categories.csv"));
        if (lines.Length < 2) return result;
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length < 3) continue;
            Category c = new Category();
            c.Id = int.Parse(parts[0]);
            c.Name = parts[1];
            c.Type = parts[2];

            result.Add(c);
        }
        return result;
    }

    public List<Dish> GetDishes()
    {
        List<Dish> result = new List<Dish>();
        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "dishes.csv"));
        if (lines.Length < 2) return result;
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length < 6) continue;
            
            int Id = int.Parse(parts[0]);
            string Name = parts[1];
            int ChefId = int.Parse(parts[2]);
            int CategoryId = int.Parse(parts[3]);
            decimal Price = decimal.Parse(parts[4]);
            int Weight = int.Parse(parts[5]);

            Dish d = new Dish( Id,  Name,  ChefId,  CategoryId, Price,  Weight);
            result.Add(d);
        }
        return result;
    }
}