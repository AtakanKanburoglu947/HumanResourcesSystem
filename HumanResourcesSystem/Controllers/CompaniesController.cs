using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "AdminOnly")]
    public class CompaniesController : Controller
    {
        private readonly IService<Company,CompanyDto> _companyService;
        private readonly IService<Department,DepartmentDto> _departmentService;
        private readonly IUserService _userService;
        public CompaniesController(IService<Company,CompanyDto> companyService, IService<Department, DepartmentDto> departmentService, IUserService userService)
        {
            _companyService = companyService;
            _departmentService = departmentService;
            _userService = userService;
        }
        public IActionResult Index(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            var companies = _companyService.Pagination(id, null);
            
            PaginationModel<Company, NoData> paginationModel = new PaginationModel<Company, NoData>()
            {
                PartialPaginationModel = new PartialPaginationModel() { Count = _companyService.GetAll().Count },
                Dataset = companies
            };
            return View(paginationModel);
        }
        public async Task<IActionResult> Company(string id)
        {
           var company = await _companyService.FindAsync(id);
            var companyPageModel = new CompanyPageModel()
            {
                Id = company.Id,
                Name = company.Name,
                Departments = _departmentService.Where(x => x.CompanyId == company.Id),
                Employees = _userService.Where(x => x.CompanyId == company.Id),
                Description = company.Description,
                Industry = company.Industry,
        };
           return View(companyPageModel);
        }
        public async Task<IActionResult> AddNewDepartment(CompanyPageModel companyPageModel)
        {
            var departments = _departmentService.Where(x => x.CompanyId == companyPageModel.Id);
            foreach (var department in departments)
            {
                if (department.Name == companyPageModel.NewDepartment)
                {
                    throw new Exception("Departman önceden tanımlanmış");
                }
            }
            var newDepartment = new DepartmentDto()
            {
                CompanyId = companyPageModel.Id,
                Name = companyPageModel.NewDepartment
            };
            await _departmentService.AddAsync(newDepartment);

            return View(companyPageModel);
        }
        public async Task<IActionResult> Change(CompanyPageModel companyPageModel)
        {
            Company company = new Company()
            {
                Id = companyPageModel.Id,
                Name = companyPageModel.Name,
                Departments = companyPageModel.Departments,
                Description = companyPageModel.Description,
                Employees = companyPageModel.Employees,
                Industry = companyPageModel.Industry
            };
            await _companyService.UpdateAsync(company);

            return View(company);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartmentName([FromBody] EditDepartmentRequest request)
        {
            var department = await _departmentService.FindAsync(request.Id);
            if (department == null)
                return NotFound();

            department.Name = request.NewName;
            await _departmentService.UpdateAsync(department);
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDepartment([FromBody] DeleteDepartmentRequest request)
        {
            var department = await _departmentService.FindAsync(request.Id);
            if (department == null)
                return NotFound();

            await _departmentService.RemoveAsync(request.Id);
            return Ok();
        }
        public IActionResult AddCompany(CompanyPageModel companyPageModel)
        {
            return View(companyPageModel);
        }
        public async Task<IActionResult> Add(CompanyPageModel companyPageModel)
        {
            CompanyDto companyDto = new CompanyDto()
            {
                Name = companyPageModel.Name,
                Description = companyPageModel.Description,
                Industry = companyPageModel.Industry,
            };
            await _companyService.AddAsync(companyDto);
            return Ok();
        }
    
    }
    public class EditDepartmentRequest
    {
        public string Id { get; set; }
        public string NewName { get; set; }
    }
    public class DeleteDepartmentRequest
    {
        public string Id { get; set; }
    }
}
