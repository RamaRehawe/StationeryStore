using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResProductAttributeDto
    {
        public string Value { get; set; }
        public int ProductAttributeQuantityId { get; set; }
        public int AttributeId { get; set; }
        public ResAttributeDto Attribute { get; set; }
    }
}
