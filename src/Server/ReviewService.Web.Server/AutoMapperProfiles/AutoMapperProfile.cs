﻿using AutoMapper;
using ReviewService.Domain.Entites;
using ReviewService.Domain.Enums;
using ReviewService.Shared.ApiEnums;
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
            CreateMap<EvaluationPointsTemplate, EvaluationPointsTemplateApiModel>().ReverseMap();
            CreateMap<EvaluationPoint, EvaluationPointApiModel>().ReverseMap();
            CreateMap<ReviewTemplate, ReviewTemplateApiModel>().ReverseMap();
            CreateMap<ReviewSession, ReviewSessionApiModel>().ReverseMap();
            CreateMap<ReviewEvaluation, ReviewEvaluationApiModel>().ReverseMap();

            CreateMap<ReviewSessionStatus, ReviewSessionStatusApiEnum>().ReverseMap();
            CreateMap<ReviewEvaluationStatus, ReviewEvaluationStatusApiEnum>().ReverseMap();
        }
    }
}