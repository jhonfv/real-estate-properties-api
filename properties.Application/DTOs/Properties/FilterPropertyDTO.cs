namespace properties.Application.DTOs.Properties
{
    public class FilterPropertyDTO
    {
        public string? Name { get; set; }
        public string? CodeInternal { get; set; }
        public string? Location { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? Year { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
