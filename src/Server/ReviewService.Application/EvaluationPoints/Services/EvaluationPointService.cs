using FluentValidation.Results;
using ReviewService.Application.Common.Exceptions;
using ReviewService.Application.EvaluationPoints.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
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
            await ValidateForUniqueName(evaluationPointsTemplate);
            ValidateForUniqueEvaluationPoints(evaluationPointsTemplate);
            await _evaluationPointTemplateRepository.CreateAsync(evaluationPointsTemplate);
        }
        private async Task ValidateForUniqueName(EvaluationPointsTemplate evaluationPointsTemplate)
        {
            var evaluationPoints = await _evaluationPointTemplateRepository.GetAllAsync();
            List<ValidationFailure> failures = new List<ValidationFailure>();
            if (evaluationPoints.Any(r => r.Name == evaluationPointsTemplate.Name))
            {
                failures.Add(new ValidationFailure(nameof(evaluationPointsTemplate.Name), "Evaluation point template with the same name already exist"));
                throw new ValidationException(failures);
            }
        }
        private void ValidateForUniqueEvaluationPoints(EvaluationPointsTemplate evaluationPointsTemplate)
        {
            List<ValidationFailure> failures = new List<ValidationFailure>();
            if (evaluationPointsTemplate.EvaluationPoints.Select(e => e.Name).Distinct().Count() != evaluationPointsTemplate.EvaluationPoints.Count)
            {
                failures.Add(new ValidationFailure(nameof(evaluationPointsTemplate.Name), "Evaluation point template contins items with the same name"));
                throw new ValidationException(failures);
            }
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
