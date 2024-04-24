namespace StationeryStore.Dto
{
    public class CheckOrderDto
    {
        public int OrderId { get; set; }
        public bool Checked { get; set; }
        public string? FailDeliver { get; set; }
    }
}
