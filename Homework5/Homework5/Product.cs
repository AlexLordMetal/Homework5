using System;

namespace Homework5
{
    public class Product
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }

        public Product() { }

        public Product(string name, int weight, int cost)
        {
            Name = name;
            Weight = weight;
            Cost = cost;
        }

        #region Methods

        /// <summary>
        /// Asks for the properties of the new product and creates this product
        /// </summary>
        public void AddFromConsole()
        {
            Console.Write("Укажите название продукта: ");
            Name = Console.ReadLine();
            Console.Write($"Укажите количество продукта \"{Name}\" (в центнерах): ");
            Weight = FarmMathUtilities.ConditionParse();
            Console.Write($"Укажите стоимость продукта \"{Name}\" за центнер: ");
            Cost = FarmMathUtilities.ConditionParse();
        }

        #endregion Methods

    }
}
