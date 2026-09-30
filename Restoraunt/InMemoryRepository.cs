
namespace Restoraunt
{
    public class InMemoryRepository
    {
        private List<Chef> _chefs;
        private List<Category> _categories;
        private List<Dish> _dishes;

        public InMemoryRepository()
        {
            // Инициализация поваров 
            _chefs = new List<Chef>
            {
                new Chef { Id = 1, FullName = "Петрова А.А.", Specialty = "Шеф-повар" },
                new Chef { Id = 2, FullName = "Иванов И.И.", Specialty = "Су-шеф" },
                new Chef { Id = 3, FullName = "Сидоров С.С.", Specialty = "Повар холодного цеха" },
                new Chef { Id = 4, FullName = "Кузнецова Е.Е.", Specialty = "Кондитер" },
                new Chef { Id = 5, FullName = "Смирнов А.А.", Specialty = "Повар горячего цеха" }
            };

            // Инициализация категорий
            _categories = new List<Category>
            {
                new Category { Id = 1, Name = "Супы", Type = "горячие блюда" },
                new Category { Id = 2, Name = "Салаты", Type = "холодные закуски" },
                new Category { Id = 3, Name = "Десерты", Type = "сладкие блюда" },
                new Category { Id = 4, Name = "Напитки", Type = "прохладительные" },
                new Category { Id = 5, Name = "Гарниры", Type = "дополнительно" }
            };

            // Инициализация блюд (согласованы внешние ключи)
            _dishes = new List<Dish>
            {
                new Dish(1, "Борщ", 1, 1, 350, 400),
                new Dish(2, "Окрошка", 1, 1, 250, 300),
                new Dish(3, "Солянка", 1, 1, 400, 350),
                new Dish(4, "Цезарь", 3, 2, 450, 250),
                new Dish(5, "Тирамису", 4, 3, 300, 150),
                new Dish(6, "Стейк Рибай", 5, 1, 1500, 600)
            };
        }

        public List<Chef> GetChefs() { return _chefs; }
        public List<Category> GetCategories() { return _categories; }
        public List<Dish> GetDishes() { return _dishes; }
    
    }
}

