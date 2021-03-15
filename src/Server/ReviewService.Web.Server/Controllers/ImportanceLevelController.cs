using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ImportanceLevels.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportanceLevelController : ControllerBase
    {
        private readonly IImportanceLevelService _importanceLevelService;
        private readonly IMapper _autoMapper;

        public ImportanceLevelController(IImportanceLevelService importanceLevelService, IMapper autoMapper)
        {
            _importanceLevelService = importanceLevelService;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        public async Task<List<ImportanceLevelApiModel>> GetImportanceLevels()
        {
            var importanceLevels = await _importanceLevelService.GetImportanceLevelsAsync();
            return _autoMapper.Map<List<ImportanceLevelApiModel>>(importanceLevels);
        }

        [HttpPost]
        public async Task AddImportanceLevel([FromBody] ImportanceLevelApiModel importanceLevelApiModel)
        {
            var importanceLevel = _autoMapper.Map<ImportanceLevel>(importanceLevelApiModel);
            await _importanceLevelService.AddImportanceLevelAsync(importanceLevel);
        }

        [HttpDelete]
        public async Task DeleteImportanceLevel(int id)
        {
            var importanceLevels = await _importanceLevelService.GetImportanceLevelsAsync();
            var importanceLevel = importanceLevels.FirstOrDefault(r => r.Id == id);
            if (importanceLevel is null)
            {
                return;
            }
            await _importanceLevelService.DeleteImportanceLevelAsync(importanceLevel);
        }
    }
}
