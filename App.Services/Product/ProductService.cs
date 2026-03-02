using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;

namespace Services;

public class ProductServices(IProductInterface productInterface,IUnitOfWork unitofWork):IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
       var  products=await productInterface.TheTopSellingProducts(count);

       if (!products.IsNullOrEmpty())
       {
           var dtoList = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stok)).ToList();
           return ServiceResult<List<ProductDto>>.Success(dtoList);
       }

       return ServiceResult<List<ProductDto>>.Failed(new string[] { "bir hata olustu" });

    }
    
    public async Task<ServiceResult<Product>> getProductByIdAsync(int id)
    {
        var theProduct = await productInterface.GetByIdAsync(id);

        if (theProduct==null)
        {
            return ServiceResult<Product>.Failed(new string[] {"The Product is not found."});
        }
        
        return ServiceResult<Product>.Success(theProduct);
    }

    //Burda bunu yapmamın sebebi buyuk verileri parcalı parcalı ve sayfa sayfa bir sekilde getirmektir//
    
    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListedAsync(int pageIndex,int pageSize)
    {
        var products = await productInterface.GetAll().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        if (products.IsNullOrEmpty())
        {
            ServiceResult.Failed(new string[] { "Listede Hic Urun Bulunamadı" });
        }
        
        
        var productsDto= products.Select(product => new ProductDto(product.Id, product.Name, product.Price, product.Stok)).ToList();

        return ServiceResult<List<ProductDto>>.Success(productsDto);
    }
    
    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stok = request.Stok 
        };

        await productInterface.AddAsync(product);

        await unitofWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
    }

    public async Task<ServiceResult> UpdateProductAsync(int ProductId, UpdateProductRequest request)
    {
        var theProduct=await productInterface.GetByIdAsync(ProductId);
        if (theProduct == null)
        {
            return ServiceResult.Failed(new[] { "Ürün Bulunamadı" });
        }
            theProduct.Name = request.Name;
            theProduct.Price = request.Price;
            theProduct.Stok = request.Stok;
            
             productInterface.Update(theProduct);

             await unitofWork.SaveChangesAsync();
             return ServiceResult.Success();
        
    }

    public async Task<ServiceResult> DeleteProductAsync(int ProductId)
    {
        var theProduct=await productInterface.GetByIdAsync(ProductId);

        if (theProduct == null)
        {
            return ServiceResult.Failed(new[] { "Bulunamadı" });
        }
        else
        {
            productInterface.Delete(theProduct);
            await unitofWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
    }

    public  async Task<ServiceResult<List<ProductDto>>> GetAllOfProductAsync()
    {
       var theList= await productInterface.GetAll().Select(x=>new ProductDto(x.Id,x.Name,x.Price,x.Stok)).ToListAsync();

       return ServiceResult<List<ProductDto>>.Success(theList);
    }
}