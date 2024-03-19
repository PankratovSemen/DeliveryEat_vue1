using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeliveryEat_vue1.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<UserContext>(options =>
           options.UseSqlServer(connection));
            services.AddMvc(); 
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
           

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                    .WithOrigins("https://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader();

            });
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSession();
            app.UseHttpsRedirection();

            

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            

            app.Run();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
