using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
    public class AccountPageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdmin { get; set; }
    }
}
