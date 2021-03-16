using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReviewService.Application.Areas.Interfaces;
using ReviewService.Application.Areas.Services;
using ReviewService.Application.EvaluationPoints.Interfaces;
using ReviewService.Application.EvaluationPoints.Services;
using ReviewService.Application.ImportanceLevels.Interfaces;
using ReviewService.Application.ImportanceLevels.Services;
using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Application.ReviewSessions.Services;
using ReviewService.Application.ReviewTemplates.Interfaces;
using ReviewService.Application.ReviewTemplates.Services;
using ReviewService.Domain.Entites;
using ReviewService.Infrastructure.Persistance;
using ReviewService.Infrastructure.Persistance.Repositories;
using ReviewService.Web.Server.AutoMapperProfiles;

namespace ReviewService.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContext, ReviewServiceDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LocalConnection")));
            services.AddControllersWithViews();
            services.AddRazorPages();
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile<AutoMapperProfile>());
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddTransient<IRepository<Area>,Repository<Area>>();
            services.AddTransient<IRepository<EvaluationPoint>, Repository<EvaluationPoint>>();
            services.AddTransient<IRepository<EvaluationPointsTemplate>, Repository<EvaluationPointsTemplate>>();
            services.AddTransient<IRepository<ImportanceLevel>, Repository<ImportanceLevel>>();
            services.AddTransient<IRepository<ReviewSession>, Repository<ReviewSession>>();
            services.AddTransient<IRepository<ReviewTemplate>, Repository<ReviewTemplate>>();

            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IEvaluationPointService, EvaluationPointService>();
            services.AddTransient<IImportanceLevelService, ImportanceLevelService>();
            services.AddTransient<IReviewSessionService, ReviewSessionService>();
            services.AddTransient<IReviewTemplateService, ReviewTemplateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
