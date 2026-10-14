namespace Furniture
{
    /// <summary>
    /// Класс, представляющий стол. Реализует интерфейс IAssemblable.
    /// </summary>
    public class Table : Furniture, IAssemblable
    {
        /// <summary>
        /// Количество мест за столом.
        /// </summary>
        public int SeatsCount { get; set; }

        /// <summary>
        /// Является ли стол большим.
        /// </summary>
        public bool IsBigTable => SeatsCount > 4;

        /// <summary>
        /// Конструктор стола.
        /// </summary>
        public Table(int id, string name, decimal price, int seatsCount) : base(id, name, price)
        {
            SeatsCount = seatsCount;
        }

        /// <summary>
        /// Реализация метода сборки стола.
        /// </summary>
        public void Assemble()
        {
            System.Console.WriteLine("Table assembled successfully.");
        }

        /// <summary>
        /// Переопределенный метод описания стола.
        /// </summary>
        public override void Describe()
        {
            System.Console.WriteLine($"[Table] {Name}, {Price}, material: {Material}, seats: {SeatsCount}");
        }

        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        public override string ToString()
        {
            return $"[Table] {Name}, {Price}, material: {Material}, seats: {SeatsCount}";
        }
    }
}