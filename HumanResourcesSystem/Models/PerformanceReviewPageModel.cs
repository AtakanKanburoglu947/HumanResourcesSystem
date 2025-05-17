using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class PerformanceReviewPageModel
    {
        public List<User> Users { get; set; }
        public List<string> Names {  get; set; }
        public string SelectedUserName { get; set; }
        public int Score { get; set; }
        public string Feedback {  get; set; }
        public List<PerformanceReview> PerformanceReviews { get; set; }
    }
}
