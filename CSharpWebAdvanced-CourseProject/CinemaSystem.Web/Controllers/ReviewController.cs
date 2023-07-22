namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data;
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
                        TempData[ErrorMessage] = "Your Review could not be post. Sorry for the inconvenience!";
                    }
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

        [HttpPost]
        public async Task<IActionResult> DeleteMy(string reviewId, string movieId)
        {
            try
            {
                string? userId = User.GetId();
                if(userId != null && (await reviewService.IsReviewCreatorAsync(reviewId, userId) || User.IsAdmin()))
                {
                    await reviewService.DeleteReviewAsync(reviewId);
                }
                else
                {
                    TempData[ErrorMessage] = "Your Review could not be deleted. Sorry for the inconvenience!";
                }
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Your Review could not be deleted. Sorry for the inconvenience!";
            }
            return RedirectToAction("Details", "Movie", new { id = movieId });
        }
    }
}
