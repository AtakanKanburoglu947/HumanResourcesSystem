using HumanResourcesSystemCore.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IAuthService
    {
        Task Register(Register register);
        Task Login(Login login);
        void Logout();
        Task ChangePassword(string newPassword, string email);
    }
}
