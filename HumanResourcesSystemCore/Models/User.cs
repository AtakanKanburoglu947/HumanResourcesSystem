using HumanResourcesSystemCore.AuthModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
        public class User : IdentityUser
        {
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
            public List<LeaveRequest> LeaveRequests { get; set; } = new();
            public List<PerformanceReview> PerformanceReviews { get; set; } = new();
            public List<RefreshToken> RefreshTokens { get; set; } = new();
            }
}
