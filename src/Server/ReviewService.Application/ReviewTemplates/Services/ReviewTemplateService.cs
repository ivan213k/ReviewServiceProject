using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.ReviewTemplates.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.ReviewTemplates.Services
{
    public class ReviewTemplateService : IReviewTemplateService
    {
        private readonly IReviewTemplateRepository _reviewTemplateRepository;
        public ReviewTemplateService(IReviewTemplateRepository reviewTemplateRepository)
        {
            _reviewTemplateRepository = reviewTemplateRepository;
        }
        public async Task AddReviewTemplateAsync(ReviewTemplate reviewTemplate)
        {
            await _reviewTemplateRepository.CreateAsync(reviewTemplate);
        }

        public async Task DeleteReviewTemplateAsync(ReviewTemplate reviewTemplate)
        {
            await _reviewTemplateRepository.DeleteAsync(reviewTemplate);
        }

        public async Task<List<ReviewTemplate>> GetReviewTemplatesAsync()
        {
            return await _reviewTemplateRepository.GetReviewTemplatesAsync();
        }

        public async Task UpdateReviewTemplateAsync(ReviewTemplate reviewTemplate)
        {
            await _reviewTemplateRepository.UpdateAsync(reviewTemplate);
        }
    }
}
