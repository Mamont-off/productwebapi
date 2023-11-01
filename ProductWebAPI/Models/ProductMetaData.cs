using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Models;

[Index(nameof(Id))]
public sealed class ProductMetaData
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int NomenclatureId { get; set; }
    [Column("MetaData")]
    public string[]? Data { get; set; }
}