using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Repository.Interfaces
{
    public interface IReviewSessionRepository : IRepository<ReviewSession>
    {
        Task<List<ReviewSession>> GetAllReviewSessionsAsync();
        Task<ReviewSession> GetReviewSessionByIdAsync(int id);
    }
}
