using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using DeliveryEat_vue1.Server.DataBase.Basket;

namespace DeliveryEat_vue1.Server.DataBase.Contexts
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<BasketData> Baskets { get; set; } = null!;
        public DbSet<Sdata> Sessions { get; set; } = null!;
        public DbSet<BasketItem> BasketItem { get; set; } = null!;
        public DbSet<UserBasket> UserBasket { get; set; } = null!;
        public DbSet<PayData> Pay { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<AddressToUser> AddressToUser { get; set; } = null!;
        public DbSet<Order> Order { get; set; } = null!;
        public DbSet<Guest> Guest { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<RolesToUsers> RolesToUSR { get; set; } = null!;
        public DbSet<Delivery> Delivery { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        

    }
}
