using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //3-json format
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(
             options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 options.JsonSerializerOptions.ReferenceHandler =
                 System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                 options.JsonSerializerOptions.DefaultIgnoreCondition =
                 System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                 options.JsonSerializerOptions.WriteIndented = true;
             });
            //connection string as Middleware

            builder.Services.AddDbContext<ClinicManagementSysContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug24Connection")));

            //2- Register Repository and service layer

            builder.Services.AddScoped<ILabtestListRepository, LabtestListRepository>();
            builder.Services.AddScoped<IPharmacistRepository,PharmacistRepository>();


            //Register Swagger
            builder.Services.AddSwaggerGen();

            //Before app --  setting all middleware
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //add this
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
