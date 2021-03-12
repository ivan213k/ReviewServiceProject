using ReviewService.Application.EvaluationPoints.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.EvaluationPoints.Services
{
    public class EvaluationPointService : IEvaluationPointService
    {
        private readonly IRepository<EvaluationPointsTemplate> _evaluationPointTemplateRepository;

        public EvaluationPointService(IRepository<EvaluationPointsTemplate> evaluationPointTemplateRepository)
        {
            _evaluationPointTemplateRepository = evaluationPointTemplateRepository;
        }

        public async Task<List<EvaluationPointsTemplate>> GetEvaluationPointTemplatesAsync()
        {
            return await _evaluationPointTemplateRepository.GetAllAsync();
        }

        public async Task AddEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate)
        {
            await _evaluationPointTemplateRepository.CreateAsync(evaluationPointsTemplate);
        }

    }
}
