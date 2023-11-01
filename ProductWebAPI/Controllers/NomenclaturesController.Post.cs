using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers;

public partial class NomenclaturesController
{
    [HttpPost]
    public async Task<ActionResult<Nomenclature>> CreateProduct(Nomenclature newProduct)
    {
        await using (var db = new ProductDbContext())
        {
            db.Nomenclature.Add(newProduct);
            await db.SaveChangesAsync();
        }

        return CreatedAtAction(nameof(CreateProduct), newProduct);
    }
    
    [HttpPost("link")]
    public async Task<ActionResult<Links>> CreateProductLink(Links newLink)
    {
        await using (var db = new ProductDbContext())
        {
            db.Links.Add(newLink);
            await db.SaveChangesAsync();
        }
        return CreatedAtAction(nameof(CreateProductLink), newLink);
    }
    
    [HttpPost("metaData")]
    public async Task<IActionResult> CreateProductData(ProductMetaData newMetaData)
    {
        await using (var db = new ProductDbContext())
        {
            db.ProductMetaData.Add(newMetaData);
            await db.SaveChangesAsync();
        }
        return Ok();
    }
}