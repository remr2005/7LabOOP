using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем продавца
            var seller = new Seller("Иван Иванов", "ivan@example.com");

            // Создаем товары
            var book = new Book
            {
                ID = 1,
                Title = "Мастер и Маргарита",
                Description = "Роман Михаила Булгакова.",
                Price = 500,
                Sale = 10,
                count = 3,
                Seller = seller
            };

            var car = new Car
            {
                ID = 2,
                Title = "Lada Granta",
                Description = "Новая модель 2024 года.",
                Price = 700000,
                Sale = 5,
                count = 1,
                Seller = seller
            };

            // Продавец добавляет товары на продажу
            seller.AddProduct(book);
            seller.AddProduct(car);

            // Создаем покупателей
            var buyer1 = new Buyer("Алексей", "alex@example.com");
            var buyer2 = new Buyer("Мария", "maria@example.com");

            // Покупатели подписываются на уведомления
            book.AddObserverCount(buyer1);
            book.AddObserverSale(buyer2);

            car.AddObserverCount(buyer2);
            car.AddObserverSale(buyer1);

            // Покупатель оформляет заказ
            var order1 = new Order(buyer1);
            buyer1.Orders.Add(order1);

            order1.AddToOrder(book);
            order1.AddToOrder(car);

            // Покупатель пополняет баланс
            buyer1.UpdateBalance(1000000);

            // Обрабатываем заказ
            order1.ProcessOrder();

            // Уменьшаем количество товара вручную и уведомляем наблюдателей
            book.count -= 1;
            book.NotifyObserversCount();

            car.Sale = 15; // Изменение скидки
            car.NotifyObserversSale();

            // Вывод информации о товарах
            Console.WriteLine(book.AllInfo());
            Console.WriteLine(car.AllInfo());
        }
    }
}
