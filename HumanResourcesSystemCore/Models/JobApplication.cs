using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class JobApplication
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CandiadateName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidatePhone { get; set; }
        public string ResumeUrl { get; set; }

        [ForeignKey("Reviewer")]
        public string ReviewerId { get; set; }
        public User Reviewer { get; set; }
        public DateTime ApplicationDate { get; set; }
        }
    }
