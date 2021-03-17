using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Areas.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _autoMapper;

        public AreaController(IAreaService areaService, IMapper autoMapper)
        {
            _areaService = areaService;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        public async Task<List<AreaApiModel>> GetAreas()
        {
            var areas = await _areaService.GetAreasAsync();
            return _autoMapper.Map<List<AreaApiModel>>(areas);
        }

        [HttpGet("{id}")]
        public async Task<AreaApiModel> GetAreaById(int id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);
            return _autoMapper.Map<AreaApiModel>(area);
        }

        [HttpPost]
        public async Task AddArea(AreaApiModel areaApiModel)
        {
            Area area = _autoMapper.Map<Area>(areaApiModel);
            await _areaService.AddAreaAsync(area);
        }

        [HttpPut]
        public async Task UpdateArea(AreaApiModel areaApiModel)
        {
            Area area = await _areaService.GetAreaByIdAsync(areaApiModel.Id);
            if (area is null)
            {
                return;
            }
            _autoMapper.Map<AreaApiModel, Area>(areaApiModel, area);
            await _areaService.UpdateAreaAsync(area);
        }

        [HttpDelete]
        public async Task DeleteArea(int id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);
            if (area is null)
            {
                return;
            }
            await _areaService.DeleteAreaAsync(area);
        }
    }
}
