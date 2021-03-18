using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ReviewSessions.Interfaces;
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
