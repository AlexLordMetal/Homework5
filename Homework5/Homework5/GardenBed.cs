using System;
using System.Collections.Generic;

namespace Homework5
{
    class GardenBed
    {
        public int Area { get; set; }
        public List<Plant> Plants { get; set; }

        public GardenBed(int area)
        {
            Area = area;
            Plants = new List<Plant>();
        }

        public GardenBed()
        {
            Console.Write("Укажите площадь грядки: ");
            Area = FarmMathUtilities.ConditionParse();
            Plants = new List<Plant>();
        }

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


        //Methods

        public void AddPlant(Plant plant)
        {
            if ((OccupiedArea + plant.Area) <= Area)
            {
                Plants.Add(plant);
                Console.WriteLine($"Растение \"{plant.Name}\" посажено на грядку."); 
            }
            else
            {
                Console.WriteLine($"Растение \"{plant.Name}\" не посажено, поскольку оно уже не помещается на грядке (превышение максимального размера грядки на {OccupiedArea + plant.Area - Area} гектар)\n");
            }
        }

        public void RemovePlant()
        {
            Console.Write($"Укажите номер растения, которое хотите выкопать с грядки (всего растений - {Plants.Count}): ");
            int plantNumber = FarmMathUtilities.ConditionParse(Plants.Count);
            Console.WriteLine($"Животное \"{Plants[plantNumber - 1].Name}\" выкопано с грядки.\n");
            Plants.RemoveAt(plantNumber - 1);
        }

        public void Report()
        {
            Console.Write($"Площадь грядки - {Area} гектар. ");
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

    }
}
