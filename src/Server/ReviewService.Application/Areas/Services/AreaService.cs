using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Areas.Services
{
    public class AreaService : IAreaService
    {
        private readonly IRepository<Area> _areaRepository;

        public AreaService(IRepository<Area> areaRepository)
        {
            _areaRepository = areaRepository;
        }
        public Task AddAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<Area> GetAreaByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Area>> GetAreasAsync()
        {
            throw new NotImplementedException();
        }
    }
}
