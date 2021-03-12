using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.EvaluationPoints.Interfaces
{
    public interface IEvaluationPointService
    {
        Task<List<EvaluationPointsTemplate>> GetEvaluationPointTemplatesAsync();
        Task AddEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate);
    }
}
