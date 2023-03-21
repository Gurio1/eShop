using Catalog.API.Persistence;
using Catalog.API.Profiles;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API;

public static class ProgramExtensions
{
    public static void AddCustomSqlServer(this IServiceCollection serviceCollection,IConfiguration config)
    {
        serviceCollection.AddDbContext<CatalogContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        serviceCollection.AddScoped<SeedDB>();
        
        
    }

    public static void AddCustomDevCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                policyBuilder => policyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
    public static void AddCustomAutomaper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(CatalogItemProfile).Assembly);
    }
    
}