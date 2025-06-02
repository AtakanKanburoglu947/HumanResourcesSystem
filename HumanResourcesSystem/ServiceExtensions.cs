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
using Microsoft.AspNetCore.Authorization;

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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));
            services.AddScoped<IUserService, UserService>();
            services.AddAuthentication("CustomSchemeAuthentication")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomSchemeAuthentication", options => { });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerOnly", policy =>
                    policy.Requirements.Add(new RoleRequirement("manager")));
                options.AddPolicy("AdminOnly", policy =>
                    policy.Requirements.Add(new RoleRequirement("ADMIN")));
                options.AddPolicy("UserOnly", policy =>
                    policy.Requirements.Add(new RoleRequirement("USER")));
            });

            services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddHttpContextAccessor();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Login";
            });
            return services;
        }
    }
}
