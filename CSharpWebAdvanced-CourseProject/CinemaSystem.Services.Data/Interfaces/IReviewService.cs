namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Web.ViewModels.Review;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task DeleteReviewAsync(string reviewId);
        Task PostReviewAsync(string movieId, string userId, string reviewText);
        Task<bool> IsReviewCreatorAsync(string reviewId, string userId);
        Task<IEnumerable<ReviewViewModel>> GetMovieReviewsPerPageAsync(string movieId, int pageNumber, int pageSize);
        Task<int> GetTotalMovieReviewsCount(string movieId);
    }
}
