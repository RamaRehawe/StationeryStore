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

        public string GetImages(int productId)
        {
            var images = _context.ImageAttributes
                .Where(i => i.ProductAttributeQuantityId == productId)
                          .FirstOrDefault();

            // Check if images is null before accessing its properties
            if (images != null)
            {
                return images.URL;
            }
            else
            {
                // Handle the case where no image is found for the given product ID
                // For example, you can return a default image URL or throw an exception
                return "default-image-url.jpg";
            }
        }
    }
}
