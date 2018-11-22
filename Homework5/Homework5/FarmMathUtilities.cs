using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
        /// Reads the list of objects from Json file
        /// </summary>
        /// <param name="file">Filename</param>
        public static List<Object> ReadFromJson(string file)
        {
            List<Object> listOfObjects = new List<Object>();
            if (File.Exists(file))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    listOfObjects = JsonConvert.DeserializeObject<List<Object>>(reader.ReadToEnd());
                }
            }
            return listOfObjects;
        }

        /// <summary>
        /// Adds new object to Json file with list of these objects
        /// </summary>
        /// <param name="thisObject">Object to add</param>
        /// <param name="file">Filename</param>
        public static void AddToJson(Object thisObject, string file)
        {
            List<Object> listOfObjects = ReadFromJson(file);
            listOfObjects.Add(thisObject);
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(JsonConvert.SerializeObject(listOfObjects));
            }

        }

        /// <summary>
        /// Reads settings from ini file
        /// </summary>
        /// <param name="file">Filename</param>
        public static void ReadSettings(out int area, out int capacity, out int money)
        {
            area = 100;
            capacity = 100;
            money = 1000;
            if (File.Exists("Settings.ini"))
            {
                using (StreamReader reader = new StreamReader("Settings.ini"))
                {
                    if (Int32.TryParse(reader.ReadLine(), out int tempArea) == true && tempArea > 0)
                    {
                        area = tempArea;
                    }
                    if (Int32.TryParse(reader.ReadLine(), out int tempCapacity) == true && tempCapacity > 0)
                    {
                        capacity = tempCapacity;
                    }
                    if (Int32.TryParse(reader.ReadLine(), out int tempMoney) == true && tempMoney > 0)
                    {
                        money = tempMoney;
                    }
                }
            }
        }

        /// <summary>
        /// Writes new settings to ini file
        /// </summary>
        /// <param name="thisObject">Object to add</param>
        /// <param name="file">Filename</param>
        public static void WriteSettings(int area, int capacity, int money)
        {
            using (StreamWriter writer = new StreamWriter("Settings.ini"))
            {
                writer.WriteLine(area);
                writer.WriteLine(capacity);
                writer.WriteLine(money);
            }
        }

    }
}
