namespace Furniture
{
    /// <summary>
    /// Класс, представляющий стул.
    /// </summary>
    public class Chair : Furniture
    {
        /// <summary>
        /// Количество ножек у стула.
        /// </summary>
        public int LegsCount { get; set; }

        /// <summary>
        /// Является ли стул табуретом.
        /// </summary>
        public bool IsStool => LegsCount == 3;

        /// <summary>
        /// Конструктор стула.
        /// </summary>
        public Chair(int id, string name, decimal price, int legsCount) : base(id, name, price)
        {
            LegsCount = legsCount;
        }

        /// <summary>
        /// Переопределенный метод описания стула.
        /// </summary>
        public override void Describe()
        {
            System.Console.WriteLine($"[Chair] {Name}, {Price}, material: {Material}, legs: {LegsCount}");
        }

        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        public override string ToString()
        {
            return $"[Chair] {Name}, {Price}, material: {Material}, legs: {LegsCount}";
        }
    }
}