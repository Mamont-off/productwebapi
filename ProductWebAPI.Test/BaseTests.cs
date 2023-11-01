using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Controllers;
using ProductWebAPI.Models;
using ProductWebAPI.Other;

namespace ProductWebAPI.Test;

public class BaseTests
{
    private readonly NomenclaturesController _controller;
    private readonly Nomenclature _testNomenclature = new() { Name="TestProduct", Price = 1000 };
    private readonly Nomenclature _testChildNomenclature = new() { Name="TestChildProduct", Price = 100 };
    private readonly Links _testLink = new() { Count = 1, NomenclatureId = 0, ParentId = 4 };
    
    public BaseTests()
    {
        _controller = new NomenclaturesController();
    }

    private async Task CreateTestNomenclatures()
    {
        await CreateTestNomenclature();
        await CreateTestChildNomenclature();
    }
    
    private async Task CreateTestNomenclature()
    {
        var result = await _controller.CreateProduct(_testNomenclature);
        var oResult = result.Result as ObjectResult;
        _testNomenclature.Id = (oResult.Value as Nomenclature).Id;
    }
    private async Task CreateTestChildNomenclature()
    {
        var result = await _controller.CreateProduct(_testChildNomenclature);
        var oResult = result.Result as ObjectResult;
        _testChildNomenclature.Id = (oResult.Value as Nomenclature).Id;
    }
    
    [Fact]
    public async void CreateAndDeleteProduct()
    {
        var resultCreate = await _controller.CreateProduct(_testNomenclature);
        
        Assert.IsAssignableFrom<CreatedAtActionResult>(resultCreate.Result);

        var oResult = resultCreate.Result as ObjectResult;
        var createId = (oResult.Value as Nomenclature).Id;
        var resultDelete = await _controller.DeleteProduct(createId);
        
        Assert.IsAssignableFrom<OkResult>(resultDelete);
    }
    
    [Fact]
    public async void GetAllProduct()
    {
        var resultCreate = await _controller.GetAll();
        
        Assert.NotNull(resultCreate.Value);
        Assert.NotEmpty(resultCreate.Value);//?
    }

    [Fact]
    public async void CreateAndDeleteLinkForProduct()
    {
        await CreateTestNomenclatures();
        _testLink.ParentId = _testNomenclature.Id;
        _testLink.NomenclatureId = _testChildNomenclature.Id;
        _testLink.Count = 2;
        
        var resultCreate = await _controller.CreateProductLink(_testLink);
        Assert.IsAssignableFrom<CreatedAtActionResult>(resultCreate.Result);
        
        var resultDelete = await _controller.DeleteLink(_testLink.ParentId, _testLink.NomenclatureId);
        Assert.IsAssignableFrom<OkResult>(resultDelete);
        
        //cleanUp
        await _controller.DeleteProduct(_testChildNomenclature.Id);
        await _controller.DeleteProduct(_testNomenclature.Id);
    }
    
    [Fact]
    public void SumPriceTest()
    {
        var productM1 = new ProductModel() { Id = 1, Name = "P1", Price = 1000, Count = 1, Sum = 0 };
        var productM2 = new ProductModel() { Id = 2, Name = "P2", Price = 100, Count = 3, Sum = 0 };
        
        var sumPrice = new SumPrice(productM1);
        sumPrice.Add(productM2, 1);

        var result = sumPrice.CalculateProductSum();
        
        Assert.NotNull(result);
        Assert.True(result[0].Sum == 1300);
        Assert.True(result[1].Sum == 300);
    }
    
    [Fact]
    public async void SumPriceNomenclatures()
    {
        await CreateTestNomenclatures();
        _testLink.ParentId = _testNomenclature.Id;
        _testLink.NomenclatureId = _testChildNomenclature.Id;
        _testLink.Count = 3;
        
        await _controller.CreateProductLink(_testLink);

        var resultSum = await _controller.GetProductSeries(_testNomenclature.Id);
        
        Assert.NotNull(resultSum.Value);
        Assert.NotEmpty(resultSum.Value);
        Assert.True(resultSum.Value.Count == 2);
        Assert.True(resultSum.Value[0].Sum == 1300);
        Assert.True(resultSum.Value[1].Sum == 300);

        //cleanUp
        await _controller.DeleteProduct(_testChildNomenclature.Id);
        await _controller.DeleteProduct(_testNomenclature.Id);
    }
}