using FluentValidation.Results;
using ReviewService.Application.Common.Exceptions;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.ReviewTemplates.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
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
            await ValidateForUniqueName(reviewTemplate);
            await _reviewTemplateRepository.CreateAsync(reviewTemplate);
        }

        private async Task ValidateForUniqueName(ReviewTemplate reviewTemplate)
        {
            var reviewTemplates = await _reviewTemplateRepository.GetReviewTemplatesAsync();
            List<ValidationFailure> failures = new List<ValidationFailure>();
            if (reviewTemplates.Any(r => r.Name == reviewTemplate.Name))
            {
                failures.Add(new ValidationFailure(nameof(reviewTemplate.Name), "Review template with the same name already exist"));
                throw new ValidationException(failures);
            }
        }

        public async Task DeleteReviewTemplateAsync(ReviewTemplate reviewTemplate)
        {
            await _reviewTemplateRepository.DeleteAsync(reviewTemplate);
        }

        public async Task<ReviewTemplate> GetByIdAsync(int id)
        {
            return await _reviewTemplateRepository.GetReviewTemplateById(id);
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
