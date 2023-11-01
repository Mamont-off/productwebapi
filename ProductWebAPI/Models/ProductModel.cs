using System.ComponentModel.DataAnnotations.Schema;

namespace ProductWebAPI.Models;

public sealed class ProductModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Decimal Price { get; set; }
    [Column("Kol")]
    public int Count { get; set; }
    [NotMapped]
    public Decimal Sum { get; set; }
}