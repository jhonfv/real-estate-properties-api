namespace properties.Domain.Entities
{
    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}
