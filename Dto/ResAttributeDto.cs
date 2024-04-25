namespace StationeryStore.Dto
{
    public class ResAttributeDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<ResProductAttributeDto> ProductAttributes { get; set; }
    }
}
