using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ImageAttributeRepository : BaseRepository, IImageAttributeRepository
    {
        public ImageAttributeRepository(DataContext context) : base(context)
        {
        }

        public void AddImage(ImageAttribute imageAttribute)
        {
            _context.ImageAttributes.Add(imageAttribute);
            _context.SaveChanges();
        }
    }
}
