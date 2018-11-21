using System;

namespace Homework5
{
    public class Product
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }
        public Product() { }

        public Product(string name, int weight = 1)
        {
            Name = name;
            Weight = weight;
        }

        #region Methods

        public void AddFromConsole()
        {
            Console.Write("Укажите название продукта: ");
            Name = Console.ReadLine();
            Console.Write($"Укажите количество продукта \"{Name}\" (в килограммах): ");
            Weight = FarmMathUtilities.ConditionParse();
            Console.Write($"Укажите стоимость продукта \"{Name}\" за килограмм: ");
            Cost = FarmMathUtilities.ConditionParse();
        }

        #endregion Methods

    }
}
