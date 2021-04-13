using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.ReviewSessions.Interfaces
{
    public interface IReviewSessionService
    {
        Task<List<ReviewSession>> GetReviewSessionsAsync();
        Task<ReviewSession> GetByIdAsync(int id);
        Task<List<PersonalReviewViewItem>> GetReviewViewItemsAsync(int sessionId);
        Task CreateReviewSessionAsync(ReviewTemplate template, ReviewSession reviewSession);
        Task UpdateReviewSessionAsync(ReviewSession reviewSession);
        Task DeleteReviewSessionAsync(ReviewSession reviewSession);
        Task PublishReviewSessionAsync(ReviewSession reviewSession);
        Task CancelReviewSessionAsync(ReviewSession reviewSession);
    }
}
