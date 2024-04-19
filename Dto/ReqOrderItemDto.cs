namespace StationeryStore.Dto
{
    public class ReqOrderItemDto
    {
        public int ProductAttributeQuantityId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
