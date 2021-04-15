using ReviewService.Shared.ApiEnums;
using System.Linq;
using System.Runtime.Serialization;

namespace ReviewService.Blazor.Client.EnumExtensions
{
    public static class ReviewEvaluationStatusExtensions
    {
        public static string ToUserFriendlyString(this ReviewEvaluationStatusApiEnum reviewEvaluationStatus)
        {
            var memberInfo = reviewEvaluationStatus.GetType().GetMember(reviewEvaluationStatus.ToString());
            var attribute = memberInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attribute is null)
            {
                return reviewEvaluationStatus.ToString();
            }
            return attribute.Value;
        }
    }
}
