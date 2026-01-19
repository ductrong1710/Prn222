using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace WasteCollectionPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================
            // ADD SERVICES
            // =========================

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default")
                ));

            // Unit Of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business Service
            builder.Services.AddScoped<IMessageService, MessageService>();

            // Firebase (demo)
            builder.Services.AddSingleton<FirebaseService>();

            // SignalR
            builder.Services.AddSignalR();

            var app = builder.Build();

            // =========================
            // MIDDLEWARE
            // =========================

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // =========================
            // MAP ENDPOINTS
            // =========================
            app.UseStaticFiles();

            app.MapControllers();
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
