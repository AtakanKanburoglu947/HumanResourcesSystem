using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    public class PerformanceReviewController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IService<PerformanceReview, PerformanceReviewDto> _performanceReviewService;
       
        public PerformanceReviewController(IAuthService authService, IUserService userService,
            IService<PerformanceReview, PerformanceReviewDto> performanceReviewService)
        {
            _authService = authService;
            _userService = userService;
            _performanceReviewService = performanceReviewService;
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "UserOnly")]
        public async Task<IActionResult> Index(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var performanceReviews = _performanceReviewService.Pagination(id, x=>x.UserId == accountDto.Id);
            if (performanceReviews.Count > 0)
               
            {
                var reviewer = await _userService.FindAsync(performanceReviews[0].ReviewerId);
                performanceReviews.ForEach(x => x.Reviewer = reviewer);
            }
            
            PaginationModel<PerformanceReview, NoData> paginationModel = new PaginationModel<PerformanceReview, NoData>()
            {
                PartialPaginationModel = new PartialPaginationModel() { Count = _performanceReviewService.Where(x=>x.UserId == accountDto.Id).Count },
                Dataset = performanceReviews
            };

            return View(paginationModel);
        }

        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "ManagerOnly")]
        public IActionResult Review(int id)
        {
            ViewData["id"] = id;
            if (id > 0)
            {
                id *= 5;
            }
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var performanceReviews = _performanceReviewService.Pagination(id, x => x.ReviewerId == accountDto.Id);
            var users = _userService.Where(x => x.ManagerId == accountDto.Id);
            var names = new List<string>();
            foreach (var user in users)
            {
                names.Add(user.UserName);
            }
            PerformanceReviewPageModel performanceReviewPageModel = 
                new PerformanceReviewPageModel { 
                    PerformanceReviews =  performanceReviews, 
                    Names = names, Users = users };
            PaginationModel<PerformanceReview, PerformanceReviewPageModel> paginationModel = new PaginationModel<PerformanceReview, PerformanceReviewPageModel>()
            {
                PartialPaginationModel = new PartialPaginationModel() { Count = _performanceReviewService.Where(x => x.UserId == accountDto.Id).Count },
                Dataset = performanceReviews
            };

            return View(paginationModel);
        }
        [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication", Policy = "ManagerOnly")]
        public async Task<IActionResult> Add(PerformanceReviewPageModel performanceReviewPageModel)
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var users = _userService.Where(x => x.ManagerId == accountDto.Id);
            var user = users.FirstOrDefault(x => x.UserName == performanceReviewPageModel.SelectedUserName);

            var performanceReviewDto = new PerformanceReviewDto()
            {
                Feedback = performanceReviewPageModel.Feedback,
                ReviewDate = DateTime.UtcNow,
                ReviewerId = accountDto.Id,
                Score = performanceReviewPageModel.Score,
                UserId = user.Id
            };
            await _performanceReviewService.AddAsync(performanceReviewDto);
            return RedirectToAction("Review");
        }
    }
}
