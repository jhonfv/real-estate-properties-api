namespace properties.Application.DTOs.Properties
{
    public class PropertyDTO
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
