
namespace Restoraunt

    
{
    /// <summary>
    /// Класс для объекта категорий 
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }


        public string Info()
        {
            return $"{Name} - {Type}";
        }
    }
}
 