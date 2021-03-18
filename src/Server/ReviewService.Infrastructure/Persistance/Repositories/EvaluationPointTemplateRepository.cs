using Microsoft.EntityFrameworkCore;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance.Repositories
{
    public class EvaluationPointTemplateRepository : Repository<EvaluationPointsTemplate>, IEvaluationPointTemplateRepository
    {
        ReviewServiceDbContext _dbContext;
        public EvaluationPointTemplateRepository(ReviewServiceDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<EvaluationPointsTemplate>> GetEvaluationPointsTemplates()
        {
            return await _dbContext.EvaluationPointsTemplates.Include(e => e.EvaluationPoints).ToListAsync();
        }
    }
}
