using Microsoft.EntityFrameworkCore;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        ReviewServiceDbContext _dbContext;

        public AreaRepository(ReviewServiceDbContext dbContext)
            :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Area>> GetAllAreasAsync()
        {
            return await _dbContext.Areas.Include(a => a.AreaItems).ToListAsync();
        }

        public async Task<Area> GetAreaByIdAsync(int id)
        {
            return await _dbContext.Areas.Include(a => a.AreaItems).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
