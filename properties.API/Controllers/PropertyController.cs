using Microsoft.AspNetCore.Mvc;
using properties.Application.Common;
using properties.Application.DTOs;
using properties.Application.DTOs.Properties;
using properties.Application.Interfaces;

namespace properties.API.Controllers
{
    /// <summary>
    /// Property Service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        readonly IPropertyServices _propertyServices;
        /// <summary>
        /// ctor 
        /// </summary>
        /// <param name="propertyServices"></param>
        public PropertyController(IPropertyServices propertyServices)
        {
            _propertyServices = propertyServices;
        }

        /// <summary>
        /// Get properties list
        /// </summary>
        /// <remarks>Get a list of properties based on filters</remarks>
        /// <returns>List of properties</returns>
        /// <response code="200">Return list properties</response>
        /// <response code="400">Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<PropertyDTO>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<IEnumerable<PropertyDTO>>))]
        [HttpGet("getByFilters")]
        public async Task<IActionResult> getByFilters([FromQuery] FilterPropertyDTO filterPropertyDTO)
        {
            var properties = await _propertyServices.getByFiltersAsync(filterPropertyDTO);
            if(properties != null)
            {
                return Ok(properties);
            }
            else
            {
                return BadRequest(properties);
            }
        }

        /// <summary>
        /// Add new property
        /// </summary>
        /// <remarks>Create a new property</remarks>
        /// <returns>new property added</returns>
        /// <response code="200">Return property</response>
        /// <response code="400">Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<PropertyDTO>))]
        [HttpPost("create")]
        public async Task<IActionResult> createAsync(CreatePropertyDTO createPropertyDTO)
        {
            var property = await _propertyServices.createAsync(createPropertyDTO);
            if (property.IsSuccess)
            {
                return Ok(property);
            }
            else
            {
                return BadRequest(property);
            }

        }

        /// <summary>
        /// Add new image
        /// </summary>
        /// <remarks>Add a new image to a property</remarks>
        /// <returns>new image added</returns>
        /// <response code="200">Return image info</response>
        /// <response code="400">Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyImageDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<PropertyImageDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<string>))]
        [HttpPost("addImage")]
        public async Task<IActionResult> addImage(IFormFile image, int idProperty)
        {
            var maxUpload=1 *1024 *1024; // max 5 MB
            if (image.Length > maxUpload)
            {
                var responseImage = new Result<string>().Failed(new List<string> { $"File size {(image.Length / 1024 / 1024)}MB no valid. MaxSize:{maxUpload/1024/1024} MB" });
                return BadRequest(responseImage);
            }

            var allowedTypeImage = new List<string> { "image/jpeg", "image/png", "image/webpg" };
            if (!allowedTypeImage.Contains(image.ContentType))
            {
                var responseImage = new Result<string>().Failed(new List<string> { $"File type {image.ContentType} no valid. allowed Type:{String.Join(",", allowedTypeImage)}" });
                return BadRequest(responseImage);
            }


            var response = await _propertyServices.addImageAsync(image, idProperty);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }          

        }

        /// <summary>
        /// Update property
        /// </summary>
        /// <remarks>Update property attributes by id</remarks>
        /// <returns>new property attributes</returns>
        /// <response code="200">Return property</response>
        /// <response code="400">Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<PropertyDTO>))]
        [HttpPut("update")]
        public async Task<IActionResult> update(PropertyDTO propertyDTO)
        {
            var propertyChange = await _propertyServices.UpdateAsync(propertyDTO);
            if (propertyChange.IsSuccess)
            {
                return Ok(propertyChange);
            }
            else
            {
                return BadRequest(propertyChange);
            }
        }

        /// <summary>
        /// Change price
        /// </summary>
        /// <remarks>Change the price of a property</remarks>
        /// <returns>update property</returns>
        /// <response code="200">Return property</response>
        /// <response code="400">Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<PropertyDTO>))]
        [HttpPut("changePrice")]
        public async Task<IActionResult> changePrice(double price, int idProperty)
        {
            var propertyChange = await _propertyServices.changePriceAsync(price, idProperty);
            if (propertyChange.IsSuccess)
            {
                return Ok(propertyChange);
            }
            else
            {
                return BadRequest(propertyChange);
            }

        }

    }
}
