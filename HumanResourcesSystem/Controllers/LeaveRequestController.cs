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
        [HttpPost]
        public async Task<IActionResult> Add(LeaveRequestPageModel leaveRequestPageModel)
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var user = await _userService.FindAsync(accountDto.Id);
            var manager = await _userService.FindAsync(user.ManagerId);
            LeaveRequestDto leaveRequestDto = new LeaveRequestDto()
            {
                EndDate = (DateTime)leaveRequestPageModel.EndDate!,
                StartDate = (DateTime)leaveRequestPageModel.StartDate!,
                IsAccepted = null,
                ManagerId = manager.Id,
                Reason = leaveRequestPageModel.Reason,
                UserId = user.Id,
            };
            await _leaveRequestService.AddAsync(leaveRequestDto);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var leaveRequests = _leaveRequestService.Where(x => x.EndDate, x => x.UserId == accountDto.Id);
            LeaveRequestPageModel leaveRequestPageModel = new LeaveRequestPageModel()
            {
                LeaveRequests = leaveRequests,
            };
            return View(leaveRequestPageModel);
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication",Roles = "manager")]
        public IActionResult Requests()
        {
            return View();
        }

    }
}
