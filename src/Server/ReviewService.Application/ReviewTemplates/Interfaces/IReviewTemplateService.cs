using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.ReviewTemplates.Interfaces
{
    public interface IReviewTemplateService
    {
        Task<List<ReviewTemplate>> GetReviewTemplatesAsync();
        Task<ReviewTemplate> GetByIdAsync(int id);
        Task AddReviewTemplateAsync(ReviewTemplate reviewTemplate);
        Task DeleteReviewTemplateAsync(ReviewTemplate reviewTemplate);
        Task UpdateReviewTemplateAsync(ReviewTemplate reviewTemplate);
    }
}
