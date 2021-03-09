using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReviewService.Application.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> searchExpression = null);
        Task CreateAsync(TEntity item);
        Task DeleteAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
