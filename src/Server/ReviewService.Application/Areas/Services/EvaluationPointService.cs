using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Areas.Services
{
    public class EvaluationPointService : IEvaluationPointService
    {
        private readonly IRepository<EvaluationPoint> _evaluationPointRepository;

        public EvaluationPointService(IRepository<EvaluationPoint> evaluationPointRepository)
        {
            _evaluationPointRepository = evaluationPointRepository;
        }

        public async Task<List<EvaluationPoint>> GetEvaluationPointsAsync()
        {
            return await _evaluationPointRepository.GetAllAsync();
        }

        public async Task AddEvaluationPointAsync(EvaluationPoint evaluationPoint)
        {
            await _evaluationPointRepository.CreateAsync(evaluationPoint);
        }

    }
}
