using Microsoft.AspNetCore.Http;
using properties.Application.Common;
using properties.Application.DTOs.Properties;
using properties.Domain.Entities;

namespace properties.Application.Interfaces
{
    public interface IPropertyServices
    {
        /// <summary>
        /// Create a new property
        /// </summary>
        /// <param name="createPropertyDTO"></param>
        /// <returns>
        /// <see cref="Result{T}"/>
        /// <code>
        /// 200 OK
        /// </code>
        /// <code>
        /// 400 Error</code>
        /// </returns>
        public Task<Result<PropertyDTO>> createAsync(CreatePropertyDTO createPropertyDTO);

        /// <summary>
        /// Change property price by idProperty
        /// </summary>
        /// <param name="price">new price property</param>
        /// <param name="idProperty">id property to change</param>
        /// <returns>
        /// <see cref="Result{T}"/>
        /// <code>200 Ok</code>
        /// <code>400 Error</code>
        /// </returns>
        public Task<Result<PropertyDTO>> changePriceAsync(double price, int idProperty);

        /// <summary>
        /// UPdate property by id
        /// </summary>
        /// <param name="propertyDTO"></param>
        /// <returns>
        /// <see cref="Result{T}"/>
        /// <code>200 Ok</code>
        /// <code>400 Error</code>
        /// </returns>
        public Task<Result<PropertyDTO>> UpdateAsync(PropertyDTO propertyDTO);

        /// <summary>
        /// Add new image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="idProperty"></param>
        /// <returns>
        /// <see cref="Result{PropertyImageDTO}"/>
        /// <code>200 Ok</code>
        /// <code>400 Error</code>
        /// </returns>
        public Task<Result<PropertyImageDTO>> addImageAsync(IFormFile image, int idProperty);

        /// <summary>
        /// Get properties by filters
        /// </summary>
        /// <param name="FilterPropertyDTO"></param>
        /// <returns>
        /// <see cref="Result{T}"/>
        /// <code>200 Ok</code>
        /// <code>400 Error</code>
        /// </returns>
        public Task<Result<IEnumerable<PropertyDTO>>> getByFiltersAsync(FilterPropertyDTO FilterPropertyDTO);

    }
}
