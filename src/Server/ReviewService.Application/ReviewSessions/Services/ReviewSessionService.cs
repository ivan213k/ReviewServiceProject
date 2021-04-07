using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewService.Domain.Enums;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;

namespace ReviewService.Application.ReviewSessions.Services
{
    public class ReviewSessionService : IReviewSessionService
    {
        private readonly IReviewSessionRepository _reviewSessionRepository;
        public ReviewSessionService(IReviewSessionRepository reviewSessionRepository)
        {
            _reviewSessionRepository = reviewSessionRepository;
        }
        public async Task CreateReviewSessionAsync(ReviewTemplate template, ReviewSession reviewSession)
        {
            reviewSession.Session_json = SerializeAreasToJson(template.Areas);
            await _reviewSessionRepository.CreateAsync(reviewSession);
           
        }

        public async Task DeleteReviewSessionAsync(ReviewSession reviewSession)
        {
            await _reviewSessionRepository.DeleteAsync(reviewSession);
        }

        public async Task<List<ReviewSession>> GetReviewSessionsAsync()
        {
            return await _reviewSessionRepository.GetAllReviewSessionsAsync();
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
            return await _reviewSessionRepository.GetReviewSessionByIdAsync(id);
        }

        public async Task UpdateReviewSessionAsync(ReviewSession reviewSession)
        {
            await _reviewSessionRepository.UpdateAsync(reviewSession);
        }

        private string SerializeAreasToJson(List<Area> areas)
        {
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(Area), nameof(Area.ReviewTemplates));

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;
            return JsonConvert.SerializeObject(areas, serializerSettings);
        }
       
    }
}
