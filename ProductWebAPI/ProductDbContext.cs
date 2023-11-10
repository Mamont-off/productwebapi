using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI;

public sealed class ProductDbContext : DbContext
{
    public DbSet<Nomenclature> Nomenclature { get; set; }
    public DbSet<Links> Links { get; set; }
    public DbSet<ProductMetaData> ProductMetaData { get; set; }
    public DbSet<ProductModel> ProductModel { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var con = new DbPostgresConnect();
        optionsBuilder.UseNpgsql(con.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>(model =>
        {
            model.HasNoKey();
        });
    }
}