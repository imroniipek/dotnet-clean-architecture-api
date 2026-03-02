using System.Linq.Expressions;

namespace Repository;

public interface IProductInterface:IGenericRepository<Product>
{
    public IQueryable<Product> GetAll();
  
    public IQueryable<Product> A(Expression<Func<Product, bool>> predicate);

    public ValueTask<Product> GetByIdAsync(int id);
    
    public ValueTask AddAsync(Product entity);
    
    public void Update(Product entity);
  
    public void Delete(Product entity);
   

    public Task<List<Product>> TheTopSellingProducts(int count);
}