using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public string? DepartmentId { get; set; }
        public string? ManagerId { get; set; }
        public string? CompanyId { get; set; }
    }
}
