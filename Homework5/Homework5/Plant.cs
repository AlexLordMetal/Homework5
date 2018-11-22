using System;

namespace Homework5
{
    public class Plant
    {
        public string Name { get; set; }
        public Seasons HarvestSeason { get; set; }
        public int Area { get; set; }
        public int Cost { get; set; }
        public bool IsMultiHarvest { get; set; }

        public Plant() { }

        public Plant(string name, Seasons harvestSeason, int area, int cost, bool isMultiHarvest = true)
        {
            Name = name;
            HarvestSeason = harvestSeason;
            Area = area;
            Cost = cost;
            IsMultiHarvest = isMultiHarvest;
        }

        #region Methods

        /// <summary>
        /// Asks for the properties of the new plant and creates this plant
        /// </summary>
        /// <returns>Plant</returns>
        public void AddFromConsole()
        {
            Console.Write("Укажите название растения: ");
            Name = Console.ReadLine();
            Console.WriteLine($"Укажите сезон сбора растения \"{Name}\":\n1 - Зима,\n2 - Весна,\n3 - Лето,\n4 - Осень");
            HarvestSeason = (Seasons)FarmMathUtilities.ConditionParse(4);
            Console.Write($"Укажите площадь посадки растения \"{Name}\" (кв.м.): ");
            Area = FarmMathUtilities.ConditionParse();
            Console.Write($"Укажите стоимость выращенного растения \"{Name}\": ");
            Cost = FarmMathUtilities.ConditionParse();
            Console.WriteLine($"Укажите, растение \"{Name}\" выкапывается при сборе или растет дальше:\n1 - Выкапывается,\n2 - Нет");
            IsMultiHarvest = true;
            if (FarmMathUtilities.ConditionParse(2) == 1)
            {
                IsMultiHarvest = false;
            }
        }

        #endregion Methods

    }
}