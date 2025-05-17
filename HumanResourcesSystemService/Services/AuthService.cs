using HumanResourcesSystemCore;
using HumanResourcesSystemCore.AuthDtos;
using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
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
        public async Task ChangePassword(string newPassword, string userId)
        {
            await _authRepository.ChangePassword(newPassword, userId);
            await _unitOfWork.CommitAsync();
        }

        public AccountDto GetAccountDetailsFromToken()
        {
            return _authRepository.GetAccountDetailsFromToken();
        }

        public async Task<IQueryable<User>> UsersWithRole(string roleName)
        {
            return await _authRepository.UsersWithRole(roleName);
        }

        public async Task Login(Login login)
        {
            await _authRepository.Login(login);
        }

        public void Logout()
        {
            _authRepository.Logout();
        }

        public async Task<AuthDto> RefreshToken(string refreshToken)
        {
          return await _authRepository.RefreshToken(refreshToken);
        }

        public async Task Register(Register register)
        {
            await _authRepository.Register(register);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveExpiredRefreshTokens(string userId)
        {
            _authRepository.RemoveExpiredRefreshTokens(userId);
            await _unitOfWork.CommitAsync();
        }

        public bool ValidateToken(string token)
        {
            return _authRepository.ValidateToken(token);
        }

        public Task<bool> HasRole(string roleName, User user)
        {
            return _authRepository.HasRole(roleName, user); 
        }
    }
}
