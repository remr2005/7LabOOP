using System;

namespace Program
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            // Создание фабрик
            var buyerFactory = new BuyerFactory();
            var sellerFactory = new SellerFactory();
            var bookFactory = new BookFactory();
            var carFactory = new CarFactory();
            var orderFactory = new ConcreteOrderFactory();

            // Создание пользователей
            IUser buyer1 = buyerFactory.CreateUser("Иван Иванов", "ivanov@example.com");
            IUser buyer2 = buyerFactory.CreateUser("Мария Петрова", "petrova@example.com");
            IUser seller = sellerFactory.CreateUser("Анна Смирнова", "anna@example.com");

            // Увеличение баланса покупателей
            buyer1.UpdateBalance(50000);
            buyer2.UpdateBalance(10000);
            Console.WriteLine($"Баланс {buyer1.Name}: {buyer1.Balance} руб.");
            Console.WriteLine($"Баланс {buyer2.Name}: {buyer2.Balance} руб.");

            // Создание товаров
            Product book = bookFactory.CreateProduct(
                title: "Программирование на C#",
                price: 1000,
                sale: 0, // изначально без скидки
                seller: seller,
                desc: "Отличная книга для изучения C#.",
                count: 0 // товар изначально отсутствует
            );

            Product car = carFactory.CreateProduct(
                title: "Toyota Corolla",
                price: 30000,
                sale: 5,
                seller: seller,
                desc: "Надежный автомобиль для города.",
                count: 5
            );

            // Добавление наблюдателей
            book.AddObserverCount(buyer1);
            book.AddObserverSale(buyer2);

            // Добавление товаров продавцом
            if (seller is Seller concreteSeller)
            {
                concreteSeller.AddProduct(book);
                concreteSeller.AddProduct(car);
            }

            // Пополнение количества книги (уведомляет подписчиков)
            Console.WriteLine("\nДобавляем книгу на склад...");
            book.count = 10;
            book.NotifyObserversCount();

            // Установка скидки на книгу (уведомляет подписчиков)
            Console.WriteLine("\nДобавляем скидку на книгу...");
            book.Sale = 20;
            book.NotifyObserversSale();

            // Создание заказа покупателем
            IOrder order = orderFactory.CreateOrder(buyer1);

            // Добавление товаров в заказ
            if (order is Order concreteOrder)
            {
                concreteOrder.AddToOrder(book);
                concreteOrder.AddToOrder(car);

                // Попытка выполнить заказ
                Console.WriteLine("\nОбработка заказа...");
                concreteOrder.ProcessOrder();
            }

            Console.WriteLine($"\nБаланс покупателя {buyer1.Name} после покупки: {buyer1.Balance} руб.");
            Console.WriteLine($"Баланс продавца {seller.Name} после продажи: {seller.Balance} руб.");
        }
    }
}
