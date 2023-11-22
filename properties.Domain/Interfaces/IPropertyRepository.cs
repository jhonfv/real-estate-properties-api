using properties.Domain.Entities;
using properties.Domain.Filters;

namespace properties.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        public Task<Property> CreateAsync(Property property);
        public Task<IEnumerable<Property>> getAllAsync();
        public Task<Property> getByIdAsync(int idProperty);
        public Task<IEnumerable<Property>> getByFiltersAsync(FilterProperty filterProperty);

        public Task<Property> UpdateAsync(Property property);
        public Task<Property> ChangePriceAsync(Property property);

        public Task<PropertyImage> AddImageAsync(PropertyImage property);

        public Task<PropertyTrace> AddTrace(PropertyTrace property);


    }
}
