// Фабричный метод для IUSer, Product и Order
namespace Program
{
    /// <summary>
    /// Абстрактная фабрика для продуктов
    /// </summary>
    public abstract class ProductFactory
    {
        public abstract Product CreateProduct(string title, decimal price, decimal sale, IUser seller, string desc, int count);
    }

    /// <summary>
    /// Фабрика для книг
    /// </summary>
    public class BookFactory : ProductFactory
    {
        public override Product CreateProduct(string title, decimal price, decimal sale, IUser seller, string desc, int count)
        {
            return new Book
            {
                Title = title,
                Price = price,
                Sale = sale,
                Seller = seller,
                Description = desc,
                count = count // по умолчанию
            };
        }
    }

    /// <summary>
    /// Фабрика для автомобилей
    /// </summary>
    public class CarFactory : ProductFactory
    {
        public override Product CreateProduct(string title, decimal price, decimal sale, IUser seller, string desc, int count)
        {
            return new Car
            {
                Title = title,
                Price = price,
                Sale = sale,
                Seller = seller,
                Description = desc,
                count = count // по умолчанию
            };
        }
    }
    /// <summary>
    /// Абстрактная фабрика для пользователей
    /// </summary>
    public abstract class UserFactory
    {
        public abstract IUser CreateUser(string name, string email);
    }

    /// <summary>
    /// Фабрика для покупателей
    /// </summary>
    public class BuyerFactory : UserFactory
    {
        public override IUser CreateUser(string name, string email)
        {
            return new Buyer(name, email);
        }
    }

    /// <summary>
    /// Фабрика для продавцов
    /// </summary>
    public class SellerFactory : UserFactory
    {
        public override IUser CreateUser(string name, string email)
        {
            return new Seller(name, email);
        }
    }
    /// <summary>
    /// Абстрактная фабрика для заказов
    /// </summary>
    public abstract class OrderFactory
    {
        public abstract IOrder CreateOrder(IUser customer);
    }

    /// <summary>
    /// Конкретная фабрика для создания заказов
    /// </summary>
    public class ConcreteOrderFactory : OrderFactory
    {
        public override IOrder CreateOrder(IUser customer)
        {
            return new Order(customer);
        }
    }

}
