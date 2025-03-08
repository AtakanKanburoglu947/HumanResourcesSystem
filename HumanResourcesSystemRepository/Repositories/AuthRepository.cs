using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemRepository.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ICookieRepository _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepository(
           UserManager<User> userManager,
           ITokenRepository tokenRepository,
           IRefreshTokenRepository refreshTokenRepository,
           ICookieRepository cookieService,
           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ChangePassword(string newPassword, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Password change failed");
            }
        }

        public async Task Login(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                throw new Exception("Invalid credentials.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            string token = _tokenRepository.Generate(claims);
            string refreshToken = await _refreshTokenRepository.Generate();
            _cookieService.Set(_httpContextAccessor.HttpContext.Response, "token", token);
            _cookieService.Set(_httpContextAccessor.HttpContext.Response, "refreshtoken", refreshToken);
        }

        public void Logout()
        {
            _cookieService.Remove(_httpContextAccessor.HttpContext.Response, "token");
            _cookieService.Remove(_httpContextAccessor.HttpContext.Response, "refreshtoken");
        }

        public async Task Register(Register register)
        {
            User user = new User
            {
                BirthDate = register.BirthDate,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };
            var result = await _userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User registration failed.");
            }
        }
    }
}
