using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class WorkReportPageModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReportDate { get; set; }
        public List<WorkReport> WorkReports { get; set; }
    }
}
