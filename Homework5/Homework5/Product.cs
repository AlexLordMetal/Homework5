namespace Homework5
{
    class Product
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public Product(string name = "Default", int weight = 1)
        {
            Name = name;
            Weight = weight;
        }
    }
}
