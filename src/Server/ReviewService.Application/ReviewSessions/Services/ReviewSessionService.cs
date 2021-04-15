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
            reviewSession.Session_json = SerializeAreasToJson(template.Areas);
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
            var evaluationPointsTemplate = await _evaluationPointsRepository.GetEvaluationPointsTemplateByIdAsync(reviewSession.EvaluationPointsTemplateId);
            string midPoint = evaluationPointsTemplate.EvaluationPoints.Where(r => r.Id == reviewSession.MidEvaluationPointId).First().Name;
            FillReviewEvaluationsJson(reviewSession.ReviewEvaluations, areas, midPoint);
            await _reviewSessionRepository.UpdateAsync(reviewSession);
        }

        private string SerializeAreasToJson(List<Area> areas)
        {
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(Area), nameof(Area.ReviewTemplates));

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;
            return JsonConvert.SerializeObject(areas, serializerSettings);
        }

        public async Task<List<FinalReviewArea>> GetFinalReviewAreasAsync(int sessionId)
        {
            var personalReviewAreas = new List<FinalReviewArea>();
            var reviewSession = await _reviewSessionRepository.GetReviewSessionByIdAsync(sessionId);

            var reviewEvaluations = reviewSession.ReviewEvaluations;
            List<EvaluationJsonModel> evaluations = GetEvaluations(reviewEvaluations);
            var areas = GetAreasFromJson(reviewSession);
            foreach (var area in areas)
            {
                var finalReviewAreaItems = new List<FinalReviewAreaItem>();
                foreach (var areaItem in area.AreaItems)
                {
                    var areaItemEvaluations = new List<EvaluationAreaItem>();
                    var reviewers = new List<Reviewer>();
                    foreach (var evaluation in evaluations)
                    {
                        var evaluationAreaItems = GetEvaluationAreaItems(evaluation.Areas);
                        var evaluationAreaItem = evaluationAreaItems.Where(r => r.Id == areaItem.Id).FirstOrDefault();
                        if (evaluationAreaItem != null)
                        {
                            var reviewerName = GetReviewerName(reviewSession, evaluation.ReviewEvaluationId);
                            areaItemEvaluations.Add(evaluationAreaItem);
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
                        AreaItemId = areaItem.Id,
                        Name = areaItem.Name,
                        Middle = areaItemEvaluations.First().MidEvaluationPoint,
                        Reviewers = reviewers
                    });
                }
                personalReviewAreas.Add(new FinalReviewArea { Name = area.Name, ViewItems = finalReviewAreaItems });
            }

            return personalReviewAreas;
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
            return JsonConvert.DeserializeObject<List<Area>>(reviewSession.Session_json);
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
