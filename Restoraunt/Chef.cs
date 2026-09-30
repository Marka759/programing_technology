
namespace Restoraunt
{
    /// <summary>
    /// Класс для объекта шефов 
    /// </summary>
    public class Chef
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }

        public bool IsChef
        { get { return Specialty == "Шеф-повар"; } }
        
        public string GetInfo()
        {
            return $"{FullName} ({Specialty})";

        }
    }
}
