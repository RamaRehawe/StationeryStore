namespace StationeryStore.Dto
{
    public class ReqCustomerServiceDto
    {
        public bool Type { get; set; } // 1->general, 0->complain
        public string Details { get; set; }

    }
}
