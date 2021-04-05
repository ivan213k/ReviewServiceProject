using Microsoft.EntityFrameworkCore;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance.Repositories
{
    public class ReviewSessionRepository : Repository<ReviewSession>, IReviewSessionRepository
    {
        ReviewServiceDbContext _dbContext;
        public ReviewSessionRepository(ReviewServiceDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReviewSession>> GetAllReviewSessionsAsync()
        {
            return await _dbContext.ReviewSessions.Include(r => r.ReviewEvaluations).ToListAsync();
        }

        public async Task<ReviewSession> GetReviewSessionByIdAsync(int id)
        {
            return await _dbContext.ReviewSessions.Include(r => r.ReviewEvaluations).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
