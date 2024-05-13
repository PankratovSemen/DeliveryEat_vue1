using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DeliveryEat_vue1.Server.Model;
using System;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

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
            
            services.AddMvc(); 
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddDistributedMemoryCache();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           // óêçûâàåò, áóäåò ëè âàëèäèðîâàòüñÿ èçäàòåëü ïðè âàëèäàöèè òîêåíà
                           ValidateIssuer = true,
                           // ñòðîêà, ïðåäñòàâëÿþùàÿ èçäàòåëÿ
                           ValidIssuer = AuthOptions.ISSUER,

                           // áóäåò ëè âàëèäèðîâàòüñÿ ïîòðåáèòåëü òîêåíà
                           ValidateAudience = true,
                           // óñòàíîâêà ïîòðåáèòåëÿ òîêåíà
                           ValidAudience = AuthOptions.AUDIENCE,
                           // áóäåò ëè âàëèäèðîâàòüñÿ âðåìÿ ñóùåñòâîâàíèÿ
                           ValidateLifetime = true,

                           // óñòàíîâêà êëþ÷à áåçîïàñíîñòè
                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                           // âàëèäàöèÿ êëþ÷à áåçîïàñíîñòè
                           ValidateIssuerSigningKey = true,
                       };
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
           
            app.UseHttpsRedirection();

            

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            

            app.Run();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
