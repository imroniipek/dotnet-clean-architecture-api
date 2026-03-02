namespace Repository;
using Microsoft.EntityFrameworkCore; 
using System.Linq.Expressions;      

public class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T> where T : class
{
  
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public IQueryable<T> GetAll() => _dbSet.AsNoTracking();
    

    public IQueryable<T> A(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();

    public async ValueTask<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
    
    public void Update(T entity) => _dbSet.Update(entity);
   
    public void Delete(T entity) => _dbSet.Remove(entity);
}