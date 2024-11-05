using System;

namespace ProductManager
{
    // Класс для представления скоропортящегося продукта, наследуется от Product
    public class PerishableProduct : Product
    {
        public int DaysUntilExpiration { get; set; }  // Количество дней до истечения срока годности

        // Конструктор по умолчанию
        public PerishableProduct() : base()
        {
            DaysUntilExpiration = 0;
        }

        // Конструктор для создания скоропортящегося продукта на основе обычного продукта
        public PerishableProduct(Product other, int daysUntilExpiration) : base(other)
        {
            DaysUntilExpiration = daysUntilExpiration;
        }

        // Конструктор с параметрами
        public PerishableProduct(string name, int price, int stockQuantity, int soldQuantity, int daysUntilExpiration)
            : base(name, price, stockQuantity, soldQuantity)
        {
            DaysUntilExpiration = daysUntilExpiration;
        }

        // Метод для проверки, истек ли срок годности продукта
        public bool IsExpired()
        {
            return DaysUntilExpiration <= 0;
        }

        // Метод для уменьшения количества дней до истечения срока годности
        public void DecreaseExpirationDays(int days)
        {
            DaysUntilExpiration -= days;
            if (DaysUntilExpiration <= 0)
                DaysUntilExpiration = 0;
        }

        // Переопределение метода ToString для представления скоропортящегося продукта в виде строки
        public override string ToString()
        {
            return base.ToString() + $", Дней до истечения срока годности: {DaysUntilExpiration}, Срок годности истек: {IsExpired()}";
        }
    }
}
