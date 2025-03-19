using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("login", "Email ve şifre boş bırakılamaz.");
                return View(login);
            }
            try
            {
                await _authService.Login(login);
                return Redirect("/");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("login",exception.Message);
                return View();
            }
        }
    }
}
