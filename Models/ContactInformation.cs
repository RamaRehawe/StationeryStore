namespace StationeryStore.Models
{
    public class ContactInformation
    {
        public int Id { get; set; }
        public string Type { get; set; } // Phone, Email, SocialMedia
        public string Value { get; set; } // Phone Number, Email Address, Social Media Link
        public string Description { get; set; }
    }
}
