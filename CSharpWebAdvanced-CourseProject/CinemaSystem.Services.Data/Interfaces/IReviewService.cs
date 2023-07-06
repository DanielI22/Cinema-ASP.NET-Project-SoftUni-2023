namespace CinemaSystem.Services.Data.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        Task PostReviewAsync(Guid movieId, Guid userId, string reviewText);
    }
}
