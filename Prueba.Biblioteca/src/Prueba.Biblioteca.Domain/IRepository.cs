
using System.Linq.Expressions;

namespace Prueba.Biblioteca.Domain;


public interface IRepository<TEntity> where TEntity : class
{
    IUnitOfWork UnitOfWork { get; }
    
    IQueryable<TEntity> GetAll(bool asNoTracking = true);

    Task<TEntity> GetByIdAsync(int id);

    Task<TEntity> AddAsync(TEntity entity);

    Task UpdateAsync (TEntity entity);

    void  Delete(TEntity entity);

    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
}
