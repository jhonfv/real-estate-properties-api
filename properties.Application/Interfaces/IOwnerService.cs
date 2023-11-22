using properties.Application.Common;
using properties.Application.DTOs;

namespace properties.Application.Interfaces
{
    public interface IOwnerService
    {

        /// <summary>
        /// Get all owners avalibles
        /// </summary>
        /// <returns>
        /// <see cref="Result{T}"/>
        /// <code>Code 200 is OK</code>
        /// <code>Code 204 if not content avalible</code>
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Result<IEnumerable<OwnerDTO>>> getAllAsync();

        /// <summary>
        /// Create new Owner
        /// </summary>
        /// <param name="ownerDTO"></param>
        /// <returns>
        /// <see cref="Result{T}"/>
        /// <code>Code 200 is OK</code>
        /// <code>Code 400 if not insert owner</code>
        /// </returns>
        public Task<Result<OwnerDTO>> CreateAsync(OwnerDTO ownerDTO);
    }
}
