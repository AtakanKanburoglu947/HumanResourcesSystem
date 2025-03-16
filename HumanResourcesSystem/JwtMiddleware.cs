using HumanResourcesSystemCore.AuthDtos;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using HumanResourcesSystemService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResourcesSystem
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public JwtMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            string path = context.Request.Path.Value?.ToLower();

            if (path == "/login" || path == "/register")
            {
                await _next(context);
                return;
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                var cookieRepository = scope.ServiceProvider.GetRequiredService<ICookieRepository>();
                var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

                string token = cookieRepository.Get(context.Request, "token");

                if (!string.IsNullOrEmpty(token))
                {
                    bool isValid = authService.ValidateToken(token);

                    if (isValid)
                    {
                        await _next(context);
                        return;
                    }
                }

                string refreshToken = cookieRepository.Get(context.Request, "refreshtoken");

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    AuthDto authDto = await authService.RefreshToken(refreshToken);

                    if (!string.IsNullOrEmpty(authDto.Token))
                    {
                        cookieRepository.Set(context.Response, "token", authDto.Token);
                        cookieRepository.Set(context.Response, "refreshtoken", authDto.RefreshToken.Token);
                        await authService.RemoveExpiredRefreshTokens(authDto.RefreshToken.UserId);

                        context.Response.Redirect("/");
                        return;
                    }
                }

                context.Response.Redirect("/login");
            }
        }
    }
}
