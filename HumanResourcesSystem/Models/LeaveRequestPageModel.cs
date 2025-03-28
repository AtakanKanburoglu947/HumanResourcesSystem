using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class LeaveRequestPageModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Reason { get; set; }
        public string? UserId { get; set; }
        public string? ManagerId { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
