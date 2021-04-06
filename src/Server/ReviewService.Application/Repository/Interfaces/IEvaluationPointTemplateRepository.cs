using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Repository.Interfaces
{
    public interface IEvaluationPointTemplateRepository : IRepository<EvaluationPointsTemplate>
    {
        Task<List<EvaluationPointsTemplate>> GetEvaluationPointsTemplates();
        Task<EvaluationPointsTemplate> GetEvaluationPointsTemplateByIdAsync(int id);

    }
}
