using System;
using System.Collections.Generic;

namespace Homework5
{
    public class Building
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public List<Livestock> Livestocks { get; set; }
        public int Area
        {
            get
            {
                return Amount * 5;
            }
        }
        public int OccupiedAmount
        {
            get
            {
                int occupiedAmount = Livestocks.Count;
                return occupiedAmount;
            }
        }

        public Building()
        {
            Livestocks = new List<Livestock>();
        }

        public Building(string name, int amount)
        {
            Name = name;
            Amount = amount;
            Livestocks = new List<Livestock>();
        }

        #region Methods

        /// <summary>
        /// Asks for the properties of the new building and creates this building
        /// </summary>
        public void AddFromConsole()
        {
            Console.Write("Укажите имя строения: ");
            Name = Console.ReadLine();
        }
        //public void AddFromConsole()
        //{
        //    Console.Write("Укажите имя строения: ");
        //    Name = Console.ReadLine();
        //    Console.Write($"Укажите площадь строения \"{Name}\" (кв.м.): ");
        //    Area = FarmMathUtilities.ConditionParse();
        //    Console.Write($"Укажите вместимость строения \"{Name}\" (максимальное количество животных): ");
        //    Amount = FarmMathUtilities.ConditionParse();
        //}

        /// <summary>
        /// Adds livestock with overflow conditions
        /// </summary>
        /// <param name="livestock"></param>
        public void AddLivestock(Livestock livestock)
        {
            if (OccupiedAmount < Amount)
            {
                Livestocks.Add(livestock);
                Console.WriteLine($"Животное \"{livestock.Name}\" заселено в строение \"{Name}\".\n");
            }
            else
            {
                Console.WriteLine($"Животное \"{livestock.Name}\" не заселено, поскольку оно уже не помещается в строении \"{Name}\" (строение уже заполнено максимально - {OccupiedAmount} животных)\n");
            }

        }

        /// <summary>
        /// Asks for the livestock number and remove this livestock
        /// </summary>
        public void RemoveLivestock()
        {
            if (Livestocks.Count > 0)
            {
                Console.Write($"Укажите номер животного, которое хотите выселить из строения \"{Name}\" (всего животных - {Livestocks.Count}): ");
            int livestockNumber = FarmMathUtilities.ConditionParse(Livestocks.Count);
            Console.WriteLine($"Животное \"{Livestocks[livestockNumber - 1].Name}\" выселено из строения \"{Name}\".\n");
            Livestocks.RemoveAt(livestockNumber - 1);
            }
            else Console.WriteLine($"Действие невозможно, поскольку в строении никого нет\n");
        }

        /// <summary>
        /// Writes report of livestock to console
        /// </summary>
        public void Report()
        {
            Console.Write($"\"{Name}\" площадью {Area} кв.м. на {Amount} животных. ");
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

        #endregion

    }
}
