using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class TrainingRecord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CourseName { get; set; }
        public string Provider {  get; set; }
        public string CertificateUrl { get; set; }
        public DateTime CompletionDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
