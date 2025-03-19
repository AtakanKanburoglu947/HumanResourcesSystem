using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]
    public class WorkReportController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IService<WorkReport, WorkReportDto> _workReportService;
        private readonly IUserService _userService;
        public WorkReportController(IAuthService authService, IService<WorkReport,WorkReportDto> workReportService, IUserService userService)
        {
            _authService = authService;
            _workReportService = workReportService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var workReports = _workReportService.Where(x => x.ReportDate, x=>x.UserId == accountDto.Id);
            WorkReportPageModel workReportPageModel = new WorkReportPageModel()
            {
                WorkReports = workReports,
            };
            return View(workReportPageModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(WorkReportPageModel workReportPageModel)
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            User user = await _userService.FindAsync(accountDto.Id);
            WorkReportDto workReportDto = new WorkReportDto() {
                Description = workReportPageModel.Description,
                ReportDate = DateTime.Now,
                Title = workReportPageModel.Title,
                UserId = accountDto.Id,
                ReviewerId = user.ManagerId
            };
            await _workReportService.AddAsync(workReportDto);
            return RedirectToAction("Index");
        }
    }
}
