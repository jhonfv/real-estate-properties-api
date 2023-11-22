using properties.Domain.Entities;

namespace properties.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        public Task<Owner> CreateAsync(Owner owner);
        public Task<IEnumerable<Owner>> GetAllAsync();
    }
}
