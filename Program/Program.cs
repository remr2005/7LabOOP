using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем продавца
            Seller seller = new Seller("Иван", "ivan@example.com");

            // Создаем товары
            Book book = new Book
            {
                Title = "Война и Мир",
                Price = 500m,
                Description = "Классика русской литературы",
                Seller = seller,
                count = 10
            };

            Car car = new Car
            {
                Title = "Toyota Camry",
                Price = 2000000m,
                Description = "Надежный и комфортный автомобиль",
                Seller = seller,
                count = 1
            };

            // Продавец добавляет товары
            seller.AddProduct(book);
            seller.AddProduct(car);

            // Покупатель
            Buyer buyer = new Buyer("Петр", "petr@example.com");
            buyer.UpdateBalance(3000000m); // Пополняем баланс покупателя

            // Создаем заказ
            Order order = new Order(buyer);
            buyer.Orders.Add(order); // Добавляем заказ покупателю

            // Покупатель добавляет товары в заказ
            order.AddToOrder(book);
            order.AddToOrder(car);

            // Вывод информации о товаре
            Console.WriteLine(book.AllInfo());
            Console.WriteLine(car.AllInfo());

            // Обработка заказа
            order.ProcessOrder();

            // Проверка остатка у покупателя
            Console.WriteLine($"Баланс покупателя {buyer.Name}: {buyer.Balance} руб.");
            // Проверка остатка у продавца
            Console.WriteLine($"Баланс продавца {seller.Name}: {seller.Balance} руб.");
        }
    }
}
