using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelBooking.Infrastructure.DataSeeding
{
    internal class HotelDataSeeder(AppDbContext dbContext, ILogger<HotelDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigration.Any())
                    await dbContext.Database.MigrateAsync(ct);

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                var path = Path.Combine(seedRoot, "hotels.json");

                await SeedIfEmptyAsync<Hotel, int>(seedRoot, "hotels.json", ct);
                int result = await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                    logger.LogInformation("Data seeding completed successfully.");
                else
                    logger.LogInformation("No new data was seeded.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex}");
            }
         
        }
        private async Task SeedIfEmptyAsync<TEntity, TKey>(string rootPath,
            string fileName,
            CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {
            if(await dbContext.Set<TEntity>().AnyAsync())
                return;
            var filePath = Path.Combine(rootPath, fileName);
            if(!File.Exists(filePath))
            {
                logger.LogWarning($"File {filePath} does not exist");
                return;
            }

            using var fileStream = File.OpenRead(filePath);
            var items = await JsonSerializer.DeserializeAsync<List<TEntity>>(fileStream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                } , ct);
            if(items?.Any() ?? false)
               await dbContext.AddRangeAsync(items);
        }
    }
}
