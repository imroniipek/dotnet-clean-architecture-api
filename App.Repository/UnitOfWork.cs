namespace Repository;

public class UnitOfWork(AppDbContext context):IUnitOfWork
{
    public Task<int> SaveChangesAsync()=>context.SaveChangesAsync();
}