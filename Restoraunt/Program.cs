using Restoraunt;
namespace RestaurantApp
{
    /// <summary>
    /// Главнное меню вызовов классов
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            
            
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 - Данные из памяти (InMemoryRepository)");
            Console.WriteLine("2 - Данные из файлов (CsvRepository)");
            Console.Write("Ваш выбор: ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Неверный выбор. Программа завершена.");
                return;
            }
            
            List<Chef> chefs = null;
            List<Category> categories = null;
            List<Dish> dishes = null;

            try
            {
                switch (choice)
                {
                    case 1:
                    InMemoryRepository repo = new InMemoryRepository();
                    chefs = repo.GetChefs();
                    categories = repo.GetCategories();
                    dishes = repo.GetDishes();
                        break;

                    case 2:
                
                    CsvRepository repo1 = new CsvRepository("data");
                    chefs = repo1.GetChefs();
                    categories = repo1.GetCategories();
                    dishes = repo1.GetDishes();
                        break;
                default:
                
                    Console.WriteLine("Неверный выбор. Программа завершена.");
                    return;
                }
            }

            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            

           
            Chef chefForBorsch = FindChef("Борщ", dishes, chefs);
            string chefResult = (chefForBorsch != null) ? chefForBorsch.GetInfo() : "null";
            Console.WriteLine($"1.FindChef('Борщ'): {chefResult}");

            

           
            
            Category catForBorsch = FindCategory("Борщ", dishes, categories);
            string catResult = (catForBorsch != null) ? catForBorsch.Info() : "null";
            Console.WriteLine($"2.FindCategory('Борщ'): {catResult}");

            

            
            int totalWeight = GetTotalWeight(dishes);
            Console.WriteLine($"3.GetTotalWeight: {totalWeight} г.");

            

            Console.WriteLine("4.GetDishesByChefSortedByPrice");
            List <Dish> sortedDishes = GetDishesByChefSortedByPrice("Петрова А.А.", dishes, chefs);

            if (sortedDishes.Count == 0)
            {
                Console.WriteLine("Блюда не найдены.");
            }
            else
            {
                foreach (Dish d in sortedDishes)
                {
                    Console.WriteLine($"   - {d.Name} ({d.Price} руб.)");
                }
            }

            
            Console.WriteLine("\n5. Полный список блюд:");
            PrintAllDishes(dishes, chefs, categories);

        
            
            Chef unknownChef = FindChef("Неизвестное блюдо", dishes, chefs);
            if (unknownChef == null)
            {
                Console.WriteLine("Поиск 'Неизвестное блюдо' вернул: null");
            }
        }

        
            // <summary>
            /// Находит повара, который приготовил блюдо с указанным названием.
            /// </summary>
            /// <param dishName>Название блюда .</param>
            /// <param dishes>Список всех блюд.</param>
            /// <param chefs>Список всех поваров.</param>
            /// <returns> FindChef — найденный повар;
            /// null, если блюдо или повар не найдены.
            /// </returns>
        static Chef FindChef(string dishName, List<Dish> dishes, List<Chef> chefs)
        {
            if (string.IsNullOrEmpty(dishName))
            {
                Console.WriteLine("Название блюда не указано.");
                return null;
            }

            Dish foundDish = null;
            foreach (Dish d in dishes)
            {
                if (d.Name == dishName)
                {
                    foundDish = d;
                    break; 
                }
            }

            // Если блюдо не найдено - возвращаем null
            if (foundDish == null) return null;

            //  ищем повара по ID
            foreach (Chef c in chefs)
            {
                if (c.Id == foundDish.ChefId)
                {
                    return c;
                }
            }
            return null; 
        }

            /// <summary>
            /// Находит категорию блюда с указанным названием.
            /// </summary>
            /// <param dishName >Название блюда для поиска.</param>
            /// <param  dishes >Список всех блюд.</param>
            /// <param  categories >Список всех категорий.</param>
            /// <returns>
            /// Объект  FindCategory > — найденная категория;
            /// null, если блюдо или категория не найдены.
            /// </returns>/ 
        static Category FindCategory(string dishName, List<Dish> dishes, List<Category> categories)
        {
            if (string.IsNullOrEmpty(dishName))
            {
                Console.WriteLine("Название блюда не указано.");
                return null;
            }
            Dish foundDish = null;
            foreach (Dish d in dishes)
            {
                if (d.Name == dishName)
                {
                    foundDish = d;
                    break;
                }
            }

            if (foundDish == null) return null;

            foreach (Category c in categories)
            {
                if (c.Id == foundDish.CategoryId)
                {
                    return c;
                }
            }
            return null;
        }
            /// <summary>
            /// Вычисляет суммарный вес всех блюд.
            /// </summary>
            /// <param dishes>Список блюд.</param>
            /// <returns>
            /// Общий вес блюд в граммах. 
            /// Если список null или пуст — возвращает 0.
            /// </returns>
       
        static int GetTotalWeight(List<Dish> dishes)
        {
            if (dishes == null || dishes.Count == 0) return 0;

            int total = 0;
            foreach (Dish d in dishes)
            {
                total += d.Weight; 
            }
            return total;
        }
            /// <summary>
            /// Возвращает список блюд указанного повара, отсортированный по цене
            /// по возрастанию. Сортировка выполняется вручную методом «пузырька».
            /// </summary>
            /// <param chefName>Полное имя повара.</param>
            /// <param dishes >Список всех блюд.</param>
            /// <param chefs>Список всех поваров.</param>
            /// <returns>
            /// Список блюд выбранного повара, отсортированный по цене.
            /// Если повар не найден — возвращается пустой список.
            /// </returns>

       
        static List<Dish> GetDishesByChefSortedByPrice(string chefName, List<Dish> dishes, List<Chef> chefs)
        {
            if (string.IsNullOrEmpty(chefName))
            {
                Console.WriteLine("Нет имён шефов.");
                return null;
            }

            int targetChefId = -1;
            foreach (Chef c in chefs)
            {
                if (c.FullName == chefName)
                {
                    targetChefId = c.Id;
                    break;
                }
            }

            List<Dish> result = new List<Dish>();
            if (targetChefId == -1) return result;


            foreach (Dish d in dishes)
            {
                if (d.ChefId == targetChefId)
                {
                    result.Add(d);
                }
            }

 
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (result[j].Price > result[j + 1].Price)
                    {
                        
                        Dish temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Выводит в консоль полный список блюд с указанием имени повара
        /// и названия категории для каждого блюда.
        /// </summary>
        /// <param dishes>Список всех блюд.</param>
        /// <param chefs>Список всех поваров (для поиска имени по ID).</param>
        /// <param categories>Список всех категорий (для поиска названия по ID).</param>
        /// <remarks>
        /// Если список блюд пуст или равен null выводится сообщение
        /// «Список блюд пуст.». Если повар или категория не найдены,
        /// вместо имени выводится символ «—».
        /// </remarks>
        static void PrintAllDishes(List<Dish> dishes, List<Chef> chefs, List<Category> categories)
        {
            if (dishes == null || chefs == null || categories == null)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (Dish d in dishes)
            {
                // Ищем имя повара
                string chefName = "—";
                foreach (Chef c in chefs)
                {
                    if (c.Id == d.ChefId)
                    {

                        chefName = c.FullName;
                        break;
                    }

                }

                // Ищем название категории
                string catName = "—";
                foreach (Category cat in categories)
                {
                    if (cat.Id == d.CategoryId)
                    {
                        catName = cat.Name;
                        break;
                    }
                }

              
                Console.WriteLine($"\"{d.GetInfo()}\" - повар {chefName}, категория \"{catName}\"");
            }
        }
    }
}