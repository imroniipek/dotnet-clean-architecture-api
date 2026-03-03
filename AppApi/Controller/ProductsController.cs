using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services.Product;

namespace AppApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await service.GetAllOfProductAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => CreateActionResult(await service.GetProductByIdAsync(id));

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

        return CreateActionResult(result);

    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        => CreateActionResult(await service.UpdateProductAsync(id, request));

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
    {
        var result = await service.UpdateStock(request);
        return CreateActionResult(result);
    }
    

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await service.DeleteProductAsync(id));
}