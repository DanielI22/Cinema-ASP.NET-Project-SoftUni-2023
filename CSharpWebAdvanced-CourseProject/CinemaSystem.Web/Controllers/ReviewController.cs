namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid movieId, string ReviewToAdd)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid userId = User.GetId();
                    await reviewService.PostReviewAsync(movieId, userId, ReviewToAdd);
                }
                catch (Exception)
                {
                    TempData[ErrorMessage] = "Your Review could not be post. Sorry for the inconvenience!";
                }
            }
            else
            {
                TempData[ErrorMessage] = "Your Review could not be post. Sorry for the inconvenience!";
            }

            return RedirectToAction("Details", "Movie", new { id = movieId });
        }
    }
}
