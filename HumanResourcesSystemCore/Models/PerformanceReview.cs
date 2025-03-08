using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class PerformanceReview
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime ReviewDate { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
        [ForeignKey("User")]

        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Reviewer")]

        public string ReviewerId { get; set; }
        public User Reviewer { get; set; }
    }
}
