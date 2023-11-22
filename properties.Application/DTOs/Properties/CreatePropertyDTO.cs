namespace properties.Application.DTOs.Properties
{
    public class CreatePropertyDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
