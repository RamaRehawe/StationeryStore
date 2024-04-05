using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class RateRepository : BaseRepository, IRateRepository
    {
        public RateRepository(DataContext context) : base(context)
        {
        }

        public bool AddRate(Rate rate)
        {
            _context.Rates.Add(rate);
            return Save();
        }

        public double? GetAverageRatingForProduct(int productId)
        {
            var ratings = _context.Rates.Where(r => r.Id == productId)
                .Select(r => r.Rating)
                .ToList();
            if(ratings.Any())
            {
                // Calculate the sum of ratings and divide by the count
                double averageRating = ratings.Average();
                return averageRating;
            }
            else 
                return null;
        }

        public Rate GetRateByUserAndProduct(int userId, int productId)
        {
            return _context.Rates.Where(r => r.UserId == userId)
                .Where(r => r.ProductId == productId)
                .FirstOrDefault();
        }

        public IEnumerable<Rate> GetRatingsForProduct(int productId)
        {
            return _context.Rates
                .Where(r => r.ProductId == productId)
                .ToList();
        }

        public bool RateExist(int userId, int productId)
        {
            return _context.Rates.Any(r => r.UserId == userId && r.ProductId == productId);
        }

        public bool UpdateRate(Rate rate)
        {
            _context.Rates.Update(rate);
            return Save();
        }
    }
}
