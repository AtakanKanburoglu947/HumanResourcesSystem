using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using HumanResourcesSystemCore;
using HumanResourcesSystemRepository.Repositories;
using HumanResourcesSystemRepository;
using HumanResourcesSystemService.Services;
using HumanResourcesSystemService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResourcesSystem
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(DtoMapper).Assembly);
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICookieRepository, CookieRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddAuthentication("CustomSchemeAuthentication")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomSchemeAuthentication", options => { });
            services.AddHttpContextAccessor();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });
            return services;
        }
    }
}
