namespace StationeryStore.Dto
{
    public class ResAttributeDto
    {
        public string Name { get; set; }
        public ICollection<ResProductAttributeDto> ProductAttributes { get; set; }
    }
}
