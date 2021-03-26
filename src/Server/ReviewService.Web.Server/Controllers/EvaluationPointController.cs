using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.EvaluationPoints.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationPointController : ControllerBase
    {
        private readonly IEvaluationPointService _evaluationPointService;
        private readonly IMapper _mapper;

        public EvaluationPointController(IEvaluationPointService evaluationPointService, IMapper mapper)
        {
            _evaluationPointService = evaluationPointService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<EvaluationPointsTemplateApiModel>> GetEvaluationPointTemplates()
        {
            var evaluationPointTemplates = await _evaluationPointService.GetEvaluationPointTemplatesAsync();
            return _mapper.Map<List<EvaluationPointsTemplateApiModel>>(evaluationPointTemplates);
        }
        
        [HttpPost]
        public async Task AddEvaluationPointTemplate([FromBody] EvaluationPointsTemplateApiModel evaluationPointsTemplateApiModel)
        {
            var evaluationPointsTemplate = _mapper.Map<EvaluationPointsTemplate>(evaluationPointsTemplateApiModel);
            await _evaluationPointService.AddEvaluationPointTemplateAsync(evaluationPointsTemplate);
        }

        [HttpPut]
        public async Task UpdateEvaluationPointTemplate([FromBody] EvaluationPointsTemplateApiModel evaluationPointsTemplateApiModel)
        {
            var evaluationPointTemplate = await _evaluationPointService.GetByIdAsync(evaluationPointsTemplateApiModel.Id);
            if (evaluationPointTemplate is null)
            {
                return;
            }
            _mapper.Map(evaluationPointsTemplateApiModel, evaluationPointTemplate);
            await _evaluationPointService.UpdateEvaluationPointTemplateAsync(evaluationPointTemplate);
        }

        [HttpDelete("{id}")]
        public async Task DeleteArea(int id)
        {
            var evaluationPoint = await _evaluationPointService.GetByIdAsync(id);
            if (evaluationPoint is null)
            {
                return;
            }
            await _evaluationPointService.DeleteEvaluationPointTemplateAsync(evaluationPoint);
        }
    }
}
