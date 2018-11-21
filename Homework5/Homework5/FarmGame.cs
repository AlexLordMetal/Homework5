using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    class FarmGame
    {
        public Farm GameFarm { get; set; }
        public Seasons CurrentSeason { get; set; }

        public FarmGame(Farm gameFarm, Seasons currentSeason = Seasons.Winter)
        {
            GameFarm = gameFarm;
            CurrentSeason = currentSeason;
        }


        //Methods

        public void NextSeason()
        {
            if (CurrentSeason != Seasons.Autumn)
            {
                CurrentSeason = CurrentSeason + 1;
            }
            else
            {
                CurrentSeason = Seasons.Winter;
            }
        }

        public void FarmGameMenu()
        {
            var stopGame = false;
            while (stopGame != true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Отчет по ферме;");
                Console.WriteLine("2 - Отчет по грядкам;");
                Console.WriteLine("3 - Отчет по строениям;");
                Console.WriteLine("4 - Отчет по складу;");
                Console.WriteLine("5 - Управление фермой;");
                Console.WriteLine("Q (q) - Выход из игры;");
                Console.WriteLine("Другое - Смена сезона.");
                string choise = Console.ReadLine();
                Console.WriteLine();

                switch (choise)
                {
                    case "1":
                        GameFarm.FarmReport();
                        break;
                    case "2":
                        GameFarm.GardenBedsReport();
                        break;
                    case "3":
                        GameFarm.BuildingsReport();
                        break;
                    case "4":
                        GameFarm.FarmWarehouse.Report();
                        break;
                    case "5":
                        GameFarm.FarmManagement();
                        break;
                    case "Q":
                        stopGame = true;
                        break;
                    case "q":
                        stopGame = true;
                        break;
                    default:
                        NextSeason();
                        GameFarm.Harvest(CurrentSeason);
                        break;
                }
            }
        }


    }
}
