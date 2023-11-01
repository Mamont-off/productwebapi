using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Models;

[Index(nameof(Id))]
public sealed class Links
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int NomenclatureId { get; set; }
    public int ParentId { get; set; }
    [Column("Kol")]
    public int Count { get; set; }
}