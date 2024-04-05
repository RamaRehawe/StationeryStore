using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IRateRepository : IBaseRepository
    {
        double? GetAverageRatingForProduct(int productId);
        Rate GetRateByUserAndProduct(int userId, int productId);
        bool RateExist (int userId, int productId);
        bool AddRate(Rate rate);
        bool UpdateRate(Rate rate);
        IEnumerable<Rate> GetRatingsForProduct(int productId);
    }
}
