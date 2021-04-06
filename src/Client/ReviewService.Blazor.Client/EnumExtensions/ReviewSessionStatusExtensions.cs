using ReviewService.Shared.ApiEnums;

namespace ReviewService.Blazor.Client.EnumExtensions
{
    public static class ReviewSessionStatusExtensions
    {
        public static string ToUserFriendlyString(this ReviewSessionStatusApiEnum sessionStatus) 
        {
            switch (sessionStatus)
            {
                case ReviewSessionStatusApiEnum.New:
                    return "New Review Session";
                case ReviewSessionStatusApiEnum.Published:
                    return "Review Published";
                case ReviewSessionStatusApiEnum.InProgress:
                    return "In Progress";
                case ReviewSessionStatusApiEnum.Submitted:
                    return "Review Submitted";
                case ReviewSessionStatusApiEnum.Canceled:
                    return "Review Canceled";
                default:
                    return sessionStatus.ToString();
            }
        }
    }
}
