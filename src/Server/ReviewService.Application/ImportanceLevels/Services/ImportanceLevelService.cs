using FluentValidation.Results;
using ReviewService.Application.Common.Exceptions;
using ReviewService.Application.ImportanceLevels.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Application.ImportanceLevels.Services
{
    public class ImportanceLevelService : IImportanceLevelService
    {
        private readonly IRepository<ImportanceLevel> _importanceLevelRepository;

        public ImportanceLevelService(IRepository<ImportanceLevel> importanceLevelRepository)
        {
            _importanceLevelRepository = importanceLevelRepository;
        }

        public async Task<List<ImportanceLevel>> GetImportanceLevelsAsync()
        {
            return await _importanceLevelRepository.GetAllAsync();
        }

        public async Task AddImportanceLevelAsync(ImportanceLevel importanceLevel)
        {
            await _importanceLevelRepository.CreateAsync(importanceLevel);
        }
        private async Task ValidateForUniqueName(ImportanceLevel importanceLevel)
        {
            var importanceLevels = await _importanceLevelRepository.GetAllAsync();
            List<ValidationFailure> failures = new List<ValidationFailure>();
            if (importanceLevels.Any(r => r.Name == importanceLevel.Name))
            {
                failures.Add(new ValidationFailure(nameof(importanceLevel.Name), "Importance level with the same name already exist"));
                throw new ValidationException(failures);
            }
        }

        public async Task DeleteImportanceLevelAsync(ImportanceLevel importanceLevel)
        {
            await _importanceLevelRepository.DeleteAsync(importanceLevel);
        }

        public async Task<ImportanceLevel> GetByIdAsync(int id)
        {
            return await _importanceLevelRepository.GetByIdAsync(id);
        }

        public async Task UpdateImportanceLevelAsync(ImportanceLevel importanceLevel)
        {
            await _importanceLevelRepository.UpdateAsync(importanceLevel);
        }
    }
}
