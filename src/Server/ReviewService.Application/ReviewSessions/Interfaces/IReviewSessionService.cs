using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.ReviewSessions.Interfaces
{
    public interface IReviewSessionService
    {
        Task<List<ReviewSession>> GetReviewSessionsAsync();
        Task CreateReviewSessionAsync(ReviewTemplate template, ReviewSession reviewSession);
        Task DeleteReviewSessionAsync(ReviewSession reviewSession);
        Task PublishReviewSessionAsync(ReviewSession reviewSession);
        Task CancelReviewSessionAsync(ReviewSession reviewSession);
    }
}
