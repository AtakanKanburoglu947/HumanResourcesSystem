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
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "UserOnly")]
        public IActionResult Index(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var workReports = _workReportService.Pagination(id, x=>x.UserId == accountDto.Id);
            WorkReportPageModel workReportPageModel = new WorkReportPageModel();
            PaginationModel<WorkReport, WorkReportPageModel> paginationModel = new PaginationModel<WorkReport, WorkReportPageModel>()
            {
                Data = workReportPageModel,
                Dataset = workReports,
                PartialPaginationModel = new PartialPaginationModel() { Count = _workReportService.Where(x=>x.UserId == accountDto.Id).Count},
            };
            return View(paginationModel);
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "UserOnly")]
        [HttpPost]
        public async Task<IActionResult> Add(PaginationModel<WorkReport, WorkReportPageModel> paginationModel)
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            User user = await _userService.FindAsync(accountDto.Id);
            if (string.IsNullOrEmpty(user.ManagerId))
            {
                TempData["error"] = "Bu kullanıcıya yönetici atanmamış. Lütfen ilgili birimlerle iletişime geçin";

            
            }
            else
            {
                WorkReportDto workReportDto = new WorkReportDto()
                {
                    Description = paginationModel.Data.Description,
                    ReportDate = DateTime.Now,
                    Title = paginationModel.Data.Title,
                    UserId = accountDto.Id,
                    ReviewerId = user.ManagerId
                };
                await _workReportService.AddAsync(workReportDto);
            }
            return RedirectToAction("Index");
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "ManagerOnly")]
        public async Task<IActionResult> Reports(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var workReports = _workReportService.Pagination(id, x => x.ReviewerId == accountDto.Id);
            foreach (var workReport in workReports)
            {
                workReport.User = await _userService.FindAsync(workReport.UserId);
            }
            WorkReportPageModel workReportPageModel = new WorkReportPageModel();
            PaginationModel<WorkReport, WorkReportPageModel> paginationModel = new PaginationModel<WorkReport, WorkReportPageModel>()
            {
                Data = workReportPageModel,
                Dataset = workReports,
                PartialPaginationModel = new PartialPaginationModel() { Count = _workReportService.Where(x => x.UserId == accountDto.Id).Count },
            };
            return View(paginationModel);
        }
    }
}
