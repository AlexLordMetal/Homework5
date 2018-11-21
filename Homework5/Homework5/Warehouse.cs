using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    class Warehouse
    {
        public int Capacity { get; set; }
        public List<Product> Products { get; set; }

        public Warehouse(int capacity = 1000)
        {
            Capacity = capacity;
            Products = new List<Product>();
        }

        public int OccupiedCapacity
        {
            get
            {
                int occupiedCapacity = 0;
                foreach (var product in Products)
                {
                    occupiedCapacity += product.Weight;
                }
                return occupiedCapacity;
            }
        }


        //Methods

        public void WarehouseFill(List<Product> newProducts)
        {
            foreach (var newProduct in newProducts)
            {
                if ((OccupiedCapacity + newProduct.Weight) <= Capacity)
                {
                    int productIndex = IndexOfProduct(Products, newProduct);
                    if (productIndex == -1)
                    {
                        Products.Add(new Product(newProduct.Name, newProduct.Weight));      //Здесь юзается костыль
                    }
                    else
                    {
                        Products[productIndex].Weight += newProduct.Weight;
                    }
                }
                else
                {
                    Console.WriteLine($"Продукт {newProduct.Name} не добавлен, поскольку он уже не помещается на склад");
                }
            }
        }

        public void Report()
        {
            Console.WriteLine($"Склад имеет общую вместимость {Capacity} килограмм. Заполнен на {FarmMathUtilities.OccupiedPercent(OccupiedCapacity, Capacity)}%.");
            if (OccupiedCapacity == 0)
            {
                Console.WriteLine("На складе пусто.");
            }
            else
            {
                Console.WriteLine("На складе хранится:");
                foreach (var product in Products)
                {
                    Console.WriteLine($"{product.Name} - {product.Weight} килограмм.");
                }
            }
            Console.WriteLine();
        }


        //Additional method

        private int IndexOfProduct(List<Product> products, Product newProduct)
        {
            int productIndex = -1;
            for (var productCount = 0; productCount < products.Count; productCount++)
            {
                if (products[productCount].Name == newProduct.Name)
                {
                    productIndex = productCount;
                    break;
                }
            }
            return productIndex;
        }

    }
}
