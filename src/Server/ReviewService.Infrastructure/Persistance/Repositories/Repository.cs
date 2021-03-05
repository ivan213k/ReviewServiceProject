using Microsoft.EntityFrameworkCore;
using ReviewService.Application.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbContext _dbContext;
        DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> searchExpression = null)
        {
            if (searchExpression is null)
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            return await _dbSet.AsNoTracking().Where(searchExpression).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _dbSet.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
