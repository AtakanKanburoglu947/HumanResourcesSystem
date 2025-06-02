using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IService<Department,DepartmentDto> _departmentService;
        private readonly IService<Company,CompanyDto> _companyService; 
        private readonly IService<PerformanceReview,PerformanceReviewDto> _performanceReviewService;
        private readonly IService<LeaveRequest,LeaveRequestDto>  _leaveRequestService;
        private readonly IAuthService _authService;

        public AdminController(IUserService userService, IService<Department,DepartmentDto> departmentService,
            IService<Company,CompanyDto> companyService, IAuthService authService, IService<PerformanceReview,PerformanceReviewDto> performanceReviewService,
            IService<LeaveRequest,LeaveRequestDto> leaveRequestService)
        {
            _userService = userService;
            _departmentService = departmentService;
            _companyService = companyService;
            _authService = authService;
            _performanceReviewService = performanceReviewService;
            _leaveRequestService = leaveRequestService;
        }
        public IActionResult Index(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            var userCount = _userService.GetAll().Count;
            var users = _userService.Pagination(id, null);
            PaginationModel<User, NoData> paginationModel = new PaginationModel<User, NoData>()
            {
                PartialPaginationModel = new PartialPaginationModel() { Count = userCount },
                Dataset = users,
            };
            return View(paginationModel);
        }
        public async Task<IActionResult> User(string id)
        {

            var user = await _userService.FindAsync(id);
            var company = await _companyService.FirstOrDefault(x=>x.Id == user.CompanyId);
            var manager = await _userService.FirstOrDefault(x=>x.ManagerId == user.ManagerId);
            var companies =  _companyService.GetAll();
            var departments = _departmentService.Where(x=>x.CompanyId == user.CompanyId);
            var department = await _departmentService.FirstOrDefault(x=>x.Id == user.DepartmentId);
            var managers = await _authService.UsersWithRole("MANAGER");
            var managerList = managers.Where(x=>x.CompanyId == user.CompanyId).Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = $"{x.FirstName} {x.LastName}",
                Selected = (user.ManagerId == x.Id)
            }).ToList();
            var companyList = companies.Select(x => new SelectListItem
            {
                 Value = x.Id,
                 Text = x.Name,
                 Selected = (user.CompanyId == x.Id)
            }).ToList();
            var departmentList = departments.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name,
                Selected = (user.DepartmentId == x.Id)  
            }).ToList();
            UserPageModel userPageModel = new UserPageModel()
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HireDate = user.HireDate,
                ManagerId = user.ManagerId,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                Company = company,
                Manager = manager,
                Department = department,
                Managers = managers.ToList(),
                ManagerList = managerList,
                CompanyList = companyList,
                DepartmentList = departmentList
               
            };
            return View(userPageModel);
        }
        public async Task<IActionResult> Change(UserPageModel userPageModel)
        {
            var user = new User
            {
                Id = userPageModel.Id,
                BirthDate = userPageModel.BirthDate,
                FirstName = userPageModel.FirstName,
                LastName = userPageModel.LastName,
                HireDate = userPageModel.HireDate,
                ManagerId = userPageModel.ManagerId,
                CompanyId = userPageModel.CompanyId,
                DepartmentId = userPageModel.DepartmentId
            };

            await _userService.Update(user);
            return RedirectToAction($"User","Admin",userPageModel);
        }
     

    }
}
