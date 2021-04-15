using Microsoft.Extensions.DependencyInjection;
using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Areas.Services;
using ReviewService.Application.EvaluationPoints.Interfaces;
using ReviewService.Application.EvaluationPoints.Services;
using ReviewService.Application.ImportanceLevels.Interfaces;
using ReviewService.Application.ImportanceLevels.Services;
using ReviewService.Application.PersonalReviewEvaluations.Interfaces;
using ReviewService.Application.PersonalReviewEvaluations.Services;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Application.ReviewSessions.Services;
using ReviewService.Application.ReviewTemplates.Interfaces;
using ReviewService.Application.ReviewTemplates.Services;

namespace ReviewService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IEvaluationPointService, EvaluationPointService>();
            services.AddTransient<IImportanceLevelService, ImportanceLevelService>();
            services.AddTransient<IReviewSessionService, ReviewSessionService>();
            services.AddTransient<IReviewTemplateService, ReviewTemplateService>();
            services.AddTransient<IReviewEvaluationService, ReviewEvaluationService>();
            return services;
        }
    }
}
