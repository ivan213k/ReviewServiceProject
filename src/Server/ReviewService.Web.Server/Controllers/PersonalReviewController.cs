using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.PersonalReviewEvaluations.Interfaces;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonalReviewController : ControllerBase
    {
        private readonly IReviewEvaluationService _reviewEvaluationService;
        private readonly IMapper _mapper;

        public PersonalReviewController(IReviewEvaluationService reviewEvaluationService, IMapper autoMapper)
        {
            _reviewEvaluationService = reviewEvaluationService;
            _mapper = autoMapper;
        }
        [HttpGet]
        public async Task<List<PersonalReviewApiModel>> GetPersonalReviews() 
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personalReviews = await _reviewEvaluationService.GetPersonalReviewsAsync(currentUserId);
            return _mapper.Map<List<PersonalReviewApiModel>>(personalReviews);
        }
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            var reviewEvaluation = await _reviewEvaluationService.GetByGuidAsync(guid);
            if (reviewEvaluation is null)
            {
                return NotFound();
            }
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != reviewEvaluation.UserId)
            {
                return Forbid();
            }
            return Ok(_mapper.Map<ReviewEvaluationApiModel>(reviewEvaluation));
        }

        [HttpPut]
        public async Task UpdateReviewEvaluation(ReviewEvaluationApiModel reviewEvaluationApiModel)
        {
            var reviewEvaluation = await _reviewEvaluationService.GetByIdAsync(reviewEvaluationApiModel.Id);
            if (reviewEvaluation is null)
            {
                return;
            }
            _mapper.Map(reviewEvaluationApiModel, reviewEvaluation);
            await _reviewEvaluationService.UpdateReviewEvaluationAsync(reviewEvaluation);
        }
    }
}
