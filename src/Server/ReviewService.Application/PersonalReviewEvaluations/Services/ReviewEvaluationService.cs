using ReviewService.Application.PersonalReviewEvaluations.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Application.PersonalReviewEvaluations.Services
{
    public class ReviewEvaluationService : IReviewEvaluationService
    {
        private readonly IRepository<ReviewEvaluation> _reviewEvaluationRepository;
        private readonly IReviewSessionRepository _reviewSessionRepository;

        public ReviewEvaluationService(IRepository<ReviewEvaluation> reviewEvaluationRepository, IReviewSessionRepository reviewSessionRepository)
        {
            _reviewEvaluationRepository = reviewEvaluationRepository;
            _reviewSessionRepository = reviewSessionRepository;
        }

        public async Task<ReviewEvaluation> GetByGuidAsync(Guid guid)
        {
            return (await _reviewEvaluationRepository.GetAllAsync(r => r.Guid == guid)).SingleOrDefault();
        }

        public async Task<ReviewEvaluation> GetByIdAsync(int id)
        {
            return await _reviewEvaluationRepository.GetByIdAsync(id);
        }

        public async Task<List<PersonalReview>> GetPersonalReviewsAsync(string userId)
        {
            var reviewEvaluations = (await _reviewEvaluationRepository.GetAllAsync()).Where(r => r.UserId == userId).ToList();
            var personalReviews = new List<PersonalReview>();
            foreach (var reviewEvaluation in reviewEvaluations)
            {
                var session = await _reviewSessionRepository.GetByIdAsync(reviewEvaluation.ReviewSessionId);
                personalReviews.Add(new PersonalReview()
                {
                    Session = session.Name,
                    PersonUnderReview = session.PersonUnderReview,
                    ReviewStatus = reviewEvaluation.Status,
                    ReviewLink = $"/reviewpage/{reviewEvaluation.Guid}"
                });
            }
            return personalReviews;
        }

        public async Task UpdateReviewEvaluationAsync(ReviewEvaluation reviewEvaluation)
        {
            await _reviewEvaluationRepository.UpdateAsync(reviewEvaluation);
        }
    }
}
