using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]

    public class PasswordController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService; 
        public PasswordController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public async Task<IActionResult> ChangePassword(string newPassword)
        {
            var accountDto = _authService.GetAccountDetailsFromToken();
            var user = await _userService.FindAsync(accountDto.Id);
            ViewData["HasRole"] = await _authService.HasRole("manager",user);
            await _authService.ChangePassword(newPassword, accountDto.Id);
            return RedirectToAction("Index");
        }
    }
}
