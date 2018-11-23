using System;
using System.Collections.Generic;

namespace Homework5
{
    public class Farm
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public List<GardenBed> GardenBeds { get; set; }
        public List<Building> Buildings { get; set; }
        public Warehouse FarmWarehouse { get; set; }
        public PrimaryWarehowse FarmPrimaryWarehowse { get; set; }
        public int OccupiedArea
        {
            get
            {
                int occupiedArea = 0;
                foreach (var gardenbed in GardenBeds)
                {
                    occupiedArea += gardenbed.Area;
                }
                foreach (var building in Buildings)
                {
                    occupiedArea += building.Area;
                }
                return occupiedArea;
            }
        }

        public Farm()
        {
            GardenBeds = new List<GardenBed>();
            Buildings = new List<Building>();
            FarmWarehouse = new Warehouse();
            FarmPrimaryWarehowse = new PrimaryWarehowse();
        }

        public Farm(int area)
        {
            Area = area;
            GardenBeds = new List<GardenBed>();
            Buildings = new List<Building>();
            FarmWarehouse = new Warehouse();
            FarmPrimaryWarehowse = new PrimaryWarehowse();
        }

        #region FarmMethods

        /// <summary>
        /// Writes report of gardenbed to console
        /// </summary>
        public void Report()
        {
            Console.WriteLine($"Ферма \"{Name}\" площадью {Area} кв.м. с {GardenBeds.Count} грядками и {Buildings.Count} строениями. Всего занято {OccupiedArea} кв.м. ({FarmMathUtilities.OccupiedPercent(OccupiedArea, Area)}% площади).\n");
        }

        /// <summary>
        /// Harvests products according to season
        /// </summary>
        /// <param name="season"></param>
        public void Harvest(Seasons season)     //пока без условий isMultiple и Seasons between
        {
            switch (season)
            {
                case Seasons.Winter:
                    Console.WriteLine("Наступила зима.");
                    break;
                case Seasons.Spring:
                    Console.WriteLine("Наступила весна.");
                    break;
                case Seasons.Summer:
                    Console.WriteLine("Наступило лето.");
                    break;
                case Seasons.Autumn:
                    Console.WriteLine("Наступила осень.");
                    break;
                default:
                    break;
            }

            List<Product> products = new List<Product>();
            foreach (var building in Buildings)
            {
                foreach (var livestock in building.Livestocks)
                {
                    Console.WriteLine($"{livestock.Name} дал(а) {livestock.Production.Name} - {livestock.Production.Weight} центнеров.");
                    products.Add(new Product(livestock.Production.Name, livestock.Production.Weight, livestock.Production.Cost));
                }
                for (int count = 0; count < building.Livestocks.Count; count++)
                {
                    if (building.Livestocks[count].IsMultiHarvest == false)
                    {
                        building.Livestocks.RemoveAt(count);
                        count--;
                    }
                }
            }
            foreach (var gardenbed in GardenBeds)
            {
                for (int index = 0; index < gardenbed.Plants.Count; index++)
                {
                    if (gardenbed.Plants[index].HarvestSeason == season)
                    {
                        products.Add(new Product(gardenbed.Plants[index].Name, 1, gardenbed.Plants[index].Cost));
                        if (gardenbed.Plants[index].IsMultiHarvest == false)
                        {
                            Console.WriteLine($"{gardenbed.Plants[index].Name} дал(а) урожай (1 центнер) и выкопан(а) с грядки.");
                            gardenbed.Plants.RemoveAt(index);
                            index--;
                        }
                        else Console.WriteLine($"{gardenbed.Plants[index].Name} дал(а) урожай (1 центнер) и продолжает расти на грядке.");
                    }
                }
            }
            FarmWarehouse.WarehouseFill(products);
            Console.WriteLine();
        }

        //public void FarmManagement()
        //{
        //    Console.WriteLine("Выберите действие:");
        //    Console.WriteLine("1 - Добавить грядку;");
        //    Console.WriteLine("2 - Убрать грядку;");
        //    Console.WriteLine("3 - Добавить строение;");
        //    Console.WriteLine("4 - Убрать строение;");
        //    Console.WriteLine("5 - Посадить растение на грядку;");
        //    Console.WriteLine("6 - Пересадить растение на другую грядку;");
        //    Console.WriteLine("7 - Выкопать растение с грядки;");
        //    Console.WriteLine("8 - Добавить животное в строение;");
        //    Console.WriteLine("9 - Переселить животное в другое строение;");
        //    Console.WriteLine("0 - Выгнать животное из строения;");
        //    Console.WriteLine("Другое - ничего не делать.");
        //    string choise = Console.ReadLine();
        //    Console.WriteLine();

