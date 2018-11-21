using System;

namespace Homework5
{
    class Plant
    {
        public string Name { get; set; }
        public Seasons PlantingSeason { get; set; }
        public Seasons HarvestSeason { get; set; }
        public int Area { get; set; }

        public Plant(string name, Seasons plantingSeason, Seasons harvestSeason, int area = 2)
        {
            Name = name;
            PlantingSeason = plantingSeason;
            HarvestSeason = harvestSeason;
            Area = area;
        }

        public Plant()
        {
            Console.Write("Укажите название растения: ");
            Name = Console.ReadLine();
            Console.WriteLine($"Укажите сезон посадки растения \"{Name}\":\n1 - Зима,\n2 - Весна,\n3 - Лето,\n4 - Осень");
            PlantingSeason = (Seasons)FarmMathUtilities.ConditionParse(4);
            Console.WriteLine($"Укажите сезон сбора растения \"{Name}\":\n1 - Зима,\n2 - Весна,\n3 - Лето,\n4 - Осень");
            HarvestSeason = (Seasons)FarmMathUtilities.ConditionParse(4);
            Console.Write($"Укажите площадь посадки растения \"{Name}\": ");
            Area = FarmMathUtilities.ConditionParse();
        }
    }
}