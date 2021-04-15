using ReviewService.Application.PersonalReviewEvaluations.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Application.PersonalReviewEvaluations.Services
{
    public class ReviewEvaluationService : IReviewEvaluationService
    {
        private readonly IRepository<ReviewEvaluation> _reviewEvaluationRepository;

        public ReviewEvaluationService(IRepository<ReviewEvaluation> reviewEvaluationRepository)
        {
            _reviewEvaluationRepository = reviewEvaluationRepository;
        }

        public async Task<ReviewEvaluation> GetByGuidAsync(Guid guid)
        {
            return (await _reviewEvaluationRepository.GetAllAsync(r => r.Guid == guid)).SingleOrDefault();
        }

        public async Task<ReviewEvaluation> GetByIdAsync(int id)
        {
            return await _reviewEvaluationRepository.GetByIdAsync(id);
        }

        public async Task UpdateReviewEvaluationAsync(ReviewEvaluation reviewEvaluation)
        {
            await _reviewEvaluationRepository.UpdateAsync(reviewEvaluation);
        }
    }
}
