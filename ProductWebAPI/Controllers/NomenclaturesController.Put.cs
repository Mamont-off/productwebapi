using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers;

public partial class NomenclaturesController
{
    [HttpPut("id")]
    public async Task<IActionResult> PutProduct(int id, Nomenclature updateNomenclature)
    {
        if (id != updateNomenclature.Id)
        {
            return BadRequest();
        }

        await using (var db = new ProductDbContext())
        {
            db.Entry(updateNomenclature).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        return Ok();
    }
}