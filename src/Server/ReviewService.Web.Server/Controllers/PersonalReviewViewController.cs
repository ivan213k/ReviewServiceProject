using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Domain.Entites;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalReviewViewController : ControllerBase
    {
        private readonly IReviewSessionService _reviewSessionService;
        private readonly IMapper _mapper;

        public PersonalReviewViewController(IReviewSessionService reviewSessionService, IMapper mapper)
        {
            _reviewSessionService = reviewSessionService;
            _mapper = mapper;
        }

        [HttpGet("{sessionId}")]
        public async Task<List<FinalReviewAreaApiModel>> GetReviewViewItems(int sessionId) 
        {
            var finalReviewAreas = await _reviewSessionService.GetFinalReviewAreasAsync(sessionId);
            return _mapper.Map<List<FinalReviewAreaApiModel>>(finalReviewAreas);
        }

        [HttpPut("{sessionId}")]
        public async Task UpdateFinalReviewItems(int sessionId, List<FinalReviewAreaApiModel> finalReviewAreasApiModel) 
        {
            var finalReviewAreas = _mapper.Map<List<FinalReviewArea>>(finalReviewAreasApiModel);
            await _reviewSessionService.UpdateFinalReviewsAsync(sessionId, finalReviewAreas);
        }
    }
}
