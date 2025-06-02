using HumanResourcesSystemCore.AuthDtos;
using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemRepository.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICookieRepository _cookieRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDbContext;
        public AuthRepository(
           UserManager<User> userManager,
           ITokenRepository tokenRepository,
           IRefreshTokenRepository refreshTokenRepository,
           ICookieRepository cookieRepository,
           AppDbContext appDbContext,
           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _cookieRepository = cookieRepository;
            _httpContextAccessor = httpContextAccessor;
            _appDbContext = appDbContext;
        }

        public AccountDto GetAccountDetailsFromToken()
        {
            return _tokenRepository.Decode();
        }

        public async Task ChangePassword(string newPassword, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Şifre değiştirme başarısız");
            }
        }

        public async Task Login(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                throw new Exception("Yanlış bilgiler.");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                };

            string token = _tokenRepository.Generate(claims);

            var existingRefreshToken = await _refreshTokenRepository.GetByUserIdAsync(user.Id);

            string refreshToken;

            if (existingRefreshToken != null && _refreshTokenRepository.Validate(existingRefreshToken))
            {
                refreshToken = existingRefreshToken.Token;
            }
            else
            {
                refreshToken = await _refreshTokenRepository.Generate(user.Id);
            }
            _cookieRepository.Set(_httpContextAccessor.HttpContext.Response, "token", token);
            _cookieRepository.Set(_httpContextAccessor.HttpContext.Response, "refreshtoken", refreshToken);
        }


        public void Logout()
        {
            _cookieRepository.Remove(_httpContextAccessor.HttpContext.Response, "token");
            _cookieRepository.Remove(_httpContextAccessor.HttpContext.Response, "refreshtoken");
        }

        public async Task<AuthDto> RefreshToken(string refreshToken)
        {
            RefreshToken existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (existingToken == null || !_refreshTokenRepository.Validate(existingToken))
            {
                throw new UnauthorizedAccessException("Yanlış veya süresi dolmuş token.");
            }
            var user = await _userManager.FindByIdAsync(existingToken.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),

            };
            string newRefreshToken = await _refreshTokenRepository.Generate(user.Id);
            existingToken.Token = newRefreshToken;
            existingToken.ExpireDate = DateTime.UtcNow.AddDays(7);  
            await _refreshTokenRepository.Update(existingToken);
            string token = _tokenRepository.Generate(claims);
            return new AuthDto() { RefreshToken = existingToken, Token = token };
        }



        public async Task Register(Register register)
        {
            User user = new User
            {
                BirthDate = register.BirthDate,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = $"{register.FirstName}{register.LastName}"
            };
            var result = await _userManager.CreateAsync(user,register.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"User");
            }
            else
            {
                throw new Exception("Kullanıcı kayıdı başarısız.");
            }
        }

        public void RemoveExpiredRefreshTokens(string userId)
        {
            _refreshTokenRepository.RemoveExpiredRefreshTokens(userId);
        }

        public bool ValidateToken(string token)
        {
            
            return _tokenRepository.Validate(token);
        }

        public async Task<IQueryable<User>> UsersWithRole(string roleName)
        {
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == roleName.ToUpper());
           

            var roleUserIds = await _appDbContext.UserRoles
                .Where(x => x.RoleId == role.Id)
                .Select(x => x.UserId)
                .ToListAsync();

            var usersWithRoles = _appDbContext.Users
                .Where(u => roleUserIds.Contains(u.Id));
            return usersWithRoles;
        }

        public async Task<bool> HasRole(string roleName, User user)
        {
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == roleName.ToUpper());
            var userWithRole = await _appDbContext.UserRoles.FirstOrDefaultAsync(x=>x.UserId == user.Id && x.RoleId == role.Id);
            return userWithRole != null;
        }
    }
}
