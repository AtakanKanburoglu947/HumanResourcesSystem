using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<string> Generate();
        Task Validate(string refreshToken);
    }
}
