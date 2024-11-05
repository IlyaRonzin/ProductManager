using System;
using System.Collections.Generic;
using ProductManager;

public class Program
{
    // Список для хранения всех продуктов, включая как обычные, так и скоропортящиеся
    private static List<Product> allProducts = new List<Product>();

    public static void Main(string[] args)
    {
        // Основной цикл программы, который предоставляет пользователю меню выбора действий
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в систему управления продуктами!");
            Console.WriteLine("1. Создать продукт");
            Console.WriteLine("2. Создать скоропортящийся продукт");
            Console.WriteLine("3. Создать скоропортящийся продукт на основе существующего продукта");
            Console.WriteLine("4. Показать все продукты");
            Console.WriteLine("5. Уменьшить срок годности у скоропортящегося продукта");
            Console.WriteLine("6. Вычислить \"больше продано или на складе\" для продукта");
            Console.WriteLine("7. Выйти");
            Console.Write("Выберите действие: ");

            // Считывание выбора пользователя
            string choice = Console.ReadLine();

            // Выполнение действия в зависимости от выбора пользователя
            switch (choice)
            {
                case "1":
                    CreateProduct();
                    break;
                case "2":
                    CreatePerishableProduct();
                    break;
                case "3":
                    CreatePerishableProductFromProduct();
                    break;
                case "4":
                    ShowAllProducts();
                    break;
                case "5":
                    DecreaseExpirationDaysForPerishableProduct();
                    break;
                case "6":
                    CalculateMaxValue();
                    break;
                case "7":
                    Console.WriteLine("Выход...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    // Метод для создания обычного продукта
    public static void CreateProduct()
    {
        Console.WriteLine("\nСоздание продукта:");

        string name = ReadString("Введите название продукта: ");
        int price = ReadInt("Введите цену продукта: ");
        int stockQuantity = ReadInt("Введите количество на складе: ");
        int soldQuantity = ReadInt("Введите количество проданного товара: ");

        // Создание и добавление продукта в список
        Product product = new Product(name, price, stockQuantity, soldQuantity);
        allProducts.Add(product);

        Console.WriteLine("\nПродукт создан:");
        Console.WriteLine(product);
    }

    // Метод для создания нового скоропортящегося продукта
    public static void CreatePerishableProduct()
    {
        Console.WriteLine("\nСоздание скоропортящегося продукта:");

        string name = ReadString("Введите название продукта: ");
        int price = ReadInt("Введите цену продукта: ");
        int stockQuantity = ReadInt("Введите количество на складе: ");
        int soldQuantity = ReadInt("Введите количество проданного товара: ");
        int daysUntilExpiration = ReadInt("Введите количество дней до истечения срока годности: ");

        // Создание и добавление скоропортящегося продукта в список
        PerishableProduct perishableProduct = new PerishableProduct(name, price, stockQuantity, soldQuantity, daysUntilExpiration);
        allProducts.Add(perishableProduct);

        Console.WriteLine("\nСкоропортящийся продукт создан:");
        Console.WriteLine(perishableProduct);
    }

    // Метод для создания скоропортящегося продукта на основе обычного продукта
    public static void CreatePerishableProductFromProduct()
    {
        // Проверка наличия продуктов
        if (allProducts.Count == 0)
        {
            Console.WriteLine("Нет доступных продуктов для создания на их основе.");
            return;
        }

        Console.WriteLine("\nВыберите продукт для создания скоропортящегося продукта на его основе:");
        ShowAllProducts();

        int index = ReadInt("Введите номер продукта: ") - 1;

        if (index < 0 || index >= allProducts.Count)
        {
            Console.WriteLine("Неверный выбор продукта.");
            return;
        }

        Product selectedProduct = allProducts[index];
        int daysUntilExpiration = ReadInt("Введите количество дней до истечения срока годности: ");

        // Создание нового скоропортящегося продукта и удаление исходного обычного продукта
        PerishableProduct perishableProduct = new PerishableProduct(selectedProduct, daysUntilExpiration);
        allProducts.Remove(selectedProduct);
        allProducts.Add(perishableProduct);

        Console.WriteLine("\nСкоропортящийся продукт на основе продукта создан:");
        Console.WriteLine(perishableProduct);
    }

    // Метод для отображения всех продуктов
    public static void ShowAllProducts()
    {
        if (allProducts.Count == 0)
        {
            Console.WriteLine("Нет доступных продуктов.");
            return;
        }

        Console.WriteLine("\nСписок всех продуктов:");
        for (int i = 0; i < allProducts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {allProducts[i]}");
        }
    }

    // Метод для уменьшения срока годности у скоропортящегося продукта
    public static void DecreaseExpirationDaysForPerishableProduct()
    {
        // Получение только скоропортящихся продуктов из списка
        List<PerishableProduct> perishableProducts = allProducts.FindAll(p => p is PerishableProduct).ConvertAll(p => (PerishableProduct)p);

        if (perishableProducts.Count == 0)
        {
            Console.WriteLine("Нет доступных скоропортящихся продуктов.");
            return;
        }

        Console.WriteLine("\nВыберите скоропортящийся продукт для уменьшения срока годности:");
        for (int i = 0; i < perishableProducts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {perishableProducts[i]}");
        }

        int index = ReadInt("Введите номер скоропортящегося продукта: ") - 1;

        if (index < 0 || index >= perishableProducts.Count)
        {
            Console.WriteLine("Неверный выбор продукта.");
            return;
        }

        int daysToDecrease = ReadInt("Введите количество дней для уменьшения срока годности: ");
        perishableProducts[index].DecreaseExpirationDays(daysToDecrease);

        Console.WriteLine("\nСрок годности обновлен.");
        Console.WriteLine(perishableProducts[index]);
    }

    // Метод для вычисления максимального значения (цены, количества на складе или проданного) для выбранного продукта
    public static void CalculateMaxValue()
    {
        if (allProducts.Count == 0)
        {
            Console.WriteLine("Нет доступных продуктов.");
            return;
        }

        Console.WriteLine("\nВыберите продукт для вычисления MaxValue:");
        ShowAllProducts();

        int index = ReadInt("Введите номер продукта: ") - 1;

        if (index < 0 || index >= allProducts.Count)
        {
            Console.WriteLine("Неверный выбор продукта.");
            return;
        }

        int maxValue = allProducts[index].MaxValue();
        Console.WriteLine($"\nМаксимальное значение для выбранного продукта: {maxValue}");
    }

    // Метод для безопасного считывания целочисленного значения с проверкой
    private static int ReadInt(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out result))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректное число.");
            }
        }
        return result;
    }

    // Метод для считывания строки с экрана
    private static string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}
