using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResCartDto
    {
        public ICollection<CartItemDto> CartItems { get; set; }
        public double TotalPrice { get; set; }

    }
}
