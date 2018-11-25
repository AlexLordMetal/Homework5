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

        #region Farm methods

        /// <summary>
        /// Writes report of farm to console
        /// </summary>
        public void Report()
        {
            Console.WriteLine($"Ферма \"{Name}\" площадью {Area} кв.м. с {GardenBeds.Count} грядками и {Buildings.Count} строениями. Всего занято {OccupiedArea} кв.м. ({FarmMathUtilities.OccupiedPercent(OccupiedArea, Area)}% площади).\n");
        }

        /// <summary>
        /// Harvests products according to season
        /// </summary>
        /// <param name="season"></param>
        public void Harvest(Seasons season)
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
                for (int index = 0; index < building.Livestocks.Count; index++)
                {
                    building.Livestocks[index].HarvestTime++;
                    if (building.Livestocks[index].HarvestTime == building.Livestocks[index].TimeBetweenHarvests)
                    {
                        building.Livestocks[index].HarvestTime = 0;
                        products.Add(new Product(building.Livestocks[index].Production.Name, building.Livestocks[index].Production.Weight, building.Livestocks[index].Production.Cost));
                        if (building.Livestocks[index].IsMultiHarvest == false)
                        {
                            Console.WriteLine($"{building.Livestocks[index].Name} дал(а) {building.Livestocks[index].Production.Name} - {building.Livestocks[index].Production.Weight} центнеров и пал(а).");
                            building.Livestocks.RemoveAt(index);
                            index--;
                        }
                        else Console.WriteLine($"{building.Livestocks[index].Name} дал(а) {building.Livestocks[index].Production.Name} - {building.Livestocks[index].Production.Weight} центнеров и продолжает жить в здании {building.Name}.");
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

        #endregion

        #region GardenBeds methods

        /// <summary>
        /// Adds gardenbed with overflow conditions
        /// </summary>
        /// <param name="gardenbed"></param>
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

        /// <summary>
        /// Writes report of farm's gardenbeds to console
        /// </summary>
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

        #region Buildings methods

        /// <summary>
        /// Adds building with overflow conditions
        /// </summary>
        /// <param name="building"></param>
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

        /// <summary>
        /// Writes report of farm's buildings to console
        /// </summary>
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

        /// <summary>
        /// Removes livestock from one chosen building and adds it to another chosen building
        /// </summary>
        public void ChangeLivestockBuilding()
        {
            if (Buildings.Count > 1)
            {
                Console.Write($"Укажите номер строения, из которого хотите переселить животное (всего строений - {Buildings.Count}): ");
                int indexFromBuilding = FarmMathUtilities.ConditionParse(Buildings.Count) - 1;
                Buildings[indexFromBuilding].ReportOnlyLivestocks();
                if (Buildings[indexFromBuilding].Livestocks.Count > 0)
                {
                    Console.Write($"Укажите номер животного, которое хотите переселить из строения {Buildings[indexFromBuilding].Name}): ");
                    int livestockNumber = FarmMathUtilities.ConditionParse(Buildings[indexFromBuilding].Livestocks.Count) - 1;
                    Console.Write($"Укажите номер строения, в которое хотите переселить животное \"{Buildings[indexFromBuilding].Livestocks[livestockNumber].Name}\" (всего строений - {Buildings.Count}): ");
                    int indexToBuilding = FarmMathUtilities.ConditionParse(Buildings.Count) - 1;
                    if (indexFromBuilding != indexToBuilding)
                    {
                        int livestockCount = Buildings[indexToBuilding].Livestocks.Count;
                        Buildings[indexToBuilding].AddLivestock(Buildings[indexFromBuilding].Livestocks[livestockNumber]);
                        if (livestockCount < Buildings[indexToBuilding].Livestocks.Count)
                        {
                            Buildings[indexFromBuilding].Livestocks.RemoveAt(livestockNumber);
                        }
                    }
                    else Console.WriteLine("Действие не выполнено, поскольку номера указанных строений одинаковы\n");
                }
            }
            else if (Buildings.Count > 0) Console.WriteLine("Переселение невозможно, так как на ферме всего одно строение.\n");
            else Console.WriteLine("На ферме нет ни одного строения.\n");
        }

        #endregion

    }
}
