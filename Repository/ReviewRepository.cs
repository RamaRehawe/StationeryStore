using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(DataContext context) : base(context) { }

        public bool CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            return Save();
        }

        public Review GetReviewByUserAndProduct(int userId, int proId)
        {
            return _context.Reviews.Where(r => r.UserId == userId)
                .Where(r => r.ProductId == proId)
                .FirstOrDefault();
        }

        public ICollection<Review> GetReviews(int proId)
        {
            return _context.Reviews.Where(r => r.ProductId == proId).ToList();
        }

        public bool UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            return Save();
        }
    }
}
