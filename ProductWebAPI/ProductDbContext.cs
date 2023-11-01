using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI;

public sealed class ProductDbContext : DbContext
{
    //TODO: move to env
    private const string Host = "postgres";
    private const string Port = "5432";
    private const string DatabaseName = "ProductDB";
    private const string Username = "postgres";
    private const string Password = "postgres";
    //
    public DbSet<Nomenclature> Nomenclature { get; set; }
    public DbSet<Links> Links { get; set; }
    public DbSet<ProductMetaData> ProductMetaData { get; set; }
    public DbSet<ProductModel> ProductModel { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql($"Host={Host};Port={Port};Database={DatabaseName};Username={Username};Password={Password}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>(model =>
        {
            model.HasNoKey();
        });
    }
}