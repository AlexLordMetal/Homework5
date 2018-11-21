using System;

namespace Homework5
{
    class Livestock
    {
        public string Name { get; set; }
        public Product Production { get; set; }

        public Livestock(string name = "Default")
        {
            Name = name;
        }

        public Livestock()
        {
            Console.Write("Укажите название животного: ");
            Name = Console.ReadLine();
            Console.Write("Укажите название получаемого животноводческого продукта: ");
            string productionName = Console.ReadLine();
            Console.Write($"Укажите количество получаемого животноводческого продукта \"{Name}\" (в килограммах): ");
            int productionWeight = FarmMathUtilities.ConditionParse();
            Production = new Product(productionName, productionWeight);
        }
    }
}
