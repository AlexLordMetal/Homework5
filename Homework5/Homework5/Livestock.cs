using System;

namespace Homework5
{
    public class Livestock
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public Product Production { get; set; }
        public int TimeBetweenHarvests { get; set; }
        public bool IsMultiHarvest { get; set; }
        
        public Livestock()
        {
            Production = new Product();
        }

        public Livestock(string name, int cost, Product production, int timeBetweenHarvests, bool isMultiHarvest = true)
        {
            Name = name;
            Cost = cost;
            Production = production;
            TimeBetweenHarvests = timeBetweenHarvests;
            IsMultiHarvest = isMultiHarvest;
        }

        #region Methods

        /// <summary>
        /// Asks for the properties of the new livestock and creates this livestock
        /// </summary>
        public void AddFromConsole()
        {
            Console.Write("Укажите название животного: ");
            Name = Console.ReadLine();
            Console.Write($"Укажите стоимость животного \"{Name}\": ");
            Cost = FarmMathUtilities.ConditionParse();
            Console.WriteLine("Укажите характеристики получаемого животноводческого продукта: ");
            Production.AddFromConsole();
            Console.Write($"Укажите количество сезонов между получением продукта \"{Production.Name}\": ");
            TimeBetweenHarvests = FarmMathUtilities.ConditionParse();
            Console.WriteLine($"Укажите, исчезает ли животное \"{Name}\" при получении продукта, или живет дальше:\n1 - Исчезает,\n2 - Нет");
            IsMultiHarvest = true;
            if (FarmMathUtilities.ConditionParse(2) == 1)
            {
                IsMultiHarvest = false;
            }
        }

        #endregion Methods

    }
}
