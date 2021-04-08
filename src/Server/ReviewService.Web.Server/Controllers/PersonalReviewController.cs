using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.PersonalReviewEvaluations.Interfaces;
using ReviewService.Shared.ApiModels;
using System;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalReviewController : ControllerBase
    {
        private readonly IReviewEvaluationService _reviewEvaluationService;
        private readonly IMapper _mapper;

        public PersonalReviewController(IReviewEvaluationService reviewEvaluationService, IMapper autoMapper)
        {
            _reviewEvaluationService = reviewEvaluationService;
            _mapper = autoMapper;
        }

        [HttpGet("{guid}")]
        public async Task<ReviewEvaluationApiModel> GetByGuid(Guid guid)
        {
            var reviewEvaluation = await _reviewEvaluationService.GetByGuidAsync(guid);
         
            return _mapper.Map<ReviewEvaluationApiModel>(reviewEvaluation);
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
