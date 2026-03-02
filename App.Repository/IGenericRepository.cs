namespace Repository;

using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> A(Expression<Func<T,bool>>predicate);
    ValueTask<T> GetByIdAsync(int id);

    ValueTask AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}