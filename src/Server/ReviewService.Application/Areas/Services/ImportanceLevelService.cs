using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Areas.Services
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

        public async Task DeleteImportanceLevelAsync(ImportanceLevel importanceLevel)
        {
            await _importanceLevelRepository.DeleteAsync(importanceLevel);
        }
    }
}
