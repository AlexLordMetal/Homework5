using System;

namespace Homework5
{
    public class Seed
    {
        public Plant ThisPlant { get; set; }
        public Seasons PlantingSeason { get; set; }
        public int Cost { get; set; }

        public Seed()
        {
            ThisPlant = new Plant();
        }

        public Seed(Plant thisPlant, Seasons plantingSeason, int cost)
        {
            ThisPlant = thisPlant;
            PlantingSeason = plantingSeason;
            Cost = cost;
        }

        #region Methods

        /// <summary>
        /// Asks for the properties of the new seed and creates this seed
        /// </summary>
        public void AddFromConsole()
        {
            Console.WriteLine("Укажите характеристики растения, от которого семена: ");
            ThisPlant.AddFromConsole();
            Console.WriteLine($"Укажите сезон ПОСАДКИ семян растения \"{ThisPlant.Name}\":\n1 - Зима\n2 - Весна\n3 - Лето\n4 - Осень");
            PlantingSeason = (Seasons)FarmMathUtilities.ConditionParse(4);
            Console.Write($"Укажите стоимость семян растения \"{ThisPlant.Name}\": ");
            Cost = FarmMathUtilities.ConditionParse();
        }

        #endregion Methods

    }
}
