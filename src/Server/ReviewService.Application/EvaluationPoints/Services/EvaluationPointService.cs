using ReviewService.Application.EvaluationPoints.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.EvaluationPoints.Services
{
    public class EvaluationPointService : IEvaluationPointService
    {
        private readonly IEvaluationPointTemplateRepository _evaluationPointTemplateRepository;

        public EvaluationPointService(IEvaluationPointTemplateRepository evaluationPointTemplateRepository)
        {
            _evaluationPointTemplateRepository = evaluationPointTemplateRepository;
        }

        public async Task<List<EvaluationPointsTemplate>> GetEvaluationPointTemplatesAsync()
        {
            return await _evaluationPointTemplateRepository.GetEvaluationPointsTemplates();
        }

        public async Task AddEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate)
        {
            await _evaluationPointTemplateRepository.CreateAsync(evaluationPointsTemplate);
        }

        public async Task<EvaluationPointsTemplate> GetByIdAsync(int id)
        {
            return await _evaluationPointTemplateRepository.GetEvaluationPointsTemplateByIdAsync(id);
        }

        public async Task UpdateEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate)
        {
            await _evaluationPointTemplateRepository.UpdateAsync(evaluationPointsTemplate);
        }

        public async Task DeleteEvaluationPointTemplateAsync(EvaluationPointsTemplate evaluationPointsTemplate)
        {
            await _evaluationPointTemplateRepository.DeleteAsync(evaluationPointsTemplate);
        }
    }
}
