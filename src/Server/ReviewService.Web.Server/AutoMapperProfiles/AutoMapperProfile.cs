using AutoMapper;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Web.Server.AutoMapperProfiles
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ImportanceLevel, ImportanceLevelApiModel>().ReverseMap();
        }
    }
}
