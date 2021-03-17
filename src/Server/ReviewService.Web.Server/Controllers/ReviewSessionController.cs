using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewSessionController : ControllerBase
    {
        private readonly IReviewSessionService _reviewSessionService;
        private readonly IMapper _mapper;
        public ReviewSessionController(IReviewSessionService reviewSessionService, IMapper mapper)
        {
            _reviewSessionService = reviewSessionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ReviewSessionApiModel>> GetReviewSessions()
        {
            var reviewSessions = await _reviewSessionService.GetReviewSessionsAsync();
            return _mapper.Map<List<ReviewSessionApiModel>>(reviewSessions);
        }

        [HttpPost]
        public async Task CreateReviewSession([FromBody] ReviewSessionApiModel reviewSessionApiModel, ReviewTemplateApiModel reviewTemplateApiModel)
        {
            var reviewTemplate = _mapper.Map<ReviewTemplate>(reviewTemplateApiModel);
            var reviewSession = _mapper.Map<ReviewSession>(reviewSessionApiModel);
            
            await _reviewSessionService.CreateReviewSessionAsync(reviewTemplate, reviewSession);
        }

        [HttpPut]
        public async Task PublishReviewSession([FromBody] ReviewSessionApiModel reviewSessionApiModel)
        {
            var reviewSessions = await _reviewSessionService.GetReviewSessionsAsync();
            var reviewSession = reviewSessions.FirstOrDefault(r => r.Id == reviewSessionApiModel.Id);
            if (reviewSession is null)
            {
                return;
            }
            await _reviewSessionService.PublishReviewSessionAsync(reviewSession);
        }

        [HttpDelete]
        public async Task DeleteReviewSession(int id)
        {
            var reviewSessions = await _reviewSessionService.GetReviewSessionsAsync();
            var reviewSession = reviewSessions.FirstOrDefault(r => r.Id == id);
            if (reviewSession is null)
            {
                return;
            }
            await _reviewSessionService.DeleteReviewSessionAsync(reviewSession);
        }
    }
}
