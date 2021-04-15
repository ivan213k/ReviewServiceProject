namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class EvaluationAreaItemApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MidEvaluationPoint { get; set; }
        public string EvaluationPoint { get; set; }
        public string Comment { get; set; }
    }
}