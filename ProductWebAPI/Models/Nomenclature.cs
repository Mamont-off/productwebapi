using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Models;

[Index(nameof(Id))]
public sealed class Nomenclature
{
    //[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public Decimal Price { get; set; }
}