namespace StationeryStore.Dto
{
    public class ResOrderItemDto
    {
        public string Name { get; set; }
        public string Atribute { get; set; }
        public string Value { get; set; }
        public string? Atribute2 { get; set; }
        public string? Value2 { get; set; }
        public int ProductAttributeQuantityId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
