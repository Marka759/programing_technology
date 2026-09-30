using System.Runtime.CompilerServices;
using System.Transactions;

namespace Restoraunt
/// <summary>
/// Класс для объекта блюд 
/// </summary>
{
    public class Dish
    {   
        public Dish (int id, string name, int chef, int category, decimal price, int weight )
        {
            if (weight<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(weight), "Вес должен быть положительным");
            }
            Id= id; Name = name; ChefId= chef; CategoryId= category; Price = price; Weight = weight;
        }


        public int Id {  get; set; }
        public string Name { get; set; }
        public int ChefId  { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }

        public decimal PricePerGram 
        { get { return Price / Weight; } }

        public bool IsHeavy
        {
            get { return Weight > 500; }
        }

        public string GetInfo ()
        {
            return $"{Name} ({Price} руб., {Weight} г)";
        }

    }
}
