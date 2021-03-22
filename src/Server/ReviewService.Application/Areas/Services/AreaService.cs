using FluentValidation.Results;
using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Common.Exceptions;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
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
            List<Area> areas = await _areaRepository.GetAllAreasAsync();
            ValidateArea(area, areas);
            ValidateAreaItems(area);
            await _areaRepository.CreateAsync(area);
        }

        public async Task DeleteAreaAsync(Area area)
        {
            await _areaRepository.DeleteAsync(area);
        }

        public async Task<Area> GetAreaByIdAsync(int id)
        {
            return await _areaRepository.GetAreaByIdAsync(id);
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            return await _areaRepository.GetAllAreasAsync();
        }

        public async Task UpdateAreaAsync(Area area)
        {
            ValidateAreaItems(area);
            await _areaRepository.UpdateAsync(area);
        }

        private void ValidateArea(Area area, List<Area> areas)
        {
            List<ValidationFailure> failures = new List<ValidationFailure>();
            if (areas.Any(a => a.Name == area.Name))
            {
                failures.Add(new ValidationFailure(nameof(area.Name), "Area with the same name already exist"));
                throw new ValidationException(failures);
            }
        }

        private void ValidateAreaItems(Area area)
        {
            List<ValidationFailure> failures = new List<ValidationFailure>();
            if (area.AreaItems.Select(a => a.Name).Distinct().Count() != area.AreaItems.Count)
            {
                failures.Add(new ValidationFailure(nameof(area.Name), "Area contins items with the same name"));
                throw new ValidationException(failures);
            }
        }
    }
}
