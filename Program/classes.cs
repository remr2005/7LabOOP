// Observer для уведомления пользователей о появлении товара
// Observer для уведомления пользователей о появлении нового товара от продавца
// Observer для уведомления пользователей о появлении скидок на товар
// Шаблонный метод для показа характеристик товара
using System;

namespace Program
{
    /// <summary>
    /// Товар
    /// </summary>
    public abstract class Product : IProduct, IObservable
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; }
        /// <summary>
        /// Количество товара
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Продавец
        /// </summary>
        public IUser Seller { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Скидка
        /// </summary>
        public decimal Sale { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Вся информация о товаре
        /// </summary>
        /// <returns></returns>
        public string AllInfo()
        {
            // Шаблонный метод
            // Общий алгоритм один, а вот GetInfo будет зависеть от класса наследника
            return GetType() + $"{Title}, цена со скидкой в {Sale}%: {Price*(1 + Sale / 100)}, осталось {count}\n" +
                $"Продавец {Seller.Name}\n" +
                Description;
        }
        /// <summary>
        /// Get params
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract string GetType();
        /// <summary>
        /// Подписка на товар
        /// </summary>
        private List<IUser> users;
        /// <summary>
        /// Добавить наблюдателя за товаром
        /// </summary>
        /// <param name="o"></param>
        public void AddObserver(IUser o)
        {
            users.Add(o);
        }
        /// <summary>
        /// Уведомить наблюдателй
        /// </summary>
        public void NotifyObservers()
        {
            foreach (Buyer observer in users)
                observer.InfSubs(this);
        }
        /// <summary>
        /// Удалить наблюдателей
        /// </summary>
        /// <param name="o"></param>
        public void RemoveObserver(IUser o)
        {
            users.Remove(o);
        }
    }
    /// <summary>
    /// Класс для книги
    /// </summary>
    public class Book : Product
    {
        /// <summary>
        /// Инфа
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override string GetType()
        {
            return "Тип товара: Книга\n";
        }
    }
    /// <summary>
    /// Класс для машины
    /// </summary>
    public class Car : Product
    {
        /// <summary>
        /// Инфа
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override string GetType()
        {
            return "Тип товара: Автомобиль\n";
        }
    }
    /// <summary>
    /// Покупатель
    /// </summary>
    public class Buyer : IUser
    {
        /// <summary>
        /// Имя юзера
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// электронная почта
        /// </summary>
        public string Email { get; }
        /// <summary>
        /// заказы
        /// </summary>
        public List<IOrder> Orders { get; set; }
        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; } = 0;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="email">email</param>
        public Buyer(string name, string email)
        {
            Name = name;
            Email = email;
            Orders = new List<IOrder>();
        }
        /// <summary>
        /// Обновление баланса на число
        /// </summary>
        /// <param name="Sum">Число</param>
        public void UpdateBalance(decimal Sum)
        {
            this.Balance += Sum;
        }
        public void InfSubs(Product product)
        {
            if (product.count > 0)
                Console.WriteLine($"{product.Title} вновь в продаже");
            if (product.Sale > 0)
                Console.WriteLine($"{product.Title} имеет скидку в {product.Sale}%");
        }
    }
    /// <summary>
    /// Продавец
    /// </summary>
    public class Seller : Buyer
    {
        /// <summary>
        /// Продукты на продажу
        /// </summary>
        public List<IProduct> Products_to_Sell { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public Seller(string name, string email) : base(name, email)
        {
            Products_to_Sell = new List<IProduct>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            Products_to_Sell.Add(product);
            Console.WriteLine($"Товар '{product.Title}' добавлен продавцом {Name}.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product)
        {
            Products_to_Sell.Remove(product);
            Console.WriteLine($"Товар '{product.Title}' удален продавцом {Name}.");
        }
    }
    public class Order : IOrder
    {
        /// <summary>
        /// ID
        /// </summary>
        public int OrderId { get; }
        /// <summary>
        /// Покупатель
        /// </summary>
        public IUser Customer { get; }
        /// <summary>
        /// Товары 
        /// </summary>
        public List<Product> Products { get; }
        /// <summary>
        /// Общая сумма товаров
        /// </summary>
        public decimal TotalAmount => Products.Sum(product => product.Price);
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="customer"></param>
        public Order(IUser customer)
        {
            Random random = new Random();

            OrderId = random.Next(1000000, 9999999);
            Customer = customer;
            Products = new List<Product>();
        }
        /// <summary>
        /// Добавление продукта
        /// </summary>
        /// <param name="product"></param>
        public void AddToOrder(Product product)
        {
            Products.Add(product);
            Console.WriteLine($"Продукт '{product.Title}' добавлен в корзину к {Customer.Name}");
        }
        /// <summary>
        /// Обработка заказа
        /// </summary>
        public void ProcessOrder()
        {
            Console.WriteLine($"Выполняем заказ #{OrderId} для {Customer.Name}.");

            if (Customer.Balance >= TotalAmount)
            {
                Customer.UpdateBalance(-TotalAmount);

                foreach (var product in Products)
                {
                    product.count -= 1;
                    if (product.Seller is Seller seller)
                    {
                        seller.UpdateBalance(product.Price);
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: Продавец для товара {product.Title} не найден или не является типом Seller.");
                    }
                }

                Customer.Orders.Remove(this); // Удаление текущего заказа из списка заказов пользователя
                Console.WriteLine("Заказ выполнен.");
                return;
            }
            Console.WriteLine("Недостаточно средств. Заказ отклонён.");
        }
    }
}
