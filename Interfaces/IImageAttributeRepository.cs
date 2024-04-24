using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IImageAttributeRepository : IBaseRepository
    {
        void AddImage(ImageAttribute imageAttribute);
    }
}
