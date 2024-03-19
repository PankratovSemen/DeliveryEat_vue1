using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
namespace DeliveryEat_vue1.Server.DataBase.Contexts
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<BasketData> Baskets { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        

    }
}
