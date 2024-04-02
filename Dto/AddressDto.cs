using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class AddressDto
    {
        public string Title { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
    }
}
