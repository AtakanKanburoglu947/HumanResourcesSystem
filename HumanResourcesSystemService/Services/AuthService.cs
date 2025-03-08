using HumanResourcesSystemCore;
using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository _authRepository;
        private IUnitOfWork _unitOfWork;
        public AuthService(IAuthRepository authRepository, IUnitOfWork unitOfWork)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task ChangePassword(string newPassword, string email)
        {
            await _authRepository.ChangePassword(newPassword, email);
            await _unitOfWork.CommitAsync();
        }

        public async Task Login(Login login)
        {
            await _authRepository.Login(login);
        }

        public void Logout()
        {
            _authRepository.Logout();
        }

        public async Task Register(Register register)
        {
            await _authRepository.Register(register);
            await _unitOfWork.CommitAsync();
        }
    }
}
