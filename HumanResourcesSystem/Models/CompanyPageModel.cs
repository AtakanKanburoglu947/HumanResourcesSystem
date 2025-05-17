using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class CompanyPageModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public List<User> Employees { get; set; }
        public List<Department> Departments { get; set; }
        public List<string> Roles { get; set; }
        public string NewDepartment { get; set; }
    }
}
