using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IRefreshTokenService
    {
        Task Generate();
        Task Validate(string refreshToken);
    }
}
