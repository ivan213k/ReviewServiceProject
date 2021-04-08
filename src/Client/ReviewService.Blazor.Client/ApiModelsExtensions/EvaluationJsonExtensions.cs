using Newtonsoft.Json;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;

namespace ReviewService.Blazor.Client.ApiModelsExtensions
{
    public static class EvaluationJsonExtensions
    {
        public static string ConvertToJsonString(this EvaluationJson evaluationJson)
        {
            return JsonConvert.SerializeObject(evaluationJson);
        }

        public static List<EvaluationArea> ConvertToEvaluationAreas(this List<AreaApiModel> areas) 
        {
            List<EvaluationArea> evaluationAreas = new List<EvaluationArea>();
            foreach (var area in areas)
            {
                var evaluationAreaItems = new List<EvaluationAreaItem>();
                foreach (var areaItem in area.AreaItems)
                {
                    evaluationAreaItems.Add(new EvaluationAreaItem()
                    {
                        Name = areaItem.Name
                    });
                }
                evaluationAreas.Add(new EvaluationArea()
                {
                    Name = area.Name,
                    AreaItems = evaluationAreaItems
                });
            }
            return evaluationAreas;
        }
    }
}
