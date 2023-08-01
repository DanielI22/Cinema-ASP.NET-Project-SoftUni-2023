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
        public async Task<IActionResult> Post(string movieId, string ReviewToAdd)
        {
            string reviewError = "Your Review could not be post. Sorry for the inconvenience!";
            if (ModelState.IsValid)
            {
                try
                {
                    string? userId = User.GetId();
                    if (userId != null)
                    {
                        await reviewService.PostReviewAsync(movieId, userId, ReviewToAdd);
                    }
                    else
                    {
                        TempData[ErrorMessage] = reviewError;
                    }
                }
                catch (Exception)
                {
                    TempData[ErrorMessage] = reviewError;
                }
            }
            else
            {
                TempData[ErrorMessage] = reviewError;
            }

            return RedirectToAction("Details", "Movie", new { id = movieId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMy(string reviewId, string movieId)
        {
            string reviewError = "Your Review could not be deleted. Sorry for the inconvenience!";
            if (reviewId == null || movieId == null)
            {
                TempData[ErrorMessage] = reviewError;
                return RedirectToAction("All", "Movies");
            }
            try
            {
                string? userId = User.GetId();
                if (userId != null && (await reviewService.IsReviewCreatorAsync(reviewId, userId) || User.IsAdmin()))
                {
                    await reviewService.DeleteReviewAsync(reviewId);
                }
                else
                {
                    TempData[ErrorMessage] = reviewError;
                }
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = reviewError;
            }
            return RedirectToAction("Details", "Movie", new { id = movieId });
        }
    }
}
