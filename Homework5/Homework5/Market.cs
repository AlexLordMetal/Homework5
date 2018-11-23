using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Homework5
{
    public class Market
    {
        public List<Seed> Seeds { get; set; }
        public List<Livestock> Livestocks { get; set; }

        public Market()
        {
            Seeds = new List<Seed>();
            Livestocks = new List<Livestock>();
        }

        #region Methods

        /// <summary>
        /// Writes the list of seeds contains in market to console
        /// </summary>
        public void SeedsToConsole()
        {
            if (Seeds.Count != 0)
            {
                Console.WriteLine("На рынке есть следующие семена:");
                for (int index = 0; index < Seeds.Count; index++)
                {
                    Console.WriteLine($"{index + 1}. {Seeds[index].ThisPlant.Name}\tСтоимость: {Seeds[index].Cost}\t(сезон посадки: {FarmMathUtilities.SeasonsToRusString(Seeds[index].PlantingSeason)}, площадь посадки: {Seeds[index].ThisPlant.Area})");
                }
            }
            else Console.WriteLine($"На рынке нет семян на продажу.");
        }

        /// <summary>
        /// Writes the list of livestocks contains in market to console
        /// </summary>
        public void LivestocksToConsole()
        {
            if (Livestocks.Count != 0)
            {
                Console.WriteLine("На рынке есть следующие животные:");
                for (int index = 0; index < Livestocks.Count; index++)
                {
                    Console.WriteLine($"{index + 1}. {Livestocks[index].Name}\t(продукция: {Livestocks[index].Production.Name}, сезонов между сбором продукции: {Livestocks[index].TimeBetweenHarvests})");
                }
            }
            else Console.WriteLine($"На рынке нет животных на продажу.");
        }

        /// <summary>
        /// Adds new list of seeds to Json file
        /// </summary>
        public void SeedsToJson()
        {
            Seed seed = new Seed();
            seed.AddFromConsole();
            if (IndexOfSeed(seed) == -1)
            {
                Seeds.Add(seed);
                using (StreamWriter writer = new StreamWriter("Seeds.json"))
                {
                    writer.Write(JsonConvert.SerializeObject(Seeds));
                }
                Console.WriteLine($"Семена растения \"{seed.ThisPlant.Name}\" добавлены в библиотеку.");
            }
            else Console.WriteLine($"Семена растения \"{seed.ThisPlant.Name}\" не добавлены, потому что семена с таким названием растения уже есть в библиотеке.");
        }

        /// <summary>
        /// Adds new list of livestocks to Json file
        /// </summary>
        public void LivestocksToJson()
        {
            Livestock livestock = new Livestock();
            livestock.AddFromConsole();
            if (IndexOfLivestock(livestock) == -1)
            {
                Livestocks.Add(livestock);
                using (StreamWriter writer = new StreamWriter("Livestocks.json"))
                {
                    writer.Write(JsonConvert.SerializeObject(Livestocks));
                }
                Console.WriteLine($"Животное \"{livestock.Name}\" добавлено в библиотеку.");
            }
            else Console.WriteLine($"Животное \"{livestock.Name}\" не добавлено, потому что животное с таким названием уже есть в библиотеке.");
        }

        /// <summary>
        /// Finds index of seed in list of seeds, if it's name is equal to the name of new seed, else returns -1
        /// </summary>
        /// <param name="newSeed"></param>
        /// <returns></returns>
        private int IndexOfSeed(Seed newSeed)
        {
            int seedIndex = -1;
            for (var seedCount = 0; seedCount < Seeds.Count; seedCount++)
            {
                if (Seeds[seedCount].ThisPlant.Name == newSeed.ThisPlant.Name)
                {
                    seedIndex = seedCount;
                    break;
                }
            }
            return seedIndex;
        }

        /// <summary>
        /// Finds index of livestock in list of livestocks, if it's name is equal to the name of new livestock, else returns -1
        /// </summary>
        /// <param name="newLivestock"></param>
        /// <returns></returns>
        private int IndexOfLivestock(Livestock newLivestock)
        {
            int livestockIndex = -1;
            for (var livestockCount = 0; livestockCount < Livestocks.Count; livestockCount++)
            {
                if (Livestocks[livestockCount].Name == newLivestock.Name)
                {
                    livestockIndex = livestockCount;
                    break;
                }
            }
            return livestockIndex;
        }

        #endregion Methods

    }
}
