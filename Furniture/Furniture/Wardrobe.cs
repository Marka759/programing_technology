namespace Furniture
{
    /// <summary>
    /// Класс, представляющий шкаф.
    /// </summary>
    public class Wardrobe : Furniture
    {
        /// <summary>
        /// Количество полок в шкафу.
        /// </summary>
        public int ShelvesCount { get; set; }

        /// <summary>
        /// Является ли шкаф вместительным.
        /// </summary>
        public bool IsSpacious => ShelvesCount > 3;

        /// <summary>
        /// Конструктор шкафа.
        /// </summary>
        public Wardrobe(int id, string name, decimal price, int shelvesCount) : base(id, name, price)
        {
            ShelvesCount = shelvesCount;
        }

        /// <summary>
        /// Переопределенный метод описания шкафа.
        /// </summary>
        public override void Describe()
        {
            System.Console.WriteLine($"[Wardrobe] {Name}, {Price}, material: {Material}, shelves: {ShelvesCount}");
        }

        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        public override string ToString()
        {
            return $"[Wardrobe] {Name}, {Price}, material: {Material}, shelves: {ShelvesCount}";
        }
    }
}