using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.AuthModels
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Expired => DateTime.UtcNow > ExpireDate;
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
