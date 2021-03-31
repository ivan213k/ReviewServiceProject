using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using ReviewService.Domain.Enums;

namespace ReviewService.Application.ReviewSessions.Services
{
    public class ReviewSessionService : IReviewSessionService
    {
        private readonly IRepository<ReviewSession> _reviewSessionRepository;
        public ReviewSessionService(IRepository<ReviewSession> reviewSessionRepository)
        {
            _reviewSessionRepository = reviewSessionRepository;
        }
        public async Task CreateReviewSessionAsync(ReviewTemplate template, ReviewSession reviewSession)
        {
            await _reviewSessionRepository.CreateAsync(reviewSession);
        }

        public async Task DeleteReviewSessionAsync(ReviewSession reviewSession)
        {
            await _reviewSessionRepository.DeleteAsync(reviewSession);
        }

        public async Task<List<ReviewSession>> GetReviewSessionsAsync()
        {
            return await _reviewSessionRepository.GetAllAsync();
        }
        public async Task PublishReviewSessionAsync(ReviewSession reviewSession)
        {
            if (reviewSession.Status == ReviewSessionStatus.New)
            {
                reviewSession.Status = ReviewSessionStatus.Published;
                await _reviewSessionRepository.UpdateAsync(reviewSession);
            }
        }
        public async Task CancelReviewSessionAsync(ReviewSession reviewSession)
        {
            if (reviewSession.Status == ReviewSessionStatus.Published)
            {
                reviewSession.Status = ReviewSessionStatus.Canceled;
                await _reviewSessionRepository.UpdateAsync(reviewSession);
            }
        }

        public async Task<ReviewSession> GetByIdAsync(int id)
        {
            return await _reviewSessionRepository.GetByIdAsync(id);
        }

        public async Task UpdateReviewSessionAsync(ReviewSession reviewSession)
        {
            await _reviewSessionRepository.UpdateAsync(reviewSession);
        }
    }
}
