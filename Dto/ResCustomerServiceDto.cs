namespace StationeryStore.Dto
{
    public class ResCustomerServiceDto
    {
        public bool Type { get; set; } // 1->general, 0->complain
        public bool? AdminResponse { get; set; } = true; // 1-> working progress, 0->Done
        public string Details { get; set; }
    }
}
