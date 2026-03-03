namespace Services.Product;
public interface IProductService
{
    public Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);

    public Task<ServiceResult<Repository.Product.Product>> GetProductByIdAsync(int id);

    public Task<ServiceResult<List<ProductDto>>> GetAllOfProductAsync();

    public Task<ServiceResult> DeleteProductAsync(int productId);

    public Task<ServiceResult> UpdateProductAsync(int productId, UpdateProductRequest request);


    public Task<ServiceResult<List<ProductDto>>> GetPagedAllListedAsync(int pageNumber,int pageSize);

    public Task<ServiceResult> UpdateStock(UpdateProductStockRequest request);
    public Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);
}


