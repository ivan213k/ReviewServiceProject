using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Application.Repository.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<List<Area>> GetAllAreasAsync();
        Task<Area> GetAreaByIdAsync(int id);
    }
}
