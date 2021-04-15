using System.Runtime.Serialization;

namespace ReviewService.Shared.ApiEnums
{
    public enum ReviewSessionStatusApiEnum
    {
        [EnumMember(Value = "New Review Session")]
        New,

        [EnumMember(Value = "Review Published")]
        Published,

        [EnumMember(Value = "In Progress")]
        InProgress,

        [EnumMember(Value = "Review Submitted")]
        Submitted,

        [EnumMember(Value = "Review Canceled")]
        Canceled
    }
}
