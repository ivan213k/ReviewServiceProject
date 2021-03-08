using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Areas.Interfaces
{
    public interface IAreaService
    {
        Task<List<Area>> GetAreasAsync();
        Task<Area> GetAreaByIdAsync(int id);
        Task AddAreaAsync(Area area);
        Task DeleteAreaAsync(Area area);
        Task UpdateAreaAsync(Area area);
    }
}
