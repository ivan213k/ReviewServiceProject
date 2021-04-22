namespace ReviewService.Domain.Entites
{
    public class FinalReviewEvaluation
    {
        public FinalReviewEvaluation()
        {

        }

        public FinalReviewEvaluation(int areaItemId, string finalPoint, string finalComment = "")
        {
            AreaItemId = areaItemId;
            FinalPoint = finalPoint;
            FinalComment = finalComment;
        }

        public int AreaItemId { get; set; }
        public string FinalPoint { get; set; }
        public string FinalComment { get; set; }
    }
}
