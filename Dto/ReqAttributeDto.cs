namespace StationeryStore.Dto
{
    public class ReqAttributeDto
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public string Name1 { get; set; }
        public string Value1{ get; set; }
        public string? Name2 { get; set; }
        public string? Value2{ get; set; }
        //public int ProductAttributeQuantityId { get; set; }
        //public string URL { get; set; }
    }
}
