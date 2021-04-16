using ReviewService.Application.Repository.Interfaces;
using ReviewService.Application.ReviewSessions.Interfaces;
using ReviewService.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewService.Domain.Enums;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using System.Linq;

namespace ReviewService.Application.ReviewSessions.Services
{
    public class ReviewSessionService : IReviewSessionService
    {
        private readonly IReviewSessionRepository _reviewSessionRepository;
        private readonly IEvaluationPointTemplateRepository _evaluationPointsRepository;
        public ReviewSessionService(IReviewSessionRepository reviewSessionRepository, IEvaluationPointTemplateRepository evaluationPointsRepository)
        {
            _reviewSessionRepository = reviewSessionRepository;
            _evaluationPointsRepository = evaluationPointsRepository;
        }
        public async Task CreateReviewSessionAsync(ReviewTemplate template, ReviewSession reviewSession)
        {
            var reviewSessionJsonModel = new ReviewSessionJsonModel(template.Areas);
            reviewSession.Session_json = SerializeReviewSessionToJson(reviewSessionJsonModel);
            reviewSession.EvaluationPointsTemplateId = template.EvaluationPointsTemplateId;
            reviewSession.MidEvaluationPointId = template.MidEvaluationPointId;
            await _reviewSessionRepository.CreateAsync(reviewSession);
            FillReviewEvaluationsJson(reviewSession.ReviewEvaluations, template.Areas, template.MidEvaluationPoint.Name);
            await _reviewSessionRepository.UpdateAsync(reviewSession);
        }

        private void FillReviewEvaluationsJson(List<ReviewEvaluation> reviewEvaluations, List<Area> areas, string midEvaluationPoint)
        {
            if (reviewEvaluations is null)
            {
                return;
            }
            foreach (var reviewEvaluation in reviewEvaluations)
            {
                if (string.IsNullOrEmpty(reviewEvaluation.Evaluation_json))
                {
                    EvaluationJsonModel evaluationJsonModel = new EvaluationJsonModel();
                    evaluationJsonModel.ReviewEvaluationId = reviewEvaluation.Id;
                    evaluationJsonModel.Areas = ConvertToEvaluationAreas(areas, midEvaluationPoint);
                    reviewEvaluation.Evaluation_json = JsonConvert.SerializeObject(evaluationJsonModel);
                }    
            }
        }

        public async Task DeleteReviewSessionAsync(ReviewSession reviewSession)
        {
            await _reviewSessionRepository.DeleteAsync(reviewSession);
        }

        public async Task<List<ReviewSession>> GetReviewSessionsAsync()
        {
            return await _reviewSessionRepository.GetAllReviewSessionsAsync();
        }
        public async Task PublishReviewSessionAsync(ReviewSession reviewSession)
        {
            if (reviewSession.Status == ReviewSessionStatus.New)
            {
                reviewSession.Status = ReviewSessionStatus.Published;
                await _reviewSessionRepository.UpdateAsync(reviewSession);
            }
        }
        public async Task CancelReviewSessionAsync(ReviewSession reviewSession)
        {
            if (reviewSession.Status == ReviewSessionStatus.Published)
            {
                reviewSession.Status = ReviewSessionStatus.Canceled;
                await _reviewSessionRepository.UpdateAsync(reviewSession);
            }
        }

        public async Task<ReviewSession> GetByIdAsync(int id)
        {
            return await _reviewSessionRepository.GetReviewSessionByIdAsync(id);
        }

        public async Task UpdateReviewSessionAsync(ReviewSession reviewSession)
        {
            var areas = JsonConvert.DeserializeObject<List<Area>>(reviewSession.Session_json);

            var midPoint = await GetMidPointAsync(reviewSession);
            FillReviewEvaluationsJson(reviewSession.ReviewEvaluations, areas, midPoint);
            await _reviewSessionRepository.UpdateAsync(reviewSession);
        }

        private string SerializeReviewSessionToJson(ReviewSessionJsonModel reviewSessionJsonModel)
        {
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(Area), nameof(Area.ReviewTemplates));

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;
            return JsonConvert.SerializeObject(reviewSessionJsonModel, serializerSettings);
        }

