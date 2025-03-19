using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class WorkReport
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("User")]

        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Reviewer")]

        public string ReviewerId { get; set; }
        public User Reviewer { get; set; }

    }
}
