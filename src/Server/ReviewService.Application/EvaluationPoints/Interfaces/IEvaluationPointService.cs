using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.EvaluationPoints.Interfaces
{
    public interface IEvaluationPointService
    {
        Task<List<EvaluationPointsTemplate>> GetEvaluationPointTemplatesAsync();
        Task<EvaluationPointsTemplate> GetByIdAsync(int id);
        Task AddEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate);
        Task UpdateEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate);
    }
}
