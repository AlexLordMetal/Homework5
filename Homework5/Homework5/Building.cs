using System;
using System.Collections.Generic;

namespace Homework5
{
    class Building
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public int Amount { get; set; }
        public List<Livestock> Livestocks { get; set; }

        public Building(string name, int area = 20, int amount = 10)
        {
            Name = name;
            Area = area;
            Amount = amount;
            Livestocks = new List<Livestock>();
        }

        public Building()
        {
            Console.Write("Укажите имя строения: ");
            Name = Console.ReadLine();
            Console.Write($"Укажите площадь строения \"{Name}\": ");
            Area = FarmMathUtilities.ConditionParse();
            Console.Write($"Укажите вместимость строения \"{Name}\" (максимальное количество животных): ");
            Amount = FarmMathUtilities.ConditionParse();
            Livestocks = new List<Livestock>();
        }

        public int OccupiedAmount
        {
            get
            {
                int occupiedAmount = Livestocks.Count;
                return occupiedAmount;
            }
        }

        //Methods

        public void AddLivestock(Livestock livestock)
        {
            if (OccupiedAmount < Amount)
            {
                Livestocks.Add(livestock);
                Console.WriteLine($"Животное \"{livestock.Name}\" заселено в строение \"{Name}\".\n");
            }
            else
            {
                Console.WriteLine($"Животное \"{livestock.Name}\" не заселено, поскольку оно уже не помещается в строение \"{Name}\" (строение уже заполнено максимально - {OccupiedAmount} животных)\n");
            }

        }

        public void RemoveLivestock()
        {
            Console.Write($"Укажите номер животного, которое хотите выселить из строения \"{Name}\" (всего животных - {Livestocks.Count}): ");
            int livestockNumber = FarmMathUtilities.ConditionParse(Livestocks.Count);
            Console.WriteLine($"Животное \"{Livestocks[livestockNumber-1].Name}\" выселено из строения \"{Name}\".\n");
            Livestocks.RemoveAt(livestockNumber - 1);
        }

        public void Report()
        {
            Console.Write($"\"{Name}\" площадью {Area} гектар на {Amount} животных. ");
            if (OccupiedAmount == 0)
            {
                Console.Write("В строении никого нет, ");
            }
            else
            {
                Console.Write("В строении живет ");
                foreach (var livestock in Livestocks)
                {
                    Console.Write($"{livestock.Name}, ");
                }
            }
            Console.WriteLine($"заполнено на {FarmMathUtilities.OccupiedPercent(OccupiedAmount, Amount)}%.");
        }

    }
}
