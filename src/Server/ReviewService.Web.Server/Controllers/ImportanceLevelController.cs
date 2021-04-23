using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ImportanceLevels.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Manager")]
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

        [HttpPut]
        public async Task UpdateImportanceLevel([FromBody] ImportanceLevelApiModel importanceLevelApiModel)
        {
            var importanceLevel = await _importanceLevelService.GetByIdAsync(importanceLevelApiModel.Id);
            if (importanceLevel is null)
            {
                return;
            }
            _autoMapper.Map(importanceLevelApiModel, importanceLevel);
            await _importanceLevelService.UpdateImportanceLevelAsync(importanceLevel);
        }
        [HttpDelete("{id}")]
        public async Task DeleteImportanceLevel(int id)
        {
            var importanceLevel = await _importanceLevelService.GetByIdAsync(id);
            if (importanceLevel is null)
            {
                return;
            }
            await _importanceLevelService.DeleteImportanceLevelAsync(importanceLevel);
        }
    }
}
