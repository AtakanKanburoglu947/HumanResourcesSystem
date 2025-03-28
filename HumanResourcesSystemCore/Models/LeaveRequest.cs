using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
   
    public class LeaveRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public bool? IsAccepted { get; set; } = null;
        [ForeignKey("User")]

        public string UserId { get; set; }

        public User User { get; set; }
        [ForeignKey("Manager")]

        public string? ManagerId { get; set; }
        public User? Manager { get; set; }
    }
}
