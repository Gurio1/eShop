using Catalog.API.Persistence;
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
    }
}