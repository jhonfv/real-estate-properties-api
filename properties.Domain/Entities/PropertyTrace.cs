namespace properties.Domain.Entities
{
    public class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public double Tax { get; set; }
        public int IdProperty { get; set; }
    }
}