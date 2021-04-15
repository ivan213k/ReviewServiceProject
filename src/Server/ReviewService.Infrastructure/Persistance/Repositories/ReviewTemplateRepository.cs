using Microsoft.EntityFrameworkCore;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance.Repositories
{
    public class ReviewTemplateRepository : Repository<ReviewTemplate>, IReviewTemplateRepository
    {
        ReviewServiceDbContext _dbContext;
        public ReviewTemplateRepository(ReviewServiceDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReviewTemplate>> GetReviewTemplatesAsync()
        {
            return await _dbContext.ReviewTemplates.Include(r => r.Areas).ToListAsync();
        }
        public async Task<ReviewTemplate> GetReviewTemplateById(int id)
        {
            return await _dbContext.ReviewTemplates.Include(r=>r.MidEvaluationPoint).Include(r => r.Areas).ThenInclude(a=>a.AreaItems).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
