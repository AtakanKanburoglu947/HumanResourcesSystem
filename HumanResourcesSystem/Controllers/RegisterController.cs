using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthService _authService;
        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Register register)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("register", "Ad, soyad, doğum tarihi, email ve şifre boş bırakılamaz.");

                return View(register);
            }
            try
            {
                await _authService.Register(register);
                return Redirect("/register");
            }
            catch (Exception exception)
            {

                ModelState.AddModelError("register", exception.Message);
                return View(register);
            }
        }
    }
}
