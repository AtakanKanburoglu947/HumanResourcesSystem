using HumanResourcesSystemCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanResourcesSystem.Models
{
    public class UserPageModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string? ManagerId { get; set; }
        public User? Manager { get; set; }
        public string? CompanyId { get; set; }
        public Company Company { get; set; }
        public List<User>? Managers { get; set; }
        public List<SelectListItem> ManagerList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> CompanyList { get; set; }

    }
}
