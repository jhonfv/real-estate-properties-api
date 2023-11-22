namespace properties.Application.DTOs.Properties
{
    public class PropertyImageDTO
    {
        public int IdProperty { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}
