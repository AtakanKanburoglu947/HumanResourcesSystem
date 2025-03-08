using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class DatabaseLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
