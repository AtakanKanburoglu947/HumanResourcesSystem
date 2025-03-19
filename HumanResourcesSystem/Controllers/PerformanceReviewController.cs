using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]
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
        public IActionResult Index()
        {
            AccountDto accountDto = _authService.GetAccountDetailsFromToken();
            var performanceReviews = _performanceReviewService.Where(x => x.ReviewDate, x=>x.UserId == accountDto.Id);
            return View(performanceReviews);
        }
    }
}
