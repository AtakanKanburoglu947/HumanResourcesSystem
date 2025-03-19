using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Dtos
{
    public class WorkReportDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public string ReviewerId { get; set; }
    }
}
