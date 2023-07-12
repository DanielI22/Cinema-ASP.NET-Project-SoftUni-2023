namespace CinemaSystem.Services.Data.Interfaces
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Web.ViewModels.Review;
    using System;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        Task<Review> GetReviewByIdAsync(Guid reviewId);
        Task DeleteReviewAsync(Guid reviewId);
        Task PostReviewAsync(Guid movieId, Guid userId, string reviewText);
        Task<bool> IsReviewCreatorAsync(Guid reviewId, Guid userId);
        Task<IEnumerable<ReviewViewModel>> GetMovieReviewsPerPageAsync(Guid movieId, int pageNumber, int pageSize);
        Task<int> GetTotalMovieReviwsCount(Guid movieId);
    }
}