        //    switch (choise)
        //    {
        //        case "1":
        //            AddGardenBed(new GardenBed());
        //            break;
        //        case "2":
        //            Console.WriteLine($"Укажите номер убираемой грядки (всего грядок - {GardenBeds.Count}):");
        //            RemoveGardenBed(FarmMathUtilities.ConditionParse(GardenBeds.Count));
        //            break;
        //        case "3":
        //            AddBuilding(new Building());
        //            break;
        //        case "4":
        //            Console.WriteLine($"Укажите номер убираемого строения (всего строений - {Buildings.Count}):");
        //            RemoveBuilding(FarmMathUtilities.ConditionParse(Buildings.Count));
        //            break;
        //        case "5":
        //            Plant plant = new Plant();
        //            Console.Write($"Укажите номер грядки, на которую хотите посадить растение \"{plant.Name}\" (всего грядок - {GardenBeds.Count}): ");
        //            GardenBeds[FarmMathUtilities.ConditionParse(GardenBeds.Count) - 1].AddPlant(plant);
        //            break;
        //        case "6":
        //            ChangePlantGardenBed();
        //            break;
        //        case "7":
        //            Console.Write($"Укажите номер грядки, c которой хотите выкопать растение (всего грядок - {GardenBeds.Count}): ");
        //            GardenBeds[FarmMathUtilities.ConditionParse(GardenBeds.Count) - 1].RemovePlant();
        //            break;
        //        case "8":
        //            Livestock livestock = new Livestock();
        //            Console.Write($"Укажите номер строения, в которое хотите поселить животное \"{livestock.Name}\" (всего строений - {Buildings.Count}): ");
        //            Buildings[FarmMathUtilities.ConditionParse(Buildings.Count) - 1].AddLivestock(livestock);
        //            break;
        //        case "9":
        //            ChangeLivestockBuilding();
        //            break;
        //        case "0":
        //            Console.Write($"Укажите номер строения, из которого хотите выселить животное (всего строений - {Buildings.Count}): ");
        //            Buildings[FarmMathUtilities.ConditionParse(Buildings.Count) - 1].RemoveLivestock();
        //            break;
        //        default:
        //            break;
        //    }
        //}

        #region GardenBeds Methods

        public void AddGardenBed(GardenBed gardenbed)
        {
            if ((OccupiedArea + gardenbed.Area) <= Area)
            {
                GardenBeds.Add(gardenbed);
                Console.WriteLine($"{GardenBeds.Count} грядка добавлена\n");
            }
            else
            {
                Console.WriteLine($"{GardenBeds.Count + 1} грядка не добавлена, поскольку она уже не помещается на ферме \"{Name}\" (превышение максимального размера фермы на {OccupiedArea + gardenbed.Area - Area} кв.м.)\n");
            }
        }

        public void GardenBedsReport()
        {
            Console.WriteLine($"Всего грядок {GardenBeds.Count}.");
            for (int i = 0; i < GardenBeds.Count; i++)
            {
                Console.Write($"Грядка {i + 1}: ");
                GardenBeds[i].Report();
            }
            Console.WriteLine();
        }

        #endregion

        //List<Building> methods

        public void AddBuilding(Building building)
        {
            if ((OccupiedArea + building.Area) <= Area)
            {
                Buildings.Add(building);
                Console.WriteLine($"Строение \"{building.Name}\" добавлено на ферму.\n");
            }
            else
            {
                Console.WriteLine($"Строение площадью {building.Area} невозможно построить, поскольку оно не поместится на ферме (превышение максимального размера фермы на {OccupiedArea + building.Area - Area} кв.м.\n)");
            }
        }

        public void RemoveBuilding(int number)
        {
            if (number > Buildings.Count)
            {
                Console.WriteLine($"{number} строения не существует. Действие не выполнено.\n");
            }
            else
            {
                Console.WriteLine($"Строение \"{Buildings[number - 1].Name}\" убрано.\n");
                Buildings.RemoveAt(number - 1);
            }
        }

        public void BuildingsReport()
        {
            Console.WriteLine($"Всего строений {Buildings.Count}.");
            for (int i = 0; i < Buildings.Count; i++)
            {
                Console.Write($"Строение {i + 1}: ");
                Buildings[i].Report();
            }
            Console.WriteLine();
        }

        public void ChangeLivestockBuilding(int from, int to, int livestockNumber = -1)
        {
            if (from != to)
            {
                int livestockCount = Buildings[to].Livestocks.Count;
                if (livestockNumber == -1)
                {
                    livestockNumber = Buildings[from].Livestocks.Count - 1;
                }
                Buildings[to].AddLivestock(Buildings[from].Livestocks[livestockNumber]);
                if (livestockCount < Buildings[to].Livestocks.Count)
                {
                    Buildings[from].Livestocks.RemoveAt(livestockNumber);
                }
            }
            else
            {
                Console.WriteLine("Ничего не изменилось, поскольку номера указанных строений одинаковы\n");
            }
        }

        public void ChangeLivestockBuilding()
        {
            Console.Write($"Укажите номер строения, из которого хотите переселить животное (всего строений - {Buildings.Count}): ");
            int numberFrom = FarmMathUtilities.ConditionParse(Buildings.Count);
            Console.Write($"Укажите номер животного, которое хотите переселить (всего животных в строении - {Buildings[numberFrom - 1].Livestocks.Count}): ");
            int numberPlant = FarmMathUtilities.ConditionParse(Buildings[numberFrom - 1].Livestocks.Count);
            Console.Write($"Укажите номер строения, в которое хотите переселить животное \"{Buildings[numberFrom - 1].Livestocks[numberPlant - 1].Name}\" (всего строений - {GardenBeds.Count}): ");
            int numberTo = FarmMathUtilities.ConditionParse(Buildings.Count);
            ChangeLivestockBuilding(numberFrom - 1, numberTo - 1, numberPlant - 1);
        }

        #endregion

    }
}
