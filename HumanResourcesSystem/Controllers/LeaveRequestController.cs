using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using HumanResourcesSystemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]
    public class LeaveRequestController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IService<LeaveRequest,LeaveRequestDto> _leaveRequestService;
        private readonly IUserService _userService;
        public LeaveRequestController(IAuthService authService, IService<LeaveRequest,LeaveRequestDto> leaveRequestService, IUserService userService)
        {
            _authService = authService;
            _leaveRequestService = leaveRequestService;
            _userService = userService;

        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "UserOnly")]

        [HttpPost]
        public async Task<IActionResult> Add(PaginationModel<LeaveRequest, LeaveRequestPageModel> paginationModel)
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var user = await _userService.FindAsync(accountDto.Id);
            var manager = await _userService.FindAsync(user.ManagerId);
            if (manager != null)
            {
                LeaveRequestDto leaveRequestDto = new LeaveRequestDto()
                {
                    EndDate = (DateTime)paginationModel.Data.EndDate!,
                    StartDate = (DateTime)paginationModel.Data.StartDate!,
                    IsAccepted = null,
                    ManagerId = manager.Id,
                    Reason = paginationModel.Data.Reason,
                    UserId = user.Id,
                };
                await _leaveRequestService.AddAsync(leaveRequestDto);
            }
            else
            {
                TempData["error"] = "Bu kullanıcıya yönetici atanmamış. Lütfen ilgili birimlerle iletişime geçin";
            }
            
            return RedirectToAction("Index");
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
            var leaveRequests = _leaveRequestService.Pagination(id, x => x.UserId == accountDto.Id);
            LeaveRequestPageModel leaveRequestPageModel = new LeaveRequestPageModel();
            PaginationModel<LeaveRequest, LeaveRequestPageModel> paginationModel = new PaginationModel<LeaveRequest, LeaveRequestPageModel>()
            {
                Data = leaveRequestPageModel,
                Dataset = leaveRequests,
                PartialPaginationModel = new PartialPaginationModel() { Count = _leaveRequestService.Where(x=>x.UserId == accountDto.Id).Count }
            };

            return View(paginationModel);
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "ManagerOnly")]
        public async Task<IActionResult> Requests(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var leaveRequests = _leaveRequestService.Pagination(id, x=>x.ManagerId == accountDto.Id);
            foreach (var leaveRequest in leaveRequests)
            {
                leaveRequest.User = await _userService.FindAsync(leaveRequest.UserId);
            }
            LeaveRequestPageModel leaveRequestPageModel = new LeaveRequestPageModel();
            PaginationModel<LeaveRequest, NoData> paginationModel = new PaginationModel<LeaveRequest, NoData>()
            {
                Dataset = leaveRequests,
                PartialPaginationModel = new PartialPaginationModel() { Count = _leaveRequestService.Where(x => x.UserId == accountDto.Id).Count }
            };
            return View(paginationModel);
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "ManagerOnly")]
        public async Task<IActionResult> Accept(string id)
        {
            var leaveRequest = await _leaveRequestService.FindAsync(id);
            leaveRequest.IsAccepted = true;
            await _leaveRequestService.UpdateAsync(leaveRequest);
            return RedirectToAction("Requests");
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "ManagerOnly")]
        public async Task<IActionResult> Reject(string id)
        {
            var leaveRequest = await _leaveRequestService.FindAsync(id);
            leaveRequest.IsAccepted = false;
            await _leaveRequestService.UpdateAsync(leaveRequest);
            return RedirectToAction("Requests");
        }

    }
}
