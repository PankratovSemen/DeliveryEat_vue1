using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeliveryEat_vue1.Server.DataBase;

namespace DeliveryEat_vue1.Server.DataBase.Contexts
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<RolesToUsers> RolesToUSR { get; set; } = null!;
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
