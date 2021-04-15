using AutoMapper;
using ReviewService.Application.Common.Models;
using ReviewService.Domain.Entites;
using ReviewService.Domain.Enums;
using ReviewService.Shared.ApiEnums;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.AuthorizationDtos;
using ReviewService.Shared.ApiModels.PersonalReviewModels;

namespace ReviewService.Web.Server.AutoMapperProfiles
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AreaItem, AreaItemApiModel>().ReverseMap();
            CreateMap<Area, AreaApiModel>().ReverseMap();
            CreateMap<ImportanceLevel, ImportanceLevelApiModel>().ReverseMap();
            CreateMap<EvaluationPointsTemplate, EvaluationPointsTemplateApiModel>().ReverseMap();
            CreateMap<EvaluationPoint, EvaluationPointApiModel>().ReverseMap();
            CreateMap<ReviewTemplate, ReviewTemplateApiModel>().ReverseMap();
            CreateMap<ReviewSession, ReviewSessionApiModel>().ReverseMap();
            CreateMap<ReviewEvaluation, ReviewEvaluationApiModel>().ReverseMap();
            CreateMap<User, UserApiModel>().ReverseMap();
            CreateMap<AuthResult, AuthResponseDto>().ReverseMap();
            
            CreateMap<EvaluationAreaItem, EvaluationAreaItemApiModel>().ReverseMap();
            CreateMap<EvaluationArea, EvaluationAreaApiModel>().ReverseMap();
            CreateMap<EvaluationJsonModel, EvaluationJsonApiModel>().ReverseMap();
            CreateMap<FinalReviewAreaItem, FinalReviewAreaItemApiModel>().ReverseMap();
            CreateMap<FinalReviewArea, FinalReviewAreaApiModel>().ReverseMap();
            CreateMap<Reviewer, ReviewerApiModel>().ReverseMap();

            CreateMap<ReviewSessionStatus, ReviewSessionStatusApiEnum>().ReverseMap();
            CreateMap<ReviewEvaluationStatus, ReviewEvaluationStatusApiEnum>().ReverseMap();
        }
    }
}
