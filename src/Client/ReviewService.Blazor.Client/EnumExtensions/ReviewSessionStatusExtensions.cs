using ReviewService.Shared.ApiEnums;
using System.Linq;
using System.Runtime.Serialization;

namespace ReviewService.Blazor.Client.EnumExtensions
{
    public static class ReviewSessionStatusExtensions
    {
        public static string ToUserFriendlyString(this ReviewSessionStatusApiEnum sessionStatus) 
        {
            var memberInfo = sessionStatus.GetType().GetMember(sessionStatus.ToString());
            var attribute = memberInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attribute is null)
            {
                return sessionStatus.ToString();
            }
            return attribute.Value;
        }
    }
}
