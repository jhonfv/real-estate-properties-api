namespace properties.Domain.Filters
{
    public class FilterProperty
    {
        public string? Name { get; set; }
        public string? CodeInternal { get; set; }
        public string? Location { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? Year { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
