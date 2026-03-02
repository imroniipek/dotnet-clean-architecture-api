using Microsoft.EntityFrameworkCore;

namespace Repository;


public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext),IProductInterface
{
    public async Task<List<Product>> TheTopSellingProducts(int count)
    {
        return await _dbSet.OrderByDescending(i => i.Price).Take(count).ToListAsync();
    }

   
}
