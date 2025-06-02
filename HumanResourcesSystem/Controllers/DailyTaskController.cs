using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]

    public class DailyTaskController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IService<DailyTask, DailyTaskDto> _dailyTaskService;
        private readonly IUserService _userService;
        public DailyTaskController(IAuthService authService, IService<DailyTask,DailyTaskDto> dailyTaskService, IUserService userService)
        {
            _authService = authService;
            _dailyTaskService = dailyTaskService;
            _userService = userService;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var user = await _userService.FindAsync(accountDto.Id);
            if (await _authService.HasRole("ADMIN",user))
            {
                return Redirect("/Admin");
            }
            ViewData["HasRole"] = await _authService.HasRole("manager", user);
            var dailyTasks = _dailyTaskService.Pagination(id, x => x.UserId == accountDto.Id && 
            x.Date.Value.Date == DateTime.UtcNow.Date);
            var dailyTaskPageModel = new DailyTaskPageModel();
            PaginationModel<DailyTask, DailyTaskPageModel> paginationModel = new PaginationModel<DailyTask, DailyTaskPageModel>()
            {
                PartialPaginationModel = new PartialPaginationModel() { Count = _dailyTaskService.Where(x => x.UserId == accountDto.Id &&
            x.Date.Value.Date == DateTime.UtcNow.Date).Count },
                Dataset = dailyTasks,
                Data = dailyTaskPageModel
            };
            return View(paginationModel);
        }
        public async Task<IActionResult> Add(PaginationModel<DailyTask, DailyTaskPageModel> paginationModel)
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            DailyTaskDto dailyTaskDto = new DailyTaskDto()
            {
                isFinished = false,
                UserId = accountDto.Id,
                Date = DateTime.UtcNow.Date,
                Description = paginationModel.Data.Description,
                Name = paginationModel.Data.Name,
            };
            await _dailyTaskService.AddAsync(dailyTaskDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MarkAsFinished(string taskId)
        {
            DailyTask dailyTask = await _dailyTaskService.FindAsync(taskId);
            dailyTask.isFinished = true;
            await _dailyTaskService.UpdateAsync(dailyTask);
            return RedirectToAction("Index");
        }
    }
}
