using System;

namespace Homework5
{
    public class FarmGame
    {
        public Farm GameFarm { get; set; }
        public Market GameMarket { get; set; }
        public Seasons CurrentSeason { get; set; }
        public int Money { get; set; }

        public FarmGame()
        {
            GameFarm = new Farm();
            GameMarket = new Market();
            CurrentSeason = Seasons.Winter;
            Money = 1000;
        }

        #region Main Game Methods

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
            FarmMathUtilities.ReadSettings(out int area, out int capacity, out int money);
            GameFarm.Area = area;
            GameFarm.FarmWarehouse.Capacity = capacity;
            Money = money;
            //GameMarket.Seeds = FarmMathUtilities.ReadFromJson("Seeds.json");        //Придется отдельно писать?
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
                Console.WriteLine("1 - Меню фермы");
                Console.WriteLine("2 - Меню грядок");
                Console.WriteLine("3 - Меню строений");
                Console.WriteLine("4 - Меню склада");
                Console.WriteLine("5 - Меню магазина");
                Console.WriteLine("S (s) - Настройки игры");
                Console.WriteLine("Q (q) - Выход из игры");
                Console.WriteLine("Другое - Смена сезона");
                string userChoise = Console.ReadLine();
                Console.WriteLine();

                switch (userChoise)
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
                    case "4":
                        WarehouseMenu();
                        break;
                    case "5":
                        MarketMenu();
                        break;
                    //case "S":
                    //    SettingsMenu();
                    //    break;
                    //case "s":
                    //    SettingsMenu();
                    //    break;
                    case "Q":
                        stopGame = true;
                        break;
                    case "q":
                        stopGame = true;
                        break;
                    default:
                        NextSeason();
                        GameFarm.Harvest(CurrentSeason);
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadKey();
                        break;
                }

            }
        }

        #endregion

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
                    BuyFarmArea();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    break;
                case "2":
                    BuyFarmBuilding();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Buys additional farm area
        /// </summary>
        public void BuyFarmArea()
        {
            int areaCost = 50;
            Console.WriteLine($"Рядом с вашей фермой продается участок земли. Цена за 1 кв.м. - {areaCost} монет.");
            Console.WriteLine("Чем больше кв.м. вы приобретете за раз, тем меньше цена за каждый кв.м. (скидка начинается при покупке от 5 кв.м.)");
            Console.WriteLine("На сколько кв.м.вы хотите расширить вашу ферму ?");
            int area = FarmMathUtilities.ConditionParse();
            double coef = 1;
            if (area < 5) coef = 1;
            else if (area < 15) coef = 0.95;
            else if (area < 30) coef = 0.90;
            else if (area < 50) coef = 0.85;
            else if (area < 70) coef = 0.80;
            else if (area < 100) coef = 0.75;
            else coef = 0.70;
            int fullCost = (int)((areaCost * area) * coef);
            Console.WriteLine($"Итоговая сумма за {area} кв.м. составляет {fullCost} монет (с учетом скидки {(int)((1 - coef) * 100)}%)");
            Console.WriteLine("1 - Купить\n2 - Отказаться от покупки");
            if (FarmMathUtilities.ConditionParse(2) == 1)
            {
                if (Money >= fullCost)
                {
                    GameFarm.Area += area;
                    Money -= fullCost;
                    Console.WriteLine($"\nВы расширили свою ферму, теперь ее площадь составляет {GameFarm.Area} кв.м.\n");
                }
                else Console.WriteLine("Покупка провалилась - у вас меньше монет, чем нужно.\n");
            }
        }

        /// <summary>
        /// Buys new farm building
        /// </summary>
        public void BuyFarmBuilding()
        {
            int buildingCost = 50;
            Console.WriteLine($"Заказать строение у строительной компании. Цена за строение, вмещающее 1 животное (площадью 5 кв.м.) - {buildingCost} монет.");
            Console.WriteLine("Чем большая вместимость строения, тем меньше стоимость за каждое животное (скидка начинается при постройке строения вместимостью от 5 животных)");
            Console.WriteLine("Строение какой вместимости вы хотите заказать?");
            int amount = FarmMathUtilities.ConditionParse();
            double coef = 1;
            if (amount < 5) coef = 1;
            else if (amount < 10) coef = 0.95;
            else if (amount < 15) coef = 0.90;
            else if (amount < 20) coef = 0.85;
            else if (amount < 25) coef = 0.80;
            else if (amount < 35) coef = 0.75;
            else coef = 0.70;
            int fullCost = (int)((buildingCost * amount) * coef);
            Building building = new Building();
            building.Amount = amount;
            Console.WriteLine($"Итоговая сумма за строение вместимостью {amount} животных (площадью {building.Area} кв.м. составляет {fullCost} монет (с учетом скидки {(int)((1 - coef) * 100)}%)");
            Console.WriteLine("1 - Купить\n2 - Отказаться от покупки");
            if (FarmMathUtilities.ConditionParse(2) == 1)
            {
                if (GameFarm.Area >= GameFarm.OccupiedArea + building.Area)
                {
                    if (Money >= fullCost)
                    {
                        building.AddFromConsole();
                        GameFarm.AddBuilding(building);
                        Money -= fullCost;
                    }
                    else Console.WriteLine("Покупка провалилась - у вас меньше монет, чем нужно.");
                }
                else GameFarm.AddBuilding(building);
            }

        }

        #endregion

        #region WarehouseMenu methods

        /// <summary>
        /// Displays Warehouse menu and starts the selected action
        /// </summary>
        public void WarehouseMenu()
        {
            GameFarm.FarmWarehouse.Report();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Продать продукт");
            Console.WriteLine("2 - Продать все продукты");
            Console.WriteLine("Другое - В главное меню");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    SellWarehouseProduct();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    break;
                case "2":
                    SellAllWarehouseProducts();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sells chosen product from warehowse
        /// </summary>
        public void SellWarehouseProduct()
        {
            if (GameFarm.FarmWarehouse.Products.Count > 0)
            {
                int profit = 0;
                GameFarm.FarmWarehouse.Report();
                Console.WriteLine("Введите номер продукта, который хотите продать");
                int productIndex = FarmMathUtilities.ConditionParse(GameFarm.FarmWarehouse.Products.Count);
                profit = GameFarm.FarmWarehouse.Products[productIndex - 1].Cost * GameFarm.FarmWarehouse.Products[productIndex - 1].Weight;
                Console.WriteLine($"Продукт {GameFarm.FarmWarehouse.Products[productIndex - 1].Name} продан со склада. Вы заработали {profit} монет\n");
                GameFarm.FarmWarehouse.Products.RemoveAt(productIndex - 1);
                Money += profit;
            }
            else Console.WriteLine("На складе отсутствуют продукты\n");
        }

        /// <summary>
        /// Sells all products in warehouse
        /// </summary>
        public void SellAllWarehouseProducts()
        {
            if (GameFarm.FarmWarehouse.Products.Count > 0)
            {
                int profit = 0;
                while (GameFarm.FarmWarehouse.Products.Count > 0)
                {
                    profit += GameFarm.FarmWarehouse.Products[0].Cost * GameFarm.FarmWarehouse.Products[0].Weight;
                    GameFarm.FarmWarehouse.Products.RemoveAt(0);
                }
                Money += profit;
                Console.WriteLine($"Все продукты со склада проданы. Вы заработали {profit} монет\n");
            }
            else Console.WriteLine("На складе отсутствуют продукты\n");
        }

        #endregion

        #region MarketMenu methods

        /// <summary>
        /// Displays Market menu and starts the selected action
        /// </summary>
        public void MarketMenu()
        {
            Console.WriteLine($"У вас {Money} монет.");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Купить семена");
            Console.WriteLine("2 - Купить животное");
            Console.WriteLine("Другое - В главное меню");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    BuySeed();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    break;
                case "2":
                    BuyLivestock();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Buys chosen seed from market
        /// </summary>
        public void BuySeed()
        {
            if (GameMarket.Seeds.Count > 0)
            {
                GameMarket.SeedsToConsole();
                Console.WriteLine("Введите номер семян, которые хотите купить");
                int seedIndex = FarmMathUtilities.ConditionParse(GameMarket.Seeds.Count);
                if (Money >= GameMarket.Seeds[seedIndex - 1].Cost)
                {
                    GameFarm.FarmPrimaryWarehowse.Seeds.Add(GameMarket.Seeds[seedIndex - 1]);
                    Money -= GameMarket.Seeds[seedIndex - 1].Cost;
                    Console.WriteLine($"\nВы купили семена \"{GameMarket.Seeds[seedIndex - 1].ThisPlant.Name}\". Семена добавлены на склад сырья\n");
                }
                else Console.WriteLine("Покупка провалилась - у вас меньше монет, чем нужно.\n");
            }
            else Console.WriteLine("В магазине отсутствуют семена\n");
        }

        /// <summary>
        /// Buys chosen livestock from market
        /// </summary>
        public void BuyLivestock()
        {
            if (GameMarket.Livestocks.Count > 0)
            {
                GameMarket.LivestocksToConsole();
                Console.WriteLine("Введите номер животного, которые хотите купить");
                int livestockIndex = FarmMathUtilities.ConditionParse(GameMarket.Livestocks.Count);
                if (Money >= GameMarket.Livestocks[livestockIndex - 1].Cost)
                {
                    GameFarm.FarmPrimaryWarehowse.Livestocks.Add(GameMarket.Livestocks[livestockIndex - 1]);
                    Money -= GameMarket.Livestocks[livestockIndex - 1].Cost;
                    Console.WriteLine($"\nВы купили животное \"{GameMarket.Livestocks[livestockIndex - 1].Name}\". Оно добавлено на склад сырья\n");
                }
                else Console.WriteLine("Покупка провалилась - у вас меньше монет, чем нужно.\n");
            }
            else Console.WriteLine("В магазине отсутствуют животные\n");
        }

        #endregion

    }
}