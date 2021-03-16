using AutoMapper;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Web.Server.AutoMapperProfiles
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AreaItem, AreaItemApiModel>().ReverseMap();
            CreateMap<Area, AreaApiModel>().ReverseMap();
            CreateMap<ImportanceLevel, ImportanceLevelApiModel>().ReverseMap();
        }
    }
}
