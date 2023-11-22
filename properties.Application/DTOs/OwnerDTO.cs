namespace properties.Application.DTOs
{
    public class OwnerDTO
    {
        public int? IdOwner { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
    }
}
