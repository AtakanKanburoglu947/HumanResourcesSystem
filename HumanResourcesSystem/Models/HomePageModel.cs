using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class HomePageModel
    {
        public List<EventModel>? Events { get; set; }
        public User? User { get; set; }
        public string? CompanyName { get; set; }
        public string? ManagerName { get; set; }
        public string? DepartmentName { get; set; }  
        public bool IsUserManager { get; set; }

    }
}
