using System;

namespace Homework5
{
    public static class FarmMathUtilities
    {
        /// <summary>
        /// Counts filling percentage
        /// </summary>
        /// <param name="occupiedArea"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public static double OccupiedPercent(int occupiedArea, int area)
        {
            double occupiedPercent = Math.Round((double)occupiedArea / (double)area * 100, 2);
            return occupiedPercent;
        }

        /// <summary>
        /// Reads string from console and converts it to number with conditions - greater than 0 and less or equal than Condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int ConditionParse(int condition = 2147483647)
        {
            var isCorrect = false;
            int area = 0;
            while (isCorrect != true)
            {
                isCorrect = Int32.TryParse(Console.ReadLine(), out area);
                if (area <= 0 || area > condition)
                {
                    isCorrect = false;
                }
                if (isCorrect == false)
                {
                    Console.Write("Ввод некорректен! Еще раз: ");
                }
            }
            return area;
        }

        /// <summary>
        /// Returns string of season in russian
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        public static string SeasonsToRusString(Seasons season)
        {
            string rusSeason = null;
            switch (season)
            {
                case Seasons.Winter:
                    rusSeason = "Зима";
                    break;
                case Seasons.Spring:
                    rusSeason = "Весна";
                    break;
                case Seasons.Summer:
                    rusSeason = "Лето";
                    break;
                case Seasons.Autumn:
                    rusSeason = "Осень";
                    break;
                default:
                    break;
            }
            return rusSeason;
        }

    }
}
