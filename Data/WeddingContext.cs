using Microsoft.EntityFrameworkCore;
using Wedding.Data.Models;

namespace Wedding.Data;

public class WeddingContext : DbContext
{
    public DbSet<Invitation> Invitations { get; set; } = null!;
    public DbSet<Attendee> Attendees { get; set; } = null!;
    public DbSet<CovidUpdate> CovidUpdates { get; set; } = null!;

    private static readonly string _db;

    static WeddingContext()
    {
        const string name = "wedding.db";
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        _db = Path.Join(path, name);        
    }

    public static async Task Migrate()
    {
        using var context = new WeddingContext();
        await context.Database.EnsureCreatedAsync();
        var migrations = await context.Database.GetPendingMigrationsAsync();
        if (migrations.Any())
        {
            await context.Database.MigrateAsync();
        }
    }

    public static async Task Seed(IHostEnvironment env, IConfiguration config)
    {
        using var context = new WeddingContext();
        if (!await context.Invitations.AnyAsync())
        {
            var seeding = new Seeding(config);
            context.Invitations.AddRange(seeding.Seeds);
            await context.SaveChangesAsync();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_db}");
}
