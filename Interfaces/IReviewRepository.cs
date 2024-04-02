using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IReviewRepository : IBaseRepository
    {
        ICollection<Review> GetReviews(int proId);
        Review GetReviewByUserAndProduct(int userId, int proId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);

    }
}
