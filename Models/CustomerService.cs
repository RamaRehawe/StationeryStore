namespace StationeryStore.Models
{
    public class CustomerService
    {
        public int Id { get; set; }
        public bool Type { get; set; } // 1->general, 0->complain
        public string Details { get; set; }
        public User UserId { get; set; }
    }
}
