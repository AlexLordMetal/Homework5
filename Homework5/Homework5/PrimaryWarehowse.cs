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
            Seeds = new List<Seed>();
            Livestocks = new List<Livestock>();
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
                for (int index = 0; index < Seeds.Count; index++)
                {
                    Console.WriteLine($"{index + 1}. {Seeds[index].ThisPlant.Name}\tСтоимость: {Seeds[index].Cost}\t(сезон посадки: {FarmMathUtilities.SeasonsToRusString(Seeds[index].PlantingSeason)}, площадь посадки: {Seeds[index].ThisPlant.Area})");
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
                for (int index = 0; index < Livestocks.Count; index++)
                {
                    Console.WriteLine($"{index + 1}. {Livestocks[index].Name}\t(продукция: {Livestocks[index].Production.Name}, сезонов между сбором продукции: {Livestocks[index].TimeBetweenHarvests})");
                }
            }
            else Console.WriteLine($"На складе сырья нет животных для заселения.");
        }

        #endregion Methods

    }
}