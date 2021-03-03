using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Areas.Interfaces;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        public Task<List<AreaApiModel>> GetAreas()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public Task<AreaApiModel> GetAreaById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
