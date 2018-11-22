using System;
using System.Collections.Generic;

namespace Homework5
{
    public class Market
    {
        public List<Seed> Seeds { get; set; }
        public List<Livestock> Livestocks { get; set; }

        public Market()
        {
            List<Seed> Seeds = new List<Seed>();
            List<Livestock> Livestocks = new List<Livestock>();
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
                foreach (var seed in Seeds)
                {
                    Console.WriteLine($"{seed.ThisPlant.Name}\tСтоимость: {seed.Cost}\t(сезон посадки: {seed.PlantingSeason}, площадь посадки: {seed.ThisPlant.Area}");
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
                foreach (var livestock in Livestocks)
                {
                    Console.WriteLine($"{livestock.Name}\tСтоимость: {livestock.Cost}\t(продукция: {livestock.Production.Name}, сезонов между сбором продукции: {livestock.TimeBetweenHarvests}");
                }
            }
            else Console.WriteLine($"На рынке нет животных на продажу.");
        }

        #endregion Methods

    }
}
