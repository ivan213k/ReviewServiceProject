using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Repository.Interfaces
{
    public interface IReviewTemplateRepository : IRepository<ReviewTemplate>
    {
        Task<List<ReviewTemplate>> GetReviewTemplatesAsync();
    }
}
