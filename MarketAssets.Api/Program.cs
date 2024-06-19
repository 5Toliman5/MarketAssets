using MarketAssets.Api.Mapping;
using MarketAssets.DataAccess.EF;
using MarketAssets.Domain.Models;
using MarketAssets.Domain.Repositories;
using MarketAssets.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MarketAssets.Fintacharts.Authentication.Abstract;
using MarketAssets.Fintacharts.Authentication.Services;
using MarketAssets.Fintacharts.RealTime.Services;
using MarketAssets.Fintacharts.Historical.Services;
using MarketAssets.Api.Middleware;
using MarketAssets.Domain.Fintacharts;
using MarketAssets.Domain.Fintacharts.Models.RealTime;

namespace MarketAssets.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(AssetProfile));
            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<AssetsDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IDataAccessRepository<MarketAsset>, MarketAssetsRepository>();
            builder.Services.AddScoped<IDataAccessRepository<AssetProvider>, AssetProvidersRepository>();

            builder.Services.Configure<FintachartsRestApiCredentials>(builder.Configuration.GetSection("Fintacharts:RestApi"));
            builder.Services.Configure<FintachartsWSApiCredentials>(builder.Configuration.GetSection("Fintacharts:WSApi"));

            builder.Services.AddTransient<IFintachartsAuthenticator, FintachartsAuthenticator>();
            builder.Services.AddTransient<IFintachartsRealTimeService, FintachartsRealTimeService>();
            builder.Services.AddTransient<IFintachartsHistoricalService, FintachartsHistoricalService>();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<ErrorHandler>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
