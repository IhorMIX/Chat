namespace Chat.DAL.Repository.Interface;

public interface IBasicRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}