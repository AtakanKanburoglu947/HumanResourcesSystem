using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Dtos
{
    public class JobApplicationDto
    {
        public string CandiadateName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidatePhone { get; set; }
        public string ResumeUrl { get; set; }
        public string ReviewerId { get; set; }
    }
}
