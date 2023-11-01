using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;
using ProductWebAPI.Other;

namespace ProductWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public partial class NomenclaturesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Nomenclature>>> GetAll()
    {
        await using (var db = new ProductDbContext())
        {
            return db.Nomenclature.ToList();
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Nomenclature>> Get(int id)
    {
        await using (var db = new ProductDbContext())
        {
            var result = db.Nomenclature.Find(id);
            if (result != null)
            {
                return result;
            }

            return NotFound($"Nomenclature with id: {id} - not exist");
        }
    }
    
    [HttpGet("sum/{id}")]
    public async Task<ActionResult<List<ProductModel>>> GetProductSeries(int id)
    {
        await using (var db = new ProductDbContext())
        {
            var exist = db.Nomenclature.Find(id);
            if (exist == null)
            {
                return NotFound($"Nomenclature with id: {id} - not exist");
            }

            var firstProduct = new ProductModel
            {
                Count = 1, //always set 1 - task (but it strange)
                Id = exist.Id, Name = exist.Name, Price = exist.Price
            };

            var sumPrice = new SumPrice(firstProduct);

            var ids = new Stack<int>();
            ids.Push(id);
            do
            {
                var rootId = ids.Pop();
                var result = GetChildProducts(rootId, db);
                if (result.Any())
                {
                    foreach (var pModel in result)
                    {
                        sumPrice.Add(pModel, rootId);
                        ids.Push(pModel.Id);
                    }
                }
            } while (ids.Count > 0);

            var resultList = sumPrice.CalculateProductSum();
            return resultList;
        }

        IQueryable<ProductModel> GetChildProducts(int productId, ProductDbContext db)
        {
            var queryString = string.Format("SELECT N.\"Id\", \"Name\", \"Price\", \"Kol\" " +
                                            "FROM \"Nomenclature\" AS N LEFT OUTER JOIN \"Links\" AS L " +
                                            "ON L.\"NomenclatureId\" = N.\"Id\" " +
                                            "WHERE N.\"Id\" IN (SELECT \"NomenclatureId\" FROM \"Nomenclature\" AS N JOIN \"Links\" AS L " +
                                            "ON L.\"ParentId\" = N.\"Id\" WHERE N.\"Id\" = {0})", productId);
            
            return db.ProductModel.FromSqlRaw(queryString);
        }
    }
}