        public async Task<List<FinalReviewArea>> GetFinalReviewAreasAsync(int sessionId)
        {
            var personalReviewAreas = new List<FinalReviewArea>();
            var reviewSession = await _reviewSessionRepository.GetReviewSessionByIdAsync(sessionId);
            var midPoint = await GetMidPointAsync(reviewSession);
            var reviewEvaluations = reviewSession.ReviewEvaluations;
            List<EvaluationJsonModel> evaluations = GetEvaluations(reviewEvaluations);
            var areas = GetAreasFromJson(reviewSession);
            foreach (var area in areas)
            {
                var finalReviewAreaItems = new List<FinalReviewAreaItem>();
                foreach (var areaItem in area.AreaItems)
                {
                    var reviewers = new List<Reviewer>();
                    foreach (var evaluation in evaluations)
                    {
                        var evaluationAreaItems = GetEvaluationAreaItems(evaluation.Areas);
                        var evaluationAreaItem = evaluationAreaItems.Where(r => r.Id == areaItem.Id).FirstOrDefault();
                        if (evaluationAreaItem != null)
                        {
                            var reviewerName = GetReviewerName(reviewSession, evaluation.ReviewEvaluationId);
                            reviewers.Add(new Reviewer()
                            {
                                Name = reviewerName,
                                Comment = evaluationAreaItem.Comment,
                                Point = evaluationAreaItem.EvaluationPoint
                            });
                        }
                    }
                    finalReviewAreaItems.Add(new FinalReviewAreaItem()
                    {
                        Id = areaItem.Id,
                        Name = areaItem.Name,
                        Middle = midPoint,
                        Reviewers = reviewers,
                        FinalReview = GetFinalReviewPoint(reviewSession,areaItem.Id)
                    });
                }
                personalReviewAreas.Add(new FinalReviewArea { Name = area.Name, ViewItems = finalReviewAreaItems });
            }

            return personalReviewAreas;
        }

        private async Task<string> GetMidPointAsync(ReviewSession reviewSession)
        {
            var evaluationPointsTemplate = await _evaluationPointsRepository.GetEvaluationPointsTemplateByIdAsync(reviewSession.EvaluationPointsTemplateId);
            string midPoint = evaluationPointsTemplate.EvaluationPoints.Where(r => r.Id == reviewSession.MidEvaluationPointId).First().Name;
            return midPoint;
        }

        private string GetFinalReviewPoint(ReviewSession reviewSession, int areaItemId)
        {
            var reviewSessionJsonModel = JsonConvert.DeserializeObject<ReviewSessionJsonModel>(reviewSession.Session_json);
            if (reviewSessionJsonModel.FinalReviewEvaluations is null)
            {
                return "";
            }
            var finalPoint = reviewSessionJsonModel.FinalReviewEvaluations.Where(r => r.AreaItemId == areaItemId).Select(r=>r.FinalPoint).FirstOrDefault();
            return finalPoint;
        }

        public async Task UpdateFinalReviewsAsync(int sessionId, List<FinalReviewArea> finalReviewAreas) 
        {
            var reviewSession = await _reviewSessionRepository.GetReviewSessionByIdAsync(sessionId);
            var reviewSessionJsonModel = JsonConvert.DeserializeObject<ReviewSessionJsonModel>(reviewSession.Session_json);
            var finalReviews = new List<FinalReviewEvaluation>();
            foreach (var area in finalReviewAreas)
            {
                finalReviews.AddRange(area.ViewItems.Select(viewItem => new FinalReviewEvaluation(viewItem.Id, viewItem.FinalReview)));
            }
            reviewSessionJsonModel.FinalReviewEvaluations = finalReviews;
            reviewSession.Session_json = SerializeReviewSessionToJson(reviewSessionJsonModel);
            await _reviewSessionRepository.UpdateAsync(reviewSession);
        }

        private string GetReviewerName(ReviewSession reviewSession, int reviewEvaluationId)
        {
            return reviewSession.ReviewEvaluations.First(r => r.Id == reviewEvaluationId).Reviewer;
        }

        private List<EvaluationJsonModel> GetEvaluations(List<ReviewEvaluation> reviewEvaluations)
        {
            var evaluations = new List<EvaluationJsonModel>();
            reviewEvaluations.ForEach(r => evaluations.Add(JsonConvert.DeserializeObject<EvaluationJsonModel>(r.Evaluation_json)));

            return evaluations;
        }

        private List<Area> GetAreasFromJson(ReviewSession reviewSession)
        {
            var sessionJsonModel = JsonConvert.DeserializeObject<ReviewSessionJsonModel>(reviewSession.Session_json);
            return sessionJsonModel.Areas;
        }
        private List<EvaluationAreaItem> GetEvaluationAreaItems(List<EvaluationArea> evaluationAreas) 
        {
            var areaItems = new List<EvaluationAreaItem>();
            evaluationAreas.ForEach(a => areaItems.AddRange(a.AreaItems));

            return areaItems;
        }

        private List<EvaluationArea> ConvertToEvaluationAreas(List<Area> areas, string midEvaluationPoint)
        {
            var evaluationAreas = new List<EvaluationArea>();
            foreach (var area in areas)
            {
                var evaluationAreaItems = new List<EvaluationAreaItem>();
                foreach (var areaItem in area.AreaItems)
                {
                    evaluationAreaItems.Add(new EvaluationAreaItem()
                    {
                        Id = areaItem.Id,
                        Name = areaItem.Name,
                        MidEvaluationPoint = midEvaluationPoint
                    });
                }
                evaluationAreas.Add(new EvaluationArea()
                {
                    Id = area.Id,
                    Name = area.Name,
                    AreaItems = evaluationAreaItems
                });
            }
            return evaluationAreas;
        }
    }
}
