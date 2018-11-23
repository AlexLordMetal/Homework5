using System;
using System.Collections.Generic;

namespace Homework5
{
    public class GardenBed
    {
        public int Area { get; set; }
        public List<Plant> Plants { get; set; }
        public int OccupiedArea
        {
            get
            {
                int occupiedArea = 0;
                for (int i = 0; i < Plants.Count; i++)
                {
                    occupiedArea += Plants[i].Area;
                }
                return occupiedArea;
            }
        }

        public GardenBed()
        {
            Plants = new List<Plant>();
        }

        public GardenBed(int area)
        {
            Area = area;
            Plants = new List<Plant>();
        }

        #region Methods

        /// <summary>
        /// Asks for the properties of the new gardenbed and creates this gardenbed
        /// </summary>
        public void AddFromConsole()
        {
            Console.Write("Укажите площадь грядки (кв.м.): ");
            Area = FarmMathUtilities.ConditionParse();
        }

        /// <summary>
        /// Adds plant with overflow conditions
        /// </summary>
        /// <param name="plant"></param>
        public void AddPlant(Plant plant)
        {
            if ((OccupiedArea + plant.Area) <= Area)
            {
                Plants.Add(plant);
                Console.WriteLine($"Растение \"{plant.Name}\" посажено на грядку.");
            }
            else
            {
                Console.WriteLine($"Растение \"{plant.Name}\" не посажено, поскольку оно уже не помещается на грядке (превышение максимального размера грядки на {OccupiedArea + plant.Area - Area} кв.м.)\n");
            }
        }

        /// <summary>
        /// Asks for the plant number and remove this plant
        /// </summary>
        public void RemovePlant()
        {
            if (Plants.Count > 0)
            {
                Console.Write($"Укажите номер растения, которое хотите выкопать с грядки, данное растение будет уничтожено (всего растений - {Plants.Count}): ");
                int plantNumber = FarmMathUtilities.ConditionParse(Plants.Count);
                Console.WriteLine($"Растение \"{Plants[plantNumber - 1].Name}\" выкопано с грядки.\n");
                Plants.RemoveAt(plantNumber - 1);
            }
            else Console.WriteLine($"Действие невозможно, поскольку на грядке ничего не растет\n");
        }

        /// <summary>
        /// Writes report of gardenbed to console
        /// </summary>
        public void Report()
        {
            Console.Write($"Площадь грядки - {Area} кв.м. ");
            if (OccupiedArea == 0)
            {
                Console.Write("На грядке ничего не растет, ");
            }
            else
            {
                Console.Write($"На грядке растет ");
                foreach (var plant in Plants)
                {
                    Console.Write($"{plant.Name}, ");
                }
            }
            Console.WriteLine($"заполнено {FarmMathUtilities.OccupiedPercent(OccupiedArea, Area)}% всей площади грядки.");
        }

        /// <summary>
        /// Writes report of gardenbed's plants to console
        /// </summary>
        public void ReportOnlyPlants()
        {
            if (OccupiedArea == 0)
            {
                Console.WriteLine("На грядке ничего не растет.");
            }
            else
            {
                Console.WriteLine($"На грядке растет:");
                for (int index = 0; index < Plants.Count; index++)
                {
                    Console.WriteLine($"{index + 1}. {Plants[index].Name}\t(Сезон сбора: {FarmMathUtilities.SeasonsToRusString(Plants[index].HarvestSeason)})");
                }
            }
        }

        #endregion

    }
}
