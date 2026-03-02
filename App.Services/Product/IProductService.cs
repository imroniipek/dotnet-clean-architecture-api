namespace Services;
using Repository;
public interface IProductService
{
    public Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);

    public Task<ServiceResult<Product>> getProductByIdAsync(int id);

    public Task<ServiceResult<List<ProductDto>>> GetAllOfProductAsync();

    public Task<ServiceResult> DeleteProductAsync(int ProductId);

    public Task<ServiceResult> UpdateProductAsync(int ProductId, UpdateProductRequest request);


    public Task<ServiceResult<List<ProductDto>>> GetPagedAllListedAsync(int pageNumber,int pageSize);

    public Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);
}


