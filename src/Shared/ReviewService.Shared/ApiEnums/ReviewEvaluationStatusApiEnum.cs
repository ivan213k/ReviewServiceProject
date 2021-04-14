using System.Runtime.Serialization;

namespace ReviewService.Shared.ApiEnums
{
    public enum ReviewEvaluationStatusApiEnum
    {
        [EnumMember(Value = "Not Started")]
        NotStarted,

        [EnumMember(Value = "In Progress")]
        InProgress,

        [EnumMember(Value = "Finished")]
        Finished
    }
}
