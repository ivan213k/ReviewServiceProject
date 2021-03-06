using ReviewService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.PersonalReviewEvaluations.Interfaces
{
    public interface IReviewEvaluationService
    {
        Task<ReviewEvaluation> GetByGuidAsync(Guid guid);
        Task<ReviewEvaluation> GetByIdAsync(int id);
        Task UpdateReviewEvaluationAsync(ReviewEvaluation reviewEvaluation);
        Task<List<PersonalReview>> GetPersonalReviewsAsync(string userId);
    }
}
