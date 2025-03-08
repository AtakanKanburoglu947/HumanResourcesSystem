using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Models
{
    public class Department
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public List<User> Users { get; set; } = new();

    }
}
