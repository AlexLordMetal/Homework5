using System;
using System.Collections.Generic;

namespace Homework5
{
    public class PrimaryWarehowse
    {
        public List<Seed> Seeds { get; set; }
        public List<Livestock> Livestocks { get; set; }

        public PrimaryWarehowse()
        {
            List<Seed> Seeds = new List<Seed>();
            List<Livestock> Livestocks = new List<Livestock>();
        }

        #region Methods

        /// <summary>
        /// Writes the list of seeds contains in PrimaryWarehowse to console
        /// </summary>
        public void SeedsToConsole()
        {
            if (Seeds.Count != 0)
            {
                Console.WriteLine("На складе сырья есть следующие семена:");
                foreach (var seed in Seeds)
                {
                    Console.WriteLine($"{seed.ThisPlant.Name}\t(сезон посадки: {seed.PlantingSeason}, площадь посадки: {seed.ThisPlant.Area}");
                }
            }
            else Console.WriteLine($"На складе сырья нет семян для посадки.");
        }

        /// <summary>
        /// Writes the list of livestocks contains in PrimaryWarehowse to console
        /// </summary>
        public void LivestocksToConsole()
        {
            if (Livestocks.Count != 0)
            {
                Console.WriteLine("На складе сырья есть следующие животные:");
                foreach (var livestock in Livestocks)
                {
                    Console.WriteLine($"{livestock.Name}\t(продукция: {livestock.Production.Name}, сезонов между сбором продукции: {livestock.TimeBetweenHarvests}");
                }
            }
            else Console.WriteLine($"На складе сырья нет животных для заселения.");
        }

        #endregion Methods

    }
}