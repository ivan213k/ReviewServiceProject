using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Application.ReviewTemplates.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewSessionController : ControllerBase
    {
        private readonly IReviewSessionService _reviewSessionService;
        private readonly IReviewTemplateService _reviewTemplateService;
        private readonly IMapper _mapper;
        public ReviewSessionController(IReviewSessionService reviewSessionService, IReviewTemplateService reviewTemplateService, IMapper mapper)
        {
            _reviewSessionService = reviewSessionService;
            _reviewTemplateService = reviewTemplateService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ReviewSessionApiModel>> GetReviewSessions()
        {
            var reviewSessions = await _reviewSessionService.GetReviewSessionsAsync();
            return _mapper.Map<List<ReviewSessionApiModel>>(reviewSessions);
        }

        [HttpPost("{templateId}")]
        public async Task CreateReviewSession(int templateId, [FromBody] ReviewSessionApiModel reviewSessionApiModel) 
        {
            var reviewSession = _mapper.Map<ReviewSession>(reviewSessionApiModel);
            var reviewTemplate = await _reviewTemplateService.GetByIdAsync(templateId);
            await _reviewSessionService.CreateReviewSessionAsync(reviewTemplate, reviewSession);
        }

        [HttpPut]
        public async Task PublishReviewSession([FromBody] ReviewSessionApiModel reviewSessionApiModel)
        {
            var reviewSession = await _reviewSessionService.GetByIdAsync(reviewSessionApiModel.Id);
            if (reviewSession is null)
            {
                return;
            }
            await _reviewSessionService.PublishReviewSessionAsync(reviewSession);
        }

        [HttpDelete]
        public async Task DeleteReviewSession(int id)
        {
            var reviewSession = await _reviewSessionService.GetByIdAsync(id);
            if (reviewSession is null)
            {
                return;
            }
            await _reviewSessionService.DeleteReviewSessionAsync(reviewSession);
        }
    }
}
