using Microsoft.AspNetCore.Identity;
using SchoolProject.Api.Middelware;
using SchoolProject.Application;
using SchoolProject.Infrastruture;
using SchoolProject.Infrastruture.Data.Seeders;
using Serilog;



namespace SchoolProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureDependencies(builder.Configuration).AddApplicationDependencies();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Information()
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();
            app.UseExceptionHandler();



            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                IdentitySeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
