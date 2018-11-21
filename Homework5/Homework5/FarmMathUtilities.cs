using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    public static class FarmMathUtilities
    {
        public static double OccupiedPercent(int occupiedArea, int area)
        {
            double occupiedPercent = Math.Round((double)occupiedArea / (double)area * 100, 2);
            return occupiedPercent;
        }

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


    }
}
