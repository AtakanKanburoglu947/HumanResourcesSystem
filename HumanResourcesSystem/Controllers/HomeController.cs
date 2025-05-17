using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IService<Announcement, AnnouncementDto> _announcementService;
        private readonly IService<Company, CompanyDto> _companyService;
        private readonly IService<Department, DepartmentDto> _departmentService;
        private readonly IService<EventModel, EventDto> _eventService;
        private readonly IUserService _userService;
        public HomeController(IAuthService authService, IService<EventModel, EventDto> eventService, 
            IService<Announcement, AnnouncementDto> announcementService, IUserService userService, IService<Department, DepartmentDto> departmentService,
            IService<Company,CompanyDto> companyService)
        {
            _authService = authService;
            _eventService = eventService;
            _announcementService = announcementService;
            _userService = userService;
            _departmentService = departmentService;
            _companyService = companyService;
        }
        public async Task<IActionResult> Index()
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            List<EventModel>? events = _eventService
                .Where(x => x.StartDate, x => x.UserId == accountDto.Id)
                .Where(x => x.EndDate.Date == DateTime.Now.Date)
                .ToList();
            List<Announcement>? announcements = _announcementService.GetAll();
            var user = await _userService.FindAsync(accountDto.Id)!;
            var manager = await _userService.FirstOrDefault(x => x.ManagerId == user.ManagerId);
            var company = await _companyService.FindAsync(user.CompanyId);
            var department = await _departmentService.FindAsync(user.DepartmentId)!;
            bool isUserManager = await _authService.HasRole("manager", user);
            
            HomePageModel homePageModel = new HomePageModel() { Announcements = announcements, Events = events, User = user
            , DepartmentName = department?.Name, CompanyName = company?.Name, ManagerName = manager?.UserName, IsUserManager = isUserManager};
            return View(homePageModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
