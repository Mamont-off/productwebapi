using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Controllers;

public partial class NomenclaturesController
{
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await using (var db = new ProductDbContext())
        {
            var result = await db.Nomenclature.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            db.Nomenclature.Remove(result);
            await db.SaveChangesAsync();
        }

        return Ok();
    }

    [HttpDelete("link/{parentId},{productId}")]
    public async Task<IActionResult> DeleteLink(int parentId, int productId)
    {
        await using (var db = new ProductDbContext())
        {
            var result = db.Links.Where(lnk => (lnk.ParentId == parentId && lnk.NomenclatureId == productId));
            if (!result.Any())
            {
                return NotFound();
            }

            db.Links.Remove(await result.FirstAsync());
            await db.SaveChangesAsync();
        }

        return Ok();
    }
}