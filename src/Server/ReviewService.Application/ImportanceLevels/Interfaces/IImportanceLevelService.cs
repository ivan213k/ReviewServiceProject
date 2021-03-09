using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.ImportanceLevels.Interfaces
{
    public interface IImportanceLevelService
    {
        Task<List<ImportanceLevel>> GetImportanceLevelsAsync();
        Task AddImportanceLevelAsync(ImportanceLevel importanceLevel);
        Task DeleteImportanceLevelAsync(ImportanceLevel importanceLevel);
    }
}
