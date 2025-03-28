using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IService<Department,DepartmentDto> _service;
        public AccountController(IAuthService authService,IUserService userService, IService<Department, DepartmentDto> service)
        {
            _authService = authService;
            _userService = userService;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            User user = await _userService.FindAsync(accountDto.Id);
            Department? department = await _service.FindAsync(user.DepartmentId);
            if (department != null)
            {
            user.Department = department;
                
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            _authService.Logout();
            return Redirect("/Login");
        }
    }
}
