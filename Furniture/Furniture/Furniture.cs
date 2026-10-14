namespace Furniture
{
    /// <summary>
    /// Абстрактный базовый класс, представляющий мебель.
    /// </summary>
    public abstract class Furniture
    {
        private int _id;

        /// <summary>
        /// Название мебели.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена мебели.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Материал изготовления.Wood - значение по умолчанию.
        /// </summary>
        public Material Material { get; set; } = Material.Wood;

        /// <summary>
        /// Вычисляемая стоимость с учетом НДС.
        /// </summary>
        public decimal PriceWithVat => Price * 1.2m;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Furniture(string name) : this(0, name, 0m)
        {
        }

        /// <summary>
        /// Конструктор для инициализации свойств.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название.</param>
        /// <param name="price">Цена.</param>
        public Furniture(int id, string name, decimal price)
        {
            _id = id;
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Абстрактный метод для описания предмета мебели.
        /// </summary>
        public abstract void Describe();
    }
}