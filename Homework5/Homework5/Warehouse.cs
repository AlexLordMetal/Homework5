using System;
using System.Collections.Generic;

namespace Homework5
{
    public class Warehouse
    {
        public int Capacity { get; set; }
        public List<Product> Products { get; set; }
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

        public Warehouse(int capacity = 100)
        {
            Capacity = capacity;
            Products = new List<Product>();
        }

        #region Methods

        /// <summary>
        /// Writes report of warehouse to console
        /// </summary>
        public void Report()
        {
            Console.WriteLine($"Склад имеет общую вместимость {Capacity} центнеров. Заполнен на {FarmMathUtilities.OccupiedPercent(OccupiedCapacity, Capacity)}%.");
            if (OccupiedCapacity == 0)
            {
                Console.WriteLine("На складе пусто.");
            }
            else
            {
                Console.WriteLine("На складе хранится:");
                foreach (var product in Products)
                {
                    Console.WriteLine($"{Products.IndexOf(product) + 1}. {product.Name} - {product.Weight} центнеров (Стоимость центнера - {product.Cost} монет)");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Adds list of products in warehouse
        /// </summary>
        /// <param name="newProducts"></param>
        public void WarehouseFill(List<Product> newProducts)
        {
            foreach (var newProduct in newProducts)
            {
                if ((OccupiedCapacity + newProduct.Weight) <= Capacity)
                {
                    int productIndex = IndexOfProduct(Products, newProduct);
                    if (productIndex == -1)
                    {
                        Products.Add(new Product(newProduct.Name, newProduct.Weight, newProduct.Cost));
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

        /// <summary>
        /// Finds index of product in list of products, if it's name is equal to the name of new product, else returns -1
        /// </summary>
        /// <param name="products"></param>
        /// <param name="newProduct"></param>
        /// <returns></returns>
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

        #endregion

    }
}