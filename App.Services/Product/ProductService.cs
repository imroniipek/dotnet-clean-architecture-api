using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Product;
using Repository;

namespace Services.Product;

public class ProductServices : IProductService
{
    private readonly IProductInterface _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServices(IProductInterface productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await _productRepository.TheTopSellingProducts(count);

        if (products.IsNullOrEmpty())
            return ServiceResult<List<ProductDto>>.Failed(new[] { "Ürün bulunamadı" });

        var dtoList = products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockCount))
            .ToList();

        return ServiceResult<List<ProductDto>>.Success(dtoList);
    }

    public async Task<ServiceResult<Repository.Product.Product>> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product.Equals(null))
            return ServiceResult<Repository.Product.Product>.Failed(new[] { "Ürün bulunamadı" });

        return ServiceResult<Repository.Product.Product>.Success(product);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListedAsync(int pageIndex, int pageSize)
    {
        var products = await _productRepository.GetAll()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (!products.Any())
            return ServiceResult<List<ProductDto>>.Failed(new[] { "Listede ürün bulunamadı" });

        var dtoList = products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockCount))
            .ToList();

        return ServiceResult<List<ProductDto>>.Success(dtoList);
    }

    public async Task<ServiceResult> UpdateStock(UpdateProductStockRequest request)
    {
        var theProduct = await _productRepository.GetByIdAsync(request.ProductId);

        if (theProduct is null)
            return ServiceResult.Failed(new[] { "The Product was not found" });

        theProduct.StockCount = request.Quantity;
        _productRepository.Update(theProduct);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }


    public async Task<ServiceResult> UpdateStock(UpdateProductRequest request)
    {
        var theProduct=await _productRepository.GetByIdAsync(request.Id);

        if (theProduct is null)
        {
            return ServiceResult.Failed(new [] { "The Product was not found " });
        }

        theProduct.StockCount = request.Stock;
        _productRepository.Update(theProduct);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Repository.Product.Product
        {
            Name = request.Name,
            Price = request.Price,
            StockCount = request.Stok
        };

        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"/api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
        {
            return ServiceResult.Failed(new[] { "Ürün bulunamadı" });
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.StockCount = request.Stock;

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product.Equals(null))
            return ServiceResult.Failed(new[] { "Ürün bulunamadı" });

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }
    public async Task<ServiceResult<List<ProductDto>>> GetAllOfProductAsync()
    {
        var dtoList = await _productRepository.GetAll()
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockCount))
            .ToListAsync();

        return ServiceResult<List<ProductDto>>.Success(dtoList);
    }
}