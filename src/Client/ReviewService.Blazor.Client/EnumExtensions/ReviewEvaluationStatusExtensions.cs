using ReviewService.Shared.ApiEnums;

namespace ReviewService.Blazor.Client.EnumExtensions
{
    public static class ReviewEvaluationStatusExtensions
    {
        public static string ToUserFriendlyString(this ReviewEvaluationStatusApiEnum reviewEvaluationStatus)
        {
            switch (reviewEvaluationStatus)
            {
                case ReviewEvaluationStatusApiEnum.NotStarted:
                    return "Not started";
                case ReviewEvaluationStatusApiEnum.InProgress:
                    return "In progress";
                case ReviewEvaluationStatusApiEnum.Finished:
                    return "Finished";
                default:
                    return reviewEvaluationStatus.ToString();
            }
        }
    }
}
