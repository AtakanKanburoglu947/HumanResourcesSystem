using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class DailyTask
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public bool? isFinished { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        
    }
}
