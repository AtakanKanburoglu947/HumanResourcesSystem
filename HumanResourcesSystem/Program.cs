using HumanResourcesSystem;
using HumanResourcesSystemCore;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using HumanResourcesSystemRepository;
using HumanResourcesSystemRepository.Repositories;
using HumanResourcesSystemService;
using HumanResourcesSystemService.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(DtoMapper).Assembly);
builder.Services.AddScoped<ITokenRepository,TokenRepository>();
builder.Services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
builder.Services.AddScoped<ICookieRepository,CookieRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication("CustomSchemeAuthentication")
                        .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomSchemeAuthentication", options => { });
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/Auth/AccessDenied"; 
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
