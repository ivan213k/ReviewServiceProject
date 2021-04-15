using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.Users.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Infrastructure.Identity;
using ReviewService.Infrastructure.Persistance;
using ReviewService.Infrastructure.Persistance.Repositories;

namespace ReviewService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReviewServiceDbContext>(options =>
                            options.UseSqlServer(
                                configuration.GetConnectionString("LocalConnection")));
            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ReviewServiceDbContext>();
            
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IEvaluationPointTemplateRepository, EvaluationPointTemplateRepository>();
            services.AddTransient<IRepository<EvaluationPointsTemplate>, Repository<EvaluationPointsTemplate>>();
            services.AddTransient<IRepository<ImportanceLevel>, Repository<ImportanceLevel>>();
            services.AddTransient<IRepository<ReviewEvaluation>, Repository<ReviewEvaluation>>();
            services.AddTransient<IReviewSessionRepository, ReviewSessionRepository>();
            services.AddTransient<IReviewTemplateRepository, ReviewTemplateRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            return services;
        }
    }
}
