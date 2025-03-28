using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class HomePageModel
    {
        public List<Announcement>? Announcements { get; set; }
        public List<EventModel>? Events { get; set; }
        public User? User { get; set; }
        public string? CompanyName { get; set; }
        public string? ManagerName { get; set; }
        public string? DepartmentName { get; set; }  

    }
}
