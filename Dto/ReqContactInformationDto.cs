namespace StationeryStore.Dto
{
    public class ReqContactInformationDto
    {
        public string Type { get; set; } // Add this property to hold the type of contact information
        public string Value { get; set; } // Phone Number, Email Address, Social Media Link
        public string Description { get; set; }
    }
}
