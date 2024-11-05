using System;

namespace ProductManager
{
    // Класс для представления обычного продукта
    public class Product
    {
        public string Name { get; set; }           // Название продукта
        public int Price { get; set; }             // Цена продукта
        public int StockQuantity { get; set; }     // Количество на складе
        public int SoldQuantity { get; set; }      // Количество проданного товара

        // Конструктор по умолчанию
        public Product()
        {
            Name = string.Empty;
            Price = 0;
            StockQuantity = 0;
            SoldQuantity = 0;
        }

        // Конструктор с параметрами
        public Product(string name, int price, int stockQuantity, int soldQuantity)
        {
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            SoldQuantity = soldQuantity;
        }

        // Конструктор копирования
        public Product(Product other)
        {
            Name = other.Name;
            Price = other.Price;
            StockQuantity = other.StockQuantity;
            SoldQuantity = other.SoldQuantity;
        }

        // Метод для вычисления максимального значения из цены, количества на складе и проданного количества
        public int MaxValue()
        {
            return Math.Max(SoldQuantity, StockQuantity);
        }

        // Переопределение метода ToString для представления продукта в виде строки
        public override string ToString()
        {
            return $"Название: {Name}, Цена = {Price}, Количество на складе = {StockQuantity}, Продано: {SoldQuantity}";
        }
    }
}
