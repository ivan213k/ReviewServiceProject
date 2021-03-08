using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Areas.Interfaces
{
    public interface IEvaluationPointService
    {
        Task<List<EvaluationPoint>> GetEvaluationPointsAsync();
        Task AddEvaluationPointAsync(EvaluationPoint evaluationPoint);
    }
}
