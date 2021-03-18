using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Areas.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }
        public async Task AddAreaAsync(Area area)
        {
            await _areaRepository.CreateAsync(area);
        }

        public async Task DeleteAreaAsync(Area area)
        {
            await _areaRepository.DeleteAsync(area);
        }

        public async Task<Area> GetAreaByIdAsync(int id)
        {
            return await _areaRepository.GetByIdAsync(id);
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            return await _areaRepository.GetAllAreasAsync();
        }

        public async Task UpdateAreaAsync(Area area)
        {
            await _areaRepository.UpdateAsync(area);
        }
    }
}
