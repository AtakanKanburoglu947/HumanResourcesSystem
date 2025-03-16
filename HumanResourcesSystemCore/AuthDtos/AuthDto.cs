using HumanResourcesSystemCore.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.AuthDtos
{
    public class AuthDto
    {
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
