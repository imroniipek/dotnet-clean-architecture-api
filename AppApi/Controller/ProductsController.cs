using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await service.GetAllOfProductAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => CreateActionResult(await service.getProductByIdAsync(id));

    [HttpGet("{pageIndex}/{pageSize}")]
    public async Task<IActionResult> GetPagedAllListAsync(int pageIndex, int pageSize) =>
        CreateActionResult(await service.GetPagedAllListedAsync(pageIndex, pageSize));
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var result = await service.CreateProductAsync(request);
        
        if (result.IsFailed) 
        {
            return CreateActionResult(result);
        }
        
        return CreatedAtAction(
            nameof(GetById),              // Gitmesi gereken metot adı
            new { id = result.Data.Id },   // O metoda lazım olan parametre
            result.Data                    // İstemciye (Swagger/Frontend) dönecek veri
        );
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        => CreateActionResult(await service.UpdateProductAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await service.DeleteProductAsync(id));
}