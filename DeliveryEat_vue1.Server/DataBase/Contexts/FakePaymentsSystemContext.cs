
using DeliveryEat_vue1.Server.DataBase.Basket;
using DeliveryEat_vue1.Server.DataBase.FakePaymentsSystem;
using Microsoft.EntityFrameworkCore;

namespace DeliveryEat_vue1.Server.DataBase.Contexts
{
    public class FakePaymentsSystemContext:DbContext
    {
        public DbSet<ClientBill> Client { get; set; } = null!;
        public DbSet<TransactionsHistory> History { get; set; } = null!;
        public FakePaymentsSystemContext(DbContextOptions<FakePaymentsSystemContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
