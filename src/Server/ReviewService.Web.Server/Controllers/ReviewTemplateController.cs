using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ReviewTemplates.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewTemplateController : ControllerBase
    {
        private readonly IReviewTemplateService _reviewTemplateService;
        private readonly IMapper _mapper;
        public ReviewTemplateController(IReviewTemplateService reviewTemplateService, IMapper mapper)
        {
            _reviewTemplateService = reviewTemplateService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ReviewTemplateApiModel>> GetReviewTemplates()
        {
            var reviewTemplates = await _reviewTemplateService.GetReviewTemplatesAsync();
            return _mapper.Map<List<ReviewTemplateApiModel>>(reviewTemplates);
        }

        [HttpPost]
        public async Task AddReviewTemplate([FromBody] ReviewTemplateApiModel reviewTemplateApiModel)
        {
            var reviewTemplate = _mapper.Map<ReviewTemplate>(reviewTemplateApiModel);
            await _reviewTemplateService.AddReviewTemplateAsync(reviewTemplate);
        }

        [HttpPut]
        public async Task UpdateReviewTemplate([FromBody] ReviewTemplateApiModel reviewTemplateApiModel) 
        {
            var reviewTemplates = await _reviewTemplateService.GetReviewTemplatesAsync();
            var reviewTemplate = reviewTemplates.FirstOrDefault(r => r.Id == reviewTemplateApiModel.Id);
            if (reviewTemplate is null)
            {
                return;
            }
            _mapper.Map(reviewTemplateApiModel, reviewTemplate);
            await _reviewTemplateService.UpdateReviewTemplateAsync(reviewTemplate);
        }

        [HttpDelete]
        public async Task DeleteReviewTemplate(int id)
        {
            var reviewTemplates = await _reviewTemplateService.GetReviewTemplatesAsync();
            var reviewTemplate = reviewTemplates.FirstOrDefault(r => r.Id == id);
            if (reviewTemplate is null)
            {
                return;
            }
            await _reviewTemplateService.DeleteReviewTemplateAsync(reviewTemplate);
        }

    }
}
