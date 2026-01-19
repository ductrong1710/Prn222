using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork;
using BusinessLogicLayer.Services;
using Presentation.Hubs;

namespace WasteCollectionPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21))));

            // SignalR
            builder.Services.AddSignalR();

            // Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business Services
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddSingleton<IFirebaseService, FirebaseService>();

            // CORS (for SignalR clients and web pages)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.SetIsOriginAllowed(_ => true)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Serve static files (wwwroot folder)
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors();
            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}
