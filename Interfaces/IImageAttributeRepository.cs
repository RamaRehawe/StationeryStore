using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IImageAttributeRepository : IBaseRepository
    {
        void AddImage(ImageAttribute imageAttribute);
        ICollection<ImageAttribute> GetImages(int productId);
    }
}
