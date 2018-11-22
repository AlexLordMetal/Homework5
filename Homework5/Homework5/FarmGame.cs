using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    public class FarmGame
    {
        public Farm GameFarm { get; set; }
        public Seasons CurrentSeason { get; set; }
        public int Money { get; set; }

        public FarmGame()
        {
            GameFarm = new Farm(100);
            CurrentSeason = Seasons.Winter;
            Money = 1000;
        }

        #region Methods

        /// <summary>
        /// Changes the season
        /// </summary>
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

        /// <summary>
        /// Starts the game, asks for the name of the farm
        /// </summary>
        public void StartGame()
        {
            Console.Write("По наследству от дальнего родственника вам досталась ферма.\nДайте название вашей ферме: ");
            GameFarm.Name = Console.ReadLine();
            Console.WriteLine($"Добро пожаловать на ферму {GameFarm.Name}!\nДля начала игры нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays FarmGame main menu and starts the selected action
        /// </summary>
        public void FarmGameMenu()
        {
            var stopGame = false;
            while (stopGame != true)
            {
                Console.Clear();
                Console.WriteLine($"У вас {Money} монет.");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Меню фермы;");
                Console.WriteLine("2 - Меню грядок;");
                Console.WriteLine("3 - Меню строений;");
                Console.WriteLine("4 - Меню склада;");
                Console.WriteLine("5 - Меню магазина;");
                Console.WriteLine("S (s) - Настройки игры;");
                Console.WriteLine("Q (q) - Выход из игры;");
                Console.WriteLine("Другое - Смена сезона.");
                string choise = Console.ReadLine();
                Console.WriteLine();

                switch (choise)
                {
                    case "1":
                        FarmMenu();
                        break;
                        //case "2":
                        //    GameFarm.GardenBedsReport();
                        //    break;
                        //case "3":
                        //    GameFarm.BuildingsReport();
                        //    break;
                        //case "4":
                        //    GameFarm.FarmWarehouse.Report();
                        //    break;
                        //case "5":
                        //    GameFarm.FarmManagement();
                        //    break;
                        //case "Q":
                        //    stopGame = true;
                        //    break;
                        //case "q":
                        //    stopGame = true;
                        //    break;
                        //default:
                        //    NextSeason();
                        //    GameFarm.Harvest(CurrentSeason);
                        //    break;
                }
            }
        }

        #region FarmMenu methods

        /// <summary>
        /// Displays Farm menu and starts the selected action
        /// </summary>
        public void FarmMenu()
        {
            GameFarm.Report();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Расширить ферму (увеличить площадь);");
            Console.WriteLine("2 - Купить строение;");
            Console.WriteLine("Другое - В главное меню.");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    //BuyFarmArea();
                    break;
                case "2":
                    //BuyFarmBuilding();
                    break;
                default:
                    break;
            }
        }

        public void BuyFarmArea()
        {
            int areaCost = 50;
            Console.WriteLine($"Рядом с вашей фермой продается участок земли. Стартовая цена за 1 кв.м. - {areaCost} монет.");
            Console.WriteLine("Чем больше кв.м. вы приобретете за раз, тем меньше цена за каждый кв.м.\nНа сколько кв.м. вы хотите расширить вашу ферму?\n");
            int area = FarmMathUtilities.ConditionParse();

            Console.WriteLine($"Рядом с вашей фермой продается участок земли. Стартовая цена за 1 кв.м. - {areaCost} монет.");
        }

        #endregion

    }
}

        #endregion

    }
}
