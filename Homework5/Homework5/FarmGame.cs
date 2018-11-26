using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
        /// Starts the game, asks for type of game (new or load)
        /// </summary>
        public void StartGame()
        {
            if (File.Exists("Seeds.json")) GameMarket.Seeds = JsonConvert.DeserializeObject<List<Seed>>(ReadFromJson("Seeds.json"));
            else Console.WriteLine("ВНИМАНИЕ! Библиотеки семян растений не найдено. Добавьте файл \"Seeds.json\" в каталог с игрой и перезапустите игру, либо добавьте семена новых растений в меню настроек игры.\n");
            if (File.Exists("Livestocks.json")) GameMarket.Livestocks = JsonConvert.DeserializeObject<List<Livestock>>(ReadFromJson("Livestocks.json"));
            else Console.WriteLine("ВНИМАНИЕ! Библиотеки животных не найдено. Добавьте файл \"Livestocks.json\" в каталог с игрой и перезапустите игру, либо добавьте новых животных в меню настроек игры.\n");
            Console.Write("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Console.Clear();

            string[] savFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.sav");
            if (savFiles.Length > 0)
            {
                Console.WriteLine("Сделайте выбор:\n1 - Начать новую игру\n2 - Загрузить сохранение");
                if (FarmMathUtilities.ConditionParse(2) == 1) NewFarmGame();
                else LoadFarmGame(savFiles);
            }
            else NewFarmGame();
            FarmGameMenu();
        }

        /// <summary>
        /// Starts new game, asks for the name of the farm
        /// </summary>
        public void NewFarmGame()
        {
            ReadGameSettings(out int area, out int capacity, out int money);
            GameFarm.Area = area;
            GameFarm.FarmWarehouse.Capacity = capacity;
            Money = money;
            Console.Write("По наследству от дальнего родственника вам досталась ферма.\nДайте название вашей ферме: ");
            GameFarm.Name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Добро пожаловать на ферму {GameFarm.Name}!\nДля начала игры нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Loads FarmGame from sav file
        /// </summary>
        /// <param name="savFiles"></param>
        public void LoadFarmGame(string[] savFiles)
        {
            Console.WriteLine("Файлы сохранения:");
            for (int i = 0; i < savFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {savFiles[i]}");
            }
            Console.Write("Выберите номер файла сохранения для загрузки: ");
            string loadGameString = ReadFromJson(savFiles[FarmMathUtilities.ConditionParse(savFiles.Length) - 1]);
            FarmGame loadGame = JsonConvert.DeserializeObject<FarmGame>(loadGameString);
            GameFarm = loadGame.GameFarm;
            CurrentSeason = loadGame.CurrentSeason;
            Money = loadGame.Money;
            Console.WriteLine("Сохранение загружено.\nДля продолжения игры нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Reads FarmGame settings from ini file
        /// </summary>
        /// <param name="file">Filename</param>
        public void ReadGameSettings(out int area, out int capacity, out int money)
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
        /// Reads a string from Json file
        /// </summary>
        /// <param name="file">Filename</param>
        public static string ReadFromJson(string file)
        {
            string stringFromFile = null;
            if (File.Exists(file))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    stringFromFile = reader.ReadToEnd();
                }
            }
            return stringFromFile;
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

                Console.Write($"У вас ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Money);
                Console.ResetColor();
                Console.WriteLine($" монет.");

                Console.Write($"Текущий сезон - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(FarmMathUtilities.SeasonsToRusString(CurrentSeason));
                Console.ResetColor();
                Console.WriteLine("!");

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Меню фермы");
                Console.WriteLine("2 - Меню грядок");
                Console.WriteLine("3 - Меню строений");
                Console.WriteLine("4 - Меню склада продуктов");
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
                    case "2":
                        GardenBedsMenu();
                        break;
                    case "3":
                        BuildingsMenu();
                        break;
                    case "4":
                        WarehouseMenu();
                        break;
                    case "S":
                        SettingsMenu();
                        break;
                    case "s":
                        SettingsMenu();
                        break;
                    case "Q":
                        stopGame = true;
                        break;
                    case "q":
                        stopGame = true;
                        break;
                    default:
                        NextSeason();
                        string bonusName = SeasonEvent();
                        GameFarm.Harvest(CurrentSeason, bonusName);
                        Console.WriteLine("Нажмите любую клавишу для продолжения.");
                        Console.ReadKey();
                        break;
                }
                if (stopGame == true)
                {
                    Console.WriteLine("Хотите сохранить игру перед выходом? Все несохраненные данные будут утеряны.\n1 - Да\n2 - Нет");
                    if (FarmMathUtilities.ConditionParse(2) == 1) SaveFarmGame();
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
            Console.WriteLine("1 - Расширить ферму (увеличить площадь)");
            Console.WriteLine("2 - Купить строение");
            Console.WriteLine("3 - Купить семена на рынке");
            Console.WriteLine("4 - Купить животное на рынке");
            Console.WriteLine("Другое - В главное меню");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    BuyFarmArea();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "2":
                    BuyFarmBuilding();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "3":
                    BuySeed();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "4":
                    BuyLivestock();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
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
            Console.WriteLine("На сколько кв.м. вы хотите расширить вашу ферму ?");
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
            Console.WriteLine($"Итоговая сумма за {area} кв.м. составляет {fullCost} монет (с учетом скидки {((1 - coef) * 100)}%)");
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
                    Console.WriteLine($"\nВы купили семена \"{GameMarket.Seeds[seedIndex - 1].ThisPlant.Name}\". Семена добавлены на склад ресурсов\n");
                }
                else Console.WriteLine("Покупка провалилась - у вас меньше монет, чем нужно.\n");
            }
            else Console.WriteLine("В магазине отсутствуют семена!\nДобавьте новые семена через пункт \"Добавить семена нового растения в библиотеку игры\" в настройках игры.");
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
                    Console.WriteLine($"\nВы купили животное \"{GameMarket.Livestocks[livestockIndex - 1].Name}\". Оно добавлено на склад ресурсов\n");
                }
                else Console.WriteLine("Покупка провалилась - у вас меньше монет, чем нужно.\n");
            }
            else Console.WriteLine("В магазине отсутствуют животные!\nДобавьте новых животных через пункт \"Добавить новое животное в библиотеку игры\" в настройках игры.");
        }

        #endregion

        #region GardenBeds menu methods

        /// <summary>
        /// Displays GardenBeds menu and starts the selected action
        /// </summary>
        public void GardenBedsMenu()
        {
            GameFarm.GardenBedsReport();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Добавить грядку;");
            Console.WriteLine("2 - Посадить семена растения на грядку;");
            Console.WriteLine("3 - Выкопать растение с грядки;");
            Console.WriteLine("4 - Убрать грядку;");
            Console.WriteLine("Другое - В главное меню");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    AddGardenBed();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "2":
                    AddPlant();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "3":
                    RemovePlant();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "4":
                    RemoveGardenBed();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates new gardenbed and adds it to list of gardenbeds
        /// </summary>
        public void AddGardenBed()
        {
            GardenBed gardenBed = new GardenBed();
            gardenBed.AddFromConsole();
            GameFarm.AddGardenBed(gardenBed);
        }

        /// <summary>
        /// Adds plant from market's list of seeds
        /// </summary>
        public void AddPlant()
        {
            if (GameFarm.GardenBeds.Count > 0)
            {
                if (GameFarm.FarmPrimaryWarehowse.Seeds.Count > 0)
                {
                    GameFarm.FarmPrimaryWarehowse.SeedsToConsole();
                    Console.Write($"Укажите номер семян растения, которое вы хотите посадить: ");
                    int numberOfSeed = FarmMathUtilities.ConditionParse(GameFarm.FarmPrimaryWarehowse.Seeds.Count) - 1;
                    Plant plant = new Plant(GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].ThisPlant.Name,
                        GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].ThisPlant.HarvestSeason,
                        GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].ThisPlant.Area,
                        GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].ThisPlant.Cost,
                        GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].ThisPlant.IsMultiHarvest);
                    if (GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].PlantingSeason == CurrentSeason)
                    {
                        Console.Write($"Укажите номер грядки, на которую хотите посадить растение \"{plant.Name}\" (всего грядок - {GameFarm.GardenBeds.Count}): ");
                        int numberOfGardenBed = FarmMathUtilities.ConditionParse(GameFarm.GardenBeds.Count) - 1;
                        int plantsCount = GameFarm.GardenBeds[numberOfGardenBed].Plants.Count;
                        GameFarm.GardenBeds[numberOfGardenBed].AddPlant(plant);
                        if (plantsCount != GameFarm.GardenBeds[numberOfGardenBed].Plants.Count)
                        {
                            GameFarm.FarmPrimaryWarehowse.Seeds.RemoveAt(numberOfSeed);
                        }
                    }
                    else Console.WriteLine($"Семена растения {plant.Name} нельзя посадить сейчас, так как их сезон посадки " +
                        $"(\"{FarmMathUtilities.SeasonsToRusString(GameFarm.FarmPrimaryWarehowse.Seeds[numberOfSeed].PlantingSeason)}\") не соответствует текущему сезону" +
                        $"(\"{FarmMathUtilities.SeasonsToRusString(CurrentSeason)}\").\n");
                }
                else GameFarm.FarmPrimaryWarehowse.SeedsToConsole();
            }
            else Console.WriteLine("На ферме нет ни одной грядки.\n");
        }

        /// <summary>
        /// Removes plant from chosen gardenbed
        /// </summary>
        public void RemovePlant()
        {
            if (GameFarm.GardenBeds.Count > 0)
            {
                Console.WriteLine($"Укажите номер грядки, c которой хотите выкопать растение (всего грядок - {GameFarm.GardenBeds.Count}): ");
                int indexOfGardenBed = FarmMathUtilities.ConditionParse(GameFarm.GardenBeds.Count) - 1;
                GameFarm.GardenBeds[indexOfGardenBed].ReportOnlyPlants();
                GameFarm.GardenBeds[indexOfGardenBed].RemovePlant();
            }
            else Console.WriteLine("На ферме нет ни одной грядки.\n");
        }

        /// <summary>
        /// Removes chosen gardenbed from list of gardenbeds
        /// </summary>
        public void RemoveGardenBed()
        {
            if (GameFarm.GardenBeds.Count > 0)
            {
                Console.WriteLine($"Укажите номер убираемой грядки (всего грядок - {GameFarm.GardenBeds.Count}):");
                int numberGardenBedRemove = FarmMathUtilities.ConditionParse(GameFarm.GardenBeds.Count);
                GameFarm.GardenBeds.RemoveAt(numberGardenBedRemove - 1);
                Console.WriteLine($"Грядка {numberGardenBedRemove} убрана.\n");
            }
            else Console.WriteLine("На ферме нет ни одной грядки.\n");
        }

        #endregion

        #region Buildings menu methods

        /// <summary>
        /// Displays Buildings menu and starts the selected action
        /// </summary>
        public void BuildingsMenu()
        {
            GameFarm.BuildingsReport();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Поселить животное в строение;");
            Console.WriteLine("2 - Переселить животное в другое строение;");
            Console.WriteLine("3 - Выгнать животное из строения (вернуть на склад ресурсов;");
            Console.WriteLine("4 - Разрушить строение;");
            Console.WriteLine("Другое - В главное меню");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    AddLivestock();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "2":
                    GameFarm.ChangeLivestockBuilding();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "3":
                    RemoveLivestock();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "4":
                    RemoveBuilding();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adds livestock from market's list of livestocks
        /// </summary>
        public void AddLivestock()
        {
            if (GameFarm.Buildings.Count > 0)
            {
                GameFarm.FarmPrimaryWarehowse.LivestocksToConsole();
                if (GameFarm.FarmPrimaryWarehowse.Livestocks.Count > 0)
                {
                    Console.Write($"Укажите номер животного, которое вы хотите заселить: ");
                    int numberOfLivestock = FarmMathUtilities.ConditionParse(GameFarm.FarmPrimaryWarehowse.Livestocks.Count) - 1;
                    Product production = new Product(GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].Production.Name,
                        GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].Production.Weight,
                        GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].Production.Cost);
                    Livestock livestock = new Livestock(GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].Name,
                        GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].Cost,
                        production,
                        GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].TimeBetweenHarvests,
                        GameFarm.FarmPrimaryWarehowse.Livestocks[numberOfLivestock].IsMultiHarvest);
                    Console.Write($"Укажите номер строения, в которое хотите поселить животное \"{livestock.Name}\" (всего строений - {GameFarm.Buildings.Count}): ");
                    int numberOfBuilding = FarmMathUtilities.ConditionParse(GameFarm.Buildings.Count) - 1;
                    int livestocksCount = GameFarm.Buildings[numberOfBuilding].Livestocks.Count;
                    GameFarm.Buildings[numberOfBuilding].AddLivestock(livestock);
                    if (livestocksCount != GameFarm.Buildings[numberOfBuilding].Livestocks.Count)
                    {
                        GameFarm.FarmPrimaryWarehowse.Livestocks.RemoveAt(numberOfLivestock);
                    }
                }
            }
            else Console.WriteLine("На ферме нет ни одного строения.\n");
        }

        /// <summary>
        /// Removes livestock from one chosen building and adds it to another chosen building
        /// </summary>
        public void ChangeLivestockBuilding()
        {
            if (GameFarm.Buildings.Count > 1)
            {

            }
            else if (GameFarm.Buildings.Count > 0) Console.WriteLine("Переселение невозможно, так как на ферме всего одно строение.\n");
            else Console.WriteLine("На ферме нет ни одного строения.\n");
        }

        /// <summary>
        /// Removes livestock from chosen building to primary warehouse
        /// </summary>
        public void RemoveLivestock()
        {
            if (GameFarm.Buildings.Count > 0)
            {
                Console.WriteLine($"Укажите номер строения, из которого хотите выселить животное (всего строений - {GameFarm.Buildings.Count}): ");
                int indexOfBuilding = FarmMathUtilities.ConditionParse(GameFarm.Buildings.Count) - 1;
                GameFarm.Buildings[indexOfBuilding].ReportOnlyLivestocks();
                if (GameFarm.Buildings[indexOfBuilding].Livestocks.Count > 0)
                {
                    GameMarket.Livestocks.Add(GameFarm.Buildings[indexOfBuilding].RemoveLivestock());
                }
            }
            else Console.WriteLine("На ферме нет ни одного строения.\n");
        }

        /// <summary>
        /// Removes chosen building from list of buildings
        /// </summary>
        public void RemoveBuilding()
        {
            if (GameFarm.Buildings.Count > 0)
            {
                Console.WriteLine($"Укажите номер строения, которое хотите разрушить (всего строений - {GameFarm.Buildings.Count}):");
                int numberBuildingRemove = FarmMathUtilities.ConditionParse(GameFarm.Buildings.Count);
                string removeBuildingName = GameFarm.Buildings[numberBuildingRemove - 1].Name;
                GameFarm.GardenBeds.RemoveAt(numberBuildingRemove - 1);
                Console.WriteLine($"Строение {numberBuildingRemove} \"{removeBuildingName}\" разрушено.\n");
            }
            else Console.WriteLine("На ферме нет ни одного строения.\n");
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
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "2":
                    SellAllWarehouseProducts();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
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

        #region Settings menu methods

        /// <summary>
        /// Displays Settings menu and starts the selected action
        /// </summary>
        public void SettingsMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Добавить семена нового растения в библиотеку игры");
            Console.WriteLine("2 - Добавить новое животное в библиотеку игры");
            Console.WriteLine("3 - Изменить стартовые параметры игры (площадь фермы, вместимость склада, количество монет)");
            Console.WriteLine("4 - Сохранить игру");
            Console.WriteLine("Другое - В главное меню");
            string userChoise = Console.ReadLine();
            Console.WriteLine();

            switch (userChoise)
            {
                case "1":
                    GameMarket.SeedsToJson();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "2":
                    GameMarket.LivestocksToJson();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "3":
                    ChangeGameSettings();
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                case "4":
                    SaveFarmGame();
                    Console.WriteLine($"Игра сохранена. Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Asks for the FarmGame new settings
        /// </summary>
        public void ChangeGameSettings()
        {
            Console.Write("Введите начальную площадь фермы (кв.м.): ");
            int area = FarmMathUtilities.ConditionParse();
            Console.Write("Введите начальную вместимость склада (центнеров): ");
            int capasity = FarmMathUtilities.ConditionParse();
            Console.Write("Введите начальное количество монет: ");
            int money = FarmMathUtilities.ConditionParse();
            WriteGameSettings(area, capasity, money);
            Console.WriteLine($"Стартовые параметры изменены. Потребуется перезапустить игру для применения новых параметров.\nНажмите любую клавишу для продолжения.\n");
        }

        /// <summary>
        /// Writes FarmGame new settings to ini file
        /// </summary>
        /// <param name="area">Starting area of the farm</param>
        /// <param name="capacity">Starting capacity of the farm's warehouse</param>
        /// <param name="money">Starting amount of money</param>
        public void WriteGameSettings(int area, int capacity, int money)
        {
            using (StreamWriter writer = new StreamWriter("Settings.ini"))
            {
                writer.WriteLine(area);
                writer.WriteLine(capacity);
                writer.WriteLine(money);
            }
        }

        /// <summary>
        /// Saves FarmGame to chosen sav file
        /// </summary>
        public void SaveFarmGame()
        {
            var saved = false;
            while (saved != true)
            {
                Console.Write("Введите имя файла сохранения: ");
                string saveName = Console.ReadLine() + ".sav";
                if (File.Exists(saveName) == true)
                {
                    Console.WriteLine("Файл с таким названием уже существует. Хотите перезаписать его?\n1 - Да\n2 - Нет");
                    if (FarmMathUtilities.ConditionParse(2) == 1)
                    {
                        using (StreamWriter writer = new StreamWriter(saveName))
                        {
                            writer.Write(JsonConvert.SerializeObject(this));
                        }
                        saved = true;
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(saveName))
                    {
                        writer.Write(JsonConvert.SerializeObject(this));
                    }
                    saved = true;
                }
            }
        }

        #endregion

        #region Season events

        /// <summary>
        /// Randoms season's event.
        /// </summary>
        /// <returns></returns>
        public string SeasonEvent()
        {
            string name = null;
            Random random = new Random();

            int rndChoice = random.Next(100);
            if (rndChoice < 35) name = PositiveEvent(random);
            else if (rndChoice < 50) NegativeEvent(random);

            return name;
        }

        /// <summary>
        /// Randoms season's positive event.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public string PositiveEvent(Random random)
        {
            string name = null;
            int rndChoice = random.Next(100);

            if (rndChoice < 35)
            {
                List<string> names = new List<string>();
                foreach (var seed in GameMarket.Seeds)
                {
                    if (seed.ThisPlant.HarvestSeason == CurrentSeason)
                    {
                        names.Add(seed.ThisPlant.Name);
                    }
                }
                if (names.Count > 0)
                {
                    name = names[random.Next(names.Count)];
                    Console.WriteLine($"Наступил сезон растения \"{name}\". {name} дает вдвое больше продукта.\n");
                }
            }
            else if (rndChoice < 70)
            {
                List<string> names = new List<string>();
                if (GameMarket.Livestocks.Count > 0)
                {
                    name = GameMarket.Livestocks[random.Next(names.Count)].Name;
                    Console.WriteLine($"Наступил сезон животного \"{name}\". {name} дает вдвое больше продукта (если в текущем сезоне животное дает продукт).\n");
                }
            }
            else if (rndChoice < 80)
            {
                if (GameMarket.Seeds.Count > 0)
                {
                    GameFarm.FarmPrimaryWarehowse.Seeds.Add(GameMarket.Seeds[random.Next(GameMarket.Seeds.Count)]);
                    Console.WriteLine($"Вы получили посылку с подарком от старого друга. Открыв посылку, вы обнаружили семена растения \"{GameFarm.FarmPrimaryWarehowse.Seeds[GameFarm.FarmPrimaryWarehowse.Seeds.Count - 1].ThisPlant.Name}\"");
                    Console.WriteLine("Семена добавлены на склад ресурсов\n");
                }
            }
            else if (rndChoice < 90)
            {
                if (GameMarket.Livestocks.Count > 0)
                {
                    GameFarm.FarmPrimaryWarehowse.Livestocks.Add(GameMarket.Livestocks[random.Next(GameMarket.Livestocks.Count)]);
                    Console.WriteLine($"Вы получили посылку с подарком от старого друга. Открыв посылку, вы обнаружили животное \"{GameFarm.FarmPrimaryWarehowse.Livestocks[GameFarm.FarmPrimaryWarehowse.Livestocks.Count - 1].Name}\"");
                    Console.WriteLine("Животное добавлено на склад ресурсов\n");
                }
            }
            else
            {
                int rndMoney = random.Next(100, 301);
                Console.WriteLine($"Ваши продукты получили премию \"Экологически чистые продукты\"! За победу вы получаете {rndMoney} монет.\n");
                Money += rndMoney;
            }

            return name;
        }

        /// <summary>
        /// Randoms season's negative event.
        /// </summary>
        /// <param name="random"></param>
        public void NegativeEvent(Random random)
        {
            int rndChoice = random.Next(100);
            if (rndChoice < 20)
            {
                if (Money > 0)
                {
                    int rndMoney = random.Next(Money + 1);
                    Console.WriteLine($"Вас ограбили. У вас украли {rndMoney} монет.\n");
                    Money -= rndMoney;
                }
            }
            else if (rndChoice < 40)
            {
                if (GameFarm.Buildings.Count > 0)
                {
                    int buildingNumber = random.Next(GameFarm.Buildings.Count);
                    Console.Write($"На ферме случилось землетрясение. Строение \"{GameFarm.Buildings[buildingNumber].Name}\" разрушено. ");
                    if (GameFarm.Buildings[buildingNumber].OccupiedAmount > 0)
                    {
                        Console.WriteLine("Все животные в нем погибли.\n");
                    }
                    else Console.WriteLine($"Повезло, что там никто не живет.\n");
                    GameFarm.Buildings.RemoveAt(buildingNumber);
                }
            }
            else if (rndChoice < 60)
            {
                if (GameFarm.Buildings.Count > 0)
                {
                    int buildingNumber = random.Next(GameFarm.Buildings.Count);
                    Console.Write($"В строении \"{GameFarm.Buildings[buildingNumber].Name}\" случился пожар. ");
                    if (GameFarm.Buildings[buildingNumber].OccupiedAmount > 0)
                    {
                        if (random.Next(10) < 7)
                        {
                            int livestockNumber = random.Next(GameFarm.Buildings[buildingNumber].Livestocks.Count);
                            Console.WriteLine($"Животное \"{GameFarm.Buildings[buildingNumber].Livestocks[livestockNumber].Name}\" погибло.\n");
                            GameFarm.Buildings[buildingNumber].Livestocks.RemoveAt(livestockNumber);
                        }
                        else
                        {
                            Console.WriteLine($"Все животные погибли.\n");
                            GameFarm.Buildings[buildingNumber].Livestocks.RemoveRange(0, GameFarm.Buildings[buildingNumber].Livestocks.Count);
                        }
                    }
                    else Console.WriteLine($"Повезло, что там никто не живет.\n");
                }
            }
            else if (rndChoice < 80)
            {
                if (CurrentSeason == Seasons.Summer || CurrentSeason == Seasons.Autumn)
                {
                    Console.Write("Давно не было дождей. На ферме засуха. ");
                    int allOccupied = 0;
                    foreach (var gardenbed in GameFarm.GardenBeds) allOccupied += gardenbed.OccupiedArea;
                    if (GameFarm.GardenBeds.Count > 0 && allOccupied > 0)
                    {
                        if (random.Next(10) < 7)
                        {
                            int gardenbedNumber = random.Next(GameFarm.GardenBeds.Count);
                            if (GameFarm.GardenBeds[gardenbedNumber].OccupiedArea > 0)
                            {
                                int plantNumber = random.Next(GameFarm.GardenBeds[gardenbedNumber].Plants.Count);
                                Console.WriteLine($"От засухи погибло растение {GameFarm.GardenBeds[gardenbedNumber].Plants[plantNumber].Name} на {gardenbedNumber + 1} грядке.\n");
                                GameFarm.GardenBeds[gardenbedNumber].Plants.RemoveAt(plantNumber);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"От сильной засухи погибли все растения на ферме\n");
                            for (int index = 0; index < GameFarm.GardenBeds.Count; index++)
                            {
                                GameFarm.GardenBeds[index].Plants.RemoveRange(0, GameFarm.GardenBeds[index].Plants.Count);
                            }
                        }
                    }
                    else Console.WriteLine($"Повезло, что на ферме ничего не растет.\n");
                }
            }
            else if (CurrentSeason == Seasons.Spring || CurrentSeason == Seasons.Summer || CurrentSeason == Seasons.Autumn)
            {
                if (random.Next(10) < 5)
                {
                    if (GameFarm.Buildings.Count > 0)
                    {
                        int buildingNumber = random.Next(GameFarm.Buildings.Count);
                        Console.Write($"Из-за сильных дождей река рядом с фермой вышла из берегов. От наводнения пострадало строение \"{GameFarm.Buildings[buildingNumber].Name}\". ");
                        if (GameFarm.Buildings[buildingNumber].OccupiedAmount > 0)
                        {
                            int livestockNumber = random.Next(GameFarm.Buildings[buildingNumber].Livestocks.Count);
                            Console.WriteLine($"Животное \"{GameFarm.Buildings[buildingNumber].Livestocks[livestockNumber].Name}\" погибло.\n");
                            GameFarm.Buildings[buildingNumber].Livestocks.RemoveAt(livestockNumber);
                        }
                        else Console.WriteLine($"Повезло, что в здании никто не живет.\n");
                    }
                }
                else
                {
                    if (GameFarm.GardenBeds.Count > 0)
                    {
                        int gardenbedNumber = random.Next(GameFarm.GardenBeds.Count);
                        Console.Write($"Из-за сильных дождей река рядом с фермой вышла из берегов. От наводнения пострадала {gardenbedNumber + 1} грядка\". ");
                        if (GameFarm.GardenBeds[gardenbedNumber].OccupiedArea > 0)
                        {
                            int plantNumber = random.Next(GameFarm.GardenBeds[gardenbedNumber].Plants.Count);
                            Console.WriteLine($"Погибло растение {GameFarm.GardenBeds[gardenbedNumber].Plants[plantNumber].Name}.\n");
                            GameFarm.GardenBeds[gardenbedNumber].Plants.RemoveAt(plantNumber);
                        }
                        else Console.WriteLine($"Повезло, что на грядке ничего не растет.\n");
                    }
                }
            }
        }

        #endregion

    }
}