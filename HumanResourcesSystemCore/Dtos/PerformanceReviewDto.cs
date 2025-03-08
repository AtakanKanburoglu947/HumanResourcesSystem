using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Dtos
{
    public class PerformanceReviewDto
    {
        public DateTime ReviewDate { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
        public string UserId { get; set; }
        public string ReviewerId { get; set; }
    }
}
