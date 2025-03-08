using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class WorkReport
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
    }
}